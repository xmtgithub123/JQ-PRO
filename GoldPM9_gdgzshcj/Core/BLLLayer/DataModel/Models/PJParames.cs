using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Models
{
    public class SelEmpParames
    {
        public string id { get; set; }
        public string setID { get; set; }

        public string vals { get; set; }

        private bool _isMult = true;
        public bool isMult
        {
            get { return _isMult; }
            set { _isMult = value; }
        }
    }
}
