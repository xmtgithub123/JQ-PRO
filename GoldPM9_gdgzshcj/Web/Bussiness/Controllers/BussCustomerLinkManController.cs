using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
namespace Bussiness.Controllers
{
    [Description("BussCustomerLinkMan")]
    public class BussCustomerLinkManController : CoreController
    {
        private DBSql.Bussiness.BussCustomerLinkMan op = new DBSql.Bussiness.BussCustomerLinkMan();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        private static int CustLinkManType = 0;


        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("CustomerInfo")));//联系人权限与客户权限一致
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
            string CustID = Request.QueryString["CustID"];
            CustLinkManType = TypeHelper.parseInt(Request.QueryString["CustLinkManType"]);
            var list = op.GetPagedList(p => p.CustLinkManType == CustLinkManType, PageModel).ToList();
            if (!string.IsNullOrEmpty(CustID))
            {
                int custID = int.Parse(CustID);
                list = op.GetPagedList(p => p.CustID == custID, PageModel).ToList();
            }

            var target = from item in list
                         select new
                         {
                             item.Id,
                             item.CustLinkManName,
                             item.CustLinkManSex,
                             item.CustLinkManDepartMent,
                             item.CustLinkManTel,
                             item.CustLinkManWeb,
                             item.CustLinkManJob,
                             item.CustLinkManNote,
                             CustName=item.FK_BussCustomerLinkMan_CustID==null?"": item.FK_BussCustomerLinkMan_CustID.CustName

                         };
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
            var model = new BussCustomerLinkMan();
            model.CustLinkManType = CustLinkManType;
            int CustID = TypeHelper.parseInt(Request.QueryString["CustID"]);
            model.CustID = CustID;
            ViewBag.Gender = SexList();
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            ViewBag.Gender = SexList();
            var model = op.Get(id);
            return View("info", model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 0;
            op.Delete(s => id.Contains(s.Id));
            op.UnitOfWork.SaveChanges();
            reuslt++;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id)
        {
            var model = new BussCustomerLinkMan();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();

            int reuslt = 0;
            if (model.Id > 0)
            {
                model.MvcDefaultEdit(userInfo.EmpID);
                model.CustLinkManType = CustLinkManType;
                op.Edit(model);
            }
            else
            {
                model.CustLinkManType = CustLinkManType;
                model.MvcDefaultSave(userInfo.EmpID);
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

        public List<SelectListItem> SexList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "男", Value = "男" });
            list.Add(new SelectListItem { Text = "女", Value = "女" });
            return list;
        }


        public string getLinkManCombobox(string CustID)
        {
            int custId = TypeHelper.parseInt(CustID);
            var list = op.GetList(p => p.CustID == custId).Select(p => new { Id = p.Id, CustLinkManName = p.CustLinkManName, linkTel = p.CustLinkManTel, linkWeb = p.CustLinkManWeb }).ToList();
            return JavaScriptSerializerUtil.getJson(list);
        }
    }
}
