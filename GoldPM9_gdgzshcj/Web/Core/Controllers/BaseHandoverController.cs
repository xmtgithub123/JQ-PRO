using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
namespace Core.Controllers
{
    [Description("BaseHandover")]
    public class BaseHandoverController : CoreController
    {
        private DBSql.Sys.BaseEmployee opemp = new DBSql.Sys.BaseEmployee();
        private DBSql.Sys.BaseHandover op = new DBSql.Sys.BaseHandover();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            return View();
        }

        [Description("移交工程任务列表")]
        public ActionResult tasklistview(int id)
        {
            var model = op.Get(id);

            ViewBag.HandEmpId = model.HandEmpId;
            ViewBag.HandEmpName = model.HandEmpName;
            ViewBag.HandEmps = "";

            if (!string.IsNullOrEmpty(model.HandEmps))
            {
                Dictionary<int, string> emps = new Dictionary<int, string>();
                var datas = opemp.GetListFromCache(m => ("," + model.HandEmps + ",").IndexOf("," + m.EmpID + ",") > -1);
                foreach (var data in datas)
                {
                    emps.Add(data.EmpID, data.EmpName);
                }
                ViewBag.Emps = emps;
            }
            return View("tasklist", model);
        }


        [Description("移交审批任务列表")]
        public ActionResult flowlistview(int id)
        {
            var model = op.Get(id);
            
            ViewBag.HandEmpId = model.HandEmpId;
            ViewBag.HandEmpName = model.HandEmpName;
            ViewBag.HandEmps = "";

            if (!string.IsNullOrEmpty(model.HandEmps))
            {
                Dictionary<int, string> emps = new Dictionary<int, string>();
                var datas = opemp.GetListFromCache(m => ("," + model.HandEmps + ",").IndexOf("," + m.EmpID + ",") > -1);
                foreach (var data in datas)
                {
                    emps.Add(data.EmpID, data.EmpName);
                }
                ViewBag.Emps = emps;
            }
            return View("flowlist", model);
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
            var list = op.GetPagedList(PageModel).ToList();

            var l = (from s in list
                     select new
                     {
                         s.Id,
                         s.HandReason,
                         HandEmps = opemp.GetEmpName(s.HandEmps),
                         s.HandNote,
                         s.HandType,
                         s.HandEmpId,
                         s.HandEmpName,
                         s.CreatorEmpId,
                         s.CreatorEmpName,
                         s.CreationTime
                     });

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = l
            });
        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new BaseHandover();

            model.CreationTime = System.DateTime.Now;
            model.CreatorEmpName = userInfo.EmpName;


            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);

            ViewBag.HandEmps = "";

            if (!string.IsNullOrEmpty(model.HandEmps))
            {
                ViewBag.HandEmps = opemp.GetEmpName(model.HandEmps);
            }

            return View("info", model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 1;
            op.Delete(s => id.Contains(s.Id));
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
            var model = new BaseHandover();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();

            if (model.HandEmpId != 0) model.HandEmpName = GetEmpInfo(model.HandEmpId).EmpName;

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
    }
}
