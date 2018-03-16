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
    /// //TODO 测试流程数据所用，过后可删除
    /// </summary>
    [Table("FlowTest")]
    public class FlowTest
    {
        [Key]
        public int ID
        {
            get; set;
        }

        public string Test1
        {
            get; set;
        }

        public string Test2
        {
            get; set;
        }
    }
}
