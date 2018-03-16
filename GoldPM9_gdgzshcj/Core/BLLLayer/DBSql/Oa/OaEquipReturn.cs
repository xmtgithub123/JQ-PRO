﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2017-03-10 11:06:05
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Common.Data;
using Common.Data.Extenstions;
using DAL;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
namespace DBSql.Oa
{
    public class OaEquipReturn : EFRepository<DataModel.Models.OaEquipReturn>
    {
        public DataTable GetUseEquip(int id)
        {
            string strSql = "select distinct ou.Id,EquipLendDate,EquipLendNote,EquipBackDate from OaEquipUse ou left join OaEquipStock os on ou.Id = os.EquipFormID where EquipActionID = 2";
            return DAL.DBExecute.ExecuteDataTable(strSql.ToString());
        }

        public DataTable GetUseRecord(int id)
        {
            string strSql = @" select * from ( select t1.Id,om.Id as EquipId,om.EquipNumber,om.EquipName,om.EquipModel,t1.EquipCount - ISNULL(t2.EquipCount,0) as Count,t1.EquipDateTime
                        from (select Id,EquipID,equipCount,EquipDateTime,EquipFormID from OaEquipStock where EquipActionID = 2 and EquipFormID = " + id + @") t1  
                        left join  (
                        select EquipID,sum(equipCount) as EquipCount,'" + id + @"' as useId from OaEquipStock 
                        where EquipActionID = 3 and EquipFormID in (select Id from OaEquipReturn where UseId = "+ id + ") group by EquipID "

                        //select Id,equipCount,EquipDateTime,'" + id + @"' as useId from OaEquipStock where EquipActionID != 2 and EquipFormID in (select Id from OaEquipReturn where UseId = " + id + @")
                        + @") t2
                        on t1.EquipFormID = t2.useId and t1.EquipID = t2.EquipID left join OaEquipment om on om.Id = t1.EquipID) t
                        where t.Count > 0 order by t.Id";
            return DAL.DBExecute.ExecuteDataTable(strSql.ToString());
        }

        public string GetNumber(int id,int rid)
        {
            string strSql = @" select * from ( select t1.Id,om.Id as EquipId,om.EquipNumber,om.EquipName,om.EquipModel,t1.EquipCount - ISNULL(t2.EquipCount,0) as Count,t1.EquipDateTime
                        from (select Id,EquipID,equipCount,EquipDateTime,EquipFormID from OaEquipStock where EquipActionID = 2 and EquipFormID = " + id + @") t1  
                        left join  (
                        select EquipID,sum(equipCount) as EquipCount,'" + id + @"' as useId from OaEquipStock 
                        where EquipActionID = 3 and EquipFormID in (select Id from OaEquipReturn where UseId = " + id + ") group by EquipID "

                        //select Id,equipCount,EquipDateTime,'" + id + @"' as useId from OaEquipStock where EquipActionID != 2 and EquipFormID in (select Id from OaEquipReturn where UseId = " + id + @")
                        + @") t2
                        on t1.EquipFormID = t2.useId and t1.EquipID = t2.EquipID left join OaEquipment om on om.Id = t1.EquipID) t
                        where t.EquipId = " + rid + " and  t.Count > 0 order by t.Id";
            return DAL.DBExecute.ExecuteDataTable(strSql.ToString()).Rows.Count > 0 ? DAL.DBExecute.ExecuteDataTable(strSql.ToString()).Rows[0]["Count"].ToString():"0";   
        }
        public DataTable GetUseRecord1(int id, int rid,DateTime time)
        {
            string strSql = @" select distinct oes.Id,om.Id as EquipId,om.EquipNumber,om.EquipName,om.EquipModel,t1.EquipCount - ISNULL(t2.EquipCount,0) as Count,t1.EquipDateTime,ISNULL(t2.EquipCount,0) as           EquipCount,oes.EquipCount eCount
                        from (select Id,EquipID,equipCount,EquipDateTime,EquipFormID from OaEquipStock where EquipActionID = 2 and EquipFormID = " + id + @") t1  
                        left join  (select EquipID,'"+id+@"' as useId ,SUM(EquipCount) EquipCount from OaEquipStock where EquipActionID != 2 and EquipFormID in (select Id from OaEquipReturn where UseId = "+id+@") and  EquipDateTime < (select CreationTime from OaEquipReturn where Id="+rid+ @") group by EquipID) t2
                        on t1.EquipFormID = t2.useId and t1.EquipID = t2.EquipID  left join OaEquipment om on om.Id = t1.EquipID left join OaEquipStock oes on oes.EquipFormID = " + rid + @"
                        and t1.EquipID = oes.EquipID and EquipActionID = 3 
                        where ISNULL(oes.EquipCount, 0) > 0
                        order by oes.Id";
//            string strSql = @" select t1.Id,om.Id as EquipId,om.EquipNumber,om.EquipName,om.EquipModel,t1.EquipCount - ISNULL(t2.EquipCount,0) as Count,t1.EquipDateTime,ISNULL(t2.EquipCount,0) as           EquipCount
//                        from (select Id,EquipID,equipCount,EquipDateTime,EquipFormID from OaEquipStock where EquipActionID = 2 and EquipFormID = " + id + @") t1  
//                        left join  (select Id,equipCount,EquipID,EquipDateTime,'" + id + @"' as useId from OaEquipStock where EquipActionID != 2 and EquipFormID = " + rid + @"  and EquipFormType = 'OaEquipReturn') t2
//                        on t1.EquipFormID = t2.useId and t1.EquipID = t2.EquipID  left join OaEquipment om on om.Id = t1.EquipID
//                        where ISNULL(t2.EquipCount,0) > 0
//                        order by t1.Id";
             return DAL.DBExecute.ExecuteDataTable(strSql.ToString());
        }
        public DataTable json(Common.SqlPageInfo condition,string EquipOrOffice)
        {
            string RowColumn = " os.*,ore.CreatorEmpId as CreatorEmpId1,f.FlowStatusID,FlowName,FlowStatusName,f.Id as Idd,f.FlowXml,ore.EquipLendNote,om.EquipName,om.EquipModel ";//,os.EquipCount";

            StringBuilder strSql = new StringBuilder().Append(@" select Count(1) from OaEquipStock os 
                                left join OaEquipReturn ore on os.EquipFormID = ore.Id
                                left join OaEquipment om on os.EquipID = om.Id 
                                left join Flow f on f.FlowRefID = ore.Id and f.FlowRefTable = 'OaequipReturn'
                                where ore.Id is not null and EquipFormType = 'OaEquipReturn'  
                                and ore.DeleterEmpId=0 and ore.EquipOrOffice = "+ EquipOrOffice + "  and EquipCount !=0 "); // order by id desc

            SqlParameter[] paras = {
                new SqlParameter("@TextCondtion",SqlDbType.VarChar),
                new SqlParameter("@CreatorDepId",SqlDbType.Int),
                new SqlParameter("@CreateEmpId",SqlDbType.Int)
            };
            if (condition.TextCondtion != null && condition.TextCondtion.ToString() != "")
            {
                strSql.Append(" and (ore.EquipLendNote like '%'+@TextCondtion+'%' or om.EquipName like '%'+@TextCondtion+'%' ) ");
                paras[0].Value = condition.TextCondtion;
            }

            if (condition.SelectCondtion != null && condition.SelectCondtion.Count > 0)
            {
                foreach (DictionaryEntry de in condition.SelectCondtion)
                {
                    switch (de.Key.ToString())
                    {
                        case "CreatorDepId":
                            if (de.Value.ToString() != "")
                            {
                                strSql.Append(" and CreatorDepId=@CreatorDepId ");
                                paras[1].Value = de.Value.ToString();
                            }
                            break;
                        case "CreateEmpId":
                            if (de.Value.ToString() != "")
                            {
                                strSql.Append(" and CreatorEmpId=@CreateEmpId ");
                                paras[2].Value = de.Value.ToString();
                            }
                            break;
                    }
                }
            }
            ////判断PageModel中查询条件是否为空
            //if (condition.PredicateValue != null && condition.PredicateValue.Length > 0 && condition.Predicate != "")
            //{
            //    string selectT = "";
            //    List<SqlParameter> _paramList = paras.ToList();
            //    condition.SetSqlPrams(_paramList, ref selectT);
            //    paras = _paramList.ToArray();
            //    strSql.Append(selectT);
            //}

            //---------------- 得到总记录数-------------------------//
            object obj = DBExecute.ExecuteScalar(DBExecute.ConnectionString, strSql.ToString(), paras);

            if (obj == null && obj == DBNull.Value) condition.PageTotleRowCount = 0;
            else condition.PageTotleRowCount = Convert.ToInt32(obj);
            //------------------------------------------------------//

            if (String.IsNullOrEmpty(condition.SelectOrder))
            {
                condition.SelectOrder = " os.Id desc";
            }

            string sql = Helper.SqlPage.ExecPageStrSql(condition, RowColumn, strSql);

            return DBExecute.ExecuteDataTable(DBExecute.ConnectionString, sql.ToString(), paras);


            //return DAL.DBExecute.ExecuteDataTable(strSql.ToString());
        }
        public string GetRID(int id)
        {
            string strSql = @" select id from OaEquipReturn where id = (select EquipFormID from OaEquipStock where ID= '"+id+"')";
            return DAL.DBExecute.ExecuteDataTable(strSql.ToString()).Rows[0][0].ToString();
        }
    }
}
