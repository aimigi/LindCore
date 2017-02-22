using LindCore.Commons;
using LindCore.LindLogger;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LindCore.RedisClient
{
    /// <summary>
    /// 测试中，没有完成
    /// Redis提供者
    /// 实例－实现方式
    /// </summary>
    public class RedisProvider : DisposableBase
    {
        ConnectionMultiplexer redis;
        ConfigurationOptions option;
        static int DefaultPoolSize = 5;
        static Dictionary<Thread, ConnectionMultiplexer> RedisPools = new Dictionary<Thread, ConnectionMultiplexer>();
        static object clientLock = new object();
        public IDatabase Db
        {
            get
            {
                return redis.GetDatabase();
            }
        }

        public IDatabase GetClient()
        {
            return redis.GetDatabase();
        }
        /// <summary>
        /// 生产一个redis连接实例
        /// </summary>
        /// <param name="conn">连接串，可以是sentinel集合</param>
        /// <param name="password">redis数据服务器密码</param>
        /// <param name="serviceName">sentinel模式下的服务名称</param>
        /// <param name="isSentinel">是否连接为sentinel</param>
        public RedisProvider(
            string conn,
            string password = "",
            string serviceName = "",
            bool isSentinel = false)
        {
            if (RedisPools.Count < 5)
            {
                redis = ConnectionMultiplexer.Connect(conn);

                if (isSentinel)
                {
                    option = new ConfigurationOptions();
                    var configArr = conn.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    configArr.ToList().ForEach(i => option.EndPoints.Add(i));
                    option.TieBreaker = "";//这行在sentinel模式必须加上
                    option.CommandMap = CommandMap.Sentinel;

                    for (int i = 0; i < option.EndPoints.Count; i++)
                    {
                        try
                        {
                            conn = redis.GetServer(option.EndPoints[i]).SentinelGetMasterAddressByName(serviceName).ToString();
                            redis = ConnectionMultiplexer.Connect(option + ",password=" + password);
                            break;
                        }
                        catch (RedisConnectionException ex)//超时
                        {
                            LoggerFactory.Logger_Debug("RedisConnectionException" + ex.Message);
                            continue;
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
                if (!RedisPools.ContainsKey(Thread.CurrentThread))
                    RedisPools.Add(Thread.CurrentThread, redis);
            }

        }

        protected override void Finalize(bool disposing)
        {
            if (!disposing)
            {
                redis.Close();
                redis.Dispose();
            }
        }
    }
}
