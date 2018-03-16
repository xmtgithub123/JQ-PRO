using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
namespace Bussiness.Controllers
{
[Description("BussCustomerRemember")]
public class BussCustomerRememberController : CoreController
    {
        private DBSql.Bussiness.BussCustomerRemember op = new DBSql.Bussiness.BussCustomerRemember();
        private DBSql.Sys.BaseData data = new DBSql.Sys.BaseData();
        private DBSql.Bussiness.BussCustomer cus = new DBSql.Bussiness.BussCustomer();
        private DBSql.Sys.BaseEmployee emp = new DBSql.Sys.BaseEmployee();
        private DBSql.Project.Project proj = new DBSql.Project.Project();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        private static int custID = 0;
        private DBSql.Project.ProjSub DbProjSub = new DBSql.Project.ProjSub();
        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("CustomerInfo")));//联系人权限与客户权限一致
            int CustID = TypeHelper.parseInt(Request.QueryString["CustID"]);
            DataModel.Models.BussCustomer modelCust = cus.Get(CustID);
            ViewBag.State = 1;
            if(modelCust.TypeID==1)
            {
                ViewBag.State = 1;
            }
            else
            {
                ViewBag.State = 0;
            }
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
            int CustID = TypeHelper.parseInt(Request.QueryString["CustID"]);
            custID = CustID;
            var list = op.GetPagedList(p=>p.CustID==CustID,PageModel).ToList();
            var target = from item in list
                         let pro=proj.Get(item.ProjID)
                         let subProj=DbProjSub.Get(item.ProjID)
                         select new
                         {
                             item.Id,
                             CustName=item.FK_BussCustomerRemember_CustID==null?"":item.FK_BussCustomerRemember_CustID.CustName,
                             CustTypeName= (item.FK_BussCustomerRemember_CustID==null||item.FK_BussCustomerRemember_CustID.FK_BussCustomer_CustType==null)?"": item.FK_BussCustomerRemember_CustID.FK_BussCustomer_CustType.BaseName,
                             ProjNumber=(pro==null)?"":pro.ProjNumber,
                             ProjName= (pro == null)?"" : pro.ProjName,
                             RecordName =emp.Get(item.RememberEmpId)==null?"": emp.Get(item.RememberEmpId).EmpName,
                             RecordDeptName=data.Get(item.RememberDeptId)==null?"":data.Get(item.RememberDeptId).BaseName,
                             RecordType=item.FK_BussCustomerRemember_RememberType==null?"": item.FK_BussCustomerRemember_RememberType.BaseName,
                             item.RememberNote,
                             item.RememberTime,
                             SubNumber=subProj==null?"":subProj.SubNumber,
                             SubName=subProj==null?"":subProj.SubName,
                             SubTypeName=(item.FK_BussCustomerRemember_CustID==null?"":string.Join(",",GetBaseName(item.FK_BussCustomerRemember_CustID.CustTypeIDs)))
                         };

            
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = target
            });
        } 
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new BussCustomerRemember();
            var custModel = cus.Get(custID);
            model.RememberTime = System.DateTime.Now;
            ViewBag.CustName = custModel==null?"":custModel.CustName;
            ViewBag.TypeName = data.Get(custModel.CustType) == null ? "" : data.Get(custModel.CustType).BaseName;
            ViewBag.EmpName = userInfo.EmpName;
            ViewBag.DeptName = data.Get(userInfo.EmpDepID) == null ? "" : data.Get(userInfo.EmpDepID).BaseName;
            ViewBag.ProjName = ProjName(model.ProjID);
            if (model.ProjID == 0)
            {
                ViewBag.Pro = "";
            }
            else
            {
                ViewBag.Pro = model.ProjID.ToString();
            }
            string pageInfo = string.Empty;
            if(custModel.TypeID==1)
            {
                pageInfo = "subinfo";
                ViewBag.TypeName = custModel == null ? "" : string.Join(",", GetBaseName(custModel.CustTypeIDs));
                ViewBag.SubName = ProjSubName(model.ProjID);
            }
            else
            {
                pageInfo = "info";
            }
            return View(pageInfo, model);
        } 
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            var custModel = cus.Get(model.CustID);
            ViewBag.CustName = custModel == null ? "" : custModel.CustName;
            ViewBag.TypeName = data.Get(custModel.CustType) == null ? "" : data.Get(custModel.CustType).BaseName;
            ViewBag.EmpName = emp.Get(model.RememberEmpId) == null ? "":emp.Get(model.RememberEmpId).EmpName;
            ViewBag.DeptName = data.Get(model.RememberDeptId) == null ? "" : data.Get(model.RememberDeptId).BaseName;
            ViewBag.ProjName = ProjName(model.ProjID);
            if (model.ProjID == 0)
            {
                ViewBag.Pro = "";
            }
            else
            {
                ViewBag.Pro = model.ProjID.ToString();
            }
            string pageInfo = string.Empty;
            if (custModel.TypeID == 1)
            {
                pageInfo = "subinfo";
                ViewBag.SubName = ProjSubName(model.ProjID);
                ViewBag.TypeName = custModel == null ? "" : string.Join(",", GetBaseName(custModel.CustTypeIDs));
            }
            else
            {
                pageInfo = "info";
            }
            return View(pageInfo, model);
        } 
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 0;
            op.Delete(s => id.Contains(s.Id));
            op.UnitOfWork.SaveChanges();
            reuslt++;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        } 
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id)
        {
            var model = new BussCustomerRemember();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();

            int reuslt = 0;                      
            if (model.Id > 0)
            {
                model.MvcDefaultEdit(userInfo.EmpID);
                model.RememberEmpId = userInfo.EmpID;
                model.RememberDeptId = userInfo.EmpDepID;

                op.Edit(model);
            }
            else
            {
                model.RememberEmpId = userInfo.EmpID;
                model.RememberDeptId= userInfo.EmpDepID;
                model.CustID = custID;
                model.MvcDefaultSave(userInfo.EmpID);
                op.Add(model);				
            }
			op.UnitOfWork.SaveChanges();
            using (var ba = new DBSql.Sys.BaseAttach())
            {
                ba.MoveFile(model.Id, userInfo.EmpID, userInfo.EmpName);
            }

            reuslt = model.Id;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion

        /// <summary>
        /// 获取工程名称
        /// </summary>
        /// <param name="ProjID"></param>
        /// <returns></returns>
        public string ProjName(int ProjID)
        {
            string projName = string.Empty;
            DataModel.Models.Project pro = proj.Get(ProjID);
            if (pro != null)
            {
                projName = pro.ProjName;
            }
            return projName;
        }

        /// <summary>
        ///  获取外委名称
        /// </summary>
        /// <param name="ProjSubID"></param>
        /// <returns></returns>
        public string  ProjSubName(int ProjSubID)
        {
            string SubName = string.Empty;
            DataModel.Models.ProjSub modelProjSub = DbProjSub.Get(ProjSubID);
            if(modelProjSub!=null)
            {
                SubName = modelProjSub.SubName;
            }
            return SubName;
        }
    }
}
