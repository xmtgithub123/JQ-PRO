using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using Common.Data.Extenstions;

namespace ISO.Controllers
{
    public partial class IsoGCDZKCTJDJZMobileController : MobileController
    {
        #region 添加
        [Description("添加")]
        public ActionResult add()
        {

            var model = new IsoGCDZKCTJDJZ();
            if (GetRequestValue<string>("type") == "xl")
            {
                ViewBag.FormType = "xl";
                ViewBag.FormTypeName = "线路";
                model.SJSM = @" 1、线路总长             地点（附图）        等级
2、拟用断面尺寸(附图)         流速           管道压力
3、拟定基底高程(附纵断)
4、重点地段要求
5、其它";
                model.FTSM = @"1、总平面位置图          比例     份      张
2、重点地段平面位置图    比例     份      张
3、纵断                  比例     份      张
4、横断                  比例     份      张";
                model.CGTSYQ = @"（提供1份盖有勘察单位公章并装订成册的成果文件）
（提供1份与上述盖章文件一致的电子文件）";
                model.TableNumber = "SCX02-08";

            }
            else
            {
                ViewBag.FormType = "dxjz";
                ViewBag.FormTypeName = "单项建筑";
                model.SJSM = @"1、建筑名称             地点（附图）           等级
2、拟用基础型式或尺寸（可附图）         
3、拟定基底高程(桥梁专业可略)
4、估计荷载
5、其它";
                model.FTSM = @"1、总平面位置图            比例     份      张
2、其它：";
                model.CGTSYQ = @"（提供1份盖有勘察单位公章并装订成册的成果文件）
（提供1份与上述盖章文件一致的电子文件）";
                model.TableNumber = "SCX02-09 ";
            }
            ViewBag.CurrEmpID = userInfo.EmpID;

            //设计人员
            var SJR = (from m in new DBSql.Sys.BaseQualification().GetQualificationEmployee((int)DataModel.NodeType.设计, 0, 0, 0)
                       select new { m.EmpID, m.EmpName }).Distinct();
            ViewBag.Qualification = JavaScriptSerializerUtil.getJson(SJR);

            var per = PermissionList("ProjectCenterList");

            if (per.Contains("allDown"))
            {
                ViewBag.Permission = 1;
            }
            else
            {
                ViewBag.Permission = 0;
            }

            DataModel.Models.DesTaskGroup dtg = new DBSql.Design.DesTaskGroup().Get(GetRequestValue<int>("taskGroupId"));
            if (dtg != null)
            {
                model.ProjID = dtg.ProjId;
                model.ProjName = dtg.ProjName;
                model.ProjNumber = dtg.ProjNumber;
                model.ProjPhaseId = GetRequestValue<string>("phaseID");
                model.ProjPhaseName = new DBSql.Sys.BaseData().Get(GetRequestValue<int>("phaseID")).BaseName;
            }

            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            if (model.FormType == "xl")
            {
                ViewBag.FormType = "xl";
                ViewBag.FormTypeName = "线路";
            }
            else
            {
                ViewBag.FormType = "dxjz";
                ViewBag.FormTypeName = "单项建筑";
            }

            //设计人员
            var SJR = (from m in new DBSql.Sys.BaseQualification().GetQualificationEmployee((int)DataModel.NodeType.设计, 0, 0, 0)
                       select new { m.EmpID, m.EmpName }).Distinct();
            ViewBag.Qualification = JavaScriptSerializerUtil.getJson(SJR);

            var per = PermissionList("ProjectCenterList");

            if (per.Contains("allDown"))
            {
                ViewBag.Permission = 1;
            }
            else
            {
                ViewBag.Permission = 0;
            }

            ViewBag.CurrEmpID = model.CreatorEmpId;
            return View("info", model);
        }
        #endregion

    }


}
