using DataModel.Models;
using DBSql.PubFlow;
using JQ.Util;
using JQ.Web;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace ISO.FlowProcessor
{
    class IsoXMCBFlow : IFlowProcessor
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
            }
        }

        public void OnExecuted(bool isSuccess)
        {

        }

        private DataModel.Models.IsoXMCB model = null;

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
                        model = this.CurrentDbContext.Set<DataModel.Models.IsoXMCB>().FirstOrDefault(m => m.Id == this.Options.RefID);
                        if (model == null)
                        {
                            throw new Common.JQException("无法找到有效的实例");
                        }
                        SetModifyProperties(model);
                    }
                    else
                    {
                        model = new DataModel.Models.IsoXMCB();
                        model.MvcDefaultSave(this.Options.CurrentUser.EmpID);
                    }
                    model.MvcSetValue();

                    model.ProjID = TypeHelper.parseInt(Request.Form["projId"]);
                    model.ProjPhaseId = TypeHelper.parseInt(Request.Form["ProjPhaseId"].ToString());

                    model.DesTaskGroupId = TypeHelper.parseInt(Request.Form["desTaskGroupId"]);
                    model.CompanyID = TypeHelper.parseInt(Request.Form["CompanyID"]);

                    DataModel.EmpSession userInfo = Session["Employee"] as DataModel.EmpSession;//获取当前登录的用户 
                    int TableId = 0;
                    if (model.Id == 0)
                    {
                        model.MvcDefaultSave(this.Options.CurrentUser);
                        this.CurrentDbContext.Set<DataModel.Models.IsoXMCB>().Add(model);
                        this.CurrentDbContext.SaveChanges();
                        TableId = model.Id;

                    }
                    else
                    {
                        TableId = this.Options.RefID;
                    }

                    #region
                    if (Request.Params["HiddenData"] != null)
                    {
                        List<ProjCost> listProjCost = new List<ProjCost>();
                        DBSql.Project.ProjCost dbProjCost = new DBSql.Project.ProjCost();
                        dbProjCost.DbContextRepository(CurrentDbContext);

                        List<ProjCostModelDto> listProjCostModelDto = new JavaScriptSerializer().Deserialize<List<ProjCostModelDto>>(Request.Params["HiddenData"].ToString());
                        if (dbProjCost.Count(m => m.XMCBID == TableId) > 0)
                        {
                            CostDTOtoEntity(listProjCostModelDto, listProjCost);

                            listProjCost.ForEach(p =>
                            {
                                p.MvcDefaultEdit(userInfo);
                                p.XMCBID = TableId;
                                p.ProjID = model.ProjID;
                                p.ProjPhaseId = model.ProjPhaseId;
                                p.DesTaskGroupId = model.DesTaskGroupId;
                                dbProjCost.Edit(p);
                            });
                            this.CurrentDbContext.SaveChanges();
                        }
                        else
                        {
                            CostDTOtoEntityDB(listProjCostModelDto, listProjCost, TableId, model.ProjID, dbProjCost, 0, model.ProjPhaseId, model.DesTaskGroupId);
                        }
                    }
                    #endregion

                    this.CurrentDbContext.SaveChanges();

                    var ba = new DBSql.Sys.BaseAttach();
                    ba.DbContextRepository(this.CurrentDbContext);
                    ba.MoveFile(model.Id, this.Options.CurrentUser.EmpID, this.Options.CurrentUser.EmpName);
                    this.Options.RefID = model.Id;


                    string ProjNumber = Request.Form["ProjNumber"].ToString();
                    string ProjName = Request.Form["ProjName"].ToString();
                    this.Options.Title = "[" + ProjNumber + "]" + ProjName + "的项目成本";
                    break;
            }
        }

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
        public void CostDTOtoEntityDB(List<ProjCostModelDto> listProjCostModelDto, List<ProjCost> listProjCost, int tableId, int projId, DBSql.Project.ProjCost dbProjCost, int parentId, int projPhaseId, int desTaskGroupId)
        {
            foreach (ProjCostModelDto dto in listProjCostModelDto)
            {
                DataModel.Models.ProjCost projCost = new ProjCost();
                EntityHelpers.EntityToEntity(dto, projCost, new string[] { "Cost", "Model" });
                listProjCost.Add(projCost);
                DataModel.EmpSession userInfo = Session["Employee"] as DataModel.EmpSession;//获取当前登录的用户
                projCost.MvcDefaultSave(userInfo);
                projCost.CostId = dto.Id;
                projCost.XMCBID = tableId;
                projCost.ProjID = projId;

                projCost.ProjPhaseId = projPhaseId;
                projCost.DesTaskGroupId = desTaskGroupId;

                projCost.ParentId = projCost.ParentId == 0 ? 0 : parentId;
                dbProjCost.Add(projCost);
                this.CurrentDbContext.SaveChanges();

                if (dto.children != null)
                    CostDTOtoEntityDB(dto.children.ToList(), listProjCost, tableId, projId, dbProjCost, projCost.Id, projPhaseId, desTaskGroupId);
            }
        }
    }

    public class ProjCostModelDto : ProjCostModel
    {
        public string state { get; set; }
        public decimal ModelFactFee { get; set; }
        public IList<ProjCostModelDto> children { get; set; }
    }
}
