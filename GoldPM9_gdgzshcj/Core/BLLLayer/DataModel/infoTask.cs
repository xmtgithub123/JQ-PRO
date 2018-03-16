using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class infoTask
    {
        public int id { get; set; }
        public int projid { get; set; }
        public int taskId { get; set; }
        public string name { get; set; }
        public int level { get; set; }
        public int progress { get; set; }
        public int duration { get; set; }
        public DateTime datePlanStart { get; set; }
        public DateTime datePlanFinish { get; set; }
        public int keyPointType { get; set; }
        public bool startIsMilestone { get; set; }
        public bool endIsMilestone { get; set; }
        public string description { get; set; }
        public infoTask()
        {
        }

    }
}
