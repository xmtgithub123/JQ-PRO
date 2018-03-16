using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System;

namespace Oa.Controllers
{
    [Description("OaBookUse")]
    public class OaBookUseController : CoreController
    {
        private DBSql.Oa.OaBookUse op = new DBSql.Oa.OaBookUse();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        { 
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
            var list = op.GetPagedDynamic(PageModel).ToList();

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }
        #endregion

        public string GetJsonByPerson()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();

            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            int empid = TypeHelper.parseInt(Request.Form["EmpID"], 0);

            if (!string.IsNullOrEmpty(Request.Form["EmpID"]))
            {
                if ( TypeHelper.parseInt(Request.Form["EmpID"])>0)
                {
                     PageModel.SelectCondtion.Add("LendEmpID", Request.Form["EmpID"]);
                } 
            }
            if (!string.IsNullOrEmpty(Request.Form["DateReturnFact"]))
            {
                PageModel.SelectCondtion.Add("DateReturnFact", Request.Form["DateReturnFact"]);
            }

            if (!string.IsNullOrEmpty(Request.QueryString["BookID"]))
            {
                PageModel.SelectCondtion.Add("BookID", Request.QueryString["BookID"]);
            }
            

            var list = new DBSql.Oa.OaBookUse().GetList(PageModel).ToList();

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });

        }

        public string GetJsonByPersonHistory()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();

            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            int empid = TypeHelper.parseInt(Request.Form["EmpID"], 0);

            if (!string.IsNullOrEmpty(Request.Form["EmpID"]))
            {
                if (TypeHelper.parseInt(Request.Form["EmpID"]) > 0)
                {
                    PageModel.SelectCondtion.Add("LendEmpID", Request.Form["EmpID"]);
                }
            }
            if (!string.IsNullOrEmpty(Request.Form["DateReturnFact"]))
            {
                PageModel.SelectCondtion.Add("DateReturnFact", Request.Form["DateReturnFact"]);
            }

            if (!string.IsNullOrEmpty(Request.QueryString["BookID"]))
            {
                PageModel.SelectCondtion.Add("BookID", Request.QueryString["BookID"]);
            }


            var list = new DBSql.Oa.OaBookUse().GetListHistory(PageModel).ToList();

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });

        }

        public string GetJsonByPerson2()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();

            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            int empid = TypeHelper.parseInt(Request.Form["EmpID"], 0);

            if (!string.IsNullOrEmpty(Request.Form["EmpID"]))
            {
                PageModel.SelectCondtion.Add("LendEmpID", Request.Form["EmpID"]);
            }
            if (!string.IsNullOrEmpty(Request.Form["DateReturnFact"]))
            {
                PageModel.SelectCondtion.Add("DateReturnFact", Request.Form["DateReturnFact"]);
            }

            var list = new DBSql.Oa.OaBookUse().GetList(PageModel).ToList();
            var emplist = new DBSql.Sys.BaseEmployee().GetList(t => true).ToList();
            var res = from l in list
                      join e in emplist
                      on l.ReturnEmpId equals e.EmpID
                      select new
                      {
                          l.BookID,
                          l.BookName,
                          l.BookNameNumber,
                          l.BookQuantity,
                          l.BookPrice,
                          l.BookPublisher,
                          l.BookAuthor,
                          l.BookDateBought,
                          l.BookNote,
                          l.BookTypeID,
                          l.CreationTime,
                          l.CreatorEmpId,
                          l.CreatorEmpName,
                          l.BookTypeName,
                          l.LendCount,
                          l.DateLend,
                          l.ReturnEmpId,
                          l.Id,
                          l.DateReturnFact,
                          l.DateReturnPlan,
                          e.EmpName
                      };

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = res.ToList()
            });
        }

        public ActionResult selectBook()
        {
            ViewBag.EmpID = Request.QueryString["empID"];
            ViewBag.EmpName = new DBSql.Sys.BaseEmployee().Get(Convert.ToInt32(Request.QueryString["empID"])) == null ? "" : new DBSql.Sys.BaseEmployee().Get(Convert.ToInt32(Request.QueryString["empID"])).EmpName;
            return View();
        }

        public ActionResult borrBook(int id, int empID, string EmpName,int rowCount)
        {
            ViewBag.bookModel = new DBSql.Oa.OaBook().Get(id);
            var model = new OaBookUse();
            model.DateLend = System.DateTime.Now;
            model.ReturnEmpId = empID;
            EmpName = new DBSql.Sys.BaseEmployee().Get(empID) == null ? "" : new DBSql.Sys.BaseEmployee().Get(empID).EmpName;
            model.ReturnEmpName = EmpName;
            ViewBag.rowCount = rowCount;
            return View("info", model);
        }

        public ActionResult BookHistoryByBook(int id)
        {
            ViewBag.bookModel = new DBSql.Oa.OaBook().Get(id);
            //var model = new OaBookUse();
            //model.DateLend = System.DateTime.Now;
            //model.ReturnEmpId = empID;
            return View();
        }

        public ActionResult returnBackBook(int borrID)
        {
            if (borrID == 0)
            {
                throw new ArgumentException("borrID参数错误");
            }
            var model = op.Get(borrID);
            if (model == null)
            {
                throw new Exception("未找到实体");
            }
            ViewBag.bookModel = new DBSql.Oa.OaBook().Get(model.BookID);
            return View("returnInfo", model);
        }

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new OaBookUse();
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

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 0;
            op.Delete(s => id.Contains(s.Id));
            op.UnitOfWork.SaveChanges();
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id)
        {
            var model = new OaBookUse();
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


        public ActionResult listOverTime()
        {
            return View();
        }

        #region 按书借阅列表
        [Description("按书借阅列表")]
        public ActionResult listbookLend()
        {
            return View();
        }
        #endregion


        public ActionResult listBookLendInfo(int id, int rowCount)
        {
            ViewBag.bookModel = new DBSql.Oa.OaBook().Get(id);
            ViewBag.rowCount = rowCount;
            //var model = new OaBookUse();
            //model.DateLend = System.DateTime.Now;
            //model.ReturnEmpId = empID;
            //model.ReturnEmpName = EmpName;
            //return View("info", model);
            return View("listBookLendInfo");

        }


        #region 遗失图书
        [Description("遗失图书")]
        [HttpPost]
        public ActionResult editIsBookLose()
        {
            var Id = Common.ModelConvert.ConvertToDefault<int>(Request.Form["Id"]);
            var BookLoseType = Common.ModelConvert.ConvertToDefault<int>(Request.Form["BookLoseType"]);

            var model = new OaBookUse();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
           
            int reuslt = 0;
            if (model.Id > 0)
            {
                model.BookLoseType = BookLoseType;
                op.Edit(model);
                op.UnitOfWork.SaveChanges();
            } 
            reuslt = model.Id;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion



       /// <summary>
       ///  催还图书
       /// </summary>
       /// <param name="borrID"></param>
       /// <returns></returns>
        public ActionResult UrgenBack(int borrID)
        {
            try
            {
                DataModel.Models.OaBookUse use = new DBSql.Oa.OaBookUse().Get(borrID);
                if (use != null)
                {
                    int result=op.SendMessage(use, userInfo);
                    if(result>0)
                    {
                        doResult=DoResultHelper.doSuccess(1,"消息发送成功");
                    }
                    else
                    {
                        doResult = DoResultHelper.doError("消息发送失败");
                    }
                }

            }
            catch(Exception ex)
            {
                doResult = DoResultHelper.doError(ex.Message);
            }
            return Json(doResult);

        }
    }
}
