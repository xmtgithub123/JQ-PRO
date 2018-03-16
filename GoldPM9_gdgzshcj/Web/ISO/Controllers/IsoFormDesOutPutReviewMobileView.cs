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
    public partial class IsoFormDesOutPutReviewMobileController : MobileController
    {
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
            //int FlowStatus = SetFlowStatus("IsoFormDesOutPutReview", model.FormID, model.CreatorEmpId, userInfo.EmpID);
            //if ((permission.Contains("edit") || permission.Contains("alledit")) && (FlowStatus == 0 || FlowStatus == 1))//默认显示
            //{
            //    //编辑权限(保存和提交按钮显示)
            //    ViewBag.editPermission = "";
            //    ViewBag.submitPermission = "";
            //    if (FlowStatus == 0)
            //    {
            //        ViewBag.detailPermission = 1;//可进行编辑
            //    }
            //    else
            //    {
            //        ViewBag.detailPermission = 0;
            //    }

            //}
            //else
            //{
            //    if (FlowStatus > 1)// 流程自动控制
            //    {
            //        ViewBag.editPermission = "";
            //        ViewBag.submitPermission = "";
            //        ViewBag.detailPermission = 0;
            //    }
            //    else
            //    {
            //        if (FlowStatus == -1)//非本人创建（查看）
            //        {
            //            //隐藏提交和保存
            //            ViewBag.editPermission = ",isShowSave:false";
            //            ViewBag.submitPermission = "$('#toolbar').children('a').eq(0).hide();";
            //            ViewBag.Upload = "$('#uploadfile1_upload').hide();$('#uploadfile1_delete').hide();";
            //            ViewBag.detailPermission = 0;
            //        }
            //    }

            //}
            ExportPermission();//导出权
            return View("info", model);
        }
        #endregion
    }
}
