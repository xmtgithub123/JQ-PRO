using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml;
using System.Data.Entity;
using System;
using Common.Data.Extenstions;
using System.Web.Script.Serialization;
using DataModel;

namespace Bussiness.Controllers
{
    [Description("BussBiddingFileController")]
    public class BussBiddingFileController : CoreController
    {
        private DBSql.Bussiness.BussBiddingInfo op = new DBSql.Bussiness.BussBiddingInfo();
        private DBSql.Bussiness.BussBiddingInfoPackage dbBiddingInfoPackage = new DBSql.Bussiness.BussBiddingInfoPackage();
        DBSql.Sys.BaseData dbBaseData = new DBSql.Sys.BaseData();
        DBSql.Sys.BaseEmployee dbBaseEmployee = new DBSql.Sys.BaseEmployee();
        DBSql.Project.Project dbProject = new DBSql.Project.Project();

        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("BidFile")));
            ViewBag.CurrEmpID = userInfo.EmpID;
            return View();
        }

        [Description("投标文件制作(创景)列表")]
        public ActionResult list_cj()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("BidFile_CJ")));
            ViewBag.CurrEmpID = userInfo.EmpID;
            return View();
        }

        [Description("投标文件制作(创景)列表")]
        public ActionResult list_sj()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("BidFile_SJ")));
            ViewBag.CurrEmpID = userInfo.EmpID;
            return View();
        }

        [Description("投标文件制作(创景)列表")]
        public ActionResult list_gc()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("BidFile_GC")));
            ViewBag.CurrEmpID = userInfo.EmpID;
            return View();
        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string json()
        {
            List<string> result = PermissionList("BidInfo");
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id";
            }
            var TWhere = QueryBuild<DataModel.Models.BussBiddingInfo>.True().And(m => m.DeleterEmpId == 0);
            if (!result.Contains("allview") && !result.Contains("dep") && !result.Contains("emp"))
            {
                TWhere= TWhere.And(p => p.CreatorEmpId == this.userInfo.EmpID);
            }
            else if (result.Contains("allview"))
            {

            }
            else if (result.Contains("dep"))
            {
                TWhere= TWhere.And(p => p.CreatorDepId == this.userInfo.EmpDepID);
            }
            else if (result.Contains("emp"))
            {
                TWhere= TWhere.And(p => p.CreatorEmpId == this.userInfo.EmpID);
            }
            var list = op.GetPagedList(TWhere, PageModel).ToList();

            var target = (from item in list
                          select new
                          {
                              item.Id,
                              item.BiddingBatch,
                              item.BiddingNumber,
                              BiddingManageID = item.FK_BussBiddingInfo_BiddingManageID == null ? 0 : item.FK_BussBiddingInfo_BiddingManageID.EmpID,
                              BiddingManageName = item.FK_BussBiddingInfo_BiddingManageID == null ? "" : item.FK_BussBiddingInfo_BiddingManageID.EmpName,
                              item.BiddingOpeningTime,

                              CustomerID = item.FK_BussBiddingInfo_CustID == null ? 0 : item.FK_BussBiddingInfo_CustID.Id,
                              CustomerName = item.CustName == null ? "" : item.CustName,

                              DeptId = item.BiddingDeptId,
                              DeptName = "",

                              IsTemporaryName = (item.IsTemporary == 0 ? "否" : "是"),
                              item.BiddingSourse,
                              datas = item.FK_BussBiddingInfoPackage_BussBiddingInfoID.Where(m => m.BussBiddingInfoID == item.Id).ToList().Select(m => new { m.Id, m.PackageNumber, m.FK_BussBiddingInfoPackage_BiddingProgress.BaseName, WinTime = m.WinTime.ToString("yyyy-MM-dd"), CostID = m.FK_BussBiddingCost_BussBiddingInfoPackageID.FirstOrDefault(p => p.BussBiddingInfoPackageID == m.Id) == null ? 0 : m.FK_BussBiddingCost_BussBiddingInfoPackageID.FirstOrDefault(p => p.BussBiddingInfoPackageID == m.Id).Id })
                          }).ToList();

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = target
            });
        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new Project();

            ViewData["ProjEmpId1"] = "0";
            ViewData["ProjEmpId2"] = "0";

            ViewData["BusinessEmpId1"] = "0";
            ViewData["BusinessEmpId2"] = "0";


            ViewData["TechnologyEmpId1"] = "0";
            ViewData["TechnologyEmpId2"] = "0";

            model.DateCreate = DateTime.Now;
            ViewBag.CreatorEmpId = userInfo.EmpID;

            string page = "info";
            string companyType = Request.Params["CompanyType"].ToString();
            switch (companyType)
            {
                case "SJ":
                    page = "info_sj";
                    break;
                case "GC":
                    page = "info_gc";
                    break;
                case "CJ":
                    page = "info_cj";
                    break;
                default:
                    page = "info";
                    break;
            }

            return View(page, model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = dbProject.Get(id);
            var _Bidding = op.Get(model.ColAttType7);
            //var _Package = dbBiddingInfoPackage.Get(model.ColAttType8);

            ViewBag.BiddingBatch = _Bidding.BiddingBatch;
            ViewBag.BiddingNumber = _Bidding.BiddingNumber;
            ViewBag.EngineeringName = _Bidding.EngineeringName;
            //ViewBag.PackageNumber = _Package.PackageNumber;

            ViewData["ProjEmpId1"] = model.ProjEmpId.ToString();
            ViewData["ProjEmpId2"] = dbBaseEmployee.GetBaseEmployeeInfo(model.ProjEmpId) == null ? "0" : dbBaseEmployee.GetBaseEmployeeInfo(model.ProjEmpId).EmpDepID.ToString();

            ViewData["BusinessEmpId1"] = model.ColAttType9.ToString();
            ViewData["BusinessEmpId2"] = dbBaseEmployee.GetBaseEmployeeInfo(model.ColAttType9) == null ? "0" : dbBaseEmployee.GetBaseEmployeeInfo(model.ColAttType9).EmpDepID.ToString();


            ViewData["TechnologyEmpId1"] = model.ColAttType10.ToString();
            ViewData["TechnologyEmpId2"] = dbBaseEmployee.GetBaseEmployeeInfo(model.ColAttType10) == null ? "0" : dbBaseEmployee.GetBaseEmployeeInfo(model.ColAttType10).EmpDepID.ToString();
            ViewBag.CreatorEmpId = model.CreatorEmpId;

            string page = "info";
            string companyType = Request.Params["CompanyType"].ToString();
            switch (companyType)
            {
                case "SJ":
                    page = "info_sj";
                    break;
                case "GC":
                    page = "info_gc";
                    break;
                case "CJ":
                    page = "info_cj";
                    break;
                default:
                    page = "info";
                    break;
            }

            return View(page, model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 0;
            try
            {
                dbProject.UpdateProjInfoList(id, this.userInfo);
                dbProject.UnitOfWork.SaveChanges();

                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.Delete(id, "Project");
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
        [ValidateInput(false)]
        public ActionResult save(int Id)
        {
            int result = 0;
            int type = 0;
            //页面model 项目信息
            var model = new DataModel.Models.Project();
            var _chilid = new DataModel.Models.Project();

            if (Id > 0)
            {
                _chilid = dbProject.Get(Id);
                model = dbProject.FirstOrDefault(p => p.DeleterEmpId == 0 && p.Id == _chilid.ParentId);
                model.MvcSetValueExceptKeys("Id");
                _chilid.MvcSetValueExceptKeys("Id");
            }
            else {
                model.MvcSetValue();
                _chilid.MvcSetValue();
            }

         


            model.ColAttType6 = 1;
            model.ColAttType7 = TypeHelper.parseInt(Request.Form["BiddingId"]);
            model.ColAttType8 = TypeHelper.parseInt(Request.Form["BiddingInfoID"]);
            var _ProjEmpId = Request.Form["ProjEmpId"];
            model.ProjEmpId = TypeHelper.parseInt(_ProjEmpId.Split('-')[0]);
            model.ProjEmpName = dbBaseEmployee.GetBaseEmployeeInfo(model.ProjEmpId).EmpName;

            var _BusinessEmpId = Request.Form["BusinessEmpId"];
            model.ColAttType9 = TypeHelper.parseInt(_BusinessEmpId.Split('-')[0]);

            var _TechnologyEmpId = Request.Form["ProjEmpId"];
            model.ColAttType10 = TypeHelper.parseInt(_TechnologyEmpId.Split('-')[0]);
            model.ProjPhaseIds = ((int)ProjectPhase.前期投标文件).ToString();
            model.ColAttType11 = TypeHelper.parseInt(Request.Form["Duration"]);


            _chilid.ColAttType6 = 1;
            _chilid.ColAttType7 = TypeHelper.parseInt(Request.Form["BiddingId"]);
            _chilid.ColAttType8 = TypeHelper.parseInt(Request.Form["BiddingInfoID"]);

            _chilid.ColAttType9 = model.ColAttType9;
            _chilid.ColAttType10 = model.ColAttType10;
            _chilid.ProjEmpId = model.ProjEmpId;
            _chilid.ProjPhaseIds = ((int)ProjectPhase.前期投标文件).ToString();
            model.ProjNumber = model.ProjName;
            _chilid.ProjNumber = _chilid.ProjName;
            _chilid.ProjEmpName = model.ProjEmpName;
            _chilid.ColAttType11 = model.ColAttType11;
            if (model.Id > 0)
            {
                type = 1;
                model.MvcDefaultEdit(userInfo);
                _chilid.MvcDefaultEdit(userInfo);
                dbProject.Edit(model);

            }
            else
            {
                model.DateCreate = DateTime.Now;
                type = 0;
                model.MvcDefaultSave(userInfo);
                _chilid.MvcDefaultSave(userInfo);
                dbProject.Add(model);
            }
            dbProject.UnitOfWork.SaveChanges();
            result = model.Id;

            ////新插入的子项信息
            int parentId = model.Id;
            string ColAttVal2 = model.ColAttVal2;

            if (type == 0)
            {
                _chilid.ParentId = parentId;
                _chilid.ColAttVal2 = string.Format("{0}/", ColAttVal2 == "" ? parentId.ToString() : ColAttVal2 + "/" + parentId.ToString());
                dbProject.Add(_chilid);
                dbProject.UnitOfWork.SaveChanges();
                var ba = new DBSql.Sys.BaseAttach();
                ba.MoveFile(_chilid.Id, this.userInfo.EmpID, this.userInfo.EmpName);
            }
            else
            {
                _chilid.ColAttVal2 = string.Format("{0}/", ColAttVal2 == "" ? parentId.ToString() : ColAttVal2 + "/" + parentId.ToString());
                dbProject.Edit(_chilid);
                dbProject.UnitOfWork.SaveChanges();
            }
            //---------------------------------------------------DesTaskGroup------------------------------------------
            //插入协同设计相关表数据
            DBSql.Design.DesTaskGroup desTaskGroupDB = new DBSql.Design.DesTaskGroup();
            DBSql.Design.DesTaskGantt desTaskGanttDB = new DBSql.Design.DesTaskGantt();
            // 插入项目分组起始节点
            var projTaskGroup = desTaskGroupDB.InsertProjGroupNode(_chilid.Id, string.Format("[{0}]{1}", _chilid.ProjNumber, _chilid.ProjName), _chilid.ProjEmpId, _chilid.ProjEmpName, _chilid.DatePlanStart, _chilid.DatePlanFinish, DBSql.Design.TaskGroupStatus.进行中, userInfo, _chilid.ProjNumber, _chilid.ProjName);

            var projTaskGantt = desTaskGanttDB.InsertProjTaskGantt(_chilid.Id, string.Format("[{0}]{1}", _chilid.ProjNumber, _chilid.ProjName), _chilid.ColAttType11, projTaskGroup.Id, _chilid.ProjEmpId, _chilid.DatePlanStart, _chilid.DatePlanFinish, userInfo);
     

            // 获取项目阶段信息
            var projPhaseIDs = _chilid.ProjPhaseIds.Split(',');
            var projPhases = new DBSql.Sys.BaseData().GetQuery(x => projPhaseIDs.Contains(x.BaseID.ToString()));
            // 插入项目阶段节点
            var v = 0;
            foreach (var projPhase in projPhases)
            {
                v++;
                var phaseTaskGroup = desTaskGroupDB.InsertPhaseGroupNode(projTaskGroup, projPhase.BaseID, projPhase.BaseName, _chilid.ProjEmpId, _chilid.ProjEmpName, _chilid.DatePlanStart, _chilid.DatePlanFinish, (v == 1 ? DBSql.Design.TaskGroupStatus.进行中 : DBSql.Design.TaskGroupStatus.未启动), userInfo, _chilid.ProjNumber, _chilid.ProjName);
                desTaskGanttDB.InsertPhaseTaskGantt(projTaskGantt, phaseTaskGroup, userInfo);
            }
            //---------------------------------------------------DesTaskGroup----End--------------------------------------   

            DoResult dr = result > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }

        #endregion
        public int GetProjNameCount(string ProjName, int Id)
        {
            int Count = dbProject.GetProjNameCount(ProjName, Id);
            return Count;
        }

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string GetBiddingFileList()
        {
            List<string> result = new List<string>();
            // emp  个人    // dep 部门    // allview  全部   // alledit  维护
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            var queryInfo = Request.Form["queryInfo"];
            List<Common.Data.Extenstions.Filter> Filter = JavaScriptSerializerUtil.objectToEntity<List<Common.Data.Extenstions.Filter>>(queryInfo);

            string companyType = Request.Params["CompanyType"].ToString();
            switch (companyType)
            {
                case "SJ":
                    result = PermissionList("BidFile_SJ");
                    PageModel.SelectCondtion.Add("CompanyID", 1);
                    break;
                case "GC":
                    result = PermissionList("BidFile_GC");
                    PageModel.SelectCondtion.Add("CompanyID", 2);
                    break;
                case "CJ":
                    result = PermissionList("BidFile_CJ");
                    PageModel.SelectCondtion.Add("CompanyID", 3);
                    break;
                default:
                    result = PermissionList("BidFile");
                    PageModel.SelectCondtion.Add("CompanyID", 0);
                    break;
            }

            if (queryInfo.isNotEmpty())
            {
                foreach (Common.Data.Extenstions.Filter item in Filter)
                {
                    Common.Data.Extenstions.FilterChilde _child = item.list[0];
                    if (_child.Key == "txtLike")
                    {
                        PageModel.TextCondtion = _child.Value;
                    }
                    else if (_child.Key == "DateCreateS")
                    {
                        PageModel.SelectCondtion.Add("DateCreateS", _child.Value);
                    }
                    else if (_child.Key == "DateCreateE")
                    {
                        PageModel.SelectCondtion.Add("DateCreateE", _child.Value);
                    }
                    else if (_child.Key == "DatePlanStartS")
                    {
                        PageModel.SelectCondtion.Add("DatePlanStartS", _child.Value);
                    }
                    else if (_child.Key == "DatePlanStartE")
                    {
                        PageModel.SelectCondtion.Add("DatePlanStartE", _child.Value);
                    }
                    else if (_child.Key == "DatePlanFinishS")
                    {
                        PageModel.SelectCondtion.Add("DatePlanFinishS", _child.Value);
                    }
                    else if (_child.Key == "DatePlanFinishE")
                    {
                        PageModel.SelectCondtion.Add("DatePlanFinishE", _child.Value);
                    }
                }
            }
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "p.Id desc";
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

            var dt = dbProject.GetBiddingFileList(PageModel);
            
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = dt
            });

        }
        #endregion
    }
}
