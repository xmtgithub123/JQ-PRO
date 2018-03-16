using System;
using System.Data;
using System.Data.SqlClient;
using DataModel.Models;

namespace DBSql
{
    public class EmpManageData
    {
        #region 人员策划

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagesize">每页容纳条数</param>
        /// <param name="pageindex">当前页数</param>
        /// <param name="keywords">项目经理名字</param>
        /// <param name="orderby">排序</param>
        /// <param name="recordcount">总记录数</param>
        /// <returns></returns>
        public DataTable GetList(int pagesize, int pageindex, string keywords, string orderby, out int recordcount,int empid)
        {
            DataTable dt = new DataTable();
            //string strWhere = " WHERE e.DeleterEmpId=0";
            string strWhere = "  ";

            if (!string.IsNullOrEmpty(keywords))
            {
                strWhere += string.Format(" AND p.ProjName like '%{0}%'", keywords);
            }

            string sqlStr = string.Format("SELECT p.ProjName EngineeringName,p1.ParentId,(CASE WHEN e.WDEmpId=" + empid + @" THEN 1 ELSE 0 END) IsWDEmp,e.* FROM [dbo].[EmpManage] e LEFT JOIN DesTaskGroup p ON e.EngineeringId = p.Id LEFT JOIN Project p1
                                            ON p.ProjId = p1.Id 
                                            WHERE e.DeleterEmpId=0 and (p.DeleterEmpId =0 or p.DeleterEmpId is null)  {0}", strWhere);
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
            string sqlStr = "SELECT * FROM [dbo].[EmpManage] WHERE DeleterEmpId=0 AND Id=@Id";
            SqlParameter p1 = new SqlParameter("@Id", Id);
            SqlHelper.FillDataTable(sqlStr, dt, p1);

            return dt;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns>自增长id</returns>
        public int Insert(EmpManage model)
        {
            string strSql = string.Empty;

            strSql = @"INSERT dbo.EmpManage ( EngineeringId ,EngineeringEmpId ,SafeEmpId ,CGEmpId ,JSEmpId ,WDEmpId ,XCEmpId ,CreationTime ,CreatorEmpId ,CreatorEmpName ,CreatorDepId ,CreatorDepName ,AgenCreatorEmpId ,AgenCreatorEmpName
      ,[EngineeringEmpName]
      ,[SafeEmpName]
      ,[CGEmpName]
      ,[JSEmpName]
      ,[WDEmpName]
      ,[XCEmpName]
      ,[LastModificationTime]
      ,[LastModifierEmpId]
      ,[LastModifierEmpName]
      ,[AgenLastModifierEmpId]
      ,[AgenLastModifierEmpName]
      ,[AgenDeleterEmpId]
      ,[AgenDeleterEmpName]
      ,[DeleterEmpId]
      ,[DeleterEmpName]
      ,[DeletionTime]
        ,[ProjName]
        ,[ProjNumber]
        ,[ProjPhase]
        ,[DesTaskGroupId]
        )
                       VALUES ( @EngineeringId ,@EngineeringEmpId ,@SafeEmpId ,@CGEmpId ,@JSEmpId ,@WDEmpId ,@XCEmpId ,GETDATE() ,@CreatorEmpId ,@CreatorEmpName ,@CreatorDepId ,@CreatorDepName ,@AgenCreatorEmpId ,@AgenCreatorEmpName
      , @EngineeringEmpName, @SafeEmpName, @CGEmpName, @JSEmpName, @WDEmpName, @XCEmpName,GETDATE(),0, '',0, '',0, '',0, '',GETDATE(),@ProjName,@ProjNumber,@ProjPhase,@DesTaskGroupId)
      SELECT @@IDENTITY AS Id";

            SqlParameter p1 = new SqlParameter("@EngineeringId", model.EngineeringId);
            SqlParameter p7 = new SqlParameter("@EngineeringEmpId", model.EngineeringEmpId);
            SqlParameter p2 = new SqlParameter("@SafeEmpId", model.SafeEmpId);
            SqlParameter p3 = new SqlParameter("@CGEmpId", model.CGEmpId);
            SqlParameter p4 = new SqlParameter("@JSEmpId", model.JSEmpId);
            SqlParameter p5 = new SqlParameter("@WDEmpId", model.WDEmpId);
            SqlParameter p6 = new SqlParameter("@XCEmpId", model.XCEmpId);
            SqlParameter p8 = new SqlParameter("@CreatorEmpId", model.CreatorEmpId);
            SqlParameter p9 = new SqlParameter("@CreatorEmpName", string.IsNullOrEmpty(model.CreatorEmpName) ? "" : model.CreatorEmpName);
            SqlParameter p10 = new SqlParameter("@CreatorDepId", model.CreatorDepId);
            SqlParameter p11 = new SqlParameter("@CreatorDepName", string.IsNullOrEmpty(model.CreatorDepName) ? "" : model.CreatorDepName);
            SqlParameter p12 = new SqlParameter("@AgenCreatorEmpId", model.AgenCreatorEmpId);
            SqlParameter p13 = new SqlParameter("@AgenCreatorEmpName", string.IsNullOrEmpty(model.AgenCreatorEmpName) ? "" : model.AgenCreatorEmpName);
            SqlParameter p14 = new SqlParameter("@EngineeringEmpName", model.EngineeringEmpName);
            SqlParameter p15 = new SqlParameter("@SafeEmpName", model.SafeEmpName);
            SqlParameter p16 = new SqlParameter("@CGEmpName", model.CGEmpName);
            SqlParameter p17 = new SqlParameter("@JSEmpName", model.JSEmpName);
            SqlParameter p18 = new SqlParameter("@WDEmpName", model.WDEmpName);
            SqlParameter p19 = new SqlParameter("@XCEmpName", model.XCEmpName);
            SqlParameter p20 = new SqlParameter("@ProjName", model.ProjName);
            SqlParameter p21 = new SqlParameter("@ProjNumber", model.ProjNumber);
            SqlParameter p22 = new SqlParameter("@ProjPhase", model.ProjPhase);
            SqlParameter p23 = new SqlParameter("@DesTaskGroupId", model.DesTaskGroupId);
            SqlParameter[] ps = { p1, p7, p2, p3, p4, p5, p6, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22,p23 };

            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, ps));
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns>受影响行数</returns>
        public int Update(EmpManage model)
        {
            string strSql = string.Empty;

            strSql = @"UPDATE dbo.EmpManage
                       SET [EngineeringId] = @EngineeringId
                          ,[EngineeringEmpId] = @EngineeringEmpId
                          ,[SafeEmpId] = @SafeEmpId
                          ,[CGEmpId] = @CGEmpId
                          ,[JSEmpId] = @JSEmpId
                          ,[WDEmpId] = @WDEmpId
                          ,[XCEmpId] = @XCEmpId
                          ,[LastModificationTime] = GETDATE()
                          ,[LastModifierEmpId] = @LastModifierEmpId
                          ,[LastModifierEmpName] = @LastModifierEmpName
                          ,[AgenLastModifierEmpId] = @AgenLastModifierEmpId
                          ,[AgenLastModifierEmpName] = @AgenLastModifierEmpName
                          ,EngineeringEmpName = @EngineeringEmpName
                          ,SafeEmpName = @SafeEmpName
                          ,CGEmpName = @CGEmpName
                          ,JSEmpName = @JSEmpName
                          ,WDEmpName = @WDEmpName
                          ,XCEmpName = @XCEmpName
                          ,ProjName = @ProjName
                          ,ProjNumber = @ProjNumber
                          ,ProjPhase = @ProjPhase
                          ,DesTaskGroupId = @DesTaskGroupId
                       WHERE Id = @Id";

            SqlParameter p1 = new SqlParameter("@Id", model.Id);
            SqlParameter p2 = new SqlParameter("@EngineeringId", model.EngineeringId);
            SqlParameter p3 = new SqlParameter("@EngineeringEmpId", model.EngineeringEmpId);
            SqlParameter p4 = new SqlParameter("@SafeEmpId", model.SafeEmpId);
            SqlParameter p5 = new SqlParameter("@CGEmpId", model.CGEmpId);
            SqlParameter p6 = new SqlParameter("@JSEmpId", model.JSEmpId);
            SqlParameter p7 = new SqlParameter("@WDEmpId", model.WDEmpId);
            SqlParameter p8 = new SqlParameter("@XCEmpId", model.XCEmpId);
            SqlParameter p9 = new SqlParameter("@LastModifierEmpId", model.LastModifierEmpId);
            SqlParameter p10 = new SqlParameter("@LastModifierEmpName", string.IsNullOrEmpty(model.LastModifierEmpName) ? "" : model.LastModifierEmpName);
            SqlParameter p11 = new SqlParameter("@AgenLastModifierEmpId", model.AgenLastModifierEmpId);
            SqlParameter p12 = new SqlParameter("@AgenLastModifierEmpName", string.IsNullOrEmpty(model.AgenLastModifierEmpName) ? "" : model.AgenLastModifierEmpName);
            SqlParameter p14 = new SqlParameter("@EngineeringEmpName", model.EngineeringEmpName);
            SqlParameter p15 = new SqlParameter("@SafeEmpName", model.SafeEmpName);
            SqlParameter p16 = new SqlParameter("@CGEmpName", model.CGEmpName);
            SqlParameter p17 = new SqlParameter("@JSEmpName", model.JSEmpName);
            SqlParameter p18 = new SqlParameter("@WDEmpName", model.WDEmpName);
            SqlParameter p19 = new SqlParameter("@XCEmpName", model.XCEmpName);
            SqlParameter p20 = new SqlParameter("@ProjName", model.ProjName);
            SqlParameter p21 = new SqlParameter("@ProjNumber", model.ProjNumber);
            SqlParameter p22 = new SqlParameter("@ProjPhase", model.ProjPhase);
            SqlParameter p23 = new SqlParameter("@DesTaskGroupId", model.DesTaskGroupId);

            SqlParameter[] ps = { p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p14, p15, p16, p17, p18, p19,p20,p21,p22,p23 };

            return SqlHelper.ExecuteNonQuery(strSql, ps);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">id集合</param>
        /// <returns>受影响行数</returns>
        public int Delete(int[] ids, int DeleterEmpId, string DeleterEmpName, int AgenDeleterEmpId, string AgenDeleterEmpName)
        {
            string strSql = string.Empty;
            string idStr = "0,";

            foreach (int i in ids)
            {
                idStr += (i.ToString() + ",");
            }

            strSql = string.Format(@"  UPDATE dbo.EmpManage
                                       SET [DeleterEmpId] = @DeleterEmpId
                                          ,[DeleterEmpName] = @DeleterEmpName
                                          ,[AgenDeleterEmpId] = @AgenDeleterEmpId
                                          ,[AgenDeleterEmpName] = @AgenDeleterEmpName
                                          ,[DeletionTime] = GETDATE()
                                       WHERE Id in ({0})", idStr.Trim(','));

            SqlParameter p2 = new SqlParameter("@DeleterEmpId", DeleterEmpId);
            SqlParameter p3 = new SqlParameter("@DeleterEmpName", string.IsNullOrEmpty(DeleterEmpName) ? "" : DeleterEmpName);
            SqlParameter p4 = new SqlParameter("@AgenDeleterEmpId", AgenDeleterEmpId);
            SqlParameter p5 = new SqlParameter("@AgenDeleterEmpName", string.IsNullOrEmpty(AgenDeleterEmpName) ? "" : AgenDeleterEmpName);
            SqlParameter[] ps = { p2, p3, p4, p5 };

            return SqlHelper.ExecuteNonQuery(strSql, ps);
        }

        #endregion

        #region 项目任务

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

        public DataTable GetProject(int Id)
        {
            DataTable dt = new DataTable();
            string sqlStr = "SELECT * FROM [dbo].[Project] WHERE Id=@Id";
            SqlParameter p1 = new SqlParameter("@Id", Id);
            SqlHelper.FillDataTable(sqlStr, dt, p1);

            return dt;
        }

        /// <summary>
        /// 获取工程列表
        /// </summary>
        /// <param name="pagesize">每页容纳条数</param>
        /// <param name="pageindex">当前页数</param>
        /// <param name="keywords">项目名称</param>
        /// <param name="orderby">排序</param>
        /// <param name="CurrentUserID">当前登录用户id</param>
        /// <param name="recordcount">总记录数</param>
        /// <returns></returns>
        public DataTable GetEngineeringList(int pagesize, int pageindex, string keywords, string orderby, int CurrentUserID, out int recordcount)
        {
            DataTable dt = new DataTable();
            string strWhere = string.Empty;

            if (!string.IsNullOrEmpty(keywords))
            {
                strWhere += string.Format(" AND d.ProjName like '%{0}%'", keywords);
            }

            string sqlStr = string.Format(@"SELECT e.Id,e.EngineeringId,e.ProjNumber,e.ProjName,e.ProjPhase,e.DesTaskGroupId,(CASE WHEN e.WDEmpId={1} THEN 1 ELSE 0 END) IsWDEmp,(CASE WHEN e.XCEmpId={1} THEN 1 ELSE 0 END) IsXCEmp,d.ProjId,p.ParentId
                                            FROM EmpManage e
                                            LEFT JOIN DesTaskGroup d
                                            ON e.EngineeringId = d.Id
                                            LEFT JOIN Project p
                                            ON d.ProjId = p.Id
                                            WHERE e.DeleterEmpId=0 and (d.DeleterEmpId=0 or d.DeleterEmpId is null) {0}", strWhere, CurrentUserID);
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

        #endregion

        #region 周报

        /// <summary>
        /// 获取周报列表
        /// </summary>
        /// <param name="pagesize">每页大小</param>
        /// <param name="pageindex">当前页</param>
        /// <param name="keywords">搜索关键字，标题</param>
        /// <param name="orderby">排序</param>
        /// <param name="taskgroupid">对应任务id</param>
        /// <param name="recordcount">总记录数</param>
        /// <returns>结果集</returns>
        public DataTable GetWeeklyList(int pagesize, int pageindex, string keywords, string orderby, int taskgroupid, out int recordcount)
        {
            DataTable dt = new DataTable();
            string strWhere = string.Empty;

            if (!string.IsNullOrEmpty(keywords))
            {
                strWhere += string.Format(" AND Title like '%{0}%'", keywords);
            }

            string sqlStr = string.Format(@"SELECT Id,TaskGroupId,Title,Detail,CONVERT(NVARCHAR(50),StartTime,23) StartTime,CONVERT(NVARCHAR(50),EndTime,23) EndTime,CreateEmpId,CreateEmpName,CONVERT(NVARCHAR(50),CreateTime,23) CreateTime
                                            FROM Weekly
                                            WHERE TaskGroupId={0} {1}", taskgroupid, strWhere);
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
        /// 获取周报
        /// </summary>
        /// <param name="Id">周报Id</param>
        /// <returns></returns>
        public DataTable GetWeekly(int Id)
        {
            DataTable dt = new DataTable();
            string sqlStr = "SELECT Id,TaskGroupId,Title,Detail,CONVERT(NVARCHAR(50),StartTime,23) StartTime,CONVERT(NVARCHAR(50),EndTime,23) EndTime,CreateEmpId,CreateEmpName,CONVERT(NVARCHAR(50),CreateTime,23) CreateTime FROM [dbo].[Weekly] WHERE Id=@Id";
            SqlParameter p1 = new SqlParameter("@Id", Id);
            SqlHelper.FillDataTable(sqlStr, dt, p1);

            return dt;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns>自增长id</returns>
        public int InsertWeekly(Weekly model)
        {
            string strSql = string.Empty;

            strSql = @"INSERT dbo.Weekly (TaskGroupId,Title,Detail,StartTime,EndTime,CreateEmpId,CreateEmpName,CreateTime)
                       VALUES (@TaskGroupId,@Title,@Detail,@StartTime,@EndTime,@CreateEmpId,@CreateEmpName,GETDATE())
                       SELECT @@IDENTITY AS Id";

            SqlParameter p1 = new SqlParameter("@TaskGroupId", model.TaskGroupId);
            SqlParameter p2 = new SqlParameter("@Title", model.Title);
            SqlParameter p3 = new SqlParameter("@Detail", model.Detail);
            SqlParameter p4 = new SqlParameter("@StartTime", model.StartTime);
            SqlParameter p5 = new SqlParameter("@EndTime", model.EndTime);
            SqlParameter p6 = new SqlParameter("@CreateEmpId", model.CreateEmpId);
            SqlParameter p7 = new SqlParameter("@CreateEmpName", model.CreateEmpName);
            SqlParameter[] ps = { p1, p2, p3, p4, p5, p6, p7 };

            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, ps));
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns>受影响行数</returns>
        public int UpdateWeekly(Weekly model)
        {
            string strSql = string.Empty;

            strSql = @"UPDATE dbo.Weekly
                       SET [TaskGroupId] = @TaskGroupId
                          ,[Title] = @Title
                          ,[Detail] = @Detail
                          ,[StartTime] = @StartTime
                          ,[EndTime] = @EndTime
                       WHERE Id = @Id";

            SqlParameter p1 = new SqlParameter("@TaskGroupId", model.TaskGroupId);
            SqlParameter p2 = new SqlParameter("@Title", model.Title);
            SqlParameter p3 = new SqlParameter("@Detail", model.Detail);
            SqlParameter p4 = new SqlParameter("@StartTime", model.StartTime);
            SqlParameter p5 = new SqlParameter("@EndTime", model.EndTime);
            SqlParameter p6 = new SqlParameter("@Id", model.Id);
            SqlParameter[] ps = { p1, p2, p3, p4, p5, p6 };

            return SqlHelper.ExecuteNonQuery(strSql, ps);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">id集合</param>
        /// <returns>受影响行数</returns>
        public int DeleteWeekly(int[] ids)
        {
            string strSql = string.Empty;
            string idStr = "0,";

            foreach (int i in ids)
            {
                idStr += (i.ToString() + ",");
            }

            strSql = string.Format("DELETE FROM Weekly WHERE Id in ({0})", idStr.Trim(','));

            return SqlHelper.ExecuteNonQuery(strSql, null);
        }

        #endregion
    }
}
