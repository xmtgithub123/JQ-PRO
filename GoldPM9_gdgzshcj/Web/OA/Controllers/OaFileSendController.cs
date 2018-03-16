using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System;
//using System.Data.Entity.SqlServer;

namespace Oa.Controllers
{
    [Description("OaFileSend")]
    public class OaFileSendController : CoreController
    {
        private DBSql.Oa.OaFileSend op = new DBSql.Oa.OaFileSend();
        private DBSql.Sys.BaseEmployee BaseEmployeeDB = new DBSql.Sys.BaseEmployee();
        private DBSql.Sys.BaseData BaseDataDB = new DBSql.Sys.BaseData();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("OaFileSend")));
            return View();
        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string json()
        {
            List<string> result = PermissionList("OaFileSend");
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            var queryInfo = Request.Form["queryInfo"];
            List<Common.Data.Extenstions.Filter> Filter = JavaScriptSerializerUtil.objectToEntity<List<Common.Data.Extenstions.Filter>>(queryInfo);
            if (queryInfo.isNotEmpty())
            {
                foreach (Common.Data.Extenstions.Filter item in Filter)
                {
                    Common.Data.Extenstions.FilterChilde _child = item.list[0];
                    if (_child.Key == "OaFileName")
                    {
                        PageModel.TextCondtion = _child.Value;
                    }
                    else if (_child.Key == "startTime")
                    {
                        PageModel.SelectCondtion.Add("startTime", _child.Value);
                    }
                    else if (_child.Key == "endTime")
                    {
                        PageModel.SelectCondtion.Add("endTime", _child.Value);
                    }
                }
            }
            if (!result.Contains("allview") && !result.Contains("dep") && !result.Contains("emp"))
            {
                PageModel.SelectCondtion.Add("QueryEmpID", this.userInfo.EmpID);
            }
            else if (result.Contains("allview"))
            {

            }
            else if (result.Contains("dep"))
            {
                PageModel.SelectCondtion.Add("QueryDeptID", this.userInfo.EmpDepID);
            }
            else if (result.Contains("emp"))
            {
                PageModel.SelectCondtion.Add("QueryEmpID", this.userInfo.EmpID);
            }
            var dt = op.GetInfoList(PageModel, this.userInfo);
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
            var model = new OaFileSend();
            model.CreationTime = DateTime.Now;
            model.CreatorEmpName = userInfo.EmpName;
            model.OaFileSendDep = GetBaseInfo(userInfo.EmpDepID).BaseName;
            ViewBag.CreatorEmpId = model.CreatorEmpId;
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            var _listBaseEmployee = BaseEmployeeDB.GetEmpName(model.OaFileGetEmp);
            var _listBaseData = BaseDataDB.getBaseNameByIds(model.OaFileGetEmpDept);
            ViewBag.AcceptEmpNames = _listBaseEmployee;
            ViewBag.AcceptDeptNames = _listBaseData;
            ViewBag.CreatorEmpId = model.CreatorEmpId;
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
                op.UpdateOaFileSendList(id, this.userInfo);
                op.UnitOfWork.SaveChanges();
                new DBSql.PubFlow.Flow().Delete(id, "OaFileSend");
                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.Delete(id, "OaFileSend");
                }
                reuslt = 1;
            }
            catch (Exception)
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
            var model = new OaFileSend();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();

            int reuslt = 0;
            if (model.Id > 0)
            {
                model.LastModificationTime = DateTime.Now;
                model.LastModifierEmpId = userInfo.EmpID;
                model.LastModifierEmpName = userInfo.EmpName;
                op.Edit(model);
            }
            else
            {
                model.OaFileSendDep = GetBaseInfo(userInfo.EmpDepID).BaseName;
                model.CreationTime = DateTime.Now;
                model.CreatorEmpId = userInfo.EmpID;
                model.CreatorEmpName = userInfo.EmpName;
                model.CreatorDepId = userInfo.EmpDepID;
                model.CreatorDepName = GetBaseInfo(userInfo.EmpDepID).BaseName;
                model.LastModificationTime = DateTime.Now;
                model.LastModifierEmpId = userInfo.EmpID;
                model.LastModifierEmpName = userInfo.EmpName;
                op.Add(model);
            }
            op.UnitOfWork.SaveChanges();
            reuslt = model.Id;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion

        /// <summary>
        /// 获取流程状态判断是否可以编辑
        /// </summary>
        /// <param name="RefID"></param>
        /// <param name="CreateEmpName"></param>
        /// <returns></returns>
        private int GetFlowStatusId(int RefID, string CreateEmpName)
        {
            int flowStatusId = 0;
            DataModel.Models.Flow flowModel = new DBSql.PubFlow.Flow().GetList(p => p.FlowRefTable == "OaFileSend" && p.FlowRefID == RefID).FirstOrDefault();
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

    }
}
