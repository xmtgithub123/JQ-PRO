using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
namespace Core.Controllers
{
    [Description("BaseQualification")]
    public class BaseQualificationController : CoreController
    {
        private DBSql.Sys.BaseQualification op = new DBSql.Sys.BaseQualification();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        private DBSql.Sys.BaseData opd = new DBSql.Sys.BaseData();
        private DBSql.Sys.BaseEmployee ope = new DBSql.Sys.BaseEmployee();

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
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
                PageModel.SelectOrder = "QualificationEmpID desc";
            }
            var list = op.GetPagedDynamic(PageModel).ToList();

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }



        [HttpPost]
        public ActionResult SaveQual(string jsonData, int SpecID = 0, int EmpID = 0, int DepID = 0)
        {
            List<DataModel.BaseQualification> model = null;
            if (!string.IsNullOrEmpty(jsonData))
            {
                model = JavaScriptSerializerUtil.parseFormJson<List<DataModel.BaseQualification>>(jsonData);
            }
            if (model == null)
            {
                model = new List<DataModel.BaseQualification>();
            }
            op.UpdateQualificationEmployee(model, DepID, SpecID, EmpID);
            return Json(DoResultHelper.doSuccess("保存成功"));
        }


        [Description("Json")]
        [HttpPost]
        public ActionResult Qualjson(int SpecID = 0, int EmpID = 0, int DepID = 0)
        {
            //按照部门、按照人员
            if (EmpID > 0)
            {
                var QualList = op.GetQualificationEmployee(0, 0, 0, EmpID);
                var data = opd.GetDataSetByEngName("Special");

                var list = (from s in data
                            select new
                            {
                                id = s.BaseID,
                                text = s.BaseName,
                                Qual18 = QualValue(s.BaseID, 18, QualList),
                                Qual16 = QualValue(s.BaseID, 16, QualList),
                                Qual19 = QualValue(s.BaseID, 19, QualList),
                                Qual134 = QualValue(s.BaseID, 134, QualList),
                                Qual20 = QualValue(s.BaseID, 20, QualList),
                                Qual21 = QualValue(s.BaseID, 21, QualList),
                                Qual22 = QualValue(s.BaseID, 22, QualList),
                            });
                return Json(list);
            }
            else if (SpecID > 0)
            {
                var QualList = op.GetQualificationEmployee(0, SpecID, DepID, 0);
                var data = ope.GetEmpList(DepID);

                var list = (from s in data
                            orderby s.DepartmentOrder ascending, s.EmpOrder ascending
                            select new
                            {
                                id = s.EmpID,
                                text = s.EmpName,
                                DepName = s.DepartmentName,
                                Qual18 = QualEmpValue(s.EmpID, 18, QualList),
                                Qual16 = QualEmpValue(s.EmpID, 16, QualList),
                                Qual19 = QualEmpValue(s.EmpID, 19, QualList),
                                Qual134 = QualEmpValue(s.EmpID, 134, QualList),
                                Qual20 = QualEmpValue(s.EmpID, 20, QualList),
                                Qual21 = QualEmpValue(s.EmpID, 21, QualList),
                                Qual22 = QualEmpValue(s.EmpID, 22, QualList),
                            });
                return Json(list);
            }

            return Json(null);

        }

        string QualEmpValue(int EmpID, int QulValue, IEnumerable<DBSql.Sys.EmpQualification> list)
        {
            int v = list.Count(s => s.EmpID == EmpID && s.QualificationValue == QulValue);

            if (v != 0)
            {
                return "checked";
            }

            return "";
        }

        string QualValue(int SpecID, int QulValue, IEnumerable<DBSql.Sys.EmpQualification> list)
        {
            int v = list.Count(s => s.QualificationSpecID == SpecID && s.QualificationValue == QulValue);

            if (v != 0)
            {
                return "checked";
            }

            return "";
        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new BaseQualification();
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            return View("info", model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 0;
            op.Delete(s => id.Contains(s.QualificationEmpID));
            op.UnitOfWork.SaveChanges();
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int QualificationEmpID)
        {
            var model = new BaseQualification();
            if (QualificationEmpID > 0)
            {
                model = op.Get(QualificationEmpID);
            }
            model.MvcSetValue();

            int reuslt = 0;
            if (model.QualificationEmpID > 0)
            {
                op.Edit(model);
            }
            else
            {
                op.Add(model);
            }
            op.UnitOfWork.SaveChanges();
            reuslt = model.QualificationEmpID;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.QualificationEmpID, "操作成功") : DoResultHelper.doSuccess(model.QualificationEmpID, "操作失败");
            return Json(dr);
        }
        #endregion
    }
}
