using DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Models;
using System.Data.SqlClient;

namespace DBSql.HR
{
    public class HREmployee : EFRepository<DataModel.Models.HREmployee>
    {
        /// <summary>
        /// 查询人员Table
        /// </summary>
        /// <returns></returns>
        public DataTable GetHREmployeeTable(Common.SqlPageInfo sqlMod)
        {
            //定义查询列
            string RowColumn = @"
                e.Id,
	            e.DepID,
	            bd.BaseName as  DepName,
	            e.EmpName,
	            e.EmpJoinDate,
	            e.EmpSexID,
	            isnull((select top(1)BaseName from BaseData where BaseID=e.EmpSexID),'')as EmpSexName,
	            e.EmpPhoneNumber,
	            e.LastEducationID,
	            isnull((select top(1)BaseName from BaseData where BaseID=e.LastEducationID),'')as LastEducationName,
                EmpAccountsAddress,
                EmpIder,
                (select COUNT(1) from HRAptitudeManager where AptitudeEmpId=e.Id) as ZZCount,
                (select COUNT(1) from HREmpWinManager where AptitudeEmpId=e.Id) as HJCount,
                VacationDays,
                VacationDays1
            ";
            //定义查询体
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Count(1) from HREmployee as e inner join BaseData bd on e.DepID=bd.BaseID where 1=1  ");
            //定义模糊查询条件
            if (!string.IsNullOrEmpty(sqlMod.TextCondtion))
            {
                strSql.AppendFormat(" and ( ");
                strSql.AppendFormat(" e.EmpName Like '%{0}%' ", sqlMod.TextCondtion);
                strSql.AppendFormat(" ) ");
            }
            //定义赋值查询条件
            if (sqlMod.SelectCondtion != null && sqlMod.SelectCondtion.Count > 0)
            {
                foreach (DictionaryEntry de in sqlMod.SelectCondtion)
                {
                    if (de.Value == null || de.Value.ToString().Trim() == "")
                    {
                        continue;
                    }
                    switch (de.Key.ToString())
                    {
                        case "DepID":
                            if (de.Value.ToString() != "0")
                            {
                                strSql.AppendFormat(" and e.DepID={0}", de.Value.ToString());
                            }
                            break;
                        case "EmpStatusID":
                            if (de.Value.ToString() != "0")
                            {
                                strSql.AppendFormat(" and e.EmpStatusID={0}", de.Value.ToString());
                            }
                            break;
                        case "BeginDate":
                            strSql.AppendFormat(" and convert(datetime,CONVERT(nvarchar(10),e.EmpJoinDate,111))>='{0}'", de.Value.ToString());
                            break;
                        case "EndDate":
                            strSql.AppendFormat(" and convert(datetime,CONVERT(nvarchar(10),e.EmpJoinDate,111))<='{0}'", de.Value.ToString());
                            break;
                        case "CreatorDepId":
                            strSql.AppendFormat(" and DepID={0}", de.Value.ToString());
                            break;
                        case "CreateEmpId":
                            strSql.AppendFormat(" and SysEmpId={0}", de.Value.ToString());
                            break;
                        case "CompanyID":
                            if (de.Value.ToString() != "0")
                            {
                                strSql.AppendFormat(" and CompanyID={0}", de.Value.ToString());
                            }
                            break;
                        case "CompanyTS":
                            strSql.AppendFormat(" and e.CompanyID IN (0,1537,1538,1612)");
                            break;
                        default:
                            break;
                    }

                }
            }
            //定义总记录数
            object obj = DBExecute.ExecuteScalar(DBExecute.ConnectionString, strSql.ToString());
            if (obj == null && obj == DBNull.Value)
            {
                sqlMod.PageTotleRowCount = 0;
            }
            else
            {
                sqlMod.PageTotleRowCount = Convert.ToInt32(obj);
            }
            //排序
            if (String.IsNullOrEmpty(sqlMod.SelectOrder))
            {
                sqlMod.SelectOrder = "bd.BaseOrder,e.Id";
            }

            //将组装后的sqlMod组装成sql语句
            string sql = Helper.SqlPage.ExecPageStrSql(sqlMod, RowColumn, strSql);
            //执行sql语句
            DataTable dt = DBExecute.ExecuteDataTable(DBExecute.ConnectionString, sql.ToString());
            return dt;
        }
        /// <summary>
        /// 查询人员信息下拉框列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetEmpSelect()
        {
            string sql = @"select e.Id,
	e.EmpName
from HREmployee as e inner join BaseData as bd on e.DepID=bd.BaseID
order by bd.BaseOrder,e.Id";
            return DBExecute.ExecuteDataTable(sql);
        }
        /// <summary>
        /// 查询单个HREmployee
        /// </summary>
        /// <returns></returns>
        public DataModel.Models.HREmployee GetHREmployee(int id)
        {
            return SingleOrDefault(p => p.Id == id);
        }
    }
}
