using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    /// <summary>
    /// 流程控件参数
    /// </summary>
    public class FlowControlPar
    {
        /// <summary>
        /// 流程业务ID
        /// </summary>
        public int flowRefID { get; set; }

        /// <summary>
        /// 流程节点ID
        /// </summary>
        public int flowNodeID { get; set; }


       /// <summary>
       /// 流程关联表
       /// </summary>
        public string flowRefTable { get; set; }



        public string contentControlName { get; set; }

        /// <summary>
        /// 表单的ID
        /// </summary>
        public string formid { get; set; }
    }

    /// <summary>
    /// 提交流程参数
    /// </summary>
    public class FlowNextPar
    {
        /// <summary>
        /// 节点ID
        /// </summary>
        public int flowNodeID { get; set; }

        /// <summary>
        /// 下一步节点ID
        /// </summary>
        public int nextNodeId { get; set; }


        /// <summary>
        /// 操作人ID
        /// </summary>
        public int empId { get; set; }

        /// <summary>
        /// 申请人ID
        /// </summary>
        public string empIds { get; set; }

        /// <summary>
        /// 处理意见
        /// </summary>
        public string remark { get; set; }
    }
}
