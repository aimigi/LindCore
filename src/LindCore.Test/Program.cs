using LindCore.LindLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LindCore.Test
{
    class Test
    {
        public string Name { get; set; }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            for (int i = 0; i < 100; i++)
            {
                RedisClient.RedisManager.Instance.GetDatabase().StringIncrement(DateTime.Now.ToString("yyyyMMddHHmm") + "::" + DateTime.Now.Second);
                Thread.Sleep(100);
            }
            LoggerFactory.Logger_Debug("test...");
            Console.WriteLine("Lind for .NetCore Platform!");
            Console.WriteLine(Hello);

            #region 数据在网络中的传输
            //原字符
            string fromStr = "hello lind!";
            //从原字符到原字节流
            var src = Encoding.UTF8.GetBytes(fromStr);
            //目标字节流
            var data = new byte[src.Length];
            //从原字符流到目标字节流
            Buffer.BlockCopy(src, 0, data, 0, src.Length);
            //从目标字节流到目标字符
            var des = Encoding.UTF8.GetString(data);
            //结果输出
            Console.WriteLine($"从原字符到byte[],再从网络byte[]到目标字符,结果为{des}");

            #endregion

            Console.ReadKey();
        }

        //只读属性初始化
        static string Hello => @"Hello world , Lind!";

        //属性初始化
        static DateTime AddTime { get; set; } = DateTime.Now;

        //字典初始化器
        static Dictionary<string, string> dictionary1 = new Dictionary<string, string>
        {
            ["name"] = "value1",
            ["age"] = "16"
        };

        //string.Format，后台引入了$，而且支持智能提示。 
        static string t2 = $"{DateTime.Now}~{DateTime.Now.AddDays(1)}";

        //空判断
        static Test test = new Test();
        static string title = test?.Name;//if(test!=null) title=test.Name;

        //空集合判断
        static List<Test> testList = null;
        static Test defaultList = testList?[0];

        //方法-单行实现
        public void ConsolePrint(string msg) => Console.WriteLine(msg);
    }
}
