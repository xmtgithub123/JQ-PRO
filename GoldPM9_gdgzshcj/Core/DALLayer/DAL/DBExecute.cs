using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    /// <summary>
    /// DBExecute ��ժҪ˵����
    /// </summary>
    public abstract class DBExecute
    {
        public DBExecute()
        {
        }

        //���ݿ������ַ���
        public static readonly string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["jqpm7Context"].ConnectionString;

        // Hashtable�����������
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());


        /// <summary>
        /// ִ�в�ѯ����������ѯ�Ľ��������DataSet��
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
        /// ִ�н���ѯ�Ľ��������DataTable
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
        /// ����һ����¼�����ؼ�¼��������
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
        /// ����һ����¼�����ؼ�¼����������������
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
        /// ִ�����/�޸�/ɾ��
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
        /// ִ�ж���SQL��䣬ʵ�����ݿ�����
        /// </summary>
        /// <param name="SQLStringList">SQL���Ĺ�ϣ��keyΪsql��䣬value�Ǹ�����SqlParameter[]��</param>
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
                        //ѭ��
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
        /// ִ�����/�޸�/ɾ����ʹ��������
        /// ע�⣺����Ҫ�ر�,�磺conn.Close();
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
        /// ִ�в�ѯ������һ��SqlDataReader�����
        /// ע�⣺SqlDataReader�ر�,�磺dr.Close();
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
        /// ִ�в�ѯ�����ص�һ����¼��һ�е�ֵ
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
        /// ����������ӵ�����
        /// </summary>
        public static void CacheParameters(string cacheKey, params SqlParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }

        /// <summary>
        /// �ӻ����л�ȡ������
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
        /// ׼��ִ������
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
