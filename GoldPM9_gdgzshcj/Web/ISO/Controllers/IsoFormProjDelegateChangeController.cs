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
    public class IsoFormProjDelegateChangeController : CoreController
    {
        private DBSql.Iso.IsoForm op = new DBSql.Iso.IsoForm();
        private DBSql.Project.Project proj = new DBSql.Project.Project();

        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            return View();
        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string json()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
         
            var TWhere = QueryBuild<DataModel.Models.IsoForm>.True();
            string KeyJQSearch = Request.Form["KeyJQSearch"];
            string ProjectPhase = Request.Form["ProjectPhase"];
            if (!string.IsNullOrEmpty(KeyJQSearch))
            {
                TWhere = TWhere.And(p => p.FormReason.Contains(KeyJQSearch) || p.FormNote.Contains(KeyJQSearch) || p.FK_IsoForm_ProjID.ProjName.Contains(KeyJQSearch) || p.FK_IsoForm_ProjID.ProjNumber.Contains(KeyJQSearch));
            }
            if (!string.IsNullOrEmpty(ProjectPhase))
            {
                TWhere = TWhere.And(p => p.FK_IsoForm_ProjID.ProjPhaseIds.Contains(ProjectPhase));
            }
            TWhere = TWhere.And((p => p.RefTable == "IsoFormProjDelegateChange"));

            var list = op.GetList(TWhere).Select(p => new
            {
                p.FormID,
                p.CreationTime,//登记日期
                p.CreatorEmpName,//登记人
                p.FormReason,  //申请原因
                p.FormNote,  //备注
                ProjName = p.FK_IsoForm_ProjID == null ? "" : p.FK_IsoForm_ProjID.ProjName,  //项目名称
                ProjNumber = p.FK_IsoForm_ProjID == null ? "" : p.FK_IsoForm_ProjID.ProjNumber,//项目编号
                ProjPhaseIds = p.FK_IsoForm_ProjID == null ? "" : String.Join(",", GetBaseName(p.FK_IsoForm_ProjID.ProjPhaseIds)),//阶段
                ProjEmpName = p.FK_IsoForm_ProjID == null ? "" : p.FK_IsoForm_ProjID.ProjEmpName,
            });

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
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            BindProjName(model.ProjID);
            return View("info", model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 1;
            op.Delete(s => id.Contains(s.FormID));
            op.UnitOfWork.SaveChanges();
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
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
                model.RefTable = "IsoFormProjDelegateChange";
                model.FormName = "工代变更申请";
                model.MvcDefaultSave(userInfo.EmpID);
                op.Add(model);
            }
            op.UnitOfWork.SaveChanges();
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
            }
            else
            {
                ViewBag.ProjNumber = "";
                ViewBag.PhaseID = "";
                ViewBag.ProjID = "";
                ViewBag.ProjEmpName = "";
            }
        }
    }
}
