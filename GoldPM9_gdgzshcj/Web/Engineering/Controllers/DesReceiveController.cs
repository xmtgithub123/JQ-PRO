using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Common.Data.Extenstions;

namespace Engineering.Controllers
{
    public class DesReceiveController : CoreController
    {
        private DBSql.Sys.BaseAttach ba = new DBSql.Sys.BaseAttach();
        private DBSql.Design.DesTask desTask = new DBSql.Design.DesTask();
        private DBSql.Sys.BaseData data = new DBSql.Sys.BaseData();
        private DBSql.Design.DesExch desExch = new DBSql.Design.DesExch();
        private DBSql.Design.DesExchRec desExchRec = new DBSql.Design.DesExchRec();
        private DBSql.PubFlow.Flow flow = new DBSql.PubFlow.Flow();
        private DBSql.Project.Project project = new DBSql.Project.Project();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        const string refTableDesReceive = "Plan";

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            return View();
        }
        #endregion

        public ActionResult ProjDesReceiveList(int taskGroupId)
        {
            //ViewBag.projID = projID;
            ViewBag.taskGroupId = taskGroupId;
            ViewBag.refTable = refTableDesReceive;
            return View("ProjDesReceive");
        }

        public string GetDesReceiveJson(int taskGroupId)
        {
            if (taskGroupId == 0)
            {
                throw new ArgumentException("taskGroupId不能为0");
            }
            try
            {
                var list = (from a in ba.DbContext.Set<DataModel.Models.BaseAttach>()
                            where a.AttachRefID == taskGroupId && a.AttachRefTable == refTableDesReceive
                            select new
                            {
                                AttachID = a.AttachID,
                                _parentId = a.AttachParentID,
                                AttachName = a.AttachName,
                                AttachExt = a.AttachExt,
                                AttachOrderPath = a.AttachOrderPath,
                                AttachPathIDs = a.AttachPathIDs,
                                AttachSize = a.AttachSize,
                                AttachDateUpload = a.AttachDateUpload,
                                AttachDateChange = a.AttachDateChange,
                                AttachEmpID = a.AttachEmpID,
                                AttachEmpName = a.AttachEmpName,
                                AttachVer = a.AttachVer,
                                AttachTag = a.AttachTag,
                                AttachGrade = a.AttachGrade,
                                state = "open"
                            }).ToList();
                string xx = JavaScriptSerializerUtil.getJson(new
                {
                    total = list.Count,
                    rows = list
                });

                return JavaScriptSerializerUtil.getJson(new
                {
                    total = list.Count,
                    rows = list
                });
            }
            catch (Exception)
            {
                return JavaScriptSerializerUtil.getJson(new
                {
                    total = 0,
                    rows = new List<DataModel.Models.BaseAttach>()
                });
            }

        }

        /// <summary>
        /// 代办收资列表信息
        /// </summary>
        /// <returns></returns>
        [Description("代办收资料列表信息")]
        public ActionResult ExchRecList()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ExchRecList")));
            if (PermissionList("ExchRecList").Contains("add"))
            {
                ViewBag.addPermission = 1;
            }
            ViewBag.TypeKind = Request.QueryString["Type"];//区分首页和列表信息
            ViewBag.EmpId = userInfo.EmpID;
            return View();
        }

        /// <summary>
        ///  收资Json列表信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string ExchRecJsonList()
        {
            if (!(userInfo.AgenTypeID == 0 || ((userInfo.AgenTypeID == -1 || userInfo.AgenTypeID == 2) && ("," + userInfo.AgenFlow + ",").IndexOf(",DesExch,") > -1)))
            {
                return JavaScriptSerializerUtil.getJson(new
                {
                    total = 0,
                    rows = new object[] { }
                });
            }
            Common.SqlPageInfo sqlPage = new Common.SqlPageInfo();
            Common.SqlPageInfo exchRecPage = new Common.SqlPageInfo();
            int PageNum = TypeHelper.parseInt(Request.Params["page"], 20);
            int Record = TypeHelper.parseInt(Request.Params["rows"], 1);

            if (!string.IsNullOrEmpty(Request.Params["KeyText"]))
            {
                string KeyText = Request.Params["KeyText"];//关键字(资料的标题、内容信息)
                sqlPage.TextCondtion = KeyText;
            }
            if (!string.IsNullOrEmpty(Request.Params["startTime"]) && TypeHelper.isDateTime(Request.Params["startTime"]))
            {
                DateTime startTime = TypeHelper.parseDateTime(Request.Params["startTime"]);
                exchRecPage.SelectCondtion.Add("DateLower", startTime);//接收时间下限
            }
            if (!string.IsNullOrEmpty(Request.Params["endTime"]) && TypeHelper.isDateTime(Request.Params["endTime"]))
            {
                DateTime endTime = TypeHelper.parseDateTime(Request.Params["endTime"]);
                exchRecPage.SelectCondtion.Add("DateUpper", endTime);//接收时间上限
            }
            Expression<Func<DataModel.Models.DesExch, bool>> desExchFunc = desExch.GetFunc(sqlPage);
            Expression<Func<DataModel.Models.DesExchRec, bool>> desExchRecFunc = desExchRec.GetFunc(exchRecPage);
            if (!string.IsNullOrEmpty(Request.Params["RecStatus"]))
            {
                int RecStatus = TypeHelper.parseInt(Request.Params["RecStatus"]);
                desExchRecFunc = desExchRecFunc.And(p => p.RecStatus == RecStatus);
            }
            List<int> SpecList = desExch.QualificationSpec((int)DataModel.NodeType.专业, userInfo);//当前人员负责的专业
            List<string> permission = PermissionList("ExchRecList");
            if (!permission.Contains("allview") && !permission.Contains("dep"))
            {
                if (SpecList.Count > 0)
                {
                    desExchRecFunc = desExchRecFunc.And(p => p.RecEmpId == userInfo.EmpID || (SpecList.Contains(p.RecSpecId) && p.RecEmpId == 0));//查看自己的和没有指定收资人的（专业负责人）
                }
                else
                {
                    desExchRecFunc = desExchRecFunc.And(p => p.RecEmpId == userInfo.EmpID);//只能查看自己的
                }

            }
            else
            {
                if (!permission.Contains("allview") && permission.Contains("dep"))
                {
                    if (SpecList.Count == 0)
                    {
                        desExchRecFunc = desExchRecFunc.And(p => p.RecEmpDepId == userInfo.EmpDepID);
                    }
                    else
                    {
                        desExchRecFunc = desExchRecFunc.And(p => p.RecEmpDepId == userInfo.EmpDepID || (SpecList.Contains(p.RecSpecId) && p.RecEmpDepId == 0));
                    }
                }
            }
            if (!string.IsNullOrEmpty(Request.Params["Type"]))
            {
                desExchRecFunc = desExchRecFunc.And(p => p.RecEmpId == userInfo.EmpID || p.RecEmpId == 0);//只能查看自己的
            }
            desExchFunc = desExchFunc.And(p => p.ExchIsInvalid == true);//有效
            var list = from exch in desExch.GetList(desExchFunc)
                       join gcItem in desExchRec.GetList(desExchRecFunc) on exch.Id equals gcItem.ExchId
                       join flows in flow.GetList(p => p.FlowRefTable == "DesExch") on exch.Id equals flows.FlowRefID// && p.FlowStatusID == (int)DataModel.FlowStatus.审批结束
                       let proj = project.Get(exch.ProjId)
                       orderby gcItem.ExchId descending, gcItem.Id descending
                       select new
                       {
                           gcItem.Id,
                           ExchId = exch.Id,
                           exch.ExchEmpId,
                           exch.ExchEmpName,
                           exch.ExchTitle,
                           exch.ExchSpecId,
                           exch.ExchSpecName,
                           RecSpecId = gcItem == null ? 0 : gcItem.RecSpecId,
                           RecSpecName = gcItem == null ? "" : gcItem.RecSpecName,
                           RecEmpId = gcItem == null ? 0 : gcItem.RecEmpId,
                           RecEmpName = gcItem == null ? "" : gcItem.RecEmpName,
                           ExcLastPutTime = gcItem == null ? new DateTime(1900, 1, 1) : gcItem.ExcLastPutTime,
                           ProjNumber = proj == null ? "" : proj.ProjNumber,
                           ProjName = proj == null ? "" : proj.ProjName,
                           RecTime = gcItem == null ? new DateTime(1900, 1, 1) : gcItem.RecTime,
                           IdentId = gcItem.RecTime > new DateTime(1900, 1, 1) ? 1 : 0,
                           gcItem.RecStatus,
                           gcItem.RecContent,
                           FlowStatusID = flows.FlowStatusID,
                           FlowStatusName = flows.FlowStatusName,
                           flowNodeId = desExch.FindApprovingNodeId(exch.Id),
                           State = gcItem.RecStatus == 0 ? "未处理" : (gcItem.RecStatus == 1 ? "回退" : "通过")

                       };
            return JavaScriptSerializerUtil.getJson(new
            {
                total = list.Count(),
                rows = (list.Skip(Record * (PageNum - 1)).Take(Record)).ToList()
            });
        }

        /// <summary>
        /// 接收资料  更新时间
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult RecExchange(int Id)
        {
            try
            {
                DataModel.Models.DesExchRec rec = desExchRec.Get(Id);
                if (rec.RecEmpId == 0)//未指定人
                {
                    desExchRec.AcceptDesExchRec(Id, userInfo);
                    doResult = DoResultHelper.doSuccess(1, "接收成功");
                }
                else
                {
                    if (rec.RecEmpId == userInfo.EmpID)
                    {
                        desExchRec.AcceptDesExchRec(Id, userInfo);
                        doResult = DoResultHelper.doSuccess(2, "接收成功");
                    }
                    else
                    {
                        doResult = DoResultHelper.doError("当前资料并未向你提供");
                    }
                }
            }

            catch (Exception ex)
            {
                doResult = DoResultHelper.doError(ex.Message);
            }
            return Json(doResult);

        }


        /// <summary>
        /// 收资信息详细页面
        /// </summary>
        /// <returns></returns>
        public ActionResult RecInfoApprove(int Id)
        {
            DataModel.Models.DesExchRec model = desExchRec.Get(Id);
            DataModel.Models.DesExch exch = desExch.Get(model.ExchId);
            DataModel.Models.Flow flowModel = flow.FirstOrDefault(p => p.FlowRefID == model.ExchId && p.FlowRefTable == "DesExch");
            if (flowModel != null)
            {
                ViewBag.FlowID = flowModel.Id;
                ViewBag.FlowStatusName = flowModel.FlowStatusName;
            }
            ViewBag.ExchTitle = exch.ExchTitle;
            ViewBag.ExchContent = exch.ExchContent;
            ViewBag.ExchSpecName = exch.ExchSpecName;
            ViewBag.ExchEmpName = exch.ExchEmpName;
            GetProjViewBagInfo(exch.taskGroupId, exch.ProjId);
            if (exch != null)
            {
                ViewBag.ExchId = exch.Id; ;
            }
            else
            {
                ViewBag.ExchId = 0;
            }
            return View("RecInfoApprove", model);
        }


        /// <summary>
        /// 同意处理
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public ActionResult AgreeRec(FormCollection form)
        {
            try
            {
                int Id = TypeHelper.parseInt(form["Id"]);
                DataModel.Models.DesExchRec model = desExchRec.Get(Id);
                model.RecContent = form["RecContent"];
                if (desExchRec.AgreeRec(model, userInfo) > 0)
                {
                    doResult = DoResultHelper.doSuccess(1, "操作成功");
                }

            }
            catch (Exception ex)
            {
                doResult = DoResultHelper.doSuccess(ex.Message);
            }
            return Json(doResult);
        }

        /// <summary>
        /// 退回
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public ActionResult RefuseRec(FormCollection form)
        {
            try
            {
                int Id = TypeHelper.parseInt(form["Id"]);
                DataModel.Models.DesExchRec model = desExchRec.Get(Id);
                model.RecContent = form["RecContent"];
                if (desExchRec.RefuseRecieve(model, userInfo) > 0)
                {
                    DataModel.Models.DesExch desExchModel = desExch.Get(model.ExchId);
                    desExch.SendExchBack(userInfo, desExchModel, desExchModel.ExchEmpId);//给回退人员发送消息
                    doResult = DoResultHelper.doSuccess(1, "操作成功");
                }

            }
            catch (Exception ex)
            {
                doResult = DoResultHelper.doSuccess(ex.Message);
            }
            return Json(doResult);
        }

        private void GetProjViewBagInfo(int taskGroupId, int ProjId)
        {

            string PhaseName = string.Empty;
            string ProjName = string.Empty;
            DataModel.Models.DesTask task = desTask.FirstOrDefault(p => p.TaskGroupId == taskGroupId && p.TaskType == 1);
            if (task != null)
            {
                DataModel.Models.BaseData model = data.Get(task.TaskPhaseId);
                if (model != null)
                {
                    PhaseName = model.BaseName;
                }
            }
            DataModel.Models.Project proj = project.Get(ProjId);
            if (proj != null)
            {
                ProjName = proj.ProjName;
            }
            ViewBag.PhaseName = PhaseName;
            ViewBag.ProjName = ProjName;
        }

    }

}
