using System;
using System.Collections.Generic;
using System.Text;

namespace LindCore.Utils.Encryptor
{

    /// <summary>
    /// �ӽ�����
    /// </summary>
    public class Utility
    {
        /// <summary>
        /// ��������
        /// </summary>
        public enum EncryptorType
        {
            /// <summary>
            /// DES����
            /// </summary>
            DES,

            /// <summary>
            /// MD5����
            /// </summary>
            MD5,
        }

        /// <summary>
        /// ���ܷ���
        /// </summary>
        /// <param name="str">�����ַ�</param>
        /// <returns></returns>
        public static string EncryptString(string str)
        {
            return EncryptString(str, EncryptorType.MD5);
        }

        /// <summary>
        /// ���ܷ���
        /// </summary>
        /// <param name="str">�����ַ���</param>
        /// <param name="code">���ܳ��ȣ�ֻ��MD5�����б�����</param>
        /// <param name="type">������������</param>
        /// <param name="type">��Կ</param>
        /// <returns></returns>
        public static string EncryptString(string str, EncryptorType type)
        {
            string _tempString = str;
            switch (type)
            {
                case EncryptorType.DES:
                    throw new ArgumentException("û��ʵ��");
                case EncryptorType.MD5:
                    _tempString = MD5Encryptor.MD5(str);
                    break;
                default:
                    throw new ArgumentException("��Ч�ļ�������");
            }
            return _tempString;
        }



        /// <summary>
        /// ���ܷ���
        /// </summary>
        /// <param name="str">�����ַ�</param>
        /// <returns></returns>
        public static string DecryptString(string str)
        {
            return DecryptString(str, EncryptorType.DES);
        }

        /// <summary>
        /// ���ܷ���
        /// </summary>
        /// <param name="str">������ַ�</param>
        /// <param name="type">���ܷ���</param>
        /// <returns></returns>
        public static string DecryptString(string str, EncryptorType type)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;

            string _tempString = str;
            switch (type)
            {
                case EncryptorType.DES:
                    throw new ArgumentException("��Ч�ļ�������");
                default:
                    throw new ArgumentException("��Ч�ļ�������");
            }
        }

    }
}
