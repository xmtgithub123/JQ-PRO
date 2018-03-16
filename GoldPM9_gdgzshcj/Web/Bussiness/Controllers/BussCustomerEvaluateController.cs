using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;
using System;

namespace Bussiness.Controllers
{
    [Description("BussCustomerEvaluate")]
    public class BussCustomerEvaluateController : CoreController
    {
        private DBSql.Bussiness.BussCustomerEvaluate op = new DBSql.Bussiness.BussCustomerEvaluate();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        private DBSql.Bussiness.BussCustomer cust = new DBSql.Bussiness.BussCustomer();
        private DBSql.Project.Project pro = new DBSql.Project.Project();
        private DBSql.Sys.BaseData data = new DBSql.Sys.BaseData();
        private static int custID = 0;
        private DBSql.Bussiness.BussCustomerLinkMan linkMan = new DBSql.Bussiness.BussCustomerLinkMan();
        private DBSql.Bussiness.BussCustomerEvaluateDetail bussdetail = new DBSql.Bussiness.BussCustomerEvaluateDetail();
   
        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("CustomerInfo")));//权限与客户权限一致
            return View();
        }
        #endregion

        #region  外委评价列表
        [Description("外委评价列表信息")]
        public ActionResult sublist()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("CustomerSubInfo")));
            return View();
        }
        #endregion

        #region  外委评价json
        [HttpPost]
        public string subjson()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            int CustID = int.Parse(Request.QueryString["CustID"]);
            custID = CustID;
            var list = op.GetPagedList(p => p.CustID == custID, PageModel).ToList();
            var targetList = from item in list
                             select new
                             {
                                 Id = item.Id,
                                 CustName = cust.Get(item.CustID) == null ? "" : cust.Get(item.CustID).CustName,//外委名称
                                 CustAddress = cust.Get(item.CustID) == null ? "" : cust.Get(item.CustID).CustAddress,//地址
                                 CustPost = cust.Get(item.CustID) == null ? "" : cust.Get(item.CustID).CustPost,
                                 CustChineseName = cust.Get(item.CustID) == null ? "" : cust.Get(item.CustID).CustChineseName,//法人代表
                                 CustEmail = cust.Get(item.CustID) == null ? "" : cust.Get(item.CustID).CustEmail,//电子邮箱
                                 CustBankName = cust.Get(item.CustID) == null ? "" : cust.Get(item.CustID).CustBankName,//开户行
                                 CustBankAccount = cust.Get(item.CustID) == null ? "" : cust.Get(item.CustID).CustBankAccount,//开户号
                                 CustBusinessCreateDate = cust.Get(item.CustID) == null ? "" : cust.Get(item.CustID).CustBusinessCreateDate.ToString("yyyy-MM-dd"),
                                 CustLinkManDepartMent = getLinkMan(item.CustID) == null ? "" : getLinkMan(item.CustID).CustLinkManDepartMent,//部门
                                 CustLinkManJob = getLinkMan(item.CustID) == null ? "" : getLinkMan(item.CustID).CustLinkManJob,
                                 CustLinkManName = getLinkMan(item.CustID) == null ? "" : getLinkMan(item.CustID).CustLinkManName,
                                 CustLinkManTel = getLinkMan(item.CustID) == null ? "" : getLinkMan(item.CustID).CustLinkManTel,
                                 item.EvaluaterNote
                             };
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = targetList
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
            int CustID = int.Parse(Request.QueryString["CustID"]);
            custID = CustID;
            var list = op.GetPagedList(p => p.CustID == custID, PageModel).ToList();
            var targetList = from item in list
                             select new
                             {
                                 Id = item.Id,
                                 CustName = cust.Get(item.CustID) == null ? "" : cust.Get(item.CustID).CustName,
                                 CustTypeName = CustTypeName(item.CustID),
                                 ProjNumber = pro.Get(item.ProjID) == null ? "" : pro.Get(item.ProjID).ProjNumber,
                                 ProjName = ProjName(item.ProjID),
                                 EvalateDate = item.CreationTime,
                                 EvaluateResult = data.Get(item.EvaluationTypeID) == null ? "" : data.Get(item.EvaluationTypeID).BaseName,
                                 item.EvaluaterNote

                             };
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = targetList
            });
        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new BussCustomerEvaluate();
            model.CustID = custID;
            model.CreationTime = System.DateTime.Now;
            ViewBag.CustTypeName = CustTypeName(custID);
            ViewBag.ProjName = ProjName(model.ProjID);
            return View("info", model);
        }
        #endregion
        [Description("添加外委评分表信息")]
        public ActionResult addsub()
        {
            var model = new BussCustomerEvaluate();
            model.CustID = custID;
            BindInfo(model.CustID);
            return View("subinfo", model);
        }

        [Description("编辑外委评分信息")]
        public ActionResult editsub(int id)
        {
            var model = op.Get(id);
            BindInfo(model.CustID);//绑定其它信息
            return View("subinfo", model);
        }

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            ViewBag.CustTypeName = CustTypeName(model.CustID);
            ViewBag.ProjName = ProjName(model.ProjID);
            if (model.ProjID == 0)
            {
                ViewBag.Pro = "";
            }
            else
            {
                ViewBag.Pro = model.ProjID.ToString();
            }
            if(!string.IsNullOrEmpty(Request.Params["Read"]))
            {
                ViewBag.Read = 1;
            }
            return View("info", model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 0;
            reuslt=op.DeleteInfo(id.ToList());
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id)
        {
            try
            {
                var model = new BussCustomerEvaluate();
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
                    model.CustID = custID;//绑定传过来的值
                    op.Add(model);

                }
                op.UnitOfWork.SaveChanges();
                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.MoveFile(model.Id, userInfo.EmpID, userInfo.EmpName);
                }
                List<string> DepEmpList = model.ReceiveEmpIDs.Split(',').ToList();
                List<int> EmpList = new List<int>();
                foreach (string Emp in DepEmpList)
                {
                    int EmpID = TypeHelper.parseInt(Emp.Split('-')[0]);//获取人员信息
                    if (EmpID != 0)
                    {
                        if (!EmpList.Contains(EmpID))
                        {
                            EmpList.Add(EmpID);
                        }
                    }
                }
                op.SendEvaluateInfo(model, EmpList, userInfo);
                reuslt = model.Id;
                doResult = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            }
            catch(Exception ex)
            {
                doResult = DoResultHelper.doError(ex.Message);
            }
            return Json(doResult);
        }
        #endregion

        #region  保存外委评分信息以及详细评分
        [Description("保存外委评分信息以及详细评分")]
        [HttpPost]
        public ActionResult savesub(int Id)

        {
            var model = new BussCustomerEvaluate();
            var evaluate = new BussCustomerEvaluateDetail();
            var link = new BussCustomerLinkMan();
            var customer = new BussCustomer();
            string CustLinkManName = Request.Form["CustLinkManName"];//联系人姓名  
            string JsonRows= Request.Form["JsonRows"];
            List <DBSql.Bussiness.EvaluteDetailJson> EvalList = new List<DBSql.Bussiness.EvaluteDetailJson>();
            List<DataModel.Models.BussCustomerEvaluateDetail> detailList = new List<BussCustomerEvaluateDetail>();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();

            int reuslt = 0;
            if (model.Id > 0)
            {
                model.MvcDefaultEdit(userInfo.EmpID);
                op.Edit(model);
            }
            else
            {
                model.MvcDefaultSave(userInfo.EmpID);
                op.Add(model);

            }
            op.UnitOfWork.SaveChanges();
            if (JsonRows != null&&JsonRows!="")
            {
                EvalList = new JavaScriptSerializer().Deserialize<List<DBSql.Bussiness.EvaluteDetailJson>>(JsonRows);
                foreach(var m in EvalList)
                {
                    DataModel.Models.BussCustomerEvaluateDetail detail = new BussCustomerEvaluateDetail();
                    detail.Id = m.Id;
                    detail.BussCustomerEvaluateID = model.Id;
                    detail.EvaluateEmpId = m.EmpId;
                    detail.EvaluateDeptId = m.DeptId;
                    detail.EvaluateDate = m.EvaluateDate;
                    detail.EvaluateIdea = m.EvaluateIdea;
                    detailList.Add(detail);
                }
                List<int> detailID = detailList.Where(p => p.Id != 0).Select(p => p.Id).ToList();
                bussdetail.Delete(p=>!detailID.Contains(p.Id)&&p.BussCustomerEvaluateID==model.Id);
                foreach (var m in detailList)
                {
                    
                   
                    if (m.Id>0)
                    {
                        bussdetail.Edit(m);
                        
                    }
                    else
                    {
                        
                        bussdetail.Add(m);
                    }
                   
                }
                bussdetail.UnitOfWork.SaveChanges();
            }

            //判断当前外委单位下的联系人是否存在
            if (linkMan.GetList(p => p.CustID == model.CustID && p.CustLinkManName.Contains(CustLinkManName)).Count()>0)
            {
                link = linkMan.GetList(p => p.CustID == model.CustID && p.CustLinkManName.Contains(CustLinkManName)).FirstOrDefault();
            }
            link.MvcSetValueExceptKeys("Id");//排除不赋值的字段
            if (link.Id > 0)
            {
                linkMan.Edit(link);
            }
            else
            {
                linkMan.Add(link);
            }
            customer = cust.Get(model.CustID);
            //保存客户的基本信息
            customer.MvcSetValueExceptKeys("Id");
            if(model.CustID>0)
            {
                cust.Edit(customer);
            }

            
            cust.UnitOfWork.SaveChanges();
            linkMan.UnitOfWork.SaveChanges();
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
        /// 绑定客户类型
        /// </summary>
        /// <param name="CustID"></param>
        /// <returns></returns>
        public string CustTypeName(int CustID)
        {
            string custTypeName = string.Empty;
            DataModel.Models.BussCustomer cus = cust.Get(CustID);
            if (cus != null)
            {
                if (cus.FK_BussCustomer_CustType != null)
                {
                    custTypeName = cus.FK_BussCustomer_CustType.BaseName;
                }
            }
            return custTypeName;
        }

        #region  根据人员查找部门
        public JsonResult GetDeptName()
        {
            int EmpId = TypeHelper.parseInt(Request.Params["EmpId"]);
            string DeptName = string.Empty;
            int DeptId = 0;
            if (EmpId != 0)
            {
                DataModel.Models.AllBaseEmployee emp = new DBSql.Sys.AllBaseEmployee().FirstOrDefault(p => p.EmpID == EmpId);
                if (emp != null)
                {
                    DeptName = emp.DepartmentName;
                    DeptId = emp.DepartmentID;
                }
            }
            return Json(new { DeptName, DeptId });

        }
        #endregion


        public string ProjName(int ProjID)
        {
            string projName = string.Empty;
            DataModel.Models.Project proj = pro.Get(ProjID);
            if (proj != null)
            {
                projName = proj.ProjName;
            }
            return projName;
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
        /// <summary>
        /// 绑定信息
        /// </summary>
        /// <param name="CustID"></param>
        private void BindInfo(int CustID)
        {
            DataModel.Models.BussCustomer customer = cust.Get(CustID);
            if (customer != null)
            {
                ViewBag.CustAddress = customer.CustAddress;
                ViewBag.CustPost = customer.CustPost;
                ViewBag.CustChineseName = customer.CustChineseName;
                ViewBag.CustEmail = customer.CustEmail;
                ViewBag.CustBankName = customer.CustBankName;
                ViewBag.CustBankAccount = customer.CustBankAccount;
                ViewBag.CustBusinessCreateDate = customer.CustBusinessCreateDate;
            }
            DataModel.Models.BussCustomerLinkMan linkMan = getLinkMan(CustID);
            if (linkMan != null)
            {
                ViewBag.CustLinkManName = linkMan.CustLinkManName;
                ViewBag.CustLinkManJob = linkMan.CustLinkManJob;
                ViewBag.CustLinkManTel = linkMan.CustLinkManTel;
                ViewBag.CustLinkManDepartMent = linkMan.CustLinkManDepartMent;
            }
        }
    }
}
