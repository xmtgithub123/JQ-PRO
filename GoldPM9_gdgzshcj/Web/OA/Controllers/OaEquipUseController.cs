using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Common.Data.Extenstions;

namespace Oa.Controllers
{
[Description("OaEquipUse")]
public class OaEquipUseController : CoreController
    {
        private DBSql.Oa.OaEquipUse op = new DBSql.Oa.OaEquipUse();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        private DBSql.PubFlow.Flow dbFlow = new DBSql.PubFlow.Flow();

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("EquipmentBorrow")));
            ViewBag.EmpId = userInfo.EmpID;
            return View();
        } 
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string json()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }

            var Thwere = QueryBuild<DataModel.Models.OaEquipUse>.True();
            List<string> permissionList = PermissionList("EquipmentBorrow");

            if (!permissionList.Contains("dep") && !permissionList.Contains("allview"))
            {
                Thwere = Thwere.And(p => p.CreatorEmpId == userInfo.EmpID);//个人查看权
            }
            else
            {
                if (!permissionList.Contains("allview") && permissionList.Contains("dep"))
                {
                    Thwere = Thwere.And(p => p.CreatorDepId == userInfo.EmpDepID);//部门查看权
                }
            }

            int EquipOrOffice = 0;
            int.TryParse(Request["EquipOrOffice"],out EquipOrOffice);
            Thwere = Thwere.And(p => p.DeleterEmpId == 0);
            Thwere = Thwere.And(p => p.EquipOrOffice == EquipOrOffice);

            var list = op.GetPagedList(Thwere, PageModel).ToList();

            var showList = (from p in list
                            join t1 in op.DbContext.Set<DataModel.Models.Flow>().Where(f => f.FlowRefTable == "OaEquipUseFlow") on p.Id equals t1.FlowRefID into nodes
                            from temp in nodes.DefaultIfEmpty()
                            select new
                            {
                                Id = p.Id,
                                EquipLendDate = p.EquipLendDate,
                                CreatorEmpName = p.CreatorEmpName,
                                p.CreatorEmpId,
                                isLess = GetIsLess(p.Id),// new DBSql.Oa.OaEquipUse().GetCount(p.Id).Rows.Count >0 ? new DBSql.Oa.OaEquipUse().GetCount(p.Id).Rows[0][4]:"",
                               EquipLendNote = p.EquipLendNote,
                               FlowID = temp == null ? 0 : temp.Id,
                               FlowName = temp == null ? "" : temp.FlowName,
                               FlowStatusID = temp == null ? 0 : temp.FlowStatusID,
                               FlowStatusName = temp == null ? "" : temp.FlowStatusName,
                               FlowXml = temp == null ? "" : temp.FlowXml
                           }).Select(m => new { m.Id, m.EquipLendDate,m.isLess, m.CreatorEmpName, m.CreatorEmpId, m.EquipLendNote, m.FlowID, m.FlowName, m.FlowStatusID, m.FlowStatusName, m.FlowXml, FlowTurnedEmpIDs = JQ.Util.TypeParse.GetTurnedEmpIDs(m.FlowXml) });
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = showList
            });
        } 
        #endregion

        public string GetIsLess(int Id)
        {
            var list = new DBSql.Oa.OaEquipUse().GetCount(Id);
            if (list.Rows.Count > 1)
            {
                var data = list.AsEnumerable().Select(p => p.Field<string>("isLess")).Distinct();
                if (data.ToList().Count > 1)
                    return "是";
                else
                    return data.ToList()[0];
            }
            else if(list.Rows.Count == 1)
            {
                return list.Rows[0][4].ToString();
            }
            else
                return "";
        }

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new OaEquipUse();
             int EquipOrOffice = 0;
            if (GetRequestValue<int>("EquipOrOffice") != null)
            {
                EquipOrOffice = GetRequestValue<int>("EquipOrOffice");
            }
            if (EquipOrOffice != 1)
            {
                ViewBag.permission = "['add','submit']";
                return View("info", model);
            }
            else
            {
                ViewBag.permission = "['add','submit']";
                return View("infoOaUsage", model);
            }
        } 
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            List<string> permissionList = PermissionList("EquipmentBorrow");
            ViewBag.isExport = permissionList.Contains("export") ? 1 : 0;

            if (model.EquipOrOffice != 1)
            {
                return View("info", model);
            }
            else
            {
                return View("infoOaUsage", model);
            }
        } 
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 0;
            op.DbContext.Set<OaEquipUse>().RemoveRange(op.DbContext.Set<OaEquipUse>().Where(p => id.Contains(p.Id)));
            //设备0，办公用品1
            int EquipOrOffice = 0;
            if (GetRequestValue<int>("EquipOrOffice") != null)
            {
                EquipOrOffice = GetRequestValue<int>("EquipOrOffice");
            }
            if (EquipOrOffice == 0)
            {
                op.DbContext.Set<OaEquipStock>().RemoveRange(op.DbContext.Set<OaEquipStock>().Where(p => p.EquipFormType == "OaEquipUse" && id.Contains(p.EquipFormID)));
                op.DbContext.SaveChanges();
                new DBSql.PubFlow.Flow().Delete(id, "OaEquipUseFlow");
            }
            else
            {
                op.DbContext.Set<OaEquipStock>().RemoveRange(op.DbContext.Set<OaEquipStock>().Where(p => p.EquipFormType == "OaEquipUseOfficeFlow" && id.Contains(p.EquipFormID)));
                op.DbContext.SaveChanges();
                new DBSql.PubFlow.Flow().Delete(id, "OaEquipUseOfficeFlow");
            }
            reuslt = 1;

            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        } 
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id)
        {
            var model = new OaEquipUse();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();

            //设备0，办公用品1
            int EquipOrOffice = 0;
            if (TypeHelper.parseInt(Request.Form["EquipOrOffice"]) != null)
            {
                EquipOrOffice = TypeHelper.parseInt(Request.Form["EquipOrOffice"]);
            }

            int reuslt = 0;                      
            if (model.Id > 0)
            {
                model.MvcDefaultEdit(userInfo.EmpID);
                model.EquipOrOffice = EquipOrOffice;
				op.Edit(model);
            }
            else
            {
                model.MvcDefaultSave(userInfo);
                model.EquipOrOffice = EquipOrOffice;
                op.Add(model);
            }
			op.UnitOfWork.SaveChanges();
			reuslt = model.Id ;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 设备选择
        [Description("列表")]
        public ActionResult EquipmentChoose()
        {
            return View();
        }
        #endregion


        #region 办公用品领用列表
        [Description("办公用品领用列表")]
        public ActionResult listOaEquipUse()
        {
            List<string> permission = PermissionList("OfficeSuppliesGet");
            ViewBag.permission = JavaScriptSerializerUtil.getJson(permission);
            //将调度权与维护权同步
            if (permission.Contains("alledit"))
            {
                ViewBag.Change = 1;//可以进行调度
            }
            else
            {
                ViewBag.Change = 0;//不可进行调度
            }
            ViewBag.EmpId = userInfo.EmpID;//当前登录账户

            return View();
        }
        #endregion

        #region 办公用品领用列表json
        [Description("办公用品领用列表json")]
        [HttpPost]
        public string jsonOaEquipUse()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            var list = op.GetPagedList(p => p.DeleterEmpId == 0 && p.EquipOrOffice == 1, PageModel).ToList();

            List<string> result = PermissionList("OfficeSuppliesRegister");
            if (!result.Contains("allview") && !result.Contains("dep") && !result.Contains("emp"))
            {
                list = op.GetPagedList(p => p.CreatorEmpId == this.userInfo.EmpID && p.DeleterEmpId == 0 && p.EquipOrOffice == 1, PageModel).ToList();
            }
            else if (result.Contains("allview"))
            {

            }
            else if (result.Contains("dep"))
            {
                list = op.GetPagedList(p => p.CreatorDepId == this.userInfo.EmpDepID && p.DeleterEmpId == 0 && p.EquipOrOffice == 1, PageModel).ToList();
            }
            else if (result.Contains("emp"))
            {
                list = op.GetPagedList(p => p.CreatorEmpId == this.userInfo.EmpID && p.DeleterEmpId == 0 && p.EquipOrOffice == 1, PageModel).ToList();
            }

            var dt = (from p in list
                     join t1 in op.DbContext.Set<DataModel.Models.Flow>().Where(f => f.FlowRefTable == "OaEquipUseOfficeFlow") on p.Id equals t1.FlowRefID into nodes
                     from temp in nodes.DefaultIfEmpty()
                     select new
                       {
                           Id = p.Id,
                           EquipLendDate = p.EquipLendDate,
                           CreatorEmpName = p.CreatorEmpName,
                           EquipLendNote = p.EquipLendNote,
                           p.CreatorEmpId,
                         //FlowStatus = GetFlowStatus(p.Id, p.CreatorEmpName)
                         FlowID = temp == null ? 0 : temp.Id,
                         FlowName = temp == null ? "" : temp.FlowName,
                         FlowStatusID = temp == null ? 0 : temp.FlowStatusID,
                         FlowStatusName = temp == null ? "" : temp.FlowStatusName,
                         FlowXml = temp == null ? "" : temp.FlowXml
                     }).Select(m => new { m.Id, m.EquipLendDate, m.CreatorEmpName, m.EquipLendNote, m.CreatorEmpId, m.FlowID, m.FlowName, m.FlowStatusID, m.FlowStatusName, m.FlowXml, FlowTurnedEmpIDs = JQ.Util.TypeParse.GetTurnedEmpIDs(m.FlowXml) });

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = dt
            });
        }
        #endregion

        #region 办公用品选择
        [Description("办公用品选择")]
        public ActionResult EquipmentOaChoose()
        {
            return View();
        }
        #endregion


        public int GetFlowStatus(int RefID, string CreateEmpName)
        {
            int flowStatus = 0;
            DataModel.Models.Flow flowModel = dbFlow.GetList(p => p.FlowRefTable == "OaEquipUseOfficeFlow" &&
            p.FlowRefID == RefID).FirstOrDefault();
            if (flowModel != null)
            {
                flowStatus = flowModel.FlowStatusID;
                if (flowModel.FlowStatusName.Contains("退回") && flowModel.FlowStatusName.Contains(CreateEmpName))
                {
                    flowStatus = 0;
                }
            }
            return flowStatus;
        }
    }
}
