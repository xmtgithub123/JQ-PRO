using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System;
using System.Web.Script.Serialization;
using System.Data;
using Common;
using Common.Data.Extenstions;
using System.IO;
using System.Web;
using DataModel;

namespace Project.Controllers
{
    [Description("Project")]
    public class ProjectController : CoreController
    {
        private DBSql.Project.Project dbProject = new DBSql.Project.Project();
        private DBSql.Sys.BaseData baseDataDB = new DBSql.Sys.BaseData();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("项目信息登记列表(分公司)")]
        public ActionResult list()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjInfoRegister")));
            return View();
        }

        [Description("项目信息登记列表(设计公司)")]
        public ActionResult list_sj()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjInfoRegister_SJ")));
            return View();
        }

        [Description("项目信息登记列表(工程公司)")]
        public ActionResult list_gc()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjInfoRegister_GC")));
            return View();
        }

        [Description("项目信息登记列表(创景公司)")]
        public ActionResult list_cj()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjInfoRegister_CJ")));
            return View();
        }

        [Description("表单中心")]
        public ActionResult IsoConfig()
        {
            ViewBag.CurrEmpId = userInfo.EmpID;
            return View();
        }
        #endregion

        #region 列表json
        //废弃的列表json
        [Description("列表json")]
        [HttpPost]
        public string json()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            int projParentID = TypeParse.parse<int>(Request["id"]);
            List<DataModel.Models.Project> list = new List<DataModel.Models.Project>();
            if (projParentID > 0)
            {
                PageModel.Predicate = "";
            }
            var TWhere = QueryBuild<DataModel.Models.Project>.True().And(x => x.ParentId == projParentID);
            TWhere = TWhere.And(x => x.DeleterEmpId == 0);
            TWhere = TWhere.And(x => x.ColAttType6 == 0);
            list = dbProject.GetPagedList(TWhere, PageModel).ToList();

            var dtoList = from item in list
                          select new
                          {
                              item.Id,
                              item.ParentId,
                              item.BridgeGuid,
                              item.ProjNumber,
                              ProjName = item.ProjNumber + " " + item.ProjName,
                              item.ProjEmpName,
                              ProjPhaseIds = String.Join(",", GetBaseName(item.ProjPhaseIds)),
                              ProjTypeID = GetBaseInfo(item.ProjTypeID).BaseName,
                              ProjPropertyID = GetBaseInfo(item.ProjPropertyID).BaseName,
                              ProjVoltID = GetBaseInfo(item.ProjVoltID).BaseName,
                              item.DateCreate,
                              item.DatePlanFinish,
                              item.ColAttType5,
                              state = (dbProject.Count(p => p.ParentId == item.Id)) == 0 ? "open" : "closed"
                          };

            string result = "";
            if (projParentID == 0)
                result = JavaScriptSerializerUtil.getJson(new { total = PageModel.PageTotleRowCount, rows = dtoList });
            else
                result = JavaScriptSerializerUtil.getJson(dtoList);
            return result;

        }
        #region 列表json
        //在使用的列表json
        [Description("获取项目信息登记列表")]
        [HttpPost]
        public string jsonNew(int category = 0)
        {
            var _model = baseDataDB.Get((int)ProjectType.主网类别);

            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "p.Id desc";
            }
            int _parentId = TypeParse.parse<int>(Request["id"]);

            if (category == 0)
            {
                #region
                #region
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
                        else if (_child.Key == "ProjPropertyID")
                        {
                            PageModel.SelectCondtion.Add("ProjPropertyID", _child.Value);
                        }
                        else if (_child.Key == "ProjDepId")
                        {
                            PageModel.SelectCondtion.Add("ProjDepId", _child.Value);
                        }
                        else if (_child.Key == "ProjectType")
                        {
                            PageModel.SelectCondtion.Add("ProjectType", _child.Value);
                        }
                        else if (_child.Key == "ProjVoltID")
                        {
                            PageModel.SelectCondtion.Add("ProjVoltID", _child.Value);
                        }
                        else if (_child.Key == "DateCreateS")
                        {
                            PageModel.SelectCondtion.Add("DateCreateS", _child.Value);
                        }
                        else if (_child.Key == "DateCreateE")
                        {
                            PageModel.SelectCondtion.Add("DateCreateE", _child.Value);
                        }
                        else if (_child.Key == "DatePlanFinishS")
                        {
                            PageModel.SelectCondtion.Add("DatePlanFinishS", _child.Value);
                        }
                        else if (_child.Key == "DatePlanFinishE")
                        {
                            PageModel.SelectCondtion.Add("DatePlanFinishE", _child.Value);
                        }else if (_child.Key == "ProjJoinDepIds")
                        {
                            PageModel.SelectCondtion.Add("ProjJoinDepIds", _child.Value);
                        }
                    }
                }
                #endregion
                List<string> result = new List<string>();
                if (Request.QueryString["FormMenu"] == null)
                {
                    string companyType = Request.Params["CompanyType"].ToString();
                    switch (companyType)
                    {
                        case "SJ":
                            result = PermissionList("ProjInfoRegister_SJ");
                            PageModel.SelectCondtion.Add("CompanyID", 1);
                            break;
                        case "GC":
                            result = PermissionList("ProjInfoRegister_GC");
                            PageModel.SelectCondtion.Add("CompanyID", 2);
                            break;
                        case "CJ":
                            result = PermissionList("ProjInfoRegister_CJ");
                            PageModel.SelectCondtion.Add("CompanyID", 3);
                            break;
                        default:
                            result = PermissionList("ProjInfoRegister");
                            PageModel.SelectCondtion.Add("CompanyID", 0);
                            break;
                    }

                    #region
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
                    #endregion
                }
                else
                {
                    #region
                    result = PermissionList(Request.QueryString["FormMenu"].ToString());
                    if (!(result.Contains("allview") || result.Contains("alledit")))
                    {
                        if (result.Contains("dep"))
                        {
                            PageModel.SelectCondtion.Add("QueryDeptID", this.userInfo.EmpDepID);
                        }
                        else
                        {
                            PageModel.SelectCondtion.Add("ProjEmpID", this.userInfo.EmpID);
                        }
                    }
                    #endregion
                }

                List<string> list = new List<string>();
                list.Add(_model.BaseOrder);
                if (Request.QueryString["FormMenu"] == "EngineerMain")
                {
                    list = null;
                }
                using (var dt = dbProject.GetListInfo(PageModel, 0, list))
                {
                    return JavaScriptSerializerUtil.getJson(new
                    {
                        total = PageModel.PageTotleRowCount,
                        rows = dt
                    });
                }
                #endregion
            }
            else
            {

                string companyType = Request.Params["CompanyType"].ToString();
                string refTable = "'Project'";
                switch (companyType)
                {
                    case "SJ":
                        refTable = "'Project_SJ'";
                        break;
                    case "GC":
                        refTable = "'Project_GC'";
                        break;
                    case "CJ":
                        refTable = "'Project_CJ'";
                        break;
                    default:
                        refTable = "'Project'";
                        break;
                }

                using (var dt = dbProject.GetChildrenListInfo(_parentId, refTable))
                {
                    return JavaScriptSerializerUtil.getJson(dt);
                }
            }

        }

        #endregion
        //新增项目立项的阶段表格
        public string jsonPhase()
        {
            int projId = TypeParse.parse<int>(Request["projId"]);
            if (projId == 0)
                return "[]";

            int childmodelId = TypeParse.parse<int>(Request["childmodelId"]);
            if (childmodelId == 0)
                return "[]";

            var model = dbProject.Get(childmodelId);

            var list = dbProject.GetList(p => p.ParentId == projId && p.BridgeGuid == model.BridgeGuid && p.DeleterEmpId == 0).ToList();

            var dtoList = from item in list
                          select new
                          {
                              item.Id,
                              item.ProjNumber,
                              ProjName = item.ProjName,
                              item.ProjEmpId,
                              item.ProjEmpName,
                              item.ProjTypeID,
                              item.ProjPhaseIds,
                              item.ProjVoltID,
                              ProjPhaseIdsName = String.Join(",", GetBaseName(item.ProjPhaseIds)),
                              item.DateCreate,
                              item.DatePlanStart,
                              item.DatePlanFinish,
                              item.ParentId,
                              item.ColAttType3, //回路数
                              item.ColAttVal3,  //线路长度(km)
                              item.ColAttVal4,  //变压器容量(MVA)
                              item.ColAttVal5,   //台数
                              item.ProjFee,  //投资额
                              item.ProjDemand,  //变压器容量
                              item.ProjNoteOther,   //备注
                              item.ProjPhaseInfo, //临时当作阶段数据
                              item.BridgeGuid,
                              item.IsQuote,
                              item.CreatorEmpId,
                              item.FProjEmpId,
                              item.FProjEmpName,
                          };

            string result = JavaScriptSerializerUtil.getJson(dtoList);
            return result;

        }
        #endregion

        #region  获取下拉列表信息
        //选择项目时显示的项目列表
        [Description("获取下拉列表数据")]

        public string combogridJson()
        {
            int PageNum = TypeHelper.parseInt(Request.Form["page"], 1);
            int Record = TypeHelper.parseInt(Request.Form["rows"], 10);
            var condition = TypeHelper.parseString(Request.Params["Words"]).Trim();
            int ProjId = TypeHelper.parseInt(Request.Params["projId"]);//将选中的记录放在第一位置
            object obj = null;//存放选中的数据对象
            var list = dbProject.GetList(p => p.Id != ProjId && p.DeleterEmpId == 0&&p.ParentId!=0).ToList();//要查询所有,p.Id!=0恒为true

            var a = (from item in list
                     where item.ProjName.Contains(condition) || item.ProjNumber.Contains(condition)
                     orderby item.CreationTime, item.Id descending
                     select new
                     {
                         item.Id,
                         item.ProjNumber,
                         item.ProjName,
                         item.ProjEmpName,
                         ProjTypeName = item.FK_Project_ProjTypeID == null ? "" : item.FK_Project_ProjTypeID.BaseName,
                         PhaseName = dbProject.GetProjPhase(item.ProjPhaseIds),//项目阶段
                         item.CustName,
                         item.DatePlanStart,
                         item.DatePlanFinish
                     }).ToList();
            List<dynamic> dyList = new List<dynamic>();
            DataModel.Models.Project modelProject = dbProject.Get(ProjId);
            if (modelProject != null)//判断当前数据是否存在
            {
                string ProjTypeName = modelProject.FK_Project_ProjTypeID == null ? "" : modelProject.FK_Project_ProjTypeID.BaseName;
                string PhaseName = dbProject.GetProjPhase(modelProject.ProjPhaseIds);
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
            dyList.AddRange(a);
            return JavaScriptSerializerUtil.getJson(new
            {
                total = dyList.Count,
                rows = (dyList.Skip(Record * (PageNum - 1)).Take(Record)).ToList()
            });
        }
        #endregion

        #region 添加
        //添加项目信息
        [Description("添加")]
        public ActionResult add()
        {
            //阶段
            var ProjPhaseIds = from m in new DBSql.Sys.BaseData().GetDataSetByEngName("ProjectPhase") select new { value = string.Format("{0}", m.BaseID), text = m.BaseName };
            ViewData["ProjPhaseIds"] = JavaScriptSerializerUtil.getJson(ProjPhaseIds);
            ViewBag.ProjPhaseIds = JavaScriptSerializerUtil.getJson(ProjPhaseIds);

            //类型
            //var ProjTypeIds = from m in new DBSql.Sys.BaseData().GetBaseDataInfoByEngName("ProjectType") select new { value = string.Format("{0}", m.BaseID), text = m.BaseName };
            // var ProjTypeIds = from m in new DBSql.Sys.BaseDataSystem().GetBaseDataInfoByEngName("ProjectType")  select new { value = string.Format("{0}", m.BaseID), text = m.BaseName };
            var DbBaseData = new DBSql.Sys.BaseData();
            var ProjTypeIds = DbBaseData.getTreeData(DbBaseData.GetBaseDataInfoByEngName("ProjectType"), 0, ((int)ProjectTypeModel.主网).ToString());
            ViewData["ProjTypeIds"] = JavaScriptSerializerUtil.getJson(ProjTypeIds);
            ViewData["ProjTypeItem"] = JavaScriptSerializerUtil.getJson(from m in new DBSql.Sys.BaseData().GetDataSetByEngName("ProjectType") select new { id = string.Format("{0}", m.BaseID), text = m.BaseName });

            //new DBSql.Sys.BaseData().GetBaseDataInfoByEngName(item.engName.ToString());

            ////电压
            //var ProjectVoltIds = from m in new DBSql.Sys.BaseData().GetDataSetByEngName("ProjectVolt") select new { value = string.Format("{0}", m.BaseID), text = m.BaseName };
            //ViewData["ProjectVoltIds"] = JavaScriptSerializerUtil.getJson(ProjectVoltIds);

            ////回路数
            //var ProjCircuitsIds = from m in new DBSql.Sys.BaseData().GetDataSetByEngName("ProjCircuits") select new { value = string.Format("{0}", m.BaseID), text = m.BaseName };
            //ViewData["ProjCircuitsIds"] = JavaScriptSerializerUtil.getJson(ProjCircuitsIds);

            //项目负责人
            var ProjRespEmps = (from m in new DBSql.Sys.BaseQualification().GetQualificationEmployee((int)DataModel.NodeType.项目设总, 0, 0, 0)
                                select new { m.EmpID, m.EmpName }).Distinct();
            ViewBag.Qualification = JavaScriptSerializerUtil.getJson(ProjRespEmps);

            var model = new DataModel.Models.Project();
            ViewBag.permission = "['add','submit']";

            model.DateCreate = DateTime.Now;
            model.CreatorEmpName = userInfo.EmpName;
            ViewBag.Variety = 1;
            ViewBag.ChildModelId = 0;

            string companyType = Request.Params["CompanyType"].ToString();
            string page = "info";
            switch (companyType)
            {
                case "SJ":
                    page = "info_sj";
                    break;
                case "GC":
                    page = "info_gc";
                    break;
                case "CJ":
                    page = "info_cj";
                    break;
                default:
                    page = "info";
                    break;
            }

            return View(page, model);
        }
        #endregion

        #region 编辑
        //修改项目信息
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            //阶段
            var ProjPhaseIds = from m in new DBSql.Sys.BaseData().GetDataSetByEngName("ProjectPhase") select new { value = string.Format("{0}", m.BaseID), text = m.BaseName };
            ViewData["ProjPhaseIds"] = JavaScriptSerializerUtil.getJson(ProjPhaseIds);

            //类型
            var DbBaseData = new DBSql.Sys.BaseData();
            var ProjTypeIds = DbBaseData.getTreeData(DbBaseData.GetBaseDataInfoByEngName("ProjectType"), 0, ((int)ProjectTypeModel.主网).ToString());
            ViewData["ProjTypeIds"] = JavaScriptSerializerUtil.getJson(ProjTypeIds);
            ViewData["ProjTypeItem"] = JavaScriptSerializerUtil.getJson(from m in new DBSql.Sys.BaseData().GetDataSetByEngName("ProjectType") select new { id = string.Format("{0}", m.BaseID), text = m.BaseName });

            //项目负责人
            var ProjRespEmps = (from m in new DBSql.Sys.BaseQualification().GetQualificationEmployee((int)DataModel.NodeType.项目设总, 0, 0, 0)
                                select new { m.EmpID, m.EmpName }).Distinct();

            ViewBag.Qualification = JavaScriptSerializerUtil.getJson(ProjRespEmps);

            var childmodel = dbProject.Get(id);
            var model = dbProject.Get(childmodel.ParentId);
            ViewBag.ChildModelId = childmodel.Id;
            ViewBag.Guid = childmodel.BridgeGuid;
            if (childmodel.IsQuote == 1)
            {
                ViewBag.Variety = 0;
            }
            else
            {
                ViewBag.Variety = 1;
            }
            ViewBag.isquote = childmodel.IsQuote;

            string companyType = Request.Params["CompanyType"].ToString();
            string page = "info";

            switch (companyType)
            {
                case "SJ":
                    page = "info_sj";
                    break;
                case "GC":
                    page = "info_gc";
                    break;
                case "CJ":
                    page = "info_cj";
                    break;
                default:
                    page = "info";
                    break;
            }

            return View(page, model);
        }
        #endregion

        public ActionResult ProjInfoOnlyRead(int id)
        {
            //阶段
            var ProjPhaseIds = from m in new DBSql.Sys.BaseData().GetDataSetByEngName("ProjectPhase") select new { value = string.Format("{0}", m.BaseID), text = m.BaseName };
            ViewData["ProjPhaseIds"] = JavaScriptSerializerUtil.getJson(ProjPhaseIds);

            //类型
            var DbBaseData = new DBSql.Sys.BaseData();
            var ProjTypeIds = DbBaseData.getTreeData(DbBaseData.GetBaseDataInfoByEngName("ProjectType"));
            ViewData["ProjTypeIds"] = JavaScriptSerializerUtil.getJson(ProjTypeIds);
            ViewData["ProjTypeItem"] = JavaScriptSerializerUtil.getJson(from m in new DBSql.Sys.BaseData().GetDataSetByEngName("ProjectType") select new { id = string.Format("{0}", m.BaseID), text = m.BaseName });

            //电压
            var ProjectVoltIds = from m in new DBSql.Sys.BaseData().GetDataSetByEngName("ProjectVolt") select new { value = string.Format("{0}", m.BaseID), text = m.BaseName };
            ViewData["ProjectVoltIds"] = "";

            //回路数
            var ProjCircuitsIds = from m in new DBSql.Sys.BaseData().GetDataSetByEngName("ProjCircuits") select new { value = string.Format("{0}", m.BaseID), text = m.BaseName };
            ViewData["ProjCircuitsIds"] = "";

            //项目负责人
            var ProjRespEmps = (from m in new DBSql.Sys.BaseQualification().GetQualificationEmployee((int)DataModel.NodeType.项目设总, 0, 0, 0)
                                select new { m.EmpID, m.EmpName }).Distinct();
            ViewBag.Qualification = JavaScriptSerializerUtil.getJson(ProjRespEmps);

            var model = dbProject.Get(id);
            //ViewBag.permission = "['add','submit']";
            return View("ProjInfoOnlyRead", model);
        }

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 1;
            try
            {
                //op.Delete(s => id.Contains(s.Id));
                dbProject.UpdateProjInfoList(id, this.userInfo);
                dbProject.UnitOfWork.SaveChanges();

                new DBSql.PubFlow.Flow().Delete(id, "Project");
                //删除本次删除的阶段工程 此处调用黄一鸣方法删除
                DBSql.Design.DesTaskGroup desTaskGroupDB = new DBSql.Design.DesTaskGroup();
                id.ToList().ForEach(p => desTaskGroupDB.DeleteProjGroupNode(p, userInfo));
            }
            catch (Exception e)
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
            int result = 0;

            //页面model 项目信息
            var model = new DataModel.Models.Project();
            if (Id > 0)
            {
                model = dbProject.Get(Id);
            }
            model.MvcSetValue();

            if (model.Id > 0)
            {
                model.MvcDefaultSave(userInfo);
                dbProject.Edit(model);
            }
            else
            {
                model.MvcDefaultEdit(userInfo);
                dbProject.Add(model);
            }
            dbProject.UnitOfWork.SaveChanges();
            result = model.Id;


            //新插入的子项信息
            int parentId = model.Id;
            string ColAttVal2 = model.ColAttVal2;
            List<DataModel.Models.Project> listProjPhase = new JavaScriptSerializer().Deserialize<List<DataModel.Models.Project>>(Request.Params["HiddenProjPhaseGridData"].ToString());

            listProjPhase.ForEach(p =>
            {
                if (p.DateCreate.Year == 1)
                    p.DateCreate = DateTime.Parse("1900-01-01");
                if (p.DatePlanStart.Year == 1)
                    p.DatePlanStart = DateTime.Parse("1900-01-01");
                if (p.DatePlanFinish.Year == 1)
                    p.DatePlanFinish = DateTime.Parse("1900-01-01");

                if (p.Id > 0)
                {
                    //删除协同项目中的阶段工程的子阶段信息 此处调用黄一鸣方法删除
                    new DBSql.Project.Project().FirstOrDefault(n => n.Id == p.Id).ProjPhaseIds.Split(',').ToList().ForEach(m =>
                    {
                        if (!p.ProjPhaseIds.Split(',').ToList().Exists(q => q.ToString() == m))
                        {
                            new DBSql.Design.DesTaskGroup().DeleteProjPhaseNode(p.Id, Convert.ToInt32(m), userInfo);
                        }
                    });

                    p.MvcDefaultEdit(userInfo);
                    p.ColAttVal2 = string.Format("{0}/", ColAttVal2 == "" ? parentId.ToString() : ColAttVal2 + "/" + parentId.ToString());
                    dbProject.Edit(p);

                }
                else
                {
                    p.MvcDefaultSave(userInfo);
                    p.ParentId = parentId;
                    p.ColAttVal2 = string.Format("{0}/", ColAttVal2 == "" ? parentId.ToString() : ColAttVal2 + "/" + parentId.ToString());
                    dbProject.Add(p);
                }
                dbProject.UnitOfWork.SaveChanges();

            });

            #region 更新项目的阶段
            string OldPhaseIds = model.ProjPhaseIds;

            model.ProjPhaseIds = "";
            listProjPhase.ForEach(p =>
            {
                p.ProjPhaseIds.Split(',').ToList().ForEach(q =>
                {
                    if (!model.ProjPhaseIds.Contains(q))
                        model.ProjPhaseIds += q + ",";
                });
            });
            model.ProjPhaseIds = model.ProjPhaseIds.TrimEnd(',');
            dbProject.UnitOfWork.SaveChanges();

            #endregion

            //---------------------------------------------------DesTaskGroup------------------------------------------
            //插入协同设计相关表数据
            DBSql.Design.DesTaskGroup desTaskGroupDB = new DBSql.Design.DesTaskGroup();
            DBSql.Design.DesTaskGantt desTaskGanttDB = new DBSql.Design.DesTaskGantt();
            DataModel.Models.DesTaskGroup projTaskGroup = new DesTaskGroup();
            DataModel.Models.DesTaskGantt projTaskGantt = new DesTaskGantt();

            //添加、修改本次的工程及阶段
            listProjPhase.ForEach(p =>
            {
                projTaskGroup = desTaskGroupDB.InsertProjGroupNode(p.Id, string.Format("[{0}]{1}", p.ProjNumber, p.ProjName), p.ProjEmpId, p.ProjEmpName, p.DatePlanStart, p.DatePlanFinish, DBSql.Design.TaskGroupStatus.进行中, userInfo);
                projTaskGantt = desTaskGanttDB.InsertProjTaskGantt(p.Id, string.Format("[{0}]{1}", p.ProjNumber, p.ProjName), p.ColAttType11, projTaskGroup.Id, p.ProjEmpId, p.DatePlanStart, p.DatePlanFinish, userInfo);

                var projPhaseIDs = p.ProjPhaseIds.Split(',');

                // 插入项目阶段节点
                var t = 0;
                string json = p.ProjPhaseInfo;
                var serializer = new JavaScriptSerializer();
                serializer.RegisterConverters(new[] { new JsonDeserializeDynamic() });
                dynamic obj = serializer.Deserialize(json, typeof(object));

                foreach (var projPhaseId in projPhaseIDs)
                {
                    foreach (var row in obj.rows)
                    {
                        if (row.PhaseId == projPhaseId)
                        {
                            int PhaseId = TypeParse.parse<int>(row.PhaseId);
                            string PhaseName = row.PhaseName;
                            int phaseEmpId = TypeParse.parse<int>(row.ProjEmpId);
                            string phaseEmpName = row.ProjEmpName;
                            DateTime planStart = TypeParse.parse<DateTime>(row.DatePlanStart, Convert.ToDateTime("1900-01-01"));
                            DateTime planFinish = TypeParse.parse<DateTime>(row.DatePlanFinish, Convert.ToDateTime("1900-01-01"));
                            string ProjNumber = row.ProjNumber;
                            string ProjName = row.ProjName;
                            t++;
                            var phaseTaskGroup = desTaskGroupDB.InsertPhaseGroupNode(projTaskGroup, PhaseId, PhaseName, phaseEmpId, phaseEmpName, planStart, planFinish, (t == 1 ? DBSql.Design.TaskGroupStatus.进行中 : DBSql.Design.TaskGroupStatus.未启动), userInfo, ProjNumber, ProjName);

                            desTaskGanttDB.InsertPhaseTaskGantt(projTaskGantt, phaseTaskGroup, userInfo);
                            break;
                        }
                    }

                }
            });
            //---------------------------------------------------DesTaskGroup----End--------------------------------------

            DoResult dr = result > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
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
                PageModel.SelectOrder = "Id desc";
            }
            var list = dbProject.GetQuery().ToList();

            List<DataModel.Models.Project> result = (from item in list
                                                     where item.ProjName.Contains(condition)    //条件查询
                                                     select item).ToList<DataModel.Models.Project>();

            var a = (from item in result
                     select new
                     {
                         item.Id,
                         item.ProjName,
                         item.ProjNumber
                     }).ToList();
            var t = (a.Skip(Record * (PageNum - 1)).Take(Record)).ToList();
            return JavaScriptSerializerUtil.getJson(new
            {
                total = a.Count,
                rows = (a.Skip(Record * (PageNum - 1)).Take(Record)).ToList()
            });

        }

        #region  获取成本项，暂时先放在这，后面要放到ProjCost的数据操作类中
        public string GetCostList()
        {
            string result = "";
            DBSql.Sys.BaseData bd = new DBSql.Sys.BaseData();
            DBSql.Project.ProjCost dbProjCost = new DBSql.Project.ProjCost();
            DBSql.Project.ProjCostModel dbProjCostModel = new DBSql.Project.ProjCostModel();

            List<DataModel.Models.BaseData> listbd = bd.GetDataSetByEngName("ProjCostType").ToList();
            List<ProjCostModelDto> listDto = new List<ProjCostModelDto>();

            //先读实际表；否则读模版表
            int projId = TypeParse.parse<int>(Request["ProjId"]);
            int isoXMCBId = TypeParse.parse<int>(Request["IsoXMCBId"]);
            if (dbProjCost.Count(m => m.XMCBID == isoXMCBId) > 0)
            {
                List<ProjCost> listProjCostSource = dbProjCost.GetList(m => m.XMCBID == isoXMCBId).ToList();
                listbd.ForEach(p =>
                {
                    ProjCostModelDto dto = new ProjCostModelDto();
                    GetCostFromRun(0, p.BaseID, dto, listProjCostSource);
                    if (dto.children != null)
                        dto.children.ToList().ForEach(q => { q.state = "open"; listDto.Add(q); });
                });

                result = JavaScriptSerializerUtil.getJson(new { rows = listDto });

            }
            else
            {
                //读模版
                List<ProjCostModel> listProjCostModelSource = dbProjCostModel.GetQuery().ToList();
                listbd.ForEach(p =>
                {
                    ProjCostModelDto dto = new ProjCostModelDto();
                    GetCostFromModel(0, p.BaseID, dto, listProjCostModelSource);
                    if (dto.children != null)
                        dto.children.ToList().ForEach(q => { q.state = "open"; listDto.Add(q); });
                });

                result = JavaScriptSerializerUtil.getJson(new { rows = listDto });
            }
            return result;
        }



        public class ProjCostModelDto : ProjCostModel
        {
            public string state { get; set; }
            public decimal ModelTempFee { get; set; }
            public decimal ModelFactFee { get; set; }
            public IList<ProjCostModelDto> children { get; set; }
        }

        //读成本模版表
        public void GetCostFromModel(int p_id, int p_typeId, ProjCostModelDto ProjCostDto, List<ProjCostModel> listProjCostModelSource)
        {
            var listProjCostModel = listProjCostModelSource.Where(p => p.ParentId == p_id && p.ModelType == p_typeId).OrderBy(p => p.CreationTime).ToList();
            foreach (var model in listProjCostModel)
            {
                ProjCostModelDto dto = new ProjCostModelDto();
                EntityHelpers.EntityToEntity(model, dto);
                if (ProjCostDto.children == null)
                    ProjCostDto.children = new List<ProjCostModelDto>();

                ProjCostDto.children.Add(dto);
            }

            if (ProjCostDto.children != null)
            {
                foreach (ProjCostModelDto dto in ProjCostDto.children)
                {
                    GetCostFromModel(dto.Id, dto.ModelType, dto, listProjCostModelSource);
                }
            }
        }

        //读成本实际表
        public void GetCostFromRun(int p_id, int p_typeId, ProjCostModelDto ProjCostDto, List<ProjCost> listProjCostSource)
        {
            var listProjCost = listProjCostSource.Where(p => p.ParentId == p_id && p.CostType == p_typeId).OrderBy(p => p.CreationTime).ToList();
            foreach (var ProjCost in listProjCost)
            {
                ProjCostModelDto dto = new ProjCostModelDto();
                EntityHelpers.EntityToEntity(ProjCost, dto, new string[] { "Model", "Cost" });
                if (ProjCostDto.children == null)
                    ProjCostDto.children = new List<ProjCostModelDto>();
                ProjCostDto.children.Add(dto);
            }

            if (ProjCostDto.children != null)
            {
                foreach (ProjCostModelDto dto in ProjCostDto.children)
                {
                    GetCostFromRun(dto.Id, dto.ModelType, dto, listProjCostSource);
                }
            }
        }

        //视图层DTO转换成数据实体
        public void CostDTOtoEntity(List<ProjCostModelDto> listProjCostModelDto, List<ProjCost> listProjCost)
        {
            foreach (ProjCostModelDto dto in listProjCostModelDto)
            {
                DataModel.Models.ProjCost projCost = new ProjCost();
                EntityHelpers.EntityToEntity(dto, projCost, new string[] { "Cost", "Model" });
                listProjCost.Add(projCost);

                if (dto.children != null)
                    CostDTOtoEntity(dto.children.ToList(), listProjCost);
            }
        }

        public void CostDTOtoEntityDB(List<ProjCostModelDto> listProjCostModelDto, List<ProjCost> listProjCost, int projId, DBSql.Project.ProjCost dbProjCost, int parentId)
        {
            foreach (ProjCostModelDto dto in listProjCostModelDto)
            {
                DataModel.Models.ProjCost projCost = new ProjCost();
                EntityHelpers.EntityToEntity(dto, projCost, new string[] { "Cost", "Model" });
                listProjCost.Add(projCost);

                projCost.MvcDefaultSave(userInfo);
                projCost.ProjID = projId;
                projCost.ParentId = projCost.ParentId == 0 ? 0 : parentId;
                dbProjCost.Add(projCost);
                dbProjCost.UnitOfWork.SaveChanges();

                if (dto.children != null)
                    CostDTOtoEntityDB(dto.children.ToList(), listProjCost, projId, dbProjCost, projCost.Id);
            }
        }

        #endregion


        public ActionResult Center()
        {
            var projectID = Common.ModelConvert.ConvertToDefault<int>(Request.QueryString["projectID"]);
            if (projectID == 0)
            {
                return View(new DataModel.Models.Project());
            }
            ViewBag.ProjectID = projectID;
            ViewBag.TaskGroupID = JQ.Util.TypeParse.parse<int>(Request.QueryString["taskGroupID"]);
            ViewBag.ShowGantt = ContainPermission("ProjGanttList");
            ViewBag.ShowContract = ContainPermission("ContractInfo");
            ViewBag.ShowContractSub = ContainPermission("ContractSubInfo");
            ViewBag.ShowProduce = ContainPermission("ProjectPlanList");
            ViewBag.ShowProgress = ContainPermission("EngineerMain");
            ViewBag.ShowArchive = ContainPermission("ArchiveCollect");
            ViewBag.ShowQuality = true;
            ViewBag.ShowLog = ContainPermission("EngineerLog");
            return View(new DBSql.Project.Project().GetTopProject(projectID));
        }

        public ActionResult CenterIndex()
        {
            var taskGroupID = JQ.Util.TypeParse.parse<int>(Request.QueryString["taskGroupID"]);
            if (taskGroupID == 0)
            {
                return View();
            }
            var taskGroupDB = new DBSql.Design.DesTaskGroup();
            var taskGroup = taskGroupDB.FirstOrDefault(m => m.Id == taskGroupID);
            if (taskGroup != null)
            {
                ViewBag.TaskGroup = taskGroup;
                var project = taskGroupDB.DbContext.Set<DataModel.Models.Project>().Where(m => m.Id == taskGroup.ProjId).Select(m => new { m.ProjNumber, m.ProjName, ProjectTypeText = m.FK_Project_ProjTypeID.BaseName, ProjectVoltText = m.FK_Project_ProjVoltID.BaseName, m.CustName, m.ProjNote, ProjectTypeName = m.FK_Project_ProjTypeID.BaseName, ProjectVoltName = m.FK_Project_ProjVoltID.BaseName }).FirstOrDefault();
                foreach (var p in project.GetType().GetProperties())
                {
                    ViewData[p.Name] = p.GetValue(project, null);
                }
                if (taskGroup.TaskGroupType == 0)
                {
                    //当前为工程
                    ViewBag.PhaseName = "";
                }
                else if (taskGroup.TaskGroupType == 1)
                {
                    //阶段
                    ViewBag.PhaseName = taskGroup.TaskGroupName;
                }
                else if (taskGroup.TaskGroupType == 2)
                {
                    //分组
                    var parent = taskGroupDB.DbContext.Set<DataModel.Models.DesTaskGroup>().Where(m => m.Id == taskGroup.TaskGroupParentId).Select(m => m.TaskGroupName).FirstOrDefault();
                    if (!string.IsNullOrEmpty(parent))
                    {
                        ViewBag.PhaseName = parent + " > " + taskGroup.TaskGroupName;
                    }
                    else
                    {
                        ViewBag.PhaseName = taskGroup.TaskGroupName;
                    }
                }
            }
            return View();
        }

        public string GetProjectTree()
        {
            var projectID = Common.ModelConvert.ConvertToDefault<int>(Request.Form["projectID"]);
            if (projectID == 0)
            {
                return "[]";
            }
            return new DBSql.Project.Project().GetProjectListForTree(projectID);
        }

        public string GetProjectTree2()
        {
            var projectID = Common.ModelConvert.ConvertToDefault<int>(Request.QueryString["projectID"]);
            if (projectID == 0)
            {
                return "[]";
            }
            return new DBSql.Project.Project().GetProjectListForTree2(projectID);
        }

        public JsonResult GetTaskGroupUsers()
        {
            var taskGroupID = JQ.Util.TypeParse.parse<int>(Request.Form["taskGroupID"]);
            if (taskGroupID == 0)
            {
                return Json("[]");
            }
            return Json(new DBSql.Design.DesTaskGroup().GetTaskGroupUsers(taskGroupID));
        }

        public JsonResult GetDynamicList()
        {
            Common.SqlPageInfo queryContext = new Common.SqlPageInfo();
            queryContext.SelectCondtion.Add("RefID", Request.Form["refID"]);
            queryContext.SelectCondtion.Add("RefTable", Request.Form["refTable"]);
            var result = new DBSql.Project.ProjectDynamic().GetList(queryContext);
            return Json(new { total = queryContext.PageTotleRowCount, rows = result });
        }

        /// <summary>
        /// 项目中心列表
        /// </summary>
        /// <returns></returns>
        [Description("项目中心列表")]
        public ActionResult CenterList()
        {
            return View();
        }

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string GetJsonList(int category = 0)
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            int _parentId = TypeParse.parse<int>(Request["id"]);

            string companyType = Request.Params["CompanyType"].ToString();
            switch (companyType)
            {
                case "SJ":
                    PageModel.SelectCondtion.Add("CompanyID", 1);
                    break;
                case "GC":
                    PageModel.SelectCondtion.Add("CompanyID", 2);
                    break;
                case "CJ":
                    PageModel.SelectCondtion.Add("CompanyID", 3);
                    break;
                default:
                    PageModel.SelectCondtion.Add("CompanyID", 0);
                    break;
            }

            DataTable dt;
            if (category == 0)
            {
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
                    }
                }
                dt = dbProject.GetListInfo(PageModel, 0);
                return JavaScriptSerializerUtil.getJson(new
                {
                    total = PageModel.PageTotleRowCount,
                    rows = dt
                });
            }
            else
            {
                dt = dbProject.GetChildrenListInfo(_parentId, "'Project','ProjectPW','ProjectYF'");
                var result = JavaScriptSerializerUtil.getJson(dt);
                return result;
            }

        }

        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string GetMainJsonList()
        {
            int typeId = TypeHelper.parseInt(Request.Form["typeId"]);
            List<string> list = new List<string>();
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            var queryInfo = Request.Form["queryInfo"];

            string companyType = Request.Params["CompanyType"].ToString();
            switch (companyType)
            {
                case "SJ":
                    PageModel.SelectCondtion.Add("CompanyID", 1);
                    break;
                case "GC":
                    PageModel.SelectCondtion.Add("CompanyID", 2);
                    break;
                case "CJ":
                    PageModel.SelectCondtion.Add("CompanyID", 3);
                    break;
                default:
                    PageModel.SelectCondtion.Add("CompanyID", 0);
                    break;
            }

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
                }
            }

            if (typeId == 1)
            {
                var _model = baseDataDB.Get((int)ProjectType.主网类别);
                list.Add(_model.BaseOrder);
            }
            else if (typeId == 2)
            {
                var _model = baseDataDB.Get((int)ProjectType.配电类别);
                list.Add(_model.BaseOrder);
            }
            else
            {
                var _modelGH = baseDataDB.Get((int)ProjectType.规划可研类);
                var _modelQT = baseDataDB.Get((int)ProjectType.其他类);

                list.Add(_modelGH.BaseOrder);
                list.Add(_modelQT.BaseOrder);
            }
            DataTable dt = dbProject.GetListInfo(PageModel, 1, list);
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = dt
            });
        }

        #endregion

        public ActionResult ListInfo()
        {
            return View("ListInfo");
        }
        public ActionResult MainListInfo(int typeId)
        {
            ViewBag.typeId = typeId;
            return View("MainListInfo");
        }

        /// <summary>
        /// 导入工程
        /// </summary>
        /// <returns></returns>
        public JsonResult ExcelImportProject(string filePath)
        {
            //阶段
            var ProjPhaseIds = from m in new DBSql.Sys.BaseData().GetDataSetByEngName("ProjectPhase") select new { value = string.Format("{0}", m.BaseID), text = m.BaseName };

            //类型
            var ProjTypeIds = from m in new DBSql.Sys.BaseData().GetDataSetByEngName("ProjectType") select new { value = string.Format("{0}", m.BaseID), text = m.BaseName };

            //电压
            var ProjVoltIds = from m in new DBSql.Sys.BaseData().GetDataSetByEngName("ProjectVolt") select new { value = string.Format("{0}", m.BaseID), text = m.BaseName };

            //回路数
            var ProjCircuits = from m in new DBSql.Sys.BaseData().GetDataSetByEngName("ProjCircuits") select new { value = string.Format("{0}", m.BaseID), text = m.BaseName };

            //项目负责人
            var ProjRespEmps = from m in new DBSql.Sys.BaseQualification().GetQualificationEmployee((int)DataModel.NodeType.项目设总, 0, 0, 0) select new { m.EmpID, m.EmpName };


            //序号	工程代码	工程编号	工程名称	工程类型   	电压等级	回路数	线路长度(km)	变压器容量(MVA)	台数	设总	投资额	工程内容说明	备注

            Dictionary<string, string> ColMap = new Dictionary<string, string>();
            //tableCol.Add("工程代码", "");                 //////////////////////
            ColMap.Add("工程编号", "ProjNumber");
            ColMap.Add("工程名称", "ProjName");
            ColMap.Add("工程类型", "ProjTypeID");
            ColMap.Add("工程阶段", "ProjPhaseIds");
            ColMap.Add("电压等级", "ProjVoltID");
            ColMap.Add("回路数", "ColAttType3");
            ColMap.Add("线路长度(km)", "ColAttVal3");
            ColMap.Add("变压器容量(MVA)", "ColAttVal4");
            ColMap.Add("台数", "ColAttVal5");
            ColMap.Add("设总", "ProjEmpId");
            ColMap.Add("投资额", "ProjFee");
            ColMap.Add("工程内容说明", "ProjDemand");
            ColMap.Add("备注", "ProjNoteOther");
            ColMap.Add("立项时间", "DateCreate");
            ColMap.Add("计划开始时间", "DatePlanStart");
            ColMap.Add("计划完成时间", "DatePlanFinish");

            DataTable dt = ExcelRender.RenderFromExcel(filePath);
            dt.AsEnumerable().ToList().ForEach(p =>
            {
                try { p.SetField("工程阶段", string.Join(",", ProjPhaseIds.Where(q => ("," + p.Field<string>("工程阶段") + ",").Contains("," + q.text.ToString() + ",")).Select(q => q.value))); } catch { }
                try { p.SetField("工程类型", ProjTypeIds.Where(q => q.text.ToString() == p.Field<string>("工程类型")).FirstOrDefault().value); } catch { }
                try { p.SetField("电压等级", ProjVoltIds.Where(q => q.text.ToString() == p.Field<string>("电压等级")).FirstOrDefault().value); } catch { }
                try { p.SetField("回路数", ProjCircuits.Where(q => q.text.ToString() == p.Field<string>("回路数")).FirstOrDefault().value); } catch { }
                try { p.SetField("设总", ProjRespEmps.Where(q => q.EmpName == p.Field<string>("设总")).FirstOrDefault().EmpID); } catch { }
            });

            //Common.ModelConvert.ConvertToDefault<DataModel.Models.Project>(dt);
            List<DataModel.Models.Project> listProject = EntityHelpers.ConvertTo<DataModel.Models.Project>(dt, ColMap).ToList();
            string result = JavaScriptSerializerUtil.getJson(listProject);
            return Json(result);
        }

        public JsonResult Favourite()
        {
            var projectID = JQ.Util.TypeParse.parse<int>(Request.Form["projectID"]);
            if (projectID == 0)
            {
                return Json(new { Result = false, Message = "参数错误" });
            }
            new DBSql.Project.Project().Favourite(projectID, userInfo.EmpID);
            return Json(new { Result = true });

        }

        public JsonResult UnFavourite()
        {
            var projectID = JQ.Util.TypeParse.parse<int>(Request.Form["projectID"]);
            if (projectID == 0)
            {
                return Json(new { Result = false, Message = "参数错误" });
            }
            new DBSql.Project.Project().UnFavourite(projectID, userInfo.EmpID);
            return Json(new { Result = true });
        }

        public JsonResult UploadTemplate()
        {
            if (Request.Files.Count == 0)
            {
                return Json(new { Result = false, Message = "找不到文件" });
            }
            var extension = System.IO.Path.GetExtension(Request.Files[0].FileName).ToLower();
            if (extension == ".xls" || extension == ".xlsx")
            {
                var fileName = Guid.NewGuid().ToString() + extension;
                var templatePath = System.IO.Path.Combine(JQ.Util.IOUtil.GetTempPath(), fileName);
                Request.Files[0].SaveAs(templatePath);
                return ExcelImportProject(templatePath.ToString());
            }
            else
            {
                return Json(new { Result = false, Message = "模版文件格式不正确，需要为Excel文件" });
            }
        }

        /// <summary>
        /// 下载工程导入模版
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public FileStreamResult DownImportExcel(string fileName)
        {
            string absoluFilePath = HttpContext.Server.MapPath("~") + "\\download\\" + fileName;
            return File(new FileStream(absoluFilePath, FileMode.Open), "application/octet-stream", Server.UrlEncode(fileName));
        }

        /// <summary>
        /// 验证工程编号重复
        /// </summary>
        /// <param name="projNumber"></param>
        /// <returns></returns>
        public string ValidateProjNumber(string projNumber)
        {
            string result = "";
            int projId = TypeParse.parse<int>(Request["projId"]);
            if (projId > 0)
            {
                if (dbProject.Count(p => p.ProjNumber.Equals(projNumber.Trim()) && p.Id != projId) == 0)
                    result = "success";
            }
            else
            {
                if (dbProject.Count(p => p.ProjNumber.Equals(projNumber.Trim())) == 0)
                    result = "success";
            }
            return result;
        }

        public string GetJsonByProject(int id)
        {
            var model = dbProject.Get(id);
            string strjson = JavaScriptSerializerUtil.getJson(new
            {
                id = model.Id,
                ProjName = model.ProjName,
                ProjNumber = model.ProjNumber,
                ProjEmpId = model.ProjEmpId,
                ProjTypeID = model.ProjTypeID,
                ProjPropertyID = model.ProjPropertyID,
                ProjVoltID = model.ProjVoltID,
                ColAttType2 = model.ColAttType2,
                ProjAreaID = model.ProjAreaID,
                ColAttType1 = model.ColAttType1,
                ColAttType4 = model.ColAttType4,
                TaskBasisName = model.TaskBasisName,
                TaskBasisNumber = model.TaskBasisNumber,
                CustId = model.CustID,
                CustName = model.CustName,
                model.CustLinkMan,
                model.CustLinkTel,
                model.CustLinkWeb,
                ProjTaskSource = model.ProjTaskSource,
                ProjDepId = model.ProjDepId,
                ProjJoinDepIds = model.ProjJoinDepIds,
                CreatorEmpName = model.CreatorEmpName,
                DateCreate = model.DateCreate,
                DatePlanStart = model.DatePlanStart,
                DatePlanFinish = model.DatePlanFinish,
                ProjTaskContent = model.ProjTaskContent,
                ProjFeeSource = model.ProjFeeSource,
                DatePlanDeliver = model.DatePlanDeliver,
                ProjDemand = model.ProjDemand,
                ProjNoteOther = model.ProjNoteOther,
                ColAttVal1 = model.ColAttVal1,
            });
            return strjson;
        }


        public int GetProjCount(int mainId, string ProjNumber, int projId, string projXml)
        {
            var _ProjNumber = HttpUtility.HtmlDecode(ProjNumber);
            var _projXml = HttpUtility.HtmlDecode(projXml);
            int Count = dbProject.GetProjCount(mainId, _ProjNumber, projId, _projXml);
            return Count;
        }

        public ActionResult DelIso(int id)
        {

            int result = 1;
            try
            {
                DataModel.Models.Flow flow = new DBSql.PubFlow.Flow().Get(id);

                new DBSql.PubFlow.Flow().Delete(id);
            }
            catch (Exception)
            {
                result = 0;
            }
            DoResult dr = result > 0 ? DoResultHelper.doSuccess(result, "操作成功") : DoResultHelper.doSuccess(result, "操作失败");
            return Json(dr);
        }

        public ActionResult DelIso1(int id,string tableName)
        {
            int result = 1;
            try
            {
                var opFlowModel = new DBSql.PubFlow.FlowModel();
                string tableNameEng = opFlowModel.FirstOrDefault(p => p.ModelName == tableName) == null ? "" : opFlowModel.FirstOrDefault(p => p.ModelName == tableName).ModelRefTable;
                DataModel.Models.Flow flow = new DBSql.PubFlow.Flow().Get(id);

                result = DAL.DBExecute.ExecuteNonQuery(string.Format(" update {0} set deleterEmpId = {1},deleterEmpName = '{2}',DeletionTime = '{3}' where Id = {4}", tableNameEng, userInfo.EmpID, userInfo.EmpName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), id));
                //new DBSql.PubFlow.Flow().Delete(new int[1] { id }, tableNameEng);
            }
            catch (Exception)
            {
                result = 0;
            }
            DoResult dr = result > 0 ? DoResultHelper.doSuccess(result, "操作成功") : DoResultHelper.doSuccess(result, "操作失败");
            return Json(dr);
        }

        public bool GetIsFinish(int projId,string phaseId)
        {
            return true;
            var proj = new DBSql.Project.Project().Get(projId);
            if (proj.ProjPropertyID == 1637)    //项目性质为规划则可直接报出
                return true;
            var 勘察List = new DBSql.Iso.IsoGCDZKCTJDJZ().GetList(p => p.ProjID == projId && p.ProjPhaseId == phaseId && p.DeleterEmpId == 0).Select(p => p.Id).ToList();
            var 工程测量List = new DBSql.Iso.IsoGCCLTJD().GetList(p => p.ProjID == projId && p.ProjPhaseId == phaseId && p.DeleterEmpId == 0).Select(p => p.Id).ToList();
            var 指导纪要List = new DBSql.Iso.IsoZYZDJYB().GetList(p => p.ProjId == projId && p.ProjPhaseId == phaseId && p.DeleterEmpId == 0).Select(p => p.Id).ToList();
            var 设计策划List = DAL.DBExecute.ExecuteDataTable("SELECT FlowXml.value('(/Root/Projects/Project/@id)[1]', 'nvarchar(max)') AS projid FROM dbo.Flow WHERE FlowName = '设计策划单' AND FlowXml.value('(/Root/Projects/Project/@id)[1]', 'nvarchar(max)') = " + projId + " ORDER BY flow.iD DESC");
            var op = new DBSql.PubFlow.Flow();
            bool isTrue = true;
            //常规的4个必要表单
            if (op.Count(o => o.FlowName == "工程地质勘察条件单" && 勘察List.Contains(o.FlowRefID)) == 0 || op.Count(o => o.FlowName == "工程测量条件单" && 工程测量List.Contains(o.FlowRefID)) == 0 || op.Count(o => o.FlowName == "专业指导纪要表" && 指导纪要List.Contains(o.FlowRefID)) == 0 || 设计策划List.Rows.Count == 0)
                isTrue = false;

            //设计策划单中如果要求评审一定要设计评审纪要表
            //有2个设计策划单 一个要评审一个不要则count(isReview) > 1 代表有一个需要评审则count查询是否填写纪要表
            string isExist = DAL.DBExecute.ExecuteScalar(@"SELECT CASE WHEN(SELECT COUNT(IsReview) FROM dbo.IsoSJCH WHERE  DeleterEmpId = 0 and projid = " + projId+ @") > 1 THEN COUNT(1)
                                                            WHEN(SELECT IsReview FROM dbo.IsoSJCH WHERE  DeleterEmpId = 0 and projid = " + projId + @") = 1 then COUNT(1)
                                                            ELSE 1 END  FROM dbo.IsoSJPSJYB where  DeleterEmpId = 0 and projid = " + projId).ToString();
            if (int.Parse(isExist) == 0)
                isTrue = false;
            return isTrue;
        }
    }
}
