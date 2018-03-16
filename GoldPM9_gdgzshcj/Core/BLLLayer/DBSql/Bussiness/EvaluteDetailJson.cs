using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSql.Bussiness
{
   public class EvaluteDetailJson
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public int DeptId { get; set; }
        public string DeptName { get; set; }
        public DateTime EvaluateDate { get; set; }
        public string EvaluateIdea { get; set; }

        public EvaluteDetailJson()
        {
            Id = 0;
            EmpId = 0;
            DeptId = 0;
            EmpName = "";
            DeptName = "";
            EvaluateDate = DateTime.Now;
            EvaluateIdea = "";     
        }
    }
}
