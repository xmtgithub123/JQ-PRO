using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System;
using System.Web.Script.Serialization;
using System.Data;
using Common;
using Common.Data.Extenstions;
using System.IO;
using System.Web;
using DataModel;

namespace Project.Controllers
{
    public partial class ProjectMobileController : MobileController
    {
        #region 添加
        //添加项目信息
        [Description("添加")]
        public ActionResult add()
        {
            //阶段
            var ProjPhaseIds = from m in new DBSql.Sys.BaseData().GetDataSetByEngName("ProjectPhase") select new { value = string.Format("{0}", m.BaseID), text = m.BaseName };
            ViewData["ProjPhaseIds"] = JavaScriptSerializerUtil.getJson(ProjPhaseIds);
            ViewBag.ProjPhaseIds = JavaScriptSerializerUtil.getJson(ProjPhaseIds);

            //类型
            //var ProjTypeIds = from m in new DBSql.Sys.BaseData().GetBaseDataInfoByEngName("ProjectType") select new { value = string.Format("{0}", m.BaseID), text = m.BaseName };
            // var ProjTypeIds = from m in new DBSql.Sys.BaseDataSystem().GetBaseDataInfoByEngName("ProjectType")  select new { value = string.Format("{0}", m.BaseID), text = m.BaseName };
            var DbBaseData = new DBSql.Sys.BaseData();
            var ProjTypeIds = DbBaseData.getTreeData(DbBaseData.GetBaseDataInfoByEngName("ProjectType"), 0, ((int)ProjectTypeModel.主网).ToString());
            ViewData["ProjTypeIds"] = JavaScriptSerializerUtil.getJson(ProjTypeIds);
            ViewData["ProjTypeItem"] = JavaScriptSerializerUtil.getJson(from m in new DBSql.Sys.BaseData().GetDataSetByEngName("ProjectType") select new { id = string.Format("{0}", m.BaseID), text = m.BaseName });

            //new DBSql.Sys.BaseData().GetBaseDataInfoByEngName(item.engName.ToString());

            ////电压
            //var ProjectVoltIds = from m in new DBSql.Sys.BaseData().GetDataSetByEngName("ProjectVolt") select new { value = string.Format("{0}", m.BaseID), text = m.BaseName };
            //ViewData["ProjectVoltIds"] = JavaScriptSerializerUtil.getJson(ProjectVoltIds);

            ////回路数
            //var ProjCircuitsIds = from m in new DBSql.Sys.BaseData().GetDataSetByEngName("ProjCircuits") select new { value = string.Format("{0}", m.BaseID), text = m.BaseName };
            //ViewData["ProjCircuitsIds"] = JavaScriptSerializerUtil.getJson(ProjCircuitsIds);

            //项目负责人
            var ProjRespEmps = (from m in new DBSql.Sys.BaseQualification().GetQualificationEmployee((int)DataModel.NodeType.项目设总, 0, 0, 0)
                                select new { m.EmpID, m.EmpName }).Distinct();
            ViewBag.Qualification = JavaScriptSerializerUtil.getJson(ProjRespEmps);

            var model = new DataModel.Models.Project();
            ViewBag.permission = "['add','submit']";

            model.DateCreate = DateTime.Now;
            model.CreatorEmpName = userInfo.EmpName;
            ViewBag.Variety = 1;
            ViewBag.ChildModelId = 0;

            string companyType = Request.Params["CompanyType"].ToString();
            string page = "info";
            switch (companyType)
            {
                case "SJ":
                    page = "info_sj";
                    break;
                case "GC":
                    page = "info_gc";
                    break;
                case "CJ":
                    page = "info_cj";
                    break;
                default:
                    page = "info";
                    break;
            }

            return View(page, model);
        }
        #endregion

        #region 编辑
        //修改项目信息
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            //阶段
            var ProjPhaseIds = from m in new DBSql.Sys.BaseData().GetDataSetByEngName("ProjectPhase") select new { value = string.Format("{0}", m.BaseID), text = m.BaseName };
            ViewData["ProjPhaseIds"] = JavaScriptSerializerUtil.getJson(ProjPhaseIds);

            //类型
            var DbBaseData = new DBSql.Sys.BaseData();
            var ProjTypeIds = DbBaseData.getTreeData(DbBaseData.GetBaseDataInfoByEngName("ProjectType"), 0, ((int)ProjectTypeModel.主网).ToString());
            ViewData["ProjTypeIds"] = JavaScriptSerializerUtil.getJson(ProjTypeIds);
            ViewData["ProjTypeItem"] = JavaScriptSerializerUtil.getJson(from m in new DBSql.Sys.BaseData().GetDataSetByEngName("ProjectType") select new { id = string.Format("{0}", m.BaseID), text = m.BaseName });

            //项目负责人
            var ProjRespEmps = (from m in new DBSql.Sys.BaseQualification().GetQualificationEmployee((int)DataModel.NodeType.项目设总, 0, 0, 0)
                                select new { m.EmpID, m.EmpName }).Distinct();

            ViewBag.Qualification = JavaScriptSerializerUtil.getJson(ProjRespEmps);

            var childmodel = dbProject.Get(id);
            var model = dbProject.Get(childmodel.ParentId);
            if (model.ProjEmpName == "")
            {
                model.ProjEmpName = childmodel.ProjEmpName;
            }
            if (model.ProjEmpId == 0)
            {
                model.ProjEmpId = childmodel.ProjEmpId;
            }
            ViewBag.ChildModelId = childmodel.Id;
            ViewBag.Guid = childmodel.BridgeGuid;
            if (childmodel.IsQuote == 1)
            {
                ViewBag.Variety = 0;
            }
            else
            {
                ViewBag.Variety = 1;
            }
            ViewBag.isquote = childmodel.IsQuote;

            string companyType = Request.Params["CompanyType"].ToString();
            string page = "info";

            switch (companyType)
            {
                case "SJ":
                    page = "info_sj";
                    break;
                case "GC":
                    page = "info_gc";
                    break;
                case "CJ":
                    page = "info_cj";
                    break;
                default:
                    page = "info";
                    break;
            }

            return View(page, model);
        }
        #endregion
    }
}