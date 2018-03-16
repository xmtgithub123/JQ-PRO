using System;
using System.Data;
using System.Data.SqlClient;

namespace DBSql
{
    public class SSManageData
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagesize">每页容纳条数</param>
        /// <param name="pageindex">当前页数</param>
        /// <param name="keywords">项目名称</param>
        /// <param name="orderby">排序</param>
        /// <param name="recordcount">总记录数</param>
        /// <returns></returns>
        public DataTable GetList(int pagesize, int pageindex, string keywords, string orderby, out int recordcount)
        {
            DataTable dt = new DataTable();
            string strWhere = " WHERE 1=1";

            if (!string.IsNullOrEmpty(keywords))
            {
                strWhere += string.Format(" AND p.ProjName like '%{0}%'", keywords);
            }

            string sqlStr = string.Format(@"SELECT em.ProjPhase,em.ProjName,p.ProjName EngineeringName,e.Id,e.EngineeringId,(CASE WHEN e.IsSSFA > 0 THEN '是' ELSE '否' END) IsSSFA
                                            ,(CASE WHEN e.IsNS > 0 THEN '是' ELSE '否' END) IsNS
                                            ,(CASE WHEN e.IsWS > 0 THEN '是' ELSE '否' END) IsWS
                                            ,e.Address,e.Level,e.TechPlan,e.TechNum,e.CreatorEmpId,e.CreatorEmpName,e.CreatorTime
                                            ,f.Id FlowID,f.FlowName,f.FlowStatusID,f.FlowStatusName,ISNULL(f.FlowXml.value('(/Root/TurnedEmpIDs)[1]','nvarchar(max)'),'') FlowTurnedEmpIDs
                                            FROM [dbo].[SSManage] e 
                                            left join EmpManage em on e.EmpManageId = em.Id
                                            LEFT JOIN DesTaskGroup p ON e.EngineeringID = p.Id
                                            LEFT JOIN Flow f ON f.FlowRefID = e.Id AND f.FlowRefTable='SSManage' {0}", strWhere);
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

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>结果集</returns>
        public DataTable Get(int Id)
        {
            DataTable dt = new DataTable();
            string sqlStr = "SELECT * FROM [dbo].[SSManage] WHERE Id=@Id";
            SqlParameter p1 = new SqlParameter("@Id", Id);
            SqlHelper.FillDataTable(sqlStr, dt, p1);

            return dt;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns>自增长id</returns>
        public int Insert(DataModel.Models.SSManage model)
        {
            string strSql = string.Empty;

            strSql = @"INSERT dbo.SSManage ( EngineeringID,IsSSFA,IsNS,IsWS,Address,Level,TechPlan,TechNum,CreatorEmpId,CreatorEmpName,CreatorTime,EmpManageId )
                       VALUES ( @EngineeringID,@IsSSFA,@IsNS,@IsWS,@Address,@Level,@TechPlan,@TechNum,@CreatorEmpId,@CreatorEmpName,GETDATE(),@EmpManageId)
                       SELECT @@IDENTITY AS Id";

            SqlParameter p1 = new SqlParameter("@EngineeringID", model.EngineeringID);
            SqlParameter p2 = new SqlParameter("@IsSSFA", model.IsSSFA);
            SqlParameter p3 = new SqlParameter("@IsNS", model.IsNS);
            SqlParameter p4 = new SqlParameter("@IsWS", model.IsWS);
            SqlParameter p5 = new SqlParameter("@Address", model.Address);
            SqlParameter p6 = new SqlParameter("@Level", model.Level);
            SqlParameter p7 = new SqlParameter("@TechPlan", model.TechPlan);
            SqlParameter p8 = new SqlParameter("@TechNum", model.TechNum);
            SqlParameter p9 = new SqlParameter("@CreatorEmpId", model.CreatorEmpId);
            SqlParameter p10 = new SqlParameter("@CreatorEmpName", model.CreatorEmpName);
            SqlParameter p11 = new SqlParameter("@EmpManageId", model.EmpManageId);
            SqlParameter[] ps = { p1, p2, p3, p4, p5, p6, p7, p8, p9, p10,p11 };

            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, ps));
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns>受影响行数</returns>
        public int Update(DataModel.Models.SSManage model)
        {
            string strSql = string.Empty;

            strSql = @"UPDATE dbo.SSManage
                       SET [EngineeringID] = @EngineeringID
                          ,[IsSSFA] = @IsSSFA
                          ,[IsNS] = @IsNS
                          ,[IsWS] = @IsWS
                          ,[Address] = @Address
                          ,[Level] = @Level
                          ,[TechPlan] = @TechPlan
                          ,[TechNum] = @TechNum
                          ,[EmpManageId] = @EmpManageId
                       WHERE Id = @Id";

            SqlParameter p1 = new SqlParameter("@EngineeringID", model.EngineeringID);
            SqlParameter p2 = new SqlParameter("@IsSSFA", model.IsSSFA);
            SqlParameter p3 = new SqlParameter("@IsNS", model.IsNS);
            SqlParameter p4 = new SqlParameter("@IsWS", model.IsWS);
            SqlParameter p5 = new SqlParameter("@Address", model.Address);
            SqlParameter p6 = new SqlParameter("@Level", model.Level);
            SqlParameter p7 = new SqlParameter("@Id", model.Id);
            SqlParameter p8 = new SqlParameter("@TechPlan", model.TechPlan);
            SqlParameter p9 = new SqlParameter("@TechNum", model.TechNum);
            SqlParameter p10 = new SqlParameter("@EmpManageId", model.EmpManageId);
            SqlParameter[] ps = { p1, p2, p3, p4, p5, p6, p7, p8, p9, p10 };

            return SqlHelper.ExecuteNonQuery(strSql, ps);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">id集合</param>
        /// <returns>受影响行数</returns>
        public int Delete(int[] ids)
        {
            string idStr = "0,";

            foreach (int i in ids)
            {
                idStr += (i.ToString() + ",");
            }

            string strSql = string.Format("DELETE FROM dbo.SSManage WHERE Id in ({0})", idStr.Trim(','));

            return SqlHelper.ExecuteNonQuery(strSql, null);
        }

        /// <summary>
        /// 查询项目
        /// </summary>
        /// <param name="Id">项目Id</param>
        /// <returns>结果集</returns>
        public DataTable GetProj(int Id)
        {
            DataTable dt = new DataTable();
            string sqlStr = "SELECT * FROM [dbo].[DesTaskGroup] WHERE Id=@Id";
            SqlParameter p1 = new SqlParameter("@Id", Id);
            SqlHelper.FillDataTable(sqlStr, dt, p1);

            return dt;
        }
    }
}
