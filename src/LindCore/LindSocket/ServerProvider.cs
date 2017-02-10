using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LindCore.LindSocket
{
    /// <summary>
    /// 服务端-提供者
    /// </summary>
    public class ServerProvider
    {
        /// <summary>
        /// 启动LindSocket数据传输服务
        /// </summary>
        public static void Start(Action<byte[]> callAction)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //服务器IP地址  
            IPAddress ip = IPAddress.Parse(GlobalConfig.ConfigManager.Config.LindSocket.Host);
            var serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(ip, GlobalConfig.ConfigManager.Config.LindSocket.Port));  //绑定IP地址：端口  
            serverSocket.Listen(GlobalConfig.ConfigManager.Config.LindSocket.ListenMaxCount);    //设定最多10个排队连接请求  
            Console.WriteLine("LindSocket已启动...");
            while (true)
            {
                Socket clientSocket = serverSocket.Accept(); //接受一个传入的连接
                clientSocket.Send(NetworkBitConverter.GetBytes($"与服务器{GlobalConfig.ConfigManager.Config.LindSocket.Host}连接成功..."));//发送数据到客户端
                Thread receiveThread = new Thread(ReceiveMessage);
                receiveThread.Start(clientSocket);

            }
        }


        /// <summary>  
        /// 接收消息  
        /// </summary>  
        /// <param name="clientSocket"></param>  
        static void ReceiveMessage(object clientSocket)
        {
            Socket myClientSocket = (Socket)clientSocket;
            while (true)
            {
                try
                {
                    byte[] result = new byte[4096];
                    //通过clientSocket接收数据  
                    int receiveNumber = myClientSocket.Receive(result);//从连接的 System.Net.Sockets.Socket 对象中接收数据
                                                                       // callAction(result);
                    Console.WriteLine($"接收客户端:{myClientSocket.RemoteEndPoint.ToString()},消息:{LindSocket.NetworkBitConverter.ToString(result)}");
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

    }
}
