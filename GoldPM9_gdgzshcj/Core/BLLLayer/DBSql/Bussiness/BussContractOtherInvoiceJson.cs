using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSql.Bussiness
{
    public class BussContractOtherInvoiceJson
    {

        public int ID { get; set; }
        public int RefID { get; set; }
        public int ConOtherTypeID { get; set; }
        public string RefTable { get; set; }
        public string DeptName { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal InvoiceMoney { get; set; }
        public int CreateEmpId { get; set; }
        public string CreateEmpName { get; set; }
        public DateTime CreationTime { get; set; }


        public BussContractOtherInvoiceJson()
        {
            ID = 0;
            RefID = 0;
            ConOtherTypeID = 0;
            RefTable = "";
            DeptName = "";
            InvoiceDate = DateTime.Now;
            InvoiceMoney = 0;
            CreateEmpId = 0;
            CreateEmpName = "";
            CreationTime = DateTime.Now;
        }
    }
}
