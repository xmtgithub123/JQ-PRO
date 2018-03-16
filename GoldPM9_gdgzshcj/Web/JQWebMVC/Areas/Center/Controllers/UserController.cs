using Microsoft.Practices.Unity;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace JQ.BPM.Web.Areas.Center.Controllers
{
    public class UserController : JQ.Web.CoreController
    {

        /// <summary>
        /// 获取出用户和角色的名称
        /// </summary>
        /// <returns>{User:"",Role:""}</returns>
        public JsonResult GetUserAndRoleNames()
        {
            var userIDs = Request.Form["userIDs"];
            var roleIDs = Request.Form["roleIDs"];
            if (string.IsNullOrEmpty(userIDs) && string.IsNullOrEmpty(roleIDs))
            {
                return Json(new { User = "", Role = "" });
            }
            var user = "";
            var role = "";
            using (var dataAccessor = new Data.ProcessAccessor())
            {
                user = BPM.Configuration.BPMContext.Resolve<Core.Organization.IUserProvider>(new ParameterOverride("dbContext", dataAccessor)).GetNames(userIDs);
                role = BPM.Configuration.BPMContext.Resolve<Core.Organization.IRoleProvider>(new ParameterOverride("dbContext", dataAccessor)).GetNames(roleIDs);
            }
            return Json(new { User = user, Role = role });
        }

        public string GetFullList()
        {
            var queryContext = new Data.QueryContext();
            if (!string.IsNullOrEmpty((Request.Form["userIDs"] ?? "").Trim()))
            {
                queryContext.Criteries.Add("UserIDs", Request.Form["userIDs"]);
            }
            queryContext.IsPaging = false;
            var sbJson = new StringBuilder();
            using (var processAccessor = new Data.ProcessAccessor())
            {
                using (var dtDepartment = BPM.Configuration.BPMContext.Resolve<Core.Organization.IDepartmentProvider>(new ParameterOverride("dbContext", processAccessor)).GetFullList(new Data.QueryContext() { IsPaging = false }))
                {
                    using (var dtUser = BPM.Configuration.BPMContext.Resolve<Core.Organization.IUserProvider>(new ParameterOverride("dbContext", processAccessor)).GetFullList(queryContext))
                    {
                        RecuriseFullUserData(sbJson, dtDepartment, dtUser, 0);
                    }
                }

            }
            return sbJson.ToString();
        }

        private int RecuriseFullUserData(StringBuilder sbJson, DataTable dtDepartment, DataTable dtUser, int parentID)
        {
            //获取出子部门
            var departments = dtDepartment.Select("ParentID=" + parentID).OrderBy(m => m.Field<int>("Order"));
            var users = dtUser.Select("DepartmentID=" + parentID).OrderBy(m => m.Field<int>("Order"));
            if (departments.Count() == 0 && users.Count() == 0)
            {
                return 0;
            }
            if (parentID > 0)
            {
                sbJson.Append(",\"children\":");
            }
            sbJson.Append("[");
            var index = 0;
            foreach (var department in departments)
            {
                if (index != 0)
                {
                    sbJson.Append(",");
                }
                sbJson.Append("{\"id\": \"department_" + department["ID"].ToString() + "\",\"type\":\"department\",\"text\":\"" + HttpUtility.UrlEncode(department["Name"].ToString()) + "\",\"realID\":\"" + department["ID"].ToString() + "\"");
                // fa-address-book-o
                var dIndex = RecuriseFullUserData(sbJson, dtDepartment, dtUser, department.Field<int>("ID"));
                //var hasChild = false;
                //if (firstRow["UserID"].ToString() == "")
                //{
                //    //下级部门继续处理
                //    RecuriseFullUserData(sbJson, source, department.Key, true);
                //}
                //else
                //{
                //    //添加当前部门的用户
                //    sbJson.Append(",\"children\":[");
                //    var dIndex = RecuriseFullUserData(sbJson, source, department.Key, false);
                //    foreach (DataRow userRow in department.OrderBy(m => m.Field<int>("UserOrder")))
                //    {
                //        if (dIndex > 0)
                //        {
                //            sbJson.Append(",");
                //        }
                //        sbJson.Append("{\"id\":\"user_" + userRow["UserID"].ToString() + "\",\"type\":\"user\",\"text\":\"" + HttpUtility.UrlEncode(userRow["UserName"].ToString()) + "\",\"realID\":\"" + userRow["UserID"].ToString() + "\",\"iconCls\":\"fa\"}");
                //        // fa-user
                //        dIndex++;
                //    }
                //    sbJson.Append("]");
                //    hasChild = true;
                //}
                sbJson.Append(",\"iconCls\":\"fa" + (dIndex > 0 ? "" : " tree-folder-open") + "\"}");
                index++;
            }
            foreach (var user in users)
            {
                if (index != 0)
                {
                    sbJson.Append(",");
                }
                sbJson.Append("{\"id\":\"user_" + user["ID"].ToString() + "\",\"type\":\"user\",\"text\":\"" + HttpUtility.UrlEncode(user["Name"].ToString()) + "\",\"realID\":\"" + user["ID"].ToString() + "\",\"iconCls\":\"fa\"}");
                index++;
            }
            sbJson.Append("]");
            return index;
        }
    }
}