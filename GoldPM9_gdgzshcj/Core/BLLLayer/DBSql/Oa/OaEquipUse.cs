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
using System.Data;
namespace DBSql.Oa
{
    public class OaEquipUse : EFRepository<DataModel.Models.OaEquipUse>
    {
        public DataTable GetCount(int equipFromId)
        {
            string strSql = @" select t1.EquipFormID,t1.EquipID,t1.EquipCount,ISNULL(t2.EquipCount,0) as Count, 
                                case when  t1.EquipCount - ISNULL(t2.EquipCount,0) > 0 then '是' else '否' end as isLess from (select EquipFormID,EquipID,equipCount from OaEquipStock where  EquipActionID = 2 and EquipFormID = '"+equipFromId+@"') t1 
                                left join
                                (select EquipID,SUM(EquipCount) as EquipCount from oaequipstock where equipformid in ( select id from oaequipreturn where UseId = '"+equipFromId+@"' ) 
                                and equipactionid = 3 group by EquipID ) t2 on t1.EquipID = t2.EquipID
                                where EquipFormID = '" + equipFromId + "' ";
            return DAL.DBExecute.ExecuteDataTable(strSql.ToString());
        }
    }
}
