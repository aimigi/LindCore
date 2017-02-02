using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LindCore.Manager.Models
{
    public partial class WebManageRoles_WebManageMenus_Authority_R : LindCore.Domain.Entities.EntityInt
    {
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        /// <summary>
        /// 授权位，64位无符号，操作删除，这个位将被回收
        /// </summary>
        public long Authority { get; set; }
        public WebManageMenus WebManageMenus { get; set; }
        public WebManageRoles WebManageRoles { get; set; }
    }
}
