using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Data;
using Common.Data.Extenstions;
namespace DBSql.Sys
{
    public class BaseMenuPermissionByEmp : EFRepository<DataModel.Models.BaseMenuPermissionByEmp>
    {
        /// <summary>
        /// 更新菜单权限 中指定人员的权限
        /// </summary>
        /// <param name="EmpID">人员ID</param>
        /// <param name="EmpItem"></param>
        /// <returns></returns>
        public bool UpdatePermitByEmp(int EmpID, List<DataModel.MenuPermitSet> EmpItem, List<int> MenuArr = null)
        {
            bool result = false;
            //step1  删除对应条件信息
            var TWhere = QueryBuild<DataModel.Models.BaseMenuPermissionByEmp>.True();
            if (EmpID != 0) TWhere = TWhere.And(p => p.BaseMenuPermissionByEmpID == EmpID);
            if (MenuArr != null && MenuArr.Count > 0)
            {
                TWhere = TWhere.And(p => MenuArr.Contains(p.BaseMenuPermissionByEmpValue));
            }
            Delete(TWhere);
            //step2  新增对象里的信息
            foreach (var item in EmpItem)
            {
                DataModel.Models.BaseMenuPermissionByEmp emp = new DataModel.Models.BaseMenuPermissionByEmp()
                {
                    BaseMenuPermissionByEmpValue = item.MenuID,
                    BaseMenuPermissionByEmpID = EmpID,
                    EmpPermissionValue = item.MenuPermissions,
                    BaseMenuPermissionByEmpNote = ""
                };
                Add(emp);
            }
            try
            {
                UnitOfWork.SaveChanges();
                EFCacheRemove("BaseMenu", "BaseMenuPermit", "BaseMenuPermissionByEmp");
                result = true;
            }
            catch { }
            return result;
        }

        public IEnumerable<DataModel.Models.BaseMenuPermissionByEmp> AllData
        {
            get
            {
                return GetListFromCahe(s => s.BaseMenuPermissionByEmpID > 0, 480, "BaseMenuPermissionByEmp");
            }
        }

        /// <summary>
        /// 获取菜单人员列表
        /// </summary>
        /// <param name="MenuID"></param>
        /// <returns></returns>
        public List<DataModel.Models.BaseMenuPermissionByEmp> GetMenuHasEmp(int MenuID=0)
        {
            if(MenuID == 0) return  AllData.ToList();
            return AllData.Where(p => p.BaseMenuPermissionByEmpValue == MenuID).ToList();
        }
    }
}
