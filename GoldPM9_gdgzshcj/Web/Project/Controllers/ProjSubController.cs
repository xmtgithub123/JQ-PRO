using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using Common.Data.Extenstions;
using System;
using System.Collections;

namespace Project.Controllers
{
    [Description("ProjSub")]
    public class ProjSubController : CoreController
    {
        private DBSql.Project.ProjSub op = new DBSql.Project.ProjSub();
        private DBSql.Project.Project project = new DBSql.Project.Project();
        DBSql.Sys.BaseEmployee dbBaseEmployee = new DBSql.Sys.BaseEmployee();
        private DBSql.Bussiness.BussContractSub bussContractSub = new DBSql.Bussiness.BussContractSub();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("项目外委登记列表(分公司)")]
        public ActionResult list()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjSubRegister")));
            return View();
        }

        [Description("项目外委登记列表(设计公司)")]
        public ActionResult list_sj()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjSubRegister_SJ")));
            return View();
        }

        [Description("项目外委登记列表(工程公司)")]
        public ActionResult list_gc()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjSubRegister_GC")));
            return View();
        }

        [Description("项目外委登记列表(创景公司)")]
        public ActionResult list_cj()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjSubRegister_CJ")));
            return View();
        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string json()
        {
            List<string> result = PermissionList("ProjSubRegister");
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            var queryInfo = Request.Form["queryInfo"];
            List<Common.Data.Extenstions.Filter> Filter = JavaScriptSerializerUtil.objectToEntity<List<Common.Data.Extenstions.Filter>>(queryInfo);

            string companyType = Request.Params["CompanyType"].ToString();
            switch (companyType)
            {
                case "SJ":
                    result = PermissionList("ProjSubRegister_SJ");
                    PageModel.SelectCondtion.Add("CompanyID", 1);
                    PageModel.SelectCondtion.Add("RefTable", "ProjSub_SJ");
                    break;
                case "GC":
                    result = PermissionList("ProjSubRegister_GC");
                    PageModel.SelectCondtion.Add("CompanyID", 2);
                    PageModel.SelectCondtion.Add("RefTable", "ProjSub_GC");
                    break;
                case "CJ":
                    result = PermissionList("ProjSubRegister_CJ");
                    PageModel.SelectCondtion.Add("CompanyID", 3);
                    PageModel.SelectCondtion.Add("RefTable", "ProjSub_CJ");
                    break;
                default:
                    result = PermissionList("ProjSubRegister");
                    PageModel.SelectCondtion.Add("CompanyID", 0);
                    PageModel.SelectCondtion.Add("RefTable", "ProjSub");
                    break;
            }

            if (queryInfo.isNotEmpty())
            {
                foreach (Common.Data.Extenstions.Filter item in Filter)
                {
                    Common.Data.Extenstions.FilterChilde _child = item.list[0];
                    if (_child.Key == "txtLike")
                    {
                        PageModel.TextCondtion = _child.Value;
                    }
                    else if (_child.Key == "ColAttDate1S")
                    {
                        PageModel.SelectCondtion.Add("ColAttDate1S", _child.Value);
                    }
                    else if (_child.Key == "ColAttDate1E")
                    {
                        PageModel.SelectCondtion.Add("ColAttDate1E", _child.Value);
                    }
                    else if (_child.Key == "CreationTimeS")
                    {
                        PageModel.SelectCondtion.Add("CreationTimeS", _child.Value);
                    }
                    else if (_child.Key == "CreationTimeE")
                    {
                        PageModel.SelectCondtion.Add("CreationTimeE", _child.Value);
                    }
                    else if (_child.Key == "ProjSubTypeState")
                    {
                        PageModel.SelectCondtion.Add("ProjSubTypeState", _child.Value);
                    }
                }
            }
            PageModel.SelectCondtion.Add("ConSubID", Request.Form["ConSubID"]);
            PageModel.SelectCondtion.Add("IsAudit", TypeHelper.parseInt(Request.Form["IsAudit"]));
            PageModel.SelectCondtion.Add("FilterConSubID", TypeHelper.parseInt(Request.Form["FilterConSubID"]));
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "s.Id desc";
            }
            if (!result.Contains("allview") && !result.Contains("dep") && !result.Contains("emp"))
            {
                PageModel.SelectCondtion.Add("QueryEmpID", this.userInfo.EmpID);
            }
            else if (result.Contains("allview"))
            {

            }
            else if (result.Contains("dep"))
            {
                PageModel.SelectCondtion.Add("QueryDeptID", this.userInfo.EmpDepID);
            }
            else if (result.Contains("emp"))
            {
                PageModel.SelectCondtion.Add("QueryEmpID", this.userInfo.EmpID);
            }

            var dt = op.GetListInfo(PageModel, this.userInfo);

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = dt
            });
        }
        #endregion       

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {

            Hashtable ht = new Hashtable();
            ht.Add("1", "是");
            ht.Add("2", "否");
            ViewBag.SubQualifiedDirectory = new SelectList(ht, "key", "value");

            var model = new ProjSub();
            model.TableNumber = "表SCX03－01 ";
            ViewData["SubEmpId1"] = model.SubEmpId.ToString();
            ViewData["SubEmpId2"] = model.SubEmpId.ToString();
            model.CreationTime = DateTime.Now;
            model.CreatorEmpName = userInfo.EmpName;
            model.CreatorDepId = userInfo.EmpDepID;
            ViewBag.CreatorEmpId = model.CreatorEmpId;

            string page = "info";
            string companyType = Request.Params["CompanyType"].ToString();
            switch (companyType)
            {
                case "SJ":
                    {
                        page = "info_sj"; ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjSubRegister_SJ")));
                    }
                    break;
                case "GC":
                    {
                        page = "info_gc"; ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjSubRegister_GC")));
                    }
                    break;
                case "CJ":
                    {
                        page = "info_cj"; ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjSubRegister_CJ")));
                    }
                    break;
                default:
                    {
                        page = "info"; ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjSubRegister")));
                    }
                    break;
            }

            List<SelectListItem> items = new List<SelectListItem>();
            SelectListItem item1 = new SelectListItem();
            item1.Value = "1"; item1.Text = "是"; items.Add(item1);
            SelectListItem item2 = new SelectListItem();
            item2.Value = "2"; item2.Text = "否"; items.Add(item2);
            ViewBag.SubQualifiedDirectory = new SelectList(items, "Value", "Text", model.SubQualifiedDirectory);

            return View(page, model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {


            var model = op.Get(id);
            ViewData["SubEmpId1"] = model.SubEmpId.ToString();
            ViewData["SubEmpId2"] = dbBaseEmployee.GetBaseEmployeeInfo(model.SubEmpId) == null ? "0" : dbBaseEmployee.GetBaseEmployeeInfo(model.SubEmpId).EmpDepID.ToString();

            var _porject = project.Get(model.ProjID);
            ViewBag.ProjInfo = "项目编号：" + (_porject != null ? _porject.ProjNumber : "") + ",项目名称:" + (_porject != null ? _porject.ProjName : "");
            ViewBag.CreatorEmpId = model.CreatorEmpId;

            string page = "info";
            string companyType = Request.Params["CompanyType"].ToString();
            switch (companyType)
            {
                case "SJ":
                    {
                        page = "info_sj"; ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjSubRegister_SJ")));
                    }
                    break;
                case "GC":
                    {
                        page = "info_gc"; ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjSubRegister_GC")));
                    }
                    break;
                case "CJ":
                    {
                        page = "info_cj"; ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjSubRegister_CJ")));
                    }
                    break;
                default:
                    {
                        page = "info"; ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjSubRegister")));
                    }
                    break;
            }
            //model.SubFactFinishDate


            List<SelectListItem> items = new List<SelectListItem>();
            SelectListItem item1 = new SelectListItem(); item1.Value = "1"; item1.Text = "是"; items.Add(item1);
            SelectListItem item2 = new SelectListItem(); item2.Value = "2"; item2.Text = "否"; items.Add(item2);
            ViewBag.SubQualifiedDirectory = new SelectList(items, "Value", "Text", model.SubQualifiedDirectory);

            return View(page, model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 0;
            try
            {
                op.UpdateProjSubInfoList(id, this.userInfo);
                op.UnitOfWork.SaveChanges();

                new DBSql.PubFlow.Flow().Delete(id, "ProjSub");
                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.Delete(id, "ProjSub");
                }
                reuslt = 1;
            }
            catch
            {
            }
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id)
        {
            var model = new ProjSub();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();
            model.SubEmpId = TypeHelper.parseInt(Request.Form["SubEmpId"].Split('-')[0]);
            int reuslt = 0;
            if (model.Id > 0)
            {
                op.Edit(model);
            }
            else
            {
                op.Add(model);
            }
            op.UnitOfWork.SaveChanges();
            reuslt = model.Id;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }

        #endregion

        #region 筛选项目外委列表json
        public ActionResult selectprojsub(int typeID, int FilterConSubID, int isMultiSelect = 0)
        {
            ViewBag.TypeID = typeID;
            ViewBag.FilterConSubID = FilterConSubID;
            ViewBag.IsMultiSelect = isMultiSelect;
            return View();

        }
        #endregion

        #region 获得项目与外委的列表
        public string GetProjSubList(FormCollection Tcondition)
        {
            string ProjSubIDs = "";
            if (Tcondition["ProjSubIDs"] != null)
            {
                ProjSubIDs = Tcondition["ProjSubIDs"].ToString();
            }
            else if (Tcondition["ProjSubIDs[]"] != null)
            {
                ProjSubIDs = Tcondition["ProjSubIDs[]"].ToString();
            }
            int[] Id = (from n in ProjSubIDs.Split(',') where n != "" select Common.ExtensionMethods.Value<int>(n)).ToArray();
            var TWhere = QueryBuild<DataModel.Models.ProjSub>.True();
            TWhere = TWhere.And(p => p.DeleterEmpId == 0);
            if (Id.Length == 0)
            {
                TWhere = TWhere.And(p => p.Id == -1);
            }
            else
            {
                var Ids = string.Join(",", Id.Select(p => p.ToString()));
                TWhere = TWhere.And(p => Id.Contains(p.Id));
            }

            var list = new DBSql.Project.ProjSub().GetList(TWhere).Select(p => new
            {
                Id = p.Id,
                p.SubNumber,
                p.SubName,
                ProjNumber = p.FK_ProjSub_ProjID == null ? "" : p.FK_ProjSub_ProjID.ProjNumber,
                ProjName = p.FK_ProjSub_ProjID == null ? "" : p.FK_ProjSub_ProjID.ProjName,
                p.SubFee,
                p.SubPlanFinishDate,
                p.SubFactFinishDate,
                CustName = p.FK_ProjSub_ColAttType2 != null ? p.FK_ProjSub_ColAttType2.CustName : "",
                ConSubNumber = p.FK_ProjSub_ConSubID != null ? p.FK_ProjSub_ConSubID.ConSubNumber : "",
                ConSubName = p.FK_ProjSub_ConSubID != null ? p.FK_ProjSub_ConSubID.ConSubName : "",
                p.CreatorDepName,
                p.ColAttDate1,
                p.CreationTime
            }).ToList();

            return JavaScriptSerializerUtil.getJson(new
            {
                total = list.Count,
                rows = list

            });
        }

        #endregion

        #region 获得外委合同信息的json
        public string GetJson()
        {
            int PageNum = TypeHelper.parseInt(Request.Form["page"]);
            int Record = TypeHelper.parseInt(Request.Form["rows"]);
            var condition = TypeHelper.parseString(Request.Form["keyword"]).Trim();

            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            var list = op.GetQuery().ToList();

            List<DataModel.Models.ProjSub> result = (from item in list
                                                     where item.SubName.Contains(condition)    //条件查询
                                                     select item).ToList<DataModel.Models.ProjSub>();
            //没有外委合同的外键，只能连接查询
            var conSub = bussContractSub.GetList(c => true).ToList();

            var a = (from item in result
                     join c in conSub
                     on item.ConSubID equals c.Id
                     select new
                     {
                         item.Id,
                         item.SubName,
                         item.SubNumber,
                         item.SubPhase,
                         item.SubSpecial,
                         item.SubType,
                         CustName = item.FK_ProjSub_ColAttType2.CustName,
                         ConSubName = c.ConSubName,
                         ConSubNumber = c.ConSubNumber
                     }).ToList();
            var t = (a.Skip(Record * (PageNum - 1)).Take(Record)).ToList();
            return JavaScriptSerializerUtil.getJson(new
            {
                total = a.Count,
                rows = (a.Skip(Record * (PageNum - 1)).Take(Record)).ToList()
            });

        }
        #endregion

        #region 验证外委编号是否重复
        public int GetSubNumberCount(string Number, int Id)
        {
            int Count = op.GetProjCodeCount(Number, Id);
            return Count;
        }

        #endregion

        #region 更新projSub的合同编号
        public void UpdateProjSubByConSubIDs(string Ids)
        {
            op.UpdateProjSubByConSubIDs(Ids);
        }

        #endregion

        #region  外委项目  下拉选择)
        public string ProjSubComboGrid()
        {
            int PageNum = TypeHelper.parseInt(Request.Form["page"], 1);
            int Record = TypeHelper.parseInt(Request.Form["rows"], 10);
            var condition = TypeHelper.parseString(Request.Params["Words"]).Trim();
            int projSubID = TypeHelper.parseInt(Request.Params["projId"]);//将选中的记录放在第一位置
            object obj = null;//存放选中的数据对象
            var projSubList = op.GetList(p => p.DeleterEmpId == 0 && p.Id != projSubID).ToList();
            var projIDList = projSubList.Select(p => p.ProjID).Distinct().ToList();
            var projList = project.GetList(p => p.DeleterEmpId == 0 && projIDList.Contains(p.Id)).ToList();
            var a = from projSub in projSubList
                    join proj in projList on projSub.ProjID equals proj.Id
                    where projSub.SubNumber.Contains(condition) || projSub.SubName.Contains(condition) ||
                          proj.ProjNumber.Contains(condition) || proj.ProjName.Contains(condition)// 检索类型 （项目编号、项目名称、外委编号、外委名称）
                    orderby projSub.CreationTime descending, projSub.Id descending
                    select new
                    {
                        projSub.Id,//外委ID
                        projSub.SubNumber,//外委项目编号
                        projSub.SubName,// 外委项目名称
                        proj.ProjNumber,// 项目编号
                        proj.ProjName,//项目名称
                        projSubTypeName = projSub.FK_ProjSub_SubType == null ? "" : projSub.FK_ProjSub_SubType.BaseName,//外委性质
                        projSubPhaseName = project.GetProjPhase(projSub.SubPhase)//外委项目阶段
                    };

            List<dynamic> dyList = new List<dynamic>();
            DataModel.Models.ProjSub modelProjSub = op.Get(projSubID);
            if (modelProjSub != null)
            {
                string ProjNumber = modelProjSub.FK_ProjSub_ProjID == null ? "" : modelProjSub.FK_ProjSub_ProjID.ProjNumber;
                string ProjName = modelProjSub.FK_ProjSub_ProjID == null ? "" : modelProjSub.FK_ProjSub_ProjID.ProjName;
                string projSubTypeName = modelProjSub.FK_ProjSub_SubType == null ? "" : modelProjSub.FK_ProjSub_SubType.BaseName;
                string projSubPhaseName = project.GetProjPhase(modelProjSub.SubPhase);
                obj = new
                {
                    modelProjSub.Id,
                    modelProjSub.SubNumber,
                    modelProjSub.SubName,
                    ProjNumber,
                    ProjName,
                    projSubTypeName,
                    projSubPhaseName
                };
                dyList.Add(obj);
            }
            dyList.AddRange(a);
            return JavaScriptSerializerUtil.getJson(new
            {
                total = dyList.Count,
                rows = (dyList.Skip(Record * (PageNum - 1)).Take(Record)).ToList()
            });
        }

        #endregion

        #region 获得项目与外委的列表
        public string GetProjSubContractList(FormCollection Tcondition)
        {

            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            PageModel.ToPageData = false;
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "s.Id desc";
            }
            PageModel.SelectCondtion.Add("ProjSubID", Tcondition["ProjSubIDs"].ToString());

            var dt = op.GetProjSubContractList(PageModel);

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = dt
            });
        }

        #endregion
    }
}
