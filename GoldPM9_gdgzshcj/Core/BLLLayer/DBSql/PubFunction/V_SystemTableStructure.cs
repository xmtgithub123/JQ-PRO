using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Data;
using DAL;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
namespace DBSql.PubFunction
{
    public class V_SystemTableStructure : EFRepository<DataModel.Models.V_SystemTableStructure>
    {
        public IEnumerable<DataModel.Models.V_SystemTableStructure> AllData
        {
            get
            {
                return GetListFromCahe(s => s.TableName != "", 600, "V_SystemTableStructure");
            }
        }

        public int SP_EditDescription(string type, string tableName, string columnName, string description)
        {
            List<System.Data.SqlClient.SqlParameter> Parameter = new List<System.Data.SqlClient.SqlParameter>();

            Parameter.Add(new System.Data.SqlClient.SqlParameter("Type", type));
            Parameter.Add(new System.Data.SqlClient.SqlParameter("TableName", tableName));
            Parameter.Add(new System.Data.SqlClient.SqlParameter("ColumnName", columnName));
            Parameter.Add(new System.Data.SqlClient.SqlParameter("Description", description));

            string Sql = "EXEC  [dbo].[SP_EditDescription] @Type, @TableName, @ColumnName, @Description";
            int result = ExecuteNonQuery(Sql, Parameter.ToArray());
            EFCacheRemove("V_SystemTableStructure");

            return result;
        }

        public int SP_SystemTableStructure()
        {
            int result = ExecuteNonQuery("EXEC  [dbo].[SP_SystemTableStructure]", new List<System.Data.SqlClient.SqlParameter>().ToArray());
            EFCacheRemove("V_SystemTableStructure");
            return result;
        }


        public virtual int SP_BakupDataBase(string path, string toPath, string userName, string userPwd, string iP, string dBName, string flagDel)
        {
            List<System.Data.SqlClient.SqlParameter> Parameter = new List<System.Data.SqlClient.SqlParameter>();

            Parameter.Add(new System.Data.SqlClient.SqlParameter("path", path));
            Parameter.Add(new System.Data.SqlClient.SqlParameter("toPath", toPath));
            Parameter.Add(new System.Data.SqlClient.SqlParameter("userName", userName));
            Parameter.Add(new System.Data.SqlClient.SqlParameter("userPwd", userPwd));
            Parameter.Add(new System.Data.SqlClient.SqlParameter("iP", iP));
            Parameter.Add(new System.Data.SqlClient.SqlParameter("dBName", dBName));
            Parameter.Add(new System.Data.SqlClient.SqlParameter("flagDel", flagDel));

            string Sql = "EXEC  [dbo].[SP_BakupDataBase] @path, @toPath, @userName, @userPwd, @iP, @dBName, @flagDel";
            int result = DBExecute.ExecuteNonQuery(DBExecute.ConnectionString, Sql, Parameter.ToArray());
            return result;
        }


        public static string GetMessContent(DataModel.Models.Flow flow)
        {
            if (flow.FlowRole == "") return flow.FlowName + " 审批完成";

            string colValue = flow.FlowRole;
            string Sql = string.Format("Select * from {0} where {1}={2}", flow.FlowRefTable, flow.FlowNum, flow.FlowRefID.ToString());
            DataTable result = DBExecute.ExecuteDataTable(DBExecute.ConnectionString, Sql);
            if (result.Rows.Count == 1)
            {
                DataRow dr = result.Rows[0];

                var bulid = @"{((?:\s|\S)*?)}";
                Regex reg = new Regex(bulid, RegexOptions.IgnoreCase);
                foreach (Match item in reg.Matches(flow.FlowRole))
                {
                    string content = item.Groups[0].ToString();
                    string colName = item.Groups[1].ToString();
                    string val = dr[colName].ToString();
                    colValue = colValue.Replace(content, val);
                }


                //[] 中的控件可用
                bulid = @"\[((?:\s|\S)*?)\]";
                reg = new Regex(bulid, RegexOptions.IgnoreCase);
                foreach (Match item in reg.Matches(flow.FlowRole))
                {
                    string content = item.Groups[0].ToString();
                    string colName = item.Groups[1].ToString();
                    int val = int.Parse(dr[colName].ToString());
                    string value = "";
                    //switch (colName)
                    //{
                    //    case "ProjID":
                    //        var model1 = new Project.Project().Get(val);
                    //        if (model1 != null)
                    //        {
                    //            value = model1.ProjNumber + model1.ProjName;
                    //        }
                    //        break;
                    //    case "ProjPhaseID":
                    //        var model2 = new Project.ProjPhase().Get(val);
                    //        if (model2 != null)
                    //        {
                    //            value = model2.PhaseName ;
                    //        }
                    //        break;
                    //    case "EngID":
                    //        var model3 = new  Business.Engineering().Get(val);
                    //        if (model3 != null)
                    //        {
                    //            value = model3.EngNumber + model3.EngName;
                    //        }
                    //        break;
                    //    case "SubID":
                    //        var model4 = new  Project.EngSub().Get(val);
                    //        if (model4 != null)
                    //        {
                    //            value = model4.SubNumber + model4.SubName;
                    //        }
                    //        break;
                    //    default:
                    //        value = new Sys.BaseData().GetBaseDataInfo(val).BaseName;
                    //        break;
                    //}
                    colValue = colValue.Replace(content, string.Format("<{0}>", value));
                }
            }

            return colValue + flow.FlowName + " 审批完成";
        }
    }
}
