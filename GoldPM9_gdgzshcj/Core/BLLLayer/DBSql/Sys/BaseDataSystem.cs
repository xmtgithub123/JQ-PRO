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

namespace DBSql.Sys
{
    public class BaseDataSystem : EFRepository<DataModel.Models.BaseDataSystem>
    {
        /// <summary>
        /// 获取所有基础数据
        /// 使用缓存
        /// </summary>
        public IEnumerable<DataModel.Models.BaseDataSystem> AllData
        {
            get
            {
                return GetListFromCahe(s => s.BaseID > 0, 480, "BaseDataSystem"); 
            }
        }

        /// <summary>
        /// 获取基础数据对象（从缓存获取）
        /// </summary>
        /// <param name="baseID">自增ID</param>
        /// <returns></returns>
        public DataModel.Models.BaseDataSystem GetBaseDataInfo(int baseID)
        {
            return AllData.SingleOrDefault(p => p.BaseID == baseID);
        }

        /// <summary>
        /// 获取基础数据对象（从缓存获取）
        /// </summary>
        /// <param name="baseorder">序号</param>
        /// <returns></returns>
        public DataModel.Models.BaseDataSystem GetBaseDataInfoByOrder(string baseorder)
        {
            return AllData.SingleOrDefault(p => p.BaseOrder == baseorder);
        }

        /// <summary>
        /// 获取基础数据对象（从缓存获取）
        /// </summary>
        /// <param name="engName">英文名</param>
        /// <returns></returns>
        public DataModel.Models.BaseDataSystem GetBaseDataInfoByEngName(string engName)
        {
            return AllData.SingleOrDefault(p => p.BaseNameEng == engName);
        }

        /// <summary>
        /// 获取基础数据对象列表（从缓存获取）
        /// </summary>
        /// <param name="engName">英文名</param>
        /// <returns></returns>
        public IEnumerable<DataModel.Models.BaseDataSystem> GetDataSetByEngName(string engName)
        {
            DataModel.Models.BaseDataSystem parent = GetBaseDataInfoByEngName(engName);
            return AllData.Where(p => p.BaseOrder.StartsWith(parent.BaseOrder + "_")) ;
        }

        public List<object> getTreeData(DataModel.Models.BaseDataSystem model,string isfilter = "0")
        {
            var list = AllData.Where(p => p.BaseOrder.StartsWith(model.BaseOrder + "_") && p.BaseIsDeleted == false && p.BaseOrder.Length == (model.BaseOrder.Length + 3 + 1)).ToList();
            var objList = new List<object>();
            if (list.isNotEmpty())
            {
                foreach (var item in list)
                {
                    var childlist = getTreeData(item);
                    if (isfilter == "1"&& item.BaseAtt3=="1")
                    {
                        continue;
                    }
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
    }
}
