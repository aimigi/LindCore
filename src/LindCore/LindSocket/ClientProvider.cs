using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LindCore.LindSocket
{
    /// <summary>
    /// 客户端-提供者
    /// Author:Lind
    /// Date:2017-02-14
    /// </summary>
    public class ClientProvider
    {
        public static void Send(byte[] data, Action<byte[]> callBack)
        {
           // Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var result = new byte[GlobalConfig.ConfigManager.Config.LindSocket.BufferSize];
            //设定服务器IP地址  
            IPAddress ip = IPAddress.Parse(GlobalConfig.ConfigManager.Config.LindSocket.Host);
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                clientSocket.Connect(new IPEndPoint(ip, GlobalConfig.ConfigManager.Config.LindSocket.Port)); //配置服务器IP与端口  
                Console.WriteLine("连接服务器成功");
            }
            catch
            {
                Console.WriteLine("连接服务器失败，请按回车键退出！");
                return;
            }
            //通过clientSocket接收数据  
            int receiveLength = clientSocket.Receive(result);//接收从服务端发来的数据
            callBack(result); //回调

            try
            {
                clientSocket.Send(data);
            }
            catch
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Dispose();
            }

        }
    }
}
