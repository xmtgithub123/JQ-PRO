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
    public class BaseEmpAgen : EFRepository<DataModel.Models.BaseEmpAgen>
    {
        public Expression<Func<DataModel.Models.BaseEmpAgen, bool>> GetFunc(Common.SqlPageInfo condition)
        {
            var TWhere = Common.Data.Extenstions.QueryBuild<DataModel.Models.BaseEmpAgen>.True();

            #region   筛选条件
            if (!String.IsNullOrEmpty(condition.TextCondtion))
            {
                TWhere = TWhere.And(p => p.AgenNote.Contains(condition.TextCondtion));
            }
            foreach (System.Collections.DictionaryEntry de in condition.SelectCondtion)
            {
                if (de.Value == null || de.Value.ToString().Trim() == "") continue;

                switch (de.Key.ToString())
                {
                    case "FromEmpID":
                        var FromEmpID = Common.ModelConvert.ConvertToDefaultType<int>(de.Value);
                        TWhere = TWhere.And(p => p.FromEmpID == FromEmpID);
                        break;
                    case "ToEmpID":
                        var ToEmpID = Common.ModelConvert.ConvertToDefaultType<int>(de.Value);
                        TWhere = TWhere.And(p => p.ToEmpID == ToEmpID);
                        break;
                    case "EmpID":
                        var EmpID = Common.ModelConvert.ConvertToDefaultType<int>(de.Value);
                        TWhere = TWhere.And(p => (p.ToEmpID == EmpID || p.FromEmpID == EmpID));
                        break;
                    case "DateLower":
                        var LogDateLower = Common.ModelConvert.ConvertToDefaultType<DateTime>(de.Value);
                        TWhere = TWhere.And(p => p.DateCreate >= LogDateLower);
                        break;
                    case "DateUpper":
                        var LogDateUpper = Common.ModelConvert.ConvertToDefaultType<DateTime>(de.Value);
                        TWhere = TWhere.And(p => p.DateCreate <= LogDateUpper);
                        break;
                    case "DateNow":
                        var DateNow = Common.ModelConvert.ConvertToDefaultType<DateTime>(de.Value);
                        TWhere = TWhere.And(p => p.DateLower <= DateNow && p.DateUpper >= DateNow);
                        break;
                    case "AgenStatus":
                        var AgenStatus = Common.ModelConvert.ConvertToDefaultType<int>(de.Value);
                        TWhere = TWhere.And(p => p.AgenStatus == AgenStatus);
                        break;
                }
            }
            #endregion
            if (String.IsNullOrEmpty(condition.SelectOrder)) condition.SelectOrder = "DateCreate desc";
            return TWhere;
        }
    }
}
