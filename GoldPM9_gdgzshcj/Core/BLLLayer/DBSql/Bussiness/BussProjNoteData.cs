using System;
using System.Data;
using System.Data.SqlClient;
using DataModel.Models;

namespace DBSql
{
    public class BussProjNoteData
    {
        /// <summary>
        /// 获取记事列表
        /// </summary>
        /// <param name="pagesize">每页大小</param>
        /// <param name="pageindex">当前页</param>
        /// <param name="keywords">搜索关键字，标题</param>
        /// <param name="orderby">排序</param>
        /// <param name="BussProjInfoRecordsId">对应项目备案id</param>
        /// <param name="recordcount">总记录数</param>
        /// <returns>结果集</returns>
        public DataTable GetNoteList(int pagesize, int pageindex, string keywords, string orderby, int BussProjInfoRecordsId, out int recordcount)
        {
            DataTable dt = new DataTable();
            string strWhere = string.Empty;

            if (!string.IsNullOrEmpty(keywords))
            {
                strWhere += string.Format(" AND Title like '%{0}%'", keywords);
            }

            string sqlStr = string.Format(@"SELECT [Id],[BussProjInfoRecordsId],[Title],[Detail],CONVERT(NVARCHAR(50),[Time],20) [Time],[BaseDataId],[BaseName],[CreateId],[CreateName],CONVERT(NVARCHAR(50),[CreateTime],20) [CreateTime]
                                            FROM ProjIRNote
                                            LEFT JOIN BaseData
                                            ON BaseID = BaseDataId
                                            WHERE BussProjInfoRecordsId={0} {1}", BussProjInfoRecordsId, strWhere);
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
        /// 获取记事
        /// </summary>
        /// <param name="Id">记事Id</param>
        /// <returns></returns>
        public DataTable GetNote(int Id)
        {
            DataTable dt = new DataTable();
            string sqlStr = "SELECT [Id],[BussProjInfoRecordsId],[Title],[Detail],CONVERT(NVARCHAR(50),[Time],20) [Time],[BaseDataId],[CreateId],[CreateName],CONVERT(NVARCHAR(50),[CreateTime],20) [CreateTime] FROM [dbo].[ProjIRNote] WHERE Id=@Id";
            SqlParameter p1 = new SqlParameter("@Id", Id);
            SqlHelper.FillDataTable(sqlStr, dt, p1);

            return dt;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns>自增长id</returns>
        public int InsertNote(ProjIRNote model)
        {
            string strSql = string.Empty;

            strSql = @"INSERT dbo.ProjIRNote (BussProjInfoRecordsId,Title,Detail,[Time],BaseDataId,CreateId,CreateName,CreateTime)
                       VALUES (@BussProjInfoRecordsId,@Title,@Detail,@Time,@BaseDataId,@CreateId,@CreateName,GETDATE())
                       SELECT @@IDENTITY AS Id";

            SqlParameter p1 = new SqlParameter("@BussProjInfoRecordsId", model.BussProjInfoRecordsId);
            SqlParameter p2 = new SqlParameter("@Title", model.Title);
            SqlParameter p3 = new SqlParameter("@Detail", model.Detail);
            SqlParameter p4 = new SqlParameter("@Time", model.Time);
            SqlParameter p5 = new SqlParameter("@BaseDataId", model.BaseDataId);
            SqlParameter p6 = new SqlParameter("@CreateId", model.CreateId);
            SqlParameter p7 = new SqlParameter("@CreateName", model.CreateName);
            SqlParameter[] ps = { p1, p2, p3, p4, p5, p6, p7 };

            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, ps));
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns>受影响行数</returns>
        public int UpdateNote(ProjIRNote model)
        {
            string strSql = string.Empty;

            strSql = @"UPDATE dbo.ProjIRNote
                       SET [BussProjInfoRecordsId] = @BussProjInfoRecordsId
                          ,[Title] = @Title
                          ,[Detail] = @Detail
                          ,[Time] = @Time
                          ,[BaseDataId] = @BaseDataId
                       WHERE Id = @Id";

            SqlParameter p1 = new SqlParameter("@BussProjInfoRecordsId", model.BussProjInfoRecordsId);
            SqlParameter p2 = new SqlParameter("@Title", model.Title);
            SqlParameter p3 = new SqlParameter("@Detail", model.Detail);
            SqlParameter p4 = new SqlParameter("@Time", model.Time);
            SqlParameter p5 = new SqlParameter("@BaseDataId", model.BaseDataId);
            SqlParameter p6 = new SqlParameter("@Id", model.Id);
            SqlParameter[] ps = { p1, p2, p3, p4, p5, p6 };

            return SqlHelper.ExecuteNonQuery(strSql, ps);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">id集合</param>
        /// <returns>受影响行数</returns>
        public int DeleteNote(int[] ids)
        {
            string strSql = string.Empty;
            string idStr = "0,";

            foreach (int i in ids)
            {
                idStr += (i.ToString() + ",");
            }

            strSql = string.Format("DELETE FROM ProjIRNote WHERE Id in ({0})", idStr.Trim(','));

            return SqlHelper.ExecuteNonQuery(strSql, null);
        }
    }
}
