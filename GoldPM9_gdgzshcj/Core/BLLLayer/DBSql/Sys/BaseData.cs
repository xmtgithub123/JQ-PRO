/********
 * 编写日期：2012-06-15
 * 编写人：梅鹏
 * 实现功能：系统模块 
 ********/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Data;
using Common.Data.Extenstions;
using JQ.Util;
using DAL;

namespace DBSql.Sys
{
    public class BaseData : EFRepository<DataModel.Models.BaseData>
    {
        /// <summary>
        /// 移除缓存
        /// </summary>
        public void CacheRemove()
        {
            EFCacheRemove("BaseData", "BaseEmployee", "BaseEmployeeRole", "BaseMenuPermit", "BaseQualification");
        }

        /// <summary>
        /// 新增基础数据信息
        /// </summary>
        /// <param name="baseData"></param>
        public int InsertBaseData(DataModel.Models.BaseData baseData)
        {
            baseData.BaseOrder = GetInsertBaseDataOrder(baseData.BaseOrder);
            Add(baseData);
            CacheRemove();
            UnitOfWork.SaveChanges();
            return baseData.BaseID;

        }
        /// <summary>
        /// 获取指定基础数据中,插入顺序号。
        /// 即比最大大1。
        /// </summary>
        /// <param name="BaseOrder"></param>
        /// <returns></returns>
        private string GetInsertBaseDataOrder(string BaseOrder)
        {
            List<DataModel.Models.BaseData> Max = AllData.Where(p => p.BaseOrder.StartsWith(BaseOrder + "_") && p.BaseOrder.Length == (BaseOrder.Length + 4) && p.BaseIsDeleted == false).OrderByDescending(p => p.BaseOrder).ToList();
            if (Max == null || Max.Count <= 0) return BaseOrder + "_001";
            string MaxOrder = Max[0].BaseOrder;
            int MaxOrderNumber = Convert.ToInt32(MaxOrder.Substring(MaxOrder.Length - 3));
            MaxOrderNumber++;
            return string.Format("{0}_{1}", BaseOrder, MaxOrderNumber.ToString("000"));
        }
        /// <summary>
        /// 删除基础数据信息
        /// </summary>
        /// <param name="baseID"></param>
        public void DeleteBaseData(int baseID)
        {
            var Model = Get(baseID);
            if (Model == null) return;

            if (Model.BaseIsFixed)
            {
                Model.BaseIsDeleted = true;
            }
            else
            {
                Delete(Model);
            }
            CacheRemove();
            UnitOfWork.SaveChanges();
        }

        /// <summary>
        /// 修改基础数据信息
        /// </summary>
        /// <param name="baseData"></param>
        public void UpdateBaseData(DataModel.Models.BaseData baseData)
        {

            Edit(baseData);
            CacheRemove();
            UnitOfWork.SaveChanges();
        }

        /// <summary>
        /// 获取所有基础数据
        /// 使用缓存
        /// </summary>
        public IEnumerable<DataModel.Models.BaseData> AllData
        {
            get
            {
                return GetListFromCahe(s => s.BaseID > 0, 480, "BaseData");
            }
        }

        /// <summary>
        /// 获取基础数据对象（从缓存获取）
        /// </summary>
        /// <param name="baseID">自增ID</param>
        /// <returns></returns>
        public DataModel.Models.BaseData GetBaseDataInfo(int baseID)
        {
            return AllData.SingleOrDefault(p => p.BaseID == baseID);
        }

        /// <summary>
        /// 获取基础数据对象（从缓存获取）
        /// </summary>
        /// <param name="baseorder">序号</param>
        /// <returns></returns>
        public DataModel.Models.BaseData GetBaseDataInfoByOrder(string baseorder)
        {
            return AllData.SingleOrDefault(p => p.BaseOrder == baseorder);
        }

        /// <summary>
        /// 获取基础数据对象（从缓存获取）
        /// </summary>
        /// <param name="engName">英文名</param>
        /// <returns></returns>
        public DataModel.Models.BaseData GetBaseDataInfoByEngName(string engName)
        {
            return AllData.SingleOrDefault(p => p.BaseNameEng == engName);
        }

        /// <summary>
        /// 获取基础数据对象列表（从缓存获取）
        /// </summary>
        /// <param name="engName">英文名</param>
        /// <returns></returns>
        public IEnumerable<DataModel.Models.BaseData> GetDataSetByEngName(string engName)
        {
            DataModel.Models.BaseData parent = GetBaseDataInfoByEngName(engName);
            return AllData.Where(p => p.BaseOrder.StartsWith(parent.BaseOrder + "_") && p.BaseIsDeleted == false).OrderBy(s => s.BaseOrder);
        }

        /// <summary>
        /// 基础数据排序 
        /// </summary>
        /// <param name="BaseID">基础数据ID</param>
        /// <param name="OrderType">操作类别(上移、下移)</param>
        public void OrderBaseData(int BaseID, int OrderType)
        {
            var FromModel = Get(BaseID);
            //获取(上移、下移)对应记录
            int baseID = GetDataOrder(FromModel.BaseOrder, OrderType);
            if (baseID == 0) return;
            var ToModel = Get(baseID);
            string ToOrder = ToModel.BaseOrder;
            string FromOrder = FromModel.BaseOrder;
            ToModel.BaseOrder = FromOrder;
            FromModel.BaseOrder = ToOrder;

            #region 更新下级节点排序信息
            var query0 = GetQuery().Where(p => p.BaseOrder.StartsWith(FromOrder + "_"));
            var query1 = GetQuery().Where(p => p.BaseOrder.StartsWith(ToOrder + "_"));


            foreach (var child in query0)
            {
                child.BaseOrder = ToOrder + child.BaseOrder.Substring(ToOrder.Length);
            }

            foreach (var child in query1)
            {
                child.BaseOrder = FromOrder + child.BaseOrder.Substring(FromOrder.Length);
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
        private int GetDataOrder(string BaseOrder, int OrderType)
        {
            string ParentPrder = BaseOrder.Substring(0, BaseOrder.Length - 4);

            var Max = AllData.Where(p => p.BaseOrder.StartsWith(ParentPrder + "_") && p.BaseOrder.Length == (ParentPrder.Length + 4) && p.BaseIsDeleted == false).OrderBy(p => p.BaseOrder);
            if (Max == null || Max.Count() <= 0) return 0;

            int self = -1;

            int baseID = 0;

            for (int i = 0; i < Max.Count(); i++)
            {
                if (Max.ElementAt(i).BaseOrder == BaseOrder)
                {
                    self = i;
                    break;
                }
            }
            if (OrderType == 1)
            {
                if (self != 0) baseID = Convert.ToInt32(Max.ElementAt(self - 1).BaseID);
            }
            else
            {
                if (self != Max.Count() - 1) baseID = Convert.ToInt32(Max.ElementAt(self + 1).BaseID);
            }
            return baseID;
        }


        #region 获取树的数据只包含顶级2层wirte by 吴俊鹏
        /// <summary>
        /// 获取树的数据只包含顶级2层wirte by 吴俊鹏
        /// </summary>
        public List<object> getTreeDataForTop2()
        {
            var list = AllData.Where(p => p.BaseNameEng != "" && p.BaseAtt1 != "").OrderBy(s => s.BaseOrder).ToList();
            List<string> space = new List<string>();
            List<object> ol = new List<object>();
            foreach (var item in list)
            {
                if (!space.Contains(item.BaseAtt1)) space.Add(item.BaseAtt1);
            }
            foreach (var item in space)
            {
                var child = list.Where(p => p.BaseAtt1 == item && p.BaseOrder.Length == 3).ToList();
                if (child.isNotEmpty())
                {
                    ol.Add(new
                    {
                        id = item,
                        text = item,
                        children = from r in child
                                   select new
                                   {
                                       id = r.BaseID,
                                       text = r.BaseName,
                                       attributes = r.BaseNameEng,
                                       state = "open"
                                   }
                    });
                }
            }
            return ol;
        }
        #endregion

        #region 获取树数据递归方式 wirte by 吴俊鹏
        /// <summary>
        /// 获取树数据递归方式 wirte by 吴俊鹏
        /// </summary>
        /// <param name="BaseID"></param>
        /// <returns></returns>
        public List<object> getTreeData(DataModel.Models.BaseData model, int filterLength = 0, string typeId = "")
        {
            //var TWhere = QueryBuild<DataModel.Models.BaseData>.True();

            //TWhere = TWhere.And(p => p.BaseOrder.StartsWith(model.BaseOrder + "_") && p.BaseIsDeleted == false && p.BaseOrder.Length == (model.BaseOrder.Length + 3 + 1));


            var list = AllData.Where(p => p.BaseOrder.StartsWith(model.BaseOrder + "_") && p.BaseIsDeleted == false && p.BaseOrder.Length == (model.BaseOrder.Length + 3 + 1)).OrderBy(s => s.BaseOrder).ToList();

            if (filterLength > 0)
            {
                list = list.Where(p => p.BaseOrder.Length == 11).ToList();
            }
            if (typeId != "")
            {
                list = list.Where(p => p.BaseAtt3 == typeId).ToList();
            }
            var objList = new List<object>();
            if (list.isNotEmpty())
            {
                foreach (var item in list)
                {
                    var childlist = getTreeData(item);
                    objList.Add(new
                    {
                        id = item.BaseID,
                        text = item.BaseName,
                        BaseOrder = item.BaseOrder,
                        BaseAtt1 = item.BaseAtt1,
                        BaseAtt2 = item.BaseAtt2,
                        BaseAtt3 = item.BaseAtt3,
                        BaseAtt4 = item.BaseAtt4,
                        BaseAtt5 = item.BaseAtt5,
                        BaseNameEng = item.BaseNameEng,
                        children = childlist
                    });
                }
            }
            return objList;
        }

        public List<object> getTreeData1(DataModel.Models.BaseData model, int filterLength = 0, string typeId = "", string isAll = "0", string allVal = "0", string allText = "全部")
        {
            //var TWhere = QueryBuild<DataModel.Models.BaseData>.True();

            //TWhere = TWhere.And(p => p.BaseOrder.StartsWith(model.BaseOrder + "_") && p.BaseIsDeleted == false && p.BaseOrder.Length == (model.BaseOrder.Length + 3 + 1));


            var list = AllData.Where(p => p.BaseOrder.StartsWith(model.BaseOrder + "_") && p.BaseIsDeleted == false && p.BaseOrder.Length == (model.BaseOrder.Length + 3 + 1)).OrderBy(s => s.BaseOrder).ToList();

            if (filterLength > 0)
            {
                list = list.Where(p => p.BaseOrder.Length == 11).ToList();
            }
            if (typeId != "")
            {
                list = list.Where(p => p.BaseAtt3 == typeId).ToList();
            }
            var objList = new List<object>();
            if (list.isNotEmpty())
            {
                foreach (var item in list)
                {
                    var childlist = getTreeData(item);
                    objList.Add(new
                    {
                        id = item.BaseID.ToString(),
                        text = item.BaseName,
                        BaseOrder = item.BaseOrder,
                        BaseAtt1 = item.BaseAtt1,
                        BaseAtt2 = item.BaseAtt2,
                        BaseAtt3 = item.BaseAtt3,
                        BaseAtt4 = item.BaseAtt4,
                        BaseAtt5 = item.BaseAtt5,
                        BaseNameEng = item.BaseNameEng,
                        children = childlist
                    });
                }
                //全部选项
                if (isAll == "1")
                {
                    if (model.BaseOrder.Length == 3)
                    {
                        DataModel.Models.BaseData modNull = new DataModel.Models.BaseData();
                        modNull.BaseID = 0;
                        modNull.BaseOrder = "000";

                        var childlist = getTreeData(modNull);
                        objList.Insert(0, new
                        {
                            id = allVal,
                            text = allText,
                            BaseOrder = model.BaseOrder + "_000",
                            BaseAtt1 = string.Empty,
                            BaseAtt2 = string.Empty,
                            BaseAtt3 = string.Empty,
                            BaseAtt4 = string.Empty,
                            BaseAtt5 = string.Empty,
                            BaseNameEng = string.Empty,
                            children = childlist
                        });
                    }
                }
            }
            return objList;
        }
        #endregion

        #region 获取所有的部门数据wirte by 吴俊鹏
        /// <summary>
        /// 获取所有的部门数据
        /// </summary>
        /// <returns></returns>
        public List<DataModel.Models.BaseData> getDeparmentList()
        {
            var model = GetBaseDataInfoByEngName("department");
            return AllData.Where(p => p.BaseOrder.StartsWith(model.BaseOrder + "_") && p.BaseIsDeleted == false).OrderBy(s => s.BaseOrder).ToList();
        }
        #endregion

        public string getBaseNameByIds(string Ids)
        {
            string info = "";
            if (Ids.Length > 0)
            {
                object obj = DBExecute.ExecuteScalar(string.Format("SELECT  BaseName + ',' FROM BaseData  WHERE  BaseID IN ({0}) FOR XML PATH('') ", Ids));
                if (DBNull.Value != obj)
                {
                    return obj.ToString();
                }
            }
            return info;
        }

        public DataModel.Models.BaseData Get(int BaseID, string BaseEngName)
        {
            DataModel.Models.BaseData dmBase = FirstOrDefault(p => p.BaseNameEng == BaseEngName);
            if (dmBase == null)
            {
                return null;
            }
            List<DataModel.Models.BaseData> list = GetList(p => p.BaseOrder.StartsWith(dmBase.BaseOrder + "_")).ToList();
            return list.FirstOrDefault(p => p.BaseID == BaseID);

        }
    }
}
