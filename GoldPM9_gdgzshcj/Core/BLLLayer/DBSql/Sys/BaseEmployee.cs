/* --------------------------------------------------------------------------
 * 编写日期：2012-06-25
 * 编写人：梅鹏
 * 实现功能：系统模块，人员操作
 * 包括：人员的增、删、改、查 
 * 人员权限 密码验证  人员排序
 * --------------------------------------------------------------------------
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Data;
using Common.Data.Extenstions;
using JQ.Util;
using DAL;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using EntityFramework.Extensions;
using DataModel.Models;
using System.Web;

namespace DBSql.Sys
{
    public class AllBaseEmployee : EFRepository<DataModel.Models.AllBaseEmployee>
    {
        public Expression<Func<DataModel.Models.AllBaseEmployee, bool>> GetFun(Common.SqlPageInfo condition)
        {

            var TWhere = QueryBuild<DataModel.Models.AllBaseEmployee>.True();
            #region   筛选条件
            foreach (System.Collections.DictionaryEntry de in condition.SelectCondtion)
            {
                if (de.Value == null || de.Value.ToString().Trim() == "") continue;
                if (de.Value.ToString().Trim() == "0") continue;
                switch (de.Key.ToString())
                {
                    case "EmpDepID":
                        var EmpDepID = Common.ModelConvert.ConvertToDefaultType<int>(de.Value);
                        if (EmpDepID != 0) TWhere = TWhere.And(p => p.EmpDepID == EmpDepID);
                        break;
                    case "EmpIsDeleted":
                        var EmpIsDeleted = Common.ModelConvert.ConvertToDefaultType<bool>(de.Value);
                        TWhere = TWhere.And(p => p.EmpIsDeleted == EmpIsDeleted);
                        break;
                    case "EmpID":
                        var EmpID = Common.ModelConvert.ConvertToDefaultType<int>(de.Value);
                        TWhere = TWhere.And(p => p.EmpID == EmpID);
                        break;
                    case "hrlist":
                        var hrlist = Common.ModelConvert.ConvertToDefaultType<List<int>>(de.Value);
                        TWhere = TWhere.And(p => !hrlist.Contains(p.EmpID));
                        break;

                }
            }
            #endregion
            if (String.IsNullOrEmpty(condition.SelectOrder)) condition.SelectOrder = "EmpOrder desc";
            return TWhere;
        }
    }

    public class BaseEmployee : EFRepository<DataModel.Models.BaseEmployee>
    {

        /// <summary>
        /// 移除缓存
        /// </summary>
        public void CacheRemove()
        {
            EFCacheRemove("AllBaseEmployee", "BaseEmployee", "BaseEmployeeRole", "BaseMenuPermit", "BaseEmpPermission");
            Common.CacheManager.CacheRemove("BaseQualification");
        }

        /// <summary>
        /// 获取所有用户信息 
        /// 使用缓存
        /// </summary>
        /// 
        public IEnumerable<DataModel.Models.BaseEmployee> AllData
        {
            get
            {
                return GetListFromCahe(s => s.EmpID > 0, 480, "BaseEmployee");
            }
        }

        #region    获取对象信息

        /// <summary>
        /// 获取指定部门中,人员的插入顺序号。
        /// 即比最大大1。
        /// </summary>
        private int GetInsertEmployeeOrder()
        {
            var Max = AllData.OrderByDescending(p => p.EmpOrder);
            int result = 1;
            if (Max != null && Max.Count() > 0)
            {
                result = Max.ElementAt(0).EmpOrder + 1;
            }
            return result;
        }

        /// <summary>
        /// 获取基础数据对象（从缓存获取）
        /// </summary>
        /// <param name="EmpID">自增ID</param>
        /// <returns></returns>
        public DataModel.Models.BaseEmployee GetBaseEmployeeInfo(int EmpID)
        {
            return AllData.SingleOrDefault(p => p.EmpID == EmpID);
        }

        /// <summary>
        /// 获取基础数据对象（从缓存获取）
        /// </summary>
        /// <param name="empLogin">登录名称</param>
        /// <returns></returns>
        public DataModel.Models.BaseEmployee GetBaseEmployeeInfo(string empLogin)
        {
            return AllData.SingleOrDefault(p => p.EmpLogin == empLogin);
        }

        /// <summary>
        /// 根据登录名和密码，验证密码是否正确。
        /// </summary>
        /// <param name="empLogin">登录名称</param>
        /// <param name="empPassword">登录密码。</param>
        /// <returns>返回是否正确。</returns>
        public bool CheckEmpPassword(string empLogin, string empPassword)
        {
            bool result = false;
            var login = Common.PubSql.SQL_Parameters(empLogin);
            var Pwd = Common.PubSql.SQL_Parameters(empPassword);
            var arr = AllData.Where(p => p.EmpIsDeleted.Equals(false) && p.EmpLogin.Equals(login) && p.EmpPassword.Equals(Pwd));
            if (arr != null && arr.Count() > 0)
            {
                result = true;
            }
            return result;
        }

        #region 获取用户有数据的条数
        /// <summary>
        /// 获取用户有数据的条数
        /// </summary>
        /// <param name="EmpLogin"></param>
        /// <returns></returns>
        public int GetCountByUserName(string EmpLogin)
        {
            return this.GetQuery(s => s.EmpLogin == EmpLogin).Count();
        }
        #endregion

        #endregion


        #region    增加、修改、删除、禁用、恢复、排序

        /// <summary>
        /// 新增BaseEmployeeInfo对象
        /// </summary>
        /// <param name="baseEmployeeInfo">BaseEmployeeInfo对象</param>
        /// <param name="roles">角色数组</param>
        /// <returns>成功返回自增ID，不成功返回－1</returns>
        public int InsertBaseEmployeeInfo(DataModel.Models.BaseEmployee baseEmployeeInfo, List<DataModel.Models.BaseEmpPermission> roles)
        {
            int success = 0;
            try
            {
                baseEmployeeInfo.FK_BaseEmpPermission_PermissionEmpID = new List<DataModel.Models.BaseEmpPermission>();
                foreach (var item in roles)
                {
                    baseEmployeeInfo.FK_BaseEmpPermission_PermissionEmpID.Add(item);
                }
                baseEmployeeInfo.EmpOrder = GetInsertEmployeeOrder();
                try
                {
                    var Size = Convert.ToInt32(new Sys.BaseConfig().GetBaseConfigInfo("PageSize").ConfigValue);
                    baseEmployeeInfo.EmpPageSize = Size <= 0 ? 20 : Size;
                }
                catch { }
                Add(baseEmployeeInfo);
                UnitOfWork.SaveChanges();
                success = 1;
            }
            catch(Exception ex) { success = -1; }

            InsertMustMenu(baseEmployeeInfo.EmpID);
            CacheRemove();
            return success;
        }

        /// <summary>
        /// 新建用户必须有的菜单插入
        /// </summary>
        /// <param name="EmpID"></param>
        private void InsertMustMenu(int EmpID)
        {
        }


        /// <summary>
        /// 删除BaseEmployee记录
        /// </summary>
        /// <param name="EmpIDList">自增ID数组</param>
        /// <returns>删除成功返回影响行数</returns>
        public int DeleteBaseEmployeeInfo(int[] EmpIDList)
        {
            var TWhere = Common.Data.Extenstions.QueryBuild<DataModel.Models.BaseEmployee>.True();
            TWhere = TWhere.And(p => EmpIDList.Contains(p.EmpID));
            Delete(TWhere);
            UnitOfWork.SaveChanges();
            return EmpIDList.Count();
        }

        /// <summary>
        /// 修改BaseEmployeeInfo对象
        /// </summary>
        /// <param name="baseEmployeeInfo">BaseEmployeeInfo</param>
        /// <param name="roles">角色数组</param>
        /// <returns>修改成功返回1，不成功返回-1</returns>
        public int UpdateBaseEmployeeInfo(DataModel.Models.BaseEmployee baseEmployeeInfo, List<DataModel.Models.BaseEmpPermission> roles)
        {
            int success = 1;
            if (roles != null)
            {
                baseEmployeeInfo.FK_BaseEmpPermission_PermissionEmpID.Clear();
                foreach (var item in roles)
                {
                    baseEmployeeInfo.FK_BaseEmpPermission_PermissionEmpID.Add(item);
                }
            }
            Edit(baseEmployeeInfo);

            UnitOfWork.SaveChanges();

            CacheRemove();
            return success;
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="EmpID"></param>
        /// <param name="OrderType"></param>
        public void UpdateEmpOrder(int EmpID, int OrderType)
        {
            DataModel.Models.BaseEmployee EmpInfo = Get(EmpID);

            int ToEmpID = GetDataOrder(EmpInfo.EmpDepID, EmpInfo.EmpOrder, OrderType);

            if (ToEmpID == 0) return;

            DataModel.Models.BaseEmployee Model = Get(ToEmpID);
            int ModelOrder = Model.EmpOrder;
            Model.EmpOrder = EmpInfo.EmpOrder;

            EmpInfo.EmpOrder = ModelOrder;

            CacheRemove();
            UnitOfWork.SaveChanges();
        }


        #region 更新用户主题
        /// <summary>
        /// 更新用户主题
        /// </summary>
        /// <param name="EmpID"></param>
        /// <param name="theme"></param>
        /// <param name="menu"></param>
        public DoResult UpdateEmpThemes(int EmpID, string theme, string menu)
        {
            DataModel.Models.BaseEmployee Model = Get(EmpID);
            Model.EmpThemesName = theme;
            Model.EmpMenuType = menu;
            CacheRemove();
            int result = DbContext.SaveChanges();
            return result > 0 ? DoResultHelper.doSuccess(result, "更新成功") : DoResultHelper.doError("更新失败");
        }
        #endregion

        private int GetDataOrder(int depID, int EmpOrder, int OrderType)
        {
            var Max = FillData().Where(p => p.EmpDepID == depID);
            if (Max == null || Max.Count() <= 0) return 0;
            int self = -1;
            int EmpID = 0;

            for (int i = 0; i < Max.Count(); i++)
            {
                if (Max.ElementAt(i).EmpOrder == EmpOrder)
                {
                    self = i;
                    break;
                }
            }
            if (OrderType == 1)
            {
                if (self != 0) EmpID = Convert.ToInt32(Max.ElementAt(self - 1).EmpID);
            }
            else
            {
                if (self != Max.Count() - 1) EmpID = Convert.ToInt32(Max.ElementAt(self + 1).EmpID);
            }
            return EmpID;
        }

        #endregion


        #region   根据不同条件获取不同用户列表


        //取有roleID角色的所有人
        public List<DataModel.Models.BaseEmployee> GetAllBEbyRole(string RoleName)
        {
            DBSql.Sys.BaseData dsBD = new BaseData();
            var dmBD = (from s in dsBD.AllData
                        where s.BaseName == RoleName
                        select s).FirstOrDefault();
            var data = from be in AllData
                       where be.EmpIsDeleted == false && be.FK_BaseEmpPermission_PermissionEmpID.FirstOrDefault().PermissionEmpValue == dmBD.BaseID
                       select be;
            return data.ToList();
        }

        public List<DataModel.Models.BaseEmployee> GetAllBEbyRole(List<int> roleIDs)
        {
            return (from be in AllData
                    where be.EmpIsDeleted == false && be.FK_BaseEmpPermission_PermissionEmpID.FirstOrDefault(m => roleIDs.Contains(m.PermissionEmpValue)) != null
                    select be).ToList();
        }
        public List<DataModel.Models.BaseEmployee> GetAllEmpList()
        {
            return (from be in AllData
                    where be.EmpIsDeleted == false select be).ToList();
        }

        /// <summary>
        /// （使用缓存）填充用户信息扩展表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DataModel.Models.AllBaseEmployee> FillData()
        {
            var list = new List<DataModel.Models.AllBaseEmployee>();

            list = new AllBaseEmployee().GetListFromCahe(s => s.EmpID > 0, 600, "AllBaseEmployee").OrderBy(p => p.DepartmentOrder).OrderBy(p => p.EmpOrder).ToList();

            return list;
        }

        /// <summary>
        /// （使用缓存）获取用户列表 根据筛选条件 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IEnumerable<DataModel.Models.AllBaseEmployee> GetEmpList(Common.SqlPageInfo condition)
        {
            IEnumerable<DataModel.Models.AllBaseEmployee> AllList = FillData();

            #region   筛选条件
            if (!String.IsNullOrEmpty(condition.TextCondtion))
            {
                AllList = AllList.Where(p => p.EmpName.Contains(condition.TextCondtion));
            }
            foreach (DictionaryEntry de in condition.SelectCondtion)
            {
                if (de.Value == null || de.Value.ToString().Trim() == "") continue;

                switch (de.Key.ToString())
                {
                    case "EmpDepID":
                        var EmpDepID = Common.ModelConvert.ConvertToDefaultType<int>(de.Value);
                        if (EmpDepID != 0) AllList = AllList.Where(p => p.EmpDepID == EmpDepID);
                        break;
                    case "EmpIsDeleted":
                        var EmpIsDeleted = Common.ModelConvert.ConvertToDefaultType<bool>(de.Value);
                        AllList = AllList.Where(p => p.EmpIsDeleted == EmpIsDeleted);
                        break;
                    case "EmpID":
                        var EmpID = Common.ModelConvert.ConvertToDefaultType<int>(de.Value);
                        AllList = AllList.Where(p => p.EmpID == EmpID).ToList();
                        break;
                    case "hrlist":
                        var hrlist = Common.ModelConvert.ConvertToDefaultType<List<int>>(de.Value);
                        AllList = AllList.Where(p => !hrlist.Contains(p.EmpID)).ToList();
                        break;
                }
            }
            #endregion

            if (condition.ToPageData)
            {
                return AllList.OrderBy(p => p.DepartmentOrder).ThenBy(p => p.EmpOrder);
            }
            condition.PageTotleRowCount = AllList.Count();
            return AllList.OrderBy(p => p.DepartmentOrder).ThenBy(p => p.EmpOrder).ToPagedList(condition.PageIndex, condition.PageSize);
        }

        /// <summary>
        /// （使用缓存）获取用户列表 根据部门ID（排除已标记删除人）
        /// </summary>
        /// <param name="DepID"></param>
        /// <returns></returns>
        public IEnumerable<DataModel.Models.AllBaseEmployee> GetEmpList(int DepID = 0)
        {
            var AllList = FillData().Where(p => p.EmpIsDeleted == false);
            if (DepID != 0) AllList = AllList.Where(p => p.EmpDepID == DepID);
            return AllList.OrderBy(s => s.DepOrder).ThenBy(s => s.EmpOrder);
        }

        /// <summary>
        /// （使用缓存）获取用户列表 根据人员ID（排除已标记删除人）
        /// </summary>
        /// <param name="empArr"></param>
        /// <returns></returns>
        public IEnumerable<DataModel.Models.AllBaseEmployee> GetEmpList(List<int> empArr)
        {
            var AllList = FillData().Where(p => p.EmpIsDeleted == false);
            AllList = AllList.Where(p => empArr.Contains(p.EmpID));
            return AllList;
        }

        #region   获取名称
        /// <summary>
        /// 获取 <部门名称>用户名称
        /// </summary>
        /// <param name="empIds"></param>
        /// <returns></returns>
        public string GetDepNameEmpName(string empIds)
        {
            empIds = empIds.Trim(',');
            if (empIds.Length != 0)
            {
                var array = empIds.Split(',').Select(a => int.Parse(a));
                var empList = from be in FillData()
                              where array.Contains(be.EmpID)
                              select be;
                return string.Join(";", (from e in empList select "<" + e.DepartmentName + ">" + e.EmpName).ToArray());
            }
            return "";
        }
        /// <summary>
        ///  获取 <,>用户名称
        /// </summary>
        /// <param name="empIds"></param>
        /// <returns></returns>
        public string GetEmpName(string empIds)
        {
            empIds = empIds.Trim(',');
            if (empIds.Length != 0)
            {
                var array = empIds.Trim().Split(',').Select(a => int.Parse(a));
                var empList = from be in FillData()
                              where array.Contains(be.EmpID)
                              select be;
                return string.Join(",", (from e in empList select e.EmpName).ToArray());
            }
            return "";

        }
        /// <summary>
        /// 获取 <部门名称>用户名称
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string GetDepNameEmpName(List<int> list)
        {
            var empList = from be in FillData()
                          where list.Contains(be.EmpID)
                          select be;
            return string.Join(";", (from e in empList
                                     select "<" + e.DepartmentName + ">" + e.EmpName).ToArray());

        }


        /// <summary>
        /// 取专业名称_人员名称
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string getSpecNameName(List<string> list)
        {
            List<string> ListSpeName = new List<string>();
            DBSql.Sys.BaseData dsBD = new BaseData();
            foreach (var item in list)
            {
                string[] Arr = item.Split('-');
                var dmSpecBE = dsBD.GetBaseDataInfo(Convert.ToInt32(Arr[0]));
                var dmEmp = GetBaseEmployeeInfo(Convert.ToInt32(Arr[1]));
                string EName = "";
                if (dmEmp != null)
                {
                    EName = dmEmp.EmpName;
                }
                ListSpeName.Add("<" + dmSpecBE.BaseName + ">" + EName);
            }
            return string.Join(";", ListSpeName.ToArray());
        }

        /// <summary>
        /// 获取用户Id
        /// </summary>
        /// <param name="EmpName"></param>
        /// <returns></returns>
        public string GetEmpIds(string EmpName)
        {
            EmpName = EmpName.Trim(',');
            if (EmpName.Length != 0)
            {
                var array = EmpName.Split(',');
                var empIds = from be in FillData()
                             where array.Contains(be.EmpName)
                             select be.EmpID;
                return string.Join(",", empIds.ToArray());
            }
            return "";
        }

        #endregion

        public IEnumerable<DataModel.Models.BaseEmployee> GetListFromCache(Func<DataModel.Models.BaseEmployee, bool> predicate)
        {
            return AllData.Where(predicate);
        }

        #endregion


        public void SaveProfileSetting(DataModel.Models.BaseEmployee baseEmployee)
        {
            this.DbContext.Set<DataModel.Models.BaseEmployee>().Where(m => m.EmpID == baseEmployee.EmpID).Update(m => new DataModel.Models.BaseEmployee() { EmpTitle = baseEmployee.EmpTitle, EmpFJNum = baseEmployee.EmpFJNum, EmpComMail = baseEmployee.EmpComMail, EmpBirthDate = baseEmployee.EmpBirthDate, EmpNote = baseEmployee.EmpNote, EmpTel = baseEmployee.EmpTel, EmpTelNX = baseEmployee.EmpTelNX, EmpHead = baseEmployee.EmpHead, EmpTelWX = baseEmployee.EmpTelWX, EmpOaMail = baseEmployee.EmpOaMail, EmpPageSize = baseEmployee.EmpPageSize, EmpIPAddress = baseEmployee.EmpIPAddress, EmpMacAddress = baseEmployee.EmpMacAddress });
            this.DbContext.SaveChanges();
            CacheRemove();
        }

        public int SyncEmployee()
        {
            string sql = "insert into HREmployee(EmpName,EmpStatusID,DepID,SysEmpId,CompanyID) select EmpName,1499 as EmpStatusID,EmpDepID as DepID,EmpId as SysEmpId,CompanyID from BaseEmployee where EmpID not in (select SysEmpId from HREmployee) and EmpID>1";

            try
            {
                int result = DBExecute.ExecuteNonQuery(sql);
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// 用户session名称
        /// </summary>
        private string userSessionName = "Employee";

        /// <summary>
        /// 保存用户EmpSession
        /// </summary>
        /// <param name="emp">当前登录的用户信息</param>
        /// <param name="agentEmp">当前登录的代理用户信息</param>
        public DataModel.EmpSession SaveSessionInfo(HttpSessionStateBase Session, DataModel.Models.BaseEmployee emp, DataModel.Models.BaseEmployee agentEmp = null)
        {
            if (emp == null) return null;
            DataModel.EmpSession empSession = new DataModel.EmpSession()
            {
                EmpID = emp.EmpID,
                EmpName = emp.EmpName,
                EmpDepID = emp.EmpDepID,
                EmpDepName = emp.EmpDepName,
                EmpMenuType = emp.EmpMenuType,
                EmpMacAddress = emp.EmpMacAddress,
                EmpThemesName = emp.EmpThemesName,
                EmpPageSize = emp.EmpPageSize,
                EmpDisk = emp.EmpDisk,
                LoginIP = GetIP()
            };
            if (agentEmp != null)
            {
                empSession.AgenDepID = agentEmp.EmpDepID;
                empSession.AgenDepName = agentEmp.EmpDepName;
                empSession.AgenEmpID = agentEmp.EmpID;
                empSession.AgenEmpName = agentEmp.EmpName;
            }
            else
            {
                empSession.AgenTypeID = 0;
            }
            Session[userSessionName] = empSession;
            string thems = string.IsNullOrEmpty(emp.EmpThemesName) ? "skin-bluelight" : "skin-" + emp.EmpThemesName;
            Session.Add("themes", thems);
            return empSession;
        }

        /// <summary>
        /// 保存用户EmpSession
        /// </summary>
        /// <param name="emp">当前登录的用户信息</param>
        /// <param name="agentEmp">当前登录的代理用户信息</param>
        public DataModel.EmpSession SaveSessionInfo(HttpSessionStateBase Session, DataModel.Models.BaseEmployee emp, DataModel.Models.BaseEmpAgen agentEmp)
        {
            if (emp == null) return null;
            DataModel.EmpSession empSession = new DataModel.EmpSession()
            {
                EmpID = emp.EmpID,
                EmpName = emp.EmpName,
                EmpDepID = emp.EmpDepID,
                EmpDepName = emp.EmpDepName,
                EmpMenuType = emp.EmpMenuType,
                EmpMacAddress = emp.EmpMacAddress,
                EmpThemesName = emp.EmpThemesName,
                EmpPageSize = emp.EmpPageSize,
                EmpDisk = emp.EmpDisk,
                LoginIP = GetIP()
            };
            if (agentEmp != null)
            {
                var empInfo = new DBSql.Sys.BaseEmployee().Get(agentEmp.ToEmpID);
                empSession.AgenDepID = empInfo.EmpDepID;
                empSession.AgenDepName = empInfo.EmpDepName;
                empSession.AgenEmpID = empInfo.EmpID;
                empSession.AgenEmpName = empInfo.EmpName;
                empSession.AgenTypeID = agentEmp.AgenType;
                empSession.AgenFlow = agentEmp.AgenFlowRefTable;
                empSession.AgenMenu = agentEmp.AgenMenu;
            }
            else
            {
                empSession.AgenTypeID = 0;
            }
            Session[userSessionName] = empSession;
            string thems = string.IsNullOrEmpty(emp.EmpThemesName) ? "skin-bluelight" : "skin-" + emp.EmpThemesName;
            Session.Add("themes", thems);
            return empSession;
        }

        /// <summary>
        /// 获取IP
        /// </summary>
        /// <returns></returns>
        public string GetIP()
        {
            string ip = string.Empty;
            if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"]))
                ip = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]);
            if (string.IsNullOrEmpty(ip))
                ip = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            return ip == "::1" ? "127.0.0.1" : ip;
        }
    }
}
