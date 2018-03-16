using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.IO;
namespace Core.Controllers
{
[Description("BaseFeedBack")]
public class BaseFeedBackController : CoreController
    {
        private DBSql.Sys.BaseFeedBack op = new DBSql.Sys.BaseFeedBack();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("SystemHelper")));
            return View();
        }

        [Description("备份列表")]
        public ActionResult baklist()
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
            var model = new BaseFeedBack();

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
            var model = new BaseFeedBack();
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


        [Description("数据备份")]
        [HttpPost]
        public ActionResult bak()
        {
            string path = GetAppSettBakStr() + "\\DBbak";

            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            string DataBaseName = System.Configuration.ConfigurationManager.AppSettings["DataBaseName"].ToString();

            var Structure = new DBSql.PubFunction.V_SystemTableStructure();
            Structure.SP_BakupDataBase(path, "", "", "", "", DataBaseName, "false");
            
            DoResult dr = DoResultHelper.doSuccess("备份操作成功") ;
            return Json(dr);
        }


        [Description("删除备份")]
        [HttpPost]
        public ActionResult delbak(string FileFullName)
        {
            int reuslt = 0;
            if (System.IO.File.Exists(FileFullName))
            {
                FileInfo DownloadFile = new FileInfo(FileFullName);
                DownloadFile.Delete();
                reuslt = 1;
            }
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess("操作成功") : DoResultHelper.doSuccess("操作失败");
            return Json(dr);
        }


        [Description("下载备份")]
        [HttpGet]
        public FileResult downbak(string FullFileName)
        {
            FileInfo DownloadFile = new FileInfo(FullFileName);
            return ResponseFile(FullFileName, DownloadFile.Name);
        }


        private FileResult ResponseFile(string path, string realName)
        {
            if (!System.IO.File.Exists(path))
            {
                Response.StatusCode = 404;
                return null;
            }
            return File(path, "application/octet-stream", realName);
        }

        private void FileDownload(string FullFileName)
        {
            FileInfo DownloadFile = new FileInfo(FullFileName);
            Response.Clear();
            Response.ClearHeaders();
            Response.Buffer = false;
            Response.ContentType = "application/octet-stream";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + Server.UrlEncode(DownloadFile.FullName));
            Response.AppendHeader("Content-Length", DownloadFile.Length.ToString());
            Response.WriteFile(DownloadFile.FullName);
            Response.Flush();
            Response.End();
        }


        public string GetAppSettBakStr()
        {
            return System.Configuration.ConfigurationManager.AppSettings["UpFileRoot"].ToString();
        }

        public string bakjson()
        {
            string path = GetAppSettBakStr() + "\\DBbak";

            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }

            var arr = from s in Directory.GetFiles(path)
                      where s.EndsWith(".bak")
                      select s;
            var list = from a in arr
                       select new
                       {
                           FileFullName = a,
                           FileName = new FileInfo(a).Name,
                           FileDate = new FileInfo(a).LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss"),
                           URL = string.Format("{0}/DBbak/{1}", GetGoldFileUrl(), new FileInfo(a).Name)
                       };

            return JavaScriptSerializerUtil.getJson(new
            {
                total = list.Count(),
                rows = list
            });
        }
        #endregion
    }
}
