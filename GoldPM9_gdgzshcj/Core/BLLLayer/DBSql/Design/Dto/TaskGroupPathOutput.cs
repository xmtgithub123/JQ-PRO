using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSql.Design.Dto
{
    public class TaskGroupPathOutput
    {
        public string Type
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public long Id
        {
            get; set;
        }

        public string ParentType
        {
            get; set;
        }

        public long ParentId
        {
            get; set;
        }
    }
}
