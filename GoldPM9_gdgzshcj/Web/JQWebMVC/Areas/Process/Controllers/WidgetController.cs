using JQ.BPM.Core;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Xml;
using AppUtility = JQ.BPM.Configuration.Utility;

namespace JQ.BPM.Web.Areas.Process.Controllers
{
    public class WidgetController : JQ.Web.CoreController
    {
        public ViewResult Node()
        {
            return View();
        }

        public ViewResult Activity()
        {
            return View();
        }

        public ViewResult Property()
        {
            return View();
        }

        public ViewResult Transition()
        {
            return View();
        }

        public ViewResult Process()
        {
            return View();
        }

        public ViewResult Progress()
        {
            return View();
        }

        public ViewResult ChooseRole()
        {
            return View();
        }

        [ValidateInput(false)]
        public JsonResult GetSubmitData()
        {
            //获取出变量
            Core.Behavior.SubmitOptionBase submitOption = new Core.Behavior.SubmitOptionBase();
            submitOption.Action = Request.Form["action"];
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(Request.Form["variables"]);
            var variables = xmlDocument.SelectNodes("Root/Item");
            if (variables.Count > 0)
            {
                submitOption.Variables = new Dictionary<string, object>();
                foreach (XmlElement xmlElement in variables)
                {
                    if (string.IsNullOrEmpty(xmlElement.GetAttribute("name")))
                    {
                        continue;
                    }
                    submitOption.Variables.Add(xmlElement.GetAttribute("name"), xmlElement.InnerText);
                }
            }
            if (Request.Form["isTemplate"] == "1")
            {
                submitOption.ProcessTemplateID = AppUtility.ToInt(Request.Form["id"]);
            }
            else
            {
                submitOption.ProcessID = AppUtility.ToInt(Request.Form["id"]);
            }
            submitOption.ActivityID = AppUtility.ToInt(Request.Form["activityID"]);
            submitOption.Operator = Configuration.BPMContext.CurrentUser.ID.ToString();
            submitOption.OperatorName = Configuration.BPMContext.CurrentUser.Name;
            submitOption.DepartmentID = Configuration.BPMContext.CurrentUser.DepartmentID.ToString();
            submitOption.DepartmentName = Configuration.BPMContext.CurrentUser.DepartmentName;
            return Json(Configuration.BPMContext.Resolve<IProcessEngine>().GetSubmitData(submitOption));
        }

        [ValidateInput(false)]
        public JsonResult Submit()
        {
            Core.Behavior.SubmitOption submitOption = new Core.Behavior.SubmitOption();
            submitOption.Action = Request.Form["action"];
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(Request.Form["variables"]);
            var variables = xmlDocument.SelectNodes("Root/Item");
            if (variables.Count > 0)
            {
                submitOption.Variables = new Dictionary<string, object>();
                foreach (XmlElement xmlElement in variables)
                {
                    if (string.IsNullOrEmpty(xmlElement.GetAttribute("name")))
                    {
                        continue;
                    }
                    submitOption.Variables.Add(xmlElement.GetAttribute("name"), xmlElement.InnerText);
                }
            }
            xmlDocument.LoadXml(Request.Form["nextSteps"]);
            var xSteps = xmlDocument.SelectNodes("Root/Step");
            if (xSteps.Count == 0)
            {
                return Json(new { Result = false, Message = "无法获取出下一步的信息！" });
            }
            submitOption.NextSteps = new List<Core.Behavior.RouteStepBase>();
            foreach (XmlElement xmlElement in xSteps)
            {
                var step = new Core.Behavior.RouteStepBase();
                step.ID = AppUtility.ToInt(xmlElement.GetAttribute("id"));
                step.Name = xmlElement.GetAttribute("name");
                step.Users = new List<Core.Behavior.RouteUser>();
                foreach (XmlElement xUser in xmlElement.SelectNodes("Users/User"))
                {
                    step.Users.Add(new Core.Behavior.RouteUser()
                    {
                        ID = xUser.InnerText,
                        Name = xUser.GetAttribute("name")
                    });
                }
                submitOption.NextSteps.Add(step);
            }
            if (Request.Form["isTemplate"] == "1")
            {
                submitOption.ProcessTemplateID = AppUtility.ToInt(Request.Form["id"]);
                submitOption.CurrentStepName = Request.Form["currentStepName"];
            }
            else
            {
                submitOption.ProcessID = AppUtility.ToInt(Request.Form["id"]);
            }
            submitOption.ActivityID = AppUtility.ToInt(Request.Form["activityID"]);
            submitOption.Operator = Configuration.BPMContext.CurrentUser.ID.ToString();
            submitOption.OperatorName = Configuration.BPMContext.CurrentUser.Name;
            submitOption.DepartmentID = Configuration.BPMContext.CurrentUser.DepartmentID.ToString();
            submitOption.DepartmentName = Configuration.BPMContext.CurrentUser.DepartmentName;
            submitOption.Note = Request.Form["note"] ?? "";
            try
            {
                if (Request.Form["formData"] != null)
                {
                    submitOption.FormTemplateID = AppUtility.ToInt(Request.Form["formTemplateID"]);
                    submitOption.OnSubmited = delegate (Core.Behavior.ExecutionContext context)
                    {
                        using (var formProcessor = new Core.Form.Processor(context.DbContext))
                        {
                            if (submitOption.ProcessTemplateID > 0)
                            {
                                formProcessor.IsNewProcess = true;
                            }
                            formProcessor.Creator = Configuration.BPMContext.CurrentUser.ID;
                            formProcessor.CreatorName = Configuration.BPMContext.CurrentUser.Name;
                            formProcessor.DepartmentID = Configuration.BPMContext.CurrentUser.DepartmentID;
                            formProcessor.DepartmentName = Configuration.BPMContext.CurrentUser.DepartmentName;
                            formProcessor.FormID = AppUtility.ToInt(Request.Form["formID"]);
                            formProcessor.FormTemplateID = AppUtility.ToInt(Request.Form["formTemplateID"]);
                            formProcessor.ProcessID = context.Process.ID;
                            formProcessor.SaveData(Request.Form["formData"]);
                        }
                    };
                }
                Configuration.BPMContext.Resolve<IProcessEngine>().Submit(submitOption);
                return Json(new { Result = true });
            }
            catch (Model.RouteException rex)
            {
                return Json(new { Result = false, Message = rex.Message });
            }
            catch (Exception ex)
            {
                AppUtility.WriteLog(ex);
                return Json(new { Result = false, Message = ex.Message });
            }
        }


        public string GetToDoList()
        {
            Data.QueryContext queryContext = new Data.QueryContext()
            {
                PageIndex = AppUtility.ToInt(Request.Form["page"]) - 1,
                PageSize = AppUtility.ToInt(Request.Form["rows"])
            };
            queryContext.Criteries.Add("actor", Configuration.BPMContext.CurrentUser.ID);
            if (!string.IsNullOrEmpty((Request.Form["keywords"] ?? "").Trim()))
            {
                queryContext.Criteries.Add("keywords", (Request.Form["keywords"] ?? "").Trim());
            }
            if (!string.IsNullOrEmpty(Request.Form["status"]))
            {
                queryContext.Criteries.Add("status", Request.Form["status"]);
            }
            using (var dt = Configuration.BPMContext.Resolve<IProcessEngine>().GetToDoList(queryContext))
            {
                return AppUtility.ObjectToJson(new { total = queryContext.TotalRowAmount, rows = dt });
            }
        }
    }
}