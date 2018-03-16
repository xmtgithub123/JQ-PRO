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
    [Description("BussFeeFact")]
    public class BussFeeFactController : CoreController
    {
        private DBSql.Bussiness.BussFeeFact op = new DBSql.Bussiness.BussFeeFact();
        private DBSql.Bussiness.BussContract opCon = new DBSql.Bussiness.BussContract();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("收款合同费用列表(分公司)")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ContractFee")));
            ViewBag.CreateEmpId = userInfo.EmpID;
            return View();
        }

        [Description("收款合同费用列表(设计公司)")]
        public ActionResult list_sj()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ContractFee_SJ")));
            ViewBag.CreateEmpId = userInfo.EmpID;
            return View();
        }

        [Description("收款合同费用列表(工程公司)")]
        public ActionResult list_gc()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ContractFee_GC")));
            ViewBag.CreateEmpId = userInfo.EmpID;
            return View();
        }

        [Description("收款合同费用列表(创景公司)")]
        public ActionResult list_cj()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ContractFee_CJ")));
            ViewBag.CreateEmpId = userInfo.EmpID;
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
                PageModel.SelectOrder = "FeeDate asc";
            }
            var TWhere = QueryBuild<DataModel.Models.BussFeeFact>.True();
            if (condition["ConID"] != null)
            {
                int Conid = Common.ExtensionMethods.Value<int>(condition["ConID"]);
                TWhere = TWhere.And(p => p.ConID == Conid);
            }
            TWhere = TWhere.And(p => p.DeleterEmpId == 0);
            var list = op.GetPagedList(TWhere, PageModel).ToList().Select(p => new
            {

                p.Id,
                p.ConID,
                ProjID = p.ProjID == 0 ? -1 : p.ProjID,
                p.FeeDate,
                p.FeeMoney,
                p.FeeNote,
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
        /// 登记收款费用时、combogrid查询使用
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public string JsonProject(FormCollection condition)
        {
            DBSql.Project.Project con = new DBSql.Project.Project();
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            var TWhere = QueryBuild<DataModel.Models.BusProjContractRelation>.True();
            if (condition["Filter"] != null && condition["Filter"] != "")
            {
                string strFilter = condition["Filter"].ToString();
                TWhere = TWhere.And(p => p.FK_BusProjContractRelation_ProjID.ProjName.Contains(strFilter) || p.FK_BusProjContractRelation_ProjID.ProjNumber.Contains(strFilter));
            }
            if (condition["ConID"] != null)
            {
                int ConID = Common.ExtensionMethods.Value<int>(condition["ConID"]);
                TWhere = TWhere.And(p => p.ConID == ConID);
            }
            TWhere = TWhere.And(p => p.FK_BusProjContractRelation_ProjID.DeleterEmpId == 0);
            var ProjList = (new DBSql.Bussiness.BusProjContractRelation().GetList(TWhere)).Select(p => new
            {
                ProjID = p.ProjID,
                ProjNumber = p.FK_BusProjContractRelation_ProjID.ProjNumber,
                ProjName = p.FK_BusProjContractRelation_ProjID.ProjName,
                Customer = p.FK_BusProjContractRelation_ProjID.CustName,
                p.ConFee,
            }).ToList();
            ProjList.Add(new { ProjID = 0, ProjNumber = "", ProjName = "未指定项目", Customer = "", ConFee = 0m });

            return JavaScriptSerializerUtil.getJson(ProjList.OrderBy(p => p.ProjID));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string jsonFact()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();

            string companyType = Request.Params["CompanyType"].ToString();
            switch (companyType)
            {
                case "SJ":
                    PageModel.SelectCondtion.Add("CompanyID", 1);
                    break;
                case "GC":
                    PageModel.SelectCondtion.Add("CompanyID", 2);
                    break;
                case "CJ":
                    PageModel.SelectCondtion.Add("CompanyID", 3);
                    break;
                default:
                    PageModel.SelectCondtion.Add("CompanyID", 0);
                    break;
            }

            //扩展列
            #region MyRegion
            string column = "";
            //column = ",(select Sum(ConFee) from BussContract act where (act.FatherID=c.Id or act.ID=c.Id)) as SumConFee";
            column += @",case BGConFee when 0 then ((select Sum(ConFee) from BussContract act where (act.FatherID=c.Id or act.ID=c.Id)))
                else (select Sum(ConFee) from BussContract act where act.FatherID=c.Id and ColAttVal1<>'1')+(select BGConFee from BussContract bc where bc.Id=c.Id)
                end as SumConFee";
            column += ",(select Sum(ConBalanceFee) from BussContract act where (act.FatherID=c.Id or act.ID=c.Id)) as SumConBalanceFee ";
            column += ",(select Sum(FeeMoney) from BussFeeFact bf where bf.ConID=c.Id) as FeeFact ";
            #endregion
            PageModel.SelectCondtion.Add("DeleterEmpId", "0");
            PageModel.SelectCondtion.Add("FormTable", "MustTable");
            PageModel.SelectOrder = "FormTableID desc,FeeDate asc";
            if (Request.Params["FormID"] != null)
            {
                PageModel.SelectCondtion.Add("FormTableID", Common.ExtensionMethods.Value<int>(Request.Params["FormID"]));
            }
            column += ",isnull(TableXml.value('(/root/Percent)[1]', 'nvarchar(max)'),'0') as Persent";
            column += ",isnull(TableXml.value('(/root/ConIsFeeFinished)[1]', 'nvarchar(max)'),'') as ConIsFeeFinished";

            PageModel.SelectCondtion.Add("OtherColumn", column);

            var perlist = PermissionList("ContractFee");
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

            DataTable dt = op.GetFeeFactList(PageModel);

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
            var model = new BussFeeFact();
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
            int reuslt = op.DelIsoForm(id, userInfo.EmpID);
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }

        public ActionResult delByIDs(FormCollection condition)
        {
            string ids = condition["ids"].ToString();
            int[] id = (from n in ids.Split(',') where n != "" select Common.ExtensionMethods.Value<int>(n)).ToArray();
            if (id.Length == 0) return Json(null);
            int reuslt = 0;
            //op.Delete(s => id.Contains(s.Id));
            foreach (int item in id)
            {
                //op.Delete(s => id.Contains(s.Id));
                DataModel.Models.BussFeeFact dmInvoice = op.Get(item);
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
            var model = new BussFeeFact();
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
                op.UnitOfWork.SaveChanges();
                reuslt = model.Id;
            }
            else
            {
                model.MvcDefaultSave(userInfo.EmpID);
                model.TableXml = "";
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
