using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using Common.Data.Extenstions;
using System.Reflection;

namespace Core.Controllers
{
[Description("BaseEmpAgen")]
public class BaseEmpAgenController : CoreController
    {
        private DBSql.Sys.BaseEmpAgen op = new DBSql.Sys.BaseEmpAgen();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.NowDate = System.DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        } 
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string json()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            PageModel.SelectField = "new(BaseEmpAgenID,FromEmpName,ToEmpName,AgenFlowRefTable,AgenMenu,DateCreate,DateLower,DateUpper,AgenNote,AgenType,AgenStatus)";
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "BaseEmpAgenID desc";
            }
            var list = op.GetPagedDynamic(PageModel).ToList();
            DBSql.Sys.BaseMenu bm = new DBSql.Sys.BaseMenu();
            DBSql.PubFlow.FlowModel fm = new DBSql.PubFlow.FlowModel();
            List<dynamic> newlist = new List<dynamic>();

            foreach (var a in list)
            {
                dynamic na = new System.Dynamic.ExpandoObject();
                na.BaseEmpAgenID = a.BaseEmpAgenID;
                na.FromEmpName = a.FromEmpName;
                na.ToEmpName = a.ToEmpName;
                na.AgenFlowRefTable = a.AgenFlowRefTable;
                na.AgenMenu = a.AgenMenu;
                na.DateCreate = a.DateCreate;
                na.DateLower = a.DateLower;
                na.DateUpper = a.DateUpper;
                na.AgenNote = a.AgenNote;
                na.AgenType = a.AgenType;
                na.AgenStatus = a.AgenStatus;

                string AgenFlowRefTable = na.AgenFlowRefTable;
                string AgenMenu = na.AgenMenu;
                na.AgenFlowName = "";
                na.AgenMenuName = "";
                string[] reftables = AgenFlowRefTable.Split(',');
                string[] menus = AgenMenu.Split(',');
                for (int k=0;k<reftables.Length;k++)
                {
                    var flowobj = fm.GetModelByRefTable(reftables[k]);
                    if (k != 0)
                        na.AgenFlowName += ",";
                    if (flowobj != null)
                    {
                        na.AgenFlowName += flowobj.ModelName;
                    }
                }
                for (int k=0;k< menus.Length;k++)
                {
                    var menuobj = bm.GetModelByEng(menus[k]);
                    if (k != 0)
                        na.AgenMenuName += ",";
                    if (menuobj != null)
                    {
                        na.AgenMenuName += menuobj.MenuName;
                    }
                }
                newlist.Add(na);
            }
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = newlist
            });
        } 
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new BaseEmpAgen();
            model.FromEmpName = userInfo.EmpName;
            return View("info", model);
        } 
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            return View("info", model);
        }
        #endregion

        #region 权限回收
        [Description("权限回收")]
        public ActionResult del(int BaseEmpAgenID)
        {
            int reuslt = 0;
            var model = op.Get(BaseEmpAgenID);
            if (model.AgenStatus == 0)
            {
                model.AgenStatus = 1;
                op.Edit(model);
                reuslt = op.DbContext.SaveChanges();
            }
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "权限回收成功") : DoResultHelper.doSuccess(reuslt, "已经失效权限");
            return Json(dr);
        }

        [Description("待处理委托JSON")]
        public string Count()
        {
            var TWhere = QueryBuild<DataModel.Models.BaseEmpAgen>.True();

            var DateNow = System.DateTime.Now;

            TWhere = TWhere.And(p => p.DateLower <= DateNow && p.DateUpper >= DateNow);

            TWhere = TWhere.And(p => p.AgenStatus == 0);

            TWhere = TWhere.And(p => p.ToEmpID == userInfo.EmpID);
            var list = op.GetList(TWhere);

            var data = (from s in list
                        select new
                        {
                            s.BaseEmpAgenID,
                            s.FromEmpID,
                            s.FromEmpName,
                            s.ToEmpID,
                            s.ToEmpName,
                            s.DateCreate,
                            s.DateLower,
                            s.DateUpper,
                            s.AgenNote,
                            s.AgenStatus,
                        });

            return JavaScriptSerializerUtil.getJson(new
            {
                total = data.Count(),
                rows = data
            });
        }


        [Description("权限切换")]
        public ActionResult SaveSessionAgenInfo(int AgenID)
        {
            var model = op.Get(AgenID);
            //获取代理人信息
            var emp = GetEmpInfo(model.FromEmpID);
            DataModel.EmpSession empSession = new DataModel.EmpSession()
            {
                EmpID = emp.EmpID,
                EmpName = emp.EmpName,
                EmpDepID = emp.EmpDepID,
                EmpDepName = emp.FK_BaseEmployee_EmpDepID.BaseName,
                EmpMenuType = emp.EmpMenuType,
                EmpMacAddress = emp.EmpMacAddress,
                EmpThemesName = emp.EmpThemesName,
                EmpPageSize = emp.EmpPageSize,
                EmpDisk = emp.EmpDisk,
                LoginIP = GetIP(),

                AgenEmpID = userInfo.EmpID,
                AgenEmpName = userInfo.EmpName,
                AgenDepID = userInfo.EmpDepID,
                AgenDepName = userInfo.EmpDepName,
                AgenTypeID = model.AgenType,
                AgenFlow= model.AgenFlowRefTable,
                AgenMenu = model.AgenMenu
            };

            if (Session["Employee"] != null) Session.Remove("Employee");
            Session.Add("Employee", empSession);
            userInfo = empSession;

            DoResult dr =  DoResultHelper.doSuccess("权限切换成功")  ;
            return Json(dr);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int BaseEmpAgenID)
        {
            var model = new BaseEmpAgen();
            if (BaseEmpAgenID > 0)
            {
                model = op.Get(BaseEmpAgenID);
            }
            model.MvcSetValue();

            int reuslt = 0;                      
            if (model.BaseEmpAgenID > 0)
            {
				op.Edit(model);
            }
            else
            {
                model.FromEmpID = userInfo.EmpID;
                model.FromEmpName = userInfo.EmpName;
                model.ToEmpName = GetEmpInfo(model.ToEmpID).EmpName;
                model.DateCreate = System.DateTime.Now; 

                op.Add(model);
            }
			op.UnitOfWork.SaveChanges();
			reuslt = model.BaseEmpAgenID ;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.BaseEmpAgenID, "操作成功") : DoResultHelper.doSuccess(model.BaseEmpAgenID, "操作失败");
            return Json(dr);
        } 
        #endregion
    }
}
