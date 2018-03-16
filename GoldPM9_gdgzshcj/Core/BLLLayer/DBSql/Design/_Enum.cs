using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSql.Design
{
    /// <summary>
    /// 任务分组类型
    /// </summary>
    public enum TaskGroupType : int
    {
        开始 = 0,
        阶段 = 1,
        分组 = 2
    }

    /// <summary>
    /// 任务分组状态
    /// </summary>
    public enum TaskGroupStatus : int
    {
        未启动 = 0,
        已安排 = 1,
        进行中 = 2,
        已完成 = 3,
        已停止 = 4
    }

    /// <summary>
    /// 任务优先级
    /// </summary>
    public enum TaskPriority : int
    {
        普通 = 0,
        重要 = 1,
        紧急 = 2
    }

    /// <summary>
    /// 任务状态
    /// </summary>
    public enum TaskStatus : int
    {
        未启动 = 0,
        已安排 = 1,
        进行中 = 2,
        已完成 = 3,
        已停止 = 4
    }

    /// <summary>
    /// 任务类型
    /// </summary>
    public enum TaskType : int
    {
        普通任务 = 0,
        专业任务 = 1
    }

    /// <summary>
    /// 任务层级类型
    /// </summary>
    public enum TaskLevelType : int
    {
        无层级 = 0,
        层级子 = 1,
        层级父 = 2
    }

    /// <summary>
    /// 生产流程节点类型
    /// </summary>
    public enum FlowNodeType : int
    {
        设计 = 19,
        校对 = 20,
        审核 = 21,
        批准 = 22
    }

    /// <summary>
    /// 节点状态
    /// </summary>
    public enum FlowNodeStatus : int
    {
        未安排 = 0,
        已安排 = 1,
        进行中 = 2,
        已完成 = 3,
        已停止 = 4,
        已回退 = 5
    }

    /// <summary>
    /// 流程对应专业类型
    /// </summary>
    public enum FlowSpecType : int
    {
        普通专业 = 0,
        汇总专业 = 1
    }

    /// <summary>
    /// 节点人员类型
    /// </summary>
    public enum FlowNodeEmpType : int
    {
        资格人员 = 0,
        任意选人 = 1,
        指定人员 = 2, 
        类型人员 = 3, 
        节点人员 = 4
    }

    /// <summary>
    /// 会签 或 提资 的 接收状态： 0 未接收 1 不同意 2 通过
    /// </summary>
    public enum RecStatus
    {
        未接收 = 0,
        退回 = 1,
        通过 = 2
    }

    /// <summary>
    /// 会签状态: 0 会签中 1 已会签
    /// </summary>
    public enum MutiStatus
    {
        会签中 = 0,
        已会签 = 1
    }
}
