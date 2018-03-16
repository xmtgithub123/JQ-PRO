using DBSql.Design.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSql.Design
{
    public static class _Public
    {
        private static DBSql.Design.DesTaskGroup desTaskGroupDB = new DBSql.Design.DesTaskGroup();
        private static DBSql.Design.DesTask desTaskDB = new DBSql.Design.DesTask();

        /// <summary>
        /// 通知 用户 更新待办任务 数
        /// </summary>
        /// <param name="type"></param>
        /// <param name="EmpID"></param>
        public static void SendNotify(string type, int EmpID)
        {
            if (type == "ChangeTodoTaskAmount")
            {
                var t = JQ.Util.IO.MessageMonitor.NotifyAsync(EmpID, delegate (int empID)
                {
                    // 更新 待办任务数
                    string DesTaskApproveMode = new DBSql.Sys.BaseConfig().GetBaseConfigInfo("DesTaskApproveMode").ConfigValue; // 任务校审模式   单步 0；同步 1
                    return new { action = "ChangeTodoTaskAmount", amount = desTaskDB.GetTaskRemindTodoAmount(
                            empID,
                            DesTaskApproveMode == "0" ?
                                String.Format("{0}", DBSql.Design.FlowNodeStatus.进行中.ToString("D"))
                                :
                                String.Format("{0},{1}", DBSql.Design.FlowNodeStatus.已安排.ToString("D"), DBSql.Design.FlowNodeStatus.进行中.ToString("D")),
                            new DesTaskRemindPermission(empID)
                        )
                    };
                });
            }
        }
    }
}
