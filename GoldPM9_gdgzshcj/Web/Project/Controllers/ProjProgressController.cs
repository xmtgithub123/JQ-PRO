﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.Xml;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System;
using System.Linq.Expressions;
using System.Text;
using Common.Data.Extenstions;
//using Common.Data.Extenstions;

namespace Project.Controllers
{
    public class ProjProgressInfo
    {
        public string DatePlanStart { get; set; }
        public string DatePlanFinish { get; set; }

        public string DraftDatePlanStart { get; set; }
        public string DraftDatePlanFinish { get; set; }
        public ProjProgressInfo()
        {
            DatePlanStart = "";
            DatePlanFinish = "";
            DraftDatePlanStart = "";
            DraftDatePlanFinish = "";
        }
    }
    public class ProjProgressController : CoreController
    {
        private DBSql.Project.ProjSub op = new DBSql.Project.ProjSub();
        private DBSql.Iso.IsoForm dbIsoForm = new DBSql.Iso.IsoForm();
        private DBSql.Project.Project project = new DBSql.Project.Project();
        DBSql.Sys.BaseEmployee dbBaseEmployee = new DBSql.Sys.BaseEmployee();
        private DBSql.Bussiness.BussContractSub bussContractSub = new DBSql.Bussiness.BussContractSub();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjProgressExchange")));
            return View();
        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string json()
        {
            List<string> result = PermissionList("ProjProgressExchange");

            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();

            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "FormID desc";
            }
            DataModel.Models.FlowModel dmFlowModel = dbIsoForm.DbContext.Set<DataModel.Models.FlowModel>().FirstOrDefault(p => p.ModelRefTable == "IsoFormProjProgressAdjust");
            if (null == dmFlowModel)
            {
                return JavaScriptSerializerUtil.getJson(new
                {
                    total = 0,
                    rows = new List<DataModel.Models.IsoForm>()
                });
            }
            var TWhere = QueryBuild<DataModel.Models.IsoForm>.True().And(m => m.DeleterEmpId == 0 && m.FormTypeID == dmFlowModel.Id);

            if (!(result.Contains("allview") ||result.Contains("alledit")))
            {
                if (result.Contains("dep"))
                {
                    TWhere = TWhere.And(p => p.CreatorDepId == this.userInfo.EmpDepID);
                }
                else
                {
                    TWhere = TWhere.And(p => p.CreatorEmpId == this.userInfo.EmpID);
                }
            }
          
            var list = dbIsoForm.GetPagedList(TWhere, PageModel).ToList();
            ProjProgressInfo info = new ProjProgressInfo();
            var dataResult = (from t in list
                              join t1 in dbIsoForm.DbContext.Set<DataModel.Models.Flow>() on t.FormID equals t1.FlowRefID into nodes
                              from temp in nodes.DefaultIfEmpty()
                              where temp == null || temp.FlowRefTable == "IsoFormProjProgressAdjust" || temp.FlowRefTable == null
                              let model = info.XmlToModel(t.FormCtlXml)
                              join p in dbIsoForm.DbContext.Set<DataModel.Models.Project>()
                              on t.ProjID equals p.Id
                              join b1 in dbIsoForm.DbContext.Set<DataModel.Models.BaseData>()
                              on t.PhaseID equals b1.BaseID
                              join b2 in dbIsoForm.DbContext.Set<DataModel.Models.BaseData>()
                              on t.SpecID equals b2.BaseID
                              join d in dbIsoForm.DbContext.Set<DataModel.Models.DesTask>()
                              on t.TaskID equals d.Id
                              into temp1
                              from t2 in temp1.DefaultIfEmpty()
                              select new
                              {
                                  t.FormID,
                                  p.ProjNumber,
                                  p.ProjName,
                                  PhaseName = b1.BaseName,
                                  ProjectType = p.FK_Project_ProjTypeID == null ? "" : p.FK_Project_ProjTypeID.BaseName,
                                  SpecilName = b2.BaseName,
                                  t2.TaskName,
                                  t.FormReason,
                                  t.FormNote,
                                  DatePlanFinish = model == null ? "" : model.DatePlanFinish,
                                  DraftDatePlanFinish = model == null ? "" : model.DraftDatePlanFinish,
                                  t.CreatorEmpName,
                                  t.CreatorEmpId,
                                  FlowID = temp == null ? 0 : temp.Id,
                                  FlowName = temp == null ? "" : temp.FlowName,
                                  FlowStatusID = temp == null ? 0 : temp.FlowStatusID,
                                  FlowStatusName = temp == null ? "" : temp.FlowStatusName,
                                  FlowXml = temp == null ? "" : temp.FlowXml
                              }).ToList().Select(m => new { m.FormID, m.ProjNumber, m.ProjName, m.PhaseName, m.ProjectType, m.SpecilName, m.TaskName, m.FormReason, m.FormNote, m.DatePlanFinish, m.DraftDatePlanFinish, m.CreatorEmpId, m.CreatorEmpName, m.FlowID, m.FlowName, m.FlowStatusID, m.FlowStatusName, FlowTurnedEmpIDs = JQ.Util.TypeParse.GetTurnedEmpIDs(m.FlowXml) });



            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = dataResult
            });
        }
        #endregion       

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new IsoForm();
            ViewBag.CreatorEmpId = model.CreatorEmpId;
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = dbIsoForm.Get(id);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(model.FormCtlXml);

            ViewBag.ProjNumber = doc.SelectNodes("/root/ProjNumber")[0].InnerText;
            ViewBag.ProjName = doc.SelectNodes("/root/ProjName")[0].InnerText;
            ViewBag.PhaseName = doc.SelectNodes("/root/PhaseName")[0].InnerText;
            ViewBag.ProjEmpName = doc.SelectNodes("/root/ProjEmpName")[0].InnerText;
            ViewBag.DatePlanStart = doc.SelectNodes("/root/DatePlanStart")[0].InnerText;
            ViewBag.DatePlanFinish = doc.SelectNodes("/root/DatePlanFinish")[0].InnerText;
            ViewBag.DraftDatePlanStart = doc.SelectNodes("/root/DraftDatePlanStart")[0].InnerText;
            ViewBag.DraftDatePlanFinish = doc.SelectNodes("/root/DraftDatePlanFinish")[0].InnerText;
            ViewBag.CreatorEmpId = model.CreatorEmpId;
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
                dbIsoForm.Delete(s => id.Contains(s.FormID));
                op.UnitOfWork.SaveChanges();
                new DBSql.PubFlow.Flow().Delete(id, "IsoFormProjProgressAdjust");
                reuslt = 1;
            }
            catch
            {
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
            var model = new ProjSub();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();
            model.SubEmpId = TypeHelper.parseInt(Request.Form["SubEmpId"].Split('-')[0]);
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
            reuslt = model.Id;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }

        #endregion

        #region 筛选项目外委列表json
        public ActionResult selectprojsub()
        {
            return View();

        }
        #endregion

        public string GetProjSubList(FormCollection Tcondition)
        {
            string ProjSubIDs = "";
            if (Tcondition["ProjSubIDs"] != null)
            {
                ProjSubIDs = Tcondition["ProjSubIDs"].ToString();
            }
            else if (Tcondition["ProjSubIDs[]"] != null)
            {
                ProjSubIDs = Tcondition["ProjSubIDs[]"].ToString();
            }
            int[] Id = (from n in ProjSubIDs.Split(',') where n != "" select Common.ExtensionMethods.Value<int>(n)).ToArray();
            var TWhere = QueryBuild<DataModel.Models.ProjSub>.True();
            if (Id.Length == 0)
            {
                TWhere = TWhere.And(p => p.Id == -1);
            }
            else
            {
                var Ids = string.Join(",", Id.Select(p => p.ToString()));
                TWhere = TWhere.And(p => Id.Contains(p.Id));
            }

            var list = new DBSql.Project.ProjSub().GetList(TWhere).Select(p => new
            {
                Id = p.Id,
                p.SubNumber,
                p.SubName,
                ProjNumber = p.FK_ProjSub_ProjID.ProjNumber,
                ProjName = p.FK_ProjSub_ProjID.ProjName,
                p.SubFee,
                p.SubPlanFinishDate,
                p.SubFactFinishDate,

            }).ToList();

            return JavaScriptSerializerUtil.getJson(new
            {
                total = list.Count,
                rows = list

            });
            return "";
        }



        public string GetJson()
        {
            int PageNum = TypeHelper.parseInt(Request.Form["page"]);
            int Record = TypeHelper.parseInt(Request.Form["rows"]);
            var condition = TypeHelper.parseString(Request.Form["keyword"]).Trim();

            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            var list = op.GetQuery().ToList();

            List<DataModel.Models.ProjSub> result = (from item in list
                                                     where item.SubName.Contains(condition)    //条件查询
                                                     select item).ToList<DataModel.Models.ProjSub>();
            //没有外委合同的外键，只能连接查询
            var conSub = bussContractSub.GetList(c => true).ToList();

            var a = (from item in result
                     join c in conSub
                     on item.ConSubID equals c.Id
                     select new
                     {
                         item.Id,
                         item.SubName,
                         item.SubNumber,
                         item.SubPhase,
                         item.SubSpecial,
                         item.SubType,
                         CustName = item.FK_ProjSub_ColAttType2.CustName,
                         ConSubName = c.ConSubName,
                         ConSubNumber = c.ConSubNumber
                     }).ToList();
            var t = (a.Skip(Record * (PageNum - 1)).Take(Record)).ToList();
            return JavaScriptSerializerUtil.getJson(new
            {
                total = a.Count,
                rows = (a.Skip(Record * (PageNum - 1)).Take(Record)).ToList()
            });

        }

    }



}