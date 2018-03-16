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
    [Description("OaEquipment")]
    public class OaEquipmentController : CoreController
    {
        private DBSql.Oa.OaEquipment op = new DBSql.Oa.OaEquipment();
        private DBSql.Oa.OaEquipUse OaEquipUse = new DBSql.Oa.OaEquipUse();

        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("EquipmentRecord")));
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

            var Thwere = QueryBuild<DataModel.Models.OaEquipment>.True();

            //List<string> permissionList = PermissionList("EquipmentRecord");

            //if (!permissionList.Contains("dep") && !permissionList.Contains("allview"))
            //{
            //    Thwere = Thwere.And(p => p.CreatorEmpId == userInfo.EmpID);//个人查看权
            //}
            //else
            //{
            //    if (!permissionList.Contains("allview") && permissionList.Contains("dep"))
            //    {
            //        Thwere = Thwere.And(p => p.CreatorDepId == userInfo.EmpDepID);//部门查看权
            //    }
            //}

            string EquIds = "";
            if (GetRequestValue<string>("EquIds") != null)
            {
                EquIds = GetRequestValue<string>("EquIds").Trim(',');
            }

            List<string> EquId = new List<string>(EquIds.Split(','));
            int EquipOrOffice = Request["EquipOrOffice"] == null ? 0 : int.Parse(Request["EquipOrOffice"]);
            Thwere = Thwere.And(p => p.DeleterEmpId == 0);
            Thwere = Thwere.And(p => p.EquipOrOffice == EquipOrOffice);
            Thwere = Thwere.And(p => !EquId.Contains(p.Id.ToString()));

            var list = op.GetPagedList(Thwere, PageModel).ToList();

            DataTable aa = list.ToDataTable();
            DataTable ss = new DBSql.Oa.OaEquipStock().GetStateSum();

            var xx = (from a in aa.AsEnumerable()
                      join b in ss.AsEnumerable() on a.Field<int>("Id") equals b.Field<int>("EquipID")
                      into target
                      from item in target.DefaultIfEmpty()
                      select new
                      {
                          Id = a.Field<int>("Id"),
                          EquipNumber = a.Field<string>("EquipNumber"),
                          EquipName = a.Field<string>("EquipName"),
                          EquipModel = a.Field<string>("EquipModel"),
                          EquipTotalCount = a.Field<int>("EquipTotalCount") + (item == null ? 0 : item.Field<int>("RK")),// - (item == null ? 0 : item.Field<int>("BF")),
                          CreatorEmpName = a.Field<string>("CreatorEmpName"),
                          EquipNote = a.Field<string>("EquipNote"),
                          SJKC = a.Field<int>("EquipTotalCount") + (item == null ? 0 : item.Field<int>("RK")) - (item == null ? 0 : item.Field<int>("TQ")) + (item == null ? 0 : item.Field<int>("GH")) - (item == null ? 0 : item.Field<int>("BF"))
                      }).ToList();


            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = xx
            });
        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new OaEquipment();
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
                return View("infoOaEquipment", model);
            }

        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            if (model.EquipOrOffice != 1)
            {
                ViewBag.permission = "['add','submit']";
                return View("info", model);
            }
            else
            {
                ViewBag.permission = "['add','submit']";
                return View("infoOaEquipment", model);
            }
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 1;
            op.MvcDefaultDel(userInfo.EmpID);
            op.Delete(s => id.Contains(s.Id));
            op.DbContext.SaveChanges();
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id)
        {
            var model = new OaEquipment();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();

            //设备0，办公用品1
            int EquipOrOffice = TypeHelper.parseInt(Request.Form["EquipOrOffice"]);
            

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

            op.DbContext.SaveChanges();

            var ba = new DBSql.Sys.BaseAttach();
            ba.MoveFile(model.Id, userInfo.EmpID, userInfo.EmpName);
            reuslt = model.Id;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion

        #region  获取下拉列表信息
        [Description("获取下拉列表数据")]
        public string combogridJson()
        {
            int PageNum = TypeHelper.parseInt(Request.Form["page"], 10);
            int Record = TypeHelper.parseInt(Request.Form["rows"], 1);
            var condition = TypeHelper.parseString(Request.Form["keyword"]).Trim();

            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }

            int EquipOrOffice = 0;
            int.TryParse(Request["EquipOrOffice"], out EquipOrOffice);
            var Thwere = QueryBuild<DataModel.Models.OaEquipment>.True();
            Thwere = Thwere.And(p => p.EquipOrOffice == EquipOrOffice);
            var list = op.GetPagedList(Thwere,PageModel).ToList();//.GetList(p => p.Id != 0 && p.EquipOrOffice == EquipOrOffice).ToList();//要查询所有,p.Id!=0恒为true
            DataTable ss = new DBSql.Oa.OaEquipStock().GetStateSum();

            var a = (from p in list
                     where p.EquipName.Contains(condition) //&& p.EquipOrOffice == EquipOrOffice
                     join b in ss.AsEnumerable() on p.Id equals b.Field<int>("EquipID")
                     into target
                     orderby p.CreationTime, p.Id descending
                     from item in target.DefaultIfEmpty()
                     select new
                     {
                         Id = p.Id,
                         EquipNumber = p.EquipNumber,
                         EquipName = p.EquipName,
                         EquipModel = p.EquipModel,
                         EquipTotalCount = p.EquipTotalCount + (item == null ? 0 : item.Field<int>("RK")) - (item == null ? 0 : item.Field<int>("BF")),
                         CreatorEmpName = p.CreatorEmpName,
                         EquipNote = p.EquipNote
                     }).ToList();

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = a
            });
        }
        #endregion

        #region 使用情况列表
        [Description("列表")]
        public ActionResult UsageList()
        {
            return View();
        }
        #endregion

        #region 获取使用情况
        [Description("获取使用情况")]
        public string GetUsageList()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            int EquId = GetRequestValue<int>("EquId");
            OaEquipment equMent = new DBSql.Oa.OaEquipment().Get(EquId);

            var ss = new DBSql.Oa.OaEquipStock().GetPagedList(p => p.EquipID == EquId && p.EquipActionID == 2, PageModel).ToList();

            var list = new DBSql.Oa.OaEquipStock().GetPagedList(p => p.EquipID == EquId && p.EquipActionID == 2, PageModel).Select(p => new
            {
                UseEmpName = p.CreatorEmpName,
                EquName = equMent.EquipName,
                EquNumber = equMent.EquipNumber,
                EquModel = equMent.EquipModel,
                UseTime = p.EquipDateTime,
                Count = p.EquipCount
            }).ToList();

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }
        #endregion



        #region 办公用品列表
        [Description("办公用品列表")]
        public ActionResult listOaEquipment()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("OfficeSuppliesRegister")));
            return View();
        }
        #endregion

        #region 办公用品列表json
        [Description("办公用品列表列表json")]
        [HttpPost]
        public string jsonOaEquipment()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            string EquIds = "";
            if (GetRequestValue<string>("EquIds") != null)
            {
                EquIds = GetRequestValue<string>("EquIds").Trim(',');
            }

            List<string> EquId = new List<string>(EquIds.Split(','));

            var list = op.GetPagedList(p => p.DeleterEmpId == 0 && p.EquipOrOffice == 1 && !EquId.Contains(p.Id.ToString()), PageModel).ToList();

            List<string> result = PermissionList("OfficeSuppliesRegister");
            if (!result.Contains("allview") && !result.Contains("dep") && !result.Contains("emp"))
            {
                list = op.GetPagedList(p => p.CreatorEmpId == this.userInfo.EmpID && p.DeleterEmpId == 0 && p.EquipOrOffice == 1 && !EquId.Contains(p.Id.ToString()), PageModel).ToList();
            }
            else if (result.Contains("allview"))
            {

            }
            else if (result.Contains("dep"))
            {
                list = op.GetPagedList(p => p.CreatorDepId == this.userInfo.EmpDepID && p.DeleterEmpId == 0 && p.EquipOrOffice == 1 && !EquId.Contains(p.Id.ToString()), PageModel).ToList();
            }
            else if (result.Contains("emp"))
            {
                list = op.GetPagedList(p => p.CreatorEmpId == this.userInfo.EmpID && p.DeleterEmpId == 0 && p.EquipOrOffice == 1 && !EquId.Contains(p.Id.ToString()), PageModel).ToList();
            }

            //展示需要的列信息
         
            DataTable aa = list.ToDataTable();
            DataTable ss = new DBSql.Oa.OaEquipStock().GetStateSum();
             
            var xx = (from a in aa.AsEnumerable()
                      join b in ss.AsEnumerable() on a.Field<int>("Id") equals b.Field<int>("EquipID")
                      into target
                      from item in target.DefaultIfEmpty()
                      select new
                      {
                          Id = a.Field<int>("Id"),
                          EquipNumber = a.Field<string>("EquipNumber"),
                          EquipName = a.Field<string>("EquipName"),
                          EquipModel = a.Field<string>("EquipModel"),
                          EquipTotalCount = a.Field<int>("EquipTotalCount") + (item == null ? 0 : item.Field<int>("RK")) - (item == null ? 0 : item.Field<int>("BF")),
                          CreatorEmpName = a.Field<string>("CreatorEmpName"),
                          EquipNote = a.Field<string>("EquipNote"),
                          SJKC = a.Field<int>("EquipTotalCount") + (item == null ? 0 : item.Field<int>("RK"))// - (item == null ? 0 : item.Field<int>("TQ")) + (item == null ? 0 : item.Field<int>("GH")) - (item == null ? 0 : item.Field<int>("BF"))
                      }).ToList();


            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = xx
            });
        }
        #endregion

        #region 办公用品使用情况列表
        [Description("办公用品使用情况列表")]
        public ActionResult OaUsageList()
        {
            return View();
        }
        #endregion


        #region 选择领用办公用品列表json
        [Description("选择领用办公用品列表json")]
        [HttpPost]
        public string jsonOaChoose()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            string EquIds = "";
            if (GetRequestValue<string>("EquIds") != null)
            {
                EquIds = GetRequestValue<string>("EquIds").Trim(',');
            }

            List<string> EquId = new List<string>(EquIds.Split(','));

            var list = op.GetPagedList(p => p.DeleterEmpId == 0 && p.EquipOrOffice == 1 && !EquId.Contains(p.Id.ToString()), PageModel).ToList();

            //List<string> result = PermissionList("OfficeSuppliesRegister");
            //if (!result.Contains("allview") && !result.Contains("dep") && !result.Contains("emp"))
            //{
            //    list = op.GetPagedList(p => p.CreatorEmpId == this.userInfo.EmpID && p.DeleterEmpId == 0 && p.EquipOrOffice == 1 && !EquId.Contains(p.Id.ToString()), PageModel).ToList();
            //}
            //else if (result.Contains("allview"))
            //{

            //}
            //else if (result.Contains("dep"))
            //{
            //    list = op.GetPagedList(p => p.CreatorDepId == this.userInfo.EmpDepID && p.DeleterEmpId == 0 && p.EquipOrOffice == 1 && !EquId.Contains(p.Id.ToString()), PageModel).ToList();
            //}
            //else if (result.Contains("emp"))
            //{
            //    list = op.GetPagedList(p => p.CreatorEmpId == this.userInfo.EmpID && p.DeleterEmpId == 0 && p.EquipOrOffice == 1 && !EquId.Contains(p.Id.ToString()), PageModel).ToList();
            //}

            //展示需要的列信息

            DataTable aa = list.ToDataTable();
            DataTable ss = new DBSql.Oa.OaEquipStock().GetStateSum();

            var xx = (from a in aa.AsEnumerable()
                      join b in ss.AsEnumerable() on a.Field<int>("Id") equals b.Field<int>("EquipID")
                      into target
                      from item in target.DefaultIfEmpty()
                      select new
                      {
                          Id = a.Field<int>("Id"),
                          EquipNumber = a.Field<string>("EquipNumber"),
                          EquipName = a.Field<string>("EquipName"),
                          EquipModel = a.Field<string>("EquipModel"),
                          EquipTotalCount = a.Field<int>("EquipTotalCount") + (item == null ? 0 : item.Field<int>("RK")),
                          CreatorEmpName = a.Field<string>("CreatorEmpName"),
                          EquipNote = a.Field<string>("EquipNote"),
                          SJKC = a.Field<int>("EquipTotalCount") + (item == null ? 0 : item.Field<int>("RK")) - (item == null ? 0 : item.Field<int>("TQ"))
                      }).ToList();


            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = xx
            });
        }
        #endregion
    }
}
