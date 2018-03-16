using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System;
using System.Reflection;

namespace Pay.Controllers
{
    /// <summary>
    /// 比例设置
    /// </summary>
    public class Percent
    {
        /// <summary>
        /// 公司提成比例
        /// </summary>
        public decimal CompanyPercent { get; set; }
        /// <summary>
        /// 工程类别比例
        /// </summary>
        public decimal EngiTypePercent { get; set; }
        /// <summary>
        /// 工程难度比例
        /// </summary>
        public decimal EngiHardPercent { get; set; }
        /// <summary>
        /// 项目经理比例
        /// </summary>
        public decimal ProjManagerPercent { get; set; }
        /// <summary>
        /// 电器一次比例
        /// </summary>
        public decimal EletricFirst { get; set; }
        /// <summary>
        /// 电器二次比例
        /// </summary>
        public decimal EletricSecond { get; set; }
        /// <summary>
        /// 土建
        /// </summary>
        public decimal DirtyConstruct { get; set; }
        /// <summary>
        /// 线路
        /// </summary>
        public decimal LineRoad { get; set; }
        /// <summary>
        /// 通讯
        /// </summary>
        public decimal Communication { get; set; }
        /// <summary>
        /// 技经
        /// </summary>
        public decimal TechBalace { get; set; }
        /// <summary>
        /// 测量
        /// </summary>
        public decimal TestExtend { get; set; }
        /// <summary>
        /// 系统
        /// </summary>
        public decimal SystemPercent { get; set; }
        /// <summary>
        /// 设计 比例
        /// </summary>
        public decimal DesPercent { get; set; }
        /// <summary>
        /// 校对比例
        /// </summary>
        public decimal Checkpercent { get; set; }
        /// <summary>
        /// 审核比例
        /// </summary>
        public decimal ReviewPercent { get; set; }
        public Percent()
        {
            CompanyPercent = 0;
            EngiHardPercent = 0;
            EngiTypePercent = 0;
            ProjManagerPercent = 0;
            EletricFirst = 0;
            EletricSecond = 0;
            DirtyConstruct = 0;
            LineRoad = 0;
            Communication = 0;
            TechBalace = 0;
            TestExtend = 0;
            SystemPercent = 0;
            DesPercent = 0;
            Checkpercent = 0;
            ReviewPercent = 0;

        }
    }

    [Description("PayModel")]
    public class PayModelController : CoreController
    {
        private DBSql.Pay.PayModel op = new DBSql.Pay.PayModel();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("BalanceModel")));
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
            var percent = new Percent();
            var targetLsit = from item in list
                             let engiVol = item.FK_PayModel_EngVolID
                             let phase = item.FK_PayModel_EngPhaseID
                             let model = percent.XmlToModel(item.ModelXML)
                             select new
                             {
                                 item.Id,
                                 item.ModelName,
                                 VolName = engiVol == null ? "" : engiVol.BaseName,
                                 PhaseName = phase == null ? "" : phase.BaseName,
                                 CompanyPercent = model == null ? 0 : model.CompanyPercent,//公司提奖比例（%）
                                 EngiTypePercent = model == null ? 0 : model.EngiTypePercent,//工程类别比例（%）
                                 EngiHardPercent = model == null ? 0 : model.EngiHardPercent,//工程难度系数
                                 ProjManagerPercent = model == null ? 0 : model.ProjManagerPercent,
                                 EletricFirst = model == null ? 0 : model.EletricFirst,
                                 EletricSecond = model == null ? 0 : model.EletricSecond,
                                 DirtyConstruct = model == null ? 0 : model.DirtyConstruct,
                                 LineRoad = model == null ? 0 : model.LineRoad,
                                 Communication = model == null ? 0 : model.Communication,
                                 TechBalace = model == null ? 0 : model.TechBalace,
                                 TestExtend = model == null ? 0 : model.TestExtend,
                                 SystemPercent = model == null ? 0 : model.SystemPercent,
                                 DesPercent = model == null ? 0 : model.DesPercent,//设计比例
                                 Checkpercent = model == null ? 0 : model.Checkpercent,//校对比例
                                 ReviewPercent = model == null ? 0 : model.ReviewPercent// 审核比例

                             };

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = targetLsit
            });
        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new PayModel();
            var percent = new Percent();
            model.MvcSetValue();
            BindViewData(percent);
            ViewBag.permission = "['add','submit']";
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            Percent percent = new Percent();
            if (!string.IsNullOrEmpty(model.ModelXML))
            {
                percent = percent.XmlToModel(model.ModelXML);
            }
            BindViewData(percent);
            ViewBag.permission = "['add','submit']";
            return View("info", model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 0;
            op.Delete(s => id.Contains(s.Id));
            op.UnitOfWork.SaveChanges();
            reuslt++;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int id)
        {
            var model = new PayModel();
            var percent = new Percent();
            if (id > 0)
            {
                model = op.Get(id);
            }
            model.MvcSetValue();
            percent.MvcSetValue();//获取要存入表单的数据
            string xml = percent.ModelToXml();//转化
            model.ModelXML = xml;
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
        /// 绑定ViewData
        /// </summary>
        /// <param name="percent"></param>
        public void BindViewData(Percent percent)
        {
            Type t = percent.GetType();
            PropertyInfo[] info = t.GetProperties();
            foreach (PropertyInfo p in info)
            {
                ViewData[p.Name] = p.GetValue(percent, null);
            }
        }

        #region   下拉选择绩效模板(combogrid)
        [HttpPost]
        [Description("绩效模板下拉")]
        public string ComboGridJson()
        {
            int PageNum = TypeHelper.parseInt(Request.Form["page"], 10);
            int Record = TypeHelper.parseInt(Request.Form["rows"], 1);
            var condition = TypeHelper.parseString(Request.Form["Words"]).Trim();

            string PhaseIDs = Request.Params["PhaseID"];
            string ProjectVolt = Request.Params["ProjectVolt"];
            int PayModelID = TypeHelper.parseInt(Request.Params["PayModelID"]);

            var list = op.GetList(p => p.Id != PayModelID).ToList();//排除已经选择的信息
            if (PhaseIDs != null)
            {
                List<int> PhaseListID = StringToListInt(PhaseIDs);
                list = list.Where(p => PhaseListID.Contains(p.EngPhaseID)).ToList();//根据阶段来进行筛选
            }
            if (ProjectVolt != null)
            {
                int Volt = TypeHelper.parseInt(ProjectVolt);
                list = list.Where(p => p.EngVolID==Volt).ToList();//根据电压等级进行筛选
            }
            List<dynamic> dyList = new List<dynamic>();
            object obj = null;
            var percent = new Percent();
            var a = (from item in list
                     let engiVol = item.FK_PayModel_EngVolID
                     let phase = item.FK_PayModel_EngPhaseID

                     where item.ModelName.Contains(condition)//根据模板名称进行筛选
                     orderby item.Id descending
                     select new
                     {
                         item.Id,
                         item.ModelName,
                         VolName = engiVol == null ? "" : engiVol.BaseName,
                         PhaseName = phase == null ? "" : phase.BaseName,
                         CompanyPercent = percent.XmlToModel(item.ModelXML) == null ? 0 : percent.XmlToModel(item.ModelXML).CompanyPercent,//公司提奖比例（%）
                         EngiTypePercent = percent.XmlToModel(item.ModelXML) == null ? 0 : percent.XmlToModel(item.ModelXML).EngiTypePercent,//工程类别比例（%）
                         EngiHardPercent = percent.XmlToModel(item.ModelXML) == null ? 0 : percent.XmlToModel(item.ModelXML).EngiHardPercent,//工程难度系数
                         ProjManagerPercent = percent.XmlToModel(item.ModelXML) == null ? 0 : percent.XmlToModel(item.ModelXML).ProjManagerPercent,//设总比例
                         DesPercent = percent.XmlToModel(item.ModelXML) == null ? 0 : percent.XmlToModel(item.ModelXML).DesPercent,//设计比例
                         Checkpercent = percent.XmlToModel(item.ModelXML) == null ? 0 : percent.XmlToModel(item.ModelXML).Checkpercent,//校对比例
                         ReviewPercent = percent.XmlToModel(item.ModelXML) == null ? 0 : percent.XmlToModel(item.ModelXML).ReviewPercent// 审核比例
                     }).ToList();

            DataModel.Models.PayModel modelPayModel = op.Get(PayModelID);
            if (modelPayModel != null)
            {
                string VolName = modelPayModel.FK_PayModel_EngVolID == null ? "" : modelPayModel.FK_PayModel_EngVolID.BaseName;//电压等级
                string PhaseName = modelPayModel.FK_PayModel_EngPhaseID == null ? "" : modelPayModel.FK_PayModel_EngPhaseID.BaseName; // 阶段名称
                decimal CompanyPercent = percent.XmlToModel(modelPayModel.ModelXML) == null ? 0 : percent.XmlToModel(modelPayModel.ModelXML).CompanyPercent;//公司提奖比例（%）
                decimal EngiTypePercent = percent.XmlToModel(modelPayModel.ModelXML) == null ? 0 : percent.XmlToModel(modelPayModel.ModelXML).EngiTypePercent;//工程类别比例（%）
                decimal EngiHardPercent = percent.XmlToModel(modelPayModel.ModelXML) == null ? 0 : percent.XmlToModel(modelPayModel.ModelXML).EngiHardPercent;//工程难度系数
                decimal ProjManagerPercent = percent.XmlToModel(modelPayModel.ModelXML) == null ? 0 : percent.XmlToModel(modelPayModel.ModelXML).ProjManagerPercent;//设总比例
                decimal DesPercent = percent.XmlToModel(modelPayModel.ModelXML) == null ? 0 : percent.XmlToModel(modelPayModel.ModelXML).DesPercent;//设计比例
                decimal Checkpercent = percent.XmlToModel(modelPayModel.ModelXML) == null ? 0 : percent.XmlToModel(modelPayModel.ModelXML).Checkpercent;//校对比例
                decimal ReviewPercent = percent.XmlToModel(modelPayModel.ModelXML) == null ? 0 : percent.XmlToModel(modelPayModel.ModelXML).ReviewPercent;// 审核比例
                obj = new {
                    modelPayModel.Id,
                    modelPayModel.ModelName,
                    VolName,
                    PhaseName,
                    CompanyPercent,
                    EngiTypePercent,
                    EngiHardPercent,
                    ProjManagerPercent,
                    DesPercent,
                    Checkpercent,
                    ReviewPercent
                };
                dyList.Add(obj);
            }
            dyList.AddRange(a);
            return JavaScriptSerializerUtil.getJson(new
            {
                total = dyList.Count,
                rows = (dyList.Skip(Record * (PageNum - 1)).Take(Record)).ToList()
            });

        }
        #endregion


        /// <summary>
        /// 返回List信息
        /// </summary>
        /// <param name="IDS"></param>
        /// <returns></returns>
        public List<int> StringToListInt(string IDS)
        {
            List<int> list = new List<int>();
            if (!string.IsNullOrEmpty(IDS))
            {
                string[] listStrID = IDS.Split(',');
                foreach (string StrID in listStrID)
                {
                    if (!string.IsNullOrEmpty(StrID) && StrID != "0")
                    {
                        int Id = TypeHelper.parseInt(StrID);
                        list.Add(Id);
                    }
                }
            }
            return list;
        }
    }
}
