using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSql.PubFlow
{
    public class FlowTask
    {
        public int FlowID
        {
            get; set;
        }

        public int FlowRefID
        {
            get; set;
        }

        public int FlowMultiSignID
        {
            get; set;
        }

        public string FlowRefTable
        {
            get; set;
        }

        public string FlowName
        {
            get; set;
        }

        public int FlowNodeID
        {
            get; set;
        }

        public string FlowNodeName
        {
            get; set;
        }

        public DateTime FlowStartDate
        {
            get; set;
        }

        public DateTime FlowFinishDate
        {
            get; set;
        }

        public string FlowStatusName
        {
            get; set;
        }

        public string CreatorEmpName
        {
            get; set;
        }

        public string FlowStatusID
        {
            get; set;
        }

        public int FlowNodeEmpId
        {
            get; set;
        }

        public string FlowNodeEmpName
        {
            get; set;
        }

        public string FlowNodeTypeID
        {
            get; set;
        }

        public int FlowNodeStatusID
        {
            get; set;
        }

        public string FlowNodeStatusName
        {
            get; set;
        }

        public string FlowNodeUrl
        {
            get; set;
        }

        public DateTime FlowNodeDate
        {
            get; set;
        }

        public DateTime FlowNodeFromDateTime
        {
            get; set;
        }

        /// <summary>
        /// 流程标题
        /// </summary>
        public string FlowTitle
        {
            get; set;
        }

        public int DialogWidth
        {
            get; set;
        }

        public int DialogHeight
        {
            get; set;
        }

        public int FlowType
        {
            get; set;
        }

        public int FormTemplateID
        {
            get; set;
        }
    }

    //public string SetX
}
