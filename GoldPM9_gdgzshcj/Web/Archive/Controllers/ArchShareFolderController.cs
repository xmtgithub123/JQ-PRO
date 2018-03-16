using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
namespace Archive.Controllers
{
[Description("ArchShareFolder")]
public class ArchShareFolderController : CoreController
    {
        private DBSql.Archive.ArchShareFolder op = new DBSql.Archive.ArchShareFolder();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.EmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ShareFolder")));
            return View();
        }

        public ActionResult fileList()
        {
            ViewBag.EmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ShareFolder")));
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
            var list = op.GetPagedDynamic(PageModel).ToList();
            
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }

        [Description("列表json")]
        [HttpPost]
        public string filejson(int FolderId = 0)
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            var list = new DBSql.Archive.FileDetail().GetPagedDynamic(PageModel,s=>s.FileCatalogId==FolderId).ToList();

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }


        [Description("列表treejson")]
        [HttpPost]
        public ActionResult treejson()
        {
            var menulist = op.GetQuery(p => p.Id> 0).ToList();
            return Json(tree(menulist, 0));
        }

        [Description("列表treejson")]
        [HttpPost]
        public ActionResult filetreejson()
        {
            var menulist = new DBSql.Archive.FileCatalog().GetQuery(p => p.Id > 0).ToList();
            return Json(filetree(menulist,0));
        }


        public List<object> filetree(IEnumerable<DataModel.Models.FileCatalog> entitys, int parentid)
        {
            if (!entitys.isNotEmpty()) return null;
            List<object> list = new List<object>();
            var entityarr = entitys.Where(m => m.ParentId == parentid);
            foreach (var item in entityarr)
            {
                //获取子类 递归
                IList<object> clist = filetree(entitys, item.Id);
                list.Add(new
                {
                    id = item.Id,
                    parentID = item.ParentId,
                    CatalogId = item.CatalogjId,
                    text=item.CatalongName,
                    children = clist
                });
            }

            if (list.Count > 0) return list;

            return null;

        }

        public List<object> tree(IEnumerable<DataModel.Models.ArchShareFolder> entitys, int parentid)
        {
            if (!entitys.isNotEmpty()) return null;
            List<object> list = new List<object>();
            var entityarr = entitys.Where(m => m.FolderParentID == parentid);
            foreach (var item in entityarr)
            {
                //获取子类 递归
                IList<object> clist = tree(entitys, item.Id);
                list.Add(new
                {
                    id = item.Id,
                    parentID = item.FolderParentID,
                    orderCode = item.FolderRemark,
                    text = item.FolderName,
                    empid = item.FolderManagerId,
                    depids = item.FolderShareDeptIds,
                    children = clist
                });
            }

            if (list.Count > 0) return list;

            return null;
        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add(int ParentId)
        {
            var model = new ArchShareFolder();

            model.FolderParentID = ParentId;

            ViewBag.ParentName = op.Get(ParentId).FolderName;


            return View("info", model);
        } 
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            ViewBag.ParentName = op.Get(model.FolderParentID).FolderName;
            return View("info", model);
        } 
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int id)
        {

            //删除目录，目录的下级目录信息更改。
            int reuslt = 1;

            var parentID = op.Get(id).FolderParentID;

            if (parentID == 0)
            {
                return Json(DoResultHelper.doSuccess("目录不能删除！"));
            }

            op.Delete(s => s.Id == id);
            //修改下级
            op.Edit(s => s.FolderParentID == id, u => new ArchShareFolder { FolderParentID = parentID });
            op.UnitOfWork.SaveChanges();

            //删除目录下的文件
            //new DBSql.Archive.ArchShareFolderFile().DeleteDir(id);


            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        } 
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id)
        {
            var model = new ArchShareFolder();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();

            int reuslt = 0;                      
            if (model.Id > 0)
            {
                op.MvcDefaultEdit(userInfo);
                op.Edit(model);
            }
            else
            {
                op.MvcDefaultSave(userInfo);
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
