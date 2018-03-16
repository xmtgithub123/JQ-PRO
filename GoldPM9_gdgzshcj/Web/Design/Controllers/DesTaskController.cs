using DataModel.Models;
using DBSql.Design.Dto;
using JQ.Util;
using JQ.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml;

namespace Design.Controllers
{
    [Description("DesTask")]
    public partial class DesTaskController : CoreController
    {
        private DBSql.Sys.BaseData baseDataDB = new DBSql.Sys.BaseData();
        private DBSql.Sys.BaseEmployee baseEmployeeDB = new DBSql.Sys.BaseEmployee();
        private DBSql.Project.Project projectDB = new DBSql.Project.Project();
        private DBSql.Design.DesTaskGroup desTaskGroupDB = new DBSql.Design.DesTaskGroup();
        private DBSql.Design.DesTask desTaskDB = new DBSql.Design.DesTask();
        private DBSql.Design.DesTaskGantt desTaskGanttDB = new DBSql.Design.DesTaskGantt();
        private DoResult doResult = DoResultHelper.doError("未知错误!");


        /**********************项目策划********************/

        /// <summary>
        /// 获取 项目策划列表 （分页）
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public string ProjectPlanListJson(FormCollection condition)
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();

            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "p.Id  desc, g.TaskGroupPath ASC ";
            }
            else
                PageModel.SelectOrder = "p." + PageModel.SelectOrder + ", g.TaskGroupPath ASC ";

            if (Request.Form["showFavourite"] != null)
            {
                //收藏工程的显示
                PageModel.SelectCondtion.Add("ShowFavourite", "1");
                PageModel.SelectCondtion.Add("CurrentEmpID", userInfo.EmpID);
            }

            if (!string.IsNullOrEmpty(GetRequestValue<string>("TaskGroupId")))
            {
                PageModel.SelectCondtion.Add("TaskGroupId", GetRequestValue<string>("TaskGroupId").Replace('_', ','));
            }

            if (!string.IsNullOrEmpty(GetRequestValue<string>("InTaskGroupId")))
            {
                PageModel.SelectCondtion.Add("InTaskGroupId", GetRequestValue<string>("InTaskGroupId").Replace('_', ','));
            }


            List<string> perlist = new List<string>();
            if (Request.Params["FormMenu"] != null)
            {
                //项目策划页面权限
                perlist = PermissionList(Request.Params["FormMenu"].ToString());
            }
            if (!(perlist.Contains("allview") || perlist.Contains("alledit")))
            {
                if (perlist.Contains("dep"))
                {
                    PageModel.SelectCondtion.Add("TaskGroupDept", userInfo.EmpDepID);
                }
                else
                {
                    PageModel.SelectCondtion.Add("TaskGroupEmpID", userInfo.EmpID);
                }
            }

            var list = desTaskDB.GetProjectPlanList(PageModel);
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }

        /// <summary>
        /// 获取 项目策划列表 （分页无权限）
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public string ProjectPlanListJsonAll(FormCollection condition)
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (Request.Params["CompanyType"] != null)
            {
                if (Request.Params["CompanyType"].ToString() != "")
                {
                    switch (Request.Params["CompanyType"].ToString())
                    {
                        case "FGS":
                            PageModel.SelectCondtion.Add("CompanyID", 0);
                            break;
                        case "GC":
                            PageModel.SelectCondtion.Add("CompanyID", 2);
                            break;
                        case "SJ":
                            PageModel.SelectCondtion.Add("CompanyID", 1);
                            break;
                        case "CJ":
                            PageModel.SelectCondtion.Add("CompanyID", 3);
                            break;
                    }
                }
            }

            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "p.Id  desc";
            }
            if (Request.Form["showFavourite"] != null)
            {
                //收藏工程的显示
                PageModel.SelectCondtion.Add("ShowFavourite", "1");
                PageModel.SelectCondtion.Add("CurrentEmpID", userInfo.EmpID);
            }

            if (!string.IsNullOrEmpty(GetRequestValue<string>("TaskGroupId")))
            {
                PageModel.SelectCondtion.Add("TaskGroupId", GetRequestValue<string>("TaskGroupId").Replace('_', ','));
            }

            if (!string.IsNullOrEmpty(GetRequestValue<string>("InTaskGroupId")))
            {
                PageModel.SelectCondtion.Add("InTaskGroupId", GetRequestValue<string>("InTaskGroupId").Replace('_', ','));
            }

            if (!string.IsNullOrEmpty(GetRequestValue<string>("TaskGroupStatus")))
            {
                PageModel.SelectCondtion.Add("TaskGroupStatus", GetRequestValue<string>("TaskGroupStatus"));
            }

            var list = desTaskDB.GetProjectPlanList(PageModel);
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }

        /// <summary>
        /// 更改 项目阶段 状态
        /// </summary>
        /// <returns></returns>
        public string ProjectPlanListChangeTaskGroupStatus(long[] Id, int status)
        {
            try
            {
                foreach (long i in Id)
                {
                    desTaskDB.ChangeTaskGroupStatus(i, (DBSql.Design.TaskGroupStatus)status, userInfo);
                }
                doResult = DoResultHelper.doSuccess(1, "操作成功");
            }
            catch (Exception)
            {
                doResult = DoResultHelper.doError("操作失败");
            }

            return JavaScriptSerializerUtil.getJson(doResult);
        }

        /// <summary>
        /// 获取 任务分组 数据
        /// </summary>
        /// <param name="projID"></param>
        /// <returns></returns>
        public string ProjectPlanInfoGetProjGroupJson(int projID)
        {
            var listTaskGroup = desTaskGroupDB.GetTaskGroupTree(projID).ToList();
            return JavaScriptSerializerUtil.getJson(new
            {
                total = listTaskGroup.Count,
                rows = listTaskGroup
            });
        }

        /// <summary>
        /// 获取 项目策划信息
        /// </summary>
        /// <param name="projID"></param>
        /// <returns></returns>
        public string ProjectPlanInfoGetProjPlanDataJson(int projID)
        {
            try
            {
                var taskGroupId = Common.ModelConvert.ConvertToDefaultType<int>(Request.Params["TaskGroupId"]);
                if (taskGroupId == 0) throw new Exception();

                DataTable dt = desTaskDB.GetProjPlanData(projID, taskGroupId);

                // 获取所以专业，并表面哪些专业已被设定
                var listProjPlan = dt.AsEnumerable().Select(b => new
                {
                    Id = b.Field<long>("Id"),
                    TaskName = b.Field<string>("TaskName"),
                    ProjId = b.Field<int>("ProjId"),
                    TaskPhaseId = b.Field<int>("TaskPhaseId"),
                    TaskGroupId = b.Field<long>("TaskGroupId"),
                    TaskSpecId = b.Field<int>("TaskSpecId"),
                    TaskSpecType = b.Field<int>("TaskSpecType"),
                    TaskStatus = b.Field<int>("TaskStatus"),
                    TaskEmpID = b.Field<int>("TaskEmpID"),
                    TaskEmpName = b.Field<string>("TaskEmpName"),
                    DatePlanStart = b.Field<DateTime>("DatePlanStart"),
                    DatePlanFinish = b.Field<DateTime>("DatePlanFinish"),
                    TaskNote = b.Field<string>("TaskNote"),
                    FlowId = b.Field<int>("FlowId"),
                    FlowName = b.Field<string>("FlowName"),
                    TaskType = b.Field<int>("TaskType"),
                    TaskOldEmpId = b.Field<int>("TaskOldEmpId"),
                    DatePlanDiff = b.Field<int>("DatePlanDiff"),
                    TaskPath = b.Field<string>("TaskPath"),
                    TaskFlowModel = b.Field<string>("TaskFlowModel") == "" ? "" : Common.ModelConvert.Xml2Json(b.Field<string>("TaskFlowModel")),
                    FlowXML = b.Field<string>("FlowXML")
                });
                return JavaScriptSerializerUtil.getJson(new
                {
                    total = listProjPlan.Count(),
                    rows = listProjPlan
                });
            }
            catch
            {
                return JavaScriptSerializerUtil.getJson(new
                {
                    total = 0,
                    rows = new List<DataModel.Models.DesTask>()
                }); ;
            }
        }

        /// <summary>
        /// 保存 项目策划 数据
        /// </summary>
        /// <param name="projID"></param>
        /// <returns></returns>
        public string ProjectPlanInfoSaveProjPlanData(int projID)
        {
            var taskGroupId = Common.ModelConvert.ConvertToDefaultType<long>(Request.Params["TaskGroupId"]);
            var jsonProjPlanData = Common.ModelConvert.ConvertToDefaultType<string>(Request.Params["ProjPlanData"]);
            var projPlanData = new JavaScriptSerializer().Deserialize<List<DesTaskInput>>(jsonProjPlanData);

            try
            {
                desTaskDB.SaveProjPlanData(projID, taskGroupId, projPlanData, userInfo);

                doResult = DoResultHelper.doSuccess(1, "操作成功");
            }
            catch (Exception ex)
            {
                doResult = DoResultHelper.doError(ex.Message);
            }

            return JavaScriptSerializerUtil.getJson(doResult);
        }

        /// <summary>
        /// 获取流程XML
        /// </summary>
        /// <param name="flowId"></param>
        /// <returns></returns>
        public string GetFlowXML(int flowId)
        {
            try
            {
                string flowXML = new DBSql.Design.DesTask().GetFlowNodeXML(flowId);

                flowXML = Common.ModelConvert.Xml2Json(flowXML);
                return JavaScriptSerializerUtil.getJson(new
                {
                    rows = flowXML
                });
            }
            catch (Exception ex)
            {
                return JavaScriptSerializerUtil.getJson(new
                {
                    rows = ""
                });
            }
        }

        /// <summary>
        /// 删除 项目策划中的 某一专业
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public string ProjectPlanInfoDelProjPlanData(int taskId)
        {
            try
            {
                desTaskDB.DelTaskNode(taskId, userInfo);
                doResult = DoResultHelper.doSuccess(1, "操作成功");
            }
            catch (Exception ex)
            {
                doResult = DoResultHelper.doError(ex.Message);
            }

            return JavaScriptSerializerUtil.getJson(doResult);
        }


        /**********************专业策划********************/

        /// <summary>
        /// 获取 专业策划列表 （分页）
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public string SpecPlanListJson(FormCollection condition)
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();

            //if (!PageModel.SelectOrder.isNotEmpty())
            //{
            //    PageModel.SelectOrder = "Id  ASC";
            //}

            //PageModel.SelectCondtion.Add("DeleterEmpId", "0");
            //PageModel.SelectCondtion.Add("FatherID", "0");

            //if (condition.CustID != 0)
            //{
            //    var objTemp = new DBSql.Bussiness.BussCustomer().Get(condition.CustID);
            //    PageModel.SelectCondtion.Add("CustName", objTemp.CustName);
            //}
            //if (condition.Filter != "")
            //{
            //    PageModel.TextCondtion = condition.Filter;
            //}
            var perlist = new List<string>();
            if (Request.QueryString["FormMenu"] != null)
            {
                perlist = PermissionList(GetRequestValue<string>("FormMenu"));
            }
            if (!(perlist.Contains("allview") || perlist.Contains("alledit")))
            {
                if (perlist.Contains("dep"))
                {
                    PageModel.SelectCondtion.Add("TaskDept", userInfo.EmpDepID);
                }
                else
                {
                    PageModel.SelectCondtion.Add("TaskEmpID", userInfo.EmpID);
                }
            }

            var list = desTaskDB.GetSpecPlanList(PageModel);
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }

        /// <summary>
        /// 获取 专业策划信息
        /// </summary>
        /// <param name="taskGroupId"></param>
        /// <returns></returns>
        public string SpecPlanInfoGetSpecPlanDataJson(long taskGroupId)
        {
            try
            {
                if (taskGroupId == 0) throw new Exception();
                var taskSpecId = Common.ModelConvert.ConvertToDefaultType<int>(Request.Params["taskSpecId"]);
                // 获取所有专业，并标明哪些专业已被设定
                var listSpecPlan = desTaskDB.GetSpecPlanData(taskGroupId, taskSpecId).ToList();
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
                    rows = new List<DataModel.Models.DesTask>()
                });
            }
        }

        /// <summary>
        /// 保存 专业策划 数据
        /// </summary>
        /// <param name="taskGroupId"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public string SpecPlanInfoSaveSpecPlanData(long taskGroupId)
        {
            try
            {
                if (taskGroupId == 0) throw new Exception();

                var jsonSpecPlanData = Common.ModelConvert.ConvertToDefaultType<string>(Request.Params["SpecPlanData"]);
                var specPlanData = new JavaScriptSerializer().Deserialize<List<DesTaskInput>>(jsonSpecPlanData);

                desTaskDB.SaveSpecPlanData(taskGroupId, specPlanData, userInfo);
                doResult = DoResultHelper.doSuccess(1, "操作成功");
            }
            catch (Exception ex)
            {
                doResult = DoResultHelper.doError(ex.Message);
            }

            return JavaScriptSerializerUtil.getJson(doResult);

        }

        /// <summary>
        /// 删除 专业策划中的 某一任务
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string SpecPlanInfoDelSpecPlanData(int taskId)
        {
            try
            {
                if (taskId == 0) throw new Exception();

                desTaskDB.DelTaskNode(taskId, userInfo);
                doResult = DoResultHelper.doSuccess(1, "操作成功");
            }
            catch (Exception ex)
            {
                doResult = DoResultHelper.doError(ex.Message);
            }

            return JavaScriptSerializerUtil.getJson(doResult);
        }

        /// <summary>
        /// 移交 专业策划中的 某一任务
        /// </summary>
        /// <returns></returns>
        public string SpecPlanInfoTaskTransfer()
        {
            var jsonTransferFlowNodeData = Common.ModelConvert.ConvertToDefaultType<string>(Request.Params["postData"]);
            var transferFlowNodeData = new JavaScriptSerializer().Deserialize<List<DesTaskSpecPlanTransferInput>>(jsonTransferFlowNodeData);

            try
            {
                foreach (var item in transferFlowNodeData)
                {
                    var transferData = new List<DesTaskRemindInput>()
                    {
                        new DesTaskRemindInput() { ItemType = item.ItemType, TaskId = item.TaskId,ID=item.ID }
                    };
                    desTaskDB.BatchDesTaskTransfer(item.fromEmpId, item.toEmpId, transferData, userInfo);
                }

                doResult = DoResultHelper.doSuccess(1, "操作成功");
            }
            catch (Exception ex)
            {
                doResult = DoResultHelper.doError(ex.Message);
            }

            return JavaScriptSerializerUtil.getJson(doResult);
        }

        /// <summary>
        /// 获取 批量专业策划 数据
        /// </summary>
        /// <returns></returns>
        public string SpecPlanBatchGetSpecPlanDataJson()
        {
            var taskIDSet = Request.Form["taskIDSet"];
            if (string.IsNullOrEmpty(taskIDSet))
            {
                return "[]";
            }
            using (var list = desTaskDB.GetSpecPlanData(userInfo.EmpID, taskIDSet))
            {
                return JavaScriptSerializerUtil.getJson(new
                {
                    total = list.Rows.Count,
                    rows = list
                });
            }
        }

        /// <summary>
        /// 保存 批量专业策划 数据
        /// </summary>
        /// <param name="taskGroupId"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public string SpecPlanBatchSaveSpecPlanData()
        {
            try
            {
                var shenHeEmpID = Request.Form["shenHe"] ?? "0";
                var piZhunEmpID = Request.Form["piZhun"] ?? "0";
                var datas = new JavaScriptSerializer().Deserialize<List<DesTaskInput>>(Request.Form["data"] ?? "[]");
                var empDB = new DBSql.Sys.BaseEmployee();
                foreach (var data in datas)
                {
                    //获取出专业策划任务
                    var specData = desTaskDB.FirstOrDefault(m => m.Id == data.Id);
                    if (specData == null)
                    {
                        continue;
                    }
                    var temp = new List<DesTaskInput>();
                    temp.Add(JQ.Util.TypeFactoryHelper.convertToDerivedType<DesTask, DesTaskInput>(specData));
                    //构造出当前的设计任务
                    data.Id = -1;
                    data.TaskSpecId = specData.TaskSpecId;
                    data.ProjId = specData.ProjId;
                    data.TaskGroupId = specData.TaskGroupId;
                    data.FlowId = specData.FlowId;
                    data.TaskParentId = specData.Id;
                    data.OldFlowId = data.FlowId;
                    data.TaskLevel = 1;
                    data.TaskEmpID = data.SheJi;
                    data.TaskEmpName = empDB.GetEmpName(data.TaskEmpID.ToString());
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(specData.TaskFlowModel);
                    //设置设计人员
                    var item = (XmlElement)xmlDocument.SelectSingleNode("root/item[@FlowNodeTypeID=\"" + DataModel.NodeType.设计.ToString("D") + "\"]");
                    if (item != null)
                    {
                        item.SetAttribute("FlowNodeEmpID", data.TaskEmpID.ToString());
                        item.SetAttribute("FlowNodeEmpName", data.TaskEmpName);
                        item.SetAttribute("FlowNodeStatus", "2");
                    }
                    //设置校核人员
                    item = (XmlElement)xmlDocument.SelectSingleNode("root/item[@FlowNodeTypeID=\"" + DataModel.NodeType.校对.ToString("D") + "\"]");
                    if (item != null)
                    {
                        item.SetAttribute("FlowNodeEmpID", data.JiaoHe.ToString());
                        item.SetAttribute("FlowNodeEmpName", empDB.GetEmpName(data.JiaoHe.ToString()));
                        item.SetAttribute("FlowNodeStatus", "1");
                    }
                    //甚至审核人员
                    item = (XmlElement)xmlDocument.SelectSingleNode("root/item[@FlowNodeTypeID=\"" + DataModel.NodeType.审核.ToString("D") + "\"]");
                    if (item != null)
                    {
                        item.SetAttribute("FlowNodeEmpID", shenHeEmpID);
                        item.SetAttribute("FlowNodeEmpName", empDB.GetEmpName(shenHeEmpID));
                        item.SetAttribute("FlowNodeStatus", "1");
                    }
                    //设置批准人员
                    item = (XmlElement)xmlDocument.SelectSingleNode("root/item[@FlowNodeTypeID=\"" + DataModel.NodeType.批准.ToString("D") + "\"]");
                    if (item != null)
                    {
                        item.SetAttribute("FlowNodeEmpID", piZhunEmpID);
                        item.SetAttribute("FlowNodeEmpName", empDB.GetEmpName(piZhunEmpID));
                        item.SetAttribute("FlowNodeStatus", "1");
                    }
                    data.TaskFlowModel = xmlDocument.OuterXml;
                    temp.Add(data);
                    desTaskDB.SaveSpecPlanData(data.TaskGroupId, temp, userInfo);
                }
            }
            catch (Exception ex)
            {
                doResult = DoResultHelper.doError(ex.Message);
            }

            return JavaScriptSerializerUtil.getJson(doResult);

        }

        /**********************任务处理********************/

        /// <summary>
        /// 获取任务信息
        /// </summary>
        /// <param name="TaskId"></param>
        /// <returns></returns>
        public string GetTaskInfo(long TaskId)
        {
            DBSql.Design.Dto.DesTaskInput task = desTaskDB.GetDesTaskInfo(TaskId);
            return JavaScriptSerializerUtil.getJson(task);
        }

        /// <summary>
        /// 获取任务分组信息
        /// </summary>
        /// <param name="TaskGroupId"></param>
        /// <returns></returns>
        public string GetTaskGroupInfo(long TaskGroupId)
        {
            DBSql.Design.Dto.DesTaskGroupInput taskGroup = desTaskGroupDB.GetTaskGroupInfo(TaskGroupId);
            //Project project = projectDB.Get(taskGroup.ProjId);
            return JavaScriptSerializerUtil.getJson(new
            {
                //Project = project,
                TaskGroup = taskGroup
            });
        }

        /// <summary>
        /// 获取 设计任务附件 列表
        /// </summary>
        /// <param name="TaskId"></param>
        /// <returns></returns>
        public string TaskInfoGetDesTaskAttachDataJson(long TaskId)
        {
            try
            {
                if (TaskId == 0) throw new Exception();

                var listSpecPlan = desTaskDB.GetDesTaskAttachData(TaskId).ToList();
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
                    rows = new List<DataModel.Models.DesTask>()
                });
            }
        }

        /// <summary>
        /// 获取 设计任务待校审附件 列表
        /// </summary>
        /// <param name="TaskId">任务ID</param>
        /// <returns></returns>
        public string TaskInfoGetDesTaskAttachSelDataJson(long TaskId)
        {
            try
            {
                string AttachIds = Common.ModelConvert.ConvertToDefaultType<string>(Request.Params["AttachIds"]);
                if (String.IsNullOrWhiteSpace(AttachIds)) throw new Exception();

                var listSpecPlan = desTaskDB.GetDesTaskAttachSelData(TaskId, AttachIds).ToList();
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
                    rows = new List<DataModel.Models.DesTask>()
                });
            }
        }

        /// <summary>
        /// 获取 前置任务 列表
        /// </summary>
        /// <param name="TaskId">任务ID</param>
        /// <returns></returns>
        public string TaskInfoGetPreTaskListJson(long TaskId)
        {
            try
            {
                var listPreTask = desTaskDB.GetPreTaskListJson(TaskId);
                return JavaScriptSerializerUtil.getJson(new
                {
                    total = listPreTask.Rows.Count,
                    rows = listPreTask
                });
            }
            catch (Exception ex)
            {
                return JavaScriptSerializerUtil.getJson(new
                {
                    total = 0,
                    rows = new List<DataModel.Models.DesTask>()
                });
            }
        }

        /// <summary>
        /// 提交 任务所选附件的 校审流程
        /// </summary>
        /// <param name="TaskId"></param>
        /// <returns></returns>
        public string TaskInfoSavePostApprove(long TaskId)
        {
            //int FlowId = Common.ModelConvert.ConvertToDefaultType<int>(Request.Params["FlowId"]);
            string AttachIds = Common.ModelConvert.ConvertToDefaultType<string>(Request.Params["AttachIds"]);
            try
            {
                //更新人员状态
                var jsonSpecPlanData = Common.ModelConvert.ConvertToDefaultType<string>(Request.Params["SpecPlanData"]);
                //var specPlanData = new JavaScriptSerializer().Deserialize<List<DesTaskInput>>(jsonSpecPlanData);
                //DataModel.Models.DesTask dmTask = desTaskDB.GetDesTaskInfo(TaskId);
                //desTaskDB.SaveSpecPlanData(dmTask.TaskGroupId, specPlanData, userInfo);
                DataModel.Models.DesTask dmTask = desTaskDB.DbContext.Set<DataModel.Models.DesTask>().FirstOrDefault(p => p.Id == TaskId);
                //dmTask.TaskFlowModel = Common.ModelConvert.JsonList2XmlA<DesFlowNodeXmlInput>(jsonSpecPlanData);
                desTaskDB.DbContext.Entry<DataModel.Models.DesTask>(dmTask).State = System.Data.Entity.EntityState.Modified;
                desTaskDB.UnitOfWork.SaveChanges();
                //提交下一步
                desTaskDB.SavePostApprove(TaskId, AttachIds, userInfo);
                doResult = DoResultHelper.doSuccess(1, "操作成功");
            }
            catch (Exception ex)
            {
                doResult = DoResultHelper.doError(ex.Message);
            }
            return JavaScriptSerializerUtil.getJson(doResult);
        }

        /// <summary>
        /// 获取 任务中只显示正要自己审批的附件 列表
        /// </summary>
        /// <param name="TaskId">任务ID</param>
        /// <returns></returns>
        public string TaskInfoApproveGetDesTaskApproveNowAttachDataJson(long TaskId)
        {
            try
            {
                if (TaskId < 1) throw new Exception("传递参数错误");

                int SessionEmpId = Common.ModelConvert.ConvertToDefaultType<int>(Request.Params["ViewEmpID"]); // 固定以某人的身份查看他的校审内容
                if (SessionEmpId == 0) SessionEmpId = userInfo.EmpID;

                var list = desTaskDB.GetDesTaskApproveNowAttachData(TaskId, SessionEmpId).ToList();
                return JavaScriptSerializerUtil.getJson(new
                {
                    total = list.Count,
                    rows = list
                });
            }
            catch (Exception ex)
            {
                return JavaScriptSerializerUtil.getJson(new
                {
                    total = 0,
                    rows = new List<DataModel.Models.DesTask>()
                });
            }
        }

        /// <summary>
        /// 获取 任务中只显示所有要我审批的附件 列表
        /// </summary>
        /// <param name="TaskId">任务ID</param>
        /// <returns></returns>
        public string TaskInfoApproveGetDesTaskApproveAllAttachDataJson(long TaskId)
        {
            try
            {
                if (TaskId < 1) throw new Exception("传递参数错误");

                int SessionEmpId = Common.ModelConvert.ConvertToDefaultType<int>(Request.Params["ViewEmpID"]); // 固定以某人的身份查看他的校审内容
                if (SessionEmpId == 0) SessionEmpId = userInfo.EmpID;

                var list = desTaskDB.GetDesTaskApproveAllAttachData(TaskId, SessionEmpId).ToList();
                return JavaScriptSerializerUtil.getJson(new
                {
                    total = list.Count,
                    rows = list
                });
            }
            catch (Exception ex)
            {
                return JavaScriptSerializerUtil.getJson(new
                {
                    total = 0,
                    rows = new List<DataModel.Models.DesTask>()
                });
            }
        }

        /// <summary>
        /// 提交 校审人员所选附件的 通过流程
        /// </summary>
        /// <param name="TaskId"></param>
        /// <returns></returns>
        public string TaskInfoApproveSavePostPass(long TaskId)
        {
            string AttachIds = Common.ModelConvert.ConvertToDefaultType<string>(Request.Params["AttachIds"]);
            try
            {
                desTaskDB.SavePostPass(TaskId, AttachIds, userInfo);
                doResult = DoResultHelper.doSuccess(1, "操作成功");
            }
            catch (Exception ex)
            {
                doResult = DoResultHelper.doError(ex.Message);
            }
            return JavaScriptSerializerUtil.getJson(doResult);
        }

        /// <summary>
        /// 提交 校审人员所选附件的 回退流程
        /// </summary>
        /// <param name="TaskId"></param>
        /// <returns></returns>
        public string TaskInfoApproveSavePostBack(long TaskId)
        {
            string AttachIds = Common.ModelConvert.ConvertToDefaultType<string>(Request.Params["AttachIds"]);
            try
            {
                desTaskDB.SavePostBack(TaskId, AttachIds, userInfo);
                doResult = DoResultHelper.doSuccess(1, "操作成功");
            }
            catch (Exception ex)
            {
                doResult = DoResultHelper.doError(ex.Message);
            }
            return JavaScriptSerializerUtil.getJson(doResult);
        }

        /// <summary>
        /// 变更 任务 进行中
        /// </summary>
        /// <param name="TaskId"></param>
        /// <returns></returns>
        public string TaskInfoChangeTaskStart(long TaskId)
        {
            try
            {
                desTaskDB.ChangeTaskStatus(TaskId, DBSql.Design.TaskStatus.进行中, userInfo);
                doResult = DoResultHelper.doSuccess(1, "操作成功");
            }
            catch (Exception ex)
            {
                doResult = DoResultHelper.doError(ex.Message);
            }
            return JavaScriptSerializerUtil.getJson(doResult);
        }

        /// <summary>
        /// 变更 任务 已完成
        /// </summary>
        /// <param name="TaskId"></param>
        /// <returns></returns>
        public string TaskInfoChangeTaskFinish(long TaskId)
        {
            try
            {
                desTaskDB.ChangeTaskStatus(TaskId, DBSql.Design.TaskStatus.已完成, userInfo);

                doResult = DoResultHelper.doSuccess(1, "操作成功");
            }
            catch (Exception ex)
            {
                doResult = DoResultHelper.doError(ex.Message);
            }
            return JavaScriptSerializerUtil.getJson(doResult);
        }

        /// <summary>
        /// 个人任务移交
        /// </summary>
        /// <param name="fromEmpId"></param>
        /// <param name="toEmpId"></param>
        public string TaskWorkBatchTransfer(int fromEmpId, int toEmpId)
        {
            var jsonTransferData = Common.ModelConvert.ConvertToDefaultType<string>(Request.Params["TransferData"]);
            var transferData = new JavaScriptSerializer().Deserialize<List<DesTaskRemindInput>>(jsonTransferData);

            try
            {
                desTaskDB.BatchDesTaskTransfer(fromEmpId, toEmpId, transferData, userInfo);

                doResult = DoResultHelper.doSuccess(1, "操作成功");
            }
            catch (Exception ex)
            {
                doResult = DoResultHelper.doError(ex.Message);
            }

            return JavaScriptSerializerUtil.getJson(doResult);
        }

        /// <summary>
        /// 重新签名
        /// </summary>
        /// <returns></returns>
        public JsonResult TaskInfoReSign(long TaskId)
        {
            try
            {
                desTaskDB.ReSign(TaskId, userInfo);
                return Json(new { Result = true });
            }
            catch
            {
                return Json(new { Result = false });
            }
        }


        /**********************任务提醒********************/

        /// <summary>
        /// 获取 个人待办设计任务 列表（不分页）
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public string TaskDesignRemindListJson(FormCollection condition)
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();

            PageModel.ToPageData = true; // 不分页
            PageModel.SelectCondtion.Add("SessionEmpId", userInfo.EmpID.ToString());

            var list = desTaskDB.GetTaskRemindDesignList(PageModel);
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }

        /// <summary>
        /// 获取 个人待办任务 列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public string TaskRemindListJson(FormCollection condition)
        {
            return TaskWorkListJson(condition);
        }

        /// <summary>
        /// 获取 个人待办任务 数量
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public string TaskRemindTodoAmount(FormCollection condition)
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();

            string itemStatus;
            switch (condition["itemStatus"])
            {
                case "waiting":
                    itemStatus = String.Format("{0}", DBSql.Design.FlowNodeStatus.已安排.ToString("D"));
                    break;
                case "now":
                    itemStatus = String.Format("{0}", DBSql.Design.FlowNodeStatus.进行中.ToString("D"));
                    break;
                case "remind":
                    itemStatus = String.Format("{0},{1}", DBSql.Design.FlowNodeStatus.已安排.ToString("D"), DBSql.Design.FlowNodeStatus.进行中.ToString("D"));
                    break;
                case "finished":
                    itemStatus = String.Format("{0}", DBSql.Design.FlowNodeStatus.已完成.ToString("D"));
                    break;
                case "back":
                    itemStatus = String.Format("{0}", DBSql.Design.FlowNodeStatus.已回退.ToString("D"));
                    break;
                default:
                    itemStatus = String.Format("{0}", DBSql.Design.FlowNodeStatus.进行中.ToString("D"));
                    break;
            }

            var amount = desTaskDB.GetTaskRemindTodoAmount(
                userInfo.EmpID,
                itemStatus,
                new DesTaskRemindPermission(userInfo)
            );
            return JavaScriptSerializerUtil.getJson(new
            {
                Result = true,
                Amount = amount,

            });
        }

        /// <summary>
        /// 个人任务查询
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public string TaskWorkListJson(FormCollection condition)
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();

            var empId = Common.ModelConvert.ConvertToDefaultType<int>(condition["EmpID"]);
            if (empId == 0) empId = userInfo.EmpID;

            var itemType = Common.ModelConvert.ConvertToDefaultType<int>(condition["ItemType"]);

            string itemStatus;
            switch (condition["itemStatus"].ToLower())
            {
                case "waiting":
                    itemStatus = String.Format("{0}", DBSql.Design.FlowNodeStatus.已安排.ToString("D"));
                    break;
                case "now":
                    itemStatus = String.Format("{0}", DBSql.Design.FlowNodeStatus.进行中.ToString("D"));
                    break;
                case "remind":
                    itemStatus = String.Format("{0},{1}", DBSql.Design.FlowNodeStatus.已安排.ToString("D"), DBSql.Design.FlowNodeStatus.进行中.ToString("D"));
                    break;
                case "finished":
                    itemStatus = String.Format("{0}", DBSql.Design.FlowNodeStatus.已完成.ToString("D"));
                    break;
                case "back":
                    itemStatus = String.Format("{0}", DBSql.Design.FlowNodeStatus.已回退.ToString("D"));
                    break;
                default:
                    itemStatus = String.Format("{0}", DBSql.Design.FlowNodeStatus.进行中.ToString("D"));
                    break;
            }

            string showAll = condition["showAll"];

            PageModel.SelectCondtion.Add("SessionEmpId", empId.ToString());
            PageModel.SelectCondtion.Add("ItemStatus", itemStatus); // 任务状态：0 未启动 1 已轮到 2 进行中 3 已完成 4 停止
            PageModel.SelectCondtion.Add("DesTaskRemindPermission", new DesTaskRemindPermission(userInfo)); // 根据权限过滤列表
            PageModel.SelectCondtion.Add("ShowAll", showAll); // 显示所有任务 或 只显示待办任务
            PageModel.SelectCondtion.Add("ItemType", itemType.ToString()); // 按项目类别筛选

            var list = desTaskDB.GetTaskWorkList(PageModel);
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }
        /// <summary>
        /// 个人任务查询
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public string TaskWorkListJson1(FormCollection condition)
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();

            var empId = Common.ModelConvert.ConvertToDefaultType<int>(condition["EmpID"]);
            if (empId == 0) empId = userInfo.EmpID;

            var itemType = Common.ModelConvert.ConvertToDefaultType<int>(condition["ItemType"]);

            string showAll = condition["showAll"];

            PageModel.SelectCondtion.Add("SessionEmpId", empId.ToString());
            PageModel.SelectCondtion.Add("ItemStatus", 3); // 任务状态：0 未启动 1 已轮到 2 进行中 3 已完成 4 停止
            PageModel.SelectCondtion.Add("DesTaskRemindPermission", new DesTaskRemindPermission(userInfo)); // 根据权限过滤列表
            PageModel.SelectCondtion.Add("ShowAll", 1);// showAll); // 显示所有任务 或 只显示待办任务
            //PageModel.SelectCondtion.Add("ItemType", itemType.ToString()); // 按项目类别筛选
            PageModel.SelectCondtion.Add("ProjId", Request["projId"]); //根据taskGroupId过滤


            var list = desTaskDB.GetTaskWorkList1(PageModel).AsEnumerable().ToList();

            var result = from p in list
                         let i = p.Field<long>("TaskGroupId")
                         from o in new DBSql.Design.DesTaskGroup().GetList(u => u.Id == i).ToList()
                         from y in desTaskDB.GetNextNode(p.Field<long>("TaskId")).AsEnumerable().ToList()
                         select new
                         {
                             Id = p.Field<long>("TaskId"),
                             ItemName = p.Field<string>("ItemName"),
                             o.ProjName,
                             EmpId = y.Field<int>("EmpId"),
                             EmpName = y.Field<string>("EmpName")
                         };

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = result //list
            });
        }

        /**********************任务进度********************/

        /// <summary>
        /// 获取 项目进度节点 数据
        /// </summary>
        /// <param name="projID"></param>
        /// <param name="taskGroupId"></param>
        /// <returns></returns>
        public string ProjectProgressInfoOrgGetListJson(int projID, int taskGroupId)
        {
            var list = desTaskDB.GetProjectProgress(projID, taskGroupId);
            return JavaScriptSerializerUtil.getJson(new
            {
                total = list.Rows.Count,
                rows = list
            });
        }

        /// <summary>
        /// 获取 项目策划进度 数据
        /// </summary>
        /// <param name="projID"></param>
        /// <param name="taskGroupId"></param>
        /// <returns></returns>
        public string ProjectProgressInfoPlanGetListJson(int projID, int taskGroupId)
        {
            var list = desTaskDB.GetSpecPlanProgress(projID, taskGroupId).ToList();
            return JavaScriptSerializerUtil.getJson(new
            {
                total = list.Count,
                rows = list
            });
        }

        /// <summary>
        /// 获取 任务文件进度 数据
        /// </summary>
        /// <param name="projId"></param>
        /// <param name="taskGroupId"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public string ProjectProgressInfoAttachGetListJson(int projId, long taskGroupId, long taskId)
        {
            try
            {
                var listSpecPlan = desTaskDB.GetTaskAttachProgress(projId, taskGroupId, taskId).ToList();
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
                    rows = new List<DataModel.Models.DesTask>()
                });
            }
        }

        /// <summary>
        /// 获取 任务开展情况统计列表
        /// </summary>
        /// <returns></returns>
        public string TaskProgressListJson(FormCollection condition)
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();

            //if (!PageModel.SelectOrder.isNotEmpty())
            //{
            //    PageModel.SelectOrder = "Id  ASC";
            //}

            //PageModel.SelectCondtion.Add("DeleterEmpId", "0");
            //PageModel.SelectCondtion.Add("FatherID", "0");

            //if (condition.CustID != 0)
            //{
            //    var objTemp = new DBSql.Bussiness.BussCustomer().Get(condition.CustID);
            //    PageModel.SelectCondtion.Add("CustName", objTemp.CustName);
            //}
            //if (condition.Filter != "")
            //{
            //    PageModel.TextCondtion = condition.Filter;
            //}
            var perlist = new List<string>();
            if (Request.QueryString["FormMenu"] != null)
            {
                perlist = PermissionList(GetRequestValue<string>("FormMenu"));
            }
            if (!(perlist.Contains("allview") || perlist.Contains("alledit")))
            {
                if (perlist.Contains("dep"))
                {
                    PageModel.SelectCondtion.Add("TaskDept", userInfo.EmpDepID);
                }
                else
                {
                    PageModel.SelectCondtion.Add("TaskEmpID", userInfo.EmpID);
                }
            }

            var list = desTaskDB.GetTaskProgressList(PageModel);
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }

        /************************其他*********************/

        /// <summary>
        /// 获取任务名称信息
        /// </summary>
        /// <returns></returns>
        public string GetDesTaskNameByDesTask()
        {
            try
            {
                Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
                if (!PageModel.SelectOrder.isNotEmpty())
                {
                    PageModel.SelectOrder = "Id";
                }
                var list = desTaskDB.GetPagedList(PageModel);

                string desTaskType = Request.Params["DesTaskType"] == null ? "" : Request.Params["DesTaskType"].ToString();

                var target = (from item in list
                              join p in desTaskDB.DbContext.Set<DataModel.Models.Project>()
                              on item.ProjId equals p.Id
                              join b in desTaskDB.DbContext.Set<DataModel.Models.BaseData>()
                              on item.TaskPhaseId equals b.BaseID into temp1
                              from t1 in temp1.DefaultIfEmpty()
                              select new
                              {
                                  item.Id,
                                  projID = p.Id,
                                  p.ProjNumber,
                                  p.ProjName,
                                  item.TaskPhaseId,
                                  TaskPhaseName = t1.BaseName,
                                  p.ProjEmpId,
                                  p.ProjEmpName,
                                  item.DatePlanStart,
                                  item.DatePlanFinish,
                                  item.TaskSpecId,
                                  item.TaskGroupId,
                                  item.TaskName,
                              }).ToList();

                return JavaScriptSerializerUtil.getJson(new
                {
                    total = PageModel.PageTotleRowCount,
                    rows = target
                });
            }
            catch (Exception)
            {
                return JavaScriptSerializerUtil.getJson(new
                {
                    total = 0,
                    rows = new List<DataModel.Models.DesTask>()
                });
            }
        }


        public string svproject()
        {
            try
            {
                string strjson = Request.Form["data"] ?? "";
                int projID = TypeHelper.parseInt(Request.Form["projID"]);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                Dictionary<string, object> json = (Dictionary<string, object>)serializer.DeserializeObject(strjson);
                object[] info = (object[])json["tasks"];

                int lastProjId = 0;
                int lastTaskId = 0;
                int inCrement = 0;
                List<DataModel.infoTask> list = new List<DataModel.infoTask>();
                for (int i = 0; i < info.Length; i++)
                {
                    DataModel.infoTask _infoTask = new DataModel.infoTask();
                    Dictionary<string, object> val = (Dictionary<string, object>)info[i];

                    if (!val.ContainsKey("projId"))
                    {
                        _infoTask.projid = lastProjId;
                    }
                    else
                    {
                        _infoTask.projid = TypeHelper.parseInt(val["projId"]);
                        lastProjId = _infoTask.projid;
                    }

                    if (!val.ContainsKey("taskId"))
                    {
                        _infoTask.taskId = lastTaskId;
                    }
                    else
                    {
                        _infoTask.taskId = TypeHelper.parseInt(val["taskId"]);
                        lastTaskId = _infoTask.taskId;
                    }



                    foreach (KeyValuePair<string, object> str in val)
                    {
                        #region
                        if (str.Key.Equals("id"))
                        {
                            int _id = TypeHelper.parseInt(str.Value);
                            if (_id == 0)
                            {
                                _infoTask.id = inCrement--;
                            }
                            else
                            {
                                _infoTask.id = TypeHelper.parseInt(str.Value);
                            }
                        }
                        if (str.Key.Equals("name"))
                        {
                            _infoTask.name = TypeHelper.parseString(str.Value);
                        }
                        if (str.Key.Equals("level"))
                        {
                            _infoTask.level = TypeHelper.parseInt(str.Value);
                        }
                        if (str.Key.Equals("progress"))
                        {
                            _infoTask.progress = TypeHelper.parseInt(str.Value);
                        }
                        if (str.Key.Equals("duration"))
                        {
                            _infoTask.duration = TypeHelper.parseInt(str.Value);
                        }
                        if (str.Key.Equals("start"))
                        {
                            _infoTask.datePlanStart = TypeHelper.parseTimeFromTimeStamp(str.Value);
                        }
                        if (str.Key.Equals("end"))
                        {
                            _infoTask.datePlanFinish = TypeHelper.parseTimeFromTimeStamp(str.Value);
                        }
                        if (str.Key.Equals("keyPointType"))
                        {
                            _infoTask.keyPointType = TypeHelper.parseInt(str.Value);
                        }
                        if (str.Key.Equals("startIsMilestone"))
                        {
                            _infoTask.startIsMilestone = str.Value.ToString() == "true" ? true : false;
                        }
                        if (str.Key.Equals("endIsMilestone"))
                        {
                            _infoTask.endIsMilestone = str.Value.ToString() == "true" ? true : false;
                        }
                        if (str.Key.Equals("description"))
                        {
                            _infoTask.description = TypeHelper.parseString(str.Value);
                        }
                        #endregion
                    }

                    list.Add(_infoTask);
                }
                desTaskGanttDB.CreateOrUpdate(list, projID);

                return "ok";
            }
            catch (Exception ex)
            {
                return "error";
            }


        }
        public string getproject(int getprojectID = 0)
        {
            string projID = Request.Form["projID"];
            if (getprojectID > 0)
            {
                projID = getprojectID.ToString();
            }

            var sbJson = new StringBuilder("{ \"tasks\":[");
            using (var dt = desTaskGanttDB.GetTableList(projID))
            {
                var rows = dt.Select("ParentID=0").OrderBy(dr => dr.Field<int>("Order")).ToArray();
                for (var m = 0; m < rows.Length; m++)
                {
                    if (m != 0)
                    {
                        sbJson.Append(",");
                    }
                    sbJson.Append("{\"id\":" + rows[m].Field<int>("ID").ToString());
                    sbJson.Append(",\"projId\":\"" + rows[m].Field<int>("ProjId").ToString() + "\"");
                    sbJson.Append(",\"taskId\":\"" + rows[m].Field<long>("TaskId").ToString() + "\"");
                    sbJson.Append(",\"parentID\":\"" + rows[m].Field<int>("ParentID").ToString() + "\"");
                    sbJson.Append(",\"name\":\"" + rows[m].Field<string>("Name").ToString() + "\"");
                    sbJson.Append(",\"depends\":\"" + rows[m].Field<string>("Depends").ToString() + "\"");
                    sbJson.Append(",\"keyPointType\":\"" + rows[m].Field<int>("KeyPointType").ToString() + "\"");
                    sbJson.Append(",\"level\":" + rows[m].Field<int>("Level").ToString());
                    sbJson.Append(",\"description\":\"" + rows[m].Field<string>("Description").ToString() + "\"");
                    sbJson.Append(",\"progress\":\"" + rows[m].Field<decimal>("Progress").ToString() + "\"");
                    sbJson.Append(",\"status\":\"STATUS_ACTIVE\"");
                    sbJson.Append(",\"canWrite\":true");
                    sbJson.Append(",\"ManageEmpName\":\"" + baseEmployeeDB.Get(rows[m].Field<int>("ManageEmpId")).EmpName + "\"");
                    sbJson.Append(",\"start\":" + TypeHelper.ConvertDateTimeToInt(rows[m].Field<DateTime>("DatePlanStart")));
                    sbJson.Append(",\"duration\":\"" + rows[m].Field<int>("Duration").ToString() + "\"");
                    //sbJson.Append(",\"end\":" + TypeHelper.ConvertDateTimeToInt(rows[m].Field<DateTime>("DatePlanFinish")));
                    sbJson.Append(",\"startIsMilestone\":false");
                    sbJson.Append(",\"endIsMilestone\":false");
                    sbJson.Append(",\"collapsed\":false");
                    sbJson.Append(",\"assigs\":[]");
                    sbJson.Append(",\"hasChild\":false");
                    sbJson.Append("}");

                    long t1 = TypeHelper.ConvertDateTimeToInt(rows[m].Field<DateTime>("DatePlanFinish"));
                    DateTime t2 = TypeHelper.ConvertToDateTime(t1);

                    var tempRows = dt.Select("ParentID=" + rows[m].Field<int>("ID").ToString()).OrderBy(dr => dr.Field<int>("Order")).ToArray();
                    if (tempRows.Length > 0)
                    {
                        RecuriseProcess(dt, tempRows, sbJson);
                    }
                }
            }
            sbJson.Append(" ],\"selectedRow\":0,\"canWrite\":true,\"canWriteOnParent\":true}");
            string info = "{\"Result\":\"ok\",\"project\":" + sbJson.ToString() + "}";
            return info;
        }

        private void RecuriseProcess(DataTable source, DataRow[] rows, StringBuilder sbJson)
        {
            if (rows.Length == 0)
            {
                return;
            }
            for (var i = 0; i < rows.Length; i++)
            {
                sbJson.Append(",{\"id\":" + rows[i].Field<int>("ID").ToString());
                sbJson.Append(",\"projId\":\"" + rows[i].Field<int>("ProjId").ToString() + "\"");
                sbJson.Append(",\"taskId\":\"" + rows[i].Field<long>("TaskId").ToString() + "\"");
                sbJson.Append(",\"parentID\":\"" + rows[i].Field<int>("ParentID").ToString() + "\"");
                sbJson.Append(",\"name\":\"" + rows[i].Field<string>("Name").ToString() + "\"");
                sbJson.Append(",\"depends\":\"" + rows[i].Field<string>("Depends").ToString() + "\"");
                sbJson.Append(",\"keyPointType\":\"" + rows[i].Field<int>("KeyPointType").ToString() + "\"");
                sbJson.Append(",\"level\":" + rows[i].Field<int>("Level").ToString());
                sbJson.Append(",\"description\":\"" + rows[i].Field<string>("Description").ToString() + "\"");
                sbJson.Append(",\"progress\":\"" + rows[i].Field<decimal>("Progress").ToString() + "\"");
                sbJson.Append(",\"status\":\"STATUS_ACTIVE\"");
                sbJson.Append(",\"canWrite\":true");
                sbJson.Append(",\"start\":" + TypeHelper.ConvertDateTimeToInt(rows[i].Field<DateTime>("DatePlanStart")));
                sbJson.Append(",\"ManageEmpName\":\"" + baseEmployeeDB.Get(rows[i].Field<int>("ManageEmpId")).EmpName + "\"");
                sbJson.Append(",\"duration\":\"" + rows[i].Field<int>("Duration").ToString() + "\"");
                sbJson.Append(",\"end\":" + TypeHelper.ConvertDateTimeToInt(rows[i].Field<DateTime>("DatePlanFinish")));
                sbJson.Append(",\"startIsMilestone\":false");
                sbJson.Append(",\"endIsMilestone\":false");
                sbJson.Append(",\"collapsed\":false");
                sbJson.Append(",\"assigs\":[]");
                sbJson.Append(",\"hasChild\":false");
                sbJson.Append("}");

                var tempRows = source.Select("ParentID=" + rows[i].Field<int>("ID").ToString()).OrderBy(dr => dr.Field<int>("Order")).ToArray();
                if (tempRows.Length > 0)
                {

                    RecuriseProcess(source, tempRows, sbJson);
                }
            }
        }


        public ActionResult addRootTask(int id, int projId)
        {
            var model = new DataModel.Models.DesTaskGantt();
            ViewBag.parentID = id;
            ViewBag.projid = projId;
            var _parentModel = desTaskGanttDB.Get(id);

            ViewBag.ParentDatePlanStart = _parentModel.DatePlanStart;
            ViewBag.ParentDatePlanFinish = _parentModel.DatePlanFinish;

            return View("EditGantt", model);
        }
        public ActionResult editTask(int id, int view = 0)
        {
            var model = desTaskGanttDB.Get(id);
            var _parentModel = desTaskGanttDB.Get(model.ParentId);
            ViewBag.ParentDatePlanStart = _parentModel != null ? _parentModel.DatePlanStart.ToString("yyyy-MM-dd") : "";
            ViewBag.ParentDatePlanFinish = _parentModel != null ? _parentModel.DatePlanFinish.ToString("yyyy-MM-dd") : "";
            var deptName = baseEmployeeDB.GetBaseEmployeeInfo(model.ManageEmpId) == null ? "0" : baseEmployeeDB.GetBaseEmployeeInfo(model.ManageEmpId).EmpDepID.ToString();
            ViewData["ManageEmpId1"] = model.ManageEmpId.ToString();
            ViewData["ManageEmpId2"] = deptName;

            ViewBag.Readonly = "readonly=\"readonly\"";

            if (model.KeyPointType > 0)
            {
                ViewBag.bit = 2;
            }
            else
            {
                ViewBag.bit = 1;
            }
            if (view > 0)
            {
                ViewBag.bit = 3;
            }
            return View("EditTask", model);
        }
        public ActionResult SaveGantt(int Id)
        {
            var model = new DesTaskGantt();
            int reuslt = 0;
            int projId = TypeHelper.parseInt(Request.Form["projId"]);
            int parentID = TypeHelper.parseInt(Request.Form["parentID"]);
            var _ManageEmpId = Request.Form["ManageEmpId"];

            try
            {
                Common.ModelConvert.MvcDefaultSave<DataModel.Models.DesTaskGantt>(model, this.userInfo);

                DataModel.Models.DesTaskGantt parentNode = desTaskGanttDB.Get(parentID);
                model.ProjId = projId;
                model.TaskId = 0;
                model.ParentId = parentID;
                model.ManageEmpId = TypeHelper.parseInt(_ManageEmpId.Split('-')[0]);
                model.Name = TypeHelper.parseString(Request.Form["name"]);
                model.Depends = "";
                model.Progress = TypeHelper.parseDecimal(Request.Form["Progress"]);
                model.Duration = TypeHelper.parseInt(Request.Form["Duration"]);
                model.DatePlanStart = TypeHelper.parseDateTime(Request.Form["DatePlanStart"]);
                model.DatePlanFinish = TypeHelper.parseDateTime(Request.Form["DatePlanFinish"]);
                model.KeyPointType = TypeHelper.parseInt(Request.Form["KeyPointType"]);
                model.startIsMilestone = false;
                model.endIsMilestone = false;
                model.Description = TypeHelper.parseString(Request.Form["Description"]);
                model.Path = parentNode.Path + parentNode.Id.ToString() + ",";
                model.Level = parentNode.Level + 1;
                string sql = string.Format("SELECT MAX([Order]) FROM DesTaskGantt WHERE Path = '{0}' and DeleterEmpId=0", parentNode.Path);
                model.Order = TypeHelper.parseInt(DAL.DBExecute.ExecuteScalar(sql)) + 1;
                desTaskGanttDB.Add(model);
                desTaskGanttDB.UnitOfWork.SaveChanges();
                reuslt = model.Id;
                reuslt = 1;
                var ba = new DBSql.Sys.BaseAttach();
                ba.MoveFile(model.Id, userInfo.EmpID, userInfo.EmpName);

            }
            catch
            {

            }

            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }

        public ActionResult SaveTask(int Id)
        {
            var model = desTaskGanttDB.Get(Id);
            int reuslt = 0;
            int projId = TypeHelper.parseInt(Request.Form["projId"]);
            int bit = TypeHelper.parseInt(Request.Form["bit"]);
            int parentID = TypeHelper.parseInt(Request.Form["parentID"]);
            var _ManageEmpId = Request.Form["ManageEmpId"];
            try
            {
                if (bit == 1)
                {
                    #region
                    model.Progress = TypeHelper.parseDecimal(Request.Form["Progress"]);
                    model.Duration = TypeHelper.parseInt(Request.Form["Duration"]);
                    model.DatePlanStart = TypeHelper.parseDateTime(Request.Form["DatePlanStart"]);
                    model.DatePlanFinish = TypeHelper.parseDateTime(Request.Form["DatePlanFinish"]);
                    model.Description = TypeHelper.parseString(Request.Form["Description"]);

                    #endregion
                }
                else if (bit == 2)
                {
                    #region
                    model.Name = TypeHelper.parseString(Request.Form["name"]);
                    model.ManageEmpId = TypeHelper.parseInt(_ManageEmpId.Split('-')[0]);
                    model.Depends = "";
                    model.Progress = TypeHelper.parseDecimal(Request.Form["Progress"]);
                    model.Duration = TypeHelper.parseInt(Request.Form["Duration"]);
                    model.DatePlanStart = TypeHelper.parseDateTime(Request.Form["DatePlanStart"]);
                    model.DatePlanFinish = TypeHelper.parseDateTime(Request.Form["DatePlanFinish"]);
                    model.KeyPointType = TypeHelper.parseInt(Request.Form["KeyPointType"]);
                    model.Description = TypeHelper.parseString(Request.Form["Description"]);
                    #endregion
                }
                else if (bit == 3)
                {
                    #region
                    model.Progress = TypeHelper.parseDecimal(Request.Form["Progress"]);
                    model.Description = TypeHelper.parseString(Request.Form["Description"]);
                    #endregion
                }

                desTaskGanttDB.Edit(model);
                desTaskGanttDB.UnitOfWork.SaveChanges();
                reuslt = model.Id;
                reuslt = 1;
            }
            catch
            {

            }

            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }


        public string GetDesTaskByCurrentUsers()
        {
            var str = "";
            var model = new DBSql.Sys.BaseConfig().FirstOrDefault(x => x.ConfigEngName == "DesTaskPreAlert");
            if (null != model)
            {
                int days = TypeHelper.parseInt(model.ConfigValue);
                DateTime limitTime = DateTime.Now.AddDays(days);
                str = JQ.Util.JavaScriptSerializerUtil.getJson(new DBSql.Design.DesTaskGantt().GetList(m => m.DeleterEmpId == 0 && m.KeyPointType > 0 && m.DatePlanStart <= limitTime && m.Progress != 100).OrderBy(m => m.DatePlanStart).Take(6).Select(m => new { m.Name, m.DatePlanStart, m.Id }).ToList());
            }

            return str;

        }

        public JsonResult GetSplitFiles()
        {
            long attachID = JQ.Util.TypeParse.parse<int>(Request.Params["attachID"]);
            long attachVer = JQ.Util.TypeParse.parse<int>(Request.Params["attachVer"]);
            int type = JQ.Util.TypeParse.parse<int>(Request.Params["type"]);
            if (attachID == 0)
            {
                return Json(new object[] { }, JsonRequestBehavior.AllowGet);
            }
            using (var db = new DBSql.Design.DesTaskSplitFile())
            {
                List<DBSql.Design.SpllitFileDisplay> data = new List<DBSql.Design.SpllitFileDisplay>();
                if (type == 1)
                {
                    data = db.GetSplitFiles(attachID, attachVer).Where(p => !p.Name.Contains("[已签]")).ToList();
                }
                else
                {
                    data = db.GetSplitFiles(attachID, attachVer).Where(p => p.Name.Contains("[已签]")).ToList();
                }

                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public string GetSplitFilesByMuli()
        {
            long attachID = JQ.Util.TypeParse.parse<int>(Request.Params["attachID"]);
            long attachVer = JQ.Util.TypeParse.parse<int>(Request.Params["attachVer"]);
            string splitFileIds = JQ.Util.TypeParse.parse<string>(Request.Params["splitIds"]);
            if (attachID == 0)
            {
                return JavaScriptSerializerUtil.getJson(new object[] { });
            }
            var db = new DBSql.Design.DesTaskSplitFile();
            var list = db.GetSplitFiles(attachID, attachVer);
            if (splitFileIds == "")
            {
                list = list.Where(p => p.ID == 0).ToList();
            }
            else
            {
                list = list.Where(p => ("," + splitFileIds + ",").Contains("," + p.ID.ToString() + ",")).ToList();
            }
            return JavaScriptSerializerUtil.getJson(list);

        }




        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <returns></returns>
        public string GetTaskListForPublic()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "p.Id desc";
            }
            PageModel.SelectCondtion.Add("ProjDeleterEmpId", "");
            PageModel.SelectCondtion.Add("TaskDeleterEmpId", "");
            PageModel.SelectCondtion.Add("ProjId", Request.Params["ProjID"] == null ? 0 : Common.ExtensionMethods.Value<int>(Request.Params["ProjID"]));
            PageModel.SelectCondtion.Add("TaskPhaseId", Request.Params["TaskPhaseID"] == null ? 0 : Common.ExtensionMethods.Value<int>(Request.Params["TaskPhaseID"]));


            #region 任务列表
            if (Request.Params["Type"] != null)
            {
                if (Request.Params["Type"].ToString() == "ShowSpecTask")
                {
                    //仅显示 专业任务列表
                    PageModel.SelectCondtion.Add("TaskType", "1");
                }
                else
                {
                    //仅显示 设计任务列表
                    PageModel.SelectCondtion.Add("TaskType", "0");
                }
            }
            else
            {
                //仅显示 设计任务列表
                PageModel.SelectCondtion.Add("TaskType", "0");
            }
            #endregion

            #region 权限
            //默认个人参与
            PageModel.SelectCondtion.Add("PermitEmpID", userInfo.EmpID);
            //根据筛选条件
            if (Request.Params["RoleJoinProject"] != null)
            {
                switch (Request.Params["RoleJoinProject"].ToString())
                {
                    case "1":
                        PageModel.SelectCondtion.Add("ProjEmp", userInfo.EmpID);
                        break;
                    case "2":
                        PageModel.SelectCondtion.Add("SpecEmp", userInfo.EmpID);
                        break;
                    case "3":
                        PageModel.SelectCondtion.Add("PersonEmp", userInfo.EmpID);
                        break;
                    default:
                        break;
                }
            }
            #endregion

            var dt = desTaskDB.GetTaskListForPublic(PageModel);
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = dt
            });

        }

        public string jsonByArrange(string mainProjId, string projId, string projPhaseId, string desTaskGroupId, string type)
        {
            //var json = { choosedUserNames:"admin,周军",choosedUsers: "1,2"} 默认格式

            string json = "{\"Result\":false}";
            string EmpIds = "";
            string EmpNames = "";
            desTaskDB.GetJsonByArrange(mainProjId, projId, projPhaseId, desTaskGroupId, type, ref EmpIds, ref EmpNames);
            if (EmpIds != "" && EmpNames != "")
            {
                string info = "{ \\\"choosedUserNames\\\":\\\"" + EmpNames.TrimEnd(',') + "\\\",\\\"choosedUsers\\\":\\\"" + EmpIds.TrimEnd(',') + "\\\"}";
                json = "{\"Result\":true,\"Info\":\"" + info + "\"}";
            }
            return json;
        }

        /// <summary>
        /// 根据条件获取校审文件
        /// </summary>
        /// <returns></returns>
        public string GetCheckFile()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "taskId desc";
            }

            if (Request.Params["ProjId"] != null)
            {
                PageModel.SelectCondtion.Add("ProjId", Request.Params["ProjId"].ToString());
            }
            if (Request.Params["ProjPhaseId"] != null)
            {
                PageModel.SelectCondtion.Add("ProjPhaseId", Request.Params["ProjPhaseId"].ToString());
            }
            if (Request.Params["EmpId"] != null)
            {
                PageModel.SelectCondtion.Add("AttachEmpId", Request.Params["EmpId"].ToString());
            }

            var dt = new DBSql.Design.DesTask().GetCheckAttach(PageModel);

            foreach(DataRow row in dt.Rows)
            {
                var flowJson = Common.ModelConvert.Xml2Json(row.Field<string>("AttachFlowNode"));
                row["AttachFlowNode"] = flowJson;
            }

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = dt
            });
        }

        public string GetCheckDetail(string ids)
        {
            DataTable dt = new DBSql.Design.DesTask().GetCheckDetail(ids);
            return JavaScriptSerializerUtil.getJson(new
            {
                rows = dt
            }); ;
        }

    }
}
