using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSql.Design.Dto
{
    public class DesFlowNodeXmlInput
    {
        public string rownum { get; set; }
        public string ID { get; set; }
        public string FlowNodeName { get; set; }
        public string FlowNodeTypeID { get; set; }
        public string FlowNodeNextID { get; set; }
        public string FlowNodeBackIDs { get; set; }
        public string FlowNodeEmpIDs { get; set; }
        public string FlowNodeEmpType { get; set; }
        public string FlowNodeEmpID { get; set; }
        public string FlowNodeEmpName { get; set; }
        public string FlowNodeStatus { get; set; }

        public string FlowNodeFinishTime { get; set; }
    }
}
