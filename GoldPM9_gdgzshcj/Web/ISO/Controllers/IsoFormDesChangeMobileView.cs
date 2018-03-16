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

    public partial class IsoFormDesChangeMobileController : MobileController
    {
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


            //List<string> permission = PermissionList("DesignChangeApply");
            ////int FlowStatus = GetFlowStatusId(model.FormID, model.CreatorEmpName);
            //int FlowStatus = SetFlowStatus("IsoFormDesChange",model.FormID,model.CreatorEmpId,userInfo.EmpID);
            //if ((permission.Contains("edit") || permission.Contains("alledit")) && (FlowStatus==0||FlowStatus==1))//默认显示
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
            //        if (FlowStatus == -1)
            //        {
            //            //隐藏提交和保存
            //            ViewBag.editPermission = ",isShowSave:false";
            //            ViewBag.submitPermission = "$('#toolbar').children('a').eq(0).hide();";
            //            ViewBag.Upload = "$('#DesChangeUpload_upload').hide();$('#DesChangeUpload_delete').hide()";
            //        }
            //    }

            //}
            //ExportPermission();//导出权
            //if (!string.IsNullOrEmpty(Request.Params["Read"]))//查看页面
            //{
            //    //隐藏提交和保存
            //    ViewBag.editPermission = ",isShowSave:false";
            //    ViewBag.submitPermission = "$('#toolbar').children('a').eq(0).hide();";

            //}

            return View("info", model);
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
