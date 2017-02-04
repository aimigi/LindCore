using LindCore.Domain.Behavors;

namespace LindCore.Manager.Entities
{
    /// <summary>
    /// 审核记录
    /// </summary>
    public partial class WebConfirmRecord : LindCore.Domain.Entities.EntityInt, IAuditedBehavor
    {
        /// <summary>
        /// 什么人编号
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 什么人
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 什么事
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 给什么人
        /// </summary>
        public int ToUserId { get; set; }

        #region IAuditedBehavor 成员

        public int AuditedUserId
        {
            get;
            set;
        }

        public string AuditedUserName
        {
            get;
            set;
        }

        public int AuditedStatus
        {
            get;
            set;
        }

        public string AuditedWorkFlow
        {
            get;
            set;
        }

        #endregion
    }
}
