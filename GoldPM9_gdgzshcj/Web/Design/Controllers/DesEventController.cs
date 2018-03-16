using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System;
using System.Web;
using System.Reflection;
using System.Data;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Design.Controllers
{
    [Description("DesEvent")]
    public partial class DesEventController : CoreController
    {
        private DBSql.Design.DesEvent op = new DBSql.Design.DesEvent();
        private DBSql.Design.DesTaskGroup desTaskGroupDB = new DBSql.Design.DesTaskGroup();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("项目记事列表")]
        public ActionResult list()
        {
            return View();
        }
        #endregion

        #region 项目选择
        [Description("项目选择")]
        public ActionResult ChooseProjectList()
        {
            if (Request.Params["CompanyType"] != null)
            {
                ViewBag.CompanyType = Request.Params["CompanyType"].ToString();
            }
            else
            {
                ViewBag.CompanyType = "";
            }
            return View();
        }
        #endregion


        public ActionResult ProjEventList(int projID, int taskGroupId)
        {
            ViewBag.projID = projID;
            ViewBag.taskGroupId = taskGroupId;
            return View();
        }


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
            Expression<Func<DesEvent, bool>> predicate = GetExpression(Request);
            var list = op.GetPagedList(predicate, PageModel).ToList();
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }

        private Expression<Func<DesEvent, bool>> GetExpression(HttpRequestBase request)
        {
            List<Expression> exprList = new List<Expression>();
            ParameterExpression paramExpr = Expression.Parameter(typeof(DesEvent), "f");

            if (1 == 1)
            {
                MemberExpression selector = Expression.Property(paramExpr, typeof(DesEvent).GetProperty("DeleterEmpId"));
                ConstantExpression nameValueExpr = Expression.Constant(0, typeof(Int32));
                Expression filter = Expression.Equal(selector, nameValueExpr);
                exprList.Add(filter);
            }
            if (request.Form["text"] != null && !string.IsNullOrEmpty(request.Form["text"].ToString()))
            {
                MemberExpression namePropExpr = Expression.Property(paramExpr, typeof(DesEvent).GetProperty("EventTitle"));
                MethodInfo containsMethod = typeof(string).GetMethod("Contains");
                ConstantExpression nameValueExpr = Expression.Constant(request.Form["text"].ToString(), typeof(string));
                MethodCallExpression nameContainsExpr = Expression.Call(namePropExpr, containsMethod, nameValueExpr);
                exprList.Add(nameContainsExpr);
            }
            if (request.Form["taskGroupId"] != null)
            {
                MemberExpression selector = Expression.Property(paramExpr, typeof(DesEvent).GetProperty("EventRefId"));
                ConstantExpression nameValueExpr = Expression.Constant(Convert.ToInt32(request.Form["taskGroupId"].ToString()), typeof(Int32));
                Expression filter = Expression.Equal(selector, nameValueExpr);
                exprList.Add(filter);
            }

            List<string> result = PermissionList("EngineerLog");
            if (!result.Contains("allview") && !result.Contains("dep") && !result.Contains("emp"))
            {
                MemberExpression selector = Expression.Property(paramExpr, typeof(DesEvent).GetProperty("CreatorEmpId"));
                ConstantExpression nameValueExpr = Expression.Constant(this.userInfo.EmpID, typeof(Int32));
                Expression filter = Expression.Equal(selector, nameValueExpr);
                exprList.Add(filter);
            }
            else if (result.Contains("allview"))
            {

            }
            else if (result.Contains("dep"))
            {
                MemberExpression selector = Expression.Property(paramExpr, typeof(DesEvent).GetProperty("CreatorDepId"));
                ConstantExpression nameValueExpr = Expression.Constant(this.userInfo.EmpDepID, typeof(Int32));
                Expression filter = Expression.Equal(selector, nameValueExpr);
                exprList.Add(filter);
            }
            else if (result.Contains("emp"))
            {
                MemberExpression selector = Expression.Property(paramExpr, typeof(DesEvent).GetProperty("CreatorEmpId"));
                ConstantExpression nameValueExpr = Expression.Constant(this.userInfo.EmpID, typeof(Int32));
                Expression filter = Expression.Equal(selector, nameValueExpr);
                exprList.Add(filter);
            }


            Expression whereExpr = null;
            foreach (var expr in exprList)
            {
                if (whereExpr == null) whereExpr = expr;
                else whereExpr = Expression.And(whereExpr, expr);
            }
            Expression<Func<DesEvent, bool>> lambda;
            if (whereExpr != null)
                lambda = Expression.Lambda<Func<DesEvent, bool>>(whereExpr, paramExpr);
            else
                lambda = f => true;
            return lambda;
        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new DesEvent();
            model.EventRefId = Convert.ToInt32(Request.QueryString["taskGroupId"].ToString());
            model.EventRefTable = "ProjEvent";
            List<string> result = PermissionList("EngineerLog");
            if (result.Contains("edit") || result.Contains("alledit"))//编辑权或者维护权
            {
                ViewBag.editPermission = "['submit','close']";
            }
            else
            {
                ViewBag.editPermission = "['close']";
            }

            int empId = TypeHelper.parseInt(Request["empId"]);

            var taskGroup = new DBSql.Design.DesTaskGroup().Get(model.EventRefId);

            ViewBag.ProjName = taskGroup.ProjName;
            ViewBag.ProjNumber = taskGroup.ProjNumber;
            if (Request["fromType"] != "1")
                return View("info", model);
            else
                return View("info1", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {

            var model = op.Get(id);
            DBSql.Design.DesEventAlert desEventAlert = new DBSql.Design.DesEventAlert();
            var list = desEventAlert.GetList(d => d.EventId == id).ToList();
            ViewBag.list = list;
            List<string> result = PermissionList("EngineerLog");
            if (result.Contains("edit") || result.Contains("alledit"))//编辑权或者维护权
            {
                ViewBag.editPermission = "['submit','close']";
            }
            else
            {
                ViewBag.editPermission = "['close']";
            }
            ViewBag.ActionFlag = "Edit";
            if(GetRequestValue<int>("View")!=0)
            {
                ViewBag.IsView = "1";
            }
            return View("info", model);
        }
        #endregion

        #region 查看
        [Description("查看")]
        public ActionResult editView(int id)
        {

            var model = op.Get(id);
            DBSql.Design.DesEventAlert desEventAlert = new DBSql.Design.DesEventAlert();
            var list = desEventAlert.GetList(d => d.EventId == id).ToList();
            ViewBag.list = list;
            List<string> result = PermissionList("EngineerLog");
            if (result.Contains("edit") || result.Contains("alledit"))//编辑权或者维护权
            {
                ViewBag.editPermission = "['submit','close']";
            }
            else
            {
                ViewBag.editPermission = "['close']";
            }
            ViewBag.ActionFlag = "Edit";
            if (GetRequestValue<int>("View") != 0)
            {
                ViewBag.IsView = "1";
            }
            return View("infoView", model);
        }
        #endregion

        #region 查看
        [Description("查看")]
        public string editInfo(int id)
        {
            var model = op.Get(id);
            DataModel.Models.DesEvent mdlEvent = new DataModel.Models.DesEvent();
            Common.ModelConvert.MvcChangeTarget<DataModel.Models.DesEvent, DataModel.Models.DesEvent>(mdlEvent, model);
            ViewBag.model = mdlEvent;
            DBSql.Design.DesEventAlert desEventAlert = new DBSql.Design.DesEventAlert();
            var list = desEventAlert.GetList(d => d.EventId == id).ToList();
            ViewBag.list = list;
            return JavaScriptSerializerUtil.getJson(ViewBag);
        }
        #endregion


        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 0;
            try
            {
                //op.Delete(s => id.Contains(s.Id));
                op.UpdateDesEventList(id, this.userInfo);
                op.UnitOfWork.SaveChanges();
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
            var model = new DesEvent();
            if (Id < 1)
            {

                DBSql.OA.OaMess oaMess = new DBSql.OA.OaMess();
                oaMess.DbContextRepository(op.UnitOfWork, op.DbContext);
                DBSql.Oa.OaMessRead oaMessRead = new DBSql.Oa.OaMessRead();
                oaMessRead.DbContextRepository(op.UnitOfWork, op.DbContext);
                DBSql.Design.DesEventAlert desEventAlert = new DBSql.Design.DesEventAlert();
                desEventAlert.DbContextRepository(op.UnitOfWork, op.DbContext);

                DBSql.Sys.BaseAttach baseAttach = new DBSql.Sys.BaseAttach();
                baseAttach.DbContextRepository(op.UnitOfWork, op.DbContext);

                try
                {
                    op.UnitOfWork.BeginTransaction();

                    string projData = Request.Form["hidGridData"];

                    DataTable dt = JavaScriptSerializerUtil.JsonToDataTable(projData);

                    int count = 0;

                    List<int> ids = new List<int>();

                    foreach (DataRow row in dt.Rows)
                    {
                        count++;
                        model = new DesEvent();
                        model.MvcSetValue();
                        model.MvcDefaultSave(userInfo);
                        model.EventRefId = TypeHelper.parseInt(row["TaskGroupId"].ToString());
                        model.ProjId = TypeHelper.parseInt(row["Id"].ToString());
                        op.Add(model);

                        op.DbContext.SaveChanges();

                        int id = model.Id;
                        var list = GetData(Request);
                        SaveEventAlert(id, list, desEventAlert);

                        SendMessage(list, model, oaMess, oaMessRead);

                        if (count == 1)
                        {
                            using (var ba = new DBSql.Sys.BaseAttach())
                            {
                                ba.MoveFile(model.Id, userInfo.EmpID, userInfo.EmpName);
                            }
                        }
                        ids.Add(model.Id);
                    }

                    if (ids.Count > 0)
                    {
                        IEnumerable<BaseAttach> baseAttack = new DBSql.Sys.BaseAttach().GetBaseAttachList(ids[0], "DesEvent");

                        for (int i = 1; i < ids.Count; i++)
                        {
                            foreach (BaseAttach ba in baseAttack)
                            {
                                BaseAttach newBaseAttack = new BaseAttach();
                                newBaseAttack.AttachDateChange = ba.AttachDateChange;
                                newBaseAttack.AttachDateUpload = ba.AttachDateUpload;
                                newBaseAttack.AttachDir = ba.AttachDir;
                                newBaseAttack.AttachEmpID = ba.AttachEmpID;
                                newBaseAttack.AttachEmpName = ba.AttachEmpName;
                                newBaseAttack.AttachExt = ba.AttachExt;
                                newBaseAttack.AttachGrade = ba.AttachGrade;
                                newBaseAttack.AttachGroupID = ba.AttachGroupID;
                                newBaseAttack.AttachLevel = ba.AttachLevel;
                                newBaseAttack.AttachName = ba.AttachName;
                                newBaseAttack.AttachOrderNum = ba.AttachOrderNum;
                                newBaseAttack.AttachOrderPath = ba.AttachOrderPath;
                                newBaseAttack.AttachParentID = ba.AttachParentID;
                                newBaseAttack.AttachPathIDs = ba.AttachPathIDs;
                                newBaseAttack.AttachRefID = ids[i];
                                newBaseAttack.AttachRefTable = ba.AttachRefTable;
                                newBaseAttack.AttachSize = ba.AttachSize;
                                newBaseAttack.AttachTag = ba.AttachTag;
                                newBaseAttack.AttachVer = ba.AttachVer;
                                newBaseAttack.ColAttType1 = ba.ColAttType1;
                                newBaseAttack.ColAttType2 = ba.ColAttType2;
                                newBaseAttack.ColAttXml = ba.ColAttXml;
                                baseAttach.Add(newBaseAttack);
                            }
                        }
                        baseAttach.DbContext.SaveChanges();
                    }

                    op.UnitOfWork.CommitTransaction();

                    DoResult dr = DoResultHelper.doSuccess(model.Id, "操作成功");
                    return Json(dr);
                }
                catch (Exception ex)
                {
                    op.UnitOfWork.RollBackTransaction();
                    DoResult dr = DoResultHelper.doSuccess(model.Id, "操作失败");
                    return Json(dr);
                }
            }
            else
            {
                try
                {
                    op.UnitOfWork.BeginTransaction();
                    string projData = Request.Form["hidGridData"];

                    DataTable dt = JavaScriptSerializerUtil.JsonToDataTable(projData);

                    foreach (DataRow row in dt.Rows)
                    {
                        model = new DBSql.Design.DesEvent().Get(Id);
                        model.MvcSetValue();
                        model.MvcDefaultSave(userInfo);
                        op.Edit(model);

                        op.DbContext.SaveChanges();

                        using (var ba = new DBSql.Sys.BaseAttach())
                        {
                            ba.MoveFile(model.Id, userInfo.EmpID, userInfo.EmpName);
                        }
                    }

                    op.UnitOfWork.CommitTransaction();
                    DoResult dr = DoResultHelper.doSuccess(model.Id, "操作成功");
                    return Json(dr);
                }
                catch (Exception ex)
                {
                    op.UnitOfWork.RollBackTransaction();
                    DoResult dr = DoResultHelper.doSuccess(model.Id, "操作失败："+ex.Message);
                    return Json(dr);
                }
            }
        }


        private void SendMessage(List<EmpAndDept> list, DesEvent model, DBSql.OA.OaMess oaMess, DBSql.Oa.OaMessRead oaMessRead)
        {
            var _oaMess = new DataModel.Models.OaMess()
            {
                MessDate = DateTime.Now,
                MessEmpId = userInfo.EmpID,
                MessEmpName = userInfo.EmpName,
                MessIsAutoReturn = false,
                MessIsDeleted = false,
                MessIsSystem = true,
                MessLinkTitle = model.EventTitle,
                MessLinkUrl = string.Format("design/DesEvent/editView?id={0}&View=1", model.Id),
                MessNote = model.EventContent,
                MessRefID = model.Id,
                MessRefTable = "DesEvent",
                MessTag = "",
                MessTitle = model.EventTitle
            };
            oaMess.Add(_oaMess);
            oaMess.DbContext.SaveChanges();
            foreach (var item in list)
            {
                oaMessRead.Add(new OaMessRead()
                {
                    Id = _oaMess.Id,
                    MessReadDate = DateTime.Parse("1900-01-01"),
                    MessReadEmpId = item.empID,
                    MessReadEmpName = item.empName,
                    MessReadIsDeleted = false,
                    MessReadNote = ""
                });
            }
            DBSql.Oa.OaMessRead.CacheRemove();
            var t = JQ.Util.IO.MessageMonitor.NotifyAsync(list.Select(x => x.empID).ToList(), delegate (int empID)
            {
                return new DBSql.Oa.OaMessRead().GetNotifyDatas(empID);
            });
        }

        private List<EmpAndDept> GetData(HttpRequestBase request)
        {
            List<EmpAndDept> list = new List<EmpAndDept>();
            DBSql.Sys.BaseEmployee be = new DBSql.Sys.BaseEmployee();
            DBSql.Sys.BaseData bd = new DBSql.Sys.BaseData();
            if (request.Form["SubEmpId"] != null)
            {
                string[] strs = request.Form["SubEmpId"].ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string str in strs)
                {
                    string[] items = str.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                    var obj = new EmpAndDept
                    {
                        empID = Convert.ToInt32(items[0]),
                        empName = be.GetBaseEmployeeInfo(Convert.ToInt32(items[0])).EmpName,
                        deptID = Convert.ToInt32(items[1]),
                        deptName = bd.GetBaseDataInfo(Convert.ToInt32(items[1])).BaseName
                    };
                    list.Add(obj);
                }
            }
            return list;
        }

        private void SaveEventAlert(int id, List<EmpAndDept> list, DBSql.Design.DesEventAlert desEventAlert)
        {
            DBSql.Sys.BaseEmployee be = new DBSql.Sys.BaseEmployee();
            DBSql.Sys.BaseData bd = new DBSql.Sys.BaseData();
            if (list != null)
            {
                foreach (EmpAndDept item in list)
                {
                    var model = new DesEventAlert()
                    {
                        EventId = id,
                        AlertEmpId = item.empID,
                        AlertEmpName = item.empName,
                        AlertEmpDepId = item.deptID,
                        AlertEmpDepName = item.deptName,
                        IsRead = false,
                        IsReceive = false,
                        ReceiveContent = ""
                    };
                    desEventAlert.Add(model);
                }
            }
            desEventAlert.DbContext.SaveChanges();

        }
        #endregion
    }

    public class EmpAndDept
    {
        public int empID { get; set; }
        public string empName { get; set; }

        public int deptID { get; set; }

        public string deptName { get; set; }
    }
}
