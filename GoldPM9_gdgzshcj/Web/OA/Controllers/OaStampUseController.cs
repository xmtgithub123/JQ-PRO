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

namespace Oa.Controllers
{
[Description("OaStampUse")]
public class OaStampUseController : CoreController
    {
        private DBSql.Oa.OaStampUse op = new DBSql.Oa.OaStampUse();
        private DBSql.Oa.OaStampInfo opOaStampInfo = new DBSql.Oa.OaStampInfo();
        private DBSql.Sys.BaseData dbBaseData = new DBSql.Sys.BaseData();
        private DBSql.PubFlow.Flow dbFlow = new DBSql.PubFlow.Flow();

        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
              List<string> permission = PermissionList("OaSealApply");
            ViewBag.permission = JavaScriptSerializerUtil.getJson(permission);
            //将调度权与维护权同步
            if (permission.Contains("alledit"))
            {
                ViewBag.Change = 1;//可以进行调度
            }
            else
            {
                ViewBag.Change = 0;//不可进行调度
            }
            ViewBag.EmpId = userInfo.EmpID;//当前登录账户
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
                PageModel.SelectOrder = "Id desc";
            }
            var TWhere = QueryBuild<DataModel.Models.OaStampUse>.True();
            var list = op.GetPagedList(p=>p.DeleterEmpId==0,PageModel).ToList();
            List<string> result = PermissionList("OaSealApply");
            if (!result.Contains("allview") && !result.Contains("dep") && !result.Contains("emp"))
            {
                list = op.GetPagedList(p => p.CreatorEmpId == this.userInfo.EmpID&&p.DeleterEmpId==0, PageModel).ToList();
            }
            else if (result.Contains("allview"))
            {

            }
            else if (result.Contains("dep"))
            {
                list = op.GetPagedList(p => p.CreatorDepId == this.userInfo.EmpDepID && p.DeleterEmpId == 0, PageModel).ToList();
            }
            else if (result.Contains("emp"))
            {
                list = op.GetPagedList(p => p.CreatorEmpId == this.userInfo.EmpID && p.DeleterEmpId == 0, PageModel).ToList();
            }

            var dt = (from p in list
             join t1 in op.DbContext.Set<DataModel.Models.Flow>().Where(f => f.FlowRefTable == "OaStampUse") on p.Id equals t1.FlowRefID into nodes
             from temp in nodes.DefaultIfEmpty()
             select new
             {
                 Id = p.Id,
                 p.CreatorEmpName,
                 p.CreatorEmpId,
                 StampID = p.FK_OaStampUse_StampID == null ? "" : p.FK_OaStampUse_StampID.StampName,
                 p.StampUseDate,
                 p.StampEmpesult,
                 p.StampReturnSrate,
                 FlowID = temp == null ? 0 : temp.Id,
                 FlowName = temp == null ? "" : temp.FlowName,
                 FlowStatusID = temp == null ? 0 : temp.FlowStatusID,
                 FlowStatusName = temp == null ? "" : temp.FlowStatusName,
                 FlowXml = temp == null ? "" : temp.FlowXml
             }).Select(m => new {
                 m.Id,
                 m.CreatorEmpName,
                 m.CreatorEmpId,
                 m.StampID,
                 m.StampUseDate,
                 m.StampEmpesult,
                 m.StampReturnSrate,
                 m.FlowID,
                 m.FlowName,
                 m.FlowStatusID,
                 m.FlowStatusName,
                 m.FlowXml,
                 FlowTurnedEmpIDs = JQ.Util.TypeParse.GetTurnedEmpIDs(m.FlowXml) });

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = dt.ToList(),
                isExport = result.Contains("export") ? 1 : 0
            });
        }
        #endregion


        public int GetFlowStatus(int RefID, string CreateEmpName)
        {
            int flowStatus = 0;
            DataModel.Models.Flow flowModel = dbFlow.GetList(p => p.FlowRefTable == "OaStampUse" &&
            p.FlowRefID == RefID).FirstOrDefault();
            if (flowModel != null)
            {
                flowStatus = flowModel.FlowStatusID;
                if (flowModel.FlowStatusName.Contains("退回") && flowModel.FlowStatusName.Contains(CreateEmpName))
                {
                    flowStatus = 0;
                }
            }
            return flowStatus;
        }


        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            ViewBag.CustName = "";
            var model = new OaStampUse();
            model.StampUseDate = System.DateTime.Now;
            model.CreatorEmpName = userInfo.EmpName;
            ViewBag.permission = "['add','submit']";
            model.StampUseDate = DateTime.Now;
            ViewBag.StampType = "";

            ViewBag.isExist = new DBSql.Hr.Vacation().GetCount(userInfo.EmpID);


            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            var staModel = new DBSql.Oa.OaStampInfo().Get(model.StampID);
            List<string> permissionList = PermissionList("OaSealApply");
            ViewBag.isExport = permissionList.Contains("export") ? 1 : 0;
            ViewBag.ProjName = model.ProjName;
            ViewBag.ProjID = model.ProjId;
            var projModel = new DBSql.Project.Project().Get(model.ProjId);
            if (projModel != null)
            {
                ViewBag.ProjNumber = projModel.ProjNumber;
            }
            else
            {
                ViewBag.ProjNumber = "";
            }
            ViewBag.StampType = staModel.FK_OaStampInfo_StampTypeID.BaseName;
            ViewBag.permission = "['add','submit']";
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
                op.UpdateOaStampUseList(id, this.userInfo);
                op.UnitOfWork.SaveChanges();
                new DBSql.PubFlow.Flow().Delete(id, "OaStampUse");
                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.Delete(id, "OaStampUse");
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
            var model = new OaStampUse();
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
			reuslt = model.Id ;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion


        #region 选择用章列表
        [Description("选择用章列表")]
        public ActionResult FilterList()
        {
            return View();
        }
        #endregion

        #region 列表FilterJson
        [Description("列表FilterJson")]
        [HttpPost]
        public string FilterJson()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }

            var list = opOaStampInfo.GetPagedList(PageModel).ToList();
            //需要的列信息
            var dt = from f in list
                     select new
                     {
                         Id = f.Id,
                         f.StampName,
                         StampTypeID = f.FK_OaStampInfo_StampTypeID == null ? "" : f.FK_OaStampInfo_StampTypeID.BaseName,
                         f.KeepEmpName,
                         f.StampStartDate,
                         f.StampNote,
                         StampStatusID = f.StampStatusID == 0 ? "" : dbBaseData.GetBaseDataInfo(f.StampStatusID).BaseName,
                         LoanRecord = GetName(f.Id,f.StampName),
                         f.FlowModelID,
                     };

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = dt.ToList()
            });
        }
        #endregion

        public string GetName(int Id,string StampName)
        {
            var OaStampUseModel = op.GetList(p => p.StampID == Id && p.DeleterEmpId == 0 && p.StampReturnSrate == 0).OrderByDescending(p => p.Id).FirstOrDefault();
          int stampID = -1;
          if (OaStampUseModel != null)
          {
              stampID = OaStampUseModel.Id;
          }
          var model = dbFlow.GetList(p => p.FlowRefTable == "OaStampUse" && p.FlowRefID == stampID ).OrderByDescending(p => p.Id).FirstOrDefault();
            string Name = string.Empty;
            if(model!=null)
            {
                Name=model.CreatorEmpName + model.CreationTime.ToString("yyyy-MM-dd") + "已借走--(正在申请使用中)";
            }
            return Name;
        }
    }
}
