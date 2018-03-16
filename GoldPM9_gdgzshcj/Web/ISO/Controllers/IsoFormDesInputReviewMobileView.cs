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
    public partial class IsoFormDesInputReviewMobileController : MobileController
    {
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
            //int FlowStatus = SetFlowStatus("IsoFormDesInputReview", model.FormID, model.CreatorEmpId, userInfo.EmpID);
            //if ((permission.Contains("edit") || permission.Contains("alledit")) && (FlowStatus == 0 || FlowStatus == 1))//默认显示
            //{
            //    //编辑权限(保存和提交按钮显示)
            //    ViewBag.editPermission = "";
            //    ViewBag.submitPermission = "";

            //}
            //else
            //{
            //    if (FlowStatus > 1)// 流程自动控制
            //    {
            //        ViewBag.editPermission = "";
            //        ViewBag.submitPermission = "";
            //    }
            //    else
            //    {
            //        if (FlowStatus == -1)//非本人创建
            //        {
            //            //隐藏提交和保存
            //            ViewBag.editPermission = ",isShowSave:false";
            //            ViewBag.submitPermission = "$('#toolbar').children('a').eq(0).hide();";
            //            ViewBag.Upload = "$('#uploadfile1_upload').hide();$('#uploadfile1_delete').hide()";
            //        }
            //    }

            //}
            ExportPermission();//导出权
            return View("info", model);
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
            if (desInputReview != null)
            {
                DataModel.Models.BaseData modelData = baseData.Get(desInputReview.DeptID);
                if (modelData != null)
                {
                    ViewBag.DeptName = modelData.BaseName;//绑定部门信息
                }
                else
                {
                    ViewBag.DeptName = "";
                }
                modelData = baseData.Get(desInputReview.MaterialSourceID);
                if (modelData != null)
                {
                    ViewBag.MaterialNames = modelData.BaseName;
                }
                else
                {
                    ViewBag.MaterialNames = "";
                }
                //ViewBag.SpecName = string.Join(",", GetBaseName(desInputReview.SpeIDs));
            }
        }


        /// <summary>
        /// 判断是否有导出权
        /// </summary>
        private void ExportPermission()
        {
            List<string> permission = PermissionList("IsoFormDesInputReview");
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
