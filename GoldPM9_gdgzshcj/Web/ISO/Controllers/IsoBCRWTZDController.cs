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

namespace Iso.Controllers
{
    [Description("IsoBCRWTZD")]
    public class IsoBCRWTZDController : CoreController
    {
        private DBSql.Iso.IsoBCRWTZD op = new DBSql.Iso.IsoBCRWTZD();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        private DBSql.Sys.BaseData dbBaseData = new DBSql.Sys.BaseData();
        private DBSql.Project.Project dbProject = new DBSql.Project.Project();

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjectCenterList")));
            return View();
        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string json()
        {
            List<string> result = PermissionList("ProjectCenterList");
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            var queryInfo = Request.Form["queryInfo"];
            List<Common.Data.Extenstions.Filter> Filter = JavaScriptSerializerUtil.objectToEntity<List<Common.Data.Extenstions.Filter>>(queryInfo);
            if (queryInfo.isNotEmpty())
            {
                foreach (Common.Data.Extenstions.Filter item in Filter)
                {
                    Common.Data.Extenstions.FilterChilde _child = item.list[0];
                    if (_child.Key == "txtLike")
                    {
                        PageModel.TextCondtion = _child.Value;
                    }
                    else if (_child.Key == "CTStartTime")
                    {
                        PageModel.SelectCondtion.Add("CTStartTime", _child.Value);
                    }
                    else if (_child.Key == "CTEndTime")
                    {
                        PageModel.SelectCondtion.Add("CTEndTime", _child.Value);
                    }
                    else if (_child.Key == "TaskPursuantState")
                    {
                        PageModel.SelectCondtion.Add("TaskPursuantState", _child.Value);
                    }
                }
            }

            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "ISO.Id desc";
            }
            if (!(result.Contains("allview") || result.Contains("alledit")))
            {
                if (result.Contains("dep"))
                {
                    PageModel.SelectCondtion.Add("QueryDeptID", this.userInfo.EmpDepID);
                }
                else
                {
                    PageModel.SelectCondtion.Add("QueryEmpID", this.userInfo.EmpID);
                }
            }
            var dt = op.GetListInfo(PageModel, this.userInfo);

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = dt
            });
        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjectCenterList")));

            var model = new IsoBCRWTZD();
            model.TableNumber = "SCX02-03";

            DataModel.Models.Project project = new DBSql.Project.Project().Get(GetRequestValue<int>("projId"));

            ViewBag.ProjDeptName = project.FK_Project_ProjDepId.BaseName;
            ViewBag.ColAttType1Name = project.FK_Project_ProjTypeID.BaseName;
            ViewBag.ProjAreaName = project.FK_Project_ProjAreaID.BaseName;

            ViewBag.CompanyName = project.CustName;
            ViewBag.CompanyLinkMan = project.CustLinkMan;
            ViewBag.CompanyTel = project.CustLinkTel;

            ViewBag.CurrEmpID = userInfo.EmpID;

            DataModel.Models.DesTaskGroup dtg = new DBSql.Design.DesTaskGroup().Get(GetRequestValue<int>("taskGroupId"));
            if (dtg != null)
            {
                model.ProjID = dtg.ProjId;
                model.ProjName = dtg.ProjName;
                model.ProjNumber = dtg.ProjNumber;
                model.ProjPhaseId = GetRequestValue<string>("phaseID");
                model.ProjPhaseName = new DBSql.Sys.BaseData().Get(GetRequestValue<int>("phaseID")).BaseName;
            }

            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjectCenterList")));

            var model = op.Get(id);

            var _project = dbProject.Get(model.ProjID);

            var _projDeptName = dbBaseData.Get(_project.ProjDepId);
            ViewBag.ProjDeptName = null != _projDeptName ? _projDeptName.BaseName : "";

            var _colAttType1Name = dbBaseData.Get(_project.ColAttType1);
            ViewBag.ColAttType1Name = null != _colAttType1Name ? _colAttType1Name.BaseName : "";

            var _projAreaName = dbBaseData.Get(_project.ProjAreaID);
            ViewBag.ProjAreaName = null != _projAreaName ? _projAreaName.BaseName : "";

            ViewBag.CompanyName = _project.CustName;
            ViewBag.CompanyLinkMan = _project.CustLinkMan;
            ViewBag.CompanyTel = _project.CustLinkTel;

            ViewBag.CurrEmpID = model.CreatorEmpId;


            return View("info", model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 0;
            try
            {
                op.UpdateIsoBCRWTZDInfo(id, this.userInfo);
                op.UnitOfWork.SaveChanges();

                new DBSql.PubFlow.Flow().Delete(id, "IsoBCRWTZD");
                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.Delete(id, "IsoBCRWTZD");
                }
                reuslt = 1;
            }
            catch
            {
            }
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id)
        {
            var model = new IsoBCRWTZD();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();

            int reuslt = 0;
            if (model.Id > 0)
            {
                op.Edit(model);
            }
            else
            {
                op.Add(model);
            }
            op.UnitOfWork.SaveChanges();
            if (Id <= 0)
            {
                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.MoveFile(model.Id, userInfo.EmpID, userInfo.EmpName);
                }
            }
            reuslt = model.Id;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion
    }
}
