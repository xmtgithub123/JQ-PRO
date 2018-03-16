using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
namespace Oa.Controllers
{
    [Description("OaStampLend")]
    public class OaStampLendController : CoreController
    {
        private DBSql.Oa.OaStampLend op = new DBSql.Oa.OaStampLend();
        private DBSql.Oa.OaStampUse opOaStampUse = new DBSql.Oa.OaStampUse();
        private DataModel.Models.OaStampUse OaStampUseModel = new OaStampUse();
        private DBSql.Oa.OaStampLend opOaStampLend = new DBSql.Oa.OaStampLend();


        private DBSql.Sys.BaseData dbBaseData = new DBSql.Sys.BaseData();
        private DBSql.PubFlow.Flow dbFlow = new DBSql.PubFlow.Flow();

        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.EmpId = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("OaSealLend")));
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
            var list = op.GetPagedList(PageModel).ToList();

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }
        #endregion


        #region 列表Returnjson
        [Description("列表Returnjson")]
        [HttpPost]
        public string Returnjson()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }

            var list = opOaStampUse.GetPagedList(PageModel).ToList();

            List<string> result = PermissionList("OaSealLend");
            if (!result.Contains("allview") && !result.Contains("dep") && !result.Contains("emp"))
            {
                list = opOaStampUse.GetPagedList(p => p.CreatorEmpId == this.userInfo.EmpID, PageModel).ToList();
            }
            else if (result.Contains("allview"))
            {

            }
            else if (result.Contains("dep"))
            {
                list = opOaStampUse.GetPagedList(p => p.CreatorDepId == this.userInfo.EmpDepID, PageModel).ToList();
            }
            else if (result.Contains("emp"))
            {
                list = opOaStampUse.GetPagedList(p => p.CreatorEmpId == this.userInfo.EmpID, PageModel).ToList();
            }

            //展示需要的列信息
            //var dt = from f in list
            //         where f.DeleterEmpId == 0
            //         select new
            //         {
            //             Id = f.Id,
            //             f.CreatorEmpName,
            //             StampID = f.FK_OaStampUse_StampID == null ? "" : f.FK_OaStampUse_StampID.StampName,
            //             f.StampUseDate,
            //             f.StampEmpesult,
            //             f.StampReturnSrate,
            //             FlowStatusID = dbFlow.GetList(p => p.FlowRefTable == "OaStampUse" && p.FlowRefID == f.Id).FirstOrDefault() == null ? 0 : dbFlow.GetList(p => p.FlowRefTable == "OaStampUse" && p.FlowRefID == f.Id).FirstOrDefault().FlowStatusID,
            //             OaStampLendID = opOaStampLend.GetList(p => p.OaStampUseId == f.Id).FirstOrDefault() == null ? 0 : opOaStampLend.GetList(p => p.OaStampUseId == f.Id).FirstOrDefault().Id
            //         };

            var dt = (from p in list
                      join t1 in op.DbContext.Set<DataModel.Models.Flow>().Where(f => f.FlowRefTable == "OaStampUse") on p.Id equals t1.FlowRefID into nodes
                      from temp in nodes.DefaultIfEmpty()
                      select new
                      {
                          Id = p.Id,
                          p.CreatorEmpName,
                          p.CreatorEmpId,
                          StampID = p.FK_OaStampUse_StampID == null ? "" : p.FK_OaStampUse_StampID.StampName,
                          p.StampUseDate,
                          p.StampEmpesult,
                          p.StampReturnSrate,
                          FlowID = temp == null ? 0 : temp.Id,
                          FlowName = temp == null ? "" : temp.FlowName,
                          FlowStatusID = temp == null ? 0 : temp.FlowStatusID,
                          FlowStatusName = temp == null ? "" : temp.FlowStatusName,
                          FlowXml = temp == null ? "" : temp.FlowXml,
                          OaStampLendID = opOaStampLend.GetList(f=> f.OaStampUseId == p.Id).FirstOrDefault() == null ? 0 : opOaStampLend.GetList(f => f.OaStampUseId == p.Id).FirstOrDefault().Id
                      }).Select(m => new
                      {
                          m.Id,
                          m.CreatorEmpName,
                          m.CreatorEmpId,
                          m.StampID,
                          m.StampUseDate,
                          m.StampEmpesult,
                          m.StampReturnSrate,
                          m.FlowID,
                          m.FlowName,
                          m.FlowStatusID,
                          m.FlowStatusName,
                          m.FlowXml,
                          FlowTurnedEmpIDs = JQ.Util.TypeParse.GetTurnedEmpIDs(m.FlowXml),
                          m.OaStampLendID
                      });

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = dt.ToList()
            });
        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            int OaStampUseId = TypeHelper.parseInt(Request.QueryString["OaStampUseId"]);
            if (OaStampUseId > 0)
            {
                OaStampUseModel = opOaStampUse.Get(OaStampUseId);
            }
            var model = new OaStampLend();
            if (OaStampUseModel != null)
            {
                model.StampID = OaStampUseModel.StampID;
                model.StampLendDate = OaStampUseModel.StampUseDate;
                model.StampLendNote = OaStampUseModel.StampEmpesult;
                model.StampReturnEmpName = OaStampUseModel.CreatorEmpName;
                model.StampFactReturnDate = System.DateTime.Now;
                model.CreationTime = System.DateTime.Now;
                model.CreatorEmpName = userInfo.EmpName;

            }
            ViewBag.OaStampUseId = OaStampUseId;

            ViewBag.permission = "['add','submit']";

            List<string> list = PermissionList("OaSealLend");
            if (list.Contains("add"))
            {
                ViewBag.Permission = "['submit', 'close']";
            }
            else
            {
                ViewBag.Permission = "['close']";
            }

            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            ViewBag.permission = "['add','submit']";

            //List<string> list = PermissionList("OaSealLend");
            //if (list.Contains("edit"))
            //{
            //    ViewBag.Permission = "['submit', 'close']";
            //}
            //else
            //{
                ViewBag.Permission = "['close']";
            //}

            ViewBag.OaStampUseId = model.OaStampUseId;

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
            int OaStampUseId = TypeHelper.parseInt(Request.Form["OaStampUseId"]);
            DataModel.Models.OaStampUse NewOaStampUseModel = new OaStampUse();
            if (OaStampUseId > 0)
            {

                NewOaStampUseModel = opOaStampUse.Get(OaStampUseId);
                NewOaStampUseModel.MvcSetValue();
                NewOaStampUseModel.Id = OaStampUseId;
                NewOaStampUseModel.StampReturnSrate = 1;
            }


            var model = new OaStampLend();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();

            int reuslt = 0;
            if (model.Id > 0)
            {
                op.Edit(model);
            }
            else
            {
                op.Add(model);
                if (NewOaStampUseModel != null)
                {
                    opOaStampUse.Edit(NewOaStampUseModel);
                    opOaStampUse.UnitOfWork.SaveChanges();
                }
            }
            op.UnitOfWork.SaveChanges();

            if (Id <= 0)
            {
                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.MoveFile(model.Id, userInfo.EmpID, userInfo.EmpName);
                }
            }
            reuslt = model.Id;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion
    }
}
