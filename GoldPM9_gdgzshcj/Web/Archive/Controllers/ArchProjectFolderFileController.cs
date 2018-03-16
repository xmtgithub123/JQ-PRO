using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
namespace Archive.Controllers
{
    [Description("ArchProjectFolderFile")]
    public class ArchProjectFolderFileController : CoreController
    {
        private DBSql.Archive.ArchProjectFolderFile op = new DBSql.Archive.ArchProjectFolderFile();
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
        public string json(int FolderId = 0,int DelEmpID = 0)
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            var list = op.GetPagedDynamic(PageModel, s => s.ArchProjectFolderId == FolderId && s.DeleterEmpId == 0).ToList();

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add(int FolderId)
        {
            var model = new ArchProjectFolderFile();
            model.CreationTime = System.DateTime.Now;
            model.ArchProjectFolderId = FolderId;
            ViewBag.DirName = new DBSql.Archive.ArchProjectFolder().Get(FolderId).FolderName;

            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);

            ViewBag.DirName = new DBSql.Archive.ArchProjectFolder().Get(model.ArchProjectFolderId).FolderName;

            return View("info", model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(params int[] id)
        {
            int reuslt = 1;

            op.Edit(s => id.Contains(s.Id), u => new ArchProjectFolderFile {  DeleterEmpId = userInfo.EmpID, DeleterEmpName = userInfo.EmpName, DeletionTime = System.DateTime.Now });
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
            var model = new ArchProjectFolderFile();
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
                op.UnitOfWork.SaveChanges();
            }
            else
            {
                model.MvcDefaultSave(userInfo);
                op.Add(model);
                op.UnitOfWork.SaveChanges();
                new DBSql.Sys.BaseAttach().MoveFile(model.Id, userInfo.EmpID, userInfo.EmpName);
            }
            
            reuslt = model.Id;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 下载
        public FileStreamResult DownFile(params int[] id)
        {
            if (id == null) return null;
            //关联附件的列表

            var list = new DBSql.Sys.BaseAttach().GetBaseAttachList(id.ToList(), "ArchProjectFolderFile");


            string filePath = System.Configuration.ConfigurationManager.AppSettings["UpFileRoot"].ToString();
            var filelist = list.Select(s => filePath + @"\" + s.AttachDir + @"\" + s.AttachName);

            string fileName = System.DateTime.Now.ToString("yyyyMMddhhmmss") + string.Format("等{0}个文件.zip", id.Count().ToString());
            string absoluFilePath = filePath + @"\ArchProjectFolderFile\" + fileName;

            if (filelist.Count() == 1)
            {
                fileName = filelist.ElementAt(0).Substring(filelist.ElementAt(0).LastIndexOf(@"\") + 1);
                absoluFilePath = filelist.ElementAt(0).Substring(0,filelist.ElementAt(0).LastIndexOf(@"\"));
                return File(new FileStream(absoluFilePath, FileMode.Open), "application/octet-stream", Server.UrlEncode(fileName));
            }
            else
            {
                try
                {
                    //文件路径预处理
                    string[] arr = filelist.ToArray();
                    //for (int i=0;i<arr.Length;i++) {
                    //    var tp = arr[i];
                    //    arr[i] = System.IO.Path.Combine(filePath, tp);
                    //}
                    var s = ArchElecFileController.ZipFiles(arr, absoluFilePath);
                    return File(s, "application/octet-stream", Server.UrlEncode(fileName));
                }
                catch
                {
                    return null;
                }
            }
        }
        #endregion
    }
}
