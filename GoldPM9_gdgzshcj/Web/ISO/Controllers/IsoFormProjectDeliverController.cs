using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System;
using Common.Data.Extenstions;
using System.Web.Script.Serialization;

namespace ISO.Controllers
{
    [Description("IsoForm")]
    public class IsoFormProjectDeliverController : CoreController
    {
        private DBSql.Iso.IsoForm op = new DBSql.Iso.IsoForm();
        private DBSql.Project.Project proj = new DBSql.Project.Project();
        private DBSql.Design.DesTask DesTaskDB = new DBSql.Design.DesTask();
        private DBSql.Design.DesTask opTask = new DBSql.Design.DesTask();



        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjectDeliver")));
            ViewBag.EmpId = userInfo.EmpID;
            return View();
        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string json()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "FormID desc";
            }

            List<string> permission = PermissionList("ProjectDeliver");
            var TWhere = QueryBuild<DataModel.Models.IsoForm>.True();
            string KeyJQSearch = Request.Form["KeyJQSearch"];
            string ProjectPhase = Request.Form["ProjectPhase[]"];
            if (!string.IsNullOrEmpty(KeyJQSearch))
            {
                TWhere = TWhere.And(p => p.FormReason.Contains(KeyJQSearch) || p.FK_IsoForm_ProjID.ProjName.Contains(KeyJQSearch) || p.FK_IsoForm_ProjID.ProjNumber.Contains(KeyJQSearch));
            }
            if (!string.IsNullOrEmpty(ProjectPhase))
            {
                TWhere = TWhere.And(p => p.FK_IsoForm_ProjID.ProjPhaseIds.Contains(ProjectPhase));
            }
            TWhere = TWhere.And((p => p.RefTable == "IsoFormProjectDeliver"));
            if (!(permission.Contains("allview") || permission.Contains("alledit")))
            {
                if (permission.Contains("dep"))
                {
                    TWhere = TWhere.And(p => p.CreatorDepId == this.userInfo.EmpDepID);
                }
                else
                {
                    TWhere = TWhere.And(p => p.CreatorEmpId == this.userInfo.EmpID);
                }
            }
            var list = op.GetPagedList(TWhere, PageModel).ToList();
            var targetList = from p in list
                             select new
                             {
                                 p.FormID,
                                 p.CreationTime,//登记日期
                                 p.CreatorEmpName,//登记人
                                 p.FormReason,  //说明
                                 p.FormNote,  //备注
                                 ProjName = p.FK_IsoForm_ProjID == null ? "" : p.FK_IsoForm_ProjID.ProjName,  //项目名称
                                 ProjNumber = p.FK_IsoForm_ProjID == null ? "" : p.FK_IsoForm_ProjID.ProjNumber,//项目编号
                                 ProjPhaseIds = p.FK_IsoForm_ProjID == null ? "" : String.Join(",", GetBaseName(p.FK_IsoForm_ProjID.ProjPhaseIds)),//阶段
                                 ProjEmpName = p.FK_IsoForm_ProjID == null ? "" : p.FK_IsoForm_ProjID.ProjEmpName,  //负责人姓名
                                 p.ColAttVal1,//交付方式
                                 p.ColAttVal2,//交付单号
                                 p.ColAttVal3,//发放人
                                 p.ColAttDate1,//发放日期
                                 p.ColAttVal4,//接收单位签收人
                                 p.ColAttVal5,//签收单验收人
                                 p.CreatorEmpId


                             };

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = targetList
            });
        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new IsoForm();
            model.CreationTime = System.DateTime.Now;
            model.CreatorEmpName = userInfo.EmpName;
            ViewData["ProjIDs"] = "";
            List<string> permission = PermissionList("ProjectDeliver");
            if (permission.Contains("export"))
            {
                ViewBag.buttonPermission = "['submit', 'close', 'exportForm']";
            }
            else
            {
                ViewBag.buttonPermission = "['submit', 'close']";
            }
            ViewBag.detailPermission = 1;
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            BindProjName(model.ProjID);
            ViewData["ProjIDs"] = string.Join(",", new DBSql.Iso.IsoFormNode().GetList(p => p.FormID == id).Select(p => p.ColAttType1.ToString()));
            List<string> permission = PermissionList("ProjectDeliver");
            if (permission.Contains("export") && (permission.Contains("edit") || permission.Contains("alledit")))
            {
                ViewBag.buttonPermission = "['submit', 'close', 'exportForm']";//导出权、编辑（维护）
            }
            else
            {
                if (permission.Contains("export") && !permission.Contains("edit") && !permission.Contains("alledit"))
                {
                    ViewBag.buttonPermission = "['close', 'exportForm']";//导出权
                }
                else
                {
                    if (!permission.Contains("export") && (permission.Contains("edit") || permission.Contains("alledit")))
                    {
                        ViewBag.buttonPermission = "['submit', 'close']";//编辑权
                    }
                    else
                    {
                        ViewBag.buttonPermission = "['close']";//查看权
                    }
                }

            }

            if (permission.Contains("edit") || permission.Contains("alledit"))
            {
                ViewBag.Upload = "";
                ViewBag.detailPermission = 1;
            }
            else
            {
                ViewBag.Upload = "$('#uploadfile1_upload').hide();$('#uploadfile1_delete').hide();";
                ViewBag.detailPermission = 0;
            }
            return View("info", model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 1;
            //op.Delete(s => id.Contains(s.FormID));
            op.DeleteProjectDeliver(id);
            op.UnitOfWork.SaveChanges();
            //new DBSql.PubFlow.Flow().Delete(id, "IsoFormProjectDeliver");
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int FormID, string HideprojIDs = "")
        {
            var model = new IsoForm();
            if (FormID > 0)
            {
                model = op.Get(FormID);
            }
            model.MvcSetValue();

            //项目信息
            List<DataModel.Models.IsoFormNode> FormNodeList = new List<IsoFormNode>();
            if (Request.Params["ProjTable_Data"] != null)
            {
                string var = Request.Params["ProjTable_Data"].ToString().Replace("Id", "ColAttType1");
                FormNodeList = new JavaScriptSerializer().Deserialize<List<IsoFormNode>>(var);
            }

            int reuslt = 0;
            if (FormID > 0)
            {
                op.MvcDefaultEdit(userInfo.EmpID);
                //op.Edit(model);
                op.Edit(model, FormNodeList);
            }
            else
            {
                model.FormDate = System.DateTime.Now;
                model.RefTable = "IsoFormProjectDeliver";
                model.FormName = "项目交付登记";
                model.MvcDefaultSave(userInfo.EmpID);
                //op.Add(model);
                op.AddProjectDeliver(model, FormNodeList);
            }
            op.UnitOfWork.SaveChanges();

            if (FormNodeList.Count() > 0)//  卷册交付 时间
            {
                foreach (var formNode in FormNodeList)
                {
                    new DBSql.Design.DesTask().SetDateForPublish(formNode.ColAttType1, DateTime.Now);
                    //formNode.ColAttType1  == DesTaskID  
                }
            }

            using (var ba = new DBSql.Sys.BaseAttach())
            {
                ba.MoveFile(model.FormID, userInfo.EmpID, userInfo.EmpName);
            }
            reuslt = model.FormID;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.FormID, "操作成功") : DoResultHelper.doSuccess(model.FormID, "操作失败");
            return Json(dr);
        }
        #endregion


        private void BindProjName(int ProjID)
        {
            DataModel.Models.Project project = proj.Get(ProjID);
            if (project != null)
            {
                ViewBag.ProjNumber = project.ProjNumber;
                ViewBag.PhaseID = proj.GetProjPhase(project.ProjPhaseIds);//project.FK_Project_ProjTypeID == null ? "" :  
                ViewBag.ProjID = ProjID;
                ViewBag.ProjEmpName = project.ProjEmpName;
                ViewBag.CustName = project.CustName;
                ViewBag.ProjName = project.ProjName;
            }
            else
            {
                ViewBag.ProjNumber = "";
                ViewBag.PhaseID = "";
                ViewBag.ProjID = "";
                ViewBag.ProjEmpName = "";
                ViewBag.CustName = "";
                ViewBag.ProjName = "";

            }
        }

        /// <summary>
        /// 选择项目任务
        /// </summary>
        /// <returns></returns>
        public ActionResult selectProj()
        {
            return View();
        }


        #region 选择任务列表
        [Description("选择任务列表")]
        public ActionResult FilterList()
        {
            ViewBag.ProjId = Request.Params["ProjId"].ToString();
            return View();
        }
        #endregion


        #region 新增交付登记列表

        public string GetProjectDeliverList(FormCollection Tcondition)
        {
            var IsoFormNodeOp = new DBSql.Iso.IsoFormNode();
            string projIDs = "";
            //int ColAttType1 = 0;
            //if (Tcondition["ColAttType1"] != null)
            //{
            //    ColAttType1 = Common.ExtensionMethods.Value<int>(Tcondition["ColAttType1"]);
            //}
            int FormID = 0;
            if (Tcondition["FormID"] != null)
            {
                FormID = Common.ExtensionMethods.Value<int>(Tcondition["FormID"]);
            }

            if (Tcondition["projIDs"] != null)
            {
                projIDs = Tcondition["projIDs"].ToString();
            }
            else if (Tcondition["projIDs[]"] != null)
            {
                projIDs = Tcondition["projIDs[]"].ToString();
            }


            int[] Id = (from n in projIDs.Split(',') where n != "" select Common.ExtensionMethods.Value<int>(n)).ToArray();
            var TWhere = QueryBuild<DataModel.Models.DesTask>.True();
            if (Id.Length == 0)
            {
                TWhere = TWhere.And(p => p.Id == -1);
            }
            else
            {
                var Ids = string.Join(",", Id.Select(p => p.ToString()));
                TWhere = TWhere.And(p => Id.Contains((int)p.Id));
            }
            TWhere = TWhere.And(p => p.DeleterEmpId == 0);

            var list = opTask.GetList(TWhere).Select(p => new
            {
                Id = p.Id,
                p.TaskName, //任务名称
                p.TaskEmpName,//任务负责人
                p.DatePlanFinish,//计划结束时间
                ProjName = p.ProjId == 0 ? "" : new DBSql.Project.Project().Get(p.ProjId).ProjName,// 
                ProjNumber = p.ProjId == 0 ? "" : new DBSql.Project.Project().Get(p.ProjId).ProjNumber,// 
                PhaseName = p.TaskPhaseId == 0 ? "" : String.Join(",", GetBaseName(p.TaskPhaseId.ToString())),//阶段
                SpecialtyName = p.TaskSpecId == 0 ? "" : String.Join(",", GetBaseName(p.TaskSpecId.ToString())),//专业
                ColAttVal1 = (IsoFormNodeOp.FirstOrDefault(n => n.FormID == FormID && n.ColAttType1 == p.Id) == null ? "" : IsoFormNodeOp.FirstOrDefault(n => n.FormID == FormID && n.ColAttType1 == p.Id).ColAttVal1),
            }).ToList();
            return JavaScriptSerializerUtil.getJson(new
            {
                total = list.Count,
                rows = list

            });
        }
        #endregion


        [Description("列表json")]
        [HttpPost]
        public string GeTaskListJson()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            var TWhere = QueryBuild<DataModel.Models.DesTask>.True();
            string KeyJQSearch = Request.Form["KeyJQSearch"];
            if (string.IsNullOrEmpty(KeyJQSearch))
            {
                KeyJQSearch = "";
            }
            TWhere = TWhere.And(p => p.TaskParentId > 0 && p.DeleterEmpId == 0);
            var ProjId = TypeHelper.parseInt(Request.Params["ProjId"].ToString());    // 
            if (ProjId != 0)
            {
                TWhere = TWhere.And(p => p.ProjId == ProjId);
            }
            var td = opTask.GetPagedList(TWhere, PageModel).ToList();

            var list = from p in td
                       let projModel = proj.Get(p.ProjId)
                       where (projModel == null ? "" : projModel.ProjName).Contains(KeyJQSearch) || (projModel == null ? "" : projModel.ProjNumber).Contains(KeyJQSearch)
                       select new
                       {
                           p.Id,   //任务id
                           p.TaskName, //任务名称
                           p.TaskEmpName,//任务负责人
                           p.DatePlanFinish,//计划结束时间
                           ProjName = projModel == null ? "" : projModel.ProjName,
                           ProjNumber = projModel == null ? "" : projModel.ProjNumber,
                           PhaseName = p.TaskPhaseId == 0 ? "" : String.Join(",", GetBaseName(p.TaskPhaseId.ToString())),//阶段
                           SpecialtyName = p.TaskSpecId == 0 ? "" : String.Join(",", GetBaseName(p.TaskSpecId.ToString())),//专业
                       };

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }


        #region 交付时间
        [Description("交付时间")]
        public ActionResult infoForPublish(int id)
        {
            var model = DesTaskDB.Get(id);
            if (model.DateForPublish.Year != 1900)
            {
                ViewBag.DateForPublish = model.DateForPublish.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                ViewBag.DateForPublish = "未交付";
            }
            return View("infoForPublish", model);
        }
        #endregion


    }
}
