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
    /// 日程
    /// </summary>
    public class SchedulerBase
    {
        /// <summary>
        /// 主键（自增）
        /// </summary>
        [Key]
        public int ID
        {
            get; set;
        }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content
        {
            get; set;
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime
        {
            get; set;
        }

        /// <summary>
        /// 参与人员的ID
        /// </summary>
        public string JoinEmpIDs
        {
            get; set;
        }

        /// <summary>
        /// 创建人EmpId
        /// </summary>
        public int CreatorEmpId
        {
            get; set;
        }

        /// <summary>
        /// 具体的通知时间
        /// </summary>
        public DateTime NotifyTime
        {
            get; set;
        }

        /// <summary>
        /// 是否为全天事项
        /// </summary>
        public bool IsFullDay
        {
            get; set;
        }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string CreatorEmpName
        {
            get; set;
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime
        {
            get; set;
        }
    }

    /// <summary>
    /// 日程表数据
    /// </summary>
    [Table("Scheduler")]
    public class Scheduler : SchedulerBase
    {

        /// <summary>
        /// 关联ID
        /// </summary>
        public int RefID
        {
            get; set;
        }

        /// <summary>
        /// 关联Table
        /// </summary>
        public string RefTable
        {
            get; set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime
        {
            get; set;
        }

        /// <summary>
        /// 扩展属性
        /// </summary>
        public string Attributes
        {
            get; set;
        }

        /// <summary>
        /// 提前提醒时间（minute）
        /// 当为-1时不提醒
        /// </summary>
        public int RemindBefore
        {
            get; set;
        }

        /// <summary>
        /// 提前提醒类型 1:minute 2:hour 3:day
        /// 如果为全天类型的事项需要提醒，此处的值将按天提醒
        /// </summary>
        public int RemindBeforeType
        {
            get; set;
        }

    }
}
