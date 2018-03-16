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
[Description("BaseEmpTeam")]
public class BaseEmpTeamController : CoreController
    {
        private DBSql.Sys.BaseEmpTeam op = new DBSql.Sys.BaseEmpTeam();
        private DBSql.Sys.BaseEmployee opemp = new DBSql.Sys.BaseEmployee();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
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
            var list = op.GetPagedList(PageModel).ToList();

            var l = (from s in list
                     select new
                     {
                         s.Id,
                         s.TeamName,
                         TeamMembers = opemp.GetEmpName(s.TeamMembers),
                         s.TeamNote,
                         s.TeamType,
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
            var model = new BaseEmpTeam();

            model.CreationTime = System.DateTime.Now;
            model.CreatorEmpName = userInfo.EmpName ;

            return View("info", model);
        } 
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);

            ViewBag.TeamMembersName = "";

            if (!string.IsNullOrEmpty(model.TeamMembers))
            {
                ViewBag.TeamMembersName = opemp.GetEmpName(model.TeamMembers);
            }

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
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        } 
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id)
        {
            var model = new BaseEmpTeam();
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
			reuslt = model.Id ;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        } 
        #endregion
    }
}
