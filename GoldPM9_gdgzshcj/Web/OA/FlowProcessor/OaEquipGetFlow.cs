using DBSql.PubFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Models;
using System.Data.Entity;
using System.Web;
using System.Web.SessionState;
using JQ.Web;
using JQ.Util;
using System.Data;

namespace OA.FlowProcessor
{
    class OaEquipGetFlow : IFlowProcessor
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
                var originalValues = this.CurrentDbContext.Set<DataModel.Models.OaEquipStock>().Where(m => m.EquipFormID == model.Id && m.EquipFormType == "OaEquipGet");
                foreach(OaEquipStock oaModel in originalValues)
                {
                    oaModel.EquipActionID = 1;
                }

                this.CurrentDbContext.SaveChanges();
                this.Options.Message += "流程审批通过！";
            }
        }

        public void OnExecuted(bool isSuccess)
        {
        }

        public void OnExecuting()
        {

        }

        private DataModel.Models.OaEquipGet model = null;

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
                        model = this.CurrentDbContext.Set<DataModel.Models.OaEquipGet>().FirstOrDefault(m => m.Id == this.Options.RefID);
                        if (model == null)
                        {
                            throw new Common.JQException("无法找到有效的实例");
                        }
                        SetModifyProperties(model);
                    }
                    else
                    {
                        model = new OaEquipGet();
                        SetCreateProperties(model);
                    }
                    model.MvcSetValue();
                    if (model.Id == 0)
                    {
                        model.MvcDefaultSave(this.Options.CurrentUser);
                        this.CurrentDbContext.Set<DataModel.Models.OaEquipGet>().Add(model);
                        this.CurrentDbContext.SaveChanges();
                        var ba = new DBSql.Sys.BaseAttach();
                        ba.DbContextRepository(this.CurrentDbContext);
                        ba.MoveFile(model.Id, this.Options.CurrentUser.EmpID, this.Options.CurrentUser.EmpName);
                        this.Options.RefID = model.Id;
                    }

                    //获取出原先老的数据
                    var originalValues = this.CurrentDbContext.Set<DataModel.Models.OaEquipStock>().Where(m => m.EquipFormID == model.Id && m.EquipFormType == "OaEquipGet");
                    string EquipId = Request.Form["EquipId"] == null ? "" : Request.Form["EquipId"].ToString();
                    if (EquipId != "")
                    {
                        DataTable dt = JavaScriptSerializerUtil.JsonToDataTable(EquipId);
                        List<int> ids = new List<int>();
                        foreach (DataRow row in dt.Rows)
                        {
                            ids.Add(JQ.Util.TypeParse.parse<int>(row["Id"]));
                            int rowID = JQ.Util.TypeParse.parse<int>(row["Id"]);
                            var stock = originalValues.FirstOrDefault(m => m.Id == rowID);
                            if (stock == null)
                            {
                                stock = new DataModel.Models.OaEquipStock();
                                stock.EquipFormID = model.Id;
                                stock.EquipNote = "设备采购";
                                stock.MvcDefaultSave(this.Options.CurrentUser);
                                stock.EquipFormType = "OaEquipGet";
                                this.CurrentDbContext.Set<DataModel.Models.OaEquipStock>().Add(stock);
                            }
                            //赋值
                            stock.EquipID = JQ.Util.TypeParse.parse<int>(row["EquipID"].ToString());
                            stock.EquipActionID = 0;
                            stock.EquipCount = Convert.ToInt32(row["Number"]);
                            stock.EquipDateTime = DateTime.Now;
                        }

                        var delModel = this.CurrentDbContext.Set<DataModel.Models.OaEquipStock>().Where(p =>p.EquipFormID==model.Id&& p.EquipFormType == "OaEquipGet" && !ids.Contains(p.Id));

                        this.CurrentDbContext.Set<DataModel.Models.OaEquipStock>().RemoveRange(delModel);
                    }

                    this.CurrentDbContext.SaveChanges();

                    this.Options.Title = "[" + model.CreationTime.ToString("yyyy-MM-dd") + "]" + model.CreatorEmpName + "的设备采购申请单";
                    break;
            }
        }
    }
}
