using JQ.BPM.Configuration;
using JQ.BPM.Data.Extern;
using JQ.BPM.Model;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml;
using AppUtility = JQ.BPM.Configuration.Utility;

namespace JQ.BPM.Web.Areas.Process.Controllers
{
    public class SetupController : JQ.Web.CoreController
    {
        public ViewResult Variable()
        {
            return View();
        }

        /// <summary>
        /// 系统变量编辑页面
        /// </summary>
        /// <returns></returns>
        public ViewResult VariableEdit()
        {
            var dataID = AppUtility.ToInt(Request.QueryString["id"]);
            SystemVariable data = null;
            if (dataID > 0)
            {
                using (var accessor = new Data.ProcessAccessor())
                {
                    data = accessor.SystemVariables.FirstOrDefault(m => m.ID == dataID);
                }
            }
            if (data == null)
            {
                data = new SystemVariable();
                data.Type = "string";
            }
            return View(data);
        }

        /// <summary>
        /// 保存系统变量
        /// </summary>
        /// <returns></returns>
        public JsonResult VariableSave()
        {
            var dataID = AppUtility.ToInt(Request.QueryString["id"]);
            Model.SystemVariable variable = null;
            using (var accessor = new Data.ProcessAccessor())
            {
                //处理ID
                if (dataID == 0)
                {
                    variable = new Model.SystemVariable();
                }
                else
                {
                    variable = accessor.SystemVariables.FirstOrDefault(m => m.ID == dataID);
                    if (variable == null)
                    {
                        return Json(new { Result = false, Message = "无法找到需要修改的实例" });
                    }
                }
                variable.Name = Request.Form["name"] ?? "";
                //验证名称是否已经存在
                var result = accessor.Database.SqlQuery<int>("SELECT CASE WHEN EXISTS(SELECT 1 FROM SystemVariable WHERE Name=@name AND ID!=" + variable.ID + ") THEN 1 ELSE 0 END", new SqlParameter("@name", variable.Name)).FirstOrDefault();
                if (result == 1)
                {
                    return Json(new { Result = false, Message = "变量名称{" + variable.Name + "}已经存在！" });
                }
                variable.Type = Request.Form["type"] ?? "";
                variable.DefaultValue = Request.Form["defaultValue"] ?? "";
                if (!string.IsNullOrEmpty(variable.DefaultValue) && variable.Type == "number")
                {
                    //当为数值时确保值为数值类型
                    variable.DefaultValue = AppUtility.To<double>(variable.DefaultValue).ToString();
                }
                if (dataID == 0)
                {
                    accessor.SystemVariables.Add(variable);
                }
                accessor.SaveChanges();
            }
            return Json(new { Result = true });
        }

        /// <summary>
        /// 获取出变量列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetVariableList()
        {
            var pageIndex = AppUtility.ToInt(Request.Form["page"]) - 1;
            var pageSize = AppUtility.ToInt(Request.Form["rows"]);
            using (var dataAccessor = new Data.ProcessAccessor())
            {
                return Json(new { total = dataAccessor.SystemVariables.Count(m => !m.IsDelete), rows = dataAccessor.Database.SqlQuery<Model.SystemVariable>("SELECT * FROM (SELECT ROW_Number() OVER(ORDER BY ID DESC) as ROW_NUMBER,* FROM [SystemVariable] WHERE IsDelete=0) AS tb WHERE tb.ROW_NUMBER BETWEEN " + pageIndex * pageSize + " AND " + (pageIndex + 1) * pageSize).ToList() });
            }
        }

        public string GetVariableUserList()
        {
            var variableID = AppUtility.ToInt(Request.Form["variableID"]);
            if (variableID == 0)
            {
                return AppUtility.ObjectToJson(new { total = 0, rows = new object[] { } });
            }
            Data.QueryContext queryContext = new Data.QueryContext()
            {
                PageIndex = AppUtility.ToInt(Request.Form["page"]) - 1,
                PageSize = AppUtility.ToInt(Request.Form["rows"])
            };
            queryContext.Criteries.Add("VariableID", variableID);
            using (var processAccessor = new Data.ProcessAccessor())
            {
                using (var dt = BPMContext.Resolve<Core.Organization.IUserProvider>(new ParameterOverride("dbContext", processAccessor)).GetVariableList(queryContext))
                {
                    return AppUtility.ObjectToJson(new { total = queryContext.TotalRowAmount, rows = dt });
                }
            }
        }

        public JsonResult VariableDelete()
        {
            var ids = (Request.Form["ids"] ?? "").Trim(',');
            if (string.IsNullOrEmpty(ids))
            {
                return Json(new { Result = false, Message = "参数错误" });
            }
            using (var formAccessor = new JQ.BPM.Data.ProcessAccessor())
            {
                formAccessor.Database.ExecuteSqlCommand("UPDATE SystemVariable SET IsDelete=1 WHERE ID IN (" + ids + ")");
                return Json(new { Result = true });
            }
        }

        [ValidateInput(false)]
        public JsonResult VariableUserSave()
        {
            var data = Request.Form["data"] ?? "";
            var variableID = AppUtility.ToInt(Request.Form["variableID"]);
            if (variableID == 0 || string.IsNullOrEmpty(data))
            {
                return Json(new { Result = false, Message = "参数错误！" });
            }
            XmlDocument xmlDocument = new XmlDocument();
            try
            {
                xmlDocument.LoadXml(data);
            }
            catch
            {
                return Json(new { Result = false, Message = "参数错误！" });
            }
            var sbSQL = new StringBuilder();
            var sqlParameters = new List<SqlParameter>();
            using (var dataAccessor = new Data.ProcessAccessor())
            {
                foreach (XmlElement xmlElement in xmlDocument.SelectNodes("Root/Item"))
                {
                    var id = AppUtility.ToInt(xmlElement.GetAttribute("id"));
                    if (id == 0)
                    {
                        var userID = AppUtility.ToInt(xmlElement.GetAttribute("userid"));
                        if (userID == 0)
                        {
                            continue;
                        }
                        //验证是否已经有值
                        id = dataAccessor.Database.SqlQuery<int>("SELECT ID FROM SystemVariableUser WHERE UserID=" + userID + " AND SystemVariableID=" + variableID).FirstOrDefault();
                        if (id == 0)
                        {
                            dataAccessor.SystemVariableUsers.Add(new SystemVariableUser()
                            {
                                SystemVariableID = variableID,
                                UserID = userID,
                                Value = xmlElement.GetAttribute("value")
                            });
                            dataAccessor.SaveChanges();
                        }
                    }
                    if (id > 0)
                    {
                        sbSQL.Append(" UPDATE SystemVariableUser SET Value=@value" + id + " WHERE ID=" + id);
                        sqlParameters.Add(new SqlParameter("@value" + id, xmlElement.GetAttribute("value")));
                    }
                }
                if (sbSQL.Length > 0)
                {
                    dataAccessor.Database.ExecuteSqlCommand(sbSQL.ToString(), sqlParameters.ToArray());
                }
            }
            return Json(new { Result = true });
        }
    }
}