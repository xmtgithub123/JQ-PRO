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
using System.Web.Script.Serialization;

namespace ISO.Controllers
{
    [Description("IsoForm")]
    public class IsoFormProjectPublishController : CoreController
    {
        private DBSql.Iso.IsoForm op = new DBSql.Iso.IsoForm();
        private DBSql.Project.Project proj = new DBSql.Project.Project();
        private DBSql.Design.DesTask desTask = new DBSql.Design.DesTask();

        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjectPublish")));
            ViewBag.EmpId = userInfo.EmpID;
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
                PageModel.SelectOrder = "FormID desc";
            }
            var TWhere = QueryBuild<DataModel.Models.IsoForm>.True();
            string KeyJQSearch = Request.Form["KeyJQSearch"];
            string ProjectPhase = Request.Form["ProjectPhase"];
            if (!string.IsNullOrEmpty(KeyJQSearch))
            {
                TWhere = TWhere.And(p => p.FormReason.Contains(KeyJQSearch) || p.FormNote.Contains(KeyJQSearch) || p.FK_IsoForm_ProjID.ProjName.Contains(KeyJQSearch) || p.FK_IsoForm_ProjID.ProjNumber.Contains(KeyJQSearch));
            }
            if (!string.IsNullOrEmpty(ProjectPhase))
            {
                TWhere = TWhere.And(p => p.PhaseID.ToString().Contains((ProjectPhase)));
            }
            TWhere = TWhere.And((p => p.RefTable == "IsoFormProjectPublish"));

            List<string> result = PermissionList("ProjectPublish");
            if (!(result.Contains("allview") || result.Contains("alledit")))
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

            var td = op.GetPagedList(TWhere, PageModel).ToList();
            var list = (from p in td
                       join t1 in op.DbContext.Set<DataModel.Models.Flow>().Where(f => f.FlowRefTable == "IsoFormProjectPublish") on p.FormID equals t1.FlowRefID into nodes
                       from temp in nodes.DefaultIfEmpty()
                       select new
                       {
                           p.FormID,
                           CreationTime = p.CreationTime.ToString("yyyy-MM-dd"),//登记日期
                           p.CreatorEmpName,//登记人
                           p.CreatorEmpId,
                           p.FormReason,  //申请原因
                           p.FormNote,  //备注
                           ProjName = p.FK_IsoForm_ProjID == null ? "" : p.FK_IsoForm_ProjID.ProjName,  //项目名称
                           ProjNumber = p.FK_IsoForm_ProjID == null ? "" : p.FK_IsoForm_ProjID.ProjNumber,//项目编号
                           ProjPhaseIds = p.FK_IsoForm_ProjID == null ? "" : String.Join(",", GetBaseName(p.FK_IsoForm_ProjID.ProjPhaseIds)),//阶段
                           PhaseIDs = p.PhaseID == 0 ? "" : String.Join(",", GetBaseName(p.PhaseID.ToString())),//阶段
                           ProjEmpName = p.FK_IsoForm_ProjID == null ? "" : p.FK_IsoForm_ProjID.ProjEmpName,
                           ColAttVal1 = p.ColAttVal1 == "" ? "" : String.Join(",", GetBaseName(p.ColAttVal1)),
                           //FlowStatus = new DBSql.PubFlow.Flow().GetFlowStatusId("IsoFormProjectPublish", p.FormID, p.CreatorEmpId, this.userInfo.EmpID),
                           FlowID = temp == null ? 0 : temp.Id,
                           FlowName = temp == null ? "" : temp.FlowName,
                           FlowStatusID = temp == null ? 0 : temp.FlowStatusID,
                           FlowStatusName = temp == null ? "" : temp.FlowStatusName,
                           FlowXml = temp == null ? "" : temp.FlowXml
                       }).Select(m => new { m.FormID, m.CreationTime, m.CreatorEmpName, m.CreatorEmpId, m.FormReason, m.FormNote, m.ProjName, m.ProjNumber,m.PhaseIDs, m.ProjPhaseIds, m.ProjEmpName,m.ColAttVal1, m.FlowID, m.FlowName, m.FlowStatusID, m.FlowStatusName, m.FlowXml, FlowTurnedEmpIDs = JQ.Util.TypeParse.GetTurnedEmpIDs(m.FlowXml) });

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
            var model = new IsoForm();
            model.CreationTime = System.DateTime.Now;
            model.CreatorEmpName = userInfo.EmpName;
            model.CreatorDepName = userInfo.EmpDepName;
            ViewBag.permission = "['add','submit']";
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            BindProjName(model.ProjID);
            ViewBag.contentXml = model.FormCtlXml;
            ViewData["ProjIDs"] = string.Join(",", new DBSql.Iso.IsoFormNode().GetList(p => p.FormID == id).Select(p => p.ColAttType1.ToString()));

            var ColAttVal1Str = new DBSql.Sys.BaseData().getBaseNameByIds(model.ColAttVal1.ToString());
            ViewBag.ColAttVal1 = ColAttVal1Str.Trim(',');

            var ColAttVal2Str = new DBSql.Sys.BaseData().getBaseNameByIds(model.ColAttVal2.ToString());
            ViewBag.ColAttVal2 = ColAttVal2Str.Trim(',');

            ViewBag.permission = "['add','submit']";
            return View("info", model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 1;
            //op.Delete(s => id.Contains(s.FormID));
            //op.UnitOfWork.SaveChanges();
            //new DBSql.PubFlow.Flow().Delete(id, "IsoFormProjectPublish");

            op.DeleteProjectDeliver(id);
            op.UnitOfWork.SaveChanges();
            new DBSql.PubFlow.Flow().Delete(id, "IsoFormProjectPublish");
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(FormCollection fc)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml("<Root></Root>");
            foreach (string key in fc.AllKeys)
            {
                if (key == "FormID")
                {
                    continue;
                }
                var node = xmlDocument.CreateElement("Item");
                node.SetAttribute("name", key);
                node.InnerText = fc[key].ToString();
                xmlDocument.DocumentElement.AppendChild(node);
            }


            var model = new IsoForm();
            int FormID = int.Parse(fc["FormID"].ToString());

            if (FormID > 0)
            {
                model = op.Get(FormID);
            }
            model.MvcSetValue();
            //项目卷册信息
            List<DataModel.Models.IsoFormNode> FormNodeList = new List<IsoFormNode>();
            if (Request.Params["ProjTable_Data"] != null)
            {
                string var = Request.Params["ProjTable_Data"].ToString().Replace("Id", "ColAttType1");
                FormNodeList = new JavaScriptSerializer().Deserialize<List<IsoFormNode>>(var);
            }


            int reuslt = 0;
            if (model.FormID > 0)
            {
                model.FormCtlXml = xmlDocument.OuterXml;
                op.MvcDefaultEdit(userInfo.EmpID);
                //op.Edit(model);
                op.Edit(model, FormNodeList);
            }
            else
            {
                model.FormCtlXml = xmlDocument.OuterXml;
                model.RefTable = "IsoFormProjectPublish";
                //其他值
                model.FormDate = System.DateTime.Now;
                model.FormName = "项目出版登记";


                model.MvcDefaultSave(userInfo.EmpID);
                //op.Add(model);
                op.AddProjectDeliver(model, FormNodeList);
            }
            op.UnitOfWork.SaveChanges();
            reuslt = model.FormID;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.FormID, "操作成功") : DoResultHelper.doSuccess(model.FormID, "操作失败");
            return Json(dr);
        }
        #endregion


        private void BindProjName(int ProjID)
        {
            DataModel.Models.Project project = proj.Get(ProjID);
            if (project != null)
            {
                ViewBag.ProjNumber = project.ProjNumber;
                ViewBag.ProjName = project.ProjName;

                ViewBag.PhaseID = proj.GetProjPhase(project.ProjPhaseIds);//project.FK_Project_ProjTypeID == null ? "" :  
                ViewBag.ProjID = ProjID;
                ViewBag.ProjName = project.ProjName;
                ViewBag.ProjEmpName = project.ProjEmpName;
            }
            else
            {
                ViewBag.ProjName = "";
                ViewBag.ProjNumber = "";
                ViewBag.PhaseID = "";
                ViewBag.ProjID = "";
                ViewBag.ProjName = "";
                ViewBag.ProjEmpName = "";
            }
        }

        #region 选择工程列表
        [Description("选择工程列表")]
        public ActionResult FilterList()
        {
            return View();
        }
        #endregion



        #region 晒图复印扫描任务单
        [Description("晒图复印扫描任务单")]
        public ActionResult infoNodeOne(int id)
        {
            var model = op.Get(id);
            BindProjName(model.ProjID);
            ViewBag.contentXml = model.FormCtlXml;
            ViewData["ProjIDs"] = string.Join(",", new DBSql.Iso.IsoFormNode().GetList(p => p.FormID == id).Select(p => p.ColAttType1.ToString()));

            var ColAttVal1Str = new DBSql.Sys.BaseData().getBaseNameByIds(model.ColAttVal1.ToString());
            ViewBag.ColAttVal1 = ColAttVal1Str.Trim(',');

            var ColAttVal2Str = new DBSql.Sys.BaseData().getBaseNameByIds(model.ColAttVal2.ToString());
            ViewBag.ColAttVal2 = ColAttVal2Str.Trim(',');
            ViewBag.CreatorDepName = userInfo.EmpDepName;
            ViewBag.CreatorEmpName = userInfo.EmpName;
            ViewBag.permission = "['add','submit']";
            return View("infoNodeOne", model);
        }
        #endregion

        #region 晒图复印扫描完成记录
        [Description("晒图复印扫描完成记录")]
        public ActionResult infoNodeTwo(int id)
        {
            var model = op.Get(id);
            BindProjName(model.ProjID);
            ViewBag.contentXml = model.FormCtlXml;
            ViewData["ProjIDs"] = string.Join(",", new DBSql.Iso.IsoFormNode().GetList(p => p.FormID == id).Select(p => p.ColAttType1.ToString()));

            var ColAttVal1Str = new DBSql.Sys.BaseData().getBaseNameByIds(model.ColAttVal1.ToString());
            ViewBag.ColAttVal1 = ColAttVal1Str.Trim(',');

            var ColAttVal2Str = new DBSql.Sys.BaseData().getBaseNameByIds(model.ColAttVal2.ToString());
            ViewBag.ColAttVal2 = ColAttVal2Str.Trim(',');

            ViewBag.permission = "['add','submit']";
            return View("infoNodeTwo", model);
        }
        #endregion



        #region 统计列表json
        [Description("统计列表json")]
        [HttpPost]
        public string GetJsonStateMentList()
        {
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
                    //else if (_child.Key == "ColAttDate1S")
                    //{
                    //    PageModel.SelectCondtion.Add("ColAttDate1S", _child.Value);
                    //}
                    //else if (_child.Key == "ColAttDate1E")
                    //{
                    //    PageModel.SelectCondtion.Add("ColAttDate1E", _child.Value);
                    //}
                    //else if (_child.Key == "ColAttDate2S")
                    //{
                    //    PageModel.SelectCondtion.Add("ColAttDate2S", _child.Value);
                    //}
                    //else if (_child.Key == "ColAttDate2E")
                    //{
                    //    PageModel.SelectCondtion.Add("ColAttDate2E", _child.Value);
                    //}
                    //else if (_child.Key == "ProjSubTypeState")
                    //{
                    //    PageModel.SelectCondtion.Add("ProjSubTypeState", _child.Value);
                    //}
                }
            }

            var dt = op.GetStateMentList(PageModel);
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = dt
            });

        }
        #endregion

        [Description("列表统计")]
        public ActionResult listStatement()
        {
            return View();
        }


        #region 出版时间
        [Description("出版时间")]
        public ActionResult infoForPrint(int id)
        {
            var model = desTask.Get(id);
            if (model.DateForPrint.Year != 1900)
            {
                ViewBag.DateForPrint = model.DateForPrint.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                ViewBag.DateForPrint = "未出版";
            }
            return View("infoForPrint", model);
        }
        #endregion


        #region 添加
        [Description("添加")]
        public ActionResult PublishAdd(int projID, int taskId)
        {
            ViewBag.pId = Request.Params["projID"].ToString();
            ViewBag.taskIds = Request.Params["taskId"].ToString();
            ViewBag.TaskPhaseId = Request.Params["TaskPhaseId"].ToString();
            ViewBag.Ename = Request.Params["Ename"].ToString();
            if (projID != 0)
            {
                DataModel.Models.Project pModel = new DBSql.Project.Project().FirstOrDefault(p => p.Id == projID);
                ViewBag.Model = pModel;
                ViewBag.TaskPhaseName = proj.GetProjPhase(pModel.ProjPhaseIds);
            }


            var model = new IsoForm();
            model.CreationTime = System.DateTime.Now;
            model.CreatorEmpName = userInfo.EmpName;
            model.CreatorDepName = userInfo.EmpDepName;
            ViewBag.FlowNumber = "无";
            ViewBag.permission = "['add','submit']";
            return View("infos", model);
        }
        #endregion


        #region  获取下拉列表信息
        [Description("获取下拉列表数据")]

        public string combogridJson()
        {
            int PageNum = TypeHelper.parseInt(Request.Form["page"], 1);
            int Record = TypeHelper.parseInt(Request.Form["rows"], 10);
            var condition = TypeHelper.parseString(Request.Params["Words"]).Trim();
            int ProjId = TypeHelper.parseInt(Request.Params["projId"]);//将选中的记录放在第一位置

            int isValue = TypeHelper.parseInt(Request.Params["isValue"]);//  
            object obj = null;//存放选中的数据对象

            int pId = TypeHelper.parseInt(Request.Params["pId"]);//将选中的记录放在第一位置

            var list = proj.GetList(p => p.Id != ProjId && p.DeleterEmpId == 0 && p.Id != 0).ToList();//要查询所有,p.Id!=0恒为true
            if (pId != 0)
            {
                list = proj.GetList(p => p.Id != ProjId && p.DeleterEmpId == 0 && p.Id != 0 && p.Id == pId).ToList();//要查询所有,p.Id!=0恒为true
            }

            var desTaskList = desTask.GetList(p => p.DeleterEmpId == 0).ToList();

            var a = (from item in list
                     where item.ProjName.Contains(condition) || item.ProjNumber.Contains(condition)
                     orderby item.CreationTime, item.Id descending
                     select new
                     {
                         item.Id,
                         item.ProjNumber,
                         item.ProjName,
                         ProjTypeName = item.FK_Project_ProjTypeID == null ? "" : item.FK_Project_ProjTypeID.BaseName,
                         PhaseName = proj.GetProjPhase(item.ProjPhaseIds),//项目阶段
                         item.ProjPhaseIds,
                         item.CustName,
                         item.DatePlanStart,
                         item.DatePlanFinish,
                         item.ProjEmpId,
                         item.ProjEmpName,

                     });

            if (isValue == 1)
            {
                a = (from item in list
                     join dask in desTaskList on item.Id equals dask.ProjId
                     where item.ProjName.Contains(condition) || item.ProjNumber.Contains(condition) && (item.ProjEmpId == userInfo.EmpID || dask.TaskEmpID == userInfo.EmpID)
                     orderby item.CreationTime, item.Id descending
                     select new
                     {
                         item.Id,
                         item.ProjNumber,
                         item.ProjName,
                         ProjTypeName = item.FK_Project_ProjTypeID == null ? "" : item.FK_Project_ProjTypeID.BaseName,
                         PhaseName = proj.GetProjPhase(item.ProjPhaseIds),//项目阶段
                         item.ProjPhaseIds,
                         item.CustName,
                         item.DatePlanStart,
                         item.DatePlanFinish,
                         item.ProjEmpId,
                         item.ProjEmpName,
                     }).ToList().Distinct();
            }

            List<dynamic> dyList = new List<dynamic>();
            DataModel.Models.Project modelProject = proj.Get(ProjId);
            if (modelProject != null)//判断当前数据是否存在
            {
                string ProjTypeName = modelProject.FK_Project_ProjTypeID == null ? "" : modelProject.FK_Project_ProjTypeID.BaseName;
                string PhaseName = proj.GetProjPhase(modelProject.ProjPhaseIds);
                obj = new
                {
                    modelProject.Id,
                    modelProject.ProjNumber,
                    modelProject.ProjName,
                    modelProject.ProjEmpName,
                    ProjTypeName,
                    PhaseName,
                    modelProject.CustName,
                    modelProject.DatePlanStart,
                    modelProject.DatePlanFinish
                };
                dyList.Add(obj);
            }
            if (a.Count() > 0)
            {
                dyList.AddRange(a);
            }

            return JavaScriptSerializerUtil.getJson(new
            {
                total = dyList.Count,
                rows = (dyList.Skip(Record * (PageNum - 1)).Take(Record)).ToList()
            });
        }
        #endregion

    }
}
