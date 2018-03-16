using JQ.BPM.Configuration;
using JQ.BPM.Data;
using JQ.BPM.Data.Extern;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Xml;
using AppUtility = JQ.BPM.Configuration.Utility;

namespace JQ.BPM.Web.Areas.Form.Controllers
{
    public class ManageController : Controller
    {
        public ViewResult Launcher()
        {
            return View();
        }

        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        public ViewResult List()
        {
            var formTemplateID = AppUtility.ToInt(Request.QueryString["formTemplateID"]);
            var xml = "";
            var tableName = "";
            var width = 0;
            if (formTemplateID > 0)
            {
                using (var dataAccessor = new Data.FormAccessor())
                {
                    var formTemplate = dataAccessor.FormTemplates.FirstOrDefault(m => m.ID == formTemplateID);
                    if (formTemplate != null)
                    {
                        ViewBag.Name = formTemplate.Name;
                        ViewBag.Version = formTemplate.Version;
                        width = formTemplate.Width;
                        XmlDocument xmlDocument = new XmlDocument();
                        xmlDocument.LoadXml(formTemplate.Content);
                        tableName = xmlDocument.DocumentElement.GetAttribute("primaryTable");
                        //获取出设置
                        var xColumns = xmlDocument.SelectSingleNode("Root/Columns");
                        if (xColumns != null && !string.IsNullOrEmpty(tableName))
                        {
                            //获取出存储模式
                            var columns = Core.Form.DBCommon.GetColumns(AppUtility.GetXmlNodeText(xmlDocument.DocumentElement, "Options/Option[@name='storageMode']"), tableName);
                            if (columns != null && columns.Count > 0)
                            {
                                var tproperty = columns[0].Attributes.GetType().GetProperty("Type");
                                foreach (var column in columns)
                                {
                                    var xmlElement = (XmlElement)xColumns.SelectSingleNode("Column[@dataField=\"" + column.Value + "\"]");
                                    if (xmlElement != null)
                                    {
                                        xmlElement.SetAttribute("dataType", tproperty.GetValue(column.Attributes).ToString());
                                    }
                                }
                            }
                            xml = xColumns.OuterXml;
                        }
                        Core.Form.Processor.SetItems(xmlDocument, dataAccessor);
                        var xKeywords = xmlDocument.SelectSingleNode("Root/Keywords");
                        if (xKeywords != null)
                        {
                            xml += xKeywords.OuterXml;
                        }
                        var xConditions = xmlDocument.SelectSingleNode("Root/Conditions");
                        if (xConditions != null)
                        {
                            var xProperties = xmlDocument.SelectNodes("Root/Properties/Property[not(@inlineTableName or @type=\"table\")]");
                            foreach (XmlElement xmlElement in xConditions.SelectNodes("Condition"))
                            {
                                var dataField = xmlElement.GetAttribute("dataField");
                                if (string.IsNullOrEmpty(dataField))
                                {
                                    continue;
                                }
                                if (dataField == "CreateTime")
                                {
                                    xmlElement.SetAttribute("type", "datebox");
                                    xmlElement.SetAttribute("formart", "yyyy-MM-dd");
                                    continue;
                                }
                                var isIn = false;
                                foreach (XmlElement xProperty in xProperties)
                                {
                                    var type = xProperty.GetAttribute("type");
                                    switch (type)
                                    {
                                        case "table":
                                            break;
                                        case "select":
                                        case "checkbox":
                                        case "radio":
                                            if (xProperty.GetAttribute("valueField") == dataField)
                                            {
                                                isIn = true;
                                                xmlElement.SetAttribute("type", type);
                                                foreach (XmlElement xItem in xProperty.SelectNodes("items/item"))
                                                {
                                                    xmlElement.AppendChild(xItem);
                                                }
                                            }
                                            else if (xProperty.GetAttribute("textField") == dataField)
                                            {
                                                isIn = true;
                                                xmlElement.SetAttribute("type", type);
                                                xmlElement.SetAttribute("dataField", xProperty.GetAttribute("valueField"));
                                                foreach (XmlElement xItem in xProperty.SelectNodes("items/item"))
                                                {
                                                    xmlElement.AppendChild(xItem);
                                                }
                                            }
                                            break;
                                        case "numberbox":
                                            if (xProperty.GetAttribute("dataField") == dataField)
                                            {
                                                isIn = true;
                                                xmlElement.SetAttribute("type", type);
                                                xmlElement.SetAttribute("precision", xProperty.GetAttribute("precision"));
                                            }
                                            break;
                                        default:
                                            if (xProperty.GetAttribute("dataField") == dataField)
                                            {
                                                isIn = true;
                                                xmlElement.SetAttribute("type", type);
                                            }
                                            break;
                                    }
                                    if (isIn)
                                    {
                                        break;
                                    }
                                }
                            }
                            xml += xConditions.OuterXml;
                        }
                    }
                }
            }
            ViewBag.Xml = "<Root>" + xml + "</Root>";
            ViewBag.FormTemplateID = formTemplateID;
            ViewBag.TableName = tableName;
            ViewBag.Width = width;
            ViewBag.CurrentUserID = BPMContext.CurrentUser.ID;
            return View();
        }

        public ViewResult TreeList()
        {
            return View();
        }

        /// <summary>
        /// 获取出表单间关系的树列表
        /// </summary>
        /// <returns></returns>
        public string GetRelationTreeData()
        {
            using (Core.Form.Processor processor = new Core.Form.Processor())
            {
                processor.FormTemplateID = AppUtility.To<int>(Request.Form["formTemplateID"]);
                processor.FormID = AppUtility.To<int>(Request.Form["formID"]);
                //获取出数据
                return processor.GetRelationTreeData();
            }
        }

        /// <summary>
        /// 编辑页面
        /// </summary>
        /// <returns></returns>
        public ViewResult Edit()
        {
            using (Core.Form.Processor processor = new Core.Form.Processor())
            {
                processor.FormTemplateID = AppUtility.To<int>(Request.QueryString["formTemplateID"]);
                processor.FormID = AppUtility.To<int>(Request.QueryString["id"]);
                processor.ProcessID = AppUtility.To<int>(Request.QueryString["processID"]);
                processor.ActivityID = AppUtility.To<int>(Request.QueryString["activityID"]);
                processor.Creator = BPMContext.CurrentUser.ID;
                processor.DepartmentID = BPMContext.CurrentUser.DepartmentID;
                ViewBag.Content = processor.GetLoadData().OuterXml;
                ViewBag.DataID = processor.FormID;
                ViewBag.Html = processor.Html;
            }
            ViewBag.CurrentDate = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.CurrentUserName = BPMContext.CurrentUser.Name;
            ViewBag.CurrentDepartmentName = BPMContext.CurrentUser.DepartmentName;
            return View();
        }

        public ViewResult Print()
        {
            return View();
        }

        /// <summary>
        /// 获取出已经发布的表单模版
        /// </summary>
        /// <returns></returns>
        public string GetPublishedTemplateList()
        {
            Data.QueryContext queryContext = new QueryContext();
            queryContext.PageIndex = AppUtility.ToInt(Request.Form["page"]) - 1;
            queryContext.PageSize = AppUtility.ToInt(Request.Form["rows"]);
            queryContext.Criteries.Add("IsCurrent", "1");
            queryContext.Criteries.Add("IsSubItem", "0");
            using (var dt = Core.Form.DBCommon.GetList(queryContext))
            {
                return AppUtility.ObjectToJson(new { total = queryContext.TotalRowAmount, rows = dt });
            }
            //using (var dataAccessor = new Data.FormAccessor())
            //{
            //    return Json(new { total = dataAccessor.FormTemplates.Count(m => !m.IsDelete && m.IsCurrent), page = pageIndex + 1, rows = dataAccessor.Database.SqlQuery<Model.FormTemplateBase>("SELECT * FROM (SELECT ROW_Number() OVER(ORDER BY CreateTime) as ROW_NUMBER,ID,Name,Version,CreateTime,Width  FROM [FormTemplate] WHERE IsDelete=0 AND IsCurrent=1) AS tb WHERE tb.ROW_NUMBER BETWEEN " + pageIndex * pageSize + " AND " + (pageIndex + 1) * pageSize).ToList() });
            //}
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public JsonResult Save()
        {
            var formTemplateID = AppUtility.ToInt(Request.QueryString["formTemplateID"]);
            var id = AppUtility.ToInt(Request.QueryString["id"]);
            if (formTemplateID == 0 && id == 0)
            {
                return Json(new { Result = false, Message = "参数错误" });
            }
            try
            {
                using (var formProcessor = new Core.Form.Processor())
                {
                    formProcessor.Creator = BPMContext.CurrentUser.ID;
                    formProcessor.CreatorName = BPMContext.CurrentUser.Name;
                    formProcessor.DepartmentID = BPMContext.CurrentUser.DepartmentID;
                    formProcessor.DepartmentName = BPMContext.CurrentUser.DepartmentName;
                    formProcessor.FormID = id;
                    formProcessor.FormTemplateID = formTemplateID;
                    formProcessor.SaveData(Request.Form["data"]);
                }
                //JQ.BPM.Core.Form.DBCommon.Save(id, formTemplateID, Request.Form["data"], BPMContext.CurrentUser.ID, BPMContext.CurrentUser.Name, BPMContext.CurrentUser.DepartmentID, 0);
                return Json(new { Result = true });
            }
            catch (Model.RouteException rex)
            {
                return Json(new
                {
                    Result = false,
                    Message = rex.Message
                });
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Message = ex.Message });
            }
        }

        [ValidateInput(false)]
        public string GetList()
        {
            QueryContext queryContext = new QueryContext()
            {
                PageIndex = AppUtility.ToInt(Request.Form["page"]) - 1,
                PageSize = AppUtility.ToInt(Request.Form["rows"])
            };
            using (var dt = Core.Form.DBCommon.GetList(queryContext, Request.Form["queryData"] ?? ""))
            {
                var list = new List<Dictionary<string, object>>();
                foreach (DataRow dr in dt.Rows)
                {
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

        [ValidateInput(false)]
        public JsonResult Delete()
        {
            var data = (Request.Form["data"] ?? "").Trim(',');
            if (string.IsNullOrEmpty(data))
            {
                return Json(new { Result = false, Message = "参数错误" });
            }
            try
            {
                Core.Form.DBCommon.Delete(data);
                return Json(new { Result = true });
            }
            catch (Model.RouteException re)
            {
                return Json(new { Result = true, Message = re.Message });
            }
            catch
            {
                return Json(new { Result = false, Message = "删除时发生错误" });
            }
        }
    }
}