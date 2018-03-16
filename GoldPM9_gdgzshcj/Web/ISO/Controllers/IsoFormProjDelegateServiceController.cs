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

namespace ISO.Controllers
{
    [Description("IsoForm")]
    public class IsoFormProjDelegateServiceController : CoreController
    {
        private DBSql.Iso.IsoForm op = new DBSql.Iso.IsoForm();
        private DBSql.Project.Project proj = new DBSql.Project.Project();
        private DBSql.PubFlow.Flow flow = new DBSql.PubFlow.Flow();

        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjDelegate")));
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
            //var dt = op.GetPagedList(p => p.RefTable == "IsoFormProjDelegateService", PageModel).ToList();

            var TWhere = QueryBuild<DataModel.Models.IsoForm>.True();
            string KeyJQSearch = Request.Form["KeyJQSearch"];
            string ProjectPhases = Request.Form["ProjectPhases[]"];
            string ProjectPhase = Request.Form["ProjectPhase"];
            if (!string.IsNullOrEmpty(KeyJQSearch))
            {
                TWhere = TWhere.And(p => p.FormReason.Contains(KeyJQSearch) || p.FormNote.Contains(KeyJQSearch) || p.FK_IsoForm_ProjID.ProjName.Contains(KeyJQSearch) || p.FK_IsoForm_ProjID.ProjNumber.Contains(KeyJQSearch));
            }
            if (!string.IsNullOrEmpty(ProjectPhases))
            {
                TWhere = TWhere.And(p => p.FK_IsoForm_ProjID.ProjPhaseIds.Contains(ProjectPhases));
                //TWhere = TWhere.And(p => ProjectPhase.Contains(p.FK_IsoForm_ProjID.ProjPhaseIds));
            }
            if (!string.IsNullOrEmpty(ProjectPhase))
            {
                // TWhere = TWhere.And(p => p.FK_IsoForm_ProjID.ProjPhaseIds.Contains(ProjectPhase));
            }
            TWhere = TWhere.And((p => p.RefTable == "IsoFormProjDelegateService"));

            List<string> result = PermissionList("ProjDelegate");
            // emp      个人
            // dep      部门
            // allview  全部
            // alledit  维护
            if (!(result.Contains("allview")||result.Contains("alledit")))
            {
                if (result.Contains("dep"))
                {
                    TWhere = TWhere.And(p => p.CreatorDepId == this.userInfo.EmpDepID);
                }
                else
                {
                    TWhere = TWhere.And(p => p.CreatorEmpId == this.userInfo.EmpID);
                }
            }



            var td = op.GetPagedList(TWhere, PageModel).ToList();
            var list = (from p in td
                       join t1 in op.DbContext.Set<DataModel.Models.Flow>().Where(f => f.FlowRefTable == "IsoFormProjDelegateService") on p.FormID equals t1.FlowRefID into nodes
                       from temp in nodes.DefaultIfEmpty()
                       select new
                       {
                           p.FormID,
                           CreationTime = p.CreationTime.ToString("yyyy-MM-dd"),//登记日期
                           p.CreatorEmpName,//登记人
                           p.CreatorEmpId,
                           p.FormReason,  //申请原因
                           p.FormNote,  //备注
                           ProjName = p.FK_IsoForm_ProjID == null ? "" : p.FK_IsoForm_ProjID.ProjName,  //项目名称
                           ProjNumber = p.FK_IsoForm_ProjID == null ? "" : p.FK_IsoForm_ProjID.ProjNumber,//项目编号
                           ProjPhaseIds = p.FK_IsoForm_ProjID == null ? "" : String.Join(",", GetBaseName(p.FK_IsoForm_ProjID.ProjPhaseIds)),//阶段
                           ProjEmpName = p.FK_IsoForm_ProjID == null ? "" : p.FK_IsoForm_ProjID.ProjEmpName,
                           //FlowStatus = GetFlowStatusId(p.FormID, p.CreatorEmpName)
                           FlowID = temp == null ? 0 : temp.Id,
                           FlowName = temp == null ? "" : temp.FlowName,
                           FlowStatusID = temp == null ? 0 : temp.FlowStatusID,
                           FlowStatusName = temp == null ? "" : temp.FlowStatusName,
                           FlowXml = temp == null ? "" : temp.FlowXml
                       }).Select(m => new { m.FormID, m.CreationTime, m.CreatorEmpName, m.CreatorEmpId, m.FormReason, m.FormNote, m.ProjName, m.ProjNumber, m.ProjPhaseIds, m.ProjEmpName, m.FlowID, m.FlowName, m.FlowStatusID, m.FlowStatusName, m.FlowXml, FlowTurnedEmpIDs = JQ.Util.TypeParse.GetTurnedEmpIDs(m.FlowXml) });

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
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
            ExportPermission();
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            BindProjName(model.ProjID);
            List<string> permission = PermissionList("ProjDelegate");
            int FlowStatus = GetFlowStatusId(model.FormID, model.CreatorEmpName);
            if ((permission.Contains("edit") || permission.Contains("alledit")) && model.CreatorEmpId == userInfo.EmpID && FlowStatus == 0)//默认显示
            {
                //编辑权限(保存和提交按钮显示)
                ViewBag.editPermission = "";
                ViewBag.submitPermission = "";
            }
            else
            {
                if (FlowStatus > 0)// 流程自动控制
                {
                    ViewBag.editPermission = "";
                    ViewBag.submitPermission = "";
                }
                else
                {
                    if (FlowStatus == 0)
                    {
                        //隐藏提交和保存
                        ViewBag.editPermission = ",isShowSave:false";
                        ViewBag.submitPermission = "$('#toolbar').children('a').eq(0).hide();";
                    }
                }

            }
            ExportPermission();
            return View("info", model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            try
            {
                var list = op.GetList(p => id.Contains(p.FormID));
                int Count = (from p in list
                            where GetFlowStatusId(p.FormID, p.CreatorEmpName) != 0 || p.CreatorEmpId != userInfo.EmpID
                            select p.FormID).Count();
                if (Count > 0)
                {
                    doResult = DoResultHelper.doSuccess(1, "选中的行中包含您无权删除的记录");
                }
                else
                {
                    op.Delete(s => id.Contains(s.FormID));
                    op.UnitOfWork.SaveChanges();
                    new DBSql.PubFlow.Flow().Delete(id, "IsoFormProjDelegateService");
                    doResult = DoResultHelper.doSuccess(1, "删除成功");

                }
            }
            catch(Exception ex)
            {
                doResult = DoResultHelper.doError(ex.Message);
            }
            return Json(doResult);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int FormID)
        {
            var model = new IsoForm();
            if (FormID > 0)
            {
                model = op.Get(FormID);
            }
            model.MvcSetValue();

            int reuslt = 0;
            if (model.FormID > 0)
            {
                op.MvcDefaultEdit(userInfo.EmpID);
                op.Edit(model);
            }
            else
            {
                model.RefTable = "IsoFormProjDelegateService";
                model.FormName = "项目工代";
                model.MvcDefaultSave(userInfo.EmpID);
                op.Add(model);
            }
            op.UnitOfWork.SaveChanges();

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
                ViewBag.ProjName = project.ProjName;
            }
            else
            {
                ViewBag.ProjNumber = "";
                ViewBag.PhaseID = "";
                ViewBag.ProjID = "";
                ViewBag.ProjEmpName = "";
                ViewBag.ProjName = "";
            }
        }

        /// <summary>
        /// 获取流程状态判断是否可以编辑
        /// </summary>
        /// <param name="RefID"></param>
        /// <param name="CreateEmpName"></param>
        /// <returns></returns>
        private int GetFlowStatusId(int RefID, string CreateEmpName)
        {
            int flowStatusId = 0;
            DataModel.Models.Flow flowModel = new DBSql.PubFlow.Flow().GetList(p => p.FlowRefTable == "IsoFormProjDelegateService" && p.FlowRefID == RefID).FirstOrDefault();
            if (flowModel != null)
            {
                flowStatusId = flowModel.FlowStatusID;
                if (flowModel.FlowStatusName.Contains("退回") && flowModel.FlowStatusName.Contains(CreateEmpName))
                {
                    flowStatusId = 0;//创建人可进行编辑
                }
            }
            return flowStatusId;
        }

        /// <summary>
        /// 判断是否有导出权
        /// </summary>
        private void ExportPermission()
        {
            List<string> permission = PermissionList("ProjDelegate");
            if (permission.Contains("export"))
            {
                ViewBag.ExportPermission = "['close', 'exportForm']";
            }
            else
            {
                ViewBag.ExportPermission = "['close']";
            }
        }
    }
}
