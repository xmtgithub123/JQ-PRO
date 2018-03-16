using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    /// <summary>
    /// DBExecute 的摘要说明。
    /// </summary>
    public abstract class DBExecute
    {
        public DBExecute()
        {
        }

        //数据库连接字符串
        public static readonly string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["jqpm7Context"].ConnectionString;

        // Hashtable用来缓存参数
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());


        /// <summary>
        /// 执行查询，并将将查询的结果保存在DataSet中
        /// </summary>
        public static DataSet ExecuteDataSet(string connectionString, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, CommandType.Text, cmdText, commandParameters);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                cmd.Parameters.Clear();
                return ds;
            }
        }
        /// <summary>
        /// 执行将查询的结果保存在DataTable
        /// </summary>		
        public static DataTable ExecuteDataTable(string connectionString, string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteDataSet(connectionString, cmdText, commandParameters).Tables[0];
        }

        public static DataTable ExecuteDataTable(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteDataSet(ConnectionString, cmdText, commandParameters).Tables[0];
        }

        /// <summary>
        /// 插入一条记录，返回记录的自增量
        /// </summary>
        public static int ExecuteInsert(string connectionString, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmdText = cmdText + " ;select @@identity";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, CommandType.Text, cmdText, commandParameters);
                int val = 0;
                try
                {
                    val = int.Parse(cmd.ExecuteScalar().ToString());
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                cmd.Parameters.Clear();
                return val;
            }
        }
        /// <summary>
        /// 插入一条记录，返回记录的自增量的事务处理
        /// </summary>
        public static int ExecuteInsert(SqlTransaction trans, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            cmdText = cmdText + " ;select @@identity";
            PrepareCommand(cmd, trans.Connection, trans, CommandType.Text, cmdText, commandParameters);
            int val = int.Parse(cmd.ExecuteScalar().ToString());
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 执行添加/修改/删除
        /// </summary>
        public static int ExecuteNonQuery(string connectionString, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, CommandType.Text, cmdText, commandParameters);
                int val = 0;
                try
                {
                    val = cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                cmd.Parameters.Clear();
                return val;
            }
        }

        public static int ExecuteNonQuery(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteNonQuery(ConnectionString, cmdText, commandParameters);
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public static int ExecuteSqlTran(string connectionString, Hashtable SQLStringList)
        {
            int res = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        //循环
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, CommandType.Text, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                            res += val;
                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        res = -1;
                        throw;
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// 执行添加/修改/删除，使用事务处理
        /// 注意：连接要关闭,如：conn.Close();
        /// </summary>
        public static int ExecuteNonQuery(SqlTransaction trans, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, CommandType.Text, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 执行查询，返回一个SqlDataReader结果集
        /// 注意：SqlDataReader关闭,如：dr.Close();
        /// </summary>
        public static SqlDataReader ExecuteReader(string connectionString, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                PrepareCommand(cmd, conn, null, CommandType.Text, cmdText, commandParameters);
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
        /// 执行查询，返回第一条记录第一列的值
        /// </summary>
        public static object ExecuteScalar(string connectionString, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, CommandType.Text, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        public static object ExecuteScalar(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteScalar(ConnectionString, cmdText, commandParameters);
        }


        /// <summary>
        /// 将参数集添加到缓存
        /// </summary>
        public static void CacheParameters(string cacheKey, params SqlParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }

        /// <summary>
        /// 从缓存中获取参数集
        /// </summary>
        public static SqlParameter[] GetCachedParameters(string cacheKey)
        {
            SqlParameter[] cachedParms = (SqlParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];

            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }

        /// <summary>
        /// 准备执行命令
        /// </summary>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
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
                foreach (SqlParameter parm in cmdParms)
                {
                    if (parm != null && parm.Value != null)
                    {
                        cmd.Parameters.Add(parm);
                    }
                }
            }
        }

    }
}
