using JQ.Web;
using JQ.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataModel.Models;
using DAL;

namespace Core.Controllers
{
    [Description("菜单管理")]
    public class menuController : CoreController
    {
        private readonly DBSql.Sys.BaseMenu menu = new DBSql.Sys.BaseMenu();
        private readonly DBSql.Sys.BaseMenuPermissionByEmp menuEmp = new DBSql.Sys.BaseMenuPermissionByEmp();
        private readonly DBSql.Sys.BaseMenuPermissionByRole menuRole = new DBSql.Sys.BaseMenuPermissionByRole();

        #region 菜单列表write by 吴俊鹏 2016-05-18
        /// <summary>
        /// 菜单列表write by 吴俊鹏 2016-05-18
        /// </summary>
        /// <returns></returns>
        [Description("菜单列表")]
        public ActionResult menulist()
        {
            return View();
        }
        #endregion

        #region 菜单列表Json write by 吴俊鹏 2016-05-18
        /// <summary>
        /// 菜单列表Jsonwrite by 吴俊鹏 2016-05-18
        /// </summary>
        /// <param name="id"></param>
        /// <param name="level">从第几级开始取菜单</param>
        /// <param name="isAll">是否根据权限来取菜单</param>
        /// <returns></returns>
        [Description("菜单列表Json")]
        [HttpPost]
        public ActionResult menujson(int id = 0, int level = 1, bool isIcon = false, bool isAll = false)
        {
            var list = new List<DataModel.Models.BaseMenu>();
            if (isAll)
            {
                list = menu.AllData.OrderBy(s => s.MenuOrder).ToList();
            }
            else
            {
                if (id > 0)
                {
                    if (!userInfo.EmpIsPay && id == 1861)
                    {
                        return Json("no");
                    }
                    var m = menu.GetBaseMenuInfo(id);
                    list = menu.GetBaseMenuListByPermit(userInfo.EmpID).Where(p => p.MenuIsSecond == false).Where(p => p.MenuOrder.StartsWith(m.MenuOrder + "_")).OrderBy(s => s.MenuOrder).ToList();
                }
                else
                {
                    list = menu.GetBaseMenuListByPermit(userInfo.EmpID).OrderBy(s => s.MenuOrder).ToList();
                }
            }
            return Json(menu.treeJson(list, level, isIcon));
        }


        [Description("菜单列表设置Json")]
        [HttpPost]
        public ActionResult menujsonlist(int MenuID = 0)
        {
            if (MenuID == 0) return Json(null);

            var m = menu.GetBaseMenuInfo(MenuID);
            var all = menu.AllData.Where(p => p.MenuOrder.StartsWith(m.MenuOrder)).OrderBy(s => s.MenuOrder).ToList();

            return Json(menu.treeJson(all, 1, false));
        }
        #endregion

        #region 添加菜单write by 吴俊鹏 2016-05-19
        /// <summary>
        ///  添加菜单write by 吴俊鹏 2016-05-18
        /// </summary>
        /// <returns></returns>
        [Description("添加菜单")]
        public ActionResult addmenu()
        {
            BaseMenu model = new BaseMenu();
            ViewBag.isSys = 0;
            ViewBag.areaValue = ControllerContext.RouteData.DataTokens["area"] as string;   //获取区域
            return View("menuinfo", model);
        }
        #endregion

        #region 编辑菜单write by 吴俊鹏 2016-05-19
        /// <summary>
        ///  write by 编辑菜单write吴俊鹏 2016-05-19
        /// </summary>
        /// <returns></returns>
        [Description("编辑菜单")]
        public ActionResult editmenu(long id)
        {
            BaseMenu model = menu.Get(id);
            ViewBag.areaValue = ControllerContext.RouteData.DataTokens["area"] as string;   //获取区域
            if (!string.IsNullOrEmpty(model.MenuCommand))  //菜单路径不为空
            {
                string[] pathArr = model.MenuCommand.Split('/');
                if (pathArr.Length == 3)   //正确路径分隔后应有4个元素
                {
                    ViewBag.areaValue = pathArr[0].Substring(0, 1).ToUpperInvariant() + pathArr[0].Substring(1);
                    ViewBag.controlValue = pathArr[1] + "Controller";
                    ViewBag.actionValue = pathArr[2].IndexOf('?') < 0 ? pathArr[2] : pathArr[2].Substring(0, pathArr[2].IndexOf('?'));
                    ViewBag.urlParam = pathArr[2].IndexOf('?') < 0 ? string.Empty : pathArr[2].Substring(pathArr[2].IndexOf('?'));
                }
            }


            //获取菜单的权限

            return View("menuinfo", model);
        }
        #endregion

        #region 保存菜单write吴俊鹏 2016-05-19  
        /// <summary>
        /// 保存菜单write吴俊鹏 2016-05-19       
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="areaValue"></param>
        /// <param name="controlValue"></param>
        /// <param name="actionValue"></param>
        /// <param name="urlParam"></param>
        /// <returns></returns>
        [Description("保存菜单")]
        [HttpPost]
        public ActionResult savemenu(int MenuID, string areaValue, string controlValue, string actionValue, string urlParam, bool MenuIsMust = false, bool MenuIsSecond = false, int ParentID = 0)
        {
            var model = new BaseMenu();
            if (MenuID > 0)
            {
                model = menu.Get(MenuID);
            }
            model.MvcSetValue();
            model.MenuIsMust = MenuIsMust;
            model.MenuIsSecond = MenuIsSecond;
            if (!string.IsNullOrEmpty(controlValue) && !string.IsNullOrEmpty(actionValue))
            {
                //拼接菜单路径并转换为小写
                model.MenuCommand = (areaValue + "/" + controlValue.Substring(0, controlValue.IndexOf("Controller"))
                    + "/" + actionValue + urlParam).ToLowerInvariant();
            }
            else if (((string.IsNullOrEmpty(controlValue) || string.IsNullOrEmpty(actionValue)) && !string.IsNullOrEmpty(urlParam))
                || (string.IsNullOrEmpty(actionValue) && !string.IsNullOrEmpty(controlValue)))
            {
                List<ValidationResult> vrs = new List<ValidationResult>();
                vrs.Add(DataAnnotationHelper.validResult("请选择完整菜单路径", "menuUrl"));
                return Json(DoResultHelper.doIsValid(vrs));
            }
            if (model.MenuID > 0)
            {
                menu.Update(model);
            }
            else
            {
                if (ParentID > 0)
                {
                    model.MenuOrder = menu.GetBaseMenuInfo(ParentID).MenuOrder;
                }
                menu.Insert(model);
            }
            return Json(DoResultHelper.doSuccess(model.MenuID, "保存成功"));
        }
        #endregion

        #region 删除菜单 write吴俊鹏 2016-06-21  
        [Description("删除菜单")]
        public JsonResult delmenu(int id)
        {
            menu.Delete(m => m.MenuID == id);
            menu.UnitOfWork.SaveChanges();
            return Json(DoResultHelper.doSuccess("操作成功"));
        }
        #endregion

        #region 移动菜单 write吴俊鹏 2016-06-21  
        [Description("移动菜单")]
        public JsonResult movemenu(int id, int type)
        {
            menu.OrderBaseData(id, type);
            return Json(DoResultHelper.doSuccess("操作成功"));
        }
        #endregion

        #region 菜单路径选择区域 write吴俊鹏 2016-06-03  
        /// <summary>
        /// 菜单路径选择区域
        /// </summary>
        /// <returns></returns>
        [Description("区域列表json")]
        public ActionResult areajson()
        {

            List<SelectListItem> areaSelect = new List<SelectListItem>();
            IEnumerable<string> areaNames = AuthHelper.readAreasFromCache();   //得到所有注册区域
            //转换为SelectListItem
            areaSelect.AddRange(areaNames.Select(m => new SelectListItem { Text = m, Value = m }));
            return Json(areaSelect, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 菜单路径选择控制器 write吴俊鹏 2016-06-03  
        /// <summary>
        /// 菜单路径选择控制器
        /// </summary>
        /// <returns></returns>
        [Description("控制器列表json")]
        public ActionResult controljson(string areaValue)
        {
            List<SelectListItem> controlSelect = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(areaValue))
            {
                List<MvcActionInfo> controlActions = AuthHelper.readControllersFromCache(typeof(JQ.Web.CoreController).Name,
                        AuthHelper.readAreasFromCache());   //得到所有控制器
                var areaControlActions = controlActions.Where(m => StringUtil.compareIgnoreCase(areaValue, m.area));
                if (areaControlActions.isNotEmpty())
                    //转换为SelectListItem
                    controlSelect.AddRange(areaControlActions.Select(m => new SelectListItem { Text = m.display, Value = m.controllerAction }));
            }
            return Json(controlSelect, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 菜单路径选择动作 write吴俊鹏 2016-06-03  
        /// <summary>
        /// 菜单路径选择动作
        /// </summary>
        /// <param name="controlValue"></param>
        /// <returns></returns>
        [Description("动作列表json")]
        public ActionResult actionjson(string controlValue)
        {
            List<SelectListItem> actionSelect = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(controlValue))
            {
                List<MvcActionInfo> controlActions = AuthHelper.readControllersFromCache(typeof(JQ.Web.CoreController).Name,
                        AuthHelper.readAreasFromCache());   //得到所有控制器
                MvcActionInfo control = controlActions.FirstOrDefault(m => StringUtil.compareIgnoreCase(controlValue, m.controllerAction));  //得到对应的控制器
                if (control != null && control.children != null)  //判断是否存在动作列表
                    //转换为SelectListItem
                    actionSelect.AddRange(control.children.Select(m => new SelectListItem { Text = m.display, Value = m.controllerAction.Split('-')[1] }));
            }
            return Json(actionSelect, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 菜单列表
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SavePermit(int RoleID = 0, int EmpID = 0, int MenuID = 0)
        {
            var list = JavaScriptSerializerUtil.parseFormJson<List<DataModel.MenuPermitSet>>(Request.Params["list"].ToString());
            var model = new List<DataModel.MenuPermitSet>();
            if (list != null)
            {
                model = list.GroupBy(t => new { t.MenuID })
                .Select(g => new DataModel.MenuPermitSet { MenuID = g.Key.MenuID, MenuPermissions = CalByte(g.Select(s => s.MenuPermissions).ToArray()) }).ToList();
            }
            var menuArr = new List<int>();
            if (MenuID > 0)
            {
                var TopMenu = menu.GetBaseMenuInfo(MenuID);
                menuArr = menu.AllData.Where(s => s.MenuOrder.StartsWith(TopMenu.MenuOrder)).Select(s => s.MenuID).ToList();
            }
            if (EmpID > 0)
            {
                menuEmp.UpdatePermitByEmp(EmpID, model, menuArr);
            }
            else if (RoleID > 0)
            {
                menuRole.UpdatePermitByRole(RoleID, model, menuArr);
            }
            return Json(DoResultHelper.doSuccess("保存成功"));
        }


        private int CalByte(int[] MenuPermissions)
        {
            int result = 0;
            foreach (var item in MenuPermissions)
            {
                result = result ^ item;
            }
            return result;
        }

        [Description("菜单列表权限Json")]
        [HttpPost]
        public ActionResult Permitjson(int MenuID = 0, int RoleID = 0, int EmpID = 0)
        {
            if (MenuID == 0) return Json(null);
            if (RoleID == 0 && EmpID == 0) return Json(null);

            var m = menu.GetBaseMenuInfo(MenuID);
            var menulist = menu.AllData.Where(p => p.MenuOrder.StartsWith(m.MenuOrder)).OrderBy(s => s.MenuOrder).ToList();
            var m1 = menuEmp.GetMenuHasEmp(0);
            var m2 = menuRole.GetMenuHasRole(0);
            var all = (from s in menulist
                       select new DataModel.MenuPermit
                       {
                           MenuID = s.MenuID,
                           MenuName = s.MenuName,
                           MenuNameEng = s.MenuNameEng,
                           MenuOrder = s.MenuOrder,
                           ParentID = s.ParentID,
                           add = PermitValue(s, 1, m1, m2, RoleID, EmpID),//s.MenuPermissions
                           edit = PermitValue(s, 2, m1, m2, RoleID, EmpID),
                           del = PermitValue(s, 4, m1, m2, RoleID, EmpID),
                           export = PermitValue(s, 8, m1, m2, RoleID, EmpID),
                           emp = PermitValue(s, 16, m1, m2, RoleID, EmpID),
                           dep = PermitValue(s, 32, m1, m2, RoleID, EmpID),
                           allview = PermitValue(s, 64, m1, m2, RoleID, EmpID),
                           alledit = PermitValue(s, 128, m1, m2, RoleID, EmpID),
                           allDown = PermitValue(s, 256, m1, m2, RoleID, EmpID),
                       });

            return Json(treeJson(all, 1));
        }

        string PermitValue(BaseMenu menu, int PermitValue, List<DataModel.Models.BaseMenuPermissionByEmp> MenuHasEmp, List<DataModel.Models.BaseMenuPermissionByRole> MenuHasRole, int RoleID = 0, int EmpID = 0)
        {
            if ((menu.MenuPermissions & PermitValue) != PermitValue) return "NoPermit";
            int MenuID = menu.MenuID;
            int v = 0;
            if (EmpID > 0)
            {
                var m = MenuHasEmp.FirstOrDefault(s => s.BaseMenuPermissionByEmpID == EmpID && s.BaseMenuPermissionByEmpValue == MenuID);
                if (m != null) v = m.EmpPermissionValue;
            }
            else if (RoleID > 0)
            {
                var m = MenuHasRole.FirstOrDefault(s => s.BaseMenuPermissionByRoleID == RoleID && s.BaseMenuPermissionByRoleValue == MenuID);
                if (m != null) v = m.RolePermissionValue;
            }

            if (v != 0)
            {
                if ((v & PermitValue) == PermitValue) return "checked";
            }

            return "";
        }


        public List<object> treeJson(IEnumerable<DataModel.MenuPermit> entitys, int leven)
        {
            List<object> list = new List<object>();
            if (entitys.isNotEmpty())
            {
                foreach (var item in entitys)
                {
                    //获取子类 递归
                    IList<object> clist = treeJson(entitys.Where(m => (m.MenuOrder != item.MenuOrder) && StringUtil.startsWithIgnoreCase(m.MenuOrder, item.MenuOrder)), leven + 1);
                    if ((item.MenuOrder.Replace("_", "").Length / 2) == leven)
                    {
                        list.Add(new
                        {
                            id = item.MenuID,
                            parentID = item.ParentID,
                            orderCode = item.MenuOrder,
                            text = item.MenuName,
                            add = item.add,
                            edit = item.edit,
                            del = item.del,
                            export = item.export,
                            emp = item.emp,
                            dep = item.dep,
                            allview = item.allview,
                            alledit = item.alledit,
                            allDown = item.allDown,
                            children = clist
                        });
                    }
                }
            }
            if (list.Count > 0)
            {
                return list;
            }
            return null;
        }

        public ActionResult PermitTopjson(int OrderLen = 2)
        {
            var list = menu.AllData;

            if (OrderLen != 0)
            {
                list = list.Where(s => s.MenuOrder.Length == OrderLen);
            }

            return Json(list.Select(s => new
            {
                s.MenuID,
                s.MenuName,
                s.MenuOrder,
                s.MenuNameEng
            }).OrderBy(s => s.MenuOrder).ToList());
        }
        #endregion

        public JsonResult GetShortcutList()
        {
            var list = new DBSql.Sys.BaseMenu().GetShortcutList(userInfo.EmpID);
            return Json(new { total = list.Count, rows = list });
        }

        public JsonResult SaveShortcut()
        {
            //if (string.IsNullOrEmpty(Request.Form["idSet"]))
            //{
            //    return Json(new { Result = false, Message = "参数错误" });
            //}
            var idSet = Request.Form["idSet"] ?? "";
            //if (string.IsNullOrEmpty(idSet))
            //{
            //    return Json(new { Result = false, Message = "参数错误" });
            //}
            var ids = new List<int>();
            foreach (var id in idSet.Split(new char[] { ',' }))
            {
                var temp = 0;
                if (int.TryParse(id, out temp) && temp > 0)
                {
                    ids.Add(temp);
                }
            }
            //if (ids.Count == 0)
            //{
            //    return Json(new { Result = false, Message = "参数错误" });
            //}
            new DBSql.Sys.BaseMenu().SaveShortcut(ids, userInfo.EmpID);
            return Json(new { Result = true });
        }
    }
}
