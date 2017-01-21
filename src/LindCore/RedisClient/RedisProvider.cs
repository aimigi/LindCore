using LindCore.Commons;
using LindCore.LindLogger;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LindCore.RedisClient
{
    /// <summary>
    /// Redis提供者
    /// 实例－实现方式
    /// </summary>
    public class RedisProvider : DisposableBase
    {
        ConnectionMultiplexer redis;
        ConfigurationOptions option;
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
            option = new ConfigurationOptions();
            var configArr = conn.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            configArr.ToList().ForEach(i => option.EndPoints.Add(i));
            option.TieBreaker = "";//这行在sentinel模式必须加上
            option.CommandMap = CommandMap.Sentinel;
            redis = ConnectionMultiplexer.Connect(option);

            for (int i = 0; i < option.EndPoints.Count; i++)
            { 
                try
                {
                    conn = redis.GetServer(option.EndPoints[i]).SentinelGetMasterAddressByName(serviceName).ToString();
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
