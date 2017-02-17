using LindCore.CacheConfigFile;
using LindCore.LindLogger;
using LindCore.RedisClient;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace LindCore.Test
{
    class Son
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int[] Age { get; set; }
    }
    class Test : IConfiger
    {
        public Test()
        {
            this.Name = "爸爸";
            this.Email = "father@sina.com";
            this.Age = new int[] { 15, 37, 48, 76 };
            Sons = new List<Son> {
                new Son {
                    Age= new int[] { 1,3,5},
                    Name="zzl1'son",
                    Email="son1@siona.com"
                 },
                  new Son {
                    Age= new int[] { 2,4,6},
                    Name="zzl2'son",
                    Email="son2@siona.com"
                 }
            };
        }
        public string Name { get; set; }
        public string Email { get; set; }
        public int[] Age { get; set; }
        public List<Son> Sons { get; set; }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            var obj = LindCore.CacheConfigFile.ConfigFactory.Instance.GetConfig<Test>();
            LoggerFactory.Logger_Debug("test...");
            LoggerFactory.Logger_Fatal("fatal test...");
            LoggerFactory.Logger_Info("info test...");

            LindSocket.ServerProvider.Start((buffer) =>
            {
                Console.WriteLine(LindSocket.NetworkBitConverter.ToString(buffer));
            });
            Console.ReadKey();
            var result2 = (int)RedisManager.Instance.GetDatabase().StringGet("abc123zzl");

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            TestServer();
            string Mobile = "13521972991";
            Mobile = string.IsNullOrEmpty(Mobile) ? "" : Mobile.Substring(0, 3) + Mobile.Substring(3, 4).Replace(Mobile.Substring(3, 4), "****") + Mobile.Substring(7);

            var a = DateTime.Parse("23:59");

            for (int i = 0; i < 100; i++)
            {
                RedisClient.RedisManager.Instance.GetDatabase().StringIncrement(DateTime.Now.ToString("yyyyMMddHHmm") + "::" + DateTime.Now.Second);
                Thread.Sleep(100);
            }
            LoggerFactory.Logger_Debug("test...");
            Console.WriteLine("Lind for .NetCore Platform!");
            Console.WriteLine(Hello);

            #region 数据在网络中的传输
            #region 发送
            //原字符
            string fromStr = "hello lind!";
            //从原字符到原字节流
            var src = Encoding.UTF8.GetBytes(fromStr);
            #endregion

            #region 接收&监听
            //目标字节流
            var data = new byte[src.Length];
            //从原字符流到目标字节流
            Buffer.BlockCopy(src, 0, data, 0, src.Length);
            //从目标字节流到目标字符
            var des = Encoding.UTF8.GetString(data);
            //结果输出
            Console.WriteLine($"从原字符到byte[],再从网络byte[]到目标字符,结果为{des}");
            #endregion
            #endregion

            Console.ReadKey();
        }

        private static int myProt = 8885;   //端口  
        static Socket serverSocket;

        #region 服务端
        static void TestServer()
        {
            //服务器IP地址  
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(ip, myProt));  //绑定IP地址：端口  
            serverSocket.Listen(10);    //设定最多10个排队连接请求  
            Console.WriteLine("启动监听{0}成功", serverSocket.LocalEndPoint.ToString());
            while (true)
            {
                Socket clientSocket = serverSocket.Accept(); //接受一个传入的连接
                clientSocket.Send(Encoding.ASCII.GetBytes("Server Say Hello"));//发送数据到客户端
                Thread receiveThread = new Thread(ReceiveMessage);
                receiveThread.Start(clientSocket);
            }
        }


        /// <summary>  
        /// 接收消息  
        /// </summary>  
        /// <param name="clientSocket"></param>  
        private static void ReceiveMessage(object clientSocket)
        {
            Socket myClientSocket = (Socket)clientSocket;
            while (true)
            {
                try
                {
                    byte[] result = new byte[1024];
                    //通过clientSocket接收数据  
                    int receiveNumber = myClientSocket.Receive(result);//从连接的 System.Net.Sockets.Socket 对象中接收数据
                    Console.WriteLine($"接收客户端{myClientSocket.RemoteEndPoint.ToString()}消息{Encoding.ASCII.GetString(result, 0, receiveNumber)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    myClientSocket.Shutdown(SocketShutdown.Both);
                    myClientSocket.Dispose();
                    break;
                }
            }
        }
        #endregion

        #region 客户端
        static void TestClient()
        {
            byte[] result = new byte[1024];
            //设定服务器IP地址  
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                clientSocket.Connect(new IPEndPoint(ip, 8885)); //配置服务器IP与端口  
                Console.WriteLine("连接服务器成功");
            }
            catch
            {
                Console.WriteLine("连接服务器失败，请按回车键退出！");
                return;
            }
            //通过clientSocket接收数据  
            int receiveLength = clientSocket.Receive(result);
            Console.WriteLine("接收服务器消息：{0}", Encoding.ASCII.GetString(result, 0, receiveLength));
            //通过 clientSocket 发送数据  
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    Thread.Sleep(1000);    //等待1秒钟  
                    string sendMessage = "client send Message Hellp" + DateTime.Now;
                    clientSocket.Send(Encoding.ASCII.GetBytes(sendMessage));
                    Console.WriteLine("向服务器发送消息：{0}" + sendMessage);
                }
                catch
                {
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Dispose();
                    break;
                }
            }
            Console.WriteLine("发送完毕，按回车键退出");
            Console.ReadLine();
        }
        #endregion
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
