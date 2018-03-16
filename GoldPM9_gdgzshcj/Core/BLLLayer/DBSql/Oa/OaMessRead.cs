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
    public class OaMessRead : EFRepository<DataModel.Models.OaMessRead>
    {
        public IEnumerable<dynamic> AllData
        {
            get
            {
                if (Common.CacheManager.GetCache("OaMessRead") == null)
                {
                    var data = (from t in GetQuery()
                                join t1 in this.DbContext.Set<DataModel.Models.OaMess>() on t.Id equals t1.Id
                                where t.MessReadIsDeleted == false && t1.MessIsDeleted == false && t.MessReadDate == JQ.Util.TypeParse.DefaultDateTime
                                orderby t1.MessDate descending
                                select new { t.Id, t.MessReadEmpId, t1.MessEmpName, t1.MessTitle, t1.MessDate }).ToList();
                    return Common.CacheManager.CacheTable<dynamic>("OaMessRead", data);
                }
                else
                {
                    return (IEnumerable<dynamic>)Common.CacheManager.GetCache("OaMessRead");
                }
            }
        }

        public static void CacheRemove()
        {
            Common.CacheManager.CacheRemove("OaMessRead");
        }

        public int GetUnReadMessage(int empID)
        {
            return AllData.Where(m => m.MessReadEmpId == empID).Count();
        }

        public List<dynamic> GetUnReadDetail(int empID, int topAmount)
        {
            //从缓存中获取中前topAmount条数据
            return AllData.Where(p => p.MessReadEmpId == empID).Take(topAmount).ToList();
        }

        public dynamic GetNotifyDatas(int empID)
        {
            return new { action = "ChangeMessageAmount", data = GetToDisplayDatas(empID) };
        }


        public dynamic GetToDisplayDatas(int empID)
        {
            return new { Result = true, Total = this.GetUnReadMessage(empID), Datas = this.GetUnReadDetail(empID, 5) };
        }
    }
}