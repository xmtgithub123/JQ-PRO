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
    public class IsoFormProjDelegateServiceRegController : CoreController
    {
        private DBSql.Iso.IsoForm op = new DBSql.Iso.IsoForm();
        private DBSql.Project.Project proj = new DBSql.Project.Project();

        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("DelegateServiceReg")));
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
            //var dt = op.GetPagedList(p => p.RefTable == "IsoFormProjDelegateServiceReg", PageModel).ToList();

            var TWhere = QueryBuild<DataModel.Models.IsoForm>.True();
            string KeyJQSearch = Request.Form["KeyJQSearch"];
            string ProjectPhase = Request.Form["ProjectPhase[]"];
            if (!string.IsNullOrEmpty(KeyJQSearch))
            {
                TWhere = TWhere.And(p => p.FormNote.Contains(KeyJQSearch) || p.FK_IsoForm_ProjID.ProjName.Contains(KeyJQSearch) || p.FK_IsoForm_ProjID.ProjNumber.Contains(KeyJQSearch));
            }
            if (!string.IsNullOrEmpty(ProjectPhase))
            {
                //TWhere = TWhere.And(p => p.FK_IsoForm_ProjID.ProjPhaseIds.Contains(ProjectPhase));
                TWhere = TWhere.And(p => p.FK_IsoForm_ProjID.ProjPhaseIds.Contains(ProjectPhase));
            }
            TWhere = TWhere.And((p => p.RefTable == "IsoFormProjDelegateServiceReg"));


            List<string> result = PermissionList("DelegateServiceReg");
            // emp      个人
            // dep      部门
            // allview  全部
            // alledit  维护
            if (!result.Contains("allview") && result.Contains("dep"))
            {
                TWhere = TWhere.And(p => p.CreatorDepId == this.userInfo.EmpDepID);
            }
            else if (!result.Contains("allview") && result.Contains("emp"))
            {
                TWhere = TWhere.And(p => p.CreatorEmpId == this.userInfo.EmpID);
            }
             
                var td = op.GetPagedList(TWhere, PageModel).ToList();
                var list = from p in td
                           select new
                           {
                               p.FormID,
                              CreationTime= p.CreationTime.ToString("yyyy-MM-dd"),//登记日期
                               p.CreatorEmpName,//登记人
                               p.CreatorEmpId,
                               p.FormReason,  //申请原因
                               p.FormNote,  //备注
                               p.ColAttDate1,   //服务开始时间
                               p.ColAttDate2,   //服务结束时间
                               ProjName = p.FK_IsoForm_ProjID == null ? "" : p.FK_IsoForm_ProjID.ProjName,  //项目名称
                               ProjNumber = p.FK_IsoForm_ProjID == null ? "" : p.FK_IsoForm_ProjID.ProjNumber,//项目编号
                               ProjPhaseIds = p.FK_IsoForm_ProjID == null ? "" : String.Join(",", GetBaseName(p.FK_IsoForm_ProjID.ProjPhaseIds)),//阶段
                               ProjEmpName = p.FK_IsoForm_ProjID == null ? "" : p.FK_IsoForm_ProjID.ProjEmpName,
                           };

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

            ViewBag.Permission = "['submit','close']";
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            BindProjName(model.ProjID);

            List<string> permission = PermissionList("DelegateServiceReg");//获取权限的集合
            if ((permission.Contains("edit")||permission.Contains("alledit")) && model.CreatorEmpId == userInfo.EmpID)//编辑权且该信息是自己登记的
            {
                ViewBag.Permission = "['submit','close']";//可以进行维护
                ViewBag.Upload = "";
            }
            else
            {
                ViewBag.Permission = "['close']";//只能进行查看
                ViewBag.Upload = "$(\"#uploadfile1_upload\").hide();$(\"#uploadfile1_delete\").hide()";//隐藏上传和删除
            }
            return View("info", model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            try
            {
                if (op.GetList(p => id.Contains(p.FormID) && p.CreatorEmpId != userInfo.EmpID).Count() > 0)
                {
                    doResult = DoResultHelper.doSuccess(1, "选中的行包含您无权删除的记录");
                }
                else
                {
                    op.Delete(s => id.Contains(s.FormID));
                    op.UnitOfWork.SaveChanges();
                    doResult = DoResultHelper.doSuccess(1, "删除成功");

                }

            }
            catch(Exception ex)
            {
                doResult = DoResultHelper.doSuccess(1,ex.Message);
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
                model.RefTable = "IsoFormProjDelegateServiceReg";
                model.FormName = "项目工代登记";
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
