using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System;

namespace Core.Controllers
{
    [Description("BaseConfig")]
    public class BaseConfigController : CoreController
    {
        private DBSql.Sys.BaseConfig op = new DBSql.Sys.BaseConfig();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("SystemConfig")));
            return View();
        }

        [Description("系统配置")]
        public ActionResult reglist()
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
                PageModel.SelectOrder = "ConfigOrder asc";
            }
            var list = op.GetPagedDynamic(PageModel).ToList();

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
            var model = new BaseConfig();
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
            op.Delete(s => id.Contains(s.ConfigID));
            op.UnitOfWork.SaveChanges();
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int ConfigID)
        {
            var model = new BaseConfig();
            if (ConfigID > 0)
            {
                model = op.Get(ConfigID);
            }
            model.MvcSetValue();
            model.MvcDefaultSave(userInfo.EmpID);
            int reuslt = 0;
            if (model.ConfigID > 0)
            {
                op.Edit(model);
            }
            else
            {
                op.Add(model);
            }
            op.UnitOfWork.SaveChanges();
            op.CacheRemove();
            reuslt = model.ConfigID;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.ConfigID, "操作成功") : DoResultHelper.doSuccess(model.ConfigID, "操作失败");
            return Json(dr);
        }
        #endregion



        #region 查看软件注册情况

        public ActionResult JinQuRegStatus()
        {
            string code = op.GetJinquCode();
            DoResult dr = DoResultHelper.doSuccess(code);
            return Json(dr);
        }

        public ActionResult RegStatus()
        {
            string code = op.GetRegStatus();

            var result = "注册成功";
            if (!string.IsNullOrEmpty(code))
            {
                result = code.Replace(";", "\n");
            }

            DoResult dr = DoResultHelper.doSuccess(result);
            return Json(dr);
        }

        public ActionResult RegServerCode()
        {
            string code = new Common.SoftReg().getMNum();
            DoResult dr = DoResultHelper.doSuccess(code);
            return Json(dr);
        }
        #endregion
    }
}
