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
    class OaEquipReturnFlow : IFlowProcessor
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
                var originalValues = this.CurrentDbContext.Set<DataModel.Models.OaEquipStock>().Where(m => m.EquipFormID == model.Id && m.EquipFormType == "OaEquipReturn");
                foreach (OaEquipStock oaModel in originalValues)
                {
                    oaModel.EquipActionID = 3;
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

        private DataModel.Models.OaEquipReturn model = null;

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
                        model = this.CurrentDbContext.Set<DataModel.Models.OaEquipReturn>().FirstOrDefault(m => m.Id == this.Options.RefID);
                        if (model == null)
                        {
                            throw new Common.JQException("无法找到有效的实例");
                        }
                        SetModifyProperties(model);
                    }
                    else
                    {
                        model = new DataModel.Models.OaEquipReturn();
                        //SetCreateProperties(model);
                    }
                    string EquipId = Request.Form["EquipId"] == null ? "" : Request.Form["EquipId"].ToString().Split('&')[0];
                    string UseId = Request.Form["EquipId"] == null ? "" : Request.Form["EquipId"].ToString().Split('&')[1];
                    if(int.Parse(UseId) != 0)
                        model.UseId = int.Parse(UseId);
                    int EquipOrOffice = model.EquipOrOffice;
                    model.MvcSetValue();
                    if (EquipOrOffice != model.EquipOrOffice)
                        model.EquipOrOffice = EquipOrOffice;
                    if (model.Id == 0)
                    {
                        model.MvcSetValue();
                        model.MvcDefaultSave(this.Options.CurrentUser);
                        this.CurrentDbContext.Set<DataModel.Models.OaEquipReturn>().Add(model);
                        this.CurrentDbContext.SaveChanges();
                        var ba = new DBSql.Sys.BaseAttach();
                        ba.DbContextRepository(this.CurrentDbContext);
                        ba.MoveFile(model.Id, this.Options.CurrentUser.EmpID, this.Options.CurrentUser.EmpName);
                        this.Options.RefID = model.Id;
                    }

                    //获取出原先老的数据
                    var originalValues = this.CurrentDbContext.Set<DataModel.Models.OaEquipStock>().Where(m => m.EquipFormID == model.Id&&m.EquipFormType== "OaEquipReturn");

                    if (EquipId != "" && EquipId.Length > 2)// && int.Parse(UseId) != 0)
                    {
                        DataTable dt = JavaScriptSerializerUtil.JsonToDataTable(EquipId);
                        //List<int> ids = new List<int>();
                        foreach (DataRow row in dt.Rows)
                        {
                            //ids.Add(JQ.Util.TypeParse.parse<int>(row["Id"]));
                            int rowID = JQ.Util.TypeParse.parse<int>(row["Id"]);
                            //var stock = originalValues.FirstOrDefault(m => m.EquipFormID == model.Id);
                            var stock = originalValues.FirstOrDefault(m => m.Id == rowID);
                            if (stock == null)
                            {
                                stock = new DataModel.Models.OaEquipStock();
                                stock.EquipFormID = model.Id;
                                stock.EquipNote = "设备归还";
                                stock.MvcDefaultSave(this.Options.CurrentUser);
                                stock.EquipFormType = "OaEquipReturn";
                                this.CurrentDbContext.Set<DataModel.Models.OaEquipStock>().Add(stock);

                                //赋值
                                stock.EquipID = JQ.Util.TypeParse.parse<int>(row["EquipID"].ToString());
                                stock.EquipActionID = 3;
                                //stock.EquipCount = int.Parse(row["EquipCount"].ToString());
                                stock.EquipDateTime = DateTime.Now;
                            }
                            ////赋值
                            //stock.EquipID = JQ.Util.TypeParse.parse<int>(row["EquipID"].ToString());
                            //stock.EquipActionID = 3;
                            //stock.EquipDateTime = DateTime.Now;
                            stock.EquipCount = int.Parse(row["EquipCount"].ToString());

                        }

                        //var delModel = this.CurrentDbContext.Set<DataModel.Models.OaEquipStock>().Where(p => p.EquipFormID == model.Id && p.EquipFormType == "OaEquipReturn" && !ids.Contains(p.Id));

                        //this.CurrentDbContext.Set<DataModel.Models.OaEquipStock>().RemoveRange(delModel);
                    }

                    this.CurrentDbContext.SaveChanges();

                    this.Options.Title = "[" + model.CreationTime.ToString("yyyy-MM-dd") + "]" + model.CreatorEmpName + "的设备归还申请单";
                    break;
            }
        }

    }
}
