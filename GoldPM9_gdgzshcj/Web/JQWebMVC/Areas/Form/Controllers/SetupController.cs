using JQ.BPM.Configuration;
using JQ.BPM.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using AppUtility = JQ.BPM.Configuration.Utility;

namespace JQ.BPM.Web.Areas.Form.Controllers
{
    public class SetupController : JQ.Web.CoreController
    {
        public ViewResult DataBase()
        {
            var section = BPMContext.GetSection("DataBase");
            if (section != null)
            {
                ViewBag.ServerName = AppUtility.GetXmlNodeText(section, "ServerName");
                ViewBag.DataBaseName = AppUtility.GetXmlNodeText(section, "DataBaseName");
                //1：Windows身份验证  2：SQLServer身份验证
                ViewBag.AuthMode = AppUtility.GetXmlNodeText(section, "AuthMode");
                ViewBag.LoginName = AppUtility.GetXmlNodeText(section, "LoginName");
                ViewBag.Password = AppUtility.GetXmlNodeText(section, "Password");
            }
            else
            {
                ViewBag.ServerName = "";
                ViewBag.DataBaseName = "";
                ViewBag.AuthMode = "2";
                ViewBag.LoginName = "";
                ViewBag.Password = "";
            }
            return View();
        }

        public ViewResult TableManage()
        {
            return View();
        }

        public ViewResult TableEdit()
        {
            var name = "";
            var id = AppUtility.ToInt(Request.QueryString["id"]);
            if (id > 0)
            {
                using (var dataAccessor = new Data.FormAccessor())
                {
                    var formTable = dataAccessor.FormTables.FirstOrDefault(m => m.ID == id);
                    if (formTable != null)
                    {
                        name = formTable.Name;
                    }
                }
            }
            ViewBag.ID = id;
            ViewBag.Name = name;
            return View();
        }

        public ViewResult TableColumnEdit()
        {
            var id = AppUtility.ToInt(Request.QueryString["id"]);
            ViewBag.TableID = AppUtility.ToInt(Request.QueryString["tableID"]);
            ViewBag.ID = id;
            if (id > 0)
            {
                using (var dataAccessor = new Data.FormAccessor())
                {
                    var formTableColumn = dataAccessor.FormTableColumns.FirstOrDefault(m => m.ID == id);
                    if (formTableColumn != null)
                    {
                        return View(formTableColumn);
                    }
                }
            }
            return View(new Model.FormTableColumn());
        }

        public JsonResult SaveDataBase()
        {
            var section = BPMContext.GetSection("DataBase");
            if (section == null)
            {
                section = BPMContext.CreateSection("DataBase");
            }
            var xmlElement = section.SelectSingleNode("ServerName");
            AppUtility.AppendXmlNode(section, "ServerName", Request.Form["txtServerName"] ?? "");
            AppUtility.AppendXmlNode(section, "DataBaseName", Request.Form["txtDataBaseName"] ?? "");
            AppUtility.AppendXmlNode(section, "AuthMode", Request.Form["listAuthMode"] ?? "");
            AppUtility.AppendXmlNode(section, "LoginName", Request.Form["txtLoginName"] ?? "");
            AppUtility.AppendXmlNode(section, "Password", Request.Form["txtPassword"] ?? "");
            BPMContext.SaveSetting();
            return Json(new { Result = true });
        }

        public JsonResult TestDataBaseConnection()
        {
            var conenctionString = JQ.BPM.Data.DBUtility.BuildConnection(Request.Form["txtServerName"] ?? "", Request.Form["txtDataBaseName"] ?? "", (Request.Form["listAuthMode"] ?? "") == "1", Request.Form["txtLoginName"] ?? "", Request.Form["txtPassword"] ?? "");
            try
            {
                using (var sqlConnection = new System.Data.SqlClient.SqlConnection(conenctionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand cmd = new SqlCommand("select top 1 name from sys.objects", sqlConnection))
                    {
                        cmd.ExecuteScalar();
                    }
                }
                return Json(new { Result = true });
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Message = ex.Message });
            }
        }

        public JsonResult TableSave()
        {
            if (string.IsNullOrEmpty(Request.Form["txtName"]))
            {
                return Json(new { Result = false, Message = "请输入数据表名称" });
            }
            var id = AppUtility.ToInt(Request.QueryString["id"]);
            Model.FormTable formTable = null;
            using (var dataAccessor = new Data.FormAccessor())
            {
                if (id > 0)
                {
                    formTable = dataAccessor.FormTables.FirstOrDefault(m => m.ID == id);
                    if (formTable == null)
                    {
                        return Json(new { Result = false, Message = "无法找到需要保存的实例" });
                    }
                }
                else
                {
                    formTable = new Model.FormTable();
                    var data = dataAccessor.FormTables.OrderByDescending(m => m.Index).FirstOrDefault();
                    if (data == null)
                    {
                        formTable.Index = 1;
                    }
                    else
                    {
                        formTable.Index = data.Index + 1;
                    }
                    formTable.IsDelete = false;
                    formTable.TableName = "FormTable_" + formTable.Index;
                    dataAccessor.FormTables.Add(formTable);
                    //创建新的表
                    Core.Form.DBCommon.CreateNewTable(formTable.TableName);
                }
                formTable.Name = Request.Form["txtName"] ?? "";
                dataAccessor.SaveChanges();
            }
            return Json(new { Result = true });
        }

        [ValidateInput(false)]
        public JsonResult TableColumnSave()
        {
            var id = AppUtility.ToInt(Request.QueryString["id"]);
            //创建数据列
            Model.FormTableColumn column = null;
            using (var dataAccessor = new Data.FormAccessor())
            {
                if (id > 0)
                {
                    column = dataAccessor.FormTableColumns.FirstOrDefault(m => m.ID == id);
                    if (column == null)
                    {
                        return Json(new { Result = false, Message = "无法找到需要保存的实例" });
                    }
                }
                else
                {
                    column = new Model.FormTableColumn()
                    {
                        FormTableID = AppUtility.ToInt(Request.QueryString["tableID"])
                    };
                    if (column.FormTableID == 0)
                    {
                        return Json(new { Result = false, Message = "无法找到需要保存的数据表" });
                    }
                    var data = dataAccessor.FormTableColumns.Where(m => !m.IsSystem && m.FormTableID == column.FormTableID).OrderByDescending(m => m.Index).FirstOrDefault();
                    if (data == null)
                    {
                        column.Index = 1;
                    }
                    else
                    {
                        column.Index = data.Index + 1;
                    }
                    column.IsDelete = false;
                    column.IsSystem = false;
                    column.ColumnName = "Column_" + column.Index;
                }
                //设置值
                column.Name = Request.Form["txtName"] ?? "";
                column.Type = Request.Form["listTypes"] ?? "";
                column.DefaultValueType = AppUtility.ToInt(Request.Form["listDefaultValueType"] ?? "");
                if (column.DefaultValueType > 0)
                {
                    column.DefaultValue = Request.Form["txtDefaultValue"] ?? "";
                }
                else
                {
                    column.DefaultValue = "";
                }
                column.IsNullable = Request.Form["cbIsNullable"] != null;
                switch (column.Type)
                {
                    case "nvarchar":
                        column.MaxLength = AppUtility.ToInt(Request.Form["txtLength"]);
                        column.Precision = 0;
                        break;
                    case "int":
                        column.MaxLength = 0;
                        column.Precision = 0;
                        break;
                    case "decimal":
                        column.MaxLength = 0;
                        column.Precision = AppUtility.ToInt(Request.Form["txtPrecision"]);
                        break;
                    case "datetime":
                        column.MaxLength = 0;
                        column.Precision = 0;
                        column.IsNullable = true;
                        break;
                }
                var formTable = dataAccessor.FormTables.FirstOrDefault(m => m.ID == column.FormTableID);
                if (formTable == null)
                {
                    return Json(new { Result = false, Message = "无法获取到需要保存的数据表" });
                }
                if (column.ID == 0)
                {
                    try
                    {
                        Core.Form.DBCommon.CreateTableColumn(formTable.TableName, column);
                    }
                    catch (Model.RouteException re)
                    {
                        return Json(new { Result = false, Message = re.Message });
                    }
                    dataAccessor.FormTableColumns.Add(column);
                }
                else
                {
                    var originalValues = dataAccessor.Entry(column).OriginalValues;
                    try
                    {
                        Core.Form.DBCommon.AlterTableColumn(formTable.TableName, column, (column.Type != originalValues.GetValue<string>("Type") || column.MaxLength != originalValues.GetValue<int>("MaxLength") || column.Precision != originalValues.GetValue<int>("Precision") || column.IsNullable != originalValues.GetValue<bool>("IsNullable")), originalValues.GetValue<int>("DefaultValueType"), originalValues.GetValue<string>("DefaultValue"));
                    }
                    catch (Model.RouteException re)
                    {
                        return Json(new { Result = false, Message = re.Message });
                    }
                }
                dataAccessor.SaveChanges();
            }
            return Json(new { Result = true });
        }

        public JsonResult TableDelete()
        {
            var ids = (Request.Form["ids"] ?? "").Trim(',');
            if (string.IsNullOrEmpty(ids))
            {
                return Json(new { Result = false, Message = "参数错误" });
            }
            using (var formAccessor = new JQ.BPM.Data.FormAccessor())
            {
                formAccessor.Database.ExecuteSqlCommand("UPDATE FormTable SET IsDelete=1 WHERE ID IN (" + ids + ")");
                return Json(new { Result = true });
            }
        }

        public JsonResult TableColumnDelete()
        {
            var ids = (Request.Form["ids"] ?? "").Trim(',');
            if (string.IsNullOrEmpty(ids))
            {
                return Json(new { Result = false, Message = "参数错误" });
            }
            using (var formAccessor = new JQ.BPM.Data.FormAccessor())
            {
                formAccessor.Database.ExecuteSqlCommand("UPDATE FormTableColumn SET IsDelete=1 WHERE ID IN (" + ids + ")");
                return Json(new { Result = true });
            }
        }

        public JsonResult GetTableList()
        {
            var pageIndex = AppUtility.ToInt(Request.Form["page"]) - 1;
            var pageSize = AppUtility.ToInt(Request.Form["rows"]);
            using (var dataAccessor = new Data.FormAccessor())
            {
                return Json(new { total = dataAccessor.FormTables.Count(m => !m.IsDelete), page = pageIndex + 1, rows = dataAccessor.Database.SqlQuery<Model.FormTable>("SELECT * FROM (SELECT ROW_Number() OVER(ORDER BY ID DESC) as ROW_NUMBER,* FROM [FormTable] WHERE IsDelete=0) AS tb WHERE tb.ROW_NUMBER BETWEEN " + pageIndex * pageSize + " AND " + (pageIndex + 1) * pageSize).ToList() });
            }
        }

        public string GetTableColumnList()
        {
            var tableID = AppUtility.ToInt(Request.Form["tableID"]);
            if (tableID == 0)
            {
                return "{\"total\":0,\"rows\":[]}";
            }
            QueryContext queryContext = new QueryContext()
            {
                PageIndex = AppUtility.ToInt(Request.Form["page"]) - 1,
                PageSize = AppUtility.ToInt(Request.Form["rows"])
            };
            using (var dt = Core.Form.DBCommon.GetFormTableColumns(queryContext, tableID))
            {
                var list = new List<Dictionary<string, object>>();
                foreach (DataRow dr in dt.Rows)
                {
                    //var defaultValue = dr["DefaultValue"].ToString();
                    //var defaultValueType = "";
                    //dr["DefaultValue"] = Core.Form.DBCommon.GetDefalutValue(dr["DefaultValue"].ToString(), ref defaultValueType);
                    //dr["DefaultValueType"] = defaultValueType;
                    switch (dr["TypeName"].ToString())
                    {
                        case "nvarchar":
                            var maxLength = AppUtility.ToInt(dr["MaxLength"].ToString());
                            if (maxLength != 0)
                            {
                                dr["TypeName"] = "文本（" + maxLength + "）";
                            }
                            else
                            {
                                dr["TypeName"] = "文本";
                            }
                            break;
                        case "int":
                            dr["TypeName"] = "整数";
                            break;
                        case "datetime":
                            dr["TypeName"] = "日期";
                            break;
                        case "decimal":
                            dr["TypeName"] = "小数（" + dr["Precision"].ToString() + "）";
                            break;
                    }
                    Dictionary<string, object> result = new Dictionary<string, object>();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        result.Add(dc.ColumnName, dr[dc].ToString());
                    }
                    list.Add(result);
                }
                return AppUtility.ObjectToJson(new
                {
                    total = queryContext.TotalRowAmount,
                    rows = list
                });
            }
        }
    }
}