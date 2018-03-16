using DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSql.Bussiness
{
    public class HZShow
    {
        public DataTable GetHZShow(Common.SqlPageInfo queryContext)
        {
            string RowColumn = @"a.Id,a.ProjNumber,a.ProjName,ProjPhaseInfo,ISNULL(d.BaseName,'未签') as ConStateName,ISNULL(d.BaseId,291) as ConStateId,
                    case ISNULL(b.ID,0) when 0 then '否' else '是' end as IsExistsContract,
                    case c.ConIsFeeFinished when 0 then '否' WHEN 1 THEN '是' else '' end as IsFinish,
                    isnull((SELECT  BaseName + ','FROM BaseData  WHERE  CHARINDEX(','+CAST(BaseID AS  nvarchar(5))+',',','+a.ProjPhaseIds+',')>0 FOR XML PATH('')),'') as PhaseNames,
                    (CASE a.CompanyID WHEN 0 THEN '分公司' WHEN 1 THEN '设计公司' WHEN 2 THEN '工程公司' WHEN 3 THEN '创景公司' END) CompanyID";

            StringBuilder strSql = new StringBuilder(@"select Count(1)
                    from Project a
                    left join BusProjContractRelation b on a.Id=b.ProjID
                    left join BussContract c on c.Id=b.ConID
                    left join BaseData d on c.ConStatus=d.BaseID
                    where a.ParentId<>0");

            SqlParameter[] paras = {
                new SqlParameter("@TextCondtion",SqlDbType.VarChar),
                new SqlParameter("@ConStatus",SqlDbType.Int),
                new SqlParameter("@CompanyID",SqlDbType.Int),
                new SqlParameter("@IsFinish",SqlDbType.Int),
            };


            if (!string.IsNullOrEmpty(queryContext.TextCondtion))
            {
                strSql.Append(" and (a.ProjNumber like '%'+@TextCondtion+'%' or a.ProjName like '%'+@TextCondtion+'%') ");
                paras[0].Value = queryContext.TextCondtion;
            }

            if (queryContext.SelectCondtion != null && queryContext.SelectCondtion.Count > 0)
            {
                foreach (DictionaryEntry de in queryContext.SelectCondtion)
                {
                    if (de.Value == null || de.Value.ToString().Trim() == "") continue;

                    switch (de.Key.ToString())
                    {
                        case "ConStatus":
                            strSql.Append(" and ISNULL(d.BaseId,291)=@ConStatus ");
                            paras[1].Value = de.Value.ToString();
                            break;
                        case "CompanyID":
                            strSql.Append(" and a.CompanyID=@CompanyID ");//项目负责人
                            paras[2].Value = de.Value.ToString();
                            break;
                        case "IsFinish":
                            strSql.Append(" and c.ConIsFeeFinished=@ConIsFeeFinished ");//项目负责人
                            paras[3].Value = de.Value.ToString();
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
                queryContext.SelectOrder = "a.Id desc";
            }
            string sql = Helper.SqlPage.ExecPageStrSql(queryContext, RowColumn, strSql);
            return DBExecute.ExecuteDataTable(DBExecute.ConnectionString, sql.ToString(), paras);
        }

        /// <summary>获取开票信息列表</summary>
        public DataTable GetInvoiceList(int pagesize, int pageindex, string keywords, string orderby, out int recordcount)
        {
            DataTable dt = new DataTable();
            string strWhere = keywords;

            string sqlStr = string.Format(@"SELECT f.Id,(CASE WHEN CHARINDEX('Proj',f.Id)>0 THEN c.ConNumber ELSE co.ConNumber END) ConNumber,(CASE WHEN CHARINDEX('Proj',f.Id)>0 THEN c.ConName ELSE co.ConrName END) ConName,ISNULL(p.ProjName,'') ProjName,f.InvoiceDate,f.InvoiceMoney,f.InvoiceNote,f.CreatorEmpName,(CASE WHEN CHARINDEX('Proj',f.Id)>0 THEN '项目合同' ELSE '其它合同' END) ConType
                                            FROM (
                                            SELECT 'Proj'+CONVERT(NVARCHAR(50),Id) Id,ProjId,ConID,FlowID,DeleterEmpId,InvoiceDate,InvoiceMoney,InvoiceNote,CreatorEmpName FROM BussFeeInvoice
                                            UNION ALL
                                            SELECT 'Other'+CONVERT(NVARCHAR(50),Id) Id,0 ProjId,RefID ConID,FlowID,DeleterEmpId,InvoiceDate,InvoiceMoney,InvoiceNote,CreatorEmpName FROM BussContractOtherInvoice WHERE ConOtherTypeID=0
                                            ) f
                                            LEFT JOIN Project p ON f.ProjID = p.Id
                                            LEFT JOIN BussContract c ON f.ConID = c.Id
                                            LEFT JOIN BussContractOther co ON f.ConID = co.Id
                                            WHERE f.DeleterEmpId=0 AND f.FlowID>=0 {0}", strWhere);
            pagesize = (pagesize < 1 ? 1 : pagesize);
            pageindex = (pageindex < 1 ? 1 : pageindex);

            SqlParameter p1 = new SqlParameter("@Source", sqlStr);
            SqlParameter p2 = new SqlParameter("@PageSize", pagesize);
            SqlParameter p3 = new SqlParameter("@CurrentPage", pageindex);
            SqlParameter p4 = new SqlParameter("@Sort", orderby.Replace("ORDER BY", ""));
            SqlParameter p5 = new SqlParameter("@RecordCount", SqlDbType.Int);
            p5.Direction = ParameterDirection.Output;
            SqlParameter[] ps = { p1, p2, p3, p4, p5 };

            dt = SqlHelper.ExecuteStoredProcedure2("pro_sys_DataPage", ps);
            recordcount = Convert.ToInt32(ps[4].Value);

            return dt;
        }

        /// <summary>获取收款信息列表</summary>
        public DataTable GetFactList(int pagesize, int pageindex, string keywords, string orderby, out int recordcount)
        {
            DataTable dt = new DataTable();
            string strWhere = keywords;

            string sqlStr = string.Format(@"SELECT f.Id,(CASE WHEN CHARINDEX('Proj',f.Id)>0 THEN c.ConNumber ELSE co.ConNumber END) ConNumber,(CASE WHEN CHARINDEX('Proj',f.Id)>0 THEN c.ConName ELSE co.ConrName END) ConName,ISNULL(p.ProjName,'') ProjName,CONVERT(NVARCHAR(50),f.FeeDate,23) FeeDate,f.FeeMoney,f.FeeNote,f.CreatorEmpName,(CASE WHEN CHARINDEX('Proj',f.Id)>0 THEN '项目合同' ELSE '其它合同' END) ConType
                                            FROM (
                                            SELECT 'Proj'+CONVERT(NVARCHAR(50),Id) Id,ProjID,ConID,FlowID,DeleterEmpId,FeeDate,FeeMoney,FeeNote,CreatorEmpName FROM BussFeeFact
                                            UNION ALL
                                            SELECT 'Other'+CONVERT(NVARCHAR(50),Id) Id,0 ProjID,RefID ConID,FlowID,DeleterEmpId,FeerDate FeeDate,FeeMoney,FeeNote,CreatorEmpName FROM BussContractOtherFeeFact WHERE FactTypeID=0
                                            ) f
                                            LEFT JOIN Project p ON f.ProjID = p.Id
                                            LEFT JOIN BussContract c ON f.ConID = c.Id
                                            LEFT JOIN BussContractOther co ON f.ConID = co.Id
                                            WHERE f.DeleterEmpId=0 AND f.FlowID>=0 {0}", strWhere);
            pagesize = (pagesize < 1 ? 1 : pagesize);
            pageindex = (pageindex < 1 ? 1 : pageindex);

            SqlParameter p1 = new SqlParameter("@Source", sqlStr);
            SqlParameter p2 = new SqlParameter("@PageSize", pagesize);
            SqlParameter p3 = new SqlParameter("@CurrentPage", pageindex);
            SqlParameter p4 = new SqlParameter("@Sort", orderby.Replace("ORDER BY", ""));
            SqlParameter p5 = new SqlParameter("@RecordCount", SqlDbType.Int);
            p5.Direction = ParameterDirection.Output;
            SqlParameter[] ps = { p1, p2, p3, p4, p5 };

            dt = SqlHelper.ExecuteStoredProcedure2("pro_sys_DataPage", ps);
            recordcount = Convert.ToInt32(ps[4].Value);

            return dt;
        }

        /// <summary>获取付款信息列表</summary>
        public DataTable GetSubFactList(int pagesize, int pageindex, string keywords, string orderby, out int recordcount)
        {
            DataTable dt = new DataTable();
            string strWhere = keywords;

            string sqlStr = string.Format(@"SELECT f.Id,(CASE WHEN CHARINDEX('Proj',f.Id)>0 THEN c.ConSubNumber ELSE co.ConNumber END) ConNumber,(CASE WHEN CHARINDEX('Proj',f.Id)>0 THEN c.ConSubName ELSE co.ConrName END) ConName,ISNULL(p.SubName,'') ProjName,CONVERT(NVARCHAR(50),f.SubFeeFactDate,23) SubFeeFactDate,f.SubFeeFactMoney,f.SubFeeFactNote,f.CreatorEmpName,(CASE WHEN CHARINDEX('Proj',f.Id)>0 THEN '项目合同' ELSE '其它合同' END) ConType
                                            FROM (
                                            SELECT 'Proj'+CONVERT(NVARCHAR(50),Id) Id,ProjSubID ProjID,ConSubID ConID,FlowID,DeleterEmpId,SubFeeFactDate,SubFeeFactMoney,SubFeeFactNote,CreatorEmpName FROM BussSubFeeFact
                                            UNION ALL
                                            SELECT 'Other'+CONVERT(NVARCHAR(50),Id) Id,0 ProjID,RefID ConID,FlowID,DeleterEmpId,FeerDate FeeDate,FeeMoney,FeeNote,CreatorEmpName FROM BussContractOtherFeeFact WHERE FactTypeID=1
                                            ) f
                                            LEFT JOIN ProjSub p ON f.ProjID = p.Id
                                            LEFT JOIN BussContractSub c ON f.ConID = c.Id
                                            LEFT JOIN BussContractOther co ON f.ConID = co.Id
                                            WHERE f.DeleterEmpId=0 AND f.FlowID>=0 {0}", strWhere);
            pagesize = (pagesize < 1 ? 1 : pagesize);
            pageindex = (pageindex < 1 ? 1 : pageindex);

            SqlParameter p1 = new SqlParameter("@Source", sqlStr);
            SqlParameter p2 = new SqlParameter("@PageSize", pagesize);
            SqlParameter p3 = new SqlParameter("@CurrentPage", pageindex);
            SqlParameter p4 = new SqlParameter("@Sort", orderby.Replace("ORDER BY", ""));
            SqlParameter p5 = new SqlParameter("@RecordCount", SqlDbType.Int);
            p5.Direction = ParameterDirection.Output;
            SqlParameter[] ps = { p1, p2, p3, p4, p5 };

            dt = SqlHelper.ExecuteStoredProcedure2("pro_sys_DataPage", ps);
            recordcount = Convert.ToInt32(ps[4].Value);

            return dt;
        }
    }
}
