using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Data;
using Common.Data.Extenstions;
using JQ.Util;
using System.Linq.Expressions;
using System.Reflection;
using EntityFramework.Extensions;

namespace DBSql.Sys
{
    public class MenuPermit : EFRepository<DataModel.Models.View_MenuPermit> { }

    public class BaseMenu : EFRepository<DataModel.Models.BaseMenu>
    {
        #region    缓存获取数据
        /// <summary>
        /// 移除缓存
        /// </summary>
        public void CacheRemove()
        {
            EFCacheRemove("BaseMenu", "BaseMenuPermit");
        }

        /// <summary>
        /// 获取所有用户信息 
        /// 使用缓存
        /// </summary>
        public IEnumerable<DataModel.Models.BaseMenu> AllData
        {
            get
            {
                return GetListFromCahe(s => s.MenuID > 0, 480, "BaseMenu");
            }
        }

        #endregion

        #region    获取对象信息

        /// <summary>
        /// （从缓存获取）得到BaseMenuInfo对象
        /// </summary>
        /// <param name="MenuID">MenuID自增ID</param>
        /// <returns>BaseMenuInfo对象</returns>
        public DataModel.Models.BaseMenu GetBaseMenuInfo(int MenuID)
        {
            return AllData.SingleOrDefault(p => p.MenuID == MenuID);
        }

        /// <summary>
        /// （从缓存获取）得到BaseMenuInfo对象
        /// </summary>
        /// <param name="MenuOrder">MenuOrder</param>
        /// <returns>BaseMenuInfo对象</returns>
        public DataModel.Models.BaseMenu GetBaseMenuInfo(string MenuOrder)
        {
            return AllData.SingleOrDefault(p => p.MenuOrder == MenuOrder);
        }

        #endregion

        #region    获取菜单列表信息（角色、权限、二级权限等）

        /// <summary>
        /// 获取所有用户信息 
        /// 使用缓存
        /// </summary>
        public List<DataModel.Models.View_MenuPermit> AllDataByPermit
        {
            get
            {
                return new MenuPermit().GetListFromCahe(s => s.EmpID > 0, 480, "BaseMenuPermit").ToList();
            }
        }

        /// <summary>
        /// （使用缓存）
        /// </summary>
        private List<DataModel.Models.View_MenuPermit> GetBaseMenuPermit()
        {
            var query = new MenuPermit().GetQuery().Distinct().ToList();
            return query;
        }

        /// <summary>
        /// （使用缓存）用户有权限所有的菜单
        /// </summary>
        /// <param name="empID">人员ID</param>
        /// <returns></returns>
        public IEnumerable<DataModel.Models.BaseMenu> GetBaseMenuListByPermit(int empID)
        {
            var query = (from q1 in AllData
                         join q2 in AllDataByPermit on q1.MenuID equals q2.MenuID into q3
                         from q2 in q3.DefaultIfEmpty()
                         where q1 != null && q2 != null
                         where q2.EmpID == empID && q2.Permit>=16   //至少个人权限才能查看
                         select new DataModel.Models.BaseMenu()
                         {
                             MenuID = q1.MenuID,
                             MenuName = q1.MenuName,
                             MenuNameEng = q1.MenuNameEng,
                             MenuOrder = q1.MenuOrder,
                             MenuNote = q1.MenuNote,
                             MenuCommand = q1.MenuCommand,
                             MenuHelp = q1.MenuHelp,
                             MenuImageUrl = q1.MenuImageUrl,
                             MenuIsMust = q1.MenuIsMust,
                             MenuIsSecond = q1.MenuIsSecond,
                             ParentID = q1.ParentID,
                             MenuPermissions = q2.Permit,
                         });

            return query.OrderBy(s => s.MenuOrder).Distinct();
        }

        /// <summary>
        /// （使用缓存）判断一个人是否有EngName权限
        /// </summary>
        /// <param name="EngName"></param>
        /// <param name="empID"></param>
        /// <returns></returns>
        public bool getPermit(int empID, string engName)
        {
            var query = GetBaseMenuListByPermit(empID);
            return query.Where(p => p.MenuNameEng == engName).Count() == 1;
        }

        /// <summary>
        /// （使用缓存）判断一个人是否有EngName权限，只需要满足其中一个就返回true
        /// </summary>
        /// <param name="empID"></param>
        /// <param name="engNames"></param>
        /// <returns></returns>
        public bool getPermit(int empID, string[] engNames)
        {
            return GetBaseMenuListByPermit(empID).FirstOrDefault(m => engNames.Contains(m.MenuNameEng)) != null;
        }

        /// <summary>
        /// （使用缓存）判断一个人是否有EngName权限，只需要满足其中一个就返回
        /// </summary>
        /// <param name="empID"></param>
        /// <param name="engNames"></param>
        /// <returns></returns>
        public DataModel.Models.BaseMenu getPermitMenu(int empID, params string[] engNames)
        {
            return GetBaseMenuListByPermit(empID).FirstOrDefault(m => engNames.Contains(m.MenuNameEng));
        }

        /// <summary>
        /// （使用缓存）根据某个菜单的二级权限,得到所有拥有此权限的人列表
        /// </summary>
        /// <param name="menuEng">菜单英文名</param>
        /// <returns></returns>
        public IEnumerable<int> GetPersonsHashRigth(string menuEngName)
        {
            var query = (from q1 in AllData
                         join q2 in AllDataByPermit on q1.MenuID equals q2.MenuID into q3
                         from q2 in q3.DefaultIfEmpty()
                         where q1 != null && q2 != null
                         where q1.MenuNameEng == menuEngName
                         select q2.EmpID);
            return query;
        }

        #endregion

        #region    上移、下移菜单
        /// <summary>
        /// 菜单数据排序 
        /// </summary>
        /// <param name="BaseID">基础数据ID</param>
        /// <param name="OrderType">操作类别(上移、下移)</param>
        public void OrderBaseData(int ID, int OrderType)
        {
            var FromModel = Get(ID);
            //获取(上移、下移)对应记录
            int baseID = GetDataOrder(FromModel.MenuOrder, OrderType);
            if (baseID == 0) return;
            var ToModel = Get(baseID);
            string ToOrder = ToModel.MenuOrder;
            string FromOrder = FromModel.MenuOrder;
            ToModel.MenuOrder = FromOrder;
            FromModel.MenuOrder = ToOrder;

            #region 更新下级节点排序信息
            var query0 = GetQuery().Where(p => p.MenuOrder.StartsWith(FromOrder + "_"));
            var query1 = GetQuery().Where(p => p.MenuOrder.StartsWith(ToOrder + "_"));


            foreach (var child in query0)
            {
                child.MenuOrder = ToOrder + child.MenuOrder.Substring(ToOrder.Length);
            }

            foreach (var child in query1)
            {
                child.MenuOrder = FromOrder + child.MenuOrder.Substring(FromOrder.Length);
            }
            #endregion

            CacheRemove();
            UnitOfWork.SaveChanges();
        }

        /// <summary>
        /// 取得排序 上移、下移 自增ID
        /// </summary>
        /// <param name="BaseOrder"></param>
        /// <param name="OrderType"></param>
        /// <returns></returns>
        private int GetDataOrder(string Order, int OrderType)
        {
            string ParentPrder = Order.Substring(0, Order.Length - 3);

            var Max = AllData.Where(p => p.MenuOrder.StartsWith(ParentPrder + "_") && p.MenuOrder.Length == (ParentPrder.Length + 3)).OrderBy(p => p.MenuOrder);
            if (Max == null || Max.Count() <= 0) return 0;

            int self = -1;

            int MenuID = 0;

            for (int i = 0; i < Max.Count(); i++)
            {
                if (Max.ElementAt(i).MenuOrder == Order)
                {
                    self = i;
                    break;
                }
            }
            if (OrderType == 1)
            {
                if (self != 0) MenuID = Convert.ToInt32(Max.ElementAt(self - 1).MenuID);
            }
            else
            {
                if (self != Max.Count() - 1) MenuID = Convert.ToInt32(Max.ElementAt(self + 1).MenuID);
            }
            return MenuID;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="baseData"></param>
        public int Insert(DataModel.Models.BaseMenu model)
        {
            model.MenuOrder = GetInsertOrder(model.MenuOrder);
            Add(model);
            CacheRemove();
            UnitOfWork.SaveChanges();
            return model.MenuID;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        public void Update(DataModel.Models.BaseMenu model)
        {
            Edit(model);
            CacheRemove();
            UnitOfWork.SaveChanges();
        }

        private string GetInsertOrder(string MenuOrder)
        {
            var Max = AllData.Where(p => p.MenuOrder.StartsWith(MenuOrder + "_") && p.MenuOrder.Length == (MenuOrder.Length + 3)).OrderByDescending(p => p.MenuOrder).ToList();
            if (Max == null || Max.Count <= 0) return MenuOrder + "_01";
            string MaxOrder = Max[0].MenuOrder;
            int MaxOrderNumber = Convert.ToInt32(MaxOrder.Substring(MaxOrder.Length - 2));
            MaxOrderNumber++;
            return string.Format("{0}_{1}", MenuOrder, MaxOrderNumber.ToString("00"));
        }
        #endregion

        #region 得到树级菜单 wirte by 吴俊鹏 2016-05-18
        /// <summary>
        /// 得到树级菜单 wirte by 吴俊鹏 2016-05-18
        /// </summary>
        /// <param name="entitys">根级菜单集合</param>
        /// <param name="leven">层级表示从第几级开始显示</param>
        /// <param name="isIcon">是否需要显示icon只有菜单导航需要，列表树中不能显示会和easyui起冲突</param>
        /// <returns></returns>
        public List<object> treeJson(IEnumerable<DataModel.Models.BaseMenu> entitys, int leven, bool isIcon = false)
        {
            List<object> list = new List<object>();
            if (entitys.isNotEmpty())
            {
                foreach (var item in entitys)
                {
                    //获取子类 递归
                    IList<object> clist = treeJson(entitys.Where(m =>
                        (m.MenuOrder != item.MenuOrder) && StringUtil.startsWithIgnoreCase(m.MenuOrder, item.MenuOrder)), leven + 1, isIcon);
                    if ((item.MenuOrder.Replace("_", "").Length / 2) == leven)
                    {
                        if (isIcon)
                        {
                            list.Add(new
                            {
                                id = item.MenuID,
                                parentID = item.ParentID,
                                orderCode = item.MenuOrder,
                                isMust = item.MenuIsMust,
                                text = item.MenuName,
                                attributes = item.MenuCommand,
                                isSecond = item.MenuIsSecond,
                                iconCls = item.MenuImageUrl,
                                MenuImageUrl = item.MenuImageUrl,
                                children = clist
                            });
                        }
                        else
                        {
                            list.Add(new
                            {
                                id = item.MenuID,
                                parentID = item.ParentID,
                                orderCode = item.MenuOrder,
                                isMust = item.MenuIsMust,
                                text = item.MenuName,
                                attributes = item.MenuCommand,
                                isSecond = item.MenuIsSecond,
                                MenuImageUrl = item.MenuImageUrl,
                                children = clist
                            });
                        }
                    }
                }
            }
            if (list.Count > 0)
            {
                return list;
            }
            return null;
        }

        #endregion

        public List<dynamic> GetShortcutList(int empID)
        {
            return (from t in AllDataByPermit.Where(m => m.EmpID == empID)
                    join t1 in AllData on t.MenuID equals t1.MenuID
                    join t2 in this.DbContext.Set<DataModel.Models.Shortcut>() on t1.MenuID equals t2.MenuID into emps
                    from temp in emps.Where(m => m.EmpID == empID).DefaultIfEmpty()
                    where t1.MenuIsMust
                    orderby t1.MenuOrder
                    select new { t1.MenuID, t1.MenuImageUrl, t1.MenuName, IsChecked = (temp == null ? 0 : 1) }).ToList<dynamic>();
        }

        public void SaveShortcut(List<int> ids, int empID)
        {
            var shortcutDB = this.DbContext.Set<DataModel.Models.Shortcut>();
            var shortcuts = shortcutDB.Where(m => m.EmpID == empID).ToList();
            using (var tran = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var id in ids)
                    {
                        if (shortcuts.FirstOrDefault(m => m.MenuID == id) == null)
                        {
                            //需要添加
                            var shortcut = new DataModel.Models.Shortcut()
                            {
                                CreationTime = DateTime.Now,
                                EmpID = empID,
                                MenuID = id,
                                Note = "",
                                RefID = 0
                            };
                            shortcutDB.Add(shortcut);
                        }
                    }
                    foreach (var shortcut in shortcuts)
                    {
                        if (!ids.Contains(shortcut.MenuID))
                        {
                            //需要删除
                            shortcutDB.Remove(shortcut);
                        }
                    }
                    this.DbContext.SaveChanges();
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                }
            }
        }

        public List<dynamic> GetShortcuts(int empID)
        {
            return (from t in AllDataByPermit.Where(m => m.EmpID == empID)
                    join t1 in AllData on t.MenuID equals t1.MenuID
                    join t2 in this.DbContext.Set<DataModel.Models.Shortcut>() on t1.MenuID equals t2.MenuID
                    where t1.MenuIsMust && t2.EmpID == empID
                    orderby t1.MenuOrder
                    select new { t1.MenuID, t1.MenuImageUrl, t1.MenuName, t1.MenuCommand }).ToList<dynamic>();
        }
        
        /// <summary>
        /// 根据菜单名查找
        /// </summary>
        /// <param name="eng"></param>
        /// <returns></returns>
        public DataModel.Models.BaseMenu GetModelByEng(string eng)
        {
            string sql = string.Format("select MenuID from BaseMenu where MenuNameEng='{0}'",eng);
            var obj= DAL.DBExecute.ExecuteScalar(sql);
            if (obj == null)
            {
                return null;
            }
           return GetQuery(x => x.MenuID == (int)obj).First();
        }
    }
}
