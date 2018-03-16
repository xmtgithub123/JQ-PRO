using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Common.Data;
using Common.Data.Extenstions;
using DAL;
namespace DBSql.Sys
{
    public class BaseLog : EFRepository<DataModel.Models.BaseLog>
    {
        public Expression<Func<DataModel.Models.BaseLog, bool>> GetFunc(Common.SqlPageInfo condition)
        {
            var TWhere = QueryBuild<DataModel.Models.BaseLog>.True();

            #region   筛选条件

            if (!String.IsNullOrEmpty(condition.TextCondtion))
            {
                TWhere = TWhere.And(p => p.BaseLogIP.Contains(condition.TextCondtion));
            }
            foreach (System.Collections.DictionaryEntry de in condition.SelectCondtion)
            {
                if (de.Value == null || de.Value.ToString().Trim() == "") continue;

                if (de.Value.ToString().Trim() == "0") continue;

                switch (de.Key.ToString())
                {
                    case "BaseLogTypeID":
                        var BaseLogTypeID = Common.ModelConvert.ConvertToDefaultType<int>(de.Value);
                        TWhere = TWhere.And(p => p.BaseLogTypeID == BaseLogTypeID);
                        break;
                    case "DateLower":
                        var LogDateLower = Common.ModelConvert.ConvertToDefaultType<DateTime>(de.Value);
                        TWhere = TWhere.And(p => p.BaseLogDate >= LogDateLower);
                        break;
                    case "DateUpper":
                        var LogDateUpper = Common.ModelConvert.ConvertToDefaultType<DateTime>(de.Value);
                        LogDateUpper =LogDateUpper.AddDays(1);
                        TWhere = TWhere.And(p => p.BaseLogDate <  LogDateUpper);
                        break;
                    case "BaseEmployee":
                        var EmpID = Common.ModelConvert.ConvertToDefaultType<int>(de.Value);
                        TWhere = TWhere.And(p => p.BaseLogEmpID == EmpID);
                        break;
                }
            }
            #endregion

            if(String.IsNullOrEmpty(condition.SelectOrder)) condition.SelectOrder = "BaseLogEmpID desc";

            
            return TWhere;
        }


        public DateTime? GetLastLockTime(string empName)
        {
            var model = this.GetQuery(s => s.EmpName == empName && s.BaseLogTypeID == -1).OrderByDescending(s => s.BaseLogID).FirstOrDefault();
            if (model == null) return null;

            return model.BaseLogDate;
        }


        public int LogLock(string empName,string IP,DateTime? LastLockTime,int LoginLockTime,int LoginMaxCount)
        {
            DBSql.Sys.BaseConfig opCofig = new DBSql.Sys.BaseConfig();
            var time = DateTime.Now.AddMinutes(-LoginLockTime);
            if (LastLockTime != null && LastLockTime.Value > time) time = LastLockTime.Value;
            var count = Count(s => s.BaseLogDate > time && s.EmpName == empName && s.BaseLogTypeID == 1);
            if (count == LoginMaxCount)
            {
                var log = new DataModel.Models.BaseLog();
                log.BaseLogEmpID = 0;
                log.EmpName = empName;
                log.BaseLogTypeID = -1;
                log.BaseLogDate = DateTime.Now;
                log.BaseLogIP = IP;
                log.BaseLogRefTable = LoginLockTime.ToString();
                log.BaseLogRefID = 0;
                log.BaseLogRefHTML = "账户锁定！";

                Add(log);
                UnitOfWork.SaveChanges();
            }
            return count;
        }
    }
}
