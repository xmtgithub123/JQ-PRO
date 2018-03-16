using JQ.BPM.Configuration;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Xml;
using AppUtility = JQ.BPM.Configuration.Utility;

namespace JQ.BPM.Web.Areas.Form.Controllers
{
    public class TemplateController : Controller
    {
        public ViewResult List()
        {
            return View();
        }

        public ViewResult Edit()
        {
            var id = AppUtility.ToInt(Request.QueryString["id"]);
            if (id > 0)
            {
                using (var formAccessor = new JQ.BPM.Data.FormAccessor())
                {
                    var formTemplate = formAccessor.FormTemplates.FirstOrDefault(m => m.ID == id);
                    if (formTemplate != null)
                    {
                        ViewBag.Name = formTemplate.Name;
                        ViewBag.Remark = formTemplate.Remark;
                    }
                }
            }
            return View();
        }

        public ViewResult Build()
        {
            return View();
        }

        public ViewResult Preview()
        {
            ViewBag.CurrentDate = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.CurrentUserName = BPMContext.CurrentUser.Name;
            ViewBag.CurrentDepartmentName = BPMContext.CurrentUser.DepartmentName;
            return View();
        }

        public ViewResult Publish()
        {
            var formTemplateID = AppUtility.ToInt(Request.QueryString["formTemplateID"]);
            if (formTemplateID > 0)
            {
                using (var dataAccessor = new Data.FormAccessor())
                {
                    var data = dataAccessor.FormTemplates.FirstOrDefault(m => m.ID == formTemplateID);
                    if (data != null)
                    {
                        ViewBag.formName = data.Name;
                        ViewBag.currentVersion = data.Version + 1;
                    }
                }
            }
            ViewBag.currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }

        public ViewResult HistoryList()
        {
            return View();
        }

        public string GetList()
        {
            var queryContext = new Data.QueryContext();
            queryContext.PageIndex = AppUtility.ToInt(Request.Form["page"]) - 1;
            queryContext.PageSize = AppUtility.ToInt(Request.Form["rows"]);
            queryContext.Criteries.Add("IsDraft", "1");
            //queryContext.Criteries.Add("IsSubItem", "0");
            using (var dt = Core.Form.DBCommon.GetList(queryContext))
            {
                return AppUtility.ObjectToJson(new { total = queryContext.TotalRowAmount, rows = dt });
            }
        }

        public JsonResult Save()
        {
            var id = AppUtility.ToInt(Request.Form["id"]);
            using (var formAccessor = new JQ.BPM.Data.FormAccessor())
            {
                //验证该表单名称是否已经存在
                if (id == 0)
                {
                    //新增
                    formAccessor.FormTemplates.Add(new Model.FormTemplate()
                    {
                        Name = Request.Form["txtName"] ?? "",
                        Content = "<Root />",
                        CreateTime = DateTime.Now,
                        GroupID = Guid.NewGuid(),
                        IsCurrent = false,
                        IsDraft = true,
                        IsDelete = false,
                        Remark = Request.Form["txtRemark"] ?? "",
                        Version = 0,
                        Html = ""
                    });
                    formAccessor.SaveChanges();
                }
                else
                {
                    var formTemplate = formAccessor.FormTemplates.FirstOrDefault(m => m.ID == id);
                    if (formTemplate == null)
                    {
                        return Json(new { Result = false, Message = "参数错误" });
                    }
                    var formTemplates = formAccessor.FormTemplates.Where(m => m.GroupID == formTemplate.GroupID);
                    foreach (var data in formTemplates)
                    {
                        data.Name = Request.Form["txtName"] ?? "";
                        data.Remark = Request.Form["txtRemark"] ?? "";
                    }
                    formAccessor.SaveChanges();
                }
            }
            return Json(new { Result = true });
        }

        [ValidateInput(false)]
        public JsonResult SaveContent()
        {
            try
            {
                var formTemplateID = AppUtility.ToInt(Request.Form["formTemplateID"]);
                if (formTemplateID == 0)
                {
                    return Json(new { Result = false, Message = "参数错误" });
                }
                using (var dataAccessor = new Data.FormAccessor())
                {
                    InternalSaveContent(dataAccessor.FormTemplates.FirstOrDefault(m => m.ID == formTemplateID));
                    dataAccessor.SaveChanges();
                }
                return Json(new { Result = true });
            }
            catch (Model.RouteException rex)
            {
                return Json(new { Result = false, Message = rex.Message });
            }
            catch (Exception ex)
            {
                AppUtility.WriteLog(ex);
                return Json(new { Result = false, Message = "发生错误" });
            }
        }

        /// <summary>
        /// 保存Content
        /// </summary>
        private void InternalSaveContent(Model.FormTemplate data)
        {
            if (data == null)
            {
                throw new JQ.BPM.Model.RouteException("无法找到对应的表单模版！");
            }
            if (!data.IsDraft)
            {
                throw new JQ.BPM.Model.RouteException("已经发布的版本不可再编辑内容！");
            }
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(data.Content);
            xmlDocument.DocumentElement.SetAttribute("width", Request.Form["width"] ?? "0");
            //var opt
            var xOptions = (XmlElement)xmlDocument.SelectSingleNode("Root/Options");
            if (xOptions == null)
            {
                xOptions = xmlDocument.CreateElement("Options");
                xmlDocument.DocumentElement.AppendChild(xOptions);
            }
            xOptions.InnerXml = Request.Form["options"] ?? "";
            xmlDocument.DocumentElement.SetAttribute("primaryTable", xOptions.SelectSingleNode("Option[@name='table']").InnerText);
            xmlDocument.DocumentElement.SetAttribute("processTemplateID", xOptions.SelectSingleNode("Option[@name='processID']").InnerText);
            xmlDocument.DocumentElement.SetAttribute("processTemplateVersionID", xOptions.SelectSingleNode("Option[@name='processVersionID']").InnerText);
            var xDatas = xmlDocument.SelectSingleNode("Root/Properties");
            if (xDatas == null)
            {
                xDatas = xmlDocument.CreateElement("Properties");
                xmlDocument.DocumentElement.AppendChild(xDatas);
            }
            xDatas.InnerXml = Request.Form["properties"] ?? "";
            xDatas = xmlDocument.SelectSingleNode("Root/Hiddens");
            if (xDatas == null)
            {
                xDatas = xmlDocument.CreateElement("Hiddens");
                xmlDocument.DocumentElement.AppendChild(xDatas);
            }
            xDatas.InnerXml = Request.Form["hiddens"] ?? "";
            data.Content = xmlDocument.OuterXml;
            data.Width = AppUtility.ToInt(Request.Form["width"]);
            data.Html = Request.Form["html"] ?? "";
        }


        public JsonResult Get()
        {
            var formTemplateID = AppUtility.ToInt(Request.Form["formTemplateID"]);
            if (formTemplateID == 0)
            {
                return Json(new { Result = false, Message = "参数错误" });
            }
            using (var dataAccessor = new Data.FormAccessor())
            {
                var data = dataAccessor.FormTemplates.FirstOrDefault(m => m.ID == formTemplateID);
                if (data == null)
                {
                    return Json(new { Result = false, Message = "无法找到对应的表单模版" });
                }
                return Json(new { Result = true, Name = data.Name, Html = data.Html, Content = data.Content });
            }
        }

        public JsonResult Delete()
        {
            var ids = (Request.Form["ids"] ?? "").Trim(',');
            if (string.IsNullOrEmpty(ids))
            {
                return Json(new { Result = false, Message = "参数错误" });
            }
            using (var formAccessor = new JQ.BPM.Data.FormAccessor())
            {
                formAccessor.Database.ExecuteSqlCommand("UPDATE FormTemplate SET IsDelete=1 WHERE GroupID IN (SELECT GroupID FROM FormTemplate WHERE ID IN (" + ids + "))");
            }
            return Json(new { Result = true });
        }

        [ValidateInput(false)]
        public JsonResult SaveContentWithPublish()
        {
            try
            {
                var formTemplateID = AppUtility.ToInt(Request.Form["formTemplateID"]);
                if (formTemplateID == 0)
                {
                    return Json(new { Result = false, Message = "参数错误" });
                }
                using (var dataAccessor = new Data.FormAccessor())
                {
                    var data = dataAccessor.FormTemplates.FirstOrDefault(m => m.ID == formTemplateID);
                    InternalSaveContent(data);
                    //创建新的版本出来
                    var formTemplate = new Model.FormTemplate()
                    {
                        Content = data.Content,
                        CreateTime = DateTime.Now,
                        GroupID = data.GroupID,
                        Html = data.Html,
                        Version = data.Version + 1,
                        IsDelete = false,
                        IsDraft = false,
                        IsCurrent = Request.Form["isCurrentVersion"] == "1",
                        Name = data.Name,
                        Remark = Request.Form["remark"],
                        Width = data.Width,
                        ParentID = data.ID
                    };
                    if (formTemplate.IsCurrent)
                    {
                        var currentVersion = dataAccessor.FormTemplates.FirstOrDefault(m => m.GroupID == formTemplate.GroupID && m.IsCurrent);
                        if (currentVersion != null)
                        {
                            currentVersion.IsCurrent = false;
                        }
                    }
                    dataAccessor.FormTemplates.Add(formTemplate);
                    data.Version = formTemplate.Version;
                    dataAccessor.SaveChanges();
                }
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
            catch
            {
                return Json(new { Result = false, Message = "发生错误" });
            }
        }

        /// <summary>
        /// 获取出子项的模版列表
        /// </summary>
        /// <returns></returns>
        public string GetSubItemList()
        {
            var queryContext = new Data.QueryContext();
            //queryContext.PageIndex = AppUtility.ToInt(Request.Form["page"]) - 1;
            //queryContext.PageSize = AppUtility.ToInt(Request.Form["rows"]);
            queryContext.IsPaging = false;
            queryContext.Criteries.Add("IsDraft", "1");
            queryContext.Criteries.Add("IsSubItem", "1");
            return AppUtility.ObjectToJson(Core.Form.DBCommon.GetList(queryContext));
        }
    }
}