using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using Common.Data.Extenstions;
using System;
using System.Collections.Generic;
namespace Archive.Controllers
{
    [Description("ArchElecFile")]
    public class ArchElecFileController : CoreController
    {
        private DBSql.Archive.ArchElecFile op = new DBSql.Archive.ArchElecFile();
        private DBSql.Archive.ArchElecProject proj = new DBSql.Archive.ArchElecProject();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            var perm = PermissionList("CollctArchiveHand");
            bool CanEdit = false;
            if (perm.Contains("alledit")) CanEdit = true;
            if (CanEdit)
            {
                ViewBag.permission = "['CanEdit']";
            }
            else
            {
                ViewBag.permission = "['NotEdit']";
            }

            return View();
        }


        [Description("viewpdf列表")]
        public ActionResult pdfView(int Id)
        {
            new DBSql.Archive.ArchElecProject().TaskArch(145, userInfo);
            new DBSql.Archive.ArchElecProject().TaskArch(143, userInfo);
            new DBSql.Archive.ArchElecProject().TaskArch(146, userInfo);
            string http = string.Format("http://{0}/GoldFile/", Request.ServerVariables["HTTP_Host"]);
            var a = op.Get(Id);
            string path = a.ElecFilePath;
            string name = a.ElecFileName;
            if (path.EndsWith(".pdf"))
            {
                name = "";
            }

            string UrL = Path.Combine(http, path, name);
            string filePath = System.Configuration.ConfigurationManager.AppSettings["UpFileRoot"].ToString();
            string bendiPath = Path.Combine(filePath, path);
            if (!System.IO.File.Exists(bendiPath))
            {
                Response.Redirect(Request.UrlReferrer.AbsoluteUri + "/Errors/notexists.html");
                return null;
                //return View("pdfNot");
            }
            else
            {
                ViewBag.URL = System.Web.HttpUtility.UrlEncode(UrL);
                return View();
            }
        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string json(string ids = "")
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }

            var T = QueryBuild<DataModel.Models.ArchElecFile>.True();
            if (!string.IsNullOrEmpty(ids))
            {
                var Arr = ids.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList().Select(s => Convert.ToInt32(s));
                T = T.And(s => Arr.Contains(s.Id));
            }
            if (string.IsNullOrEmpty(PageModel.SelectField))
            {
                PageModel.SelectField = "new(Id,ArchProjId,ArchElecFileId,FK_ArchElecFile_ArchProjId.ProjectId,FK_ArchElecFile_ArchProjId.ElecNumber,FK_ArchElecFile_ArchProjId.ElecName,FK_ArchElecFile_ArchProjId.ProjEmpName,FK_ArchElecFile_ArchProjId.PhaseName,ElecFileName,ElecExt,ElecSize,ElecFileVersionId,CreationTime,CreatorEmpName)";
            }
            var list = op.GetPagedDynamic(PageModel, T).ToList();

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }


        [Description("目录下文件列表 treejson")]
        [HttpPost]
        public string dirjson(int refid = 0, string refTable = "ArchElecFile")
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }

            var T = QueryBuild<DataModel.Models.ArchElecFile>.True();

            T = T.And(s => s.ArchElecFileRefTable == refTable);
            T = T.And(s => s.ArchElecFileId == refid);

            var list = op.GetPagedDynamic(PageModel, T).ToList();

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
            var model = new ArchElecFile();
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
        public ActionResult del(params int[] id)
        {
            int reuslt = 1;
            op.Delete(id.ToList());
            op.DbContext.SaveChanges();
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 上传
        [Description("上传")]
        [HttpPost]
        public ActionResult save(int Id, int ProjID)
        {
            var reuslt = op.UploadFile(Id, ProjID, userInfo);
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 下载   下载后记录日志 
        public static MemoryStream ZipFiles(string[] files, string zipFileName)
        {
            MemoryStream ms = new MemoryStream();
            byte[] buffer = null;

            using (ZipFile file = ZipFile.Create(ms))
            {
                file.BeginUpdate();

                file.NameTransform = new MyNameTransfom();
                foreach (var item in files)
                {
                    if (System.IO.File.Exists(item))
                        file.Add(item);

                }
                file.CommitUpdate();
                buffer = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(buffer, 0, buffer.Length);   //读取文件内容(1次读ms.Length/1024M)  
                ms.Flush();
                //ms.Close();
            }
            ms.Position = 0;
            return ms;
        }

        /// <summary>
        /// 下载日志
        /// </summary>
        /// <param name="list"></param>
        private void InsertLog(IEnumerable<string> list)
        {
            DBSql.Sys.BaseLog logAdd = new DBSql.Sys.BaseLog();
            foreach (var item in list)
            {
                DataModel.Models.BaseLog log = new DataModel.Models.BaseLog();
                log.BaseLogEmpID = userInfo.EmpID;
                log.EmpName = userInfo.EmpName;
                log.BaseLogTypeID = 10;
                log.BaseLogDate = DateTime.Now;
                log.BaseLogIP = GetIP();
                log.BaseLogRefTable = "ArchElecFile";
                log.BaseLogRefID = userInfo.EmpID;
                log.BaseLogRefHTML = item + "，下载成功！";
                logAdd.Add(log);
            }

            logAdd.UnitOfWork.SaveChanges();
        }

        [HttpGet]
        public FileStreamResult DownFile(params int[] id)
        {
            string filePath = System.Configuration.ConfigurationManager.AppSettings["UpFileRoot"].ToString();
            var filelist = op.GetList(s => id.Contains(s.Id)).Select(s => filePath + @"\" + s.ElecFilePath + @"\" + s.ElecFileName);

            try
            {
                InsertLog(filelist);
            }
            catch { }

            string fileName = System.DateTime.Now.ToString("yyyyMMddhhmmss") + string.Format("等{0}个文件.zip", id.Count().ToString());
            string absoluFilePath = filePath + @"\ArchElecFile\" + fileName;

            if (filelist.Count() == 1)
            {
                fileName = filelist.ElementAt(0).Substring(filelist.ElementAt(0).LastIndexOf(@"\") + 1);
                absoluFilePath = filelist.ElementAt(0).Substring(0, filelist.ElementAt(0).LastIndexOf(@"\"));
                if (System.IO.File.Exists(absoluFilePath))
                {
                    return File(new FileStream(absoluFilePath, FileMode.Open), "application/octet-stream", Server.UrlEncode(fileName));
                }
                else
                {
                    Response.Redirect(Request.UrlReferrer.AbsoluteUri + "/Errors/notexists.html");
                    return null;
                }
            }
            else
            {
                try
                {
                    var arr = filelist.Select(p => p.Substring(0, p.LastIndexOf(@"\"))).ToArray();
                    var s = ZipFiles(arr, absoluFilePath);
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

    public class MyNameTransfom : ICSharpCode.SharpZipLib.Core.INameTransform
    {
        #region INameTransform 成员

        public string TransformDirectory(string name)
        {
            return null;
        }

        public string TransformFile(string name)
        {
            return Path.GetFileName(name);
        }

        #endregion
    }
}
