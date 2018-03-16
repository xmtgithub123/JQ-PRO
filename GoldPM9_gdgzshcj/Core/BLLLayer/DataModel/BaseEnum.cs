using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel
{
    /// <summary>
    /// 权限类型
    /// </summary>
    public enum PermitType : int
    {
        add = 1,
        edit = 2,
        del = 4,
        export = 8,
        emp = 16,
        dep = 32,
        allview = 64,
        alledit = 128,
        allDown = 256,
    }
    /// <summary>
    ///  节点类型 现在对应 
    ///  设计 = 设计,
    ///  校对 = 校对,
    ///  审核 = 审核,
    ///  批准 = 批准,
    ///  预算批准=主设
    ///  
    /// </summary>
    public enum NodeType
    {
        卷 = 82,
        册 = 83,
        包 = 84,
        图纸 = 85,

        专业 = 16,
        阶段 = 17,
        项目设总 = 18,
        主设 = 73,

        设计 = 19,
        校对 = 20,
        审核 = 21,
        批准 = 22,
        其它 = 23,
        会签 = 86,
    }

    /// <summary>
    /// 节点状态
    /// </summary>
    public enum NodeStatus
    {
        未安排 = 27,
        安排 = 28,
        轮到 = 29,
        进行 = 30,
        问题 = 31,
        回退 = 32,
        被退 = 33,
        经过 = 34,
        完成 = 35,
        超期 = 36,
        超期完成 = 87,
        会签中 = 88,
        跳过 = 112
    }
    /// <summary>
    /// 节点动作
    /// </summary>
    public enum NodeAction
    {
        安排 = 38,
        轮到 = 39,
        换人 = 40,
        变更 = 41,
        接受 = 42,
        问题 = 43,
        跳过 = 44,
        回退 = 45,
        被退 = 46,
        完成 = 47,
    }
    /// <summary>
    /// 工程状态
    /// </summary>
    public enum ProjStatus
    {
        未启动 = 49,
        启动 = 50,
        停止 = 51,
        完成 = 52,
    }
    /// <summary>
    /// 设计评价
    /// </summary>
    public enum DesignQuality
    {
        优秀 = 54,
        良好 = 55,
        合格 = 56,
        不合格 = 57,
    }
    /// <summary>
    /// 错误类型
    /// </summary>
    public enum DesignErrorType
    {
        原则性差错 = 59,
        技术性差错 = 60,
        一般性差错 = 61,
        建议 = 97,
    }

    public enum FlowStatus
    {
        未提交审批 = 1,
        审批中 = 2,
        审批结束 = 3,
        审批不同意 = 4
    }

    /// <summary>
    /// BaseDataSystem　中图档的类型
    /// </summary>
    public enum VolCatalogType
    {
        项目 = 114,
        外委情况 = 115,
        收款合同情况 = 116,
        付款合同情况 = 118,
        工程 = 119,
        对外收资 = 120,
        工程记事 = 121,
        工程表单 = 122,
        成品进度 = 123,
        提资进度 = 124,
        阶段 = 795,
        专业 = 789,
        设计成品 = 128,
        卷册 = 792,
        底图 = 791,
        蓝图 = 790,
        原始资料 = 132,
        其他 = 793,
    }

    /// <summary>
    /// 合同履行方式
    /// </summary>
    public enum ConDealWays
    {
        开口 = 1316,
        闭口 = 1317
    }

    /// <summary>
    /// 项目绩效结清状态
    /// </summary>
    public enum ProjBalanceState
    {
        已结清 = 1411,
        未结清 = 1412
    }
    /// <summary>
    /// 绩效结算状态
    /// </summary>
    public enum BalanceStatus
    {
        预结算 = 0,
        已结算 = 1
    }


    public enum IsoForm
    {
        IsoFormGrade = 31,
        IsoFormAccept = 32,
    }

    public enum ProjectPhase
    {
        选址选线 = 1079,
        可研 = 59,
        初设 = 60,
        施工图 = 61,
        竣工图 = 62,
        前期投标文件 = 1468,
        研发 = 1479,
        评审 = 1480,
        结题宣贯 = 1481,
        立项 = 1482,
    }
    public enum ProjectSpecial
    {
        商务部分 = 1635,
        技术部分 = 1678,
        配电 = 1296,
        技经 = 1294,
        配电自动化 = 1295
    }
    public enum ProjectType
    {
        主网类别 = 1583,
        配电类别 = 1577,
        规划可研类 = 1571,
        其他类 = 1478,
    }
    public enum KeyPointType
    {
        工代 = 1444,
        系统管理员 = 234
    }
    public enum ProjEventType
    {
        问题记录 = 1438,
    }
    public enum GanttTypeId
    {
        DesTaskGroup = 0,
        DesTask = 1,
    }

    public enum ProgressStatus
    {
        中标 = 1342,
    }

    public enum ProjectTypeModel
    {
        主网 = 1,
        配网 = 2,
        可研 = 3

    }
}
