using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LindCore.Manager.Entities
{
    /// <summary>
    /// 数据集控制的结果
    /// </summary>
    public class WebDataSetting : LindCore.Domain.Entities.EntityInt
    {
        [DisplayName("角色编号"), Required]
        public string ObjectIdArr { get; set; }
        [DisplayName("部门编号"), Required]
        public int WebDepartmentsId { get; set; }
        [DisplayName("被授予的名称")]
        public string ObjectNameArr { get; set; }
        public WebDataCtrl WebDataCtrl { get; set; }
        public WebManageRoles WebManageRoles { get; set; }


    }

}
