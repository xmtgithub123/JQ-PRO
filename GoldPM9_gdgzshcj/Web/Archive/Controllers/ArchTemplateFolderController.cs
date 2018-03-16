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
[Description("ArchShareFolder")]
public class ArchTemplateFolderController : CoreController
    {
        private DBSql.Archive.ArchTemplateFolder op = new DBSql.Archive.ArchTemplateFolder();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.EmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("TemplateShareFolder")));
            return View();
        }
        public ActionResult temptree(int MbId)
        {
            ViewBag.EmpID = userInfo.EmpID;
            ViewBag.MbId = MbId;
            return View();
        }
        #endregion

        #region 列表json
        [Description("模板列表json")]
        [HttpPost]
        public string json()
        {
            try
            {
                Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
                if (!PageModel.SelectOrder.isNotEmpty())
                {
                    PageModel.SelectOrder = "Id desc";
                }
                var TWhere = QueryBuild<DataModel.Models.ArchTemplateFolder>.True();
                TWhere = TWhere.And(x => x.FolderParentID == 0);
                var list = op.GetPagedDynamic(PageModel, TWhere).ToList();

                return JavaScriptSerializerUtil.getJson(new
                {
                    total = PageModel.PageTotleRowCount,
                    rows = list
                });
            }
            catch (Exception ex)
            {
                DoResult dr = DoResultHelper.doError(ex.Message);
                return JavaScriptSerializerUtil.getJson(dr);
            }
           
        }

        [Description("模板下拉json")]
        public string ddldatasource()
        {
            var list = op.GetList(x => x.FolderParentID == 0).ToList();
            List<Dictionary<string, object>> items = new List<Dictionary<string, object>>();
            foreach (var m in list)
            {
                Dictionary<string, object> item = new Dictionary<string, object>();
                item.Add("id",m.Id);
                item.Add("text",m.FolderName);
                items.Add(item);
            }
            return JavaScriptSerializerUtil.getJson(items);
        }


        [Description("列表treejson")]
        [HttpPost]
        public ActionResult treejson(int MbId)
        {
            var menulist = op.GetQuery(p => p.Id> 0).ToList();
            ArchTemplateFolder MbObj = op.Get(MbId);
            Dictionary<string, object> item = new Dictionary<string, object>();
            item["text"] = MbObj.FolderName;
            item["id"] = MbObj.Id;
            item["children"] = tree(menulist, MbId);
            List<object> datas = new List<object>();
            datas.Add(item);

            return Json(datas);
        }


        public List<object> tree(IEnumerable<DataModel.Models.ArchTemplateFolder> entitys, int parentid)
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
            var model = new  ArchTemplateFolder();
            model.FolderParentID = ParentId;
            if (ParentId > 0)
            {
                ViewBag.ParentName = op.Get(ParentId).FolderName;
            }
            return View("info", model);
        }
        #endregion
        public ActionResult template_add(int ParentId)
        {
            var model = new ArchTemplateFolder();
            model.FolderParentID = ParentId;
            if (ParentId > 0)
            {
                ViewBag.ParentName = op.Get(ParentId).FolderName;
            }
            return View("add", model);
        }


        
        [Description("模板编辑")]
        public ActionResult template_edit(int Id)
        {
            var model = op.Get(Id);
            return View("add", model);
        }
        public ActionResult edit(int Id)
        {
            var model = op.Get(Id);
            ViewBag.ParentName = op.Get(model.FolderParentID).FolderName;
            return View("info", model);
        }

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
            op.Edit(s => s.FolderParentID == id, u => new ArchTemplateFolder { FolderParentID = parentID });
            op.UnitOfWork.SaveChanges();

            //删除目录下的文件
            //new DBSql.Archive.ArchShareFolderFile().DeleteDir(id);


            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        public ActionResult template_del(int id)
        {

            //删除目录，目录的下级目录信息更改。
            int reuslt = 1;

            var parentID = op.Get(id).FolderParentID;

            if (op.Count(x => x.FolderParentID == id) > 0)
            {
                return Json(DoResultHelper.doError("存在目录项，不能删除！"));
            }

            op.Delete(s => s.Id == id);
            //修改下级
            op.Edit(s => s.FolderParentID == id, u => new ArchTemplateFolder { FolderParentID = parentID });
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
            try {
                var model = new ArchTemplateFolder();
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
                DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doError("操作失败");
                return Json(dr);
            } catch (Exception ex) {
                DoResult dr = DoResultHelper.doError("操作发生异常！");
                return Json(dr);
            }
            
        } 
        #endregion
    }
}
