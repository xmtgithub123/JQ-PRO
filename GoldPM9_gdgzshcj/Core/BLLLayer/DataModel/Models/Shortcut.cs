using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Models
{
    /// <summary>
    /// 快捷方式实体类
    /// </summary>
    [Table("Shortcut")]
    public class Shortcut
    {
        [Key]
        public int Id
        {
            get; set;
        }

        public int MenuID
        {
            get; set;
        }


        public int EmpID
        {
            get; set;
        }

        public int RefID
        {
            get; set;
        }

        public string Note
        {
            get; set;
        }

        public DateTime CreationTime
        {
            get; set;
        }

        [ForeignKey("MenuID")]
        public BaseMenu FK_Shortcut_MenuID
        {
            get; set;
        }
    }
}
