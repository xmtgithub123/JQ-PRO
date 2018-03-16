using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.Xml;

namespace Core.Controllers
{
    [Description("BaseAttach")]
    public class BaseAttachController : CoreController
    {
        private DBSql.Sys.BaseAttach baseAttachDB = new DBSql.Sys.BaseAttach();
        private DBSql.Sys.BaseAttachVer baseAttachVerDB = new DBSql.Sys.BaseAttachVer();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 页面导航

        [Description("显示 附件版本历史 页面")]
        public ActionResult AttachHistory(long AttachID)
        {
            ViewBag.AttachID = AttachID;
            return View();
        }

        [Description("显示 附件详情 页面")]
        public ActionResult AttachInfo(long AttachID)
        {
            var model = baseAttachDB.Get(AttachID);

            ViewBag.Note = "";
            if (!string.IsNullOrWhiteSpace(model.ColAttXml))
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(model.ColAttXml);
                var xNote = (XmlElement)xmlDocument.SelectSingleNode("Root/Note");
                if (xNote != null)
                {
                    ViewBag.Note = xNote.InnerText;
                }
            }

            return View("AttachInfo", model);
        }

        [Description("显示 附件历史版本详情 页面")]
        public ActionResult AttachVerInfo(long AttachVerID)
        {
            var model = baseAttachVerDB.Get(AttachVerID);

            ViewBag.BaseAttach = baseAttachDB.Get(model.AttachID);
            return View("AttachVerInfo", model);
        }

        #endregion

        #region 获取数据

        [Description("获取 附件版本历史")]
        public string GetAttachHistroyListJson(long AttachID)
        {
            var histroyList = baseAttachVerDB.GetList(x => x.AttachID == AttachID)
                .OrderBy(x => x.AttachVer)
                .Select(x => new
                {
                    AttachVerId = x.AttachVerId,
                    AttachID = x.AttachID,
                    AttachSize = x.AttachSize,
                    AttachDateUpload = x.AttachDateUpload,
                    AttachDateChange = x.AttachDateChange,
                    AttachEmpID = x.AttachEmpID,
                    AttachEmpName = x.AttachEmpName,
                    AttachVer = x.AttachVer
                })
                .ToList();

            return JavaScriptSerializerUtil.getJson(new
            {
                total = histroyList.Count(),
                rows = histroyList
            });
        }

        #endregion

        #region 保存数据

        /// <summary>
        /// 保存 附件备注信息
        /// </summary>
        /// <returns></returns>
        public JsonResult SaveAttachNote()
        {
            var attachID = JQ.Util.TypeParse.parse<int>(Request.Form["attachID"]);
            if (attachID == 0)
            {
                return Json(new { Result = false, Message = "参数错误" });
            }
            baseAttachDB.UpdateNote(attachID, Request.Form["attachnote"] ?? "");
            return Json(new { Result = true });
        }

        #endregion
    }
}
