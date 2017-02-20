using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LindCore.GlobalConfig;
using StackExchange.Redis;
using LindCore.LindLogger;

namespace LindCore.RedisClient
{
    /// <summary>
    /// StackExchange.Redis管理者
    /// 注意：这个客户端没有连接池的概念，而是有了多路复用技术
    /// </summary>
    public class RedisManager
    {
        /// <summary>
        /// 锁对象
        /// </summary>
        private static object _locker = new object();
        /// <summary>
        /// StackExchange.Redis对象
        /// </summary>
        private static ConnectionMultiplexer _redis;


        /// <summary>
        /// 得到StackExchange.Redis单例对象
        /// </summary>
        public static ConnectionMultiplexer Instance
        {
            get
            {
                if (_redis == null || !_redis.IsConnected)
                {
                    lock (_locker)
                    {
                        if (_redis == null || !_redis.IsConnected)
                        {

                            _redis = GetManager();
                            return _redis;
                        }
                    }
                }

                return _redis;
            }
        }

        /// <summary>
        /// 构建链接,返回对象
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        private static ConnectionMultiplexer GetManager()
        {
            return GetCurrentRedis();
        }

        /// <summary>
        /// 每个连接串用逗号分开,sentinel不支持密码
        /// </summary>
        /// <returns></returns>
        static ConnectionMultiplexer GetCurrentRedis()
        {
            var connectionString = ConfigManager.Config.Redis.Host;
            ConnectionMultiplexer conn;
            var option = new ConfigurationOptions();
            option.Proxy = (Proxy)ConfigManager.Config.Redis.Proxy;//代理模式,目前支持TW

            //sentinel模式下自动连接主redis
            if (ConfigManager.Config.Redis.IsSentinel == 1)
            {
                var configArr = connectionString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                configArr.ToList().ForEach(i => option.EndPoints.Add(i));

                option.TieBreaker = "";//这行在sentinel模式必须加上
                option.CommandMap = CommandMap.Sentinel;
                conn = ConnectionMultiplexer.Connect(option);

                for (int i = 0; i < option.EndPoints.Count; i++)
                {
                    try
                    {
                        connectionString = conn.GetServer(option.EndPoints[i]).SentinelGetMasterAddressByName(ConfigManager.Config.Redis.ServiceName).ToString();
                        Console.WriteLine("当前主master[{0}]:{1}", i, connectionString);
                        break;
                    }
                    catch (RedisConnectionException ex)//超时
                    {
                        LoggerFactory.Logger_Debug("GetCurrentRedis().RedisConnectionException" + ex.Message);
                        continue;
                    }
                    catch (Exception ex)
                    {
                        LoggerFactory.Logger_Debug("GetCurrentRedis().Exception" + ex.Message);
                        throw;
                    }
                }

            }

            return ConnectionMultiplexer.Connect(connectionString + ",password=" + ConfigManager.Config.Redis.AuthPassword);
        }
    }
}
