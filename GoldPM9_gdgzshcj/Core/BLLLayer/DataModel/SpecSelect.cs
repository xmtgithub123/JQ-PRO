using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
   public class SpecSelect
    {
       public int PhaseID { get; set; }
        public int SpecID { get; set; }
        public int SpecEmpID { get; set; }
        public DateTime PlanFinish { get; set; }
        public string SpeNote { get; set; }
    }
}
