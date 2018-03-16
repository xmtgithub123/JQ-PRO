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
using DBSql.Design;
using System.Web.Script.Serialization;
using Design.Controllers;

namespace Design.FlowProcessor
{
    public class DesExchApprove : IFlowProcessor
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
            this.Options.Message = "[" + model.ExchTitle + "]提资审批";

            if (this.Options.Action == DBSql.PubFlow.Action.Reject)
            {
                this.Options.Message += "流程审批未通过！";
            }
            else
            {
                this.Options.Message += "流程审批通过！";
                DataModel.Models.Flow flowModel = this.CurrentDbContext.Set<DataModel.Models.Flow>().FirstOrDefault(m => m.FlowRefTable == "DesExch" && m.FlowRefID == this.Options.RefID);
                IQueryable<DataModel.Models.DesExchRec> list = this.CurrentDbContext.Set<DataModel.Models.DesExchRec>().Where(p => p.ExchId == model.Id && p.RecEmpId != 0);
                if (flowModel != null)
                {
                    var emps = new List<int>();
                    foreach (DataModel.Models.DesExchRec recModel in list)
                    {
                        emps.Add(recModel.RecEmpId);
                        DataModel.Models.FlowNodeExe exeModel = new DataModel.Models.FlowNodeExe();
                        exeModel.FlowID = flowModel.Id;
                        exeModel.ExeActionID = (int)DataModel.NodeAction.轮到;
                        exeModel.ExeActionDate = System.DateTime.Now;
                        exeModel.ExeEmpId = recModel.RecEmpId;
                        exeModel.ExeEmpName = recModel.RecEmpName;
                        //exeModel.FlowNodeID = 220;
                        this.CurrentDbContext.Set<DataModel.Models.FlowNodeExe>().Add(exeModel);
                    }
                    this.CurrentDbContext.SaveChanges();
                    //消息通知 +1
                    var t = JQ.Util.IO.MessageMonitor.NotifyAsync(emps, delegate (int _empID, JQ.Util.IO.MessageMonitor.NotifyOption _option)
                    {
                        if (_option == null || (_option != null && ("," + _option.AgentFlows + ",").IndexOf("," + this.Options.RefTable + ",") > -1))
                        {
                            return new { action = "ChangeToDoExchRecAmount", amount = 1 };
                        }
                        return null;
                    });
                }

                #region 更新项目动态及日志      
                var _desExch = model = this.CurrentDbContext.Set<DataModel.Models.DesExch>().FirstOrDefault(m => m.Id == this.Options.RefID);

                DBSql.Project.ProjectDynamic ProjectDynamicDB = new DBSql.Project.ProjectDynamic();
                DBSql.Sys.BaseLog __log = new DBSql.Sys.BaseLog();
                var grouplist = this.CurrentDbContext.Set<DataModel.Models.DesTaskGroup>().Where(m => m.TaskGroupType == 1 && m.ProjId == _desExch.ProjId && m.DeleterEmpId == 0).ToList();
                foreach (DataModel.Models.DesTaskGroup d in grouplist)
                {

                    string logRefHTML = String.Format("[{0}]{1}>提资单,被批准", d.ProjNumber, d.TaskGroupName);
                    DataModel.Models.BaseLog log = new DataModel.Models.BaseLog();
                    log.BaseLogRefTable = "DesTaskGroup";
                    log.BaseLogRefID = (int)d.Id;
                    log.BaseLogRefHTML = logRefHTML;
                    log.BaseLogEmpID = this.Options.CurrentUser.EmpID;
                    log.EmpName = this.Options.CurrentUser.EmpName;
                    log.BaseLogIP = this.Options.CurrentUser.LoginIP;
                    log.BaseLogDate = DateTime.Now;
                    log.BaseLogTypeID = 10;
                    __log.Add(log);
                    __log.UnitOfWork.SaveChanges();

                    // 写入 项目动态
                    ProjectDynamicDB.AddDynamic(model.ProjId, "ISOForm", (int)d.Id, logRefHTML, this.Options.CurrentUser);
                }


                #endregion

            }
        }

        public void OnExecuted(bool isSuccess)
        {

        }

        private DataModel.Models.DesExch model = null;
        private DBSql.Design.DesExch desExch = new DBSql.Design.DesExch();
        private DBSql.Sys.AllBaseEmployee emp = new DBSql.Sys.AllBaseEmployee();
        DataModel.Models.OaMess oaMessModel = new DataModel.Models.OaMess();

        public void OnExecuting()
        {

        }

        public void OnSaveForm()
        {
            switch (this.Options.Action)
            {
                case DBSql.PubFlow.Action.Back:
                case DBSql.PubFlow.Action.Save:
                case DBSql.PubFlow.Action.Next:
                case DBSql.PubFlow.Action.Finish:
                case DBSql.PubFlow.Action.Reject:
                case DBSql.PubFlow.Action.Transfer:
                    if (this.Options.RefID > 0)
                    {
                        model = this.CurrentDbContext.Set<DataModel.Models.DesExch>().FirstOrDefault(m => m.Id == this.Options.RefID);
                        if (model == null)
                        {
                            throw new Common.JQException("无法找到有效的实例");
                        }
                        SetModifyProperties(model);
                    }
                    else
                    {
                        model = new DataModel.Models.DesExch();
                        //SetCreateProperties(model);   
                    }
                    model.MvcSetValue();
                    int ReId = TypeHelper.parseInt(Request.Params["ReId"]);//判断是否为新增
                    int ReverseIdent = TypeHelper.parseInt(Request.Params["ReverseIdent"]);//反提标识
                    if (ReId == 0 && ReverseIdent == 4)
                    {
                        //资料重提
                        DataModel.Models.DesExch desExchModel = this.CurrentDbContext.Set<DataModel.Models.DesExch>().FirstOrDefault(p => p.Id == model.FlowId);
                        if (desExchModel != null)
                        {
                            model.ExchSpecName = desExchModel.ExchSpecName;
                            model.ExchEmpId = desExchModel.ExchEmpId;
                            model.ExchEmpName = desExchModel.ExchEmpName;
                            model.ExchIsInvalid = true;
                            model.ExchEmpDepId = desExchModel.ExchEmpDepId;
                            model.ExchEmpDepName = desExchModel.ExchEmpDepName;
                            model.ProjId = desExchModel.ProjId;
                            long taskId = TypeHelper.parseLong(Request.Params["taskId"]);
                            if (taskId != 0)
                            {
                                model.ExchTaskId = taskId;
                            }
                            else
                            {
                                model.ExchTaskId = desExchModel.ExchTaskId;
                            }
                            model.ExchSpecId = desExchModel.ExchSpecId;
                            if (model.ExchSpecId == 0)
                            {
                                model.ExchSpecName = "汇总";
                            }
                            model.FlowId = 0;
                            model.ExchType = desExchModel.ExchType;
                            model.MvcDefaultSave(Options.CurrentUser.EmpID);

                            desExchModel.ExchIsInvalid = false;
                            //this.CurrentDbContext.Entry(desExchModel).State = EntityState.Modified;
                            desExch.Edit(desExchModel);
                        }

                        // this.CurrentDbContext.Set<DataModel.Models.DesExch>().Add(model);
                        //this.CurrentDbContext.SaveChanges();
                        desExch.Add(model);
                        desExch.UnitOfWork.SaveChanges();
                        using (var ba = new DBSql.Sys.BaseAttach())
                        {
                            ba.MoveFile(model.Id, this.Options.CurrentUser.EmpID, this.Options.CurrentUser.EmpName);
                        }
                        this.Options.RefID = model.Id;


                    }
                    if (ReverseIdent == 2 && ReId == 0)//反提
                    {
                        DataModel.Models.DesExchRec desExchRecModel = this.CurrentDbContext.Set<DataModel.Models.DesExchRec>().FirstOrDefault(p => p.Id == model.FlowId);
                        DataModel.Models.DesExch desModel = this.CurrentDbContext.Set<DataModel.Models.DesExch>().FirstOrDefault(p => p.Id == desExchRecModel.ExchId);
                        if (desExchRecModel != null && desModel != null)
                        {
                            model.ExchSpecName = desExchRecModel.RecSpecName;
                            model.ExchEmpDepId = desExchRecModel.RecEmpDepId;
                            model.ExchEmpDepName = desExchRecModel.RecEmpDepName;
                            model.ExchEmpId = desExchRecModel.RecEmpId;
                            model.ProjId = desModel.ProjId;
                            model.taskGroupId = desModel.taskGroupId;
                            model.ExchSpecId = desExchRecModel.RecSpecId;
                            model.ExchEmpName = desExchRecModel.RecEmpName;
                            model.FlowId = 0;
                            model.ExchType = desModel.ExchType;//提资类别
                            model.ExchIsInvalid = true;
                            model.MvcDefaultSave(Options.CurrentUser.EmpID);

                        }
                        desExch.Add(model);
                        desExch.UnitOfWork.SaveChanges();//提交信息
                        using (var ba = new DBSql.Sys.BaseAttach())
                        {
                            ba.MoveFile(model.Id, this.Options.CurrentUser.EmpID, this.Options.CurrentUser.EmpName);
                        }
                        this.Options.RefID = model.Id;

                    }
                    if (ReverseIdent == 3 && ReId == 0)//计划外的新增提资
                    {
                        model.ExchIsInvalid = true;
                        model.ExchType = 2;
                        model.MvcDefaultSave(Options.CurrentUser.EmpID);
                        if (model.ExchSpecId == 0)
                        {
                            model.ExchSpecName = "汇总";
                        }
                        if (model.ExchEmpId != 0)
                        {
                            DataModel.Models.AllBaseEmployee empModel = emp.FirstOrDefault(p => p.EmpID == model.ExchEmpId);
                            if (empModel != null)
                            {
                                model.ExchEmpName = empModel.EmpName;
                                model.ExchEmpDepId = empModel.EmpDepID;
                                model.ExchEmpDepName = empModel.EmpDepName;
                            }
                            desExch.Add(model);
                            desExch.UnitOfWork.SaveChanges();//提交信息
                            using (var ba = new DBSql.Sys.BaseAttach())
                            {
                                ba.MoveFile(model.Id, this.Options.CurrentUser.EmpID, this.Options.CurrentUser.EmpName);
                            }
                            this.Options.RefID = model.Id;

                        }
                    }
                    if (ReverseIdent == 1 && ReId == 0)//新增提资
                    {
                        model.ExchIsInvalid = true;
                        model.ExchType = 2;
                        model.MvcDefaultSave(Options.CurrentUser.EmpID);
                        if (model.ExchSpecId == 0)
                        {
                            model.ExchSpecName = "汇总";
                        }
                        if (model.ExchEmpId != 0)
                        {
                            DataModel.Models.AllBaseEmployee empModel = emp.FirstOrDefault(p => p.EmpID == model.ExchEmpId);
                            if (empModel != null)
                            {
                                model.ExchEmpName = empModel.EmpName;
                                model.ExchEmpDepId = empModel.EmpDepID;
                                model.ExchEmpDepName = empModel.EmpDepName;
                            }
                            desExch.Add(model);
                            desExch.UnitOfWork.SaveChanges();//提交信息
                            using (var ba = new DBSql.Sys.BaseAttach())
                            {
                                ba.MoveFile(model.Id, this.Options.CurrentUser.EmpID, this.Options.CurrentUser.EmpName);
                            }
                            //ba.DbContextRepository(this.CurrentDbContext);
                            this.Options.RefID = model.Id;

                        }
                    }

                    string JsonRows = Request.Params["JsonRows"];//获取收资专业和列表信息
                    DataModel.EmpSession userInfo = Session["Employee"] as DataModel.EmpSession;//获取当前登录的用户
                    List<DataModel.Models.DesExchRec> recList = new List<DataModel.Models.DesExchRec>();
                    List<TempExchRec> list = new List<TempExchRec>();
                    if (!string.IsNullOrEmpty(JsonRows))
                    {
                        list = new JavaScriptSerializer().Deserialize<List<TempExchRec>>(JsonRows);
                        foreach (TempExchRec temp in list)
                        {
                            DataModel.Models.DesExchRec rec = new DataModel.Models.DesExchRec();
                            rec.Id = temp.Id;
                            rec.RecEmpId = TypeHelper.parseInt(temp.RecEmpId);
                            rec.RecEmpName = temp.RecEmpName;
                            rec.RecSpecId = temp.RecSpecId;
                            rec.RecSpecName = temp.RecSpecName;

                            //添加部门和部门编码
                            if (rec.RecEmpId != 0)
                            {
                                rec.RecEmpDepId = emp.FirstOrDefault(p => p.EmpID == rec.RecEmpId).DepartmentID;
                                rec.RecEmpDepName = emp.FirstOrDefault(p => p.EmpID == rec.RecEmpId).DepartmentName;
                            }
                            else
                            {
                                rec.RecEmpDepId = 0;
                                rec.RecEmpDepName = "";
                            }
                            recList.Add(rec);

                        }
                    }
                    if (TypeHelper.isDateTime(Request.Form["ExcLastPutTime"]) && !string.IsNullOrEmpty(Request.Form["ExcLastPutTime"]))
                    {
                        foreach (DataModel.Models.DesExchRec recModel in recList)
                        {
                            recModel.ExcLastPutTime = TypeHelper.parseDateTime(Request.Form["ExcLastPutTime"]);
                        }
                    }
                    // 没有指定提资人员，可以进行抢夺任务,默认第一个提交的人作为提资人员
                    if (model.ExchEmpId == 0)
                    {
                        model.ExchEmpId = this.Options.CurrentUser.EmpID;
                        if (userInfo != null)
                        {
                            desExch.InsertOrUpdateExchRecData(model, recList, userInfo, userInfo);
                        }
                    }
                    else
                    {
                        desExch.InsertOrUpdateExchRecData(model, recList, null, userInfo);
                    }
                    if (!string.IsNullOrEmpty(Request.Params["AttachExchIds"]))
                    {
                        string AttachExchIds = Request.Params["AttachExchIds"];
                        desExch.InsertExchAttach(model.Id, Request.Params["AttachExchIds"]);
                    }
                    this.Options.ProjectIDs.Clear();
                    this.Options.ProjectIDs.Add(model.ProjId);

                    this.Options.Title = "[" + model.ExchTitle + "]" + "-提资单";

                    this.Options.ProjectIDs.Clear();
                    this.Options.ProjectPhases.Clear();
                    this.Options.ProjectPhases.Add(model.ProjId, null);
                    break;
            }
        }


    }
}
