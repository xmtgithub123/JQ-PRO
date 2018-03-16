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
using System.Data;
using DataModel.Models;

namespace OA.FlowProcessor
{
  public  class OaMeetingUseFlow : IFlowProcessor
    {
        public DbContext CurrentDbContext
        {
            get;
            set;
        }

        public FlowWareOptions Options
        {
            get;

            set;
        }

        public HttpRequest Request
        {
            get;

            set;
        }

        public HttpSessionState Session
        {
            get;

            set;
        }

        public Action<object> SetCreateProperties
        {
            get;

            set;
        }

        public Action<object> SetModifyProperties
        {
            get;

            set;
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

                //将会议信息添加到日程记录
                DataModel.Models.Scheduler schModel = new DataModel.Models.Scheduler();
                //将记录写到申请人的记录
                schModel.CreationTime = System.DateTime.Now;
                schModel.CreatorEmpId = model.CreatorEmpId;
                schModel.CreatorEmpName = model.CreatorEmpName;
                string RoomName = model.FK_OaMeetingUse_MeetingID == null ? "" : model.FK_OaMeetingUse_MeetingID.RoomName;//会议室名称
                schModel.Content = RoomName+"-"+model.MeetingUseConent;//会议室+用途
                string startDate = model.MeetingStartDate.ToString("yyyy-MM-dd");
                string endDate = model.MeetingEndDate.ToString("yyyy-MM-dd");
                //判断是否跨越天数
                if(startDate!=endDate)
                {
                    schModel.IsFullDay = true;//表示全天日程
                    schModel.StartTime = TypeHelper.parseDateTime(startDate);//不包含小时
                    schModel.EndTime = TypeHelper.parseDateTime(endDate);
                    schModel.RemindBefore = 1;
                    schModel.RemindBeforeType = 3;//提醒类型为小时
                    schModel.NotifyTime = schModel.StartTime.AddDays(-1);

                }
                else
                {
                    schModel.IsFullDay = false;
                    schModel.StartTime = model.MeetingStartDate;//开始时间
                    schModel.EndTime = model.MeetingEndDate;//结束时间
                    //默认为提醒(提前1小时)
                    schModel.RemindBefore = 1;
                    schModel.RemindBeforeType = 2;//提醒类型为小时
                    schModel.NotifyTime = schModel.StartTime.AddHours(-1);


                }
                schModel.JoinEmpIDs = model.CreatorEmpId.ToString()+","+model.MeetingLeader.ToString();//会议负责人+申请人
                StringBuilder stb = new StringBuilder();
                stb.Append("<Root>");
                stb.Append("<Notify EmpID=\""+model.CreatorEmpId.ToString()+"\"  DateTime=\"\" Status=\"0\"/>");
                stb.Append("<Notify EmpID=\"" + model.MeetingLeader.ToString() + "\"  DateTime=\"\" Status=\"0\"/>");
                stb.Append("</Root>");
                schModel.Attributes = stb.ToString();
                schModel.RefID = model.Id;
                schModel.RefTable = "OaMeetingUse";
                this.CurrentDbContext.Set<DataModel.Models.Scheduler>().Add(schModel);
                this.CurrentDbContext.SaveChanges();
            }
        }

        public void OnExecuted(bool isSuccess)
        {

        }

        public void OnExecuting()
        {

        }

        private DataModel.Models.OaMeetingUse model = null;

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
                        model = this.CurrentDbContext.Set<DataModel.Models.OaMeetingUse>().FirstOrDefault(m => m.Id == this.Options.RefID);
                        if (model == null)
                        {
                            throw new Common.JQException("无法找到有效的实例");
                        }
                        SetModifyProperties(model);
                    }
                    else
                    {
                        model = new DataModel.Models.OaMeetingUse();
                        model.MvcDefaultSave(this.Options.CurrentUser.EmpID);
                    }
                    model.MvcSetValue();
                    DateTime startDate = TypeHelper.parseDateTime(Request.Params["StartDate"]);
                    int startHour = TypeHelper.parseInt(Request.Params["startHour"]);
                    int startMinu = TypeHelper.parseInt(Request.Params["startMinu"]);
                    model.MeetingStartDate = startDate.AddHours(startHour).AddMinutes(startMinu);//计划开始时间
                    DateTime endDate = TypeHelper.parseDateTime(Request.Params["EndDate"]);
                    int endHour = TypeHelper.parseInt(Request.Params["endHour"]);
                    int endMinu = TypeHelper.parseInt(Request.Params["endMinu"]);
                    model.MeetingEndDate = endDate.AddHours(endHour).AddMinutes(endMinu);

                    if (model.Id == 0)
                    {
                        model.MvcDefaultSave(this.Options.CurrentUser.EmpID);
                        this.CurrentDbContext.Set<DataModel.Models.OaMeetingUse>().Add(model);
                        this.CurrentDbContext.SaveChanges();
                        using (var ba = new DBSql.Sys.BaseAttach())
                        {
                            ba.MoveFile(model.Id, this.Options.CurrentUser.EmpID, this.Options.CurrentUser.EmpName);
                        }
                        this.Options.RefID = model.Id;
                    }
                    this.Options.Title = "会议室申请单";
                    break;
            }
        }

    }
}
