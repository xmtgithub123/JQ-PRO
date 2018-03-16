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
        private DBSql.Iso.IsoForm op = new DBSql.Iso.IsoForm();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        private DBSql.Project.Project proj = new DBSql.Project.Project();
        private DBSql.Iso.IsoFormNode bussdetail = new DBSql.Iso.IsoFormNode();

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
