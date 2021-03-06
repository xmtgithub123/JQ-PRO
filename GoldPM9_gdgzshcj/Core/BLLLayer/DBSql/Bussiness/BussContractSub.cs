﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-07-12 14:56:40
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Common.Data;
using Common.Data.Extenstions;
using DAL;
using System.Data.Entity;
using JQ.Util;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace DBSql.Bussiness
{
    public class BussContractSub : EFRepository<DataModel.Models.BussContractSub>
    {
        public void CreateOrUpdate(DataModel.Models.BussContractSub model, string ProjSubIDs, int CreateEmpId, int CustID)
        {
            using (var accessor = base.DbContext)
            {
                accessor.Database.Connection.Open();
                int MainTableID = -1;
                if (model.Id == 0)
                {
                    model.CreateEmpId = CreateEmpId;
                }                
                model.ConSubCustId = CustID;

                using (var tran = base.DbContext.Database.BeginTransaction())
                {
                    #region 新增/修改数据
                    if (model.Id > 0)
                    {
                        accessor.Entry(model).State = System.Data.Entity.EntityState.Modified;
                        accessor.SaveChanges();
                        MainTableID = model.Id;
                    }
                    else
                    {
                        accessor.Set<DataModel.Models.BussContractSub>().Add(model);
                        accessor.SaveChanges();
                        MainTableID = model.Id;
                    }
                    #endregion
                    string[] list = ProjSubIDs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    var source = accessor.Set<DataModel.Models.ProjSub>().Where(m => m.ConSubID == MainTableID).ToList();
                    RecuriseCreateOrUpdate(list, accessor, MainTableID);
                    RecuriseDelete(source, list, accessor);
                    accessor.SaveChanges();
                    tran.Commit();
                }
            }
        }
        public void OnlyCreateOrUpdate(DataModel.Models.BussContractSub model, string ProjSubIDs, int CreateEmpId, int CustID)
        {
            using (var accessor = base.DbContext)
            {
                accessor.Database.Connection.Open();
                if (model.Id == 0)
                {
                    model.CreateEmpId = CreateEmpId;
                }
                model.ConSubCustId = CustID;

                using (var tran = base.DbContext.Database.BeginTransaction())
                {
                    string[] list = ProjSubIDs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    var source = accessor.Set<DataModel.Models.ProjSub>().Where(m => m.ConSubID == model.Id).ToList();
                    RecuriseCreateOrUpdate(list, accessor, model.Id);
                    RecuriseDelete(source, list, accessor);
                    accessor.SaveChanges();
                    tran.Commit();
                }
            }
        }
        private void RecuriseCreateOrUpdate(string[] list, DbContext accessor, int mainTableID)
        {
            foreach (string item in list)
            {
                var id = TypeHelper.parseInt(item);
                var data = accessor.Set<DataModel.Models.ProjSub>().FirstOrDefault(m => m.Id == id);
                if (null != data)
                {
                    data.ConSubID = mainTableID;
                    accessor.Entry(data).State = System.Data.Entity.EntityState.Modified;
                    accessor.SaveChanges();
                    id = data.Id;
                }
            }
        }
        private void RecuriseDelete(List<DataModel.Models.ProjSub> sources, string[] list, DbContext accessor)
        {

            foreach (var source in sources)
            {
                var isIn = false;
                foreach (string item in list)
                {
                    if (TypeHelper.parseInt(item) == source.Id)
                    {
                        isIn = true;
                        break;
                    }
                }
                if (!isIn)
                {
                    var data = accessor.Set<DataModel.Models.ProjSub>().FirstOrDefault(m => m.Id == source.Id);
                    if (data == null)
                    {
                        continue;
                    }
                    data.ConSubID = 0;
                    accessor.Entry(data).State = System.Data.Entity.EntityState.Modified;
                    accessor.SaveChanges();
                }
            }
        }


        public DataTable GetListInfo(Common.SqlPageInfo queryContext, DataModel.EmpSession userInfo = null)
        {
            string RowColumn = "s.Id,s.ConSubNumber,s.ConSubName,s.ConSubDate,s.ConSubFee,c.CustName,s.ArchNumber,s.CreatorEmpId";
            RowColumn += ",isnull((select Sum(SubFeeFactMoney) from  BussSubFeeFact where DeleterEmpId=0 and  ConSubId=s.Id),0) as  AlreadySumFeeMoney";
            RowColumn += ",isnull((select Sum(SubFeeInvoiceMoney) from  BussSubFeeInvoice where DeleterEmpId=0 and  ConSubId=s.Id),0) as  AlreadySumInvoiceMoney";
            RowColumn += ", UnPay = (s.ConSubFee - isnull((select Sum(SubFeeFactMoney) from  BussSubFeeFact where DeleterEmpId=0 and ConSubId=s.Id),0))";
            RowColumn += ",isnull((select baseName from basedata where baseID=s.ConSubType),'') as  ConSubTypeName";
            RowColumn += ",isnull((select baseName from basedata where baseID=s.ConSubCategory),'') as  ConSubCategoryName";
            RowColumn += ",isnull((select baseName from basedata where baseID=s.ConSubStatus),'') as  ConSubStatusName";
            RowColumn += ",f.Id AS FlowID, f.FlowName,f.FlowStatusID,f.FlowStatusName,f.FlowXml.value('(Root/TurnedEmpIDs/text())[1]', 'nvarchar(200)') AS FlowTurnedEmpIDs ";
          
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select Count(1) from dbo.BussContractSub AS s LEFT JOIN dbo.BussCustomer AS c ON s.ConSubCustId = c.Id  LEFT JOIN Flow f ON f.FlowRefID=s.Id and f.FlowRefTable=@RefTable where  s.DeleterEmpId=0 ");
            SqlParameter[] paras = {
                new SqlParameter("@TextCondtion",SqlDbType.VarChar),
                new SqlParameter("@ConSubDateS",SqlDbType.DateTime),
                new SqlParameter("@ConSubDateE",SqlDbType.DateTime),
                new SqlParameter("@ArchNumber",SqlDbType.VarChar),
                new SqlParameter("@CreatorDepId",SqlDbType.Int),
                new SqlParameter("@CreatorEmpId",SqlDbType.Int),
                new SqlParameter("@ConSubCustId",SqlDbType.Int),
                new SqlParameter("@ProjectID",SqlDbType.Int),
                new SqlParameter("@CompanyID",SqlDbType.Int),
                new SqlParameter("@RefTable",SqlDbType.VarChar),
            };
            paras[9].Value = "BussContractSub";

            if (!string.IsNullOrEmpty(queryContext.TextCondtion))
            {
                strSql.Append(" and (s.ConSubNumber like '%'+@TextCondtion+'%' or s.ConSubName like '%'+@TextCondtion+'%' or c.CustName like '%'+@TextCondtion+'%') ");
                paras[0].Value = queryContext.TextCondtion;
            }

            if (queryContext.SelectCondtion != null && queryContext.SelectCondtion.Count > 0)
            {
                foreach (DictionaryEntry de in queryContext.SelectCondtion)
                {
                    if (de.Value == null || de.Value.ToString().Trim() == "") continue;

                    switch (de.Key.ToString())
                    {
                        case "ConSubDateS":
                            if (de.Value.ToString() != "")
                            {
                                strSql.Append(" and s.ConSubDate>=@ConSubDateS");
                                paras[1].Value = Convert.ToDateTime(de.Value);
                            }
                            break;
                        case "ConSubDateE":
                            if (de.Value.ToString() != "")
                            {
                                strSql.Append(" and s.ConSubDate<=@ConSubDateE");
                                paras[2].Value = Convert.ToDateTime(de.Value).AddHours(23.99);
                            }
                            break;
                        case "ArchNumber":
                            if (de.Value.ToString() != "")
                            {
                                strSql.Append(" and (s.ArchNumber like '%'+@ArchNumber+'%') ");
                                paras[3].Value = de.Value.ToString();
                            }
                            break;
                        case "ConSubType":
                            if (de.Value.ToString() != "")
                            {
                                strSql.Append(string.Format(" and s.ConSubType in ({0})", de.Value.ToString()));
                            }
                            break;
                        case "ConSubStatus":
                            if (de.Value.ToString() != "")
                            {
                                strSql.Append(string.Format(" and s.ConSubStatus in ({0})", de.Value.ToString()));
                            }
                            break;
                        case "ConSubCategory":
                            if (de.Value.ToString() != "")
                            {
                                strSql.Append(string.Format(" and s.ConSubCategory in ({0})", de.Value.ToString()));
                            }
                            break;

                        case "QueryDeptID":
                            {
                                if (de.Value.ToString() != "0")
                                {
                                    strSql.Append(" and s.CreatorDepId=@CreatorDepId");
                                    paras[4].Value = Convert.ToInt32(de.Value.ToString());
                                }
                            }
                            break;
                        case "QueryEmpID":
                            {
                                if (de.Value.ToString() != "0")
                                {
                                    strSql.Append(" and s.CreatorEmpId=@CreatorEmpId");
                                    paras[5].Value = Convert.ToInt32(de.Value.ToString());
                                }
                            }
                            break;
                        case "IsAudit":
                            {
                                if (de.Value.ToString() != "0")
                                {
                                    strSql.Append(" and (SELECT COUNT(*) FROM dbo.Flow WHERE FlowRefTable=@RefTable AND FlowRefID=s.id AND  FlowStatusID=3)>0");
                                }
                            }
                            break;
                        case "ConSubCustId":
                            {
                                if (de.Value.ToString() != "0")
                                {
                                    strSql.Append(" and s.ConSubCustId=@ConSubCustId");
                                    paras[6].Value = Convert.ToInt32(de.Value.ToString());
                                }
                            }
                            break;
                        case "ProjectID":
                            {
                                if (de.Value.ToString() != "0")
                                {
                                    strSql.AppendFormat(" and (select count(Id) FROM ProjSub where ProjID in(SELECT * from dbo.F_Split('{0}',',')) and DeleterEmpId=0 and ConSubID=s.Id)>0", de.Value.ToString());
                                }
                            }
                            break;
                        case "EngineeringNumber":
                            if (de.Value.ToString() != "0")
                            {
                                strSql.AppendFormat(" and EXISTS (SELECT * FROM dbo.F_Split(EngineeringNumber,',') WHERE s IN (SELECT * from dbo.F_Split('{0}',',')))", de.Value.ToString());
                            }
                            break;
                        case "CompanyID":
                            strSql.Append(" and s.CompanyID=@CompanyID ");
                            paras[8].Value = Convert.ToInt32(de.Value.ToString());
                            break;
                        case "RefTable":
                            paras[9].Value = de.Value.ToString();
                            break;
                        default:
                            break;
                    }
                }
            }
            //---------------- 得到总记录数-------------------------//
            object obj = DBExecute.ExecuteScalar(DBExecute.ConnectionString, strSql.ToString(), paras);

            if (obj == null && obj == DBNull.Value) queryContext.PageTotleRowCount = 0;
            else queryContext.PageTotleRowCount = Convert.ToInt32(obj);
            //------------------------------------------------------//

            if (String.IsNullOrEmpty(queryContext.SelectOrder))
            {
                queryContext.SelectOrder = "s.ConSubDate desc";
            }

            string sql = Helper.SqlPage.ExecPageStrSql(queryContext, RowColumn, strSql);

            return DBExecute.ExecuteDataTable(DBExecute.ConnectionString, sql.ToString(), paras);
        }

        public void UpdateBussContractSubInfoList(int[] ids, DataModel.EmpSession userInfo)
        {
            if (ids.Length == 0)
            {
                return;
            }
            string idSet = string.Join(",", ids);
            if (string.IsNullOrEmpty(idSet))
            {
                return;
            }
            var sbSQL = new StringBuilder();
            sbSQL.Append(string.Format("Update BussContractSub set DeleterEmpId={0},DeleterEmpName='{1}',DeletionTime='{2}' WHERE ID IN ({3})", userInfo.EmpID, userInfo.EmpName, DateTime.Now, idSet));
            sbSQL.Append(string.Format("Update BussSubFeeFact set DeleterEmpId={0},DeleterEmpName='{1}',DeletionTime='{2}' WHERE ID IN ({3})", userInfo.EmpID, userInfo.EmpName, DateTime.Now, idSet));
            sbSQL.Append(string.Format("Update BussSubFeeInvoice set DeleterEmpId={0},DeleterEmpName='{1}',DeletionTime='{2}' WHERE ID IN ({3})", userInfo.EmpID, userInfo.EmpName, DateTime.Now, idSet));
            sbSQL.Append(string.Format("Update ProjSub set ConSubID=0 WHERE ConSubID IN ({0})", idSet));
            DAL.DBExecute.ExecuteNonQuery(sbSQL.ToString());
        }
    }
}
