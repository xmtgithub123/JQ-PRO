using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
namespace Oa.Controllers
{
    [Description("OaMail")]
    public class OaMailController : CoreController
    {
        private DBSql.Oa.OaMail op = new DBSql.Oa.OaMail();
        private DBSql.Oa.OaMailRead opread = new DBSql.Oa.OaMailRead();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表

        [Description("列表")]
        public ActionResult list()
        {
            return View();
        }

        [Description("写邮件")]
        public ActionResult MailWrite()
        {
            ViewBag.MailID = GetRequestValue<int>("MailID");
            ViewBag.TypeID = GetRequestValue<int>("TypeID");
            var model = InitPage(ViewBag.MailID, ViewBag.TypeID);
            model.MailDate = System.DateTime.Now;
            //获取出附件（只转移最新版本的）

            return View("MailWrite", model);
        }

        private OaMail InitPage(int MailID, int TypeID)
        {
            if (MailID == 0) return new OaMail();
            OaMail model = op.Get(MailID);
            switch (TypeID)
            {
                case 1:
                    model.MailTitle = "【转发】" + model.MailTitle;
                    //拷贝附件到临时目录
                    try
                    {
                        model.Id = 0;
                        ViewBag.AttachData = new DBSql.Sys.BaseAttach().CopyFileToTemp(MailID, "OaMail", userInfo.EmpID);
                    }
                    catch { }
                    break;
                case 2:
                    model.MailTitle = "【回复】" + model.MailTitle;
                    ViewBag.MailNote = "";
                    model.Id = 0;
                    string Emptxt = new DBSql.Sys.BaseEmployee().GetEmpName(model.CreatorEmpId.ToString());
                    ViewBag.SendEmp = Emptxt;

                    break;
                case 3:
                    //获取先前选中的人

                    var EmpList = new DBSql.Oa.OaMailRead().GetMailInBoxEmp(MailID);
                    ViewBag.SendEmp = new DBSql.Sys.BaseEmployee().GetEmpName(string.Join(",", EmpList));
                    break;
            }
            return model;
        }


        [Description("发件箱")]
        public ActionResult MailSend()
        {
            return View();
        }



        [Description("收件箱")]
        public ActionResult MailReceive()
        {
            ViewBag.Read = GetRequestValue<int>("Read");
            return View();
        }


        [Description("草稿箱")]
        public ActionResult MailDraft()
        {
            return View();
        }


        [Description("垃圾箱")]
        public ActionResult MailJunk()
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


        [Description("列表json")]
        [HttpPost]
        public string mailsendjson(int Flag = 0)
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            //外键筛选 
            PageModel.SelectCondtion.Add("MailEmpID", userInfo.EmpID);
            PageModel.SelectCondtion.Add("MailFlag", Flag);
            PageModel.SelectCondtion.Add("MailIsDelete", false);

            var TWhere = op.GetFunc(PageModel);
            var list = op.GetPagedList(TWhere, PageModel).ToList();
            var data = (from s in list
                        select new
                        {
                            s.Id,
                            s.MailDate,
                            s.MailTitle,
                            MailNote = s.MailNote.Length > 0 ? "有" : "无",
                            s.CreatorEmpId,
                            MailEmpID_Name = s.CreatorEmpName,
                            MailFlag = "fa fa-envelope",
                            s.MailIsDelete,
                            MailEmpName = opread.GetMailInBoxEmpName(s.Id),
                        }).ToList();



            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = data
            });
        }


        public string mailjunkjson(string DateLower = "", string DateUpper = "", string txtCondtion = "")
        {
            int PageSize = TypeHelper.parseInt(HttpContext.Request["rows"], 20);
            int PageIndex = TypeHelper.parseInt(HttpContext.Request["page"], 1);

            //var list = op.GetQuery().Where(p => p.DeleterEmpId == 0);

            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            var list = op.GetPagedList(PageModel).ToList();

            var In = new DBSql.Oa.OaMailRead();
            In.DbContextRepository(op.UnitOfWork, op.DbContext);
            var Tlist = In.GetQuery();
            var data = (from s in list
                        select new
                        {
                            IsSend = 0,
                            s.Id,
                            EmpID = s.CreatorEmpId,
                            MailEmpName = "",
                            s.MailDate,
                            s.MailTitle,
                            MailNote = s.MailNote.Length > 0 ? "有" : "无",
                            MailFlag = "fa fa-envelope",
                            DeleteType = s.MailIsDelete,
                        })
                        .Union
                        (from s in Tlist
                         select new
                         {
                             IsSend = 1,
                             s.Id,
                             EmpID = s.MailReadEmpId,
                             MailEmpName = "",
                             MailDate = s.FK_OaMailRead_Id.MailDate,
                             MailTitle = s.FK_OaMailRead_Id.MailTitle,
                             MailNote = s.FK_OaMailRead_Id.MailNote.Length > 0 ? "有" : "无",
                             MailFlag = s.MailReadDate == new System.DateTime(1900, 1, 1) ? "fa fa-envelope" : "fa fa-envelope-o",
                             DeleteType = s.MailReadIsDelete == 1,
                         });

            data = data.Where(p => p.EmpID == userInfo.EmpID && p.DeleteType == true);
            if (!string.IsNullOrEmpty(DateLower))
            {
                var Lower = System.Convert.ToDateTime(DateLower);
                data = data.Where(p => p.MailDate > Lower);
            }
            if (!string.IsNullOrEmpty(DateUpper))
            {
                var Upper = System.Convert.ToDateTime(DateUpper);
                var sUpper = Upper.AddDays(1);
                data = data.Where(p => p.MailDate < sUpper);
            }
            if (txtCondtion.Trim() != "") data = data.Where(p => p.MailTitle.Contains(txtCondtion.Trim()));


            //获取收件人
            var listdata = (from s in data.ToList()
                            select new
                            {
                                s.IsSend,
                                s.Id,
                                s.EmpID,
                                MailEmpName = opread.GetMailInBoxEmpName(s.Id),
                                s.MailDate,
                                s.MailTitle,
                                s.MailNote,
                                s.MailFlag,
                                s.DeleteType,
                            });


            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = listdata
            });
        }


        public string mailreceivejson(int Read = 0)
        {
            var handle = new DBSql.Oa.OaMailRead();
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();

            //外键筛选 
            PageModel.SelectCondtion.Add("MailInBoxEmpID", userInfo.EmpID);
            PageModel.SelectCondtion.Add("MailFlag", 0);
            PageModel.SelectCondtion.Add("MailReadIsDelete", 0);

            if (Read == 2) PageModel.SelectCondtion.Add("NotRead", 1);

            var TWhere = handle.GetFunc(PageModel);
            var list = handle.GetPagedList(TWhere, PageModel);
            var data = (from s in list
                        select new
                        {
                            s.Id,
                            MailID = s.FK_OaMailRead_Id.Id,
                            MailFlag = s.MailReadDate == new System.DateTime(1900, 1, 1) ? "fa fa-envelope" : "fa fa-envelope-o",
                            MailEmpName = s.FK_OaMailRead_Id.MailIsBBC == 1 ? "******" : s.FK_OaMailRead_Id.CreatorEmpName,
                            MailDate = s.FK_OaMailRead_Id.MailDate,
                            MailTitle = s.FK_OaMailRead_Id.MailTitle,
                            MailNote = s.FK_OaMailRead_Id.MailNote.Length > 0 ? "有" : "无",

                        });

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = data
            });
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            var EmpList = new DBSql.Oa.OaMailRead().GetMailInBoxEmp(id);
            ViewBag.SendEmp = new DBSql.Sys.BaseEmployee().GetEmpName(string.Join(",", EmpList));

            if (model.MailIsBBC == 1) model.CreatorEmpName = "******";

            var ReceiveFlag = GetRequestValue<int>("ReceiveFlag");
            if (ReceiveFlag == 1)
            {
                new DBSql.Oa.OaMailRead().UpdateRead(id, userInfo.EmpID);
            }
            ViewBag.ReceiveFlag = ReceiveFlag;

            return View("MailDetail", model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id, bool DelType = false, bool IsResum = false)
        {
            int reuslt = 1;
            op.DelMail(id.ToList(), userInfo, DelType, IsResum);

            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult save(int MailFlag, string SendEmp, int Id = 0, int TypeID = 0, string MailIsBBC = "")
        {
            var model = new OaMail();

            bool IsEdit = TypeID == 3;
            if (IsEdit)
            {
                model.MvcDefaultEdit(userInfo);
                model = op.Get(Id);
            }
            else
            {
                model.MvcDefaultSave(userInfo);
            }
            model.MvcSetValue();
            model.MailDate = System.DateTime.Now;
            model.MailFlag = MailFlag;
            model.CreatorEmpId = userInfo.EmpID;
            model.MailIsDelete = false;
            model.MailIsBBC = MailIsBBC == "0" ? 1 : 0;

            string EmpIDs = new DBSql.Sys.BaseEmployee().GetEmpIds(SendEmp);
            op.SendMail(model, EmpIDs, IsEdit);
            using (var ba = new DBSql.Sys.BaseAttach())
            {
                ba.MoveFile(model.Id, userInfo.EmpID, userInfo.EmpName);
            }

            var reuslt = model.Id;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion
    }
}
