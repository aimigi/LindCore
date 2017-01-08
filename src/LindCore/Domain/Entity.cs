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
    public class Entity<Key>
    {
        /// <summary>
        /// 统一主键
        /// </summary>
       public Key Id { get; set; }
    }
}
