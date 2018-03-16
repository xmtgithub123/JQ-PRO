using DBSql.PubFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web;
using System.Web.SessionState;
using JQ.Web;
using JQ.Util;
using DataModel;

namespace Bussiness.FlowProcessor
{
    class BussBiddingFile : IFlowProcessor
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
            this.Options.Message = "投标文件:" + model.ProjName;
            if (this.Options.Action == DBSql.PubFlow.Action.Reject)
            {
                this.Options.Message += "流程审批未通过！";
            }
            else
            {
                this.Options.Message += "流程审批通过！";
            }
        }

        public void OnExecuted(bool isSuccess)
        {

        }

        public void OnExecuting()
        {

        }

        DBSql.Sys.BaseData dbBaseData = new DBSql.Sys.BaseData();
        DBSql.Sys.BaseEmployee dbBaseEmployee = new DBSql.Sys.BaseEmployee();
        DBSql.Project.Project dbProject = new DBSql.Project.Project();

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
                    int result = 0;
                    int type = 0;
                    //页面model 项目信息
                    model = new DataModel.Models.Project();
                    var _chilid = new DataModel.Models.Project();

                    if (this.Options.RefID > 0)
                    {
                        _chilid = dbProject.Get(this.Options.RefID);
                        model = dbProject.FirstOrDefault(p => p.DeleterEmpId == 0 && p.Id == _chilid.ParentId);
                        model.MvcSetValueExceptKeys("Id");
                        _chilid.MvcSetValueExceptKeys("Id");
                    }
                    else
                    {
                        model.MvcSetValue();
                        _chilid.MvcSetValue();
                    }

                    model.ColAttType6 = 1;
                    model.ColAttType7 = TypeHelper.parseInt(Request.Form["BiddingId"]);
                    model.ColAttType8 = TypeHelper.parseInt(Request.Form["BiddingInfoID"]);
                    var _ProjEmpId = Request.Form["ProjEmpId"];
                    model.ProjEmpId = TypeHelper.parseInt(_ProjEmpId.Split('-')[0]);
                    model.ProjEmpName = dbBaseEmployee.GetBaseEmployeeInfo(model.ProjEmpId).EmpName;

                    var _BusinessEmpId = Request.Form["BusinessEmpId"];
                    model.ColAttType9 = TypeHelper.parseInt(_BusinessEmpId.Split('-')[0]);

                    var _TechnologyEmpId = Request.Form["ProjEmpId"];
                    model.ColAttType10 = TypeHelper.parseInt(_TechnologyEmpId.Split('-')[0]);
                    model.ProjPhaseIds = ((int)ProjectPhase.前期投标文件).ToString();
                    model.ColAttType11 = TypeHelper.parseInt(Request.Form["Duration"]);

                    string companyType = Request.Params["CompanyType"].ToString();
                    switch (companyType)
                    {
                        case "SJ":
                            model.CompanyID = 1;
                            _chilid.CompanyID = 1;
                            break;
                        case "GC":
                            model.CompanyID = 2;
                            _chilid.CompanyID = 2;
                            break;
                        case "CJ":
                            model.CompanyID = 3;
                            _chilid.CompanyID = 3;
                            break;
                        default:
                            model.CompanyID = 0;
                            _chilid.CompanyID = 0;
                            break;
                    }

                    _chilid.ColAttType6 = 1;
                    _chilid.ColAttType7 = TypeHelper.parseInt(Request.Form["BiddingId"]);
                    _chilid.ColAttType8 = TypeHelper.parseInt(Request.Form["BiddingInfoID"]);

                    _chilid.ColAttType9 = model.ColAttType9;
                    _chilid.ColAttType10 = model.ColAttType10;
                    _chilid.ProjEmpId = model.ProjEmpId;
                    _chilid.ProjPhaseIds = ((int)ProjectPhase.前期投标文件).ToString();
                    model.ProjNumber = model.ProjName;
                    _chilid.ProjNumber = _chilid.ProjName;
                    _chilid.ProjEmpName = model.ProjEmpName;
                    _chilid.ColAttType11 = model.ColAttType11;
                    if (model.Id > 0)
                    {
                        type = 1;
                        model.MvcDefaultEdit(this.Options.CurrentUser);
                        _chilid.MvcDefaultEdit(this.Options.CurrentUser);
                        dbProject.Edit(model);

                    }
                    else
                    {
                        model.DateCreate = DateTime.Now;
                        type = 0;
                        model.MvcDefaultSave(this.Options.CurrentUser);
                        _chilid.MvcDefaultSave(this.Options.CurrentUser);
                        dbProject.Add(model);
                    }
                    dbProject.UnitOfWork.SaveChanges();
                    result = model.Id;

                    ////新插入的子项信息
                    int parentId = model.Id;
                    string ColAttVal2 = model.ColAttVal2;

                    if (type == 0)
                    {
                        _chilid.ParentId = parentId;
                        _chilid.ColAttVal2 = string.Format("{0}/", ColAttVal2 == "" ? parentId.ToString() : ColAttVal2 + "/" + parentId.ToString());
                        dbProject.Add(_chilid);
                        dbProject.UnitOfWork.SaveChanges();
                        this.Options.RefID = _chilid.Id;
                        var ba = new DBSql.Sys.BaseAttach();
                        ba.MoveFile(_chilid.Id, this.Options.CurrentUser.EmpID, this.Options.CurrentUser.EmpName);
                    }
                    else
                    {
                        _chilid.ColAttVal2 = string.Format("{0}/", ColAttVal2 == "" ? parentId.ToString() : ColAttVal2 + "/" + parentId.ToString());
                        dbProject.Edit(_chilid);
                        dbProject.UnitOfWork.SaveChanges();
                    }
                    //---------------------------------------------------DesTaskGroup------------------------------------------
                    //插入协同设计相关表数据
                    DBSql.Design.DesTaskGroup desTaskGroupDB = new DBSql.Design.DesTaskGroup();
                    DBSql.Design.DesTaskGantt desTaskGanttDB = new DBSql.Design.DesTaskGantt();
                    // 插入项目分组起始节点
                    var projTaskGroup = desTaskGroupDB.InsertProjGroupNode(_chilid.Id, string.Format("[{0}]{1}", _chilid.ProjNumber, _chilid.ProjName), _chilid.ProjEmpId, _chilid.ProjEmpName, _chilid.DatePlanStart, _chilid.DatePlanFinish, DBSql.Design.TaskGroupStatus.进行中, this.Options.CurrentUser, _chilid.ProjNumber, _chilid.ProjName);

                    var projTaskGantt = desTaskGanttDB.InsertProjTaskGantt(_chilid.Id, string.Format("[{0}]{1}", _chilid.ProjNumber, _chilid.ProjName), _chilid.ColAttType11, projTaskGroup.Id, _chilid.ProjEmpId, _chilid.DatePlanStart, _chilid.DatePlanFinish, this.Options.CurrentUser);


                    // 获取项目阶段信息
                    var projPhaseIDs = _chilid.ProjPhaseIds.Split(',');
                    var projPhases = new DBSql.Sys.BaseData().GetQuery(x => projPhaseIDs.Contains(x.BaseID.ToString()));
                    // 插入项目阶段节点
                    var v = 0;
                    foreach (var projPhase in projPhases)
                    {
                        v++;
                        var phaseTaskGroup = desTaskGroupDB.InsertPhaseGroupNode(projTaskGroup, projPhase.BaseID, projPhase.BaseName, _chilid.ProjEmpId, _chilid.ProjEmpName, _chilid.DatePlanStart, _chilid.DatePlanFinish, (v == 1 ? DBSql.Design.TaskGroupStatus.进行中 : DBSql.Design.TaskGroupStatus.未启动), this.Options.CurrentUser, _chilid.ProjNumber, _chilid.ProjName);
                        desTaskGanttDB.InsertPhaseTaskGantt(projTaskGantt, phaseTaskGroup, this.Options.CurrentUser);
                    }
                    
                    this.Options.Title = "投标文件:" + model.ProjName;
                    break;
            }
        }
    }
}
