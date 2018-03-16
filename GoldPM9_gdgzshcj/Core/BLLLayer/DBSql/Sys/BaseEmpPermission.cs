using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Data;
using Common.Data.Extenstions;
namespace DBSql.Sys
{
    public class BaseEmpPermission : EFRepository<DataModel.Models.BaseEmpPermission>
    {
        /// <summary>
        /// 获取所有用户信息 
        /// 使用缓存
        /// </summary>
        public IEnumerable<DataModel.Models.BaseEmpPermission> AllData
        {
            get
            {
                return GetListFromCahe(s => s.PermissionEmpID > 0, 480, "BaseEmpPermission"); 
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public IEnumerable<DataModel.Models.BaseEmpPermission> getRolesEmpList(int[] roles)
        {
            var list = AllData.Where(p => roles.Contains(p.PermissionEmpValue)) ;
            if (list == null || list.Count() <= 0) return null;
            return list;
        }

        /// <summary>
        /// 根据用户ID
        /// 取得角色名称列表串
        /// </summary>
        /// <param name="empID">用户ID</param>
        /// <returns>角色名称列表。</returns>
        public List<string> GetRoleNames(int empID)
        {
            List<DataModel.Models.BaseEmpPermission> list = AllData.Where(p => p.PermissionEmpID == empID).ToList();
            List<string> role = new List<string>();

            var dsBaseData = new Sys.BaseData();
            foreach (var dr in list)
            {
                if (dr.FK_BaseEmpPermission_PermissionEmpValue != null)
                {
                    var model = dsBaseData.GetBaseDataInfo(dr.PermissionEmpValue);
                    if (model != null) role.Add(model.BaseName);
                }
            }
            return role;
        }
    }
}
