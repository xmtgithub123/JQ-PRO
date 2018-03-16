using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using System;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.Web.Script.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Common.Data.Extenstions;
using System.Data;

namespace Bussiness.Controllers
{
    [Description("BussCustomer")]
    public class BussCustomerController : CoreController
    {
        private DBSql.Bussiness.BussCustomer op = new DBSql.Bussiness.BussCustomer();
        private DBSql.Sys.BaseData data = new DBSql.Sys.BaseData();
        private DBSql.Bussiness.BussCustomerLinkMan linkMan = new DBSql.Bussiness.BussCustomerLinkMan();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("CustomerInfo")));
            return View();
        }

        [Description("统计列表")]
        public ActionResult custstatisticslist()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("CustomerStatistics")));
            return View();
        }

        [Description("外委统计列表")]
        public ActionResult substisticslist()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("CustomerSubStatistics")));
            return View();
        }
        #endregion

        #region   外协客户列表信息
        [Description("外协客户列表信息")]
        public ActionResult sublist()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("CustomerSubInfo")));
            return View();
        }
        #endregion

        #region 其它客户单位

        [Description("其它客户列表信息")]
        public ActionResult otherlist()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("OtherCustomerInfo")));
            return View();
        }
        #endregion

        #region 外协客户列表json信息

        [Description("外协列表Json信息")]
        [HttpPost]
        public string sublistJson()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            var list = op.GetPagedList(p => p.TypeID == 1, PageModel).ToList();
            //展示需要的列信息
            var target = from item in list
                         let QualiGrade = data.Get(item.CustQualiGrade)
                         let custype = GetBaseName(item.CustTypeIDs)
                         select new
                         {
                             Id = item.Id,
                             //TypeName=GetSubTypeName(item.CustTypeIDs),
                             TypeName = string.Join(",", custype.ToArray()),
                             item.CustName,//外协客户名称
                             item.CustAddress,//注册地址
                             CustQualiGradeName = QualiGrade == null ? "" : QualiGrade.BaseName,//资质等级
                             item.CustBusinessCreateDate,//业务成立时间
                             LinkManCount = op.LinkManCount(item.Id),//联系人数量
                             BussCustomerEvaluateCount = op.EvaluateCount(item.Id),//客户评价数量
                             RecordCount = op.RecordCount(item.Id)//记事的数量

                         };



            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = target.ToList()
            });
        }
        #endregion

        #region 外委下拉列表数据
        public string dropListJson()
        {
            int PageNum = TypeHelper.parseInt(Request.Form["page"], 1);
            int Record = TypeHelper.parseInt(Request.Form["rows"], 10);
            var condition = TypeHelper.parseString(Request.Form["keyWord"]).Trim();
            int CustId = TypeHelper.parseInt(Request.QueryString["custId"]);
            object obj = null;
            var list = op.GetList(c => c.TypeID == 1&&c.Id!=CustId).ToList();//外委客户
            List<dynamic> dyList = new List<dynamic>();
            DataModel.Models.BussCustomer modelCust = op.Get(CustId);
            if(modelCust!=null)
            {
                obj = new {
                    modelCust.Id,
                    modelCust.CustName,
                    modelCust.CustAddress,
                    modelCust.CustChineseName,
                    modelCust.CustEmail,
                    modelCust.CustPost,
                    modelCust.CustBankName,
                    modelCust.CustBankAccount,
                    modelCust.CustBusinessCreateDate,
                    CustLinkManDepartMent = getLinkMan(modelCust.Id) == null ? "" : getLinkMan(modelCust.Id).CustLinkManDepartMent,//部门
                    CustLinkManJob = getLinkMan(modelCust.Id) == null ? "" : getLinkMan(modelCust.Id).CustLinkManJob,
                    CustLinkManName = getLinkMan(modelCust.Id) == null ? "" : getLinkMan(modelCust.Id).CustLinkManName,
                    CustLinkManTel = getLinkMan(modelCust.Id) == null ? "" : getLinkMan(modelCust.Id).CustLinkManTel

                };
                dyList.Add(obj);
            }
            var a = (from item in list
                     where item.CustName.Contains(condition)
                     orderby item.Id descending
                     select new
                     {
                         Id = item.Id,
                         CustName = item.CustName,//外委名称
                         CustAddress = item.CustAddress,//地址
                         CustChineseName = item.CustChineseName,//法人代表
                         CustEmail = item.CustEmail,//电子邮箱
                         CustPost = item.CustPost,
                         CustBankName = item.CustBankName,//开户行
                         CustBankAccount = item.CustBankAccount,//开户号
                         CustBusinessCreateDate = item.CustBusinessCreateDate,
                         CustLinkManDepartMent = getLinkMan(item.Id) == null ? "" : getLinkMan(item.Id).CustLinkManDepartMent,//部门
                         CustLinkManJob = getLinkMan(item.Id) == null ? "" : getLinkMan(item.Id).CustLinkManJob,
                         CustLinkManName = getLinkMan(item.Id) == null ? "" : getLinkMan(item.Id).CustLinkManName,
                         CustLinkManTel = getLinkMan(item.Id) == null ? "" : getLinkMan(item.Id).CustLinkManTel

                     }).ToList();
            dyList.AddRange(a);
            return JavaScriptSerializerUtil.getJson(new
            {
                total = dyList.Count,
                rows = (dyList.Skip(Record * (PageNum - 1)).Take(Record)).ToList()
            });

        }

        #endregion

        #region 客户与其他客户下拉列表信息
        [HttpPost]
        public string dropListCustmerJson()
        {
            int PageNum = TypeHelper.parseInt(Request.Form["page"], 1);
            int Record = TypeHelper.parseInt(Request.Form["rows"], 10);
            var condition = TypeHelper.parseString(Request.Form["keyWord"]).Trim();
            int custId = TypeHelper.parseInt(Request.Params["custId"]);
            var list = op.GetList(c => (c.TypeID == 0 || c.TypeID == 3)&&c.Id!=custId).ToList();//客户单位和其他客户单位
            object obj = null;
            List<dynamic> dyList = new List<dynamic>();
            DataModel.Models.BussCustomer modelCust = op.Get(custId);
            if(modelCust!=null)
            {
                obj = new
                {
                    modelCust.Id,
                    modelCust.CustType,
                    CustTypeName = modelCust.FK_BussCustomer_CustType.BaseName,//客户类别
                    modelCust.CustName,//客户名称
                    modelCust.CustAddress,//办公地址
                    CustRegion = modelCust.FK_BussCustomer_CustAddressID.BaseName,//客户地区
                    modelCust.CustPost,//客户邮编信息
                    modelCust.CustDate//业务建立时间
                };
                dyList.Add(obj);
            }
            var a = (from item in list
                     where item.CustName.Contains(condition)
                     orderby item.Id descending
                     select new
                     {
                         Id = item.Id,
                         item.CustType,//客户类别编号
                         CustTypeName = item.FK_BussCustomer_CustType.BaseName,//客户类别
                         item.CustName,//客户名称
                         item.CustAddress,//办公地址
                         CustRegion = item.FK_BussCustomer_CustAddressID.BaseName,//客户地区
                         item.CustPost,//客户邮编信息
                         item.CustDate//业务建立时间

                     }).ToList();
            dyList.Add(a);
            return JavaScriptSerializerUtil.getJson(new
            {
                total = dyList.Count,
                rows = (dyList.Skip(Record * (PageNum - 1)).Take(Record)).ToList()
            });

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
            int TypeID = TypeHelper.parseInt(Request.QueryString["TypeID"]);
            var list = op.GetPagedList(p => p.TypeID == TypeID, PageModel).ToList();
            //展示需要的列信息
            var target = from item in list
                         let CustDate= item.CustDate.Year==1900?"":item.CustDate.ToString("yyyy-MM-dd")
                         select new
                         {
                             Id = item.Id,
                             CustTypeId = item.FK_BussCustomer_CustType.BaseID,//客户类别编号
                             CustTypeName = item.FK_BussCustomer_CustType.BaseName,//客户类别
                             item.CustName,//客户名称
                             item.CustAddress,//办公地址
                             CustRegion = item.FK_BussCustomer_CustAddressID.BaseName,//客户地区
                             item.CustPost,//客户邮编信息
                             CustDate,//业务建立时间
                             LinkManCount = op.LinkManCount(item.Id),//联系人数量
                             BussCustomerEvaluateCount = op.EvaluateCount(item.Id),//客户评价数量
                             RecordCount = op.RecordCount(item.Id)//记事的数量
                         };



            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = target.ToList()
            });
        }
        #endregion

        #region  客户统计列表信息

        [Description("json")]
        [HttpPost]
        public string StatisticsListjson()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }

            string startTime = Request.Form["startTime"];
            string endTime = Request.Form["endTime"];
            string ConStatus = Request.Form["ConStatus"];
            if (!string.IsNullOrEmpty(startTime) && TypeHelper.isDateTime(startTime))
            {
                PageModel.SelectCondtion.Add("ConDateLower", startTime);

            }
            if (!string.IsNullOrEmpty(endTime) && TypeHelper.isDateTime(endTime))
            {
                PageModel.SelectCondtion.Add("ConDateUpper", endTime);
            }
            if (!string.IsNullOrEmpty(ConStatus) && ConStatus != "0")
            {
                PageModel.SelectCondtion.Add("ConStatus", ConStatus);
            }
            var list = op.GetPagedList(p => p.TypeID == 0, PageModel).ToList();
            //展示需要的列信息
            var target = from item in list
                         select new
                         {
                             Id = item.Id,
                             CustType = item.CustType,
                             CustName = item.CustName,
                             Fee = op.GetContractMoney(item.CustName, PageModel),
                             Num = op.GetContractCount(item.CustName, PageModel)
                         };



            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = target.ToList()
            });
        }
        #endregion



        #region  外委客户统计列表信息

        [Description("json")]
        [HttpPost]
        public string SubStatisticsListjson()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            string startTime = Request.Form["startTime"];
            string endTime = Request.Form["endTime"];
            string ConSubStatus = Request.Form["ConSubStatus"];
            if (!string.IsNullOrEmpty(startTime) && TypeHelper.isDateTime(startTime))
            {
                PageModel.SelectCondtion.Add("ConSubDateLower", startTime);
            }
            if (!string.IsNullOrEmpty(endTime) && TypeHelper.isDateTime(endTime))
            {
                PageModel.SelectCondtion.Add("ConSubDateUpper", endTime);
            }
            if (!string.IsNullOrEmpty(ConSubStatus) && ConSubStatus != "0")
            {

                PageModel.SelectCondtion.Add("ConSubStatus", ConSubStatus);
            }
            var list = op.GetPagedList(p => p.TypeID == 1, PageModel).ToList();
            //展示需要的列信息
            var target = from item in list
                         select new
                         {
                             Id = item.Id,
                             CustName = item.CustName,
                             Fee = op.GetContractSubMoney(item.Id, PageModel),
                             Num = op.GetContractSubCount(item.Id, PageModel)
                         };



            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = target.ToList()
            });
        }
        #endregion

        ///////     TypeID=0  客户
        //////      TypeID=1  外委       TypeID=3 其他客户

        public string GetJson()
        {
            int PageNum = TypeHelper.parseInt(Request.Form["page"]);
            int Record = TypeHelper.parseInt(Request.Form["rows"]);
            var condition = TypeHelper.parseString(Request.Form["keyword"]).Trim();
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            var list = op.GetQuery().ToList();

            List<DataModel.Models.BussCustomer> result = (from item in list
                                                          where item.CustName.Contains(condition)    //条件查询
                                                          select item).ToList<DataModel.Models.BussCustomer>();

            var a = (from item in result
                     select new
                     {
                         item.Id,
                         item.CustName,
                         item.CustAddress,
                         item.CustBankName,
                         item.CustBankAccount
                     }).ToList();
            var t = (a.Skip(Record * (PageNum - 1)).Take(Record)).ToList();
            return JavaScriptSerializerUtil.getJson(new
            {
                total = a.Count,
                rows = (a.Skip(Record * (PageNum - 1)).Take(Record)).ToList()
            });

        }

        //public ActionResult PermitTopjson(int OrderLen = 2)
        //{
        //    var list = menu.AllData;

        //    if (OrderLen != 0)
        //    {
        //        list = list.Where(s => s.MenuOrder.Length == OrderLen);
        //    }

        //    return Json(list.Select(s => new
        //    {
        //        s.MenuID,
        //        s.MenuName,
        //        s.MenuOrder,
        //        s.MenuNameEng
        //    }).OrderBy(s => s.MenuOrder).ToList());
        //}
        public JsonResult GetCustListForProject()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id";
            }
            int TypeID = TypeHelper.parseInt(Request.Params["TypeID"]);
            var list = new DBSql.Bussiness.BussCustomer().GetPagedDynamic(PageModel).ToList();
            if (TypeID>3)
            {
                  list = new DBSql.Bussiness.BussCustomer().GetPagedDynamic(PageModel, c => c.DeleterEmpId == 0).ToList();
            }
            else
            {
                 list = new DBSql.Bussiness.BussCustomer().GetPagedDynamic(PageModel, c => c.TypeID == TypeID).ToList();
            }
            //var list = new DBSql.Bussiness.BussCustomer().GetPagedDynamic(PageModel, c => c.TypeID == TypeID).ToList();
            return Json(new { total = PageModel.PageTotleRowCount, rows = list });
        }

        /// <summary>
        /// 外委客户
        /// </summary>
        /// <returns></returns>
        public string GetCustomerSubJson()
        {
            int PageNum = TypeHelper.parseInt(Request.Form["page"]);
            int Record = TypeHelper.parseInt(Request.Form["rows"]);
            var condition = TypeHelper.parseString(Request.Form["keyword"]).Trim();

            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            var list = op.GetList(c => c.TypeID == 1).ToList();

            List<DataModel.Models.BussCustomer> result = (from item in list
                                                          where item.CustName.Contains(condition)    //条件查询
                                                          select item).ToList<DataModel.Models.BussCustomer>();

            var a = (from item in result
                     select new
                     {
                         item.Id,
                         item.CustName,
                         item.CustAddress
                     }).ToList();
            var t = (a.Skip(Record * (PageNum - 1)).Take(Record)).ToList();
            return JavaScriptSerializerUtil.getJson(new
            {
                total = a.Count,
                rows = (a.Skip(Record * (PageNum - 1)).Take(Record)).ToList()
            });

        }


        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new BussCustomer();
            model.TypeID = 0;//客户单位
            ViewBag.permission = "['add','submit']";
            return View("info", model);
        }
        #endregion

        #region 添加外委客户
        [Description("添加外委客户")]
        public ActionResult addsub()
        {
            var model = new BussCustomer();
            model.TypeID = 1;//外委客户单位
            ViewBag.permission = "['add','submit']";
            return View("subinfo", model);
        }
        #endregion

        #region 添加其它单位
        [Description("添加其它单位")]

        public ActionResult addother()
        {
            var model = new BussCustomer();
            model.TypeID = 3;//其它客户单位
            ViewBag.permission = "['add','submit']";
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            string views = "info";
            if (model.TypeID == 1)
            {
                views = "subinfo";
            }
            ViewBag.permission = "['add','submit']";
            return View(views, model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 0;

            //验证是否有被使用过，如果有则不能删除该客户

            DoResult dresult;
            List<int> list = id.ToList();

            DataTable dt = op.GetUserCustomer(string.Join(",", id));

            bool IsNotDelete = false;
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToInt32(dr["custCount"]) != 0)
                {
                    IsNotDelete = true;
                }
            }

            if (IsNotDelete)
            {
                dresult = DoResultHelper.doSuccess(reuslt, "操作失败,有客户已经使用，不能删除");
            }
            else
            {

                reuslt = op.DeleteCustomerInfo(list);//删除当前信息并删除子信息
                dresult = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            }
            return Json(dresult);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id)
        {
            var model = new BussCustomer();
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
            using (var ba = new DBSql.Sys.BaseAttach())
            {
                ba.MoveFile(model.Id, userInfo.EmpID, userInfo.EmpName);
            }

            reuslt = model.Id;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion
        /// <summary>
        /// /根据IDS查找外委类型名称
        /// </summary>
        /// <param name="IDS"></param>
        /// <returns></returns>
        private string GetSubTypeName(string IDS)
        {
            if (string.IsNullOrEmpty(IDS.Trim()))
                return string.Empty;
            System.Text.StringBuilder stb = new System.Text.StringBuilder();
            string[] ID = IDS.Split(',');
            for (int countIndex = 0; countIndex < ID.Length; countIndex++)
            {
                if (!string.IsNullOrEmpty(ID[countIndex]))
                {
                    DataModel.Models.BaseData basedata = data.Get(int.Parse(ID[countIndex]));
                    if (basedata != null)
                    {
                        stb.Append(basedata.BaseName + ",");

                    }
                }
            }
            return stb.ToString().Trim(',');
        }

        /// <summary>
        /// 联系人相关信息
        /// </summary>
        /// <param name="custID"></param>
        /// <returns></returns>
        private DataModel.Models.BussCustomerLinkMan getLinkMan(int custID)
        {
            return linkMan.FirstOrDefault(p => p.CustID == custID);
        }
        public string GetCustomerList()
        {
            //Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            //if (!PageModel.SelectOrder.isNotEmpty())
            //{
            //    PageModel.SelectOrder = "Id";
            //}
            //int TypeID = TypeHelper.parseInt(Request.Params["TypeID"]);
            //int CustID = TypeHelper.parseInt(Request.Params["CustID"]);
            ////List<dynamic> dyList = new List<dynamic>();
            //dynamic selectCust = op.GetPagedDynamic(PageModel, p=>p.Id==CustID).FirstOrDefault();
            //var list = op.GetPagedDynamic(PageModel, p => p.TypeID == TypeID && p.Id > 0&&p.Id!=CustID).ToList();
            //int total = 0;
            //if(selectCust==null)
            //{
            //    total = PageModel.PageTotleRowCount;
            //}
            //else
            //{
            //    dyList.Add(selectCust);
            //    total = PageModel.PageTotleRowCount + 1;
            //}
            //dyList.AddRange(list);
            //return Json(new { total = total, rows = dyList });

            int PageNum = TypeHelper.parseInt(Request.Form["page"], 1);
            int Record = TypeHelper.parseInt(Request.Form["rows"], 10);
            var condition = TypeHelper.parseString(Request.Form["keyWord"]).Trim();
            int TypeID = TypeHelper.parseInt(Request.Params["TypeID"]);
            int CustID = TypeHelper.parseInt(Request.Params["CustID"]);
            object obj = null;
            var list = op.GetList(c => c.TypeID == TypeID && c.Id != CustID).ToList();//外委客户
            List<dynamic> dyList = new List<dynamic>();
            DataModel.Models.BussCustomer modelCust = op.Get(CustID);
            if (modelCust != null)
            {
                obj = new
                {
                    modelCust.Id,
                    modelCust.CustName,
                    modelCust.CustAddress,
                    modelCust.CustChineseName,
                    modelCust.CustEmail,
                    modelCust.CustPost,
                    modelCust.CustBankName,
                    modelCust.CustBankAccount,
                    modelCust.CustBusinessCreateDate

                };
                dyList.Add(obj);
            }
            var a = (from item in list
                     where item.CustName.Contains(condition)
                     orderby item.Id descending
                     select new
                     {
                         Id = item.Id,
                         CustName = item.CustName,
                         CustAddress = item.CustAddress,
                         CustChineseName = item.CustChineseName,
                         CustEmail = item.CustEmail,//
                         CustPost = item.CustPost,
                         CustBankName = item.CustBankName,
                         CustBankAccount = item.CustBankAccount,
                         CustBusinessCreateDate = item.CustBusinessCreateDate
                     }).ToList();
            dyList.AddRange(a);
            return JavaScriptSerializerUtil.getJson(new
            {
                total = dyList.Count,
                rows = (dyList.Skip(Record * (PageNum - 1)).Take(Record)).ToList()
            });

        }
        public string GetCustomerListCombobox()
        {
            var list = op.GetList(p => p.Id != 0 && p.TypeID == 0).Select(p => new { Id = p.Id, CustName = p.CustName }).ToList();
            return JavaScriptSerializerUtil.getJson(list);
        }

        public string GetSubCustomerListCombobox()
        {
            var list = op.GetList(p => p.Id != 0 && p.TypeID == 1).Select(p => new { Id = p.Id, CustName = p.CustName }).ToList();
            return JavaScriptSerializerUtil.getJson(list);
        }

        public string GetAllCustomerListCombobox()
        {
            var list = op.GetList(p => p.Id != 0).Select(p => new { Id = p.Id, CustName = p.CustName, CustBankName=p.CustBankName, CustBankAccount=p.CustBankAccount }).ToList();
            return JavaScriptSerializerUtil.getJson(list);
        }

        /// <summary>
        ///  检测客户是否存在
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public string CheckCustomerExists(FormCollection form)
        {
            int Count = 0;
            int TypeID = TypeHelper.parseInt(Request.Params["TypeID"]);//客户类型
            int Id = TypeHelper.parseInt(Request.Params["Id"]);
            string CustName = Request.Params["CustName"].Trim();
            if(Id==0)//新增客户
            {
                Count = op.GetList(p=>p.CustName.Trim()==CustName&&p.TypeID==TypeID).Count();
            }
            else//修改客户
            {
                Count = op.GetList(p => p.CustName == CustName && p.TypeID == TypeID&&p.Id!=Id).Count();
            }
            return Count.ToString();
        }

        public string GetCustomerListComboboxWithCount(int TopCount)
        {
            string DefaultCustName = "";
            var Twhere = QueryBuild<BussCustomer>.True();

            if (Request.Params["TypeID"] != null)
            {
                int TypeID = Common.ExtensionMethods.Value<int>(Request.Params["TypeID"]);
                Twhere = Twhere.And(p => p.Id != 0 && p.TypeID == TypeID);
            }
            else
            {
                Twhere = Twhere.And(p => p.Id != 0 && p.TypeID == 0);
            }

            if (Request.Params["q"] != null && Request.Params["q"].ToString().Trim() != "")
            {
                string textField = Server.HtmlDecode(Request.Params["q"].ToString().Trim());
                Twhere = Twhere.And(p => p.CustName.Contains(textField));
            }
            if (Request.Params["DefaultCustName"] != null)
            {
                DefaultCustName = Common.ExtensionMethods.Value<string>(Request.Params["DefaultCustName"]).Trim();
                if (DefaultCustName != "")
                {
                    Twhere = Twhere.Or(p => p.CustName == DefaultCustName);
                }
            }
            if (Request.Params["DefaultCustID"] != null)
            {
                #region 
                int CustID = Common.ExtensionMethods.Value<int>(Request.Params["DefaultCustID"]);
                if (CustID != 0)
                {
                    Twhere = Twhere.Or(p => p.Id == CustID);
                }
                var _list = op.GetList(Twhere).Select(p => new { Id = p.Id, CustName = p.CustName }).OrderBy(p => p.Id == CustID ? 0 : 1).Take(TopCount).ToList();
                return JavaScriptSerializerUtil.getJson(_list);
                #endregion
            }

            var list = op.GetList(Twhere).Select(p => new { Id = p.Id, CustName = p.CustName }).OrderBy(p => p.CustName.Contains(DefaultCustName) ? 0 : 1).Take(TopCount).ToList();
            return JavaScriptSerializerUtil.getJson(list);
        }
    }
}
