using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System;

namespace Archive.Controllers
{
    public class ArchProjectFolderController : CoreController
    {
        private DBSql.Archive.ArchProjectFolder op = new DBSql.Archive.ArchProjectFolder();
        private DBSql.Project.Project proop = new DBSql.Project.Project();
        private DBSql.Archive.ArchTemplateFolder tempop = new DBSql.Archive.ArchTemplateFolder();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        [Description("模板设置页面")]
        public ActionResult settemplate(int ProjID)
        {
            var proobj = proop.Get(ProjID);
            var count = op.Count(x => x.FolderProjectId == ProjID && x.FolderParentID == 0);
            DataModel.Models.ArchProjectFolder model = null;
            if (count == 0)
            {
                model = new ArchProjectFolder();
                model.Id =0;
            }
            else
            {
                model = op.FirstOrDefault(x => x.FolderProjectId == ProjID && x.FolderParentID == 0);
            }
            ViewBag.ProjID = ProjID;
            ViewBag.ProjName = proobj == null ? "" : proobj.ProjName;
            return View("settemplate", model);
        }

        public ActionResult list(int ProjID)
        {
            var proobj = proop.Get(ProjID);
            var count = op.Count(x => x.FolderProjectId == ProjID && x.FolderParentID ==0);
            DataModel.Models.ArchProjectFolder model = null;
            if (count == 0)
            {
                model = new ArchProjectFolder();
                model.Id = 0;
            }
            else
            {
                model = op.FirstOrDefault(x => x.FolderProjectId == ProjID && x.FolderParentID == 0);
            }
            ViewBag.ProjID = ProjID;
            ViewBag.ProjName = proobj==null?"": proobj.ProjName;
            return View("list", model);
        }

        [HttpPost]
        public ActionResult save(int Id)
        {
            try
            {
                #region 设置模板
                if (Id > 0)
                {
                   var obj= DAL.DBExecute.ExecuteScalar(string.Format("select a.Id from [dbo].[ArchProjectFolder] a inner join [dbo].[ArchProjectFolderFile] b on a.Id=b.ArchProjectFolderId where FolderProjectId={0} and b.DeleterEmpId=0",Id));
                    if (obj != DBNull.Value)
                    {
                        return Json(DoResultHelper.doError("目录已上传文件，不可再切换模板！"));
                    }
                }

                var model = new ArchProjectFolder();
                if (Id > 0)
                {
                    model = op.Get(Id);
                }
                model.MvcSetValue();

                //清除先有模板
                op.Delete(x=>x.FolderProjectId==model.FolderProjectId && x.FolderParentID >0);

                if (Id > 0)
                {
                    model.MvcDefaultEdit(userInfo);
                    op.Edit(model);
                }
                else
                {
                    model.MvcDefaultSave(userInfo);
                    //model.Id = Guid.NewGuid();
                    op.Add(model);
                }
                op.UnitOfWork.SaveChanges();
                #endregion

                #region 拷贝模板
                var menulist = tempop.GetQuery(p => p.Id > 0).ToList();
                List<Dictionary<string, object>> mbdatas = templatetree(menulist,model.FolderTemplateId);
                createtree(model, mbdatas);
                #endregion

                DoResult dr = DoResultHelper.doSuccess(model.Id, "操作成功");
                return Json(dr);
            }
            catch (Exception ex)
            {
                DoResult dr = DoResultHelper.doError(ex.Message);
                return Json(dr);
            }
        }
        
        public void createtree(ArchProjectFolder model, List<Dictionary<string, object>> mbdatas)
        {
            foreach (var f in mbdatas)
            {
                ArchProjectFolder mlmodel = new ArchProjectFolder();
                mlmodel.MvcDefaultSave(userInfo);
                //mlmodel.Id = Guid.NewGuid();
                mlmodel.FolderName = f["text"].ToString();
                mlmodel.FolderTemplateId= int.Parse(f["id"].ToString());
                mlmodel.FolderParentID = model.Id;
                mlmodel.FolderProjectId = model.FolderProjectId;
                mlmodel.FolderProjectName = model.FolderProjectName;
                op.Add(mlmodel);
                op.UnitOfWork.SaveChanges();
                if (f["children"] != null)
                {
                    List<Dictionary<string, object>> clist = (List<Dictionary<string, object>>)f["children"];
                    createtree(mlmodel, clist);
                }
            }
        }

        public List<Dictionary<string,object>> templatetree(IEnumerable<DataModel.Models.ArchTemplateFolder> entitys, int parentid)
        {
            if (!entitys.isNotEmpty()) return null;
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            var entityarr = entitys.Where(m => m.FolderParentID == parentid);
            foreach (var item in entityarr)
            {
                //获取子类 递归
                List<Dictionary<string, object>> clist = templatetree(entitys, item.Id);
                list.Add(new Dictionary<string, object>() {
                    {"id",item.Id},
                    {"parentID",item.FolderParentID},
                    {"text",item.FolderName},
                    {"children", clist}
                });
            }

            if (list.Count > 0) return list;

            return null;
        }

        public List<Dictionary<string, object>> projecttree(IEnumerable<DataModel.Models.ArchProjectFolder> entitys, int parentid)
        {
            if (!entitys.isNotEmpty()) return null;
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            var entityarr = entitys.Where(m => m.FolderParentID == parentid);
            foreach (var item in entityarr)
            {
                //获取子类 递归
                List<Dictionary<string, object>> clist = projecttree(entitys, item.Id);
                string text = item.FolderName;
                if (parentid == 0)
                {
                    text=proop.Get(item.FolderProjectId).ProjName;
                }
                list.Add(new Dictionary<string, object>() {
                    {"id",item.Id},
                    {"parentID",item.FolderParentID},
                    {"text",text},
                    {"children", clist}
                });
            }

            if (list.Count > 0) return list;

            return null;
        }
        
        /// <summary>
        /// 过程参照，左边树数据源
        /// </summary>
        /// <param name="ProjID"></param>
        /// <returns></returns>
        public ActionResult treejson(int ProjID)
        {
            var menulist = op.GetQuery(p => p.FolderProjectId==ProjID).ToList();
            return Json(projecttree(menulist, 0));
        }
    }
}
