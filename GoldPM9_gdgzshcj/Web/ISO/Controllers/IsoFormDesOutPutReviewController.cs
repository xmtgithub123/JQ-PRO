using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.Xml;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Data.SqlClient;
using System;
using Common.Data.Extenstions;

namespace ISO.Controllers
{
    /// <summary>
    /// 设计评审记录单
    /// </summary>
    public class DesOutPutReview
    {
        /// <summary>
        /// 签名
        /// </summary>
        public string SignName { get; set; }
        /// <summary>
        /// 签名日期
        /// </summary>
        public string SignDate { get; set; }
        /// <summary>
        /// 评审主持人
        /// </summary>
        public string ReviewName { get; set; }
        /// <summary>
        /// 参加评审人员
        /// </summary>
        public string ReviewPerson { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string Number { get; set; }

        public DesOutPutReview()
        {
            SignName = "";
            SignDate = "";
            ReviewName = "";
            ReviewPerson = "";
            Number = "";
        }
    }

    public class OutReviewNode
    {
        public int FormID { get; set; }
        public int SpecialId { get; set; }
        public string SpecialName { get; set; }
        public string ReviewTarget { get; set; }
        public string ReviewContent { get; set; }
        public string ReviewResult { get; set; }
        public string ReviewSugg { get; set; }
        public int RefID { get; set; }
        public OutReviewNode()
        {
            FormID = 0;
            RefID = 0;
            SpecialId = 0;
            SpecialName = "";
            ReviewTarget = "";
            ReviewContent = "";
            ReviewResult = "";
            ReviewSugg = "";
        }
    }
    /// <summary>
    /// 设计输出评审单（共用IsoForm）
    /// </summary>
    public class IsoFormDesOutPutReviewController : CoreController
    {
        private DBSql.Iso.IsoForm op = new DBSql.Iso.IsoForm();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        private DBSql.Project.Project proj = new DBSql.Project.Project();
        private DBSql.Iso.IsoFormNode bussdetail = new DBSql.Iso.IsoFormNode();

        [Description("弹出列表信息")]
        public ActionResult selectReview()
        {
            return View();
        }

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("DesignOutReview")));
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
            List<string> permissionList = PermissionList("DesignOutReview");
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "FormID desc";
            }
            string Keys = Request.Form["Keys"];
            var Thwere = QueryBuild<DataModel.Models.IsoForm>.True();
            Thwere = Thwere.And(p => p.RefTable == "IsoFormDesOutPutReview");
            if (!string.IsNullOrEmpty(Keys))
            {
                Thwere = Thwere.And(p => p.FK_IsoForm_ProjID == null ? "".Contains(Keys) :
                (p.FK_IsoForm_ProjID.ProjName.Contains(Keys) || p.FK_IsoForm_ProjID.ProjNumber.Contains(Keys)));
            }
            if (!(permissionList.Contains("allview")||permissionList.Contains("alledit")))
            {
                if (permissionList.Contains("dep"))
                {
                    Thwere = Thwere.And(p => p.CreatorDepId == userInfo.EmpDepID);
                }
                else
                {
                    Thwere = Thwere.And(p => p.CreatorEmpId == userInfo.EmpID);//个人查看权
                }
            }
           
            if (!string.IsNullOrEmpty(Request.Params["startDate"]) && TypeHelper.isDateTime(Request.Params["startDate"]))
            {
                DateTime startDate = TypeHelper.parseDateTime(Request.Params["startDate"]);
                Thwere = Thwere.And(p => p.FormDate >= startDate);
            }
            if (!string.IsNullOrEmpty(Request.Params["endDate"]) && TypeHelper.isDateTime(Request.Params["endDate"]))
            {
                DateTime endDate = TypeHelper.parseDateTime(Request.Params["endDate"]);
                Thwere = Thwere.And(p => p.FormDate <= endDate);
            }
            var list = op.GetPagedList(Thwere, PageModel).ToList();
            var model = new DesOutPutReview();
            var target = from item in list
                         let pro = proj.Get(item.ProjID)
                         //where pro != null&&(pro == null ? "" : pro.ProjNumber).Contains(Keys) || (pro == null ? "" : pro.ProjName).Contains(Keys)

                         select new
                         {
                             item.FormID,
                             item.FormDate,
                             ProjNumber = pro == null ? "" : pro.ProjNumber,
                             ProjName = pro == null ? "" : pro.ProjName,
                             item.CreatorEmpName,
                             item.CreatorEmpId,
                             item.FormNote,
                             Number = string.IsNullOrEmpty(item.FormCtlXml) ? "" : model.XmlToModel(item.FormCtlXml).Number,
                             ReviewName = string.IsNullOrEmpty(item.FormCtlXml) ? "" : model.XmlToModel(item.FormCtlXml).ReviewName,
                             FlowStatus = SetFlowStatus("IsoFormDesOutPutReview", item.FormID, item.CreatorEmpId, userInfo.EmpID)
                         };


            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = target.ToList()
            });
        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new IsoForm();
            var desOutPutReview = new DesOutPutReview();
            model.FormDate = System.DateTime.Now;
            BindViewData(desOutPutReview);//绑定ViewData数据信息
            ViewData["EmpName"] = userInfo.EmpName;//当前人员信息
            BindProjName(model.ProjID);
            ExportPermission();//导出权
            ViewBag.detailPermission = 1;
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            var desOutPutReview = new DesOutPutReview();
            if (!string.IsNullOrEmpty(model.FormCtlXml))
            {
                desOutPutReview.XmlToModel(model.FormCtlXml);//将xml中的值赋给实体对象
            }
            BindViewData(desOutPutReview);
            BindProjName(model.ProjID);
            ViewData["EmpName"] = model.CreatorEmpName;

            List<string> permission = PermissionList("DesignOutReview");
            int FlowStatus = SetFlowStatus("IsoFormDesOutPutReview", model.FormID, model.CreatorEmpId, userInfo.EmpID);
            if ((permission.Contains("edit") || permission.Contains("alledit")) && (FlowStatus == 0 || FlowStatus == 1))//默认显示
            {
                //编辑权限(保存和提交按钮显示)
                ViewBag.editPermission = "";
                ViewBag.submitPermission = "";
                if (FlowStatus == 0)
                {
                    ViewBag.detailPermission = 1;//可进行编辑
                }
                else
                {
                    ViewBag.detailPermission = 0;
                }

            }
            else
            {
                if (FlowStatus > 1)// 流程自动控制
                {
                    ViewBag.editPermission = "";
                    ViewBag.submitPermission = "";
                    ViewBag.detailPermission = 0;
                }
                else
                {
                    if (FlowStatus == -1)//非本人创建（查看）
                    {
                        //隐藏提交和保存
                        ViewBag.editPermission = ",isShowSave:false";
                        ViewBag.submitPermission = "$('#toolbar').children('a').eq(0).hide();";
                        ViewBag.Upload = "$('#uploadfile1_upload').hide();$('#uploadfile1_delete').hide();";
                        ViewBag.detailPermission = 0;
                    }
                }

            }
            ExportPermission();//导出权
            return View("info", model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            try
            {
                var list = op.GetList(p => p.RefTable == "IsoFormDesOutPutReview" && id.Contains(p.FormID));
                int Count = (
                             from item in list
                             where SetFlowStatus("IsoFormDesOutPutReview", item.FormID, item.CreatorEmpId, userInfo.EmpID) != 0
                             select item.FormID
                          ).Count();
                if (Count > 0)
                {
                    doResult = DoResultHelper.doSuccess(1, "选中的记录包含您无权删除的行");
                } 
                else
                {
                    op.DeleteReview(id.ToList());
                    new DBSql.PubFlow.Flow().Delete(id, "IsoFormDesOutPutReview");
                    doResult = DoResultHelper.doSuccess(1, "删除成功");
                }
            }
            catch(Exception ex)
            {
                doResult = DoResultHelper.doError(ex.Message);
            }
            return Json(doResult);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int FormID)
        {
            var model = new IsoForm();
            var desOutPutReview = new DesOutPutReview();
            List<OutReviewNode> EvalList = new List<OutReviewNode>();
            string JsonRows = Request.Form["JsonRows"];
            try
            {
                if (FormID > 0)
                {
                    model = op.Get(FormID);
                }
                model.MvcSetValue();
                desOutPutReview.MvcSetValue();
                string xml = desOutPutReview.ModelToXml();
                if (model.FormID > 0)
                {
                    model.FormCtlXml = xml;
                    model.MvcDefaultEdit(userInfo.EmpID);
                    op.Edit(model);
                }
                else
                {

                    model.MvcDefaultSave(userInfo.EmpID);
                    model.FormCtlXml = xml;
                    model.RefTable = "IsoFormDesOutPutReview";
                    model.FormName = "设计评审记录";
                    op.Add(model);
                }
                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.MoveFile(model.FormID, userInfo.EmpID, userInfo.EmpName);
                }
                op.UnitOfWork.SaveChanges();
                List<IsoFormNode> detailList = new List<IsoFormNode>();
                if (JsonRows != null && JsonRows != "")
                {
                    EvalList = new JavaScriptSerializer().Deserialize<List<OutReviewNode>>(JsonRows);
                    foreach (var m in EvalList)
                    {
                        DataModel.Models.IsoFormNode detail = new IsoFormNode();
                        detail.FormID = model.FormID;
                        detail.RefID = m.RefID;
                        detail.ColAttType1 = m.SpecialId;
                        detail.ColAttVal1 = m.ReviewTarget;
                        detail.ColAttVal2 = m.ReviewContent;
                        detail.ColAttVal3 = m.ReviewResult;
                        detail.ColAttVal4 = m.ReviewSugg;
                        detail.ColAttVal5 = model.RefTable;
                        detailList.Add(detail);
                    }
                    List<long> detailID = detailList.Where(p => p.FormID == model.FormID).Select(p => p.RefID).ToList();
                    bussdetail.Delete(p => !detailID.Contains(p.RefID) && p.FormID == model.FormID);
                    long next = 0;
                    if (bussdetail.GetList(p => p.FormID == model.FormID).Count() > 0)
                    {
                        next = bussdetail.GetList(p => p.FormID == model.FormID).Select(p => p.RefID).Max();
                    }
                    foreach (var m in detailList)
                    {

                        if (m.RefID > 0)
                        {
                            string sql = "update IsoFormNode set ColAttType1=@ColAttType1,ColAttVal1=@ColAttVal1,ColAttVal2=@ColAttVal2,ColAttVal3=@ColAttVal3,ColAttVal4=@ColAttVal4 where FormID=@FormID and RefID=@RefID";
                            //node = bussdetail.FirstOrDefault(p=>p.RefID==m.RefID&&p.FormID==m.FormID);
                            //bussdetail.Edit(m);用Edit方法会出问题
                            List<SqlParameter> para = new List<SqlParameter>();
                            para.Add(new SqlParameter("@ColAttType1", m.ColAttType1));
                            para.Add(new SqlParameter("@ColAttVal1", m.ColAttVal1));
                            para.Add(new SqlParameter("@ColAttVal2", m.ColAttVal2));
                            para.Add(new SqlParameter("@ColAttVal3", m.ColAttVal3));
                            para.Add(new SqlParameter("@ColAttVal4", m.ColAttVal4));
                            para.Add(new SqlParameter("@FormID", m.FormID));
                            para.Add(new SqlParameter("@RefID", m.RefID));
                            bussdetail.ExecuteNonQuery(sql, para.ToArray());

                        }
                        else
                        {
                            next = next + 1;
                            m.RefID = next;
                            bussdetail.Add(m);
                        }

                    }

                    bussdetail.UnitOfWork.SaveChanges();
                    doResult = DoResultHelper.doSuccess(1, "操作成功");
                }
            }
            catch (Exception ex)
            {
                doResult = DoResultHelper.doError(ex.Message);
            }
            return Json(doResult);
        }
        #endregion

        /// <summary>
        /// 将实体的相关数据绑定到ViewData
        /// </summary>
        /// <param name="desOutPutReview"></param>
        public void BindViewData(DesOutPutReview desOutPutReview)
        {
            System.Type t = desOutPutReview.GetType();
            PropertyInfo[] info = t.GetProperties();
            foreach (PropertyInfo p in info)
            {
                ViewData[p.Name] = p.GetValue(desOutPutReview, null);
            }
        }

        private void BindProjName(int ProjID)
        {
            DataModel.Models.Project project = proj.Get(ProjID);
            if (project != null)
            {
                ViewBag.ProjNumber = project.ProjNumber;
                ViewBag.Phase = proj.GetProjPhase(project.ProjPhaseIds);// project.FK_Project_ProjTypeID == null ? "" : project.FK_Project_ProjTypeID.BaseName;
                ViewBag.ProjID = ProjID;
                ViewBag.ProjName = project.ProjName;
            }
            else
            {
                ViewBag.ProjNumber = "";
                ViewBag.Phase = "";
                ViewBag.ProjID = "";
                ViewBag.ProjName = "";
            }
        }


        /// <summary>
        /// 判断是否有导出权
        /// </summary>
        private void ExportPermission()
        {
            List<string> permission = PermissionList("DesignOutReview");
            if (permission.Contains("export"))
            {
                ViewBag.ExportPermission = "['close', 'exportForm']";
            }
            else
            {
                ViewBag.ExportPermission = "['close']";
            }
        }
    }
}