using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System;
using System.Data;
using Aspose.Words;
using System.Text;

namespace Design.Controllers
{
    [Description("DesMutiSign")]
    public partial class DesMutiSignController : CoreController
    {
        private DBSql.Design.DesMutiSign op = new DBSql.Design.DesMutiSign();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        /**********************任务处理********************/

        /// <summary>
        /// 会签列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult DesMutiSignList()
        {
            ViewBag.taskId = Request.Params["taskId"] == null ? "0" : Request.Params["taskId"].ToString();
            ViewBag.EmpID = userInfo.EmpID;

            var divid = Common.ModelConvert.ConvertToDefaultType<string>(Request.Params["divid"]); // 所在对话框ID
            ViewBag._DialogId = divid;
            return View();
        }

        /// <summary>
        /// 会签列表数据
        /// </summary>
        /// <returns></returns>
        public string json()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (Request.Params["taskId"] != null)
            {
                PageModel.SelectCondtion.Add("TaskID", Request.Params["taskId"].ToString());
            }
            PageModel.SelectCondtion.Add("DeleterEmpId", 0);
            var list = op.DesMutiSignkList(PageModel);


            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list,
            });
        }

        public ActionResult DesMutiSignInfo(int taskId)
        {
            var model = new DesMutiSign();

            ViewBag.fileIds = "";
            if (Request.Params["fileIds"] != null)
            {
                ViewBag.fileIds = string.Join(",", (from n in Request.Params["fileIds"].ToString().Split(',') where n != "" select n).ToArray());
            }

            DataModel.Models.DesTask _TaskModel = new DBSql.Design.DesTask().Get(taskId);
            ViewBag.TaskModel = _TaskModel;

            DataModel.Models.Project _ProjModel = new DBSql.Project.Project().Get(_TaskModel.ProjId);
            ViewBag.ProjModel = _ProjModel;

            DataModel.Models.DesTaskGroup _TaskGroup = new DBSql.Design.DesTaskGroup().Get(_TaskModel.TaskGroupId);
            ViewBag.TaskGroupName = _TaskGroup.TaskGroupName;
            DataModel.Models.BaseData dmPhase = new DBSql.Sys.BaseData().Get(_TaskGroup.TaskGroupPhaseId);
            ViewBag.TaskGroupPhaseName = dmPhase == null ? "" : dmPhase.BaseName;

            model.CreationTime = System.DateTime.Now;
            model.CreatorEmpName = userInfo.EmpName;
            DataTable dt = new DBSql.Design.DesTask().GetSpecHeaderByTaskId(Convert.ToInt32(_TaskModel.Id));
            if (dt.Rows.Count > 0)
            {
                ViewBag.SQZY = dt.Rows[0]["TaskName"];
                ViewBag.SQZYFZR = dt.Rows[0]["TaskEmpName"];
            }
            else
            {
                ViewBag.SQZY = "";
                ViewBag.SQZYFZR = "";
            }

            return View("DesMutiSignInfo", model);
        }

        public ActionResult Edit(int Id)
        {
            var model = op.Get(Id);
            if (model == null)
            {
                return Json(DoResultHelper.doSuccess(-1, "缺少关键接收人，操作失败！"), JsonRequestBehavior.AllowGet);
            }
            ViewBag.fileIds = "";
            List<DataModel.Models.DesMutiSignAttach> attachList = op.DbContext.Set<DataModel.Models.DesMutiSignAttach>().Where(p => p.MutiSignID == model.Id).ToList();
            ViewBag.fileIds = string.Join(",", attachList.Select(p => p.AttachID.ToString()).ToArray());

            DataModel.Models.DesTask _TaskModel = new DBSql.Design.DesTask().Get(model.TaskId);
            ViewBag.TaskModel = _TaskModel;
            ViewBag.TaskSpecName = (new DBSql.Sys.BaseData().Get(_TaskModel.TaskSpecId, "Special")) == null ? "项目汇总" : (new DBSql.Sys.BaseData().Get(_TaskModel.TaskSpecId, "Special")).BaseName;

            DataModel.Models.Project _ProjModel = new DBSql.Project.Project().Get(_TaskModel.ProjId);
            ViewBag.ProjModel = _ProjModel;

            DataModel.Models.DesTaskGroup _TaskGroup = new DBSql.Design.DesTaskGroup().Get(_TaskModel.TaskGroupId);
            ViewBag.TaskGroup = _TaskGroup;
            DataModel.Models.BaseData dmPhase = new DBSql.Sys.BaseData().Get(_TaskGroup.TaskGroupPhaseId);
            ViewBag.TaskGroupPhaseName = dmPhase == null ? "" : dmPhase.BaseName;

            var list = op.DbContext.Set<DataModel.Models.DesMutiSignRec>().Where(p => p.MutiSignId == model.Id).Select(p => new
            {
                SpecID = p.RecSpecId,
                SpecName = p.RecSpecName,
                EmpName = p.RecEmpName,
                EmpID = p.RecEmpId,
                Note=p.RecContent,
                Time=p.RecDealDate
            }).ToList();

            ViewBag.SpecEmps = JavaScriptSerializerUtil.getJson(list);


            string html = "";
            for (int i=0;i< list.Count; i++)
            {
                html += "<tr>";
                html += "<td style='width:65%;font-weight:bold;text-align:left;border:0.5pt solid windowtext;padding:2px;height:200px;' >会签意见（注明会签文件名称）：" + list[i].Note + "<br/><br/></td>";
                html += "<td valign='bottom' style='font-weight:bold;text-align:left;border:0.5pt solid windowtext;padding:2px;width:35%;height:200px;'>会签专业：" + list[i].SpecName + "<br/>专业负责人/日期：" + list[i].EmpName + "</td>";
                html += "</tr>";
            }
            ViewBag.HTML = html;


            DataTable dt = new DBSql.Design.DesTask().GetSpecHeaderByTaskId(Convert.ToInt32(_TaskModel.Id));
            if (dt.Rows.Count > 0)
            {
                ViewBag.SQZY = dt.Rows[0]["TaskName"];
                ViewBag.SQZYFZR = dt.Rows[0]["TaskEmpName"];
            }
            else
            {
                ViewBag.SQZY = "";
                ViewBag.SQZYFZR = "";
            }

            if (Request.Params["from"] != null && Request.Params["from"] == "Remind")
            {
                if (model.MutiStatus == 0)
                {
                    ViewData["showAgree"] = true;
                }
                ViewData["RecID"] = Request.Params["RecID"].ToString();
            }

            var per = JavaScriptSerializerUtil.getJson((PermissionList("ProjectCenterList")));
            if (per.Contains("allDown"))
            {
                ViewBag.Permission = 1;
            }
            else
            {
                ViewBag.Permission = 0;
            }

            return View("DesMutiSignInfo", model);
        }

        /// <summary>
        /// 保存 任务提交会签 数据
        /// </summary>
        /// <returns></returns>
        public JsonResult save()
        {
            DataModel.Models.DesMutiSign model = new DesMutiSign();
            model.MvcSetValue();
            model.MvcDefaultSave(userInfo.EmpID);

            DataModel.Models.DesTask _TaskModel = new DBSql.Design.DesTask().Get(model.TaskId);
            model.ProjId = _TaskModel.ProjId;
            model.MutiSignPhaseId = _TaskModel.TaskPhaseId;
            model.MutiSignSpecId = _TaskModel.TaskSpecId;
            model.MutiSignSpecName = (new DBSql.Sys.BaseData().Get(model.MutiSignSpecId)).BaseName;

            var attachlist = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<List<DataModel.Models.DesMutiSignAttach>>(Request.Params["AttachIDDate"].ToString());

            var reclist = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<List<DataModel.Models.DesMutiSignRec>>(Request.Params["RecEmpData"].ToString());
            if (reclist.Count == 0)
            {
                return Json(DoResultHelper.doSuccess("-1", "保存失败，缺少会签人！"), JsonRequestBehavior.AllowGet);
            }
            int reuslt = op.InsertDesMutiSign(model, attachlist, reclist);

            DoResult dr = new DoResult();
            dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt.ToString(), "发起会签成功！") : DoResultHelper.doSuccess(reuslt.ToString(), "操作失败！"); ;
            return Json(dr, JsonRequestBehavior.AllowGet);
        }

        public JsonResult del(int id)
        {
            int result = 0;
            try
            {
                DataModel.Models.DesMutiSign dmSign = op.Get(id);
                dmSign.DeleterEmpId = userInfo.EmpID;
                dmSign.DeleterEmpName = userInfo.EmpName;
                dmSign.DeletionTime = System.DateTime.Now;
                op.Edit(dmSign);
                op.UnitOfWork.SaveChanges();
                result = 1;
            }
            catch (System.Exception)
            {
                result = -1;
                throw;
            }
            DoResult dr = new DoResult();
            dr = result > 0 ? DoResultHelper.doSuccess(result.ToString(), "删除成功！") : DoResultHelper.doSuccess(result.ToString(), "删除失败！"); ;
            return Json(dr, JsonRequestBehavior.AllowGet);
        }

        [Description("图纸会签")]
        [HttpPost]
        public JsonResult UpdateRec(FormCollection Condition)
        {
            if (Condition["RecID"] == null)
            {
                return Json(DoResultHelper.doSuccess(-1, "缺少关键接收人，操作失败！"));
            }
            if (Condition["RecStatus"] == null)
            {
                return Json(DoResultHelper.doSuccess(-1, "缺少关键动作，操作失败！"));
            }
            int ID = Common.ExtensionMethods.Value<int>(Condition["Id"]);
            int RecID = Common.ExtensionMethods.Value<int>(Condition["RecID"]);
            DataModel.Models.DesMutiSignRec dmRec = op.DbContext.Set<DataModel.Models.DesMutiSignRec>().FirstOrDefault(p => p.Id == RecID);
            dmRec.RecStatus = Common.ExtensionMethods.Value<int>(Condition["RecStatus"]);
            dmRec.RecContent = Common.ExtensionMethods.Value<string>(Condition["RecContent"]);
            dmRec.RecDealDate = System.DateTime.Now;

            using (var tran = op.DbContext.Database.BeginTransaction())
            {
                try
                {
                    op.DbContext.Entry(dmRec).State = System.Data.Entity.EntityState.Modified;
                    op.DbContext.SaveChanges();
                    //判断是否都处理完成
                    List<DataModel.Models.DesMutiSignRec> recList = op.DbContext.Set<DataModel.Models.DesMutiSignRec>().Where(p => p.RecStatus == 0 && p.Id != dmRec.Id && p.MutiSignId == ID).ToList();
                    if (recList.Count == 0)
                    {
                        DataModel.Models.DesMutiSign dmMuli = op.Get(ID);
                        dmMuli.MutiStatus = 1;
                        dmMuli.MutiFinishDate = System.DateTime.Now;
                        op.Edit(dmMuli);
                        op.DbContext.SaveChanges();
                    }
                    //添加到流转记录中
                    Flow dmFlow = op.DbContext.Set<Flow>().FirstOrDefault(p => p.FlowRefTable == "DesMutiSign" && p.FlowRefID == ID);
                    if (dmFlow != null)
                    {
                        DataModel.Models.FlowNodeExe exeModel = new FlowNodeExe();
                        exeModel.FlowID = dmFlow.Id;
                        exeModel.ExeActionID = (int)DataModel.NodeAction.完成;
                        exeModel.ExeActionDate = System.DateTime.Now;
                        exeModel.ExeEmpId = userInfo.EmpID;
                        exeModel.ExeEmpName = userInfo.EmpName;
                        exeModel.ExeArgEmpId = userInfo.AgenEmpID;
                        exeModel.ExeArgEmpName = userInfo.AgenEmpName == null ? "" : userInfo.AgenEmpName;
                        exeModel.ExeNote = string.Format("【{0}】—备注：{1}", (dmRec.RecStatus == 1 ? "不同意" : (dmRec.RecStatus == 2 ? "同意" : "")), dmRec.RecContent);
                        op.DbContext.Set<FlowNodeExe>().Add(exeModel);
                        op.DbContext.SaveChanges();
                    }
                    tran.Commit();

                    #region 会签人消息处理
                    DBSql.OA.OaSendMess _send = new DBSql.OA.OaSendMess();
                    DataModel.Models.DesMutiSign dmMuliModel = op.Get(ID);
                    DataModel.Models.DesTask dmTaskModel = new DBSql.Design.DesTask().Get(dmMuliModel.TaskId);
                    DataModel.Models.DesTaskGroup dmGroup = new DBSql.Design.DesTaskGroup().Get(dmTaskModel.TaskGroupId);
                    _send.MessTitle = string.Format("[会签单]—{0}({1}{2})", dmTaskModel.TaskName, userInfo.EmpName, (dmRec.RecStatus == 1 ? "不同意" : (dmRec.RecStatus == 2 ? "同意" : "")));
                    _send.MessNote = string.Format("{0}<br />{1}<br />{2}<br />{3}",
                        "" + userInfo.EmpName + "会签意见：" + (dmRec.RecStatus == 1 ? "不同意" : (dmRec.RecStatus == 2 ? "同意" : "")),
                        "备注：" + dmRec.RecContent,
                        "项目信息：" + dmGroup.ProjNumber + dmGroup.ProjName,
                        "任务名称：" + dmTaskModel.TaskName
                        );
                    _send.EmpID = userInfo.EmpID;
                    _send.MessRefID = ID;
                    _send.MessRefTable = "DesMutiSignForm";
                    _send.DialogWidth = 800;
                    _send.DialogHeight = 600;
                    _send.RecEmpID = (new int[] { dmMuliModel.CreatorEmpId }).ToList();
                    _send.MessLinkTitle = _send.MessTitle;
                    _send.MessLinkUrl = string.Format("Design/DesMutiSign/Edit?id={0}", dmMuliModel.Id);
                    _send.SendMess();
                    #endregion
                    return Json(DoResultHelper.doSuccess(1, "处理成功！"));
                }
                catch (System.Exception ex)
                {
                    tran.Rollback();
                    return Json(DoResultHelper.doSuccess(-1, "处理失败！"));
                }

            }

        }

        /// <summary>
        /// 显示代办会签
        /// </summary>
        /// <returns></returns>
        public ActionResult DesMutiSignRemind()
        {
            return View();
        }

        public string jsonRemind()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            //if (Request.Params["taskId"] != null)
            //{
            //    PageModel.SelectCondtion.Add("TaskID", Request.Params["taskId"].ToString());
            //}
            PageModel.SelectCondtion.Add("DeleterEmpId", 0);
            string column = ",(select ProjName from Project where ProjId=Id) as ProjName";
            column += ",(select ProjNumber from Project where ProjId=Id) as ProjNumber";
            column += ",(select BaseName from BaseData where BaseID = MutiSignPhaseId) as phaseName";
            column += ",isnull((select TaskName from DesTask where DesTask.Id=dms.TaskId),'') as TaskName";
            PageModel.SelectCondtion.Add("otherColumn", column);
            PageModel.SelectCondtion.Add("FlowStatusID", (int)DataModel.FlowStatus.审批结束);
            PageModel.SelectCondtion.Add("RecEmpId", userInfo.EmpID);
            PageModel.SelectCondtion.Add("RecStatus", "0");
            if (Request.Params["textContent"] != null)
            {
                PageModel.TextCondtion = Request.Params["textContent"].ToString();
            }

            var list = op.DesMutiSignkList(PageModel);


            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list,
            });
        }

        public string DesMutiSignAttach(int MuliSignID)
        {
            try
            {
                string AttachIds = Common.ModelConvert.ConvertToDefaultType<string>(Request.Params["AttachIds"]);
                if (String.IsNullOrWhiteSpace(AttachIds)) throw new Exception();

                var listSpecPlan = new DBSql.Sys.BaseAttachVer().GetDesTaskAttachVerByMuli(MuliSignID, AttachIds).ToList();
                return JavaScriptSerializerUtil.getJson(new
                {
                    total = listSpecPlan.Count,
                    rows = listSpecPlan
                });
            }
            catch (Exception ex)
            {
                return JavaScriptSerializerUtil.getJson(new
                {
                    total = 0,
                    rows = new List<DataModel.Models.BaseAttach>()
                });
            }
        }
    }
}

