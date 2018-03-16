using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSql.Bussiness
{
   public class BussContractOtherFeeFactJson
    {
            public int ID { get; set; }
        public int RefID { get; set; }
        public int FactTypeID { get; set; }
        public string RefTable { get; set; }
        public string DeptName { get; set; }
        public DateTime FeerDate { get; set; }
        public decimal FeeMoney { get; set; }
        public int CreateEmpId { get; set; }
        public string CreateEmpName { get; set; }
        public DateTime CreationTime { get; set; }

        public BussContractOtherFeeFactJson()
        {
            ID = 0;
            RefID = 0;
            FactTypeID = 0;
            RefTable = "";
            DeptName = "";
            FeerDate = DateTime.Now;
            FeeMoney = 0;
            CreateEmpId = 0;
            CreateEmpName = "";
            CreationTime = DateTime.Now;
        }
    }
}
