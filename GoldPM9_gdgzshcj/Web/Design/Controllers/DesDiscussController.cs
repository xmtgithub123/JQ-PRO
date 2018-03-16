using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;

namespace Design.Controllers
{
    [Description("DesDiscuss")]
    public partial class DesDiscussController : CoreController
    {
        private DBSql.Design.DesDiscuss op = new DBSql.Design.DesDiscuss();
        private DBSql.Design.DesDiscussReply desDiscussReply = new DBSql.Design.DesDiscussReply();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            return View();
        }
        #endregion

        public ActionResult ProjDiscussList(int projID, int taskGroupId)
        {
            ViewBag.projID = projID;
            ViewBag.taskGroupId = taskGroupId;
            return View();
        }

        public ActionResult ProjDiscussListView(int id)
        {
            var model = op.Get(id);
            model.ReadCount = model.ReadCount + 1;
            op.Edit(model);
            op.UnitOfWork.SaveChanges();
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.TotalReply = op.GetTotalReply(model.Id);
            return View(model);
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
            Expression<Func<DesDiscuss, bool>> predicate = GetExpression(Request);
            var list = op.GetPagedList(predicate, PageModel).ToList();
            var target = (from item in list
                          select new
                          {
                              item.Id,
                              item.TalkTitle,
                              item.ReplyCount,
                              item.ReadCount,
                              HY = item.ReplyCount + "/" + item.ReadCount,
                              item.CreatorEmpName,
                              item.CreationTime,
                              item.TalkContent,
                              item.CreatorEmpId,
                          });

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = target
            });
        }
        #endregion

        private Expression<Func<DesDiscuss, bool>> GetExpression(HttpRequestBase request)
        {
            List<Expression> exprList = new List<Expression>();
            ParameterExpression paramExpr = Expression.Parameter(typeof(DesDiscuss), "f");
            if (1 == 1)
            {
                MemberExpression selector = Expression.Property(paramExpr, typeof(DesDiscuss).GetProperty("DeleterEmpId"));
                ConstantExpression nameValueExpr = Expression.Constant(0, typeof(Int32));
                Expression filter = Expression.Equal(selector, nameValueExpr);
                exprList.Add(filter);
            }

            if (request.Form["text"] != null && !string.IsNullOrEmpty(request.Form["text"].ToString()))
            {
                MemberExpression namePropExpr = Expression.Property(paramExpr, typeof(DesDiscuss).GetProperty("TalkTitle"));
                MethodInfo containsMethod = typeof(string).GetMethod("Contains");
                ConstantExpression nameValueExpr = Expression.Constant(request.Form["text"].ToString(), typeof(string));
                MethodCallExpression nameContainsExpr = Expression.Call(namePropExpr, containsMethod, nameValueExpr);
                exprList.Add(nameContainsExpr);
            }
            if (request.Form["taskGroupId"] != null)
            {
                MemberExpression selector = Expression.Property(paramExpr, typeof(DesDiscuss).GetProperty("TalkRefId"));
                ConstantExpression nameValueExpr = Expression.Constant(Convert.ToInt32(request.Form["taskGroupId"].ToString()), typeof(Int32));
                Expression filter = Expression.Equal(selector, nameValueExpr);
                exprList.Add(filter);
            }

            List<string> result = PermissionList("ProjDiscuss");
            if (!result.Contains("allview") && !result.Contains("dep") && !result.Contains("emp"))
            {
                MemberExpression selector = Expression.Property(paramExpr, typeof(DesDiscuss).GetProperty("CreatorEmpId"));
                ConstantExpression nameValueExpr = Expression.Constant(this.userInfo.EmpID, typeof(Int32));
                Expression filter = Expression.Equal(selector, nameValueExpr);
                exprList.Add(filter);
            }
            else if (result.Contains("allview"))
            {

            }
            else if (result.Contains("dep"))
            {
                MemberExpression selector = Expression.Property(paramExpr, typeof(DesDiscuss).GetProperty("CreatorDepId"));
                ConstantExpression nameValueExpr = Expression.Constant(this.userInfo.EmpDepID, typeof(Int32));
                Expression filter = Expression.Equal(selector, nameValueExpr);
                exprList.Add(filter);
            }
            else if (result.Contains("emp"))
            {
                MemberExpression selector = Expression.Property(paramExpr, typeof(DesDiscuss).GetProperty("CreatorEmpId"));
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
            Expression<Func<DesDiscuss, bool>> lambda;
            if (whereExpr != null)
                lambda = Expression.Lambda<Func<DesDiscuss, bool>>(whereExpr, paramExpr);
            else
                lambda = f => true;
            return lambda;
        }

        public string GetReplyList()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            int talkID = TypeHelper.parseInt(Request.Form["talkId"], 0);
            if (talkID == 0)
            {
                throw new ArgumentException("参数错误");
            }
            var list = desDiscussReply.GetPagedList(r => r.TalkId == talkID && r.DeleterEmpId == 0, PageModel).ToList();

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new DesDiscuss();
            model.TalkRefId = Convert.ToInt32(Request.QueryString["taskGroupId"].ToString());
            model.TalkRefTable = "ProjDiscuss";
            return View("info", model);
        }
        #endregion

        //#region 添加
        //[Description("添加")]
        //public ActionResult addApp(int[] id)
        //{
        //    int reuslt = 0;
        //    try
        //    {
        //        var model = new DesDiscuss();
        //        model.
        //        op.UpdateDesDiscussList(id, this.userInfo);
        //        op.UnitOfWork.SaveChanges();
        //        reuslt = 1;
        //    }
        //    catch
        //    {

        //    }
        //    DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
        //    return Json(dr);
        //}
        //#endregion

        public ActionResult addReply()
        {
            var model = new DesDiscussReply();
            model.TalkId = Convert.ToInt32(Request.QueryString["TalkId"].ToString());
            return View("infoReply", model);
        }


        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            if (Request.QueryString["view"] == "1")
            {
                ViewBag.ViewMode = 1;
            }
            return View("info", model);
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
                op.UpdateDesDiscussList(id, this.userInfo);
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
        #region 删除
        [Description("删除")]
        public ActionResult delReply(int[] id)
        {
            int reuslt = 0;
            DBSql.Design.DesDiscuss dd = new DBSql.Design.DesDiscuss();
            dd.DbContextRepository(desDiscussReply.UnitOfWork, desDiscussReply.DbContext);
            try
            {
                dd.UnitOfWork.BeginTransaction();
                int talkId = -1;
                if (id.Length > 0)
                {
                    int replyId = id[0];
                    var replyModel = new DBSql.Design.DesDiscussReply().FirstOrDefault(p => p.Id == replyId);
                    if (replyModel != null)
                    {
                        talkId = replyModel.TalkId;
                    }
                }

                desDiscussReply.UpdateDesDiscussReplyList(id, this.userInfo);
                desDiscussReply.DbContext.SaveChanges();

                if (talkId > 0)
                {
                    var model = dd.FirstOrDefault(p => p.Id == talkId);
                    if (model != null)
                    {
                        model.ReplyCount = desDiscussReply.GetList(p => p.TalkId == talkId && p.DeleterEmpId == 0).Count();
                        dd.Edit(model);
                        dd.DbContext.SaveChanges();
                    }
                }

                dd.UnitOfWork.CommitTransaction();
                reuslt = 1;

            }
            catch (Exception e)
            {
                dd.UnitOfWork.RollBackTransaction();
                reuslt = 0;
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
            var model = new DesDiscuss();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();

            int reuslt = 0;
            if (model.Id > 0)
            {
                model.MvcDefaultEdit(userInfo);
                op.Edit(model);
            }
            else
            {
                model.MvcDefaultSave(userInfo);
                op.Add(model);
            }
            op.UnitOfWork.SaveChanges();
            reuslt = model.Id;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion

        public JsonResult saveReply()
        {
            var model = new DesDiscussReply();
            DBSql.Design.DesDiscuss dd = new DBSql.Design.DesDiscuss();
            dd.DbContextRepository(desDiscussReply.UnitOfWork, desDiscussReply.DbContext);
            try
            {
                desDiscussReply.UnitOfWork.BeginTransaction();
                model.MvcSetValue();
                model.MvcDefaultSave(userInfo);
                desDiscussReply.Add(model);
                var desDiscussModel = dd.Get(model.TalkId);
                desDiscussModel.ReplyCount = desDiscussModel.ReplyCount + 1;
                dd.Edit(desDiscussModel);
                desDiscussReply.DbContext.SaveChanges();
                dd.DbContext.SaveChanges();
                desDiscussReply.UnitOfWork.CommitTransaction();
                return Json(new { Result = true, Data = model });
            }
            catch
            {
                return Json(new { Result = false, Message = "评论出错" });
            }
        }

        public JsonResult GetReplyData()
        {
            Common.SqlPageInfo queryContext = new Common.SqlPageInfo();
            queryContext.PageIndex = JQ.Util.TypeParse.parse<int>(Request.Form["pageIndex"]) + 1;
            queryContext.PageSize = JQ.Util.TypeParse.parse<int>(Request.Form["pageSize"]);
            if (string.IsNullOrEmpty(queryContext.SelectOrder))
            {
                queryContext.SelectOrder = "CreationTime";
            }
            int talkID = TypeHelper.parseInt(Request.Form["talkId"], 0);
            if (talkID == 0)
            {
                throw new ArgumentException("参数错误");
            }
            var list = desDiscussReply.GetPagedList(r => r.TalkId == talkID && r.DeleterEmpId == 0, queryContext).ToList();
            return Json(new { total = queryContext.PageTotleRowCount, rows = list });
        }

        public JsonResult GetDiscussInfo()
        {
            var id = JQ.Util.TypeParse.parse<int>(Request.Form["id"]);
            if (id == 0)
            {
                return Json(new { Result = false });
            }
            var data = op.Get(id);
            if (data == null)
            {
                return Json(new { Result = false });
            }
            return Json(new { Result = true, Title = data.TalkTitle, ReplyCount = data.ReplyCount, ReadCount = data.ReadCount });
        }
    }
}
