using JQ.BPM.Core.Form;
using Microsoft.Practices.Unity;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Xml;
using AppUtility = JQ.BPM.Configuration.Utility;
using JQ.BPM.Data.Extern;

namespace JQ.BPM.Web.Areas.Form.Controllers
{
    public class WidgetController : Controller
    {
        public ViewResult ImportLayoutWord()
        {
            return View();
        }

        public ViewResult Layout()
        {
            return View();
        }

        public ViewResult Label()
        {
            return View();
        }

        public ViewResult TextBox()
        {
            return View();
        }

        public ViewResult TextArea()
        {
            return View();
        }

        public ViewResult Radio()
        {
            return View();
        }

        public ViewResult CheckBox()
        {
            return View();
        }

        public ViewResult Select()
        {
            return View();
        }

        public ViewResult DateBox()
        {
            return View();
        }

        public ViewResult NumberBox()
        {
            return View();
        }

        public ViewResult Panel()
        {
            return View();
        }

        public ViewResult Map()
        {
            return View();
        }

        public ViewResult SignBox()
        {
            return View();
        }

        public ViewResult Hidden()
        {
            return View();
        }

        public ViewResult Table()
        {
            return View();
        }

        public ViewResult Setting()
        {
            //获取出所有的数据库
            //try
            //{
            //    ViewBag.Tables = DBCommon.GetAllTables();
            //}
            //catch
            //{
            //    ViewBag.Tables = new List<string>();
            //}
            //获取出所有的流程模版
            using (var dataAccessor = new Data.ProcessAccessor())
            {
                ViewBag.Processes = dataAccessor.Database.SqlQuery<Model.SelectItem>("SELECT Name AS Text,CAST(ID AS NVARCHAR) AS Value FROM ProcessTemplate WHERE IsDelete=0 AND IsDraft=1 AND [Version]>0 ORDER BY [CreateTime] DESC").ToList();
            }
            return View();
        }

        public JsonResult GetTables()
        {
            var storageMode = Request.Form["storageMode"];
            switch (storageMode)
            {
                case "1":
                    return Json(DBCommon.GetCustomTables());
                case "2":
                    return Json(DBCommon.GetDataBaseTables());
                default:
                    return Json(new object[] { });
            }
        }

        public JsonResult GetColumns()
        {
            var tableName = Request.Form["tableName"];
            var storageMode = Request.Form["storageMode"];
            if (string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(Request.Form["storageMode"]))
            {
                return Json(new object[] { });
            }
            var result = DBCommon.GetColumns(storageMode, tableName);
            if (result == null)
            {
                return Json(new object[] { });
            }
            return Json(result);
        }

        /// <summary>
        /// 通过模版获取出所有的列
        /// </summary>
        /// <returns></returns>
        public JsonResult GetColumnsByFormTemplateID()
        {
            var formTemplateID = AppUtility.ToInt(Request.Form["formTemplateID"]);
            if (formTemplateID == 0)
            {
                return Json(new object[] { });
            }
            using (var dataAccessor = new Data.FormAccessor())
            {
                var obj = dataAccessor.Database.ExecuteScalar("SELECT [Content] FROM [FormTemplate] WHERE ID=" + formTemplateID);
                if (obj == null || obj == DBNull.Value)
                {
                    return Json(new object[] { });
                }
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(obj.ToString());
                var tableName = AppUtility.GetXmlNodeText(xmlDocument.DocumentElement, "Options/Option[@name=\"table\"]");
                var storageMode = AppUtility.GetXmlNodeText(xmlDocument.DocumentElement, "Options/Option[@name=\"storageMode\"]");
                if (string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(storageMode))
                {
                    return Json(new object[] { });
                }
                var result = DBCommon.GetColumns(storageMode, tableName);
                if (result == null)
                {
                    return Json(new object[] { });
                }
                return Json(result);
            }
        }

        public ViewResult List()
        {
            var formTemplateID = AppUtility.ToInt(Request.QueryString["formTemplateID"]);
            var xml = "";
            if (formTemplateID > 0)
            {
                using (var dataAccessor = new Data.FormAccessor())
                {
                    var formTemplate = dataAccessor.FormTemplates.FirstOrDefault(m => m.ID == formTemplateID);
                    if (formTemplate != null)
                    {
                        XmlDocument xmlDocument = new XmlDocument();
                        xmlDocument.LoadXml(formTemplate.Content);
                        var xColumns = xmlDocument.SelectSingleNode("Root/Columns");
                        if (xColumns != null)
                        {
                            xml = xColumns.OuterXml;
                        }
                        var xKeywords = xmlDocument.SelectSingleNode("Root/Keywords");
                        if (xKeywords != null)
                        {
                            xml += xKeywords.OuterXml;
                        }
                        var xConditions = xmlDocument.SelectSingleNode("Root/Conditions");
                        if (xConditions != null)
                        {
                            xml += xConditions.OuterXml;
                        }
                    }
                }
            }
            ViewBag.Xml = "<Root>" + xml + "<Root>";
            return View();
        }

        public JsonResult UploadWordTemplate()
        {
            //解析文件
            if (Request.Files.Count == 0)
            {
                return Json(new { Result = false, Message = "找不到文件" });
            }
            var extension = System.IO.Path.GetExtension(Request.Files[0].FileName).ToLower();
            if (extension == ".docx")
            {
                //解析文件
                try
                {
                    using (var stream = Request.Files[0].InputStream)
                    {
                        var tableReader = new TableReader();
                        tableReader.FileStream = stream;
                        tableReader.Read();
                        return Json(new { Result = true, TableXml = tableReader.Result });
                    }
                }
                catch (Exception ex)
                {
                    AppUtility.WriteLog(ex);
                    return Json(new { Result = false });
                }
            }
            else
            {
                return Json(new { Result = false, Message = "需要为word文件（2007版本以上的word）" });
            }
        }

        /// <summary>
        /// 保存表单模版的列表设置
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public JsonResult SaveGridSetting()
        {
            var formTemplateID = AppUtility.ToInt(Request.QueryString["formTemplateID"]);
            if (formTemplateID == 0)
            {
                return Json(new { Result = false, Message = "参数错误" });
            }
            XmlDocument xmlDocument = new XmlDocument();
            try
            {
                xmlDocument.LoadXml(Request.Form["data"]);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Message = ex.Message });
            }
            using (var dataAccessor = new Data.FormAccessor())
            {
                var formTemplate = dataAccessor.FormTemplates.FirstOrDefault(m => m.ID == formTemplateID);
                if (formTemplate != null)
                {
                    XmlDocument cDocument = new XmlDocument();
                    cDocument.LoadXml(formTemplate.Content);
                    var xColumns = cDocument.SelectSingleNode("Root/Columns");
                    if (xColumns == null)
                    {
                        xColumns = cDocument.CreateElement("Columns");
                        cDocument.DocumentElement.AppendChild(xColumns);
                    }
                    xColumns.InnerXml = xmlDocument.SelectSingleNode("Root/Columns").InnerXml;
                    var xKeywords = (XmlElement)cDocument.SelectSingleNode("Root/Keywords");
                    if (xKeywords == null)
                    {
                        xKeywords = cDocument.CreateElement("Keywords");
                        cDocument.DocumentElement.AppendChild(xKeywords);
                    }
                    var xKeywords_ = (XmlElement)xmlDocument.SelectSingleNode("Root/Keywords");
                    if (xKeywords_ != null)
                    {
                        xKeywords.SetAttribute("isShow", xKeywords_.GetAttribute("isShow"));
                        xKeywords.SetAttribute("placeHolder", xKeywords_.GetAttribute("placeHolder"));
                        xKeywords.InnerXml = xmlDocument.SelectSingleNode("Root/Keywords").InnerXml;
                    }
                    var xConditions = (XmlElement)cDocument.SelectSingleNode("Root/Conditions");
                    if (xConditions == null)
                    {
                        xConditions = cDocument.CreateElement("Conditions");
                        cDocument.DocumentElement.AppendChild(xConditions);
                    }
                    xConditions.InnerXml = xmlDocument.SelectSingleNode("Root/Conditions").InnerXml;
                    formTemplate.Content = cDocument.OuterXml;
                    dataAccessor.SaveChanges();
                }
            }
            return Json(new { Result = true });
        }

        /// <summary>
        /// 获取出基础数据
        /// </summary>
        /// <returns></returns>
        public string GetBaseDataCategory()
        {
            using (var formAccessor = new Data.FormAccessor())
            {
                using (var dt = BPM.Configuration.BPMContext.Resolve<IBaseData>(new ParameterOverride("dbContext", formAccessor)).GetCategory(Request.Form["name"]))
                {
                    return AppUtility.ObjectToJson(dt);
                }
            }
        }

        public string GetBaseDataItems()
        {
            var id = Request.Form["id"];
            if (string.IsNullOrEmpty(id))
            {
                return "[]";
            }
            using (var formAccessor = new Data.FormAccessor())
            {
                using (var dt = BPM.Configuration.BPMContext.Resolve<IBaseData>(new ParameterOverride("dbContext", formAccessor)).GetItems(id))
                {
                    return AppUtility.ObjectToJson(dt);
                }
            }
        }
    }
}