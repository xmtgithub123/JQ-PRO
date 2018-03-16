using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSql.Design.Dto
{
    public class DesTaskRemindInput
    {
        public string ItemPath1 { get; set; }
        public string ItemPath2 { get; set; }
        public string ItemName { get; set; }
        public string ItemEmpName { get; set; }
        public int ItemStatus { get; set; }
        public DateTime DatePlanStart { get; set; }
        public DateTime DatePlanFinish { get; set; }
        public DateTime DateActualFinish { get; set; }
        public int ItemType { get; set; }
        public string ItemAction { get; set; }
        public int ProjId { get; set; }
        public long TaskGroupId { get; set; }
        public long TaskId { get; set; }
        public int TabId { get; set; }
        public int ID { get; set; }

    }
}
