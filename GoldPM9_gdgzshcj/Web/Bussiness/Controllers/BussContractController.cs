using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq.Expressions;
using System;
using Common.Data.Extenstions;
using System.Web.Script.Serialization;

namespace Bussiness.Controllers
{
    [Description("BussContract")]
    public class BussContractController : CoreController
    {
        private DBSql.Bussiness.BussContract op = new DBSql.Bussiness.BussContract();
        private DBSql.Bussiness.BussFeePlan BFP = new DBSql.Bussiness.BussFeePlan();
        private DBSql.Bussiness.BussFeeFact BF = new DBSql.Bussiness.BussFeeFact();
        private DBSql.Bussiness.BussFeeInvoice BFI = new DBSql.Bussiness.BussFeeInvoice();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        /// <summary>
        /// Json获取条件参数
        /// </summary>
        public class TempCondition
        {
            /// <summary>
            /// 客户id
            /// </summary>
            public int CustID { get; set; }

            /// <summary>
            /// 筛选内容
            /// </summary>
            public string Filter { get; set; }

            public int ProjID { get; set; }
            public string CustName { get; set; }
        }

        #region 列表
        [Description("收款合同列表(分公司)")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ContractInfo")));
            ViewBag.Feepermission = JavaScriptSerializerUtil.getJson((PermissionList("ContractFee")));
            int CustID = string.IsNullOrEmpty(Request.Params["CustID"]) ? 0 : Convert.ToInt32(Request.Params["CustID"]);
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewData["CustID"] = CustID;
            return View();
        }

        [Description("收款合同列表(设计公司)")]
        public ActionResult list_sj()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ContractInfo_SJ")));
            ViewBag.Feepermission = JavaScriptSerializerUtil.getJson((PermissionList("ContractFee_SJ")));
            int CustID = string.IsNullOrEmpty(Request.Params["CustID"]) ? 0 : Convert.ToInt32(Request.Params["CustID"]);
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewData["CustID"] = CustID;
            return View();
        }

        [Description("收款合同列表(工程公司)")]
        public ActionResult list_gc()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ContractInfo_GC")));
            ViewBag.Feepermission = JavaScriptSerializerUtil.getJson((PermissionList("ContractFee_GC")));
            int CustID = string.IsNullOrEmpty(Request.Params["CustID"]) ? 0 : Convert.ToInt32(Request.Params["CustID"]);
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewData["CustID"] = CustID;
            return View();
        }

        [Description("收款合同列表(创景公司)")]
        public ActionResult list_cj()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ContractInfo_CJ")));
            ViewBag.Feepermission = JavaScriptSerializerUtil.getJson((PermissionList("ContractFee_CJ")));
            int CustID = string.IsNullOrEmpty(Request.Params["CustID"]) ? 0 : Convert.ToInt32(Request.Params["CustID"]);
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewData["CustID"] = CustID;
            return View();
        }

        [Description("根据客户ID、获取合同列表")]
        public ActionResult listByCust(int CustID)
        {
            var CustModel = new DBSql.Bussiness.BussCustomer().Get(CustID);
            if (CustModel != null)
            {
                ViewData["CustName"] = CustModel.CustName;
            }
            else
            {
                ViewData["CustName"] = "-1";
            }

            return View(Url.Action("", "") + "?");
        }


        /// <summary>
        /// 选择工程
        /// </summary>
        /// <returns></returns>
        public ActionResult selectProj()
        {
            return View();
        }


        /// <summary>
        /// 选择工程
        /// 返回Json值
        /// </summary>
        /// <param name="projIDs"></param>
        /// <returns></returns>
        public string JsonProject(string projIDs)
        {
            DBSql.Project.Project con = new DBSql.Project.Project();
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();

            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = " DatePlanStart desc";
            }

            string companyType = Request.Params["CompanyType"].ToString();
            int CompanyID = 0;
            switch (companyType)
            {
                case "SJ":
                    CompanyID = 1;
                    break;
                case "GC":
                    CompanyID = 2;
                    break;
                case "CJ":
                    CompanyID = 3;
                    break;
                default:
                    CompanyID = 0;
                    break;
            }

            // 只查询子任务
            string OnlyTask = string.IsNullOrEmpty(Request.Params["OnlyTask"]) ? "" : Request.Params["OnlyTask"].ToString().ToLower();

            var Thwere = QueryBuild<DataModel.Models.Project>.True();

            if ("true" == OnlyTask)
            {
                Thwere = Thwere.And(p => p.DeleterEmpId == 0 && p.ParentId > 0);
            }
            else
            {
                Thwere = Thwere.And(p => p.DeleterEmpId == 0 && p.ParentId == 0);
            }

            Thwere = Thwere.And(p => p.CompanyID == CompanyID);
            var list = con.GetPagedList(Thwere, PageModel).Select(p => new
            {
                p.Id,
                p.ProjNumber,
                p.ProjName,
                p.CustName,
                p.CustLinkMan,
                p.ProjEmpName,
                p.ProjVoltID,
                p.DatePlanStart,
                p.DatePlanFinish,
                p.ParentId,
            }).ToList();

            var fatherIds = list.Select(p => p.Id).ToArray();
            var Childs = con.GetList(p => fatherIds.Contains(p.ParentId) && p.DeleterEmpId == 0).Select(p => new
            {
                p.Id,
                p.ProjNumber,
                p.ProjName,
                p.CustName,
                p.CustLinkMan,
                p.ProjEmpName,
                p.ProjVoltID,
                p.DatePlanStart,
                p.DatePlanFinish,
                p.ParentId
            }).ToList();
            var datalist = new DBSql.Sys.BaseData().GetDataSetByEngName("ProjectVolt");
            int rowIndex = PageModel.PageIndex - 1 < 1 ? 0 : PageModel.PageIndex - 1;
            var Newlist = list.Select((p, index) => new
            {
                row_number = (rowIndex * PageModel.PageSize) + (index + 1),
                p.Id,
                p.ProjNumber,
                p.ProjName,
                p.CustName,
                p.CustLinkMan,
                p.ProjEmpName,
                ProjVoltName = datalist.FirstOrDefault(b => b.BaseID == p.ProjVoltID) == null ? "" : datalist.FirstOrDefault(b => b.BaseID == p.ProjVoltID).BaseName,
                p.DatePlanStart,
                p.DatePlanFinish,
                state = Childs.Where(t => t.ParentId == p.Id).Count() == 0 ? "" : "expanded",
                p.ParentId,
                children = Childs.Where(t => t.ParentId == p.Id).Select(x => new
                {
                    x.Id,
                    x.ProjNumber,
                    x.ProjName,
                    x.CustName,
                    x.CustLinkMan,
                    x.ProjEmpName,
                    ProjVoltName = datalist.FirstOrDefault(b => b.BaseID == x.ProjVoltID) == null ? "" : datalist.FirstOrDefault(b => b.BaseID == x.ProjVoltID).BaseName,
                    x.DatePlanStart,
                    x.DatePlanFinish,
                    x.ParentId,
                })
            });

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = Newlist
            });
        }


        /// <summary>
        /// 显示收款 开票
        /// </summary>
        /// <param name="ConID"></param>
        /// <returns></returns>
        public ActionResult FeeDetails(int ConID)
        {
            var model = op.Get(ConID);
            var ProjList = (new DBSql.Bussiness.BusProjContractRelation().GetList(p => p.ConID == ConID)).Select(p => new { ProjID = p.ProjID, text = p.FK_BusProjContractRelation_ProjID == null ? "" : p.FK_BusProjContractRelation_ProjID.ProjName }).ToList();
            ProjList.Add(new { ProjID = 0, text = "未指定项目" });
            ViewData["Projs"] = JavaScriptSerializerUtil.getJson(ProjList.OrderBy(p => p.ProjID).ToList());

            model.ConFee = op.GetConFee(ConID);
            return View(model);
        }
        #endregion

        #region 列表json
        [Description("收款合同列表json")]
        [HttpPost]
        public string json(TempCondition condition)
        {
            List<string> result = PermissionList("ContractInfo");
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "c.ConDate  desc";
            }

            string companyType = Request.Params["CompanyType"].ToString();
            switch (companyType)
            {
                case "SJ":
                    result = PermissionList("ContractInfo_SJ");
                    PageModel.SelectCondtion.Add("CompanyID", 1);
                    PageModel.SelectCondtion.Add("RefTable", "BussContract_SJ");
                    break;
                case "GC":
                    result = PermissionList("ContractInfo_GC");
                    PageModel.SelectCondtion.Add("CompanyID", 2);
                    PageModel.SelectCondtion.Add("RefTable", "BussContract_GC");
                    break;
                case "CJ":
                    result = PermissionList("ContractInfo_CJ");
                    PageModel.SelectCondtion.Add("CompanyID", 3);
                    PageModel.SelectCondtion.Add("RefTable", "BussContract_CJ");
                    break;
                default:
                    result = PermissionList("ContractInfo");
                    PageModel.SelectCondtion.Add("CompanyID", 0);
                    PageModel.SelectCondtion.Add("RefTable", "BussContract");
                    break;
            }

            //扩展列
            #region MyRegion
            string column = ",(select Count(1) from dbo.BussContract act where act.FatherID=c.Id) as ChildLength";
            column += @",case BGConFee when 0 then ((select ISNULL(Sum(ConFee),0) from BussContract act where (act.FatherID=c.Id or act.ID=c.Id)))
                else (select ISNULL(Sum(ConFee),0) from BussContract act where act.FatherID=c.Id and ColAttVal1<>'1')+(select BGConFee from BussContract bc where bc.Id=c.Id)
                end as SumConFee";
            column += ",(select Sum(ConBalanceFee) from BussContract act where (act.FatherID=c.Id or act.ID=c.Id)) as SumConBalanceFee";
            #endregion
            PageModel.SelectCondtion.Add("OtherColumn", column);
            PageModel.SelectCondtion.Add("DeleterEmpId", "0");
            PageModel.SelectCondtion.Add("FatherID", "0");
            if (!result.Contains("allview"))
            {
                if (result.Contains("dep"))
                {
                    PageModel.SelectCondtion.Add("CreatorDepId", userInfo.EmpDepID);
                }
                else
                {
                    PageModel.SelectCondtion.Add("CreateEmpId", userInfo.EmpID);
                }
            }
            if (condition.CustID != 0)
            {
                var objTemp = new DBSql.Bussiness.BussCustomer().Get(condition.CustID);
                PageModel.SelectCondtion.Add("CustName", objTemp.CustName);
            }
            if (condition.Filter != "")
            {
                PageModel.TextCondtion = condition.Filter;
            }
            if (condition.ProjID != 0)
            {
                PageModel.SelectCondtion.Add("ProjID", condition.ProjID);
            }
            if (Request.Params["q"] != null && Request.Params["q"].ToString() != "")
            {
                PageModel.TextCondtion = Request.Params["q"].ToString();
            }

            if (Request.Params["ConDefaultID"] != null)
            {
                PageModel.SelectCondtion.Add("DefaultConID", Request.Params["ConDefaultID"].ToString());
                if (Request.Params["ConDefaultID"].ToString() != "0")
                {
                    PageModel.SelectOrder = " (case when c.Id=" + Request.Params["ConDefaultID"].ToString() + " then 0 else 1 end) asc,c.Id  desc ";
                }
            }


            DataTable dt = op.GetContractList(PageModel);

            #region 子查询
            Common.SqlPageInfo NewCondition = new Common.SqlPageInfo();
            NewCondition.SelectCondtion.Add("DeleterEmpId", "0");
            NewCondition.SelectCondtion.Add("IsFatherID", "0");
            if (!NewCondition.SelectOrder.isNotEmpty())
            {
                NewCondition.SelectOrder = "c.Id  desc";
            }

            switch (companyType)
            {
                case "SJ":
                    NewCondition.SelectCondtion.Add("RefTable", "BussContract_SJ");
                    break;
                case "GC":
                    NewCondition.SelectCondtion.Add("RefTable", "BussContract_GC");
                    break;
                case "CJ":
                    NewCondition.SelectCondtion.Add("RefTable", "BussContract_CJ");
                    break;
                default:
                    NewCondition.SelectCondtion.Add("RefTable", "BussContract");
                    break;
            }
            var _Alldt = op.GetContractList(NewCondition);
            #endregion

            var NewList = dt.AsEnumerable().Select(p => new
            {
                row_number = p["row_number"],
                Id = p["Id"],
                Keyid = p["Id"],
                FatherID = 0,
                ConNumber = p["ConNumber"],
                ConName = p["ConName"],
                CustName = p["CustName"],
                ConDate = p["ConDate"],
                ConFee = p["ConFee"],
                FeeFact = p["FeeFact"],
                FeeInvoice = p["FeeInvoice"],
                ConFulfilType = p["ConFulfilType"],
                ConFulfilTypeName = p["ConFulfilTypeName"],
                ConBalanceFee = p["ConBalanceFee"],
                SumConFee = p["SumConFee"],
                SumConBalanceFee = p["SumConBalanceFee"],
                ProjId = p["ProjId"],
                ColAttVal1 = p["ColAttVal1"],
                NewConFee = GetNoFee(p["ConFulfilType"].ToString(), p["SumConFee"].ToString(), p["SumConBalanceFee"].ToString()),
                ConIsFeeFinished = p["ConIsFeeFinished"].ToString() == "True" ? "是" : "否",
                NoFee = Common.ExtensionMethods.Value<decimal>(GetNoFee(p["ConFulfilType"].ToString(), p["SumConFee"].ToString(), p["SumConBalanceFee"].ToString())) - Common.ExtensionMethods.Value<decimal>(p["FeeFact"]) < 0 ? 0 : (Common.ExtensionMethods.Value<decimal>(GetNoFee(p["ConFulfilType"].ToString(), p["SumConFee"].ToString(), p["SumConBalanceFee"].ToString())) - Common.ExtensionMethods.Value<decimal>(p["FeeFact"])),
                FlowIDD = p["FlowIDD"],
                FlowName = p["FlowName"],
                FlowStatusID = p["FlowStatusID"],
                FlowStatusName = p["FlowStatusName"],
                FlowTurnedEmpIDs = p["FlowTurnedEmpIDs"],
                CreatorEmpId = p["CreatorEmpId"],
                EngineeringNumber=p["EngineeringNumber"],
                ConStatusShow=p["ConStatusShow"],
                ConStreamNumber=p["ConStreamNumber"],
                state = Common.ExtensionMethods.Value<int>(p["ChildLength"]) > 0 ? "open" : "",
                children = _Alldt.AsEnumerable().Where(s => Common.ExtensionMethods.Value<int>(s["FatherID"]) == Common.ExtensionMethods.Value<int>(p["Id"])).Select(t => new
                {
                    Id = t["Id"],
                    Keyid = t["Id"],
                    ConNumber = t["ConNumber"],
                    ConName = t["ConName"],
                    CustName = t["CustName"],
                    ConDate = t["ConDate"],
                    ConFulfilType = p["ConFulfilType"],
                    ConFee =Convert.ToDouble(t["ConFee"]).ToString("F2"),
                    ConBalanceFee = Convert.ToDouble(t["ConBalanceFee"]).ToString("F2"),
                    SumConFee = Convert.ToDouble(t["ConFee"]).ToString("F2"),
                    SumConBalanceFee = Convert.ToDouble(t["ConBalanceFee"]).ToString("F2"),
                    NewConFee = p["ConFulfilType"].ToString() == ((int)DataModel.ConDealWays.开口).ToString() ? Convert.ToDouble(t["ConFee"]).ToString("F2") : Convert.ToDouble(t["ConBalanceFee"]).ToString("F2"),
                    FeeFact = "",
                    ColAttVal1 = t["ColAttVal1"],
                    FeeInvoice = "",
                    NoFee = "",
                    FatherID = t["FatherID"],
                    ConIsFeeFinished = "",
                    FlowIDD = t["FlowIDD"],
                    FlowName = t["FlowName"],
                    FlowStatusID = t["FlowStatusID"],
                    FlowStatusName = t["FlowStatusName"],
                    FlowTurnedEmpIDs = t["FlowTurnedEmpIDs"],
                    CreatorEmpId = t["CreatorEmpId"],
                    EngineeringNumber=t["EngineeringNumber"],
                    ConStatusShow = p["ConStatusShow"],
                    ConStreamNumber = p["ConStreamNumber"],
                }),
            });
            string custName = condition.CustName == null ? "" : condition.CustName;
            var listResult = NewList.Where(p => p.CustName.ToString().IndexOf(custName) >= 0);
            if (Request.QueryString["Footer"] != null)
            {
                string row = JavaScriptSerializerUtil.getJson(new
                {
                    total = PageModel.PageTotleRowCount,
                    rows = listResult,
                    footer = ShowFooter(PageModel)
                });
                return row;
            }
            else
            {
                return JavaScriptSerializerUtil.getJson(new
                {
                    total = PageModel.PageTotleRowCount,
                    rows = listResult
                });
            }
        }
        private object ShowFooter(Common.SqlPageInfo condition)
        {
            condition.PageIndex = 0;
            condition.ToPageData = true;

            DataTable dt = op.GetContractList(condition);
            List<object> list = new List<object>();
            object a = new
            {
                Footer = 1,
                ConFulfilType = (int)DataModel.ConDealWays.开口,
                //NewConFee = Convert.ToDouble(dt.AsEnumerable().Sum(p => p["ConFulfilType"].ToString() == ((int)DataModel.ConDealWays.开口).ToString() ? Common.ExtensionMethods.Value<decimal>(p["SumConFee"].ToString()) : Common.ExtensionMethods.Value<decimal>(p["ConBalanceFee"].ToString()))).ToString("F2"),
                NewConFee = Convert.ToDouble(dt.AsEnumerable().Sum(p => Common.ExtensionMethods.Value<decimal>(p["SumConFee"].ToString()))).ToString("F2"),
                ColAttVal1 = "合计：",
                FeeFact = Convert.ToDouble(dt.AsEnumerable().Sum(p => Common.ExtensionMethods.Value<decimal>(p["FeeFact"].ToString()))).ToString("F2"),
                FeeInvoice = Convert.ToDouble(dt.AsEnumerable().Sum(p => Common.ExtensionMethods.Value<decimal>(p["FeeInvoice"].ToString()))).ToString("F2")

            };
            list.Add(a);
            return list;
        }
        public string NewJson(TempCondition condition)
        {
            List<string> result = PermissionList("ContractInfo");
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "c.Id  desc";
            }
            //扩展列
            #region MyRegion
            string column = ",(select Count(1) from dbo.BussContract act where act.FatherID=c.Id) as ChildLength";
            //column += ",(select Sum(ConFee) from BussContract act where (act.FatherID=c.Id or act.ID=c.Id)) as SumConFee";
            column += @",case BGConFee when 0 then ((select Sum(ConFee) from BussContract act where (act.FatherID=c.Id or act.ID=c.Id)))
                else (select Sum(ConFee) from BussContract act where act.FatherID=c.Id and ColAttVal1<>'1')+(select BGConFee from BussContract bc where bc.Id=c.Id)
                end as SumConFee";

            column += ",(select Sum(ConBalanceFee) from BussContract act where (act.FatherID=c.Id or act.ID=c.Id)) as SumConBalanceFee";
            #endregion
            PageModel.SelectCondtion.Add("OtherColumn", column);
            PageModel.SelectCondtion.Add("DeleterEmpId", "0");
            if (Request.Params["FatherID"] == null)
            {
                PageModel.SelectCondtion.Add("FatherID", "0");
            }
            else
            {
                PageModel.SelectCondtion.Add("FatherID", Request.Params["FatherID"].ToString());
            }

            if (!result.Contains("allview"))
            {
                if (result.Contains("dep"))
                {
                    PageModel.SelectCondtion.Add("CreatorDepId", userInfo.EmpDepID);
                }
                else
                {
                    PageModel.SelectCondtion.Add("CreateEmpId", userInfo.EmpID);
                }
            }
            if (condition.CustID != 0)
            {
                var objTemp = new DBSql.Bussiness.BussCustomer().Get(condition.CustID);
                PageModel.SelectCondtion.Add("CustName", objTemp.CustName);
            }
            if (condition.Filter != "")
            {
                PageModel.TextCondtion = condition.Filter;
            }
            if (condition.ProjID != 0)
            {
                PageModel.SelectCondtion.Add("ProjID", condition.ProjID);
            }
            if (Request.Params["q"] != null && Request.Params["q"].ToString() != "")
            {
                PageModel.TextCondtion = Request.Params["q"].ToString();
            }

            DataTable dt = op.GetContractList(PageModel);

            var NewList = dt.AsEnumerable().Select(p => new
            {
                Id = p["Id"],
                Keyid = p["Id"],
                FatherID = 0,
                ConNumber = p["ConNumber"],
                ConName = p["ConName"],
                CustName = p["CustName"],
                ConDate = p["ConDate"],
                ConFee = p["ConFee"],
                FeeFact = p["FeeFact"],
                FeeInvoice = p["FeeInvoice"],
                ConFulfilType = p["ConFulfilType"],
                ConFulfilTypeName = p["ConFulfilTypeName"],
                ConBalanceFee = p["ConBalanceFee"],
                SumConFee = p["SumConFee"],
                SumConBalanceFee = p["SumConBalanceFee"],
                NewConFee = GetNoFee(p["ConFulfilType"].ToString(), p["SumConFee"].ToString(), p["SumConBalanceFee"].ToString()),
                ConIsFeeFinished = p["ConIsFeeFinished"].ToString() == "True" ? "已结清" : "未结清",
                NoFee = Common.ExtensionMethods.Value<decimal>(GetNoFee(p["ConFulfilType"].ToString(), p["SumConFee"].ToString(), p["SumConBalanceFee"].ToString())) - Common.ExtensionMethods.Value<decimal>(p["FeeFact"]) < 0 ? 0 : (Common.ExtensionMethods.Value<decimal>(GetNoFee(p["ConFulfilType"].ToString(), p["SumConFee"].ToString(), p["SumConBalanceFee"].ToString())) - Common.ExtensionMethods.Value<decimal>(p["FeeFact"])),
                FlowIDD = p["FlowIDD"],
                FlowName = p["FlowName"],
                FlowStatusID = p["FlowStatusID"],
                FlowStatusName = p["FlowStatusName"],
                FlowTurnedEmpIDs = p["FlowTurnedEmpIDs"],
                CreatorEmpId = p["CreatorEmpId"],
            });

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = NewList
            });
        }

        public string GetNoFee(string Type, string ConFee, string ConBalance)
        {
            if (Type == ((int)DataModel.ConDealWays.开口).ToString())
            {
                return ConFee;
            }
            else
            {
                return ConBalance;
            }
        }

        public JsonResult GetProjJson(string projIDs)
        {

            List<DataModel.Models.Project> _list = new List<Project>();
            if (projIDs == null)
            {
                _list.Add(new DataModel.Models.Project() { Id = 0, ProjName = "不指定项目" });
                return Json(_list.Select(p => new { id = p.Id.ToString(), text = p.ProjName }));
            }
            int[] Id = (from n in projIDs.Split(',') where n != "" select Common.ExtensionMethods.Value<int>(n)).ToArray();
            var TWhere = QueryBuild<DataModel.Models.Project>.True();
            TWhere = TWhere.And(p => Id.Contains(p.Id));
            if (Id.Count() > 0)
            {
                List<DataModel.Models.Project> list = new DBSql.Project.Project().GetList(TWhere).ToList();
                //DataModel.Models.Project _a = new DataModel.Models.Project(){;
                list.Add(new DataModel.Models.Project() { Id = 0, ProjName = "不指定项目" });
                var Newlist = list.Select(p => new { id = p.Id.ToString(), text = p.ProjName }).OrderBy(p => p.id).ToList();
                return Json(Newlist);
            }
            else
            {
                _list.Add(new DataModel.Models.Project() { Id = 0, ProjName = "不指定项目" });
                return Json(_list.Select(p => new { id = p.Id.ToString(), text = p.ProjName }));
            }

        }

        public JsonResult jsonPlan(FormCollection Tcondtion)
        {
            int ConID = 0;
            string projIDs = "";
            if (Tcondtion["ConID"] != null)
            {
                ConID = Common.ExtensionMethods.Value<int>(Tcondtion["ConID"]);
            }
            if (Tcondtion["projIDs"] != null)
            {
                projIDs = Tcondtion["projIDs"].ToString();

            }
            DataModel.Models.BussContract conModel = op.Get(ConID);
            if (conModel == null)
            {
                var NullList = BFP.GetList(p => p.Id == -1).ToList();
                return Json(new { total = NullList.Count(), rows = NullList });
            }
            var projIDArray = (from n in projIDs.Split(',') select Common.ExtensionMethods.Value<int>(n)).ToArray();
            var list = BFP.GetList(p => p.DeleterEmpId == 0 && projIDArray.Contains(p.ProjID) && p.ConID == ConID).ToList();
            return Json(new
            {
                total = list.Count,
                rows = list
            });

        }
        #endregion

        #region 新增收款合同中项目列表
        /// <summary>
        /// 获取相应的项目信息
        /// </summary>
        /// <param name="ConID"></param>
        /// <returns></returns>string projIDs, int NConID
        public string GetProjList(FormCollection Tcondition)
        {
            var relationOp = new DBSql.Bussiness.BusProjContractRelation();
            string projIDs = "";
            int ConID = 0;
            if (Tcondition["ConID"] != null)
            {
                ConID = Common.ExtensionMethods.Value<int>(Tcondition["ConID"]);
            }

            if (Tcondition["projIDs"] != null)
            {
                projIDs = Tcondition["projIDs"].ToString();
            }
            else if (Tcondition["projIDs[]"] != null)
            {
                //js grid中方法中重新加载url传值
                projIDs = Tcondition["projIDs[]"].ToString();
            }


            int[] Id = (from n in projIDs.Split(',') where n != "" select Common.ExtensionMethods.Value<int>(n)).ToArray();
            var TWhere = QueryBuild<DataModel.Models.Project>.True();
            if (Id.Length == 0)
            {
                TWhere = TWhere.And(p => p.Id == -1);
            }
            else
            {
                var Ids = string.Join(",", Id.Select(p => p.ToString()));
                TWhere = TWhere.And(p => Id.Contains(p.Id));
            }
            TWhere = TWhere.And(p => p.DeleterEmpId == 0);

            var list = new DBSql.Project.Project().GetList(TWhere).Select(p => new
            {
                //Id = p.Id,
                ProjID = p.Id,
                ProjNumber = p.ProjNumber,
                p.ProjName,
                ConFee = (relationOp.FirstOrDefault(n => n.ProjID == p.Id && n.ConID == ConID) == null ? 0 : relationOp.FirstOrDefault(n => n.ProjID == p.Id && n.ConID == ConID).ConFee),
            }).ToList();
            return JavaScriptSerializerUtil.getJson(new
            {
                total = list.Count,
                rows = list

            });
        }
        #endregion


        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new BussContract();
            model.CreateDate = System.DateTime.Now;
            ViewBag.CreatorEmpId = userInfo.EmpID;
            model.CreateEmpName = userInfo.EmpName;
            ViewBag.permission = "['add','submit']";

            ViewBag.EmpId = userInfo.EmpID.ToString();
            ViewBag.EmpName = userInfo.EmpName.ToString();
            ViewBag.Time = DateTime.Now.ToString("yyyy-MM-dd");

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

        /// <summary>
        /// 新增子项合同
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [Description("新增子项合同")]
        public ActionResult AddChild(int parentId)
        {
            var model = op.Get(parentId);
            var NewModel = new DataModel.Models.BussContract();
            NewModel.FatherID = model.Id;
            model.Id = 0;
            //手动将父合同的信息，赋值给子合同
            NewModel.ConMainNumber = model.ConNumber;
            NewModel.ConNumber = model.ConNumber;
            NewModel.ConName = model.ConName;
            NewModel.ConType = model.ConType;
            NewModel.ConFulfilType = model.ConFulfilType;
            NewModel.ConStatus = model.ConStatus;
            NewModel.CustName = model.CustName;
            NewModel.CustLinkManName = model.CustLinkManName;
            NewModel.CustLinkManTel = model.CustLinkManTel;
            NewModel.CustLinkManWeb = model.CustLinkManWeb;
            NewModel.ConDate = System.DateTime.Now;

            ViewBag.EmpId = userInfo.EmpID.ToString();

            NewModel.CreateDate = System.DateTime.Now;
            NewModel.CreateEmpName = userInfo.EmpName;
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

            return View(page, NewModel);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            ViewData["ProjIDs"] = string.Join(",", new DBSql.Bussiness.BusProjContractRelation().GetList(p => p.ConID == id).Select(p => p.ProjID.ToString()));
            ViewBag.permission = "['add','submit']";
            ViewBag.CreatorEmpId = model.CreatorEmpId;
            ViewBag.EmpId = userInfo.EmpID.ToString();
            ViewBag.EmpName = userInfo.EmpName.ToString();
            ViewBag.Time = DateTime.Now.ToString("yyyy-MM-dd");


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
            using (var tran = op.DbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (int item in id)
                    {
                        DataModel.Models.BussContract dmCon = op.Get(item);
                        if (dmCon == null) continue;
                        dmCon.MvcDefaultDel(this.userInfo);
                        op.DbContext.SaveChanges();
                        //删除费用
                        DBSql.Bussiness.BussFeePlan plan = new DBSql.Bussiness.BussFeePlan();
                        plan.DbContextRepository(op.DbContext);
                        DBSql.Bussiness.BussFeeFact fact = new DBSql.Bussiness.BussFeeFact();
                        fact.DbContextRepository(op.DbContext);
                        DBSql.Bussiness.BussFeeInvoice Invoice = new DBSql.Bussiness.BussFeeInvoice();
                        Invoice.DbContextRepository(op.DbContext);

                        plan.Edit((p => p.ConID == item), (x => new DataModel.Models.BussFeePlan() { DeleterEmpId = userInfo.EmpID, DeleterEmpName = userInfo.EmpName, DeletionTime = System.DateTime.Now }));
                        fact.Edit((p => p.ConID == item), (x => new DataModel.Models.BussFeeFact() { DeleterEmpId = userInfo.EmpID, DeleterEmpName = userInfo.EmpName, DeletionTime = System.DateTime.Now }));
                        Invoice.Edit((p => p.ConID == item), (x => new DataModel.Models.BussFeeInvoice() { DeleterEmpId = userInfo.EmpID, DeleterEmpName = userInfo.EmpName, DeletionTime = System.DateTime.Now }));
                        op.DbContext.SaveChanges();

                    }
                    new DBSql.PubFlow.Flow().Delete(id, "BussContract");
                    reuslt = 1;

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    reuslt = -1;
                }
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
            var model = new BussContract();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();
            //项目信息
            List<DataModel.Models.BusProjContractRelation> ProList = new List<BusProjContractRelation>();
            if (Request.Params["ProjTable_Data"] != null)
            {
                ProList = new JavaScriptSerializer().Deserialize<List<BusProjContractRelation>>(Request.Params["ProjTable_Data"].ToString());
            }

            int reuslt = 0;
            if (model.Id > 0)
            {
                model.MvcDefaultEdit(userInfo.EmpID);
                #region 客户单位保存
                DBSql.Bussiness.AddCustmer addModel = new DBSql.Bussiness.AddCustmer();
                addModel.CustName = model.CustName;
                addModel.CustLinkMan = model.CustLinkManName;
                addModel.CustLinkTel = model.CustLinkManTel;
                addModel.CustLinkMail = model.CustLinkManWeb;
                addModel.EmpModel = userInfo;
                int _custId = addModel.AddCust();
                model.CustID = _custId > 0 ? _custId : 0;
                #endregion

                reuslt = op.Edit(model, ProList);
            }
            else
            {
                List<DataModel.Models.BussFeePlan> planList = new List<BussFeePlan>();
                if (Request.Params["PlanTable_Data"] != null)
                {
                    planList = new JavaScriptSerializer().Deserialize<List<BussFeePlan>>(Request.Params["PlanTable_Data"].ToString());
                    foreach (var planModel in planList)
                    {
                        planModel.MvcDefaultSave(userInfo.EmpID);
                        planModel.ProjID = planModel.ProjID == -1 ? 0 : planModel.ProjID;
                    }
                }
                //扩展赋值
                model.CreateEmpName = userInfo.EmpName;
                model.CreateDate = System.DateTime.Now;
                //赋值默认值
                model.MvcDefaultSave(userInfo.EmpID);
                #region 客户单位保存
                DBSql.Bussiness.AddCustmer addModel = new DBSql.Bussiness.AddCustmer();
                addModel.CustName = model.CustName;
                addModel.CustLinkMan = model.CustLinkManName;
                addModel.CustLinkTel = model.CustLinkManTel;
                addModel.CustLinkMail = model.CustLinkManWeb;
                addModel.EmpModel = userInfo;
                int _custId = addModel.AddCust();
                model.CustID = _custId > 0 ? _custId : 0;
                #endregion

                reuslt = op.Add(model, planList, ProList);

                var ba = new DBSql.Sys.BaseAttach();
                ba.DbContextRepository(op.DbContext);
                ba.MoveFile(model.Id, userInfo.EmpID, userInfo.EmpName);
            }

            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }

        /// <summary>
        /// 设置是否结清
        /// </summary>
        /// <param name="ConItem"></param>
        /// <returns></returns>
        public JsonResult ChangeFeeStatus(FormCollection ConItem)
        {
            int reuslt = 0;
            if (ConItem["ConID"] != null)
            {
                var model = op.Get(Common.ExtensionMethods.Value<int>(ConItem["ConID"]));
                if (model != null)
                {
                    model.ConIsFeeFinished = Common.ExtensionMethods.Value<bool>(ConItem["ActType"]);
                    op.UnitOfWork.SaveChanges();
                    reuslt = model.Id;
                }
            }
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion


        public string GetConCodeCount(string ConNumber, int Id)
        {
            if (Id == 0)
            {
                ConNumber = ConNumber.Trim();
                //新增
                int Count = op.GetList(p => p.ConNumber == ConNumber && p.DeleterEmpId == 0).Count();
                return Count.ToString();
            }
            else
            {
                //修改
                int Count = op.GetList(p => p.ConNumber == ConNumber && p.DeleterEmpId == 0 && p.Id != Id).Count();
                return Count.ToString();
            }
        }
    }
}
