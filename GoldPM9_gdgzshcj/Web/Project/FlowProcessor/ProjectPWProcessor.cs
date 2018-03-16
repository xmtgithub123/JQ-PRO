using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DataModel.Models;
using DBSql.PubFlow;
using JQ.Web;
using System.Xml;
using JQ.Util;
using System.Web.SessionState;
using System.Web.Script.Serialization;

namespace Project.FlowProcessor
{
    public class ProjectPWProcessor : IFlowProcessor
    {

        public DbContext CurrentDbContext
        {
            get; set;
        }

        public FlowWareOptions Options
        {
            get; set;
        }

        public HttpRequest Request
        {
            get; set;
        }

        public HttpSessionState Session
        {
            get; set;
        }

        public Action<object> SetCreateProperties
        {
            get; set;
        }

        public Action<object> SetModifyProperties
        {
            get; set;
        }

        public void OnAfterApproveFinished()
        {
            if (model == null)
            {
                return;
            }
            this.Options.Message = "项目编号:" + model.ProjNumber + ",项目名称" + model.ProjName;
            if (this.Options.Action == DBSql.PubFlow.Action.Reject)
            {
                this.Options.Message += "流程审批未通过！";
            }
            else
            {
                this.Options.Message += "流程审批通过！";

                #region  DesTaskGroup及DesTaskGantt图基础数据 
                DBSql.Design.DesTaskGroup desTaskGroupDB = new DBSql.Design.DesTaskGroup();
                DBSql.Design.DesTaskGantt desTaskGanttDB = new DBSql.Design.DesTaskGantt();
                DataModel.EmpSession userInfo = Session["Employee"] as DataModel.EmpSession;//获取当前登录的用户
                var list = this.CurrentDbContext.Set<DataModel.Models.Project>().Where(m => m.BridgeGuid == this.Options.Guid && m.ParentId != 0 && m.DeleterEmpId == 0).ToList();
                foreach (DataModel.Models.Project p in list)
                {
                    p.BridgeGuid = this.Options.Guid;
                    p.FlowID = this.Options.FlowID;
                    this.CurrentDbContext.Entry<DataModel.Models.Project>(p).State = System.Data.Entity.EntityState.Modified;

                    DataModel.Models.DesTaskGroup projTaskGroup = new DesTaskGroup();
                    DataModel.Models.DesTaskGantt projTaskGantt = new DesTaskGantt();
                    #region 开始节点
                    try
                    {
                        DateTime FPlanStart = TypeParse.parse<DateTime>(p.DatePlanStart, Convert.ToDateTime("1900-01-01"));
                        DateTime FPlanFinish = TypeParse.parse<DateTime>(p.DatePlanFinish, Convert.ToDateTime("1900-01-01"));
                        projTaskGroup = desTaskGroupDB.InsertProjGroupNode(p.Id, string.Format("[{0}]{1}", p.ProjNumber, p.ProjName), p.ProjEmpId, p.ProjEmpName, p.DatePlanStart, p.DatePlanFinish, DBSql.Design.TaskGroupStatus.进行中, userInfo, p.ProjNumber, p.ProjName);
                        if (FPlanStart.Year == 1900 && FPlanFinish.Year == 1900)
                        {
                            FPlanStart = DateTime.Now;
                            FPlanFinish = DateTime.Now;
                        }
                        if (FPlanStart.Year != 1900 && FPlanFinish.Year == 1900)
                        {
                            FPlanFinish = FPlanStart.AddDays(1);
                        }
                        if (FPlanStart.Year == 1900 && FPlanFinish.Year != 1900)
                        {
                            FPlanStart = FPlanFinish.AddDays(-1);
                        }
                        int FIntervalDays = TypeHelper.GetIntervalDays(FPlanStart, FPlanFinish);
                        int FDuration = (FIntervalDays == 0 ? 1 : FIntervalDays) + 1;
                        projTaskGantt = desTaskGanttDB.InsertProjTaskGantt(p.Id, string.Format("[{0}]{1}", p.ProjNumber, p.ProjName), FDuration, projTaskGroup.Id, p.ProjEmpId, FPlanStart, FPlanFinish, userInfo);

                    }
                    catch { }
                    #endregion

                    #region 分拆对应的阶段
                    var projPhaseIDs = p.ProjPhaseIds.Split(',');
                    var t = 0;
                    string json = p.ProjPhaseInfo;
                    if (json != "")
                    {
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


                                    string ProjNumber = row.ProjNumber;
                                    string ProjName = row.ProjName;
                                    t++;


                                    DateTime CPlanStart = TypeParse.parse<DateTime>(row.DatePlanStart, Convert.ToDateTime("1900-01-01"));
                                    DateTime CPlanFinish = TypeParse.parse<DateTime>(row.DatePlanFinish, Convert.ToDateTime("1900-01-01"));

                                    var phaseTaskGroup = desTaskGroupDB.InsertPhaseGroupNode(projTaskGroup, PhaseId, PhaseName, phaseEmpId, phaseEmpName, CPlanStart, CPlanFinish, (t == 1 ? DBSql.Design.TaskGroupStatus.进行中 : DBSql.Design.TaskGroupStatus.未启动), userInfo, ProjNumber, ProjName);

                                    if (CPlanStart.Year == 1900 && CPlanFinish.Year == 1900)
                                    {
                                        CPlanStart = DateTime.Now;
                                        CPlanFinish = DateTime.Now;
                                    }
                                    if (CPlanStart.Year != 1900 && CPlanFinish.Year == 1900)
                                    {
                                        CPlanFinish = CPlanStart.AddDays(1);
                                    }
                                    if (CPlanStart.Year == 1900 && CPlanFinish.Year != 1900)
                                    {
                                        CPlanStart = CPlanFinish.AddDays(-1);
                                    }
                                    int CIntervalDays = TypeHelper.GetIntervalDays(CPlanStart, CPlanFinish);
                                    int CDuration = (CIntervalDays == 0 ? 1 : CIntervalDays) + 1;
                                    projTaskGantt.DatePlanStart = CPlanStart;
                                    projTaskGantt.DatePlanFinish = CPlanFinish;
                                    projTaskGantt.Duration = CDuration;

                                    desTaskGanttDB.InsertPhaseTaskGantt(projTaskGantt, phaseTaskGroup, userInfo);
                                    break;
                                }
                            }
                        }

                    }
                    #endregion
                }

                #endregion
            }
        }


        public int Distance(DateTime dateBegin, DateTime dateEnd)
        {
            TimeSpan span = dateEnd - dateBegin;
            int AllDays = Convert.ToInt32(span.TotalDays) + 1;//差距的所有天数
            int totleWeek = AllDays / 7;//差别多少周
            int yuDay = AllDays % 7; //除了整个星期的天数
            int lastDay = 0;
            if (yuDay == 0) //正好整个周
            {
                lastDay = AllDays - (totleWeek * 2);
            }
            else
            {
                int weekDay = 0;
                int endWeekDay = 0; //多余的天数有几天是周六或者周日
                switch (dateBegin.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        weekDay = 1;
                        break;
                    case DayOfWeek.Tuesday:
                        weekDay = 2;
                        break;
                    case DayOfWeek.Wednesday:
                        weekDay = 3;
                        break;
                    case DayOfWeek.Thursday:
                        weekDay = 4;
                        break;
                    case DayOfWeek.Friday:
                        weekDay = 5;
                        break;
                    case DayOfWeek.Saturday:
                        weekDay = 6;
                        break;
                    case DayOfWeek.Sunday:
                        weekDay = 7;
                        break;
                }
                if ((weekDay == 6 && yuDay >= 2) || (weekDay == 7 && yuDay >= 1) || (weekDay == 5 && yuDay >= 3) || (weekDay == 4 && yuDay >= 4) || (weekDay == 3 && yuDay >= 5) || (weekDay == 2 && yuDay >= 6) || (weekDay == 1 && yuDay >= 7))
                {
                    endWeekDay = 2;
                }
                if ((weekDay == 6 && yuDay < 1) || (weekDay == 7 && yuDay < 5) || (weekDay == 5 && yuDay < 2) || (weekDay == 4 && yuDay < 3) || (weekDay == 3 && yuDay < 4) || (weekDay == 2 && yuDay < 5) || (weekDay == 1 && yuDay < 6))
                {
                    endWeekDay = 1;
                }
                lastDay = AllDays - (totleWeek * 2) - endWeekDay;
            }
            return lastDay;
        }

        public void OnExecuted(bool isSuccess)
        {

        }

        public void OnExecuting()
        {

        }
        DataModel.Models.Project model = null;

        public void OnSaveForm()
        {
            switch (this.Options.Action)
            {
                case DBSql.PubFlow.Action.Save:
                case DBSql.PubFlow.Action.Next:
                case DBSql.PubFlow.Action.Back:
                case DBSql.PubFlow.Action.Finish:
                case DBSql.PubFlow.Action.Reject:
                case DBSql.PubFlow.Action.Transfer:

                    Guid strGuid = this.Options.Guid;

                    int FactAttachID = 0;
                    int IsQuote = 0;
                    int parentId = 0;

                    string companyType = Request.Params["CompanyType"].ToString();
                    int CompanyID = 0;
                    switch (companyType)
                    {
                        case "SJ":
                            CompanyID = 1;
                            break;
                        case "GC":
                            CompanyID = 2;
                            break;
                        case "CJ":
                            CompanyID = 3;
                            break;
                        default:
                            CompanyID = 0;
                            break;
                    }

                    #region 插入项目主表
                    if (this.Options.RefID > 0)
                    {
                        parentId = TypeHelper.parseInt(Request.Form["projId"]);
                        model = this.CurrentDbContext.Set<DataModel.Models.Project>().FirstOrDefault(m => m.Id == parentId);
                        model.CompanyID = CompanyID;
                        if (model == null)
                        {
                            throw new Common.JQException("无法找到有效的实例");
                        }
                        IsQuote = JQ.Util.TypeParse.parse<int>("IsQuote");
                        //strGuid = parentmolde.BridgeGuid;
                        model.MvcSetValueExceptKeys("Id");
                        model.IsQuote = 0;
                        SetModifyProperties(model);
                        #region 客户单位保存
                        DBSql.Bussiness.AddCustmer addModel = new DBSql.Bussiness.AddCustmer();
                        addModel.CustName = model.CustName;
                        addModel.CustLinkMan = model.CustLinkMan;
                        addModel.CustLinkTel = model.CustLinkTel;
                        addModel.CustLinkMail = model.CustLinkWeb;
                        addModel.EmpModel = this.Options.CurrentUser;
                        int _custId = addModel.AddCust();
                        model.CustID = _custId > 0 ? _custId : 0;
                        #endregion
                        this.CurrentDbContext.Entry(model).State = EntityState.Modified;
                        this.CurrentDbContext.SaveChanges();
                    }
                    else
                    {
                        //strGuid = System.Guid.NewGuid();
                        var _projId = TypeHelper.parseInt(Request.Form["projId"]);
                        if (_projId > 0)
                        {
                            IsQuote = 1;
                            model = this.CurrentDbContext.Set<DataModel.Models.Project>().FirstOrDefault(m => m.Id == _projId);
                            if (model == null)
                            {
                                throw new Common.JQException("无法找到有效的实例");
                            }
                            model.MvcSetValueExceptKeys("Id", "ProjPhaseIds", "ProjNumber", "ProjName");
                            SetModifyProperties(model);
                            parentId = TypeHelper.parseInt(Request.Form["projId"]);
                        }
                        else
                        {
                            model = new DataModel.Models.Project();
                            model.MvcSetValue();
                            SetCreateProperties(model);
                            model.BridgeGuid = strGuid;
                        }

                        model.CompanyID = CompanyID;
                    }
                    if (model.Id == 0)
                    {
                        #region 客户单位保存
                        DBSql.Bussiness.AddCustmer addModel = new DBSql.Bussiness.AddCustmer();
                        addModel.CustName = model.CustName;
                        addModel.CustLinkMan = model.CustLinkMan;
                        addModel.CustLinkTel = model.CustLinkTel;
                        addModel.CustLinkMail = model.CustLinkWeb;
                        addModel.EmpModel = this.Options.CurrentUser;
                        int _custId = addModel.AddCust();
                        model.CustID = _custId > 0 ? _custId : 0;
                        #endregion
                        this.CurrentDbContext.Set<DataModel.Models.Project>().Add(model);
                        this.CurrentDbContext.SaveChanges();
                        parentId = model.Id;

                    }
                    #endregion

                    #region  操作子项
                    #region 插入和修改 
                    string ColAttVal2 = model.ColAttVal2;
                    List<DataModel.Models.Project> listProjPhase = new JavaScriptSerializer().Deserialize<List<DataModel.Models.Project>>(Request.Params["HiddenProjPhaseGridData"].ToString());
                    DataModel.EmpSession userInfo = Session["Employee"] as DataModel.EmpSession;//获取当前登录的用户    

                    var first = 0;

                    listProjPhase.ForEach(p =>
                    {
                        p.CompanyID = CompanyID;

                        if (p.DateCreate.Year == 1900)
                            p.DateCreate = DateTime.Now;
                        if (p.DatePlanStart.Year == 1)
                            p.DatePlanStart = DateTime.Parse("1900-01-01");
                        if (p.DatePlanFinish.Year == 1)
                            p.DatePlanFinish = DateTime.Parse("1900-01-01");

                        if (p.Id > 0)
                        {
                            p.MvcDefaultEdit(userInfo);
                            p.ColAttVal2 = string.Format("{0}/", ColAttVal2 == "" ? parentId.ToString() : ColAttVal2 + "/" + parentId.ToString());
                            if (first == 0)
                            {
                                FactAttachID = p.Id;
                            }

                            #region 继承model
                            p.ProjPropertyID = model.ProjPropertyID;
                            p.ColAttType2 = model.ColAttType2;
                            p.ProjAreaID = model.ProjAreaID;
                            p.ColAttType1 = model.ColAttType1;
                            p.ColAttType4 = model.ColAttType4;
                            p.CustName = model.CustName;
                            p.CustLinkMan = model.CustLinkMan;
                            p.CustLinkTel = model.CustLinkTel;
                            p.CustLinkWeb = model.CustLinkWeb;
                            p.ProjTaskSource = model.ProjTaskSource;
                            p.ProjDepId = model.ProjDepId;
                            p.ProjJoinDepIds = model.ProjJoinDepIds;
                            p.BridgeFact = FactAttachID;
                            p.ParentId = parentId;
                            #endregion

                            this.CurrentDbContext.Entry(p).State = EntityState.Modified;
                            this.CurrentDbContext.SaveChanges();
                        }
                        else
                        {


                            p.MvcDefaultSave(userInfo);
                            p.ParentId = parentId;
                            p.ColAttVal2 = string.Format("{0}/", ColAttVal2 == "" ? parentId.ToString() : ColAttVal2 + "/" + parentId.ToString());
                            p.BridgeGuid = strGuid;
                            p.ParentId = parentId;
                            p.IsQuote = IsQuote;

                            #region 继承model
                            p.ProjPropertyID = model.ProjPropertyID;
                            p.ColAttType2 = model.ColAttType2;
                            p.ProjAreaID = model.ProjAreaID;
                            p.ColAttType1 = model.ColAttType1;
                            p.ColAttType4 = model.ColAttType4;
                            p.CustName = model.CustName;
                            p.CustLinkMan = model.CustLinkMan;
                            p.CustLinkTel = model.CustLinkTel;
                            p.CustLinkWeb = model.CustLinkWeb;
                            p.ProjTaskSource = model.ProjTaskSource;
                            p.ProjDepId = model.ProjDepId;
                            p.ProjJoinDepIds = model.ProjJoinDepIds;
                            p.BridgeFact = FactAttachID;
                            #endregion

                            this.CurrentDbContext.Set<DataModel.Models.Project>().Add(p);
                            this.CurrentDbContext.SaveChanges();
                            if (first == 0)
                            {
                                FactAttachID = p.Id;
                            }

                        }

                        if (first == 0)
                        {
                            FactAttachID = p.Id;
                            p.BridgeFact = FactAttachID;
                        }
                        else
                        {
                            p.BridgeFact = FactAttachID;
                        }
                        first++;
                    });
                    #endregion

                    #region 删除
                    List<string> delList = new List<string>();
                    if (this.Options.RefID > 0)
                    {
                        var list = this.CurrentDbContext.Set<DataModel.Models.Project>().Where(m => m.BridgeGuid == this.Options.Guid && m.ParentId != 0 && m.DeleterEmpId == 0).ToList();
                        if (null != list && listProjPhase.Count > 0)
                        {
                            foreach (DataModel.Models.Project m in list)
                            {
                                var isIn = false;
                                foreach (DataModel.Models.Project k in listProjPhase)
                                {
                                    if (m.Id == k.Id)
                                    {
                                        isIn = true;
                                        break;
                                    }
                                }
                                if (!isIn)
                                {
                                    List<string> lst1 = m.ProjPhaseIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                    foreach (string t1 in lst1)
                                    {
                                        if (!delList.Contains(t1)) { delList.Add(t1); }
                                    }
                                    this.CurrentDbContext.Set<DataModel.Models.Project>().Remove(m);
                                }
                            }
                        }
                    }
                    #endregion

                    #region
                    List<string> listGather = model.ProjPhaseIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    List<string> factdelList = new List<string>();
                    if (this.Options.RefID > 0)
                    {
                        var tList = this.CurrentDbContext.Set<DataModel.Models.Project>().Where(m => m.ParentId == this.Options.BridgeID && m.DeleterEmpId == 0 && m.BridgeGuid != this.Options.Guid);
                        foreach (DataModel.Models.Project tp in tList)
                        {
                            List<string> at = tp.ProjPhaseIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            foreach (string s in delList)
                            {
                                if (!at.Contains(s))
                                {
                                    factdelList.Add(s);
                                }
                            }
                        }
                    }

                    foreach (DataModel.Models.Project t in listProjPhase)
                    {
                        List<string> at = t.ProjPhaseIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        foreach (string s in at)
                        {
                            if (!listGather.Contains(s)) { listGather.Add(s); }
                        }
                    }
                    foreach (string f in factdelList)
                    {
                        listGather.Remove(f);
                    }

                    model.ProjPhaseIds = string.Join(",", listGather.ToArray());
                    this.CurrentDbContext.Entry(model).State = EntityState.Modified;
                    #endregion

                    #endregion

                    #region 成本项
                    int projId = model.Id;
                    if (Request.Params["HiddenData"] != null)
                    {
                        List<ProjCost> listProjCost = new List<ProjCost>();
                        DBSql.Project.ProjCost dbProjCost = new DBSql.Project.ProjCost();
                        dbProjCost.DbContextRepository(CurrentDbContext);
                        List<Controllers.ProjectController.ProjCostModelDto> listProjCostModelDto = new JavaScriptSerializer().Deserialize<List<Controllers.ProjectController.ProjCostModelDto>>(Request.Params["HiddenData"].ToString());

                        if (dbProjCost.Count(m => m.ProjID == projId) > 0)
                        {
                            CostDTOtoEntity(listProjCostModelDto, listProjCost);

                            listProjCost.ForEach(p =>
                            {
                                p.MvcDefaultEdit(userInfo);
                                p.ProjID = projId;
                                dbProjCost.Edit(p);
                            });
                            this.CurrentDbContext.SaveChanges();
                        }
                        else
                        {
                            CostDTOtoEntityDB(listProjCostModelDto, listProjCost, projId, dbProjCost, 0);
                        }
                    }
                    #endregion

                    #region 移动文件
                    if (this.Options.RefID == 0)
                    {
                        var ba = new DBSql.Sys.BaseAttach();
                        ba.DbContextRepository(this.CurrentDbContext);
                        ba.MoveFile(FactAttachID, this.Options.CurrentUser.EmpID, this.Options.CurrentUser.EmpName);
                        this.Options.RefID = FactAttachID;
                    }
                    else
                    {

                        var flowModel = this.CurrentDbContext.Set<DataModel.Models.Flow>().FirstOrDefault(f => f.FlowRefTable == "Project" && f.FlowRefID == this.Options.RefID);
                        if (flowModel != null)
                        {
                            flowModel.FlowRefID = FactAttachID;
                            this.CurrentDbContext.Entry(flowModel).State = EntityState.Modified;
                            this.CurrentDbContext.SaveChanges();
                        }

                        var Attachlist = this.CurrentDbContext.Set<DataModel.Models.BaseAttach>().Where(m => m.AttachRefTable == "Project" && m.AttachRefID == this.Options.RefID).ToList();
                        if (null != Attachlist)
                        {
                            foreach (DataModel.Models.BaseAttach Attach in Attachlist)
                            {
                                Attach.AttachRefID = FactAttachID;
                                this.CurrentDbContext.Entry(Attach).State = EntityState.Modified;
                                this.CurrentDbContext.SaveChanges();
                            }
                        }
                        this.Options.RefID = FactAttachID;
                    }
                    #endregion


                    // 缓存项目组成员
                    Task.Factory.StartNew(delegate () {
                        DBSql.Design.DesTaskGroup desTaskGroupDB = new DBSql.Design.DesTaskGroup();
                        desTaskGroupDB.SetProjectEmps(model.Id);
                    });

                    this.Options.Title = "项目编号:" + model.ProjNumber + ",项目名称:" + model.ProjName;
                    break;
            }
        }

        public void CostDTOtoEntity(List<Controllers.ProjectController.ProjCostModelDto> listProjCostModelDto, List<ProjCost> listProjCost)
        {
            foreach (Controllers.ProjectController.ProjCostModelDto dto in listProjCostModelDto)
            {
                DataModel.Models.ProjCost projCost = new ProjCost();
                EntityHelpers.EntityToEntity(dto, projCost, new string[] { "Cost", "Model" });
                listProjCost.Add(projCost);

                if (dto.children != null)
                    CostDTOtoEntity(dto.children.ToList(), listProjCost);
            }
        }
        public void CostDTOtoEntityDB(List<Controllers.ProjectController.ProjCostModelDto> listProjCostModelDto, List<ProjCost> listProjCost, int projId, DBSql.Project.ProjCost dbProjCost, int parentId)
        {
            foreach (Controllers.ProjectController.ProjCostModelDto dto in listProjCostModelDto)
            {
                DataModel.Models.ProjCost projCost = new ProjCost();
                EntityHelpers.EntityToEntity(dto, projCost, new string[] { "Cost", "Model" });
                listProjCost.Add(projCost);
                DataModel.EmpSession userInfo = Session["Employee"] as DataModel.EmpSession;//获取当前登录的用户
                projCost.MvcDefaultSave(userInfo);
                projCost.ProjID = projId;
                projCost.ParentId = projCost.ParentId == 0 ? 0 : parentId;
                dbProjCost.Add(projCost);
                this.CurrentDbContext.SaveChanges();

                if (dto.children != null)
                    CostDTOtoEntityDB(dto.children.ToList(), listProjCost, projId, dbProjCost, projCost.Id);
            }
        }

    }
}
