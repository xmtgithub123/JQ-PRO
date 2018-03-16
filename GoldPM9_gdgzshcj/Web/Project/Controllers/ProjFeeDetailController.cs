using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Project.Controllers
{
    [Description("ProjFeeDetail")]
    public class ProjFeeDetailController : CoreController
    {
        private DBSql.Design.DesTaskFeeDetails op = new DBSql.Design.DesTaskFeeDetails();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            return View();
        }

        /// <summary>
        /// 选择阶段工程
        /// </summary>
        /// <returns></returns>
        public ActionResult selectTask()
        {
            return View();
        }

        /// <summary>
        /// 显示导入数据
        /// </summary>
        /// <returns></returns>
        public ActionResult ExcelData()
        {
            ViewBag.ModelUrl = Url.Content("~/download/技经费用导入模版.xlsx");
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
                PageModel.SelectOrder = " dt.Id desc ";
            }
            PageModel.SelectCondtion.Add("NoFatherProjID", "false");
            var list = op.GetFeeDetailsList(PageModel);

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }

        public string selectTaskJson()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = " g.ProjId desc,g.Id asc ";
            }
            PageModel.SelectCondtion.Add("NoJoinDetail", "1");
            string otherColumn = " ,isnull((select Top 1 Id, FeeZBF, FeeXMQQF, FeeKCF, FeeSJF, FeeYSBZF, FeeJGTBZF, FeeZTZ  from DesTaskFeeDetails dts where dts.TaskGroupId = g.Id for xml raw),'') as TaskFeeDetails";
            PageModel.SelectCondtion.Add("otherColumn", otherColumn);
            var list = new DBSql.Design.DesTask().GetProjectPlanList(PageModel);

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new DesTaskFeeDetails();
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            DataModel.Models.DesTaskGroup TaskG = op.DbContext.Set<DataModel.Models.DesTaskGroup>().FirstOrDefault(p => p.Id == model.TaskGroupId);
            ViewBag.ProjNumber = TaskG.ProjNumber;
            ViewBag.ProjName = TaskG.ProjName;
            ViewBag.ProjPhase = (new DBSql.Sys.BaseData().GetBaseDataInfo(TaskG.TaskGroupPhaseId)).BaseName;
            ViewBag.ProjEmpName = TaskG.TaskGroupEmpName;
            return View("info", model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 0;
            DoResult dr = new DoResult();
            try
            {
                op.Delete(s => id.Contains(s.Id));
                op.UnitOfWork.SaveChanges();
                dr = DoResultHelper.doSuccess(reuslt, "操作成功");
            }
            catch (System.Exception ex)
            {
                dr = DoResultHelper.doSuccess(reuslt, "操作失败");
                throw;
            }

            return Json(dr);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id)
        {
            var model = new DesTaskFeeDetails();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();

            int reuslt = 0;
            if (model.Id > 0)
            {
                model.MvcDefaultEdit(userInfo.EmpID);
                op.Edit(model);
                op.UnitOfWork.SaveChanges();
                reuslt = model.Id;
            }
            else
            {
                model.MvcDefaultSave(userInfo.EmpID);
                op.Add(model);
                op.UnitOfWork.SaveChanges();
                reuslt = model.Id;
            }

            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 导入数据操作
        string[] Columns = new string[] { "项目编号", "项目名称", "项目阶段", "招标费", "项目前期费", "设计费", "预算编制费", "竣工图编制费", "工程勘察费", "总投资" };
        private class ResultClass
        {
            public string Msg;
            public string Status;
        }

        private List<ResultClass> _list = new List<ResultClass>();

        public string GetExcelData()
        {
            try
            {
                string TempPath = JQ.Util.IOUtil.GetTempPath();
                if (Request.Params["files"] != null)
                {
                    List<string> FileArray = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<List<string>>(Request.Params["files"]);
                    if (FileArray.Count > 0)
                    {
                        foreach (string path in FileArray)
                        {
                            DataTable dt = Common.ExcelRender.RenderFromExcel(System.IO.Path.Combine(TempPath, path));
                            foreach (var item in Columns)
                            {
                                if (!dt.Columns.Contains(item))
                                {
                                    dt.Columns.Add(item, typeof(decimal));
                                    dt.Columns[item].DefaultValue = 0;
                                }
                            }

                            foreach (DataRow drItem in dt.Rows)
                            {
                                InsertTable(drItem);
                            }
                        }
                    }
                }
                //return Json(DoResultHelper.doSuccess(1, JavaScriptSerializerUtil.getJson(_list)));
                return JavaScriptSerializerUtil.getJson(new
                {
                    stateType = 0,
                    stateValue = 1,
                    stateMsg = _list,
                });
            }
            catch (System.Exception ex)
            {
                return JavaScriptSerializerUtil.getJson(DoResultHelper.doSuccess(-1, "操作失败"));
            }

        }

        private void InsertTable(DataRow dr)
        {
            string ProjNumber = dr["项目编号"].ToString();
            DataModel.Models.Project dmProj = new DBSql.Project.Project().FirstOrDefault(p => p.ProjNumber == ProjNumber);
            if (dmProj == null)
            {
                _list.Add(new ResultClass() { Msg = string.Format("项目编号:{0},无法在系统中查询！", ProjNumber), Status = "err" });
                return;
            }
            string PhaseName = dr["项目阶段"].ToString();
            DataModel.Models.BaseData phaseData = new DBSql.Sys.BaseData().FirstOrDefault(p => p.BaseName == PhaseName);
            if (phaseData == null)
            {
                _list.Add(new ResultClass() { Msg = string.Format("项目编号:{0},无法在系统中查询相应{1}阶段！", ProjNumber, PhaseName), Status = "err" });
                return;
            }
            DataModel.Models.DesTaskGroup dmGroup = new DBSql.Design.DesTaskGroup().FirstOrDefault(p => p.ProjId == dmProj.Id && p.TaskGroupPhaseId == phaseData.BaseID);
            if (dmGroup == null)
            {
                _list.Add(new ResultClass() { Msg = string.Format("项目编号:{0},无法在系统中查询相应{1}阶段项目！", ProjNumber, PhaseName), Status = "err" });
                return;
            }
            DataModel.Models.DesTaskFeeDetails details = new DBSql.Design.DesTaskFeeDetails().FirstOrDefault(p => p.TaskGroupId == dmGroup.Id);
            if (details == null)
            {
                details = new DesTaskFeeDetails();
                details.ProjId = dmGroup.ProjId;
                details.TaskGroupId = dmGroup.Id;
                details.PhaseID = dmGroup.TaskGroupPhaseId;
                details.TaskID = 0;
                details.FeeZBF = Common.ExtensionMethods.Value<decimal>(dr["招标费"].ToString());
                details.FeeXMQQF = Common.ExtensionMethods.Value<decimal>(dr["项目前期费"].ToString());
                details.FeeKCF = Common.ExtensionMethods.Value<decimal>(dr["工程勘察费"].ToString());
                details.FeeSJF = Common.ExtensionMethods.Value<decimal>(dr["设计费"].ToString());
                details.FeeYSBZF = Common.ExtensionMethods.Value<decimal>(dr["预算编制费"].ToString());
                details.FeeJGTBZF = Common.ExtensionMethods.Value<decimal>(dr["竣工图编制费"].ToString());
                details.FeeZTZ = Common.ExtensionMethods.Value<decimal>(dr["总投资"].ToString());
                details.MvcDefaultSave(userInfo.EmpID);
                op.DbContext.Set<DesTaskFeeDetails>().Add(details);
                op.UnitOfWork.SaveChanges();
                _list.Add(new ResultClass() { Msg = string.Format("项目编号:{0},阶段:{1}。插入成功！", ProjNumber, PhaseName), Status = "suc" });
            }
            else
            {
                _list.Add(new ResultClass() { Msg = string.Format("项目编号:{0},阶段:{1}。已存在技经费用。请手动进行调整！", ProjNumber, PhaseName), Status = "err" });
            }

        }

        #endregion

    }
}
