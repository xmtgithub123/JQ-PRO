using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSql.Design.Dto
{
    /// <summary>
    /// 判断你是否有不同类型项目任务提醒的权限
    /// </summary>
    public class DesTaskRemindPermission
    {
        public EmpSession userInfo;

        public DesTaskRemindPermission(EmpSession UserInfo)
        {
            userInfo = UserInfo;
        }
        public DesTaskRemindPermission(int EmpId)
        {
            userInfo = new EmpSession()
            {
                EmpID = EmpId
            };
        }

        /// <summary>
        /// 是否有录入计划表的权限
        /// </summary>
        public bool PlanTableList
        {
            get
            {
                return ContainPermission("PlanTableList");
            }
        }
        /// <summary>
        /// 是否有项目策划权
        /// </summary>
        public bool ProjectPlanList
        {
            get
            {
                return ContainPermission("ProjectPlanList");
            }
        }
        /// <summary>
        /// 是否有提资策划权
        /// </summary>
        public bool ExchPlanList
        {
            get
            {
                return ContainPermission("ExchPlanList");
            }
        }
        /// <summary>
        /// 是否有专业策划权
        /// </summary>
        public bool SpecPlanList
        {
            get
            {
                return ContainPermission("SpecPlanList");
            }
        }
        /// <summary>
        /// 是否有关键节点维护权
        /// </summary>
        public bool ProjGanttList
        {
            get
            {
                return ContainPermission("ProjGanttList");
            }
        }

        /// <summary>
        /// 判断用户是否存在某权限
        /// </summary>
        /// <param name="permissionName">权限ENG</param>
        /// <returns></returns>
        private bool ContainPermission(string permissionName)
        {
            return new DBSql.Sys.BaseMenu().getPermit(userInfo.EmpID, permissionName);
        }
    }
}
