using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
namespace Oa.Controllers
{
    [Description("OaBook")]
    public class OaBookController : CoreController
    {
        private DBSql.Oa.OaBook op = new DBSql.Oa.OaBook();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("BookReg")));
            return View();
        }
        #endregion

        public ActionResult listBorr()
        {
            return View();
        }

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public JsonResult json()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            if (!string.IsNullOrEmpty(Request.Form["text"]))
            {
                PageModel.TextCondtion = Request.Form["text"].ToString();
            }
            if (!string.IsNullOrEmpty(Request.Form["bookType[]"]))
            {
                PageModel.SelectCondtion.Add("BookTypeID", Request.Form["bookType[]"]);
            }
            var list = new DBSql.Oa.OaBook().GetList(PageModel).ToList();
            return Json(new { total = PageModel.PageTotleRowCount, rows = list });
        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new OaBook();
            ViewBag.permission = "['add','submit']";
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            ViewBag.permission = "['add','submit']";
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
            var model = new OaBook();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();

            int reuslt = 0;
            if (model.Id > 0)
            {
                model.MvcDefaultEdit(userInfo);
                op.Edit(model);
            }
            else
            {
                model.MvcDefaultSave(userInfo);
                op.Add(model);
            }
            op.UnitOfWork.SaveChanges();
            reuslt = model.Id;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion


        /// <summary>
        /// 检测图书名称与图书编号是否冲突
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CheckConflictBookOrNumber()
        {
            string BookName = Request.Params["BookName"];
            string BookNumber = Request.Params["BookNumber"];
            if(!string.IsNullOrEmpty(BookName))
            {
                BookName = BookName.Trim();
            }
            if(!string.IsNullOrEmpty(BookNumber))
            {
                BookNumber = BookNumber.Trim();
            }
            int Id = TypeHelper.parseInt(Request.Params["Id"]);
            object result = null;
            int BookNameCount = 0;
            int BookNumberCount = 0;//将名称重复与编号重复分开方便判断
            if(Id==0)//新增状态
            {
                BookNameCount = op.GetList(p=>p.BookName==BookName&&p.DeleterEmpId==0).Count();
                BookNumberCount = op.GetList(p=>p.BookNameNumber==BookNumber&&p.DeleterEmpId==0).Count();
                result = new { BookNameCount = BookNameCount, BookNumberCount = BookNumberCount };
            }
            else//编辑状态
            {
                BookNameCount = op.GetList(p => p.BookName == BookName && p.DeleterEmpId == 0&&p.Id!=Id).Count();
                BookNumberCount = op.GetList(p => p.BookNameNumber == BookNumber && p.DeleterEmpId == 0&&p.Id!=Id).Count();
                result = new { BookNameCount = BookNameCount, BookNumberCount = BookNumberCount };
            }
            return Json(result);
        }
    }
}
