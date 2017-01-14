using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace LindCore.Utils.Encryptor
{
    internal class MD5Encryptor
    {

        /// <summary>
        /// MD5函数
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns>MD5结果</returns>
        public static string MD5(string str)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            byte[] b = Encoding.UTF8.GetBytes(str);
            b = System.Security.Cryptography.MD5.Create().ComputeHash(b);
            StringBuilder ret = new StringBuilder();
            for (int i = 0; i < b.Length; i++)
                ret.Append(b[i].ToString("x").PadLeft(2, '0'));
            return ret.ToString();
        }
    }
}
