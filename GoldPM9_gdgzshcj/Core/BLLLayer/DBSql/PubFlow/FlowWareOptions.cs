using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSql.PubFlow
{
    public class FlowWareOptions
    {
        public FlowWareOptions()
        {
            this.IsValidateEmp = true;
        }
        /// <summary>
        /// 关联ID
        /// </summary>
        public int RefID
        {
            get; set;
        }
        public int BridgeID
        {
            get; set;
        }

        /// <summary>
        /// 关联Table
        /// </summary>
        public string RefTable
        {
            get; set;
        }

        /// <summary>
        /// 执行的动作<see cref="DBSql.PubFlow.Action"/>
        /// </summary>
        public Action Action
        {
            get; set;
        }

        /// <summary>
        /// 当前节点的ID
        /// </summary>
        public int FlowNodeID
        {
            get; set;
        }

        /// <summary>
        /// 当前节点的序号
        /// </summary>
        public int FlowNodeOrder
        {
            get; set;
        }

        public int FlowMultiSignID
        {
            get; set;
        }

        /// <summary>
        /// 当前登录的用户ID
        /// </summary>
        public DataModel.EmpSession CurrentUser
        {
            get; set;
        }

        /// <summary>
        /// 处理程序
        /// </summary>
        public string Processor
        {
            get; set;
        }

        /// <summary>
        /// 下一步的节点
        ///  提交审批时为下一步
        ///  提交退回时上上一部
        /// </summary>
        public int NextNodeID
        {
            get; set;
        }

        /// <summary>
        /// 下一个Emp的ID
        /// 如果为会签，则为多个EmpID，
        /// 如果不为会签，只有一个EmpID
        /// </summary>
        public string NextEmpIDs
        {
            get; set;
        }

        /// <summary>
        /// 流程实例的ID
        /// </summary>
        public int FlowID
        {
            get; set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note
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
        /// 流程标题（由调用方返回参数）
        /// </summary>
        public string Title
        {
            get; set;
        }

        /// <summary>
        /// 是否验证当前的处理人员
        /// </summary>
        /// <returns></returns>
        public bool IsValidateEmp
        {
            get; set;
        }
        public Guid Guid
        {
            get; set;
        }

        /// <summary>
        /// 是否为发起新的流程
        /// </summary>
        public bool IsNew
        {
            get; set;
        }

        /// <summary>
        /// 相关联的工程列表ID（大项）
        /// </summary>
        public List<int> ProjectIDs
        {
            get; set;
        }

        /// <summary>
        /// 相关联的工程列表以及阶段（指定阶段的子项）
        /// <para>Key：子项ID</para>
        /// <para>Value：阶段ID的集合（如果不指定阶段则传空）</para>
        /// </summary>
        public Dictionary<int, List<int>> ProjectPhases
        {
            get; set;
        }

        public string[] MobilePushFlowRefTables
        {
            get
            {
                DataModel.Models.BaseConfig config = new DBSql.Sys.BaseConfig().GetBaseConfigInfo("MobilePushFlowRefTables");
                return config == null ? new string[] { } : config.ConfigValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }
    }

    public enum Action
    {
        /// <summary>
        /// 默认值
        /// </summary>
        Default = 0,
        /// <summary>
        /// 加载时的请求
        /// </summary>
        Load = 1,

        /// <summary>
        /// 点击提交审批后的加载信息
        /// </summary>
        LoadNext = 2,

        /// <summary>
        /// 点击提交退回后的加载信息
        /// </summary>
        LoadBack = 3,

        /// <summary>
        /// 点击直接完成后的加载信息
        /// </summary>
        LoadFinish = 4,

        /// <summary>
        /// 点击直接否决后的加载信息
        /// </summary>
        LoadReject = 5,

        /// <summary>
        /// 点击工作移交后的加载信息
        /// </summary>
        LoadTransfter = 6,

        /// <summary>
        /// 保存表单
        /// </summary>
        Save = 7,

        /// <summary>
        /// 提交审批
        /// </summary>
        Next = 8,

        /// <summary>
        /// 提交退回
        /// </summary>
        Back = 9,

        /// <summary>
        /// 直接完成
        /// </summary>
        Finish = 10,

        /// <summary>
        /// 直接否决
        /// </summary>
        Reject = 11,

        /// <summary>
        /// 工作移交
        /// </summary>
        Transfer = 12,

        /// <summary>
        /// 点击撤销后的加载信息
        /// </summary>
        LoadUndo = 13,

        /// <summary>
        /// 撤销
        /// </summary>
        Undo = 14
    }
}
