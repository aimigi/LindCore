using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LindCore.LindSocket
{
    /// <summary>
    /// network bit converter.
    /// </summary>
    public static class NetworkBitConverter
    {
        /// <summary>
        /// 以网络字节数组的形式返回指定的 16 位有符号整数值。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        static public byte[] GetBytes(short value)
        {
            return BitConverter.GetBytes(IPAddress.HostToNetworkOrder(value));
        }
        /// <summary>
        /// 以网络字节数组的形式返回指定的 32 位有符号整数值。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        static public byte[] GetBytes(int value)
        {
            return BitConverter.GetBytes(IPAddress.HostToNetworkOrder(value));
        }
        /// <summary>
        /// 以网络字节数组的形式返回指定的 64 位有符号整数值。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        static public byte[] GetBytes(long value)
        {
            return BitConverter.GetBytes(IPAddress.HostToNetworkOrder(value));
        }
        /// <summary>
        /// 返回由网络字节数组中指定位置的两个字节转换来的 16 位有符号整数。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        static public short ToInt16(byte[] value, int startIndex)
        {
            return IPAddress.NetworkToHostOrder(BitConverter.ToInt16(value, startIndex));
        }
        /// <summary>
        /// 返回由网络字节数组中指定位置的四个字节转换来的 32 位有符号整数。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        static public int ToInt32(byte[] value, int startIndex)
        {
            return IPAddress.NetworkToHostOrder(BitConverter.ToInt32(value, startIndex));
        }
        /// <summary>
        /// 返回由网络字节数组中指定位置的八个字节转换来的 64 位有符号整数。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        static public long ToInt64(byte[] value, int startIndex)
        {
            return IPAddress.NetworkToHostOrder(BitConverter.ToInt64(value, startIndex));
        }
        /// <summary>
        /// 字节数组转字符
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToString(byte[] value)
        {
            return Encoding.UTF8.GetString(value);
        }
        /// <summary>
        /// 字符到字节数组
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] GetBytes(string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }
    }

}
