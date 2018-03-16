using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System;

namespace Oa.Controllers
{
    [Description("OaCarUse")]
    public class OaCarUseController : CoreController
    {
        private DBSql.OA.OaCarUse op = new DBSql.OA.OaCarUse();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        private DBSql.PubFlow.FlowNode opNode = new DBSql.PubFlow.FlowNode();


        public ActionResult selectcar()
        {
            ViewData["leave"] = GetRequestValue<DateTime>("leave");
            ViewData["arrive"] = GetRequestValue<DateTime>("arrive");
            if (ViewData["leave"].ToString() == "0001/1/1 0:00:00")
                ViewData["leave"] = Request["leave"] + ":00:00";
            if (ViewData["arrive"].ToString() == "0001/1/1 0:00:00")
                ViewData["arrive"] = Request["arrive"] + ":00:00";
            return View();    // View();
        }
        public ActionResult CarUselist()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("CarUseStatistics")));
            return View();
        }
        public ActionResult CarUseStatistics()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("CarUse")));
            return View();
        }
        public ActionResult CarUseEmp()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("CarUseStatisticsByEmp")));
            return View();
        }
        #region 取司机
        public ActionResult GetDirverList()
        {
            var carOp = new DBSql.OA.OaCar();
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "dbo.OaCar.Id desc";
            }

            var list = carOp.GetCarList(PageModel).AsEnumerable().Select(p => new
            {
                CarDriver = p["CarDriver"],
            }).Distinct().ToList();

            return Json(list.Select(s => new
            {
                s.CarDriver

            }).OrderBy(s => s.CarDriver).ToList());
        }
        #endregion

        #region 用车记录
        public string jsonStatu()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "cu.Id desc";
            }
            PageModel.SelectCondtion.Add("UseStatu", 1);
            var list = op.GetCarUseList(PageModel, userInfo).AsEnumerable().Select(p => new
            {
                Id = p["Id"],
                CarID = p["CarID"],
                CarInfo = p["CarName"] + "[" + p["CarNumber"] + "]",
                check = "登记",
                UsePurpose = p["UsePurpose"],
                UsePlace = p["UsePlace"],
                UseLeaderEmp = p["UseLeaderEmp"],
                UseApplyDatetime = p["UseApplyDatetime"],
                UsePeople = p["UsePeople"],
                UsePeopleNum = p["UsePeopleNum"],
                UseNote = p["UseNote"],
                DateLeavePlan = p["DateLeavePlan"],
                DateArrivePlan = p["DateArrivePlan"],
                DateLeave = p["DateLeave"],
                DateArrive = p["DateArrive"],
                UseCarDriver = p["UseCarDriver"],
                UseLeaderEmpName = p["UseLeaderEmpName"],

                IsFinish = p["IsFinish"]
            }).OrderByDescending(p => p.DateLeavePlan).ToList();

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }
        #endregion
        #region 按车统计
        public string jsonCarUseStatistics()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "t.Id desc";
            }
            PageModel.SelectCondtion.Add("UseStatu", 1);

            var list = op.GetTotalByCar(PageModel).AsEnumerable().Select(p => new
            {
                CarNumber = p["CarNumber"],
                CarName = p["CarName"],
                CarDriver = p["CarDriver"],
                // TotalKm = p["TotalKm"],
                TotalCarFee = p["TotalCarFee"],
                TotalInvoice = p["TotalInvoice"],

            }).ToList();

            //PageModel.PageTotleRowCount = list.Count;

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }
        #endregion
        #region 按人统计
        public string jsonCarUseEmp()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "x.CreatorEmpId desc";
            }
            PageModel.SelectCondtion.Add("UseStatu", 1);
            var list = op.GetTotalByEmp(PageModel).AsEnumerable().Select(p => new
            {
                UseApplyEmp = p["UseApplyEmp"],
                DeptName = p["DeptName"],
                TotalCarTime = p["TotalCarTime"],
                //TotalKm = p["TotalKm"],
                TotalCarFee = p["TotalCarFee"],
                TotalInvoice = p["TotalInvoice"],
            }).ToList();

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }
        #endregion
        #region
        public string jsoncar()
        {
            var carOp = new DBSql.OA.OaCar();
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "dbo.OaCar.Id desc";
            }
            int useid = GetRequestValue<int>("useid");
            var usemodel = op.Get(useid);
            if (usemodel != null)
            {
                if (usemodel.DateLeavePlan.Year != 1900)
                {
                    PageModel.SelectCondtion.Add("DateLower", usemodel.DateLeavePlan);
                }
                if (usemodel.DateArrivePlan.Year != 1900)
                {
                    PageModel.SelectCondtion.Add("DateUpper", usemodel.DateArrivePlan);
                }
            }
            var list = carOp.GetCarListByUse(PageModel).AsEnumerable().Select(p => new
            {
                Id = p["Id"],
                CarName = p["CarName"],
                CarNumber = p["CarNumber"],
                CarSeat = p["CarSeat"],
                CarOil = p["CarOil"],
                CarDriver = p["CarDriver"],
                CarBuyDate = p["CarBuyDate"],
                CarIsUse = op.CheckCarUse(useid, int.Parse(p["Id"].ToString()), usemodel.DateLeavePlan, usemodel.DateArrivePlan) > 0 ? "使用中" : "正常"
            }).ToList();

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }

        /// <summary>
        /// 选择车辆参照
        /// </summary>
        /// <returns></returns>
        public string jsoncar_ref()
        {
            var carOp = new DBSql.OA.OaCar();
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "dbo.OaCar.Id desc";
            }
            int useid = GetRequestValue<int>("useid");
            DateTime leave = GetRequestValue<DateTime>("leave");
            DateTime arrive = GetRequestValue<DateTime>("arrive");

            if (leave.Year != 1900)
            {
                PageModel.SelectCondtion.Add("DateLower", leave);
            }
            if (arrive.Year != 1900)
            {
                PageModel.SelectCondtion.Add("DateUpper", arrive);
            }

            var list = carOp.GetCarListByUse(PageModel).AsEnumerable().Select(p => new
            {
                Id = p["Id"],
                CarName = p["CarName"],
                CarNumber = p["CarNumber"],
                CarSeat = p["CarSeat"],
                CarOil = p["CarOil"],
                CarDriver = p["CarDriver"],
                CarBuyDate = p["CarBuyDate"],
                CarIsUse = op.CheckCarUse(useid, int.Parse(p["Id"].ToString()), leave, arrive) > 0 ? "使用中" : "正常"
            }).ToList();

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }
        #endregion



        public ActionResult editBefore(int id)
        {

            var model = op.Get(id);
            var carOp = new DBSql.OA.OaCar();
            var empOp = new DBSql.Sys.BaseEmployee();
            var LeaderempInfo = empOp.Get(model.UseLeaderEmp);
            var carModel = carOp.Get(model.CarID);

            try
            {
                ViewData["CarInfo"] = carModel.CarName + "[" + carModel.CarNumber + "]";
            }
            catch
            {
                ViewData["CarInfo"] = "";
            }

            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("CarApply")));
            ViewBag.LempInfo = LeaderempInfo.EmpID.ToString();

            ViewBag.CreatorEmpId = model.CreatorEmpId;
            return View("Createinfo", model);
        }


        public string editPhone(int id)
        {
            try
            {
                var model = op.Get(id);
                ViewBag.model = model;
                var carModel = new DBSql.OA.OaCar().Get(model.CarID);
                try
                {
                    ViewBag.CarInfo = carModel.CarName + "[" + carModel.CarNumber + "]";
                }
                catch
                {
                    ViewBag.CarInfo = "";
                }
                ViewBag.CreatorEmpId = model.CreatorEmpId;
                return JavaScriptSerializerUtil.getJson(ViewBag);
            }
            catch (Exception)
            {
                return "";
            }

        }

        public ActionResult edit01(int id)
        {

            var model = op.Get(id);
            var carOp = new DBSql.OA.OaCar();
            var carModel = carOp.Get(model.CarID);

            try
            {
                ViewData["CarInfo"] = carModel.CarName + "[" + carModel.CarNumber + "]";
            }
            catch
            { }
            ViewBag.CreatorEmpId = model.CreatorEmpId;
            if (model.DateLeave.Year == 1900)
            {
                model.DateLeave = model.DateLeavePlan;
            }
            if (model.DateArrive.Year == 1900)
            {
                model.DateArrive = model.DateArrivePlan;
            }
            return View("CheckIn", model);
        }

        public ActionResult editView(int id)
        {

            var model = op.Get(id);
            var carOp = new DBSql.OA.OaCar();
            var carModel = carOp.Get(model.CarID);

            try
            {
                ViewData["CarInfo"] = carModel.CarName + "[" + carModel.CarNumber + "]";
            }
            catch
            { }
            string permission = JavaScriptSerializerUtil.getJson((PermissionList("CarApply")));
            ViewBag.AllowDelete = false;
            ViewBag.AllowUpload = false;
            if (permission.Contains("alledit") || permission.Contains("edit") && model.CreatorEmpId == userInfo.EmpID)
            {
                ViewBag.AllowDelete = true;
                ViewBag.AllowUpload = true;
                ViewBag.permission = "['submit','close']";
            }
            ViewBag.CreatorEmpId = model.CreatorEmpId;
            return View("ViewCheckIn", model);
        }
        public ActionResult insert(int id)
        {
            ViewData["EditType"] = "Insert";
            var model = op.Get(id);
            var carOp = new DBSql.OA.OaCar();
            var carModel = carOp.Get(model.CarID);
            ViewData["CarInfo"] = carModel.CarName + "[" + carModel.CarNumber + "]";
            //var model = new
            //{
            //    id = usemodel.Id,
            //    UsePurpose = usemodel.UsePurpose,
            //    UsePlace = usemodel.UsePlace,
            //    DateLeavePlan= usemodel.DateLeavePlan,
            //    DateArrivePlan = usemodel.DateArrivePlan,
            //    UseLeaderEmp = usemodel.UseLeaderEmp,
            //    UsePeople = usemodel.UsePeople,
            //    UsePeopleNum = usemodel.UsePeopleNum,
            //    CarID = usemodel.CarID,
            //    CarInfo = carModel.CarName+"["+carModel.CarNumber+"]",
            //    UseNote = usemodel.UseNote,

            //};
            ViewBag.CreatorEmpId = model.CreatorEmpId;
            return View("info", model);
        }


        #region 取人员
        public string GetUser()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "DepartmentOrder asc,EmpOrder asc";
            }
            var list = new DBSql.Sys.AllBaseEmployee().GetPagedDynamic(PageModel).ToList();
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }

        #endregion


        public bool CheckCarIsUse(int Id)
        {
            var model = new OaCarUse();
            // var FormType = Request.Params["hFormType"].ToString();
            if (Id > 0)
            {
                model = op.Get(Id);

            }
            string sCarID = "0";
            if (Request.Params["hCarID"] != null)
            {
                sCarID = Request.Params["hCarID"].ToString();
                if (op.CheckCarUse(Id, int.Parse(sCarID), model.DateLeavePlan, model.DateArrivePlan) > 0)
                {
                    //DoResult drf = DoResultHelper.doError("该车已有安排请重新选择车辆！");
                    return false;
                }
            }
            return true;
        }
        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id)
        {
            var model = new OaCarUse();
            var FormType = Request.Params["hFormType"].ToString();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            string leavePlan = Request["DateLeavePlan"].ToString() + ":00:00";
            string arrPlan = Request["DateArrivePlan"].ToString() + ":00:00";
            string leave = Request["DateLeave"].ToString() + ":00:00";
            string arr = Request["DateArrive"].ToString() + ":00:00";
            model.MvcSetValue();
            try
            {
                model.DateLeavePlan = DateTime.Parse(leavePlan);
                model.DateArrivePlan = DateTime.Parse(arrPlan);
            }
            catch { }
            try
            {
                model.DateLeave = DateTime.Parse(leave);
                model.DateArrive = DateTime.Parse(arr);
            }
            catch { }
            try
            {

                if (FormType == "SetCar")
                {

                    string sCarID = "0";
                    if (Request.Params["hCarID"] != null)
                    {
                        sCarID = Request.Params["hCarID"].ToString();
                        if (op.CheckCarUse(Id, int.Parse(sCarID), model.DateLeavePlan, model.DateArrivePlan) > 0)
                        {
                            DoResult drf = DoResultHelper.doError("该车已有安排请重新选择车辆！");
                            return Json(drf);
                        }
                    }
                    else
                    {
                        sCarID = "0";
                    }
                    model.CarID = int.Parse(sCarID);
                }
            }
            catch
            {
                model.CarID = 0;
            }

            int reuslt = 0;
            if (model.Id > 0)
            {
                model.MvcDefaultEdit(userInfo.EmpID);
                if (FormType == "Finish")
                {
                    model.IsFinish = 1;
                }
                op.Edit(model);

            }
            else
            {
                model.MvcDefaultSave(userInfo.EmpID);
                model.UseApplyDatetime = System.DateTime.Now;

                op.Add(model);
            }
            op.UnitOfWork.SaveChanges();
            reuslt = model.Id;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion


        #region

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("CarApply")));
            ViewBag.CurrentEmpID = userInfo.EmpID;
            return View();
        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string json()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();

            List<string> result = PermissionList("CarApply");
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "cu.Id desc";
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

            var list = op.GetCarUseList(PageModel, userInfo).AsEnumerable().Select(p => new
            {
                Id = p["Id"],
                CarID = p["CarID"],
                CarInfo = p["CarName"] + "[" + p["CarNumber"] + "]",
                check = "登记",
                UsePurpose = p["UsePurpose"],
                UsePlace = p["UsePlace"],
                UseLeaderEmp = p["UseLeaderEmp"],
                UseApplyDatetime = p["UseApplyDatetime"],
                UsePeople = p["UsePeople"],
                UsePeopleNum = p["UsePeopleNum"],
                UseNote = p["UseNote"],
                DateLeavePlan = p["DateLeavePlan"],
                DateArrivePlan = p["DateArrivePlan"],
                DateLeave = p["DateLeave"],
                DateArrive = p["DateArrive"],
                UseCarDriver = p["UseCarDriver"],
                UseLeaderEmpName = p["UseLeaderEmpName"],
                FlowStatusID = p["FlowStatusID"],
                FlowID = p["FlowID"],
                FlowName = p["FlowName"],
                FlowStatusName = p["FlowStatusName"],
                FlowTurnedEmpIDs = p["FlowTurnedEmpIDs"],
                CreatorEmpId = p["CreatorEmpId"],
                IsFinish = p["IsFinish"],
                FlowNodeTypeID = p["FlowNodeTypeID"],
                FlowMultiSignStatus = p["FlowMultiSignStatus"],
                FlowFinishControls = p["FlowFinishControls"],
                FlowNodeOrder= p["FlowNodeOrder"]

            }).ToList();

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
            var model = new OaCarUse();
            ViewBag.CreatorEmpId = model.CreatorEmpId;
            model.DateLeavePlan = System.DateTime.Now;
            model.DateArrivePlan = System.DateTime.Now;
            ViewBag.CreatorEmpId = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("CarApply")));
            return View("Createinfo", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var FlowNodeID = GetRequestValue<int>("flowNodeID");
            var model = op.Get(id);

            var carOp = new DBSql.OA.OaCar();
            var empOp = new DBSql.Sys.BaseEmployee();
            var LeaderempInfo = empOp.Get(model.UseLeaderEmp);
            model.CarID = model.CarID == 0 ? -1 : model.CarID;
            var carModel = carOp.Get(model.CarID);
            var FlowNode = new DataModel.Models.FlowNode();

            var IsSelectCar = 0;
            try
            {
                string sName = GetRequestValue<string>("sName");
                if (sName.IndexOf("退回") > -1)
                    IsSelectCar = 0;
                else
                    IsSelectCar = 1;
            }
            catch (Exception ex)
            {
                IsSelectCar = 1;
                try
                {
                    var flownode = new DBSql.PubFlow.FlowNode().Get(FlowNodeID);
                    var flow = new DBSql.PubFlow.Flow().Get(flownode.FlowID);
                    if (flow.FlowStatusName.IndexOf("退") > -1)
                        IsSelectCar = 0;
                }
                catch(Exception ex1)
                {
                }
            }

            ViewBag.IsSelectCar = IsSelectCar;
            try
            {
                ViewData["CarInfo"] = carModel.CarName + "[" + carModel.CarNumber + "]";
            }
            catch
            {
                ViewData["CarInfo"] = "";
            }

            ViewBag.LempInfo = LeaderempInfo.EmpID.ToString();
            ViewData["EditType"] = "SetCar";
            ViewBag.CreatorEmpId = model.CreatorEmpId;
            ViewBag.permission = JavaScriptSerializerUtil.getJson(PermissionList("CarApply"));
            return View("UplateUseInfo", model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 0;
            try
            {
                op.UpdateOaCarList(id, this.userInfo);
                op.UnitOfWork.SaveChanges();
                new DBSql.PubFlow.Flow().Delete(id, "CarUse");
                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.Delete(id, "OaCarUse");
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
        #endregion
    }
}
