using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Data;
using Common.Data.Extenstions;
namespace DBSql.Sys
{
    public class BaseMenuPermissionByRole : EFRepository<DataModel.Models.BaseMenuPermissionByRole>
    {
        /// <summary>
        /// 更新菜单权限 指定角色的权限
        /// </summary>
        /// <param name="roleID"></param>
        /// <param name="MenuItem"></param>
        /// <returns></returns>
        public bool UpdatePermitByRole(int roleID, List<DataModel.MenuPermitSet> MenuItem, List<int> MenuArr = null)
        {
            bool result = false;
            //step1  删除对应条件信息
            var TWhere = QueryBuild<DataModel.Models.BaseMenuPermissionByRole>.True();
            if (roleID != 0) TWhere = TWhere.And(p => p.BaseMenuPermissionByRoleID == roleID);
            if (MenuArr != null && MenuArr.Count > 0)
            {
                TWhere = TWhere.And(p => MenuArr.Contains(p.BaseMenuPermissionByRoleValue));
            }
            Delete(TWhere);
            //step2  新增对象里的信息
            foreach (var item in MenuItem)
            {
                DataModel.Models.BaseMenuPermissionByRole role = new DataModel.Models.BaseMenuPermissionByRole() 
                {
                    BaseMenuPermissionByRoleID = roleID,
                    BaseMenuPermissionByRoleValue = item.MenuID,
                    RolePermissionValue = item.MenuPermissions,
                    BaseMenuPermissionByRoleNote =""
                };
                Add(role);
            }
            try
            {
                UnitOfWork.SaveChanges();
                EFCacheRemove("BaseMenu", "BaseMenuPermit", "BaseMenuPermissionByRole");
                result =  true;
            }
            catch { }
            return result;
        }


        /// <summary>
        /// 更新菜单权限 中指定角色的权限
        /// </summary>
        /// <param name="MenuID"></param>
        /// <param name="RoleItem"></param>
        public bool UpdatePermitByMenu(int MenuID, List<int> RoleItem)
        {
            bool result = false;
            //step1  删除对应条件信息
            var TWhere = QueryBuild<DataModel.Models.BaseMenuPermissionByRole>.True();
            if (MenuID != 0) TWhere = TWhere.And(p => p.BaseMenuPermissionByRoleValue == MenuID);
            Delete(TWhere);
            //step2  新增对象里的信息
            foreach (var item in RoleItem)
            {
                DataModel.Models.BaseMenuPermissionByRole role = new DataModel.Models.BaseMenuPermissionByRole()
                {
                    BaseMenuPermissionByRoleID = item,
                    BaseMenuPermissionByRoleValue = MenuID,
                    BaseMenuPermissionByRoleNote = ""
                };
                Add(role);
            }
            try
            {
                UnitOfWork.SaveChanges();
                EFCacheRemove("BaseMenu", "BaseMenuPermit", "BaseMenuPermissionByRole");
                result = true;
            }
            catch { }
            return result;
        }

        public IEnumerable<DataModel.Models.BaseMenuPermissionByRole> AllData
        {
            get
            {
                return GetListFromCahe(s => s.BaseMenuPermissionByRoleID > 0, 480, "BaseMenuPermissionByRole");
            }
        }

        /// <summary>
        /// 获取菜单角色列表
        /// </summary>
        /// <param name="MenuID"></param>
        /// <returns></returns>
        public List<DataModel.Models.BaseMenuPermissionByRole> GetMenuHasRole(int MenuID)
        {
            if (MenuID == 0) return AllData.ToList();
            return AllData.Where(p => p.BaseMenuPermissionByRoleValue == MenuID).ToList();
        }
    }
}
