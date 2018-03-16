using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [Serializable]
    public class BarModel
    {
        public List<List<object>> data { get; set; }

        public List<object> legendData { get; set; }

        public List<object> dataName { get; set; }
    }
}
