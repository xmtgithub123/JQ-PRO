using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using Common.Data.Extenstions;
using System.Data;

namespace Bussiness.Controllers
{
    [Description("BussFeeInvoice")]
    public class BussFeeInvoiceController : CoreController
    {
        private DBSql.Bussiness.BussFeeInvoice op = new DBSql.Bussiness.BussFeeInvoice();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("收款合同费用-开票(分公司)")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ContractFee")));
            ViewBag.CurrentEmpID = userInfo.EmpID;
            return View();
        }

        [Description("收款合同费用-开票(设计公司)")]
        public ActionResult list_sj()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ContractFee_SJ")));
            ViewBag.CurrentEmpID = userInfo.EmpID;
            return View();
        }

        [Description("收款合同费用-开票(工程公司)")]
        public ActionResult list_gc()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ContractFee_GC")));
            ViewBag.CurrentEmpID = userInfo.EmpID;
            return View();
        }

        [Description("收款合同费用-开票(创景公司)")]
        public ActionResult list_cj()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ContractFee_CJ")));
            ViewBag.CurrentEmpID = userInfo.EmpID;
            return View();
        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string json(FormCollection condition)
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();

            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "InvoiceDate asc";
            }
            var TWhere = QueryBuild<DataModel.Models.BussFeeInvoice>.True();
            if (condition["ConID"] != null)
            {
                int Conid = Common.ExtensionMethods.Value<int>(condition["ConID"]);
                TWhere = TWhere.And(p => p.ConID == Conid);
            }
            if (condition["IsFlow"] != null)
            {
                if (condition["IsFlow"].ToString() == "false")
                {
                    TWhere = TWhere.And(p => p.FlowID >= 0);
                }
            }

            TWhere = TWhere.And(p => p.DeleterEmpId == 0);
            var list = op.GetPagedList(TWhere, PageModel).ToList().Select(p => new
            {
                p.Id,
                p.ConID,
                ProjID = p.ProjID == 0 ? -1 : p.ProjID,
                p.InvoiceDate,
                p.InvoiceMoney,
                p.InvoiceNote,
                p.CreatorEmpName,
                text = p.ProjID == 0 ? "未指定项目" : ((new DBSql.Project.Project().Get(p.ProjID)) == null ? "" : new DBSql.Project.Project().Get(p.ProjID).ProjName),
            });


            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string jsonInvoice()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();

            string companyType = Request.Params["CompanyType"].ToString();
            var perlist = PermissionList("ContractFee");

            switch (companyType)
            {
                case "SJ":
                    PageModel.SelectCondtion.Add("CompanyID", 1);
                    PageModel.SelectCondtion.Add("RefTable", "BussFeeInvoiceISO_SJ");
                    perlist = PermissionList("ContractFee_SJ");
                    break;
                case "GC":
                    PageModel.SelectCondtion.Add("CompanyID", 2);
                    PageModel.SelectCondtion.Add("RefTable", "BussFeeInvoiceISO_GC");
                    perlist = PermissionList("ContractFee_GC");
                    break;
                case "CJ":
                    PageModel.SelectCondtion.Add("CompanyID", 3);
                    PageModel.SelectCondtion.Add("RefTable", "BussFeeInvoiceISO_CJ");
                    perlist = PermissionList("ContractFee_CJ");
                    break;
                default:
                    PageModel.SelectCondtion.Add("CompanyID", 0);
                    PageModel.SelectCondtion.Add("RefTable", "BussFeeInvoiceISO");
                    perlist = PermissionList("ContractFee");
                    break;
            }


            //扩展列
            #region MyRegion
            string column = "";
            //column =   ",(select Sum(ConFee) from BussContract act where (act.FatherID=c.Id or act.ID=c.Id)) as SumConFee";
            column += @",case BGConFee when 0 then ((select Sum(ConFee) from BussContract act where (act.FatherID=c.Id or act.ID=c.Id)))
                else (select Sum(ConFee) from BussContract act where act.FatherID=c.Id and ColAttVal1<>'1')+(select BGConFee from BussContract bc where bc.Id=c.Id)
                end as SumConFee";
            column += ",(select Sum(ConBalanceFee) from BussContract act where (act.FatherID=c.Id or act.ID=c.Id)) as SumConBalanceFee ";
            column += ",(select Sum(InvoiceMoney) from BussFeeInvoice bf where bf.ConID=c.Id) as FeeInvoice ";
            column += ",TableXml.value('(/root/Persent)[1]', 'nvarchar(max)') as Persent";
            #endregion
            PageModel.SelectCondtion.Add("DeleterEmpId", "0");
            PageModel.SelectCondtion.Add("ISODeleterEmpId", "0");

            PageModel.SelectCondtion.Add("FormTable", "MustTable");
            PageModel.SelectOrder = "FormTableID desc,InvoiceDate asc";
            if (Request.Params["FormID"] != null)
            {
                PageModel.SelectCondtion.Add("FormTableID", Common.ExtensionMethods.Value<int>(Request.Params["FormID"]));
            }
            else
            {
                if (!perlist.Contains("allview"))
                {
                    if (perlist.Contains("dep"))
                    {
                        PageModel.SelectCondtion.Add("CreatorDepId", userInfo.EmpDepID);
                    }
                    else
                    {
                        PageModel.SelectCondtion.Add("CreatorEmpId", userInfo.EmpID);
                    }

                }
            }
            PageModel.SelectCondtion.Add("OtherColumn", column);
            

            DataTable dt = op.GetFeeInvoiceList(PageModel);

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
            var model = new BussFeeInvoice();
            model.InvoiceDate = System.DateTime.Now;
            model.CreationTime = System.DateTime.Now;
            model.CreatorEmpName = userInfo.EmpName;
            ViewBag.permission = "['add','submit']";

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
            var model = op.Get(id);
            ViewBag.permission = "['add','submit']";

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
            reuslt = op.DelIsoForm(id, userInfo.EmpID);
            new DBSql.PubFlow.Flow().Delete(id, "BussFeeInvoiceISO");

            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }

        public ActionResult delByIDs(FormCollection condition)
        {
            string ids = condition["ids"].ToString();
            int[] id = (from n in ids.Split(',') where n != "" select Common.ExtensionMethods.Value<int>(n)).ToArray();
            if (id.Length == 0) return Json(null);
            int reuslt = 0;
            foreach (int item in id)
            {
                //op.Delete(s => id.Contains(s.Id));
                DataModel.Models.BussFeeInvoice dmInvoice = op.Get(item);
                dmInvoice.MvcDefaultDel(userInfo.EmpID);
            }         
            op.UnitOfWork.SaveChanges();
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id)
        {
            var model = new BussFeeInvoice();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();
            model.ProjID = model.ProjID == -1 ? 0 : model.ProjID;
            int reuslt = 0;
            if (model.Id > 0)
            {
                op.Edit(model);
                reuslt = model.Id;
                op.UnitOfWork.SaveChanges();
            }
            else
            {
                model.MvcDefaultSave(userInfo.EmpID);
                op.Add(model);
                op.UnitOfWork.SaveChanges();
                reuslt = model.Id;
            }

            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion
    }
}
