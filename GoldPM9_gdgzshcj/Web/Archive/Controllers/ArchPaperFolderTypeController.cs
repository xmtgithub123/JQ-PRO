using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System;

namespace Archive.Controllers
{
[Description("ArchPaperFolderType")]
public class ArchPaperFolderTypeController : CoreController
    {
        private DBSql.Archive.ArchPaperFolderType op = new DBSql.Archive.ArchPaperFolderType();
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
            var model = new ArchPaperFolderType();
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
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
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id)
        {
            var model = new ArchPaperFolderType();
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
                var mlModel= new ArchPaperFolderType();
                if (model.Type == 0)
                {
                    op.UnitOfWork.SaveChanges();
                    mlModel.ParentId = model.Id;
                    mlModel.ArchPaperFolderId = model.ArchPaperFolderId;
                    mlModel.Name = "目录";
                    mlModel.Number = "目录";
                    mlModel.Type = 2;
                    op.Add(mlModel);
                }
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

        public ActionResult treejson(int id)
        {
            var dt = new DBSql.Archive.ArchPaperFolder().GetTreeInfo(id);

            return Json(tree(dt, "0"));
        }

        public List<object> tree(DataTable dt, string parentid)
        {
            if (dt.Rows.Count==0) return null;
            List<object> list = new List<object>();
            DataRow[] entityarr = dt.Select("parentId='" + parentid + "'");
            foreach (DataRow item in entityarr)
            {
                //获取子类 递归
                IList<object> clist = tree(dt, item["GId"].ToString());
                list.Add(new
                {
                    GId = item["GId"].ToString(),
                    Id = item["Id"].ToString(),
                    number = item["Number"].ToString(),
                    numberShow=item["NumberShow"].ToString(),
                    parentID = item["ParentId"].ToString(),
                    name = item["Name"].ToString(),
                    type=item["Type"].ToString(),
                    children = clist
                });
            }

            if (list.Count > 0) return list;

            return null;
        }

        public string GetFile()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();

            if (Request.Params["ArchPaperFolderId"] != null)
            {
                PageModel.SelectCondtion.Add("ArchPaperFolderId", Request.Params["ArchPaperFolderId"].ToString());
            }

            if (Request.Params["BaseId"] != null)
            {
                PageModel.SelectCondtion.Add("Id", Request.Params["BaseId"].ToString());
            }

            PageModel.SelectCondtion.Add("State", Request.Params["State"] == null ? "" : Request.Params["State"].ToString());

            PageModel.TextCondtion = string.IsNullOrEmpty(Request.Params["TxtLike"]) == true ? "" : Request.Params["TxtLike"];

            DataTable dt = new DBSql.Archive.ArchPaperFolderType().GetArchPaperFolderFile(PageModel);

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = dt
            });
        }

        public ActionResult WJState(int[] ids)
        {
            int result = 0;
            try
            {
                List<long> attachId = new List<long>();
                foreach (long id in ids)
                {
                    attachId.Add(id);
                }

                var baseAttach = new DBSql.Sys.BaseAttach();

                var models = baseAttach.GetList(p => attachId.Contains(p.AttachID));

                var state = Request.Params["state"].ToString();
                if (state == "1")
                {
                    foreach (DataModel.Models.BaseAttach model in models)
                    {
                        model.AttachTag = "作废";
                        baseAttach.Edit(model);
                    }
                }
                else
                {
                    foreach (DataModel.Models.BaseAttach model in models)
                    {
                        model.AttachTag = "";
                        baseAttach.Edit(model);
                    }
                }

                baseAttach.UnitOfWork.SaveChanges();
                result = 1;
            }
            catch (Exception ex)
            {
                result = 0;
            }

            DoResult dr = result > 0 ? DoResultHelper.doSuccess(result, "操作成功") : DoResultHelper.doSuccess(result, "操作失败");
            return Json(dr);
        }

        public ActionResult WJDel(int[] ids)
        {
            int result = 0;
            try
            {
                List<long> attachId = new List<long>();
                foreach (long id in ids)
                {
                    attachId.Add(id);
                }

                var baseAttach = new DBSql.Sys.BaseAttach();

                var models = baseAttach.GetList(p => attachId.Contains(p.AttachID));

                foreach (DataModel.Models.BaseAttach model in models)
                {
                    model.AttachTag = "作废";
                    baseAttach.Delete(model);
                }

                baseAttach.UnitOfWork.SaveChanges();
                result = 1;
            }
            catch (Exception ex)
            {
                result = 0;
            }

            DoResult dr = result > 0 ? DoResultHelper.doSuccess(result, "操作成功") : DoResultHelper.doSuccess(result, "操作失败");
            return Json(dr);
        }
    }
}
