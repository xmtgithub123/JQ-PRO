using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSql.Design.Dto
{
    public class DesTaskInput : DataModel.Models.DesTask
    {
        /// <summary>
        /// 上次任务负责人
        /// </summary>
        public int TaskOldEmpId { get; set; }

        /// <summary>
        /// 上次流程
        /// </summary>
        public int OldFlowId { get; set; }

        /// <summary>
        /// 流程模板json格式
        /// </summary>
        public string TaskFlowModelJson { get; set; }

        /// <summary>
        /// 下一流程流程模板json格式
        /// </summary>
        public string TaskNextFlowModelJson { get; set; }

        /// <summary>
        /// 计划时间间隔
        /// </summary>
        public int DatePlanDiff { get; set; }

        /// <summary>
        /// 设计人员 批量策划时用到
        /// </summary>
        public int SheJi
        {
            get; set;
        }

        /// <summary>
        /// 校核人员 批量策划时用到
        /// </summary>
        public int JiaoHe
        {
            get; set;
        }
    }
}
