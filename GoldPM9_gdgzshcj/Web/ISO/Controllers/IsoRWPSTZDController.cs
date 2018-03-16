using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System;
using Common.Data.Extenstions;
using System.Xml;

namespace Iso.Controllers
{
    [Description("IsoRWPSTZD")]
    public class IsoRWPSTZDController : CoreController
    {
        private DBSql.Iso.IsoRWPSTZD op = new DBSql.Iso.IsoRWPSTZD();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        private DBSql.Sys.BaseData dbBaseData = new DBSql.Sys.BaseData();
        private DBSql.Project.Project dbProject = new DBSql.Project.Project();

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjectCenterList")));
            return View();
        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string json()
        {
            List<string> result = PermissionList("ProjectCenterList");
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            var queryInfo = Request.Form["queryInfo"];
            List<Common.Data.Extenstions.Filter> Filter = JavaScriptSerializerUtil.objectToEntity<List<Common.Data.Extenstions.Filter>>(queryInfo);
            if (queryInfo.isNotEmpty())
            {
                foreach (Common.Data.Extenstions.Filter item in Filter)
                {
                    Common.Data.Extenstions.FilterChilde _child = item.list[0];
                    if (_child.Key == "txtLike")
                    {
                        PageModel.TextCondtion = _child.Value;
                    }
                    else if (_child.Key == "CTStartTime")
                    {
                        PageModel.SelectCondtion.Add("CTStartTime", _child.Value);
                    }
                    else if (_child.Key == "CTEndTime")
                    {
                        PageModel.SelectCondtion.Add("CTEndTime", _child.Value);
                    }
                    else if (_child.Key == "TaskPursuantState")
                    {
                        PageModel.SelectCondtion.Add("TaskPursuantState", _child.Value);
                    }
                    else if (_child.Key == "AppraisalWayState")
                    {
                        PageModel.SelectCondtion.Add("AppraisalWayState", _child.Value);
                    }
                    else if (_child.Key == "ProjectClassState")
                    {
                        PageModel.SelectCondtion.Add("ProjectClassState", _child.Value);
                    }
                }
            }

            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "ISO.Id desc";
            }
            if (!(result.Contains("allview") || result.Contains("alledit")))
            {
                if (result.Contains("dep"))
                {
                    PageModel.SelectCondtion.Add("QueryDeptID", this.userInfo.EmpDepID);
                }
                else
                {
                    PageModel.SelectCondtion.Add("QueryEmpID", this.userInfo.EmpID);
                }
            }
            var dt = op.GetListInfo(PageModel, this.userInfo);

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = dt
            });
        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {

            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjectCenterList")));

            var model = new IsoRWPSTZD();

            DataModel.Models.Project project = new DBSql.Project.Project().Get(GetRequestValue<int>("projId"));

            DataModel.Models.DesTaskGroup dtg = new DBSql.Design.DesTaskGroup().Get(GetRequestValue<int>("taskGroupId"));
            if (dtg != null)
            {
                model.ProjID = dtg.ProjId;
                model.ProjName = dtg.ProjName;
                model.ProjNumber = dtg.ProjNumber;
                model.ProjPhaseId = GetRequestValue<string>("phaseID");
                model.ProjPhaseName = new DBSql.Sys.BaseData().Get(GetRequestValue<int>("phaseID")).BaseName;
            }

            model.TableNumber = "SCX02-02";
            ViewBag.CurrEmpID = userInfo.EmpID;

            ViewBag.ProjDeptName = project.FK_Project_ProjDepId.BaseName;
            ViewBag.ColAttType1Name = project.FK_Project_ProjTypeID.BaseName;
            ViewBag.ProjAreaName = project.FK_Project_ProjAreaID.BaseName;

            ViewBag.CompanyName = project.CustName;
            ViewBag.CompanyLinkMan = project.CustLinkMan;
            ViewBag.CompanyTel = project.CustLinkTel;

            ViewBag.AppraisalContents1 = "";
            ViewBag.AppraisalContents2 = "";
            ViewBag.AppraisalContents3 = "";
            ViewBag.AppraisalContents4 = "";
            ViewBag.AppraisalContents5 = "";
            ViewBag.AppraisalContents6 = "";

            List<SelectListItem> itemsA = new List<SelectListItem>();
            SelectListItem itemA1 = new SelectListItem(); itemA1.Value = "1"; itemA1.Text = "是"; itemsA.Add(itemA1);
            SelectListItem itemA2 = new SelectListItem(); itemA2.Value = "2"; itemA2.Text = "否"; itemsA.Add(itemA2);
            ViewBag.itemsA = new SelectList(itemsA, "Value", "Text");

            List<SelectListItem> itemsB = new List<SelectListItem>();
            SelectListItem itemsB1 = new SelectListItem(); itemsB1.Value = "1"; itemsB1.Text = "是"; itemsB.Add(itemsB1);
            SelectListItem itemsB2 = new SelectListItem(); itemsB2.Value = "2"; itemsB2.Text = "否"; itemsB.Add(itemsB2);
            ViewBag.itemsB = new SelectList(itemsB, "Value", "Text");

            List<SelectListItem> itemsC = new List<SelectListItem>();
            SelectListItem itemsC1 = new SelectListItem(); itemsC1.Value = "1"; itemsC1.Text = "是"; itemsC.Add(itemsC1);
            SelectListItem itemsC2 = new SelectListItem(); itemsC2.Value = "2"; itemsC2.Text = "否"; itemsC.Add(itemsC2);
            ViewBag.itemsC = new SelectList(itemsC, "Value", "Text");

            List<SelectListItem> itemsD = new List<SelectListItem>();
            SelectListItem itemsD1 = new SelectListItem(); itemsD1.Value = "1"; itemsD1.Text = "是"; itemsD.Add(itemsD1);
            SelectListItem itemsD2 = new SelectListItem(); itemsD2.Value = "2"; itemsD2.Text = "否"; itemsD.Add(itemsD2);
            ViewBag.itemsD = new SelectList(itemsD, "Value", "Text");

            List<SelectListItem> itemsE = new List<SelectListItem>();
            SelectListItem itemsE1 = new SelectListItem(); itemsE1.Value = "1"; itemsE1.Text = "是"; itemsE.Add(itemsE1);
            SelectListItem itemsE2 = new SelectListItem(); itemsE2.Value = "2"; itemsE2.Text = "否"; itemsE.Add(itemsE2);
            ViewBag.itemsE = new SelectList(itemsE, "Value", "Text");

            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjectCenterList")));

            var model = op.Get(id);

            var _project = dbProject.Get(model.ProjID);

            var _projDeptName = dbBaseData.Get(_project.ProjDepId);
            ViewBag.ProjDeptName = null != _projDeptName ? _projDeptName.BaseName : "";

            var _colAttType1Name = dbBaseData.Get(_project.ColAttType1);
            ViewBag.ColAttType1Name = null != _colAttType1Name ? _colAttType1Name.BaseName : "";

            var _projAreaName = dbBaseData.Get(_project.ProjAreaID);
            ViewBag.ProjAreaName = null != _projAreaName ? _projAreaName.BaseName : "";

            ViewBag.CompanyName = _project.CustName;
            ViewBag.CompanyLinkMan = _project.CustLinkMan;
            ViewBag.CompanyTel = _project.CustLinkTel;

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(model.AppraisalContens);

            XmlNodeList listContents1 = xmlDocument.SelectNodes("Root/Item[@name='AppraisalContents1']");
            ViewBag.AppraisalContents1 = ((XmlNode)listContents1[0]).InnerText;
            XmlNodeList listContents2 = xmlDocument.SelectNodes("Root/Item[@name='AppraisalContents2']");
            ViewBag.AppraisalContents2 = ((XmlNode)listContents2[0]).InnerText;
            XmlNodeList listContents3 = xmlDocument.SelectNodes("Root/Item[@name='AppraisalContents3']");
            ViewBag.AppraisalContents3 = ((XmlNode)listContents3[0]).InnerText;
            XmlNodeList listContents4 = xmlDocument.SelectNodes("Root/Item[@name='AppraisalContents4']");
            ViewBag.AppraisalContents4 = ((XmlNode)listContents4[0]).InnerText;
            XmlNodeList listContents5 = xmlDocument.SelectNodes("Root/Item[@name='AppraisalContents5']");
            ViewBag.AppraisalContents5 = ((XmlNode)listContents5[0]).InnerText;
            XmlNodeList listContents6 = xmlDocument.SelectNodes("Root/Item[@name='AppraisalContents6']");
            ViewBag.AppraisalContents6 = ((XmlNode)listContents6[0]).InnerText;


            XmlNodeList listContentsA = xmlDocument.SelectNodes("Root/Item[@name='itemsA']");
            XmlNode nodeA = (XmlNode)listContentsA[0];

            XmlNodeList listContentsB = xmlDocument.SelectNodes("Root/Item[@name='itemsB']");
            XmlNode nodeB = (XmlNode)listContentsB[0];

            XmlNodeList listContentsC = xmlDocument.SelectNodes("Root/Item[@name='itemsC']");
            XmlNode nodeC = (XmlNode)listContentsC[0];

            XmlNodeList listContentsD = xmlDocument.SelectNodes("Root/Item[@name='itemsD']");
            XmlNode nodeD = (XmlNode)listContentsD[0];

            XmlNodeList listContentsE = xmlDocument.SelectNodes("Root/Item[@name='itemsE']");
            XmlNode nodeE = (XmlNode)listContentsE[0];


            List<SelectListItem> itemsA = new List<SelectListItem>();
            SelectListItem itemA1 = new SelectListItem(); itemA1.Value = "1"; itemA1.Text = "是"; itemsA.Add(itemA1);
            SelectListItem itemA2 = new SelectListItem(); itemA2.Value = "2"; itemA2.Text = "否"; itemsA.Add(itemA2);
            ViewBag.itemsA = new SelectList(itemsA, "Value", "Text", null != nodeA ? nodeA.InnerText : "");
            ViewBag.stritemsA = null != nodeA ? nodeA.InnerText.TrimEnd(',') : "";


            List<SelectListItem> itemsB = new List<SelectListItem>();
            SelectListItem itemB1 = new SelectListItem(); itemB1.Value = "1"; itemB1.Text = "是"; itemsB.Add(itemB1);
            SelectListItem itemB2 = new SelectListItem(); itemB2.Value = "2"; itemB2.Text = "否"; itemsB.Add(itemB2);
            ViewBag.itemsB = new SelectList(itemsB, "Value", "Text", null != nodeB ? nodeB.InnerText : "");
            ViewBag.stritemsB = null != nodeB ? nodeB.InnerText.TrimEnd(',') : "";


            List<SelectListItem> itemsC = new List<SelectListItem>();
            SelectListItem itemC1 = new SelectListItem(); itemC1.Value = "1"; itemC1.Text = "是"; itemsC.Add(itemC1);
            SelectListItem itemC2 = new SelectListItem(); itemC2.Value = "2"; itemC2.Text = "否"; itemsC.Add(itemC2);
            ViewBag.itemsC = new SelectList(itemsC, "Value", "Text", null != nodeC ? nodeC.InnerText : "");
            ViewBag.stritemsC = null != nodeC ? nodeC.InnerText.TrimEnd(',') : "";



            List<SelectListItem> itemsD = new List<SelectListItem>();
            SelectListItem itemD1 = new SelectListItem(); itemD1.Value = "1"; itemD1.Text = "是"; itemsD.Add(itemD1);
            SelectListItem itemD2 = new SelectListItem(); itemD2.Value = "2"; itemD2.Text = "否"; itemsD.Add(itemD2);
            ViewBag.itemsD = new SelectList(itemsD, "Value", "Text", null != nodeD ? nodeD.InnerText : "");
            ViewBag.stritemsD = null != nodeD ? nodeD.InnerText.TrimEnd(',') : "";


            List<SelectListItem> itemsE = new List<SelectListItem>();
            SelectListItem itemE1 = new SelectListItem(); itemE1.Value = "1"; itemE1.Text = "是"; itemsE.Add(itemE1);
            SelectListItem itemE2 = new SelectListItem(); itemE2.Value = "2"; itemE2.Text = "否"; itemsE.Add(itemE2);
            ViewBag.itemsE = new SelectList(itemsE, "Value", "Text", null != nodeE ? nodeE.InnerText : "");
            ViewBag.stritemsE = null != nodeE ? nodeE.InnerText.TrimEnd(',') : "";


            return View("info", model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 0;
            try
            {
                op.UpdateIsoRWPSTZDInfo(id, this.userInfo);
                op.UnitOfWork.SaveChanges();

                new DBSql.PubFlow.Flow().Delete(id, "IsoBCRWTZD");
                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.Delete(id, "IsoBCRWTZD");
                }
                reuslt = 1;
            }
            catch
            {
                reuslt = 0;
            }
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id)
        {
            var model = new IsoRWPSTZD();
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
            reuslt = model.Id;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion
    }
}
