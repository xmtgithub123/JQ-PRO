using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Models;
using DBSql.PubFlow;
using System.Web;
using JQ.Web;
using JQ.Util;
using ISO.Controllers;
using DBSql.Project;
using System.Web.SessionState;
using System.Data.SqlClient;
using System.Web.Script.Serialization;

namespace ISO.FlowProcessor
{
    public class IsoDesignOutputFlow : IFlowProcessor
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

        public Action<object> SetCreateProperties
        {
            get; set;
        }

        public Action<object> SetModifyProperties
        {
            get; set;
        }

        public HttpSessionState Session
        {
            get; set;
        }

        public void OnAfterApproveFinished()
        {
            if (model == null)
            {
                return;
            }
            if (this.Options.Action == DBSql.PubFlow.Action.Reject)
            {
                this.Options.Message += "流程审批未通过！";
            }
            else
            {
                this.Options.Message += "流程审批通过！";

                #region 更新项目动态及日志      
                var _isoForm = this.CurrentDbContext.Set<DataModel.Models.IsoForm>().FirstOrDefault(m => m.FormID == this.Options.RefID);

                DBSql.Project.ProjectDynamic ProjectDynamicDB = new DBSql.Project.ProjectDynamic();
                DBSql.Sys.BaseLog __log = new DBSql.Sys.BaseLog();
                var list = this.CurrentDbContext.Set<DataModel.Models.DesTaskGroup>().Where(m => m.TaskGroupType == 1 && m.ProjId == _isoForm.ProjID && m.DeleterEmpId == 0).ToList();
                foreach (DataModel.Models.DesTaskGroup d in list)
                {

                    string logRefHTML = String.Format("[{0}]{1}>设计评审记录,被批准", d.ProjNumber, d.TaskGroupName);
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
                    ProjectDynamicDB.AddDynamic(_isoForm.ProjID, "ISOForm", (int)d.Id, logRefHTML, this.Options.CurrentUser);
                }


                #endregion
            }

            //this.Options.Title = "[" + model.FK_IsoForm_ProjID == null ? "" : model.FK_IsoForm_ProjID.ProjNumber + "]" + model.FK_IsoForm_ProjID == null ? "" : model.FK_IsoForm_ProjID.ProjName;
        }

        public void OnExecuted(bool isSuccess)
        {

        }

        private DataModel.Models.IsoForm model = null;
        private DesOutPutReview desOut = new DesOutPutReview();
        private DBSql.Project.Project proj = new DBSql.Project.Project();
        private DBSql.Iso.IsoFormNode bussdetail = new DBSql.Iso.IsoFormNode();
        private DBSql.Iso.IsoForm op = new DBSql.Iso.IsoForm();

        public void OnExecuting()
        {

        }

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
                    if (this.Options.RefID > 0)
                    {
                        model = this.CurrentDbContext.Set<DataModel.Models.IsoForm>().FirstOrDefault(m => m.FormID == this.Options.RefID);
                        if (model == null)
                        {
                            throw new Common.JQException("无法找到有效的实例");
                        }
                        SetModifyProperties(model);
                    }
                    else
                    {
                        model = new IsoForm();
                        model.MvcDefaultSave(this.Options.CurrentUser.EmpID);
                        //SetCreateProperties(model);
                    }
                    model.MvcSetValue();
                    desOut.MvcSetValue();
                    string xml = desOut.ModelToXml();
                    model.FormCtlXml = xml;
                    if (model.FormID == 0)
                    {
                        model.RefTable = "IsoFormDesOutPutReview";
                        model.FormName = "设计评审记录";

                        this.CurrentDbContext.Set<DataModel.Models.IsoForm>().Add(model);
                        this.CurrentDbContext.SaveChanges();
                        using (var ba = new DBSql.Sys.BaseAttach())
                        {
                            ba.MoveFile(model.FormID, Options.CurrentUser.EmpID, Options.CurrentUser.EmpName);
                        }
                        this.Options.RefID = model.FormID;
                    }

                    List<OutReviewNode> EvalList = new List<OutReviewNode>();
                    string JsonRows = Request.Form["JsonRows"];

                    if (model.FormID > 0)
                    {
                        model.MvcDefaultEdit(this.Options.CurrentUser.EmpID);
                        this.CurrentDbContext.Entry(model).State = EntityState.Modified;
                        this.CurrentDbContext.SaveChanges();


                    }
                    List<IsoFormNode> detailList = new List<IsoFormNode>();
                    if (JsonRows != null && JsonRows != "")
                    {
                        EvalList = new JavaScriptSerializer().Deserialize<List<OutReviewNode>>(JsonRows);
                        foreach (var m in EvalList)
                        {
                            DataModel.Models.IsoFormNode detail = new IsoFormNode();
                            detail.FormID = model.FormID;
                            detail.RefID = m.RefID;
                            detail.ColAttType1 = m.SpecialId;
                            detail.ColAttVal1 = m.ReviewTarget;
                            detail.ColAttVal2 = m.ReviewContent;
                            detail.ColAttVal3 = m.ReviewResult;
                            detail.ColAttVal4 = m.ReviewSugg;
                            detail.ColAttVal5 = model.RefTable;
                            detailList.Add(detail);
                        }
                        List<long> detailID = detailList.Where(p => p.FormID == model.FormID).Select(p => p.RefID).ToList();
                        bussdetail.Delete(p => !detailID.Contains(p.RefID) && p.FormID == model.FormID);
                        long next = 0;
                        if (bussdetail.GetList(p => p.FormID == model.FormID).Count() > 0)
                        {
                            next = bussdetail.GetList(p => p.FormID == model.FormID).Select(p => p.RefID).Max();
                        }
                        foreach (var m in detailList)
                        {

                            if (m.RefID > 0)
                            {
                                string sql = "update IsoFormNode set ColAttType1=@ColAttType1,ColAttVal1=@ColAttVal1,ColAttVal2=@ColAttVal2,ColAttVal3=@ColAttVal3,ColAttVal4=@ColAttVal4 where FormID=@FormID and RefID=@RefID";
                                List<SqlParameter> para = new List<SqlParameter>();
                                para.Add(new SqlParameter("@ColAttType1", m.ColAttType1));
                                para.Add(new SqlParameter("@ColAttVal1", m.ColAttVal1));
                                para.Add(new SqlParameter("@ColAttVal2", m.ColAttVal2));
                                para.Add(new SqlParameter("@ColAttVal3", m.ColAttVal3));
                                para.Add(new SqlParameter("@ColAttVal4", m.ColAttVal4));
                                para.Add(new SqlParameter("@FormID", m.FormID));
                                para.Add(new SqlParameter("@RefID", m.RefID));
                                bussdetail.ExecuteNonQuery(sql, para.ToArray());

                            }
                            else
                            {
                                next = next + 1;
                                m.RefID = next;
                                bussdetail.Add(m);
                            }

                        }

                        bussdetail.UnitOfWork.SaveChanges();
                    }
                    this.Options.ProjectIDs.Clear();
                    this.Options.ProjectPhases.Clear();
                    this.Options.ProjectPhases.Add(model.ProjID, null);



                    if (proj.Get(model.ProjID) == null)
                    {
                        this.Options.Title = "设计评审记录";
                    }
                    else
                    {
                        this.Options.Title = "[" + proj.Get(model.ProjID) == null ? "" : proj.Get(model.ProjID).ProjNumber + "]" + proj.Get(model.ProjID) == null ? "" : proj.Get(model.ProjID).ProjName+ "--设计评审记录";
                    }
                    break;
            }
        }
    }
}
