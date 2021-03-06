﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-07-30 08:42:15
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Common.Data;
using Common.Data.Extenstions;
using DAL;
namespace DBSql.Oa
{
    public class OaMail : EFRepository<DataModel.Models.OaMail>
    {
        DBSql.Sys.BaseEmployee emp = new Sys.BaseEmployee();
        public Expression<Func<DataModel.Models.OaMail, bool>> GetFunc(Common.SqlPageInfo condition)
        {
            var TWhere = QueryBuild<DataModel.Models.OaMail>.True();
            #region   筛选条件
            if (!String.IsNullOrEmpty(condition.TextCondtion))
            {
                TWhere = TWhere.And(p => p.MailTitle.Contains(condition.TextCondtion));
            }
            foreach (System.Collections.DictionaryEntry de in condition.SelectCondtion)
            {
                if (de.Value == null || de.Value.ToString().Trim() == "") continue;
                switch (de.Key.ToString())
                {
                    case "MailID":
                        var MailID = Common.ModelConvert.ConvertToDefaultType<int>(de.Value);
                        TWhere = TWhere.And(p => p.Id == MailID);
                        break;
                    case "DateLower":
                        var DateLower = Common.ModelConvert.ConvertToDefaultType<DateTime>(de.Value);
                        TWhere = TWhere.And(p => p.MailDate >= DateLower);
                        break;
                    case "DateUpper":
                        var DateUpper = Common.ModelConvert.ConvertToDefaultType<DateTime>(de.Value).AddDays(1);
                        TWhere = TWhere.And(p => p.MailDate < DateUpper);
                        break;
                    case "MailEmpID":
                        var MailEmpID = Common.ModelConvert.ConvertToDefaultType<int>(de.Value);
                        TWhere = TWhere.And(p => p.CreatorEmpId == MailEmpID);
                        break;
                    case "MailFlag":
                        var MailFlag = Common.ModelConvert.ConvertToDefaultType<int>(de.Value);
                        TWhere = TWhere.And(p => p.MailFlag == MailFlag);
                        break;
                    case "MailIsDelete":
                        var MailIsDelete = Common.ModelConvert.ConvertToDefaultType<bool>(de.Value);
                        TWhere = TWhere.And(p => p.MailIsDelete == MailIsDelete);
                        break;
                }
            }
            #endregion
            if (String.IsNullOrEmpty(condition.SelectOrder)) condition.SelectOrder = "MailDate desc";
            return TWhere;
        }

        public int SendMail(DataModel.Models.OaMail model, string EmpIDs, bool IsEdit)
        {
            int success = 0;
            try
            {
                if (IsEdit)
                {
                    model.FK_OaMailRead_Id.Clear();
                }
                var empIDs = new List<int>();
                foreach (var empID in EmpIDs.Split(','))
                {
                    var temp = 0;
                    if (int.TryParse(empID, out temp) && temp > 0 && !empIDs.Contains(temp))
                    {
                        empIDs.Add(temp);
                    }
                }
                model.FK_OaMailRead_Id = new List<DataModel.Models.OaMailRead>();
                foreach (var empID in empIDs)
                {
                    DataModel.Models.OaMailRead send = new DataModel.Models.OaMailRead()
                    {
                        MailReadEmpId = empID,
                        MailReadEmpName = emp.GetBaseEmployeeInfo(empID).EmpName,
                        MailReadIsDelete = 0,
                        MailReadDate = new DateTime(1900, 1, 1),
                    };
                    model.FK_OaMailRead_Id.Add(send);
                }
                if (IsEdit)
                {
                    Edit(model);
                }
                else
                {
                    Add(model);
                }
                UnitOfWork.SaveChanges();
                new OaMailRead().CacheRemove();
                //发送首页通知
                var t = JQ.Util.IO.MessageMonitor.NotifyAsync(empIDs, delegate (int empID)
                  {
                      return new DBSql.Oa.OaMailRead().GetNotifyDatas(empID);
                  });
            }
            catch { success = -1; }

            return success;
        }

        public int DelMail(List<int> arr, DataModel.EmpSession emp, bool DelType = false,bool IsResum = false)
        {
            int Result = 0;

            var date = System.DateTime.Now;
            if (!IsResum)
            {
                if (DelType)
                {
                    Delete(p => arr.Contains(p.Id));
                }
                else
                {
                    Edit(s => arr.Contains(s.Id), u => new DataModel.Models.OaMail { MailIsDelete = true });
                }
            }
            else
            {
                date = new System.DateTime(1900,1,1);
                Edit(s => arr.Contains(s.Id), u => new DataModel.Models.OaMail { DeleterEmpId = 0, DeleterEmpName = "", DeletionTime = date, MailIsDelete = false });
            }
            Result = DbContext.SaveChanges();

            new OaMailRead().CacheRemove();
            return Result;
        }
    }
}
