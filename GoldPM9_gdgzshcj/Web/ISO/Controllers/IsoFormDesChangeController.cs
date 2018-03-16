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
using System.Text;
using Common.Data.Extenstions;

namespace ISO.Controllers
{
    /// <summary>
    /// 设计变更相关的表单字段信息
    /// </summary>
    public class DesChange
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 建设单位
        /// </summary>
        public string ConstructUnit { get; set; }
        /// <summary>
        /// 提出单位
        /// </summary>
        public string SupplyUnit { get; set; }
        /// <summary>
        /// 变更费用
        /// </summary>
        public decimal ChangeFee { get; set; }
        /// <summary>
        /// 修改内容
        /// </summary>
        public string ModifyContent { get; set; }
        /// <summary>
        /// 设计人员
        /// </summary>
        public string DesignName { get; set; }
        /// <summary>
        /// 设计校核
        /// </summary>
        public string DesCheckName { get; set; }
        /// <summary>
        /// 设计审核
        /// </summary>
        public string DesReview { get; set; }
        /// <summary>
        /// 造价人员
        /// </summary>
        public string EvaluatePriceName { get; set; }
        /// <summary>
        /// 造价校审
        /// </summary>
        public string EvaluatePriceCheckName { get; set; }
        /// <summary>
        /// 造价审核
        /// </summary>
        public string EvaluatepriceReview { get; set; }
        /// <summary>
        /// 审批
        /// </summary>
        public string ApproveName { get; set; }
        /// <summary>
        /// 专业监理工程师
        /// </summary>
        public string SpecMonitorSignNameDate { get; set; }
        /// <summary>
        /// 项目总监签名
        /// </summary>
        public string ProjMajorSignNameDate { get; set; }
        /// <summary>
        /// 监理单位技术负责人
        /// </summary>
        public string MonitorUnitTechMajorSignNameDate { get; set; }
        /// <summary>
        /// 项目经理签名
        /// </summary>
        public string ProjManagerSignNamedate { get; set; }
        /// <summary>
        /// 建设单位管理部门负责人
        /// </summary>
        public string ConstructUnitDeptMajorSignNameDate { get; set; }
        /// <summary>
        /// 主管领导
        /// </summary>
        public string MajorLeaderSignNameDate { get; set; }
        /// <summary>
        /// 监理单位
        /// </summary>
        public string MonitorUnitSignNameDate { get; set; }
        /// <summary>
        /// 业主项目部
        /// </summary>
        public string HostProjDeptSignNameDate { get; set; }
        public DesChange()
        {
            Number = "";
            ConstructUnit = "";
            SupplyUnit = "";
            ChangeFee = 0;
            ModifyContent = "";
            DesignName = "";
            DesCheckName = "";
            DesReview = "";
            ApproveName = "";
            EvaluatePriceName = "";
            EvaluatePriceCheckName = "";
            EvaluatepriceReview = "";
            SpecMonitorSignNameDate = "";
            ProjMajorSignNameDate = "";
            MonitorUnitTechMajorSignNameDate = "";
            ProjManagerSignNamedate = "";
            ConstructUnitDeptMajorSignNameDate = "";
            MajorLeaderSignNameDate = "";
            MonitorUnitSignNameDate = "";
            HostProjDeptSignNameDate = "";
        }
    }

    /// <summary>
    /// 设计变更通知单（共用IsoForm）
    /// </summary>
    public class IsoFormDesChangeController : CoreController
    {
        private DBSql.Iso.IsoForm op = new DBSql.Iso.IsoForm();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        private DBSql.Sys.BaseEmployee emp = new DBSql.Sys.BaseEmployee();
        private DBSql.Project.Project proj = new DBSql.Project.Project();
        private DBSql.Sys.BaseData baseData = new DBSql.Sys.BaseData();
        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("DesignChangeApply")));
            ViewBag.EmpId = userInfo.EmpID;
            return View();
        }
        #endregion

        [Description("查看页面")]
        public ActionResult readList()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjectChange")));
            return View();
        }

        /// <summary>
        /// 统计页面
        /// </summary>
        /// <returns></returns>
        [Description("统计页面")]
        public ActionResult DesChangeStatistics()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("DesignChangeStati")));
            List<string> permission = PermissionList("DesignChangeStati");
            if (!(permission.Contains("allview")||permission.Contains("alledit")))
            {
                if (permission.Contains("dep"))
                {
                    ViewBag.Permit = (int)DataModel.PermitType.dep;//设置combobox时使用
                    ViewBag.DeptId = userInfo.EmpDepID;
                }
                else
                {
                    ViewBag.Permit = (int)DataModel.PermitType.emp;//view中判断使用
                    ViewBag.DeptId = userInfo.EmpDepID;
                    ViewBag.EmpID = userInfo.EmpID;
                }
            }

            return View();
        }

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string json()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            List<string> permission = PermissionList("DesignChangeApply");
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "FormID desc";
            }
            string Keys = Request.Form["Keys"];
            var Thwere = QueryBuild<DataModel.Models.IsoForm>.True();
            Thwere = Thwere.And(p => p.RefTable == "IsoFormDesChange");
            if (!string.IsNullOrEmpty(Keys))
            {
                Thwere = Thwere.And(p => p.FK_IsoForm_ProjID == null ? "".Contains(Keys) :
                (p.FK_IsoForm_ProjID.ProjName.Contains(Keys) || p.FK_IsoForm_ProjID.ProjNumber.Contains(Keys)));
            }

            if (!string.IsNullOrEmpty(Request.QueryString["EmpID"]))
            {
                int EmpID = TypeHelper.parseInt(Request.QueryString["EmpID"]);
                Thwere = Thwere.And(p => p.CreatorEmpId == EmpID);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["EmpDeptID"]))
            {
                int DeptID = TypeHelper.parseInt(Request.QueryString["EmpDeptID"]);
                Thwere = Thwere.And(p => p.CreatorDepId == DeptID);
            }

            if (!string.IsNullOrEmpty(Request.Params["startDate"]) && TypeHelper.isDateTime(Request.Params["startDate"]))
            {
                DateTime startDate = TypeHelper.parseDateTime(Request.Params["startDate"]);// 开始时间
                Thwere = Thwere.And(p => p.FormDate >= startDate);
            }

            if (!string.IsNullOrEmpty(Request.Params["endDate"]) && TypeHelper.isDateTime(Request.Params["endDate"]))
            {
                DateTime endDate = TypeHelper.parseDateTime(Request.Params["endDate"]);//结束时间
                Thwere = Thwere.And(p => p.FormDate <= endDate);
            }

            //判断个人查看权和部门查看权(多个页面共用一个json,权限以第一个菜单为主)
            if (!(permission.Contains("allview")|| permission.Contains("alledit")))
            {
                if (permission.Contains("dep"))
                {
                    Thwere = Thwere.And(p => p.CreatorDepId == userInfo.EmpDepID);
                }
                else
                {
                    Thwere = Thwere.And(p => p.CreatorEmpId == userInfo.EmpID);
                }
            }
            //if (!permission.Contains("allview") && !permission.Contains("dep"))// 个人查看权
            //{
            //    Thwere = Thwere.And(p => p.CreatorEmpId == userInfo.EmpID);
            //}
            //if (!permission.Contains("allview") && permission.Contains("dep"))//部门查看权
            //{
            //    Thwere = Thwere.And(p => p.CreatorDepId == userInfo.EmpDepID);
            //}

            var list = op.GetPagedList(Thwere, PageModel).ToList();

            DesChange desChange = new DesChange();

            var target = from item in list
                         let model = desChange.XmlToModel(item.FormCtlXml)
                         let proj = item.FK_IsoForm_ProjID
                         //where (proj == null ? "" : proj.ProjNumber).Contains(Keys) || (proj == null ? "" : proj.ProjName).Contains(Keys)

                         select new
                         {
                             item.FormID,
                             item.FormDate,//变更日期
                             ProjNumber = proj == null ? "" : proj.ProjNumber,//项目编号
                             ProjName = proj == null ? "" : proj.ProjName,//项目名称
                             Number = model == null ? "" : model.Number,//编号
                             item.CreatorEmpName,//发起人
                             item.CreatorEmpId,
                             //FlowStatus = GetFlowStatusId(item.FormID, item.CreatorEmpName),
                             FlowStatus= SetFlowStatus("IsoFormDesChange", item.FormID, item.CreatorEmpId, userInfo.EmpID),
                             DesignChange = GetBaseNames(item.ColAttVal1),//变更性质
                             DesignReason = GetBaseNames(item.ColAttVal2)//变更原因
                         };


            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = target.ToList()
            });
        }
        #endregion

        #region 人员列表信息

        [HttpPost]
        public string empJson()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "DepartmentOrder asc,EmpOrder asc";
            }
            var list = new DBSql.Sys.AllBaseEmployee().GetPagedList(PageModel).ToList();
            if (!string.IsNullOrEmpty(Request.QueryString["DeptID"]))
            {
                int DeptID = TypeHelper.parseInt(Request.QueryString["DeptID"]);
                list = new DBSql.Sys.AllBaseEmployee().GetPagedList(p => p.EmpDepID == DeptID, PageModel).ToList();
            }
            List<string> permission = PermissionList("DesignChangeStati");
            if (!(permission.Contains("allview")|| permission.Contains("alledit")))
            {
                if (permission.Contains("dep"))
                {
                    list = new DBSql.Sys.AllBaseEmployee().GetPagedList(p => p.EmpDepID == userInfo.EmpDepID, PageModel).ToList();
                }
                else
                {
                    list = new DBSql.Sys.AllBaseEmployee().GetPagedList(p => p.EmpID == userInfo.EmpID, PageModel).ToList();
                }
            }

            var td = from n in list
                     select new
                     {
                         n.EmpID,
                         n.DepartmentID,
                         n.EmpDepID,
                         n.DepartmentName,
                         n.EmpName
                     };

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = td
            });
        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new IsoForm();
            model.FormDate = DateTime.Now;//登记时间默认当前时间
            DesChange desChange = new DesChange();
            BindViewDataValues(desChange);//绑定初始化的值
            BindProjName(model.ProjID);
            ExportPermission();//导出权

            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            DesChange desChange = new DesChange();
            if (!string.IsNullOrEmpty(model.FormCtlXml))
            {
                desChange.XmlToModel(model.FormCtlXml);//将Xml转化成实体对象
            }
            BindProjName(model.ProjID);
            BindViewDataValues(desChange);
            ViewBag.DesignKind = GetBaseNames(model.ColAttVal1);//变更性质
            ViewBag.DesignChangeReason = GetBaseNames(model.ColAttVal2);//变更原因用于导出


            List<string> permission = PermissionList("DesignChangeApply");
            //int FlowStatus = GetFlowStatusId(model.FormID, model.CreatorEmpName);
            int FlowStatus = SetFlowStatus("IsoFormDesChange",model.FormID,model.CreatorEmpId,userInfo.EmpID);
            if ((permission.Contains("edit") || permission.Contains("alledit")) && (FlowStatus==0||FlowStatus==1))//默认显示
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
                    if (FlowStatus == -1)
                    {
                        //隐藏提交和保存
                        ViewBag.editPermission = ",isShowSave:false";
                        ViewBag.submitPermission = "$('#toolbar').children('a').eq(0).hide();";
                        ViewBag.Upload = "$('#DesChangeUpload_upload').hide();$('#DesChangeUpload_delete').hide()";
                    }
                }

            }
            ExportPermission();//导出权
            if (!string.IsNullOrEmpty(Request.Params["Read"]))//查看页面
            {
                //隐藏提交和保存
                ViewBag.editPermission = ",isShowSave:false";
                ViewBag.submitPermission = "$('#toolbar').children('a').eq(0).hide();";

            }

            return View("info", model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            try
            {
                var list = op.GetList(p => p.RefTable == "IsoFormDesChange" && id.Contains(p.FormID));
                int Count = (
                             from item in list
                             where SetFlowStatus("IsoFormDesChange", item.FormID, item.CreatorEmpId, userInfo.EmpID) != 0
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
                    new DBSql.PubFlow.Flow().Delete(id, "IsoFormDesChange");
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
            var desChange = new DesChange();
            if (FormID > 0)
            {
                model = op.Get(FormID);
            }
            model.MvcSetValue();
            desChange.MvcSetValue();//获取相关的属性值
            string xml = desChange.ModelToXml();
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
                model.RefTable = "IsoFormDesChange";
                model.FormName = "设计变更通知单";
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
        public void BindViewDataValues(DesChange desChange)
        {
            Type t = desChange.GetType();
            PropertyInfo[] proInfo = t.GetProperties();
            foreach (PropertyInfo info in proInfo)
            {
                ViewData[info.Name] = info.GetValue(desChange, null);
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
        public string GetBaseNames(string BaseIDs)
        {
            string BaseName = string.Empty;
            if (!string.IsNullOrEmpty(BaseIDs))
            {
                string[] BaseID = BaseIDs.Split(',');
                foreach (string baseID in BaseID)
                {
                    int ID = JQ.Util.TypeHelper.parseInt(baseID);
                    DataModel.Models.BaseData data = baseData.Get(ID);
                    if (data != null)
                    {
                        if (BaseName != "")
                        {
                            BaseName = BaseName + ",";
                        }

                        BaseName += data.BaseName;
                    }
                }
            }
            return BaseName;
        }


        /// <summary>
        /// 判断是否有导出权
        /// </summary>
        private void ExportPermission()
        {
            List<string> permission = PermissionList("DesignChangeApply");
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