using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LindCore.Manager.Entities
{
    public partial class WebManageRoles_WebManageMenus_Authority_R : LindCore.Domain.Entities.EntityInt
    {
        /// <summary>
        /// 授权位，64位无符号，操作删除，这个位将被回收
        /// </summary>
        public long Authority { get; set; }
        public WebManageMenus WebManageMenus { get; set; }
        public WebManageRoles WebManageRoles { get; set; }
    }
}
