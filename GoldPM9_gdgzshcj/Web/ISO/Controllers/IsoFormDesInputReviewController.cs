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
using System;
using System.Linq.Expressions;
using Common.Data.Extenstions;

namespace ISO.Controllers
{
    /// <summary>
    /// 设计输入评审
    /// </summary>
    public class DesInputReview
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 资料名称
        /// </summary>
        public string MaterialName { get; set; }
        /// <summary>
        /// 资料来源
        /// </summary>
        public int MaterialSourceID { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public int DeptID { get; set; }
        /// <summary>
        /// 参与专业
        /// </summary>
        public string SpeIDs { get; set; }
        /// <summary>
        /// 评审人
        /// </summary>
        public string ReviewName { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string SignName { get; set; }
        /// <summary>
        /// 签名日期
        /// </summary>
        public string SignDate { get; set; }


        public DesInputReview()
        {
            Number = "";
            MaterialName = "";
            MaterialSourceID = 0;
            DeptID = 0;
            SpeIDs = "";
            ReviewName = "";
            SignName = "";
            //SignDate = System.DateTime.Now;
            SignDate = "";
        }
    }

    /// <summary>
    /// 设计输入评审单（共用IsoForm）
    /// </summary>
    public class IsoFormDesInputReviewController : CoreController
    {
        private DBSql.Iso.IsoForm op = new DBSql.Iso.IsoForm();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        private DBSql.Sys.BaseEmployee emp = new DBSql.Sys.BaseEmployee();
        private DBSql.Project.Project proj = new DBSql.Project.Project();
        private DBSql.Sys.BaseData baseData = new DBSql.Sys.BaseData();
        private DBSql.PubFlow.Flow flow = new DBSql.PubFlow.Flow();
        #region 列表
        [Description("列表")]
        public ActionResult list()
        {

            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("IsoFormDesInputReview")));
            ViewBag.EmpId = userInfo.EmpID;//权限设定对比
            return View();
        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string json()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            List<string> permissionList = PermissionList("IsoFormDesInputReview");
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "FormID desc";
            }
            string Keys = Request.Form["Keys"];
            var Thwere = QueryBuild<DataModel.Models.IsoForm>.True();
            if (Request.Params["ProjID"] != null)
            {
                int ProjID = Common.ExtensionMethods.Value<int>(Request.Params["ProjID"]);
                Thwere = Thwere.And(p => p.ProjID == ProjID);
            }         
            Thwere = Thwere.And(p => p.RefTable == "IsoFormDesInputReview");
            if (!string.IsNullOrEmpty(Keys))
            {
                Thwere = Thwere.And(p => p.FK_IsoForm_ProjID == null ? "".Contains(Keys) :
                (p.FK_IsoForm_ProjID.ProjName.Contains(Keys) || p.FK_IsoForm_ProjID.ProjNumber.Contains(Keys)));
            }

            if (!(permissionList.Contains("allview") ||permissionList.Contains("alledit")))
            {
                if (permissionList.Contains("dep"))
                {
                    Thwere = Thwere.And(p => p.CreatorDepId == userInfo.EmpDepID);//部门查看权
                }
                else
                {
                    Thwere = Thwere.And(p => p.CreatorEmpId == userInfo.EmpID);//个人查看权
                }
            }
            if(!string.IsNullOrEmpty(Request.Params["startDate"])&&TypeHelper.isDateTime(Request.Params["startDate"]))
            {
                DateTime startDate = TypeHelper.parseDateTime(Request.Params["startDate"]);
                Thwere = Thwere.And(p=>p.FormDate>=startDate);
            }
            if(!string.IsNullOrEmpty(Request.Params["endDate"])&&TypeHelper.isDateTime(Request.Params["endDate"]))
            {
                DateTime endDate = TypeHelper.parseDateTime(Request.Params["endDate"]);
                Thwere = Thwere.And(p=>p.FormDate<=endDate);
            }
            var list = op.GetPagedList(Thwere, PageModel).ToList();
            DesInputReview desInputReview = new DesInputReview();
            var target = from item in list
                         let model = desInputReview.XmlToModel(item.FormCtlXml)
                         let proj = item.FK_IsoForm_ProjID
                         //where ((proj == null ? "" : proj.ProjNumber).Contains(Keys) || (proj == null ? "" : proj.ProjName).Contains(Keys))
                         select new
                         {
                             item.FormID,
                             item.CreatorEmpId,
                             flowStatus= SetFlowStatus("IsoFormDesInputReview",item.FormID,item.CreatorEmpId,userInfo.EmpID),
                             item.FormDate,//登记日期
                             ProjNumber = proj == null ? "" : proj.ProjNumber,//项目编号
                             ProjName = proj == null ? "" : proj.ProjName,//项目名称
                             MeterialName = string.IsNullOrEmpty(item.FormCtlXml) ? "" : desInputReview.XmlToModel(item.FormCtlXml).MaterialName,//资料名称
                             ReviewName = string.IsNullOrEmpty(item.FormCtlXml) ? "" : desInputReview.XmlToModel(item.FormCtlXml).ReviewName,
                             SignDate = string.IsNullOrEmpty(item.FormCtlXml) ? "" : desInputReview.XmlToModel(item.FormCtlXml).SignDate,
                             MeterialSource = string.IsNullOrEmpty(item.FormCtlXml) ? "" : (baseData.Get(model.MaterialSourceID) == null ? "" : baseData.Get(model.MaterialSourceID).BaseName),
                             item.CreatorEmpName//登记人
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
            model.FormDate = DateTime.Now;//登记时间默认当前时间
            DesInputReview desInputView = new DesInputReview();
            BindViewDataValues(desInputView);//绑定初始化的值
            BindProjName(model.ProjID);
            BindExportInfo(desInputView);//绑定导出信息
            ViewData["EmpName"] = emp.Get(userInfo.EmpID) == null ? "" : emp.Get(userInfo.EmpID).EmpName;//登记人
            ExportPermission();//导出权
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            DesInputReview desInputReview = new DesInputReview();
            if (!string.IsNullOrEmpty(model.FormCtlXml))
            {
                desInputReview.XmlToModel(model.FormCtlXml);//将Xml转化成实体对象
            }
            BindProjName(model.ProjID);
            BindViewDataValues(desInputReview);
            BindExportInfo(desInputReview);//绑定导出信息
            ViewData["EmpName"] = emp.Get(model.CreatorEmpId) == null ? "" : emp.Get(model.CreatorEmpId).EmpName;//登记人

            List<string> permission = PermissionList("IsoFormDesInputReview");
            int FlowStatus = SetFlowStatus("IsoFormDesInputReview", model.FormID, model.CreatorEmpId, userInfo.EmpID);
            if ((permission.Contains("edit")||permission.Contains("alledit")) && (FlowStatus==0||FlowStatus==1))//默认显示
            {
                //编辑权限(保存和提交按钮显示)
                ViewBag.editPermission = "";
                ViewBag.submitPermission = "";

            }
            else
            {
                if (FlowStatus > 1)// 流程自动控制
                {
                    ViewBag.editPermission = "";
                    ViewBag.submitPermission = "";
                }
                else
                {
                    if (FlowStatus == -1)//非本人创建
                    {
                        //隐藏提交和保存
                        ViewBag.editPermission = ",isShowSave:false";
                        ViewBag.submitPermission = "$('#toolbar').children('a').eq(0).hide();";
                        ViewBag.Upload = "$('#uploadfile1_upload').hide();$('#uploadfile1_delete').hide()";
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
                var list = op.GetList(p => p.RefTable == "IsoFormDesInputReview" && id.Contains(p.FormID));
                int Count = (
                             from item in list
                             where SetFlowStatus("IsoFormDesInputReview", item.FormID, item.CreatorEmpId, userInfo.EmpID) != 0
                             select item.FormID
                          ).Count();
                if (Count > 0)
                {
                    doResult = DoResultHelper.doSuccess(1, "选中的记录包含您无权删除的行");
                }
                else
                {
                    op.Delete(s => id.Contains(s.FormID));
                    op.UnitOfWork.SaveChanges();
                    new DBSql.PubFlow.Flow().Delete(id, "IsoFormDesInputReview");
                    doResult = DoResultHelper.doSuccess(1, "删除成功");
                }
            }
            catch (Exception ex)
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
            var desInputReview = new DesInputReview();
            if (FormID > 0)
            {
                model = op.Get(FormID);
            }
            model.MvcSetValue();
            desInputReview.MvcSetValue();//获取相关的属性值
            string xml = desInputReview.ModelToXml();
            //string xml = Common.XmlConvert.ObjectToXMLRemoveHeader<DesInputReview>(desInputReview);
            int reuslt = 0;
            if (model.FormID > 0)
            {
                op.MvcDefaultEdit(userInfo.EmpID);
                model.FormCtlXml = xml;
                op.Edit(model);
            }
            else
            {

                model.FormCtlXml = xml;
                model.RefTable = "IsoFormDesInputReview";
                model.FormName = "设计输入评审";
                model.MvcDefaultSave(userInfo.EmpID);
                op.Add(model);
            }
            op.UnitOfWork.SaveChanges();
            using (var ba = new DBSql.Sys.BaseAttach())
            {
                ba.MoveFile(model.FormID, userInfo.EmpID, userInfo.EmpName);
            }
            reuslt = model.FormID;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.FormID, "操作成功") : DoResultHelper.doSuccess(model.FormID, "操作失败");
            return Json(dr);
        }
        #endregion

        /// <summary>
        /// 为Viewba绑定值
        /// </summary>
        /// <param name="desInputReview"></param>
        public void BindViewDataValues(DesInputReview desInputReview)
        {
            Type t = desInputReview.GetType();
            PropertyInfo[] proInfo = t.GetProperties();
            foreach (PropertyInfo info in proInfo)
            {
                ViewData[info.Name] = info.GetValue(desInputReview, null);
            }
        }


        private void BindProjName(int ProjID)
        {
            DataModel.Models.Project project = proj.Get(ProjID);
            if (project != null)
            {
                ViewBag.ProjNumber = project.ProjNumber;
                ViewBag.Phase = proj.GetProjPhase(project.ProjPhaseIds);//project.FK_Project_ProjTypeID == null ? "" : project.FK_Project_ProjTypeID.BaseName;
                ViewBag.ProjID = ProjID;
                ViewBag.ProjectName = project.ProjName;
            }
            else
            {
                ViewBag.ProjNumber = "";
                ViewBag.Phase = "";
                ViewBag.ProjID = "";
                ViewBag.ProjectName = "";
            }
        }
 
        /// <summary>
        /// 绑定专业、部门、资料来源用于Word导出
        /// </summary>
        /// <param name="desInputReview"></param>
        private void BindExportInfo(DesInputReview desInputReview)
        {
            if(desInputReview!=null)
            {
                DataModel.Models.BaseData modelData = baseData.Get(desInputReview.DeptID);
                if(modelData!=null)
                {
                    ViewBag.DeptName = modelData.BaseName;//绑定部门信息
                }
                else
                {
                    ViewBag.DeptName = "";
                }
                modelData = baseData.Get(desInputReview.MaterialSourceID);
                if(modelData!=null)
                {
                    ViewBag.MaterialNames = modelData.BaseName;
                }
                else
                {
                    ViewBag.MaterialNames = "";
                }
                ViewBag.SpecName =string.Join(",",GetBaseName(desInputReview.SpeIDs));
            }
        }


        /// <summary>
        /// 判断是否有导出权
        /// </summary>
        private void ExportPermission()
        {
            List<string> permission = PermissionList("IsoFormDesInputReview");
            if(permission.Contains("export"))
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