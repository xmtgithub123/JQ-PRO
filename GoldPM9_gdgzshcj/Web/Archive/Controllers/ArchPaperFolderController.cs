using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using Common.Data.Extenstions;
using System;

namespace Archive.Controllers
{
[Description("ArchPaperFolder")]
public class ArchPaperFolderController : CoreController
    {
        private DBSql.Archive.ArchPaperFolder op = new DBSql.Archive.ArchPaperFolder();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ArchPaperFolder")));
            var year = DateTime.Now.Year - 5;
            string _data = "[";
            _data += "{\"Id\":0,\"Text\":\"全部\"},";
            _data += "{\"Id\":-1,\"Text\":\"其他\"},";
            for (int i = 0; i < 8; i++)
            {
                _data += "{\"Id\":" + (year + i) + ",\"Text\":" + (year + i) + "},";
            }
            _data = _data.Trim(',');
            _data += "]";
            ViewBag._data = _data;
            ViewBag.Year = DateTime.Now.Year;
            return View();
        }

        [Description("图档信息列表")]
        public ActionResult archPaperFile()
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

            PageModel.TextCondtion = Request.Params["TxtLike"];
            if (Request.Params["Year"] == null)
            {
                PageModel.SelectCondtion.Add("Year", DateTime.Now.Year);
            }
            else
            {
                PageModel.SelectCondtion.Add("Year", Request.Params["Year"]);
            }

            var list = op.GetListByWhere(PageModel);

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
            var model = new ArchPaperFolder();

            var ProjRespEmps = (from m in new DBSql.Sys.BaseQualification().GetQualificationEmployee((int)DataModel.NodeType.项目设总, 0, 0, 0)
                                select new { m.EmpID, m.EmpName }).Distinct();
            ViewBag.Qualification = JavaScriptSerializerUtil.getJson(ProjRespEmps);
            model.ArchNumber = op.GetMaxArchNumber();

            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            var ProjRespEmps = (from m in new DBSql.Sys.BaseQualification().GetQualificationEmployee((int)DataModel.NodeType.项目设总, 0, 0, 0)
                                select new { m.EmpID, m.EmpName }).Distinct();
            ViewBag.Qualification = JavaScriptSerializerUtil.getJson(ProjRespEmps);
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

        [Description("删除树形节点")]
        public ActionResult delNode(int id)
        {
            int result = 0;
            result = new DBSql.Archive.ArchPaperFolderType().DeleteNode(id);
            DoResult dr = result > 0 ? DoResultHelper.doSuccess(result, "操作成功") : DoResultHelper.doSuccess(result, "操作失败");
            return Json(dr);
        }

        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id)
        {
            var model = new ArchPaperFolder();
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
			if (Id <= 0)
            {
                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.MoveFile(model.Id, userInfo.EmpID, userInfo.EmpName);
                }
            }
			reuslt = model.Id ;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion
    }
}
