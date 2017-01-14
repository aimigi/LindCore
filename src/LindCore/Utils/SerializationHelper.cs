using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace LindCore.Utils
{
    /// <summary>
    /// ���л��뷴���л����ļ�
    /// </summary>
    public class SerializationHelper
    {
        private static object lockObj = new object();

        #region JSON
        /// <summary>
        /// ���������л�������
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="obj"></param>
        public static void SerializableToJson(string fileName, object obj)
        {
            lock (lockObj)
            {
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                    {
                        sw.Write(SerializeMemoryHelper.SerializeToJson(obj));
                    }
                }
            }
        }
        /// <summary>
        /// �����Ʒ����л��Ӵ��̵��ڴ����
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T DeserializeFromJson<T>(string fileName)
        {
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    using (StreamReader sw = new StreamReader(fs, Encoding.UTF8))
                    {
                        return SerializeMemoryHelper.DeserializeFromJson<T>(sw.ReadToEnd());
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }

}
