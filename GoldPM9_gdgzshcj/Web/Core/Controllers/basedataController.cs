using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JQ.Web;
using System.ComponentModel;
using JQ.Util;

namespace Core.Controllers
{
    [Description("基础数据管理")]
    public class basedataController : CoreController
    {
        private DBSql.Sys.BaseData basedata = new DBSql.Sys.BaseData();


        #region 系统字典主界面 write by 吴俊鹏
        /// <summary>
        /// 系统字典主界面 write by 吴俊鹏
        /// </summary>
        /// <returns></returns>
        [Description("系统字典主界面")]
        public ActionResult index()
        {
            return View();
        }
        #endregion

        #region 字典树json write by 吴俊鹏
        /// <summary>
        /// 字典树json write by 吴俊鹏
        /// </summary>
        /// <returns></returns>
        [Description("字典树json")]
        public ActionResult treejsonbytop2()
        {
            return Json(basedata.getTreeDataForTop2());
        }
        #endregion

        #region 表格json write by 吴俊鹏
        /// <summary>
        /// 表格json write by 吴俊鹏
        /// </summary>
        /// <param name="baseID"></param>
        /// <param name="isContains">是否包含自己</param>
        /// <returns></returns>
        [Description("表格json")]
        public ActionResult girdjson(int baseID, bool isContains = false)
        {
            if (baseID <= 0)
            {
                return null;
            }
            var model = new DBSql.Sys.BaseData().GetBaseDataInfo(baseID);
            var list = basedata.getTreeData(model);
            if (isContains)
            {
                var list1 = new List<object>();
                list1.Add(new
                {
                    id = model.BaseID,
                    text = model.BaseName,
                    BaseOrder = model.BaseOrder,
                    BaseAtt1 = model.BaseAtt1,
                    BaseAtt2 = model.BaseAtt2,
                    BaseAtt3 = model.BaseAtt3,
                    BaseAtt4 = model.BaseAtt4,
                    BaseAtt5 = model.BaseAtt5,
                    BaseNameEng = model.BaseNameEng,
                    children = list
                });
                return Json(list1);
            }
            return Json(list);
        }
        #endregion

        #region 树json write by 吴俊鹏
        /// <summary>
        /// 树json write by 吴俊鹏
        /// </summary>
        /// <param name="engName"></param>
        /// <param name="isSel">是否包含选择项</param>
        /// <returns></returns>
        [Description("树json")]
        public ActionResult treejson(string engName, int filterLength = 0, string typeId = "")
        {
            if (string.IsNullOrEmpty(engName))
            {
                return null;
            }
            var model = new DBSql.Sys.BaseData().GetBaseDataInfoByEngName(engName);
            var list = basedata.getTreeData(model, filterLength, typeId);

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 添加基础数据write by 吴俊鹏 2016-05-19
        /// <summary>
        ///  添加基础数据write by 吴俊鹏 2016-05-18
        /// </summary>
        /// <returns></returns>
        [Description("添加基础数据")]
        public ActionResult add(int parentid)
        {
            var model = new DataModel.Models.BaseData();
            ViewBag.parentid = parentid;
            return View("info", model);
        }
        #endregion

        #region 编辑基础数据write by 吴俊鹏 2016-05-20
        /// <summary>
        ///  write by 编辑基础数据write by 吴俊鹏 2016-05-19
        /// </summary>
        /// <returns></returns>
        [Description("编辑基础数据")]
        public ActionResult edit(int BaseID)
        {
            var model = basedata.GetBaseDataInfo(BaseID);
            ViewBag.parentid = basedata.GetBaseDataInfoByOrder(model.BaseOrder.Substring(0, model.BaseOrder.Length - 4)).BaseID;
            return View("info", model);
        }
        #endregion

        #region 根据BaseNameEng获取基础数据
        [Description("根据BaseNameEng获取基础数据")]
        [HttpPost]
        public ActionResult GetBasdByBaseNameEng(string baseNameEng)
        {
            var list = basedata.GetDataSetByEngName(baseNameEng).Select(p=>new {
                p.BaseID,
                p.BaseName,
                p.BaseOrder
            }).ToList();
            return Json(list);
        }
        #endregion


        #region 删除
        [Description("删除")]
        public ActionResult del(int BaseID)
        {
            int reuslt = 1;

            basedata.DeleteBaseData(BaseID);

            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion



        #region 移动排序
        [Description("删除")]
        public ActionResult move(int BaseID, int OrderType)
        {
            int reuslt = 1;

            basedata.OrderBaseData(BaseID, OrderType);

            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 保存基础数据 2016-05-20       
        /// <summary>
        /// 保存基础数据 2016-05-20       
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="areaValue"></param>
        /// <param name="controlValue"></param>
        /// <param name="actionValue"></param>
        /// <param name="urlParam"></param>
        /// <returns></returns>
        [Description("保存基础数据")]
        [HttpPost]
        public ActionResult save(int BaseID, int ParentID)
        {
            var model = new DataModel.Models.BaseData();
            if (BaseID > 0)
            {
                model = basedata.Get(BaseID);
            }
            model.MvcSetValue();

            int reuslt = 0;
            if (model.BaseID > 0)
            {
                basedata.UpdateBaseData(model);
                reuslt = 1;
            }
            else
            {
                if (ParentID > 0)
                {
                    model.BaseOrder = GetBaseInfo(ParentID).BaseOrder;
                }

                reuslt = basedata.InsertBaseData(model);
            }
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.BaseID, "操作成功") : DoResultHelper.doSuccess(model.BaseID, "操作失败");
            return Json(dr);
        }
        #endregion 

    }
}
