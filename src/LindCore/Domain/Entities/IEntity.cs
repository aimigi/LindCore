using System;

namespace LindCore.Domain.Entities
{
    /// <summary>
    /// 实体基类
    /// </summary>
    public interface IEntity
    {
        DateTime AddTime { get; set; }
        DateTime LastedTime { get; set; }
    }
}