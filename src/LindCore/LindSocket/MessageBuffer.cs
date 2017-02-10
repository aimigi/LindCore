using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LindCore.LindSocket
{
    /// <summary>
    /// 通讯协议
    /// </summary>
    public class MessageBuffer
    {
        /// <summary>
        /// 批次
        /// </summary>
        public int SegmentId { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public byte[] Buffer { get; set; }
    }
}
