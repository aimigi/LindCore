using System;
using System.Collections.Generic;
using System.Text;

namespace LindCore.Utils.Encryptor
{

    /// <summary>
    /// 加解密类
    /// </summary>
    public class Utility
    {
        /// <summary>
        /// 加密类型
        /// </summary>
        public enum EncryptorType
        {
            /// <summary>
            /// DES加密
            /// </summary>
            DES,

            /// <summary>
            /// MD5加密
            /// </summary>
            MD5,
        }

        /// <summary>
        /// 加密方法
        /// </summary>
        /// <param name="str">加密字符</param>
        /// <returns></returns>
        public static string EncryptString(string str)
        {
            return EncryptString(str, EncryptorType.MD5);
        }

        /// <summary>
        /// 加密方法
        /// </summary>
        /// <param name="str">加密字符串</param>
        /// <param name="code">加密长度，只有MD5加密有本参数</param>
        /// <param name="type">产生密码类型</param>
        /// <param name="type">密钥</param>
        /// <returns></returns>
        public static string EncryptString(string str, EncryptorType type)
        {
            string _tempString = str;
            switch (type)
            {
                case EncryptorType.DES:
                    throw new ArgumentException("没有实现");
                case EncryptorType.MD5:
                    _tempString = MD5Encryptor.MD5(str);
                    break;
                default:
                    throw new ArgumentException("无效的加密类型");
            }
            return _tempString;
        }



        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="str">解密字符</param>
        /// <returns></returns>
        public static string DecryptString(string str)
        {
            return DecryptString(str, EncryptorType.DES);
        }

        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="str">需解密字符</param>
        /// <param name="type">解密方法</param>
        /// <returns></returns>
        public static string DecryptString(string str, EncryptorType type)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;

            string _tempString = str;
            switch (type)
            {
                case EncryptorType.DES:
                    throw new ArgumentException("无效的加密类型");
                default:
                    throw new ArgumentException("无效的加密类型");
            }
        }

    }
}
