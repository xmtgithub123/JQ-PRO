using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSql.PubFlow
{
    /// <summary>
    /// 流程返回的实例
    /// </summary>
    [Serializable]
    public class FlowWareResult
    {
        internal FlowWareResult()
        {

        }

        public FlowWareResult(bool result)
        {
            this.Result = result;
        }

        public FlowWareResult(bool result, string message)
        {
            this.Result = result;
            this.Message = message;
        }

        /// <summary>
        /// 结果
        /// </summary>
        public bool Result
        {
            get; set;
        }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message
        {
            get; set;
        }

        /// <summary>
        /// 当前结点的名称
        /// </summary>
        public string NodeName
        {
            get; set;
        }

        /// <summary>
        /// 当前结点允许编辑的控件
        /// </summary>
        public string AllowEditControls
        {
            get; set;
        }

        /// <summary>
        /// 当前需要签名的控件
        /// </summary>
        public string SignControls
        {
            get; set;
        }

        /// <summary>
        /// 是否流程已经审批完成
        /// </summary>
        public bool IsFlowFinished
        {
            get; set;
        }

        /// <summary>
        /// 是否已经审批完成
        /// </summary>
        public bool IsFinished
        {
            get; set;
        }

        public int UserID
        {
            get; set;
        }

        public string UserName
        {
            get; set;
        }

        public int DepartmentID
        {
            get; set;
        }

        public string DepartmentName
        {
            get; set;
        }

        /// <summary>
        /// 0：不能提交下一步
        /// 1：提交下一步
        /// 2：提交跳步
        /// </summary>
        public int NextAction
        {
            get; set;
        }

        /// <summary>
        /// 0：不能换人
        /// 1：可以换人
        /// </summary>
        public int ChangeAction
        {
            get; set;
        }

        /// <summary>
        /// 0：不能退回
        /// 1：指定退回
        /// 2：选择退回
        /// </summary>
        public int BackAction
        {
            get; set;
        }

        /// <summary>
        /// 直接完成
        /// </summary>
        public int FinishAction
        {
            get; set;
        }

        /// <summary>
        /// 直接否决
        /// </summary>
        public int RejectAction
        {
            get; set;
        }

        /// <summary>
        /// 返回的请求地址
        /// </summary>
        public string ActionUrl
        {
            get; set;
        }

        /// <summary>
        /// 返回请求的进度查看页面
        /// </summary>
        public string ProgressUrl
        {
            get; set;
        }

        /// <summary>
        /// 当前步骤
        /// </summary>
        public int StepOrder
        {
            get; set;
        }

        /// <summary>
        /// 是否为最后一个步骤
        /// </summary>
        public bool IsLastStep
        {
            get; set;
        }

        /// <summary>
        /// 下一步的节点
        /// </summary>
        public List<FlowWareStep> NextSteps
        {
            get; set;
        }

        public int DefaultChoosedStep
        {
            get; set;
        }

        public string DefaultNote
        {
            get; set;
        }

        public int FlowID
        {
            get; set;
        }

        /// <summary>
        /// 流程名称
        /// </summary>
        public string FlowName
        {
            get; set;
        }

        /// <summary>
        /// 流程关联ID
        /// </summary>
        public int FlowRefID
        {
            get; set;
        }

        /// <summary>
        /// 流程审批意见是否必须
        /// </summary>
        public bool IsNoteRequired
        {
            get; set;
        }

        /// <summary>
        /// 当前日期
        /// </summary>
        public string Date
        {
            get; set;
        }

        /// <summary>
        /// 上次备注
        /// </summary>
        public string LastNote
        {
            get; set;
        }

        /// <summary>
        /// 是否为新的流程
        /// </summary>
        public bool IsNew
        {
            get; set;
        }

        public int FlowNodeID
        {
            get; set;
        }

        public int FlowMultiSignID
        {

            get; set;
        }

        /// <summary>
        /// 签名内容
        /// </summary>
        public string SignDatas
        {
            get; set;
        }

        /// <summary>
        /// 代理登录用户名称
        /// </summary>
        public string AgentEmpName
        {
            get; set;
        }

        /// <summary>
        /// 是否允许撤销
        /// </summary>
        public bool IsAllowUndo
        {
            get; set;
        }
    }
}
