using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System;
using Common.Data.Extenstions;

namespace Iso.Controllers
{
    [Description("IsoFile")]
    public class IsoFileController : CoreController
    {
        private DBSql.Iso.IsoFile op = new DBSql.Iso.IsoFile();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("发布列表")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("UsefulVer")));
            return View();
        }
        #endregion

        [Description("作废列表")]
        public ActionResult listDel()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("UnusefulVer")));
            return View();
        }

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string json()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "FileID desc";
            }
            var TWhere = QueryBuild<DataModel.Models.IsoFile>.True().And(f => f.DeleterEmpId == 0);
            if (Request.Form["isZF"] == "1")//作废
            {
                TWhere = TWhere.And(f => f.FileIsModules == 1);
                List<string> result = PermissionList("UnusefulVer");

                if (!(result.Contains("allview")|| result.Contains("alledit")))
                {
                    if (result.Contains("dep"))
                    {
                        TWhere = TWhere.And(f => f.CreatorDepID == this.userInfo.EmpDepID);
                    }
                    else
                    {
                        TWhere = TWhere.And(f => f.CreatorEmpId == this.userInfo.EmpID);
                    }
                }

            }
            else//发布
            {
                TWhere = TWhere.And(f => f.FileIsModules == 0);
                List<string> result = PermissionList("UsefulVer");

                if (!(result.Contains("allview") || result.Contains("alledit")))
                {
                    if (result.Contains("dep"))
                    {
                        TWhere = TWhere.And(f => f.CreatorDepID == this.userInfo.EmpDepID);
                    }
                    else
                    {
                        TWhere = TWhere.And(f => f.CreatorEmpId == this.userInfo.EmpID);
                    }
                }
            }

            var list = op.GetPagedList(TWhere, PageModel).ToList();

            var target = (from item in list
                          select new
                          {
                              item.FileID,
                              item.FileNumber,
                              item.FileName,
                              FileType = item.FileType == 0 ? "表单" : "文件",
                              item.FileVersion,
                              item.FileDate,
                              item.FileZFDate,
                              FileIsModules = item.FileIsModules == 0 ? "启用" : "作废",
                          });

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
            var model = new IsoFile();
            SetStatusDropDownList();
            SetTypeDropDownList();//
            model.FileTypeName = userInfo.EmpName;
            ViewBag.editPermission = "['submit','close']";
            return View("info", model);
        }
        #endregion

        public ActionResult addDel()
        {
            var model = new IsoFile();
            SetStatusDropDownList();
            SetTypeDropDownList();
            model.FileTypeName = userInfo.EmpName;
            ViewBag.editPermission = "['submit','close']";
            return View("infoDel", model);
        }

        public ActionResult editDel(int id)
        {
            var model = op.Get(id);
            SetStatusDropDownList();
            SetTypeDropDownList();
            ViewBag.edit = 1;
            List<string> result = PermissionList("UnusefulVer");
            if (result.Contains("edit") || result.Contains("alledit"))//编辑权或者维护权
            {
                ViewBag.editPermission = "['submit','close']";
            }
            else
            {
                ViewBag.editPermission = "['close']";
            }
            return View("infoDel", model);
        }

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            SetStatusDropDownList(model.FileIsModules);
            SetTypeDropDownList();
            List<string> result = PermissionList("UsefulVer");
            if (result.Contains("edit") || result.Contains("alledit"))//编辑权或者维护权
            {
                ViewBag.editPermission = "['submit','close']";
            }
            else
            {
                ViewBag.editPermission = "['close']";
            }
            return View("info", model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            //int reuslt = 0;
            //op.Delete(s => id.Contains(s.FileID));
            //op.UnitOfWork.SaveChanges();
            //DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            //return Json(dr);

            int reuslt = 0;
            try
            {
                op.UpdateIsoFileList(id, this.userInfo);
                op.UnitOfWork.SaveChanges();

                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.Delete(id, "BusinessInfoRecord");
                }
                reuslt = 1;
            }
            catch (Exception)
            {

            }

            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion

        public ActionResult saveDel(int FileID)
        {
            var model = new DataModel.Models.IsoFile();

            if (FileID > 0)
            {
                model = op.Get(FileID);
                model.MvcSetValueExceptKeys("model", "id");
            }
            else
            {

                model.MvcSetValue();
            }

            if (model.FileIsModules == 0)//启用
            {
                model.FileZFDate = new DateTime(1900, 01, 01);
            }
            if (model.FileID > 0)
            {
                model.MvcDefaultEdit(userInfo.EmpID);
                op.Edit(model);
                op.UnitOfWork.SaveChanges();
            }
            else
            {
                model.MvcDefaultSave(userInfo.EmpID);
                op.Add(model);
                op.UnitOfWork.SaveChanges();
                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.MoveFile(model.FileID, userInfo.EmpID, userInfo.EmpName);
                }
            }


            int reuslt = model.FileID;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.FileID, "操作成功") : DoResultHelper.doSuccess(model.FileID, "操作失败");
            return Json(dr);
        }


        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int FileID)
        {
            var model = new DataModel.Models.IsoFile();
            if (FileID > 0)
            {
                model = op.Get(FileID);
                model.MvcSetValueExceptKeys("model", "id");
            }
            else
            {
                model.MvcSetValue();
            }

            if (model.FileIsModules == 1)//作废
            {
                model.FileZFDate = DateTime.Now;
            }
            int reuslt = 0;
            if (model.FileID > 0)
            {
                model.MvcDefaultEdit(userInfo.EmpID);
                op.Edit(model);
                op.UnitOfWork.SaveChanges();
            }
            else
            {
                model.MvcDefaultSave(userInfo.EmpID);
                op.Add(model);
                op.UnitOfWork.SaveChanges();
                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.MoveFile(model.FileID, userInfo.EmpID, userInfo.EmpName);
                }
            }
            reuslt = model.FileID;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.FileID, "操作成功") : DoResultHelper.doSuccess(model.FileID, "操作失败");
            return Json(dr);
        }
        #endregion


        public string GetJson()
        {
            int PageNum = TypeHelper.parseInt(Request.Form["page"]);
            int Record = TypeHelper.parseInt(Request.Form["rows"]);
            var condition = TypeHelper.parseString(Request.Form["keyword"]).Trim();

            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "FileID desc";
            }
            var list = op.GetPagedList(f => f.FileIsModules == 1, PageModel);
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });

        }


        #region 下拉框
        private void SetStatusDropDownList(int id = 0)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = "启用", Value = "0", Selected = id == 0 ? true : false });
            list.Add(new SelectListItem() { Text = "作废", Value = "1", Selected = id == 1 ? true : false });
            ViewData["statusdata"] = list;
        }

        private void SetTypeDropDownList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = "表单", Value = "0" });
            list.Add(new SelectListItem() { Text = "文件", Value = "1" });
            ViewData["typedata"] = list;
        }
        #endregion

    }
}
