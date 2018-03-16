using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System;
using SIO = System.IO;
using System.Web;

namespace Oa.Controllers
{
    [Description("OaNew")]
    public class OaNewController : CoreController
    {
        private DBSql.OA.OaNew op = new DBSql.OA.OaNew();
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
            //展示需要的列信息

            if(!string.IsNullOrEmpty(Request.Params["endDate"])&&TypeHelper.isDateTime(Request.Params["endDate"]))
            {
                DateTime endDate = TypeHelper.parseDateTime(Request.Params["endDate"]).AddDays(1);
                list = op.GetPagedList(p=>p.NewsDate<endDate,PageModel).ToList();
            }
            var target = (from item in list
                          let NewsDate=item.NewsDate.ToString("yyyy-MM-dd")
                          select new
                          {
                              Id = item.Id,
                              NewsTypeID = item.FK_OaNew_NewsTypeID.BaseID,//新闻类别编号
                              NewsTypeName = item.FK_OaNew_NewsTypeID.BaseName,//新闻类别
                              item.NewsTitle,//新闻标题
                              item.CreatorEmpName,//发布人
                              NewsDate,//发布时间
                          }).ToList();



            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = target
            });
        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new OaNew();
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
        [ValidateInput(false)]
        public ActionResult save(int Id)
        {
            DataModel.Models.OaNew model = null;
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            else
            {
                model = new OaNew();
                model.MvcSetValue();
            }
            var newsImage = Request.Form["NewsImage"];
            if (newsImage == "clear")
            {
                //图片清除
                if (!string.IsNullOrEmpty(model.NewsImage))
                {
                    //删除图片
                    JQ.Util.IOUtil.TryDeleteFile(SIO.Path.Combine(JQ.Util.ConfigUtil.GetConfigValue("UpFileRoot"), model.NewsImage));
                    model.NewsImage = "";
                }
                model.NewsDescription = "";
            }
            else if (!string.IsNullOrEmpty(newsImage))
            {
                //验证图片是否存在
                var path = System.IO.Path.Combine(JQ.Util.IOUtil.GetTempPath(), Request.Form["NewsImage"]);
                if (SIO.File.Exists(path))
                {
                    var rootPath = JQ.Util.ConfigUtil.GetConfigValue("UpFileRoot");
                    var newsImagePath = System.IO.Path.Combine("NewsImage", DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("dd"));
                    if (!string.IsNullOrEmpty(model.NewsImage))
                    {
                        //删除老图片
                        JQ.Util.IOUtil.TryDeleteFile(SIO.Path.Combine(JQ.Util.ConfigUtil.GetConfigValue("UpFileRoot"), model.NewsImage));
                    }
                    model.NewsImage = SIO.Path.Combine(newsImagePath, Request.Form["NewsImage"]);
                    //删除原先的老图片
                    if (!SIO.File.Exists(SIO.Path.Combine(rootPath, newsImagePath)))
                    {
                        SIO.Directory.CreateDirectory(SIO.Path.Combine(rootPath, newsImagePath));
                    }
                    SIO.File.Move(path, SIO.Path.Combine(rootPath, model.NewsImage));
                    model.NewsDescription = Request.Form["NewsImageDescription"] ?? "";
                }
                else if (string.IsNullOrEmpty(model.NewsImage))
                {
                    model.NewsDescription = "";
                }
            }
            else if (string.IsNullOrEmpty(model.NewsImage))
            {
                model.NewsDescription = "";
            }
            int reuslt = 0;
            if (model.Id > 0)
            {
                model.MvcDefaultEdit(userInfo.EmpID);
                op.Edit(model);
            }
            else
            {
                model.NewsDate = System.DateTime.Now;
                model.MvcDefaultSave(userInfo.EmpID);
                op.Add(model);
            }
            op.UnitOfWork.SaveChanges();

            using (var ba = new DBSql.Sys.BaseAttach())
            {
                ba.MoveFile(model.Id, userInfo.EmpID, userInfo.EmpName);
            }

            reuslt = model.Id;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion
        /// <summary>
        /// 查看
        /// </summary>
        /// <returns></returns>
        public ActionResult Query(int id)
        {
            var model = op.Get(id);
            ViewBag.NewsTypeText = model.FK_OaNew_NewsTypeID.BaseName;
            return View("Query", model);
        }

        public ActionResult Editor()
        {
            return View();
        }

        public FileResult ShowImage()
        {
            var path = HttpUtility.UrlDecode(Request.QueryString["path"] ?? "");
            if (string.IsNullOrEmpty(path) || !path.StartsWith("NewsImage\\"))
            {
                Response.StatusCode = 404;
                return null;
            }
            var filePath = SIO.Path.Combine(JQ.Util.ConfigUtil.GetConfigValue("UpFileRoot"), path);
            if (SIO.File.Exists(filePath))
            {
                return File(filePath, "application/octet-stream");
            }
            else
            {
                Response.StatusCode = 404;
                return null;
            }
        }

        public JsonResult UploadImage()
        {
            if (Request.Files.Count == 0)
            {
                return Json(new { Result = false, Message = "找不到图片" });
            }
            var extension = System.IO.Path.GetExtension(Request.Files[0].FileName).ToLower();
            if (extension == ".jpg" || extension == ".jpeg" || extension == ".bmp" || extension == ".png")
            {
                var fileName = Guid.NewGuid().ToString() + extension;
                Request.Files[0].SaveAs(System.IO.Path.Combine(JQ.Util.IOUtil.GetTempPath(), fileName));
                return Json(new { Result = true, FileName = fileName });
            }
            else
            {
                return Json(new { Result = false, Message = "图片格式不正确" });
            }
        }
    }
}
