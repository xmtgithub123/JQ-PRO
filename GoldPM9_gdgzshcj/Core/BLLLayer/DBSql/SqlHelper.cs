using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;

namespace DBSql
{
    /// <summary>
    /// 数据连接助手类
    /// </summary>
    public static class SqlHelper
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public static readonly string CONN_STRING = System.Configuration.ConfigurationManager.ConnectionStrings["jqpm7Context"].ConnectionString;

        // 用户缓存参数。
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());
        /// <summary>
        /// 使用默认连接， 执行SQL语句，仅仅返回数据库受影响行数。
        /// 所需参数：命令文本，参数列表。
        /// </summary>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                PrepareCommand(cmd, conn, null, CommandType.Text, cmdText, cmdParms);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 执行一个sql命令，仅仅返回数据库受影响行数。
        /// 所需参数：连接字符串，命令类型，命令文本，参数列表。
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns>数据库受影响行数</returns>
        public static int ExecuteNonQuery(string connString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 执行一个sql命令，仅仅返回数据库受影响行数。
        /// 所需参数：连接对象，命令类型，命令文本，参数列表。
        /// </summary>
        /// <param name="conn">连接对象</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns>数据库受影响行数</returns>
        public static int ExecuteNonQuery(SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 执行一个sql命令，仅仅返回数据库受影响行数。(用于需要事务的情况)
        /// 所需参数：事务对象，命令类型，命令文本，参数列表。
        /// </summary>
        /// <param name="trans">事务对象</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns>数据库受影响行数</returns>
        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 执行一个sql查询语句，返回DataReader对象。使用默认连接。
        /// 所需参数：命令文本，参数列表。
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(CONN_STRING);

            // 此处使用try/catch的原因：当出现异常时，也可以保证能关闭连接。
            try
            {
                PrepareCommand(cmd, conn, null, CommandType.Text, cmdText, cmdParms);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// 执行一个sql查询命令，返回DataReader对象。
        /// 所需参数：连接字符串，命令类型，命令文本，参数列表。
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns>查询的结果 DataReader对象</returns>
        public static SqlDataReader ExecuteReader(string connString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connString);

            // 此处使用try/catch的原因：当出现异常时，也可以保证能关闭连接。
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }
        /// <summary>
        /// 执行一个sql查询命令，返回DataReader对象。
        /// 所需参数：连接对象，命令类型，命令文本，参数列表。
        /// </summary>
        /// <param name="conn">连接对象</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
            SqlDataReader rdr = cmd.ExecuteReader();
            cmd.Parameters.Clear();
            return rdr;
        }

        /// <summary>
        /// 执行一个sql查询语句，返回查询结果的第一行第一列的值。使用默认连接
        /// 所需参数：命令文本，参数列表。
        /// </summary>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns></returns>
        public static object ExecuteScalar(string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                PrepareCommand(cmd, conn, null, CommandType.Text, cmdText, cmdParms);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }


        /// <summary>
        /// 执行一个sql查询命令，返回查询结果的第一行第一列的值。
        /// 所需参数：连接字符串，命令类型，命令文本，参数列表。
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns>查询结果的第一行第一列的值</returns>
        public static object ExecuteScalar(string connString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }
        /// <summary>
        /// 执行一个sql查询命令，返回查询结果的第一行第一列的值。
        /// 所需参数：连接对象，命令类型，命令文本，参数列表。
        /// </summary>
        /// <param name="conn">连接对象</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns>查询结果的第一行第一列的值</returns>
        public static object ExecuteScalar(SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;

        }
        /// <summary>
        /// 执行一个sql查询命令，返回查询结果的第一行第一列的值, 支持事务。
        /// 所需参数：事务对象，命令类型，命令文本，参数列表。
        /// </summary>
        /// <param name="tran">事务对象</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns>查询结果的第一行第一列的值</returns>
        public static object ExecuteScalar(SqlTransaction tran, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, tran.Connection, tran, cmdType, cmdText, cmdParms);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;

        }

        /// <summary>
        /// 执行一个SQL查询，填充DataTable
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="dt"></param>
        /// <param name="cmdParms"></param>
        public static void FillDataTable(string cmdText, DataTable dt, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                PrepareCommand(cmd, conn, null, CommandType.Text, cmdText, cmdParms);
                SqlDataAdapter ada = new SqlDataAdapter(cmd);
                ada.Fill(dt);
                ada.Dispose();
            }
        }

        /// <summary>
        /// 执行一个存储过程，填充DataTable
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="dt"></param>
        /// <param name="cmdParms"></param>
        public static void FillDataSet(SqlCommand cmd, DataSet ds)
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                PrepareCommand(cmd, conn, null, CommandType.StoredProcedure, null, null);
                SqlDataAdapter ada = new SqlDataAdapter();
                ada.SelectCommand = cmd;
                ada.Fill(ds);
                ada.Dispose();
            }
        }

        /// <summary>
        /// 执行一个存储过程，填充DataTable
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="dt"></param>
        /// <param name="cmdParms"></param>
        public static void FillDataTable(SqlCommand cmd, DataTable dt)
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                PrepareCommand(cmd, conn, null, CommandType.StoredProcedure, null, null);
                SqlDataAdapter ada = new SqlDataAdapter();
                ada.SelectCommand = cmd;
                ada.Fill(dt);
                ada.Dispose();
            }
        }

        /// <summary>
        /// 缓存一个参数数组。
        /// </summary>
        /// <param name="cacheKey">参数数组在缓存中的名称</param>
        /// <param name="cmdParms">参数数组</param>
        public static void SetCacheParameters(string cacheKey, params SqlParameter[] cmdParms)
        {
            parmCache[cacheKey] = cmdParms;
        }

        /// <summary>
        /// 从缓存中获取一个参数数组。
        /// </summary>
        /// <param name="cacheKey">参数数组在缓存中的名称</param>
        /// <returns>参数数组的副本</returns>
        public static SqlParameter[] GetCachedParameters(string cacheKey)
        {
            SqlParameter[] cachedParms = (SqlParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            //克隆一个参数数组的原因：此处的操作会对获取的参数进行赋值操作。
            SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];

            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }

        /// <summary>
        /// 准备一个可以执行的Sql命令对象。
        /// </summary>
        /// <param name="cmd">命令对象</param>
        /// <param name="conn">连接对象</param>
        /// <param name="trans">事务对象</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdParms">参数列表</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;

            if (cmdText != null)
                cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;


            if (cmdParms != null)
            {
                SqlParameter[] clonedParameter = new SqlParameter[cmdParms.Length];
                for (int i = 0; i < cmdParms.Length; i++)
                {
                    clonedParameter[i] = (SqlParameter)((ICloneable)cmdParms[i]).Clone();
                }

                foreach (SqlParameter parm in clonedParameter)
                    cmd.Parameters.Add(parm);
            }

            // cmdParms.Clone();
        }

        /// <summary>
        /// 执行存储过程，返回DataSet
        /// </summary>
        /// <param name="i"></param>
        /// <param name="spName"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static DataSet ExecuteStoredProcedure(string spName, params SqlParameter[] cmdParms)
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                SqlCommand cmd = new SqlCommand(spName, conn);
                CommandType cmdType = CommandType.StoredProcedure;
                PrepareCommand(cmd, conn, null, cmdType, null, cmdParms);
                DataSet ds = new DataSet();
                FillDataSet(cmd, ds);
                // 输出参数
                cmd.Parameters.CopyTo(cmdParms, 0);
                return ds;
            }
        }

        /// <summary>
        /// 执行存储过程，返回DataTable
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="spName"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static DataTable ExecuteStoredProcedure2(string spName, params SqlParameter[] cmdParms)
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                SqlCommand cmd = new SqlCommand(spName, conn);
                CommandType cmdType = CommandType.StoredProcedure;
                PrepareCommand(cmd, conn, null, cmdType, null, cmdParms);
                DataTable dt = new DataTable();
                FillDataTable(cmd, dt);
                // 输出参数
                cmd.Parameters.CopyTo(cmdParms, 0);
                return dt;
            }
        }
        public static int ExecuteStoredProcedure3(string spName, params SqlParameter[] cmdParms)
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                SqlCommand cmd = new SqlCommand(spName, conn);
                CommandType cmdType = CommandType.StoredProcedure;
                PrepareCommand(cmd, conn, null, cmdType, null, cmdParms);
                // DataTable dt = new DataTable();
                //FillDataTable(cmd, dt);
                cmd.ExecuteNonQuery();
                // 输出参数
                cmd.Parameters.CopyTo(cmdParms, 0);
                int error_info = (int)cmd.Parameters["@RETURN_VALUE"].Value;
                cmd.Dispose();
                return error_info;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static object[] ExecuteStoredProcedure4(string spName, params SqlParameter[] cmdParms)
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                SqlCommand cmd = new SqlCommand(spName, conn);
                CommandType cmdType = CommandType.StoredProcedure;
                PrepareCommand(cmd, conn, null, cmdType, null, cmdParms);
                // DataTable dt = new DataTable();
                //FillDataTable(cmd, dt);
                cmd.ExecuteNonQuery();
                // 输出参数
                cmd.Parameters.CopyTo(cmdParms, 0);
                object[] error_info = { (int)cmd.Parameters["@RETURN_VALUE"].Value, (string)cmd.Parameters["@dbsession"].Value };
                cmd.Dispose();
                return error_info;
            }
        }

        public static ArrayList ExecuteStoredProcedure5(string spName, params SqlParameter[] cmdParms)
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                SqlCommand cmd = new SqlCommand(spName, conn);
                CommandType cmdType = CommandType.StoredProcedure;
                PrepareCommand(cmd, conn, null, cmdType, null, cmdParms);
                // DataTable dt = new DataTable();
                //FillDataTable(cmd, dt);
                cmd.ExecuteNonQuery();
                // 输出参数
                cmd.Parameters.CopyTo(cmdParms, 0);
                ArrayList al = new ArrayList();
                for (int j = 0; j < cmd.Parameters.Count; j++)
                {
                    al.Add(cmd.Parameters[j].Value);
                }
                cmd.Dispose();
                return al;
            }
        }

        public static DataTable GetDataTableByPage(string sql, string sort, int start, int limit, out int total)
        {

            SqlParameter[] parms = new SqlParameter[]
            {

                new SqlParameter("@Source",sql),
                new SqlParameter("@PageSize",limit),
                new SqlParameter("@CurrentPage",start),
                new SqlParameter("@Sort",sort),
                new SqlParameter("@RecordCount",DBNull.Value){ SqlDbType = SqlDbType.Int,Direction=ParameterDirection.InputOutput},
                new SqlParameter("@Start",DBNull.Value){ SqlDbType = SqlDbType.Int},
                new SqlParameter("@Top",DBNull.Value){ SqlDbType = SqlDbType.Int},
            };

            var data = ExecuteStoredProcedure2("pro_sys_DataPage", parms);
            // var data = db.SqlQuery<T>(@"EXEC pro_sys_DataPage @Source,@PageSize,@CurrentPage,@Sort,@RecordCount output,@Start,@Top", parms).ToList();

            total = (int)parms[4].Value;
            return data;

        }

        public static DataTable GetDataTableByPageStart(string sql, string sort, int start, int limit, out int total)
        {

            SqlParameter[] parms = new SqlParameter[]
            {

                new SqlParameter("@Source",sql),
                new SqlParameter("@PageSize",limit),
                new SqlParameter("@CurrentPage",DBNull.Value),
                new SqlParameter("@Sort",sort),
                new SqlParameter("@RecordCount",DBNull.Value){ SqlDbType = SqlDbType.Int,Direction=ParameterDirection.InputOutput},
                new SqlParameter("@Start",start+1){ SqlDbType = SqlDbType.Int},
                new SqlParameter("@Top",DBNull.Value){ SqlDbType = SqlDbType.Int},
            };

            var data = ExecuteStoredProcedure2("pro_sys_DataPage", parms);
            // var data = db.SqlQuery<T>(@"EXEC pro_sys_DataPage @Source,@PageSize,@CurrentPage,@Sort,@RecordCount output,@Start,@Top", parms).ToList();

            total = (int)parms[4].Value;
            return data;

        }

        public static DataTable GetDataTableByPage(this Database db, string sql, string sort, int start, int limit, out int total)
        {
            /*
@Source		NVARCHAR(MAX),				--表名、视图名、查询语句   
@PageSize		INT =NULL,					--每页的大小(行数)   
@CurrentPage	INT =NULL,					--要显示的页   
@Sort			NVARCHAR(MAX)=NULL,			--排序字段列表   
@RecordCount  INT =NULL   OUTPUT,			--输出记录数,   如果@Count为null,   则输出记录数,   否则不要输出   
@Start		INT =NULL,					--开始行号  
@Top			INT =NULL					--取前N条记录
             */
            SqlParameter[] parms = new SqlParameter[]
            {

                new SqlParameter("@Source",sql),
                new SqlParameter("@PageSize",limit),
                new SqlParameter("@CurrentPage",start),
                new SqlParameter("@Sort",sort),
                new SqlParameter("@RecordCount",DBNull.Value){ SqlDbType = SqlDbType.Int,Direction=ParameterDirection.InputOutput},
                new SqlParameter("@Start",DBNull.Value){ SqlDbType = SqlDbType.Int},
                new SqlParameter("@Top",DBNull.Value){ SqlDbType = SqlDbType.Int},
            };

            var data = QueryDataTable(db.Connection, CommandType.StoredProcedure, "pro_sys_DataPage", parms);
            // var data = db.SqlQuery<T>(@"EXEC pro_sys_DataPage @Source,@PageSize,@CurrentPage,@Sort,@RecordCount output,@Start,@Top", parms).ToList();

            total = (int)parms[4].Value;
            return data;
        }

        public static DataTable GetDataTableByPageStart(this Database db, string sql, string sort, int start, int limit, out int total)
        {
            /*
@Source		NVARCHAR(MAX),				--表名、视图名、查询语句   
@PageSize		INT =NULL,					--每页的大小(行数)   
@CurrentPage	INT =NULL,					--要显示的页   
@Sort			NVARCHAR(MAX)=NULL,			--排序字段列表   
@RecordCount  INT =NULL   OUTPUT,			--输出记录数,   如果@Count为null,   则输出记录数,   否则不要输出   
@Start		INT =NULL,					--开始行号  
@Top			INT =NULL					--取前N条记录
             */
            SqlParameter[] parms = new SqlParameter[]
            {

                new SqlParameter("@Source",sql),
                new SqlParameter("@PageSize",limit),
                new SqlParameter("@CurrentPage",DBNull.Value),
                new SqlParameter("@Sort",sort),
                new SqlParameter("@RecordCount",DBNull.Value){ SqlDbType = SqlDbType.Int,Direction=ParameterDirection.InputOutput},
                new SqlParameter("@Start",start){ SqlDbType = SqlDbType.Int},
                new SqlParameter("@Top",DBNull.Value){ SqlDbType = SqlDbType.Int},
            };

            var data = QueryDataTable(db.Connection, CommandType.StoredProcedure, "pro_sys_DataPage", parms);
            // var data = db.SqlQuery<T>(@"EXEC pro_sys_DataPage @Source,@PageSize,@CurrentPage,@Sort,@RecordCount output,@Start,@Top", parms).ToList();

            total = (int)parms[4].Value;
            return data;
        }



        /// <summary>
        /// 获取DataTable
        /// </summary>
        /// <param name="connection">连接</param>
        /// <param name="cmdType">Cmd的类型<</param>
        /// <param name="cmdText">Cmd命令文本</param>
        /// <param name="commandParameters">Parameter数组</param>
        /// <returns>DataTable</returns>
        public static DataTable QueryDataTable(DbConnection connection, CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            DbCommand cmd = connection.CreateCommand();
            cmd.CommandTimeout = 600;
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);

            var factory = GetFactory(connection.ConnectionString);
            using (DbDataAdapter da = factory.CreateDataAdapter())
            {
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmd.Parameters.Clear();
                dt.TableName = "dt";
                return dt;
            }
        }

        public static DbProviderFactory GetFactory(string connectionString)
        {
            return SqlClientFactory.Instance;

        }

        /// <summary>
        /// Prepare a command for execution
        /// </summary>
        /// <param name="cmd">SqlCommand object</param>
        /// <param name="conn">DbConnection object</param>
        /// <param name="trans">DbTransaction object</param>
        /// <param name="cmdType">Cmd type e.g. stored procedure or text</param>
        /// <param name="cmdText">Command text, e.g. Select * from Products</param>
        /// <param name="cmdParms">DbParameters to use in the command</param>
        private static void PrepareCommand(DbCommand cmd, DbConnection conn, DbTransaction trans, CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();


            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (var parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

    }
}
