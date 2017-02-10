using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LindCore.TestClient
{
    public class Program
    {
        private static byte[] result = new byte[1024];
        private static int myProt = 8885;   //端口  
        public static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            for (int i = 0; i < 100; i++)
            {
                // TestClient();
                LindSocket.ClientProvider.Send(LindSocket.NetworkBitConverter.GetBytes("hello"), (msg) =>
                {
                    Console.WriteLine(LindSocket.NetworkBitConverter.ToString(msg));
                });
                Thread.Sleep(1000);
            }
        }

        #region 客户端
        static void TestClient()
        {
            //设定服务器IP地址  
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                clientSocket.Connect(new IPEndPoint(ip, myProt)); //配置服务器IP与端口  
                Console.WriteLine("连接服务器成功");
            }
            catch
            {
                Console.WriteLine("连接服务器失败，请按回车键退出！");
                return;
            }
            //通过clientSocket接收数据  
            int receiveLength = clientSocket.Receive(result);//接收从服务端发来的数据
            Console.WriteLine($"接收服务器消息：{Encoding.ASCII.GetString(result, 0, receiveLength)}");
            //通过 clientSocket 发送数据  
            for (int i = 0; i < 100; i++)
            {
                try
                {
                    Thread.Sleep(1000);    //等待1秒钟  
                    string sendMessage = "client send Message Hello " + DateTime.Now;
                    clientSocket.Send(Encoding.ASCII.GetBytes(sendMessage));
                    Console.WriteLine($"向服务器发送消息：{sendMessage}");
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
    }
}
