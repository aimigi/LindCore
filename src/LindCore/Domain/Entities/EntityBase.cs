using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LindCore
{
    /// <summary>
    /// 领域实体基类
    /// </summary>
    /// <typeparam name="Key">主键类型</typeparam>
    public abstract class EntityBase<Key>
    {
        /// <summary>
        /// 统一主键
        /// </summary>
        public virtual Key Id { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastedTime { get; set; }
    }
}
