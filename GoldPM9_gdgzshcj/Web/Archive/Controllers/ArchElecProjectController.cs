using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using Common.Data.Extenstions;
using System;
using System.Text;
using System.Data;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Archive.Controllers
{
    [Description("ArchElecProject")]
    public class ArchElecProjectController : CoreController
    {
        private DBSql.Archive.ArchElecProject op = new DBSql.Archive.ArchElecProject();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        private DBSql.Archive.ArchElecFile opArchElecFile = new DBSql.Archive.ArchElecFile();

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            return View();
        }

        [Description("按项目查询列表")]
        public ActionResult archlist()
        {
            return View();
        }

        public ActionResult Center()
        {
            return View();
        }

        public ActionResult treelist()
        {
            int Id = GetRequestValue<int>("Id");
            int ProjID = GetRequestValue<int>("ProjID");
            ViewBag.ArchProjID = Id;
            ViewBag.ProjID = ProjID;

            ViewBag.ArchElecFileId = GetRequestValue<int>("rowid");
            int IsRead = GetRequestValue<int>("IsRead");
            //如果是项目负责人 或 有权限的人员，才能操作
            //ViewBag.permission = JavaScriptSerializerUtil.getJson(());
            var perm = PermissionList("CollctArchiveHand");
            var model = new DBSql.Project.Project().Get(GetRequestValue<int>("ProjID"));

            bool CanEdit = false;
            if (perm.Contains("alledit")) CanEdit = true;
            if (model != null && model.ProjEmpId == userInfo.EmpID) CanEdit = true;

            if (IsRead != 1 && CanEdit)
            {
                ViewBag.permission = "['CanEdit']";
            }
            else
            {
                ViewBag.permission = "['NotEdit']";
            }

            if (perm.Contains("allDown"))
            {
                ViewBag.allDown = "1";
            }
            else
            {
                ViewBag.allDown = "0";
            }
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
                PageModel.SelectOrder = "Id desc";
            }

            var list = op.GetPagedDynamic(PageModel).ToList();

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }

        [Description("列表treejson")]
        [HttpPost]
        public ActionResult treejson(int ProjID, int PhaseID = 0)
        {
            if (ProjID == 0) return Json(null);
            var menulist = op.GetQuery(p => p.ProjectId == ProjID).ToList();
            return Json(tree(menulist, 0));
        }

        /// <summary>
        /// 验证是否有权限下载
        /// </summary>
        /// <param name="ProjID"></param>
        /// <param name="PhaseID"></param>
        /// <returns></returns>
        public ActionResult IsHavePermissionDown(params int[] id)
        {
            string reslut = string.Empty;
            DoResult drreslut = null;
             List< ValidationResult> vrs = new List<ValidationResult>();
             List<string> archList = new List<string>();//存放没有审批通过的文件
             string ids = string.Empty;//不是自己上传的文档
            //1 如果有下载权限直接就可以下载。
            var perm = PermissionList("CollctArchiveHand");
            if (perm.Contains("allDown"))
            {
                drreslut = DoResultHelper.doSuccess("", "操作成功");
                return Json(drreslut);
            }
            else
            {
                //2 如果是自己上传文件直接就可以下载
                 DataTable dt= opArchElecFile.GetNOUserUploadFile(id, userInfo.EmpID);
                 bool isApprovePass = false;
                 if (dt.Rows.Count > 0)
                 {
                     //获取审批通过的文件
                     DataTable dtNoApprove = opArchElecFile.GetApporveUploadFile(  userInfo.EmpID);
                     if (dtNoApprove.Rows.Count > 0)
                     {
                         foreach (DataRow dr in dt.Rows)
                         {
                             isApprovePass = false;
                             ids += dr["Id"].ToString() + ",";

                             foreach (DataRow drApprove in dtNoApprove.Rows)
                             {
                                 string[] strs = drApprove["ColAttVal1"].ToString().Split(',');

                                 foreach (string s in strs)
                                 {
                                     if (dr["id"].ToString().Equals(s))
                                     {
                                         isApprovePass = true;
                                         break;
                                     }
                                 }
                             }
                             //表示该文件没有审批通过下载权限
                             if (isApprovePass == false)
                             {
                                 archList.Add(dr["ElecFileName"].ToString());
                             }

                         }

                         //3 判断是否申请文件下载，并是审批通过的



                     }



                     if (archList.Count > 0)
                     {
                         reslut = string.Join(",", archList);

 
                         vrs.Add(DataAnnotationHelper.validResult(reslut));
                         drreslut = DoResultHelper.doIsValid(vrs);

                         return Json(drreslut);

                     }
                     else
                     {
                         drreslut = DoResultHelper.doSuccess("", "操作成功");
                         return Json(drreslut);
                     }

                     //foreach (DataRow dr in dt.Rows)
                     //{
                     //    reslut += dr["ElecFileName"].ToString() + ",";
                     //}
                     //    vrs.Add(DataAnnotationHelper.validResult(reslut));
                     //   drreslut = DoResultHelper.doIsValid(vrs);
                     
                     //return Json(drreslut);
                 }
                 else
                 {

                     drreslut = DoResultHelper.doSuccess("", "操作成功");
                     return Json(drreslut);
                 }
                
            }
        }
        


        public List<object> tree(IEnumerable<DataModel.Models.ArchElecProject> entitys, int parentid)
        {
            if (!entitys.isNotEmpty()) return null;
            List<object> list = new List<object>();
            var entityarr = entitys.Where(m => m.ParentId == parentid);
            foreach (var item in entityarr)
            {
                //获取子类 递归
                IList<object> clist = tree(entitys, item.Id);
                list.Add(new
                {
                    id = item.Id,
                    parentID = item.ParentId,
                    orderCode = item.ElecNumber,
                    text = item.ElecName,
                    electype = item.ElecType,
                    eleccode = item.ElecCode,
                    children = clist
                });
            }

            if (list.Count > 0) return list;

            return null;
        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add(int ParentId, int ProjectId)
        {
            var model = new ArchElecProject();

            model.ElecGuid = System.Guid.NewGuid().ToString();
            model.ParentId = ParentId;
            model.ProjectId = ProjectId;

            ViewBag.ParentName = op.Get(ParentId).ElecName;


            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id, int ProjectId)
        {
            var model = op.Get(id);

            ViewBag.ParentName = op.Get(model.ParentId).ElecName;

            return View("info", model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int id)
        {
            //删除目录，目录的下级目录信息更改。
            int reuslt = 1;

            var model = op.Get(id);
            var parentID = model.ParentId;

            if (parentID == 0)
            {
                return Json(DoResultHelper.doSuccess("目录不能删除！"));
            }

            InsertLog(model, "，删除目录");

            op.Delete(s => s.Id == id);
            //修改下级
            op.Edit(s => s.ParentId == id, u => new ArchElecProject { ParentId = parentID });
            op.UnitOfWork.SaveChanges();

            //删除目录下的文件
            new DBSql.Archive.ArchElecFile().DeleteDir(id);


            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id)
        {
            var model = new ArchElecProject();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();

            int reuslt = 0;
            if (model.Id > 0)
            {
                model.MvcDefaultEdit(userInfo);
                op.Edit(model);
            }
            else
            {
                model.MvcDefaultSave(userInfo);
                op.Add(model);
            }
            op.UnitOfWork.SaveChanges();

            if (Id > 0)
                InsertLog(model, "，编辑目录");
            else
                InsertLog(model, "，新增目录");



            reuslt = model.Id;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }


        private void InsertLog(ArchElecProject model, string opName)
        {
            DBSql.Sys.BaseLog logAdd = new DBSql.Sys.BaseLog();

            DataModel.Models.BaseLog log = new DataModel.Models.BaseLog();
            log.BaseLogEmpID = userInfo.EmpID;
            log.EmpName = userInfo.EmpName;
            log.BaseLogTypeID = 10;
            log.BaseLogDate = DateTime.Now;
            log.BaseLogIP = GetIP();
            log.BaseLogRefTable = "ArchElecProject";
            log.BaseLogRefID = model.Id;
            log.BaseLogRefHTML = model.ElecName + opName;
            logAdd.Add(log);

            //找到项目名称

            logAdd.UnitOfWork.SaveChanges();
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public string GetTreeList()
        {
            var taskGroupID = JQ.Util.TypeParse.parse<int>(Request.Form["taskGroupID"]);
            if (taskGroupID == 0)
            {
                return "[]";
            }
            var sbJson = new StringBuilder();
            using (var dt = DAL.DBExecute.ExecuteDataTable("SELECT Id,ProjectId,ParentId,ElecCode,ElecType,ElecName FROM ArchElecProject WHERE ProjectId=(SELECT TOP 1 ProjectId FROM ArchElecProject WHERE ElecCode=" + taskGroupID + " AND ElecType=" + (int)DataModel.VolCatalogType.阶段 + ")"))
            {

                var rows = dt.Select("ElecCode=" + taskGroupID + " AND ElecType=" + (int)DataModel.VolCatalogType.阶段);
                if (rows.Length == 0)
                {
                    sbJson.Append("[]");
                }
                else
                {
                    RecuriseTree(dt, rows, sbJson);
                }
            }
            return sbJson.ToString();
        }

        public void RecuriseTree(DataTable source, DataRow[] rows, StringBuilder sbJson)
        {
            sbJson.Append("[");
            for (var i = 0; i < rows.Length; i++)
            {
                if (i != 0)
                {
                    sbJson.Append(",");
                }
                sbJson.Append("{\"id\":\"" + rows[i]["Id"].ToString() + "\",\"text\":\"" + HttpUtility.UrlEncode(rows[i]["ElecName"].ToString()) + "\"");
                var children = source.Select("ParentId=" + rows[i]["Id"].ToString());
                if (children.Length > 0)
                {
                    sbJson.Append(",\"children\":");
                    RecuriseTree(source, children, sbJson);
                }
                sbJson.Append("}");
            }
            sbJson.Append("]");
        }
    }
}
