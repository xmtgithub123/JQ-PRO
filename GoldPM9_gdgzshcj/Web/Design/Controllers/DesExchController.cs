using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;
using System;
using System.Linq.Expressions;
using Common.Data.Extenstions;
using System.Xml;
using System.Data;
using System.Text;
using System.Xml.Linq;

namespace Design.Controllers
{
    [Description("DesExch")]
    public partial class DesExchController : CoreController
    {
        private DBSql.Design.DesExch op = new DBSql.Design.DesExch();
        private DBSql.Design.DesExchRec rec = new DBSql.Design.DesExchRec();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        private DBSql.Design.DesTask desTask = new DBSql.Design.DesTask();
        private DBSql.Sys.BaseData data = new DBSql.Sys.BaseData();
        private DBSql.Sys.AllBaseEmployee emp = new DBSql.Sys.AllBaseEmployee();
        private DBSql.Core.ModelExchange exchange = new DBSql.Core.ModelExchange();
        private DBSql.Core.ModelExchangeReceive exchRecModel = new DBSql.Core.ModelExchangeReceive();
        private DBSql.Project.Project project = new DBSql.Project.Project();

        /**********************提资策划********************/
        /// <summary>
        /// 转到 提资策划页面
        /// </summary>
        /// <param name="projID"></param>
        /// <param name="taskGroupId"></param>
        /// <returns></returns>
        [Description("提资策划")]
        public ActionResult ExchPlanInfo(int projID, int taskGroupId)
        {
            ViewBag.projID = projID;
            ViewBag.taskGroupId = taskGroupId;
            var taskSpecId = Common.ModelConvert.ConvertToDefaultType<int>(Request.Params["taskSpecId"]);
            ViewBag.taskSpecId = taskSpecId;
            DataModel.Models.DesTaskGroup TaskGroupModel = new DBSql.Design.DesTaskGroup().FirstOrDefault(p => p.Id == taskGroupId);
            ViewBag.TaskGroupModel = TaskGroupModel;
            ViewBag.EmpId = userInfo.EmpID;
            int ProjEmpId = 0;
            DataModel.Models.Project Proj = new DBSql.Project.Project().Get(projID);
            if (Proj != null)
            {
                ProjEmpId = Proj.ProjEmpId;
            }

            ViewBag.Qualification = JavaScriptSerializerUtil.getJson(new DBSql.Sys.BaseQualification().GetQualificationEmployee(0, 0, 0, 0));
            List<string> permission = PermissionList("ExchPlanList");
            ViewBag.permission = JavaScriptSerializerUtil.getJson(permission);
            if (permission.Contains("add") || TaskGroupModel.TaskGroupEmpID == userInfo.EmpID || ProjEmpId == userInfo.EmpID)
            {
                ViewBag.addPermission = 1;
            }
            if (permission.Contains("del") || TaskGroupModel.TaskGroupEmpID == userInfo.EmpID || ProjEmpId == userInfo.EmpID)
            {
                ViewBag.delPermission = 1;
            }
            if (permission.Contains("add") || TaskGroupModel.TaskGroupEmpID == userInfo.EmpID || ProjEmpId == userInfo.EmpID)
            {
                ViewBag.addPermission = 1;
            }
            if (permission.Contains("edit") || permission.Contains("alledit") || TaskGroupModel.TaskGroupEmpID == userInfo.EmpID || ProjEmpId == userInfo.EmpID)
            {
                ViewBag.editPermission = 1;
            }

            ViewBag.mainTabsUsed = JavaScriptSerializerUtil.getJson(new
            {
                PlanTableList = ContainPermission("PlanTableList"),
                ProjectPlanList = ContainPermission("ProjectPlanList"),
                ExchPlanList = ContainPermission("ExchPlanList"),
                SpecPlanList = ContainPermission("SpecPlanList"),
                ProjGanttList = ContainPermission("ProjGanttList")
            });

            return View();
        }

        /// <summary>
        /// 下拉选择TaskGroupId下的专业
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ComboSpec()
        {
            //int TaskGroupId = TypeHelper.parseInt(Request.QueryString["TaskGroupId"]);
            //var listSpec = desTask.GetList(x =>
            //        x.TaskGroupId == TaskGroupId
            //        && x.DeleterEmpId == 0
            //        && x.TaskType == 1
            //        && x.TaskSpecId != 0
            //    )
            //    .Select(x => new
            //    {
            //        Id = x.Id,
            //        TaskSpecId = x.TaskSpecId,
            //        TaskName = x.TaskName,

            //    });
            //所有专业
            DataModel.Models.BaseData baseModel = new DBSql.Sys.BaseData().FirstOrDefault(p => p.BaseNameEng == "Special");
            string OrderNumber = "000_";
            if (baseModel != null)
            {
                OrderNumber = baseModel.BaseOrder + "_";
            }
            List<dynamic> SpecList = new List<dynamic>();
            var listSpec = data.GetList(p => p.BaseOrder.StartsWith(OrderNumber) && p.BaseIsDeleted == false).Select(p => new
            {
                TaskSpecId = p.BaseID,
                TaskName = p.BaseName,
            });
            if (!string.IsNullOrEmpty(Request.Params["IsTotal"]))// 是否包含汇总专业
            {
                object totalSpec = new
                {
                    TaskSpecId = 0,
                    TaskName = "汇总"
                };
                SpecList.Add(totalSpec);
            }
            SpecList.AddRange(listSpec);
            return Json(SpecList);

        }

        /**********************任务处理********************/

        /// <summary>
        /// 保存 任务提出资料 数据
        /// </summary>
        /// <returns></returns>

        public ActionResult PostExchSave(FormCollection form)
        {
            int Id = TypeHelper.parseInt(form["Id"]);
            var model = new DataModel.Models.DesExch();
            model.MvcSetValue();
            DataModel.EmpSession selectExchEmpId = null;//选定的提资人员
            if (model.ExchEmpId != 0)
            {
                if (emp.FirstOrDefault(p => p.EmpID == model.ExchEmpId) != null)
                {
                    selectExchEmpId = new DataModel.EmpSession();

                    selectExchEmpId.EmpDepID = emp.FirstOrDefault(p => p.EmpID == model.ExchEmpId).DepartmentID;
                    selectExchEmpId.EmpDepName = emp.FirstOrDefault(p => p.EmpID == model.ExchEmpId).DepartmentName;
                    selectExchEmpId.EmpID = model.ExchEmpId;
                    selectExchEmpId.EmpName = emp.FirstOrDefault(p => p.EmpID == model.ExchEmpId).EmpName;
                }
            }
            try
            {
                List<DataModel.Models.DesExchRec> recList = new List<DesExchRec>();
                List<TempExchRec> list = new List<TempExchRec>();
                if (!string.IsNullOrEmpty(form["JsonRows"]))
                {
                    list = new JavaScriptSerializer().Deserialize<List<TempExchRec>>(form["JsonRows"]);
                    foreach (TempExchRec temp in list)
                    {
                        DataModel.Models.DesExchRec rec = new DesExchRec();
                        rec.Id = temp.Id;
                        rec.RecEmpId = TypeHelper.parseInt(temp.RecEmpId);
                        rec.RecEmpName = temp.RecEmpName;
                        rec.RecSpecId = temp.RecSpecId;
                        rec.RecSpecName = temp.RecSpecName;

                        //添加部门和部门编码
                        if (rec.RecEmpId != 0)
                        {
                            rec.RecEmpDepId = emp.FirstOrDefault(p => p.EmpID == rec.RecEmpId).DepartmentID;
                            rec.RecEmpDepName = emp.FirstOrDefault(p => p.EmpID == rec.RecEmpId).DepartmentName;
                        }
                        else
                        {
                            rec.RecEmpDepId = 0;
                            rec.RecEmpDepName = "";
                        }
                        recList.Add(rec);

                    }
                }
                if (TypeHelper.isDateTime(form["ExcLastPutTime"]) && !string.IsNullOrEmpty(form["ExcLastPutTime"]))
                {
                    foreach (DataModel.Models.DesExchRec recModel in recList)
                    {
                        recModel.ExcLastPutTime = TypeHelper.parseDateTime(form["ExcLastPutTime"]);
                    }
                }

                int result = op.InsertOrUpdateExchRecData(model, recList, selectExchEmpId, userInfo);
                if (result > 0)
                {
                    doResult = DoResultHelper.doSuccess(1, "操作成功");
                }
                else
                {
                    doResult = DoResultHelper.doError("操作失败");
                }

            }
            catch (Exception ex)
            {
                doResult = DoResultHelper.doError(ex.Message);
            }
            return Json(doResult);
        }

        /// <summary>
        /// 添加  提资计划 页面
        /// </summary>
        /// <returns></returns>
        public ActionResult SpecExchAdd()
        {
            DataModel.Models.DesExch desExchModel = new DesExch();
            desExchModel.ProjId = TypeHelper.parseInt(Request.Params["projID"]);
            int taskGroupId = TypeHelper.parseInt(Request.Params["taskGroupId"]);
            desExchModel.ExchSpecId = TypeHelper.parseInt(Request.QueryString["ExchSpecID"]);
            desExchModel.taskGroupId = taskGroupId;
            List<string> permission = PermissionList("ExchPlanList");
            DataModel.Models.DesTaskGroup TaskGroupModel = new DBSql.Design.DesTaskGroup().FirstOrDefault(p => p.Id == taskGroupId);
            ViewBag.TaskGroupModel = TaskGroupModel;
            ViewBag.EmpId = userInfo.EmpID;
            int ProjEmpId = 0;
            DataModel.Models.Project Proj = new DBSql.Project.Project().Get(desExchModel.ProjId);
            if (Proj != null)
            {
                ProjEmpId = Proj.ProjEmpId;
            }
            int TaskSpecEmpID = 0;
            DataModel.Models.DesTask desTask = new DBSql.Design.DesTask().FirstOrDefault(p => p.TaskGroupId == taskGroupId && p.TaskSpecId == desExchModel.ExchSpecId && p.TaskType == 1);
            if (desTask != null)
            {
                TaskSpecEmpID = desTask.TaskEmpID;
            }

            if (permission.Contains("add") || permission.Contains("alledit") || TaskGroupModel.TaskGroupEmpID == userInfo.EmpID || ProjEmpId == userInfo.EmpID || TaskSpecEmpID == userInfo.EmpID)
            {
                ViewBag.addPermission = 1;
                ViewBag.SaveInfo = "<a class=\"easyui-linkbutton\" data-options=\"plain: true,iconCls: 'fa fa-save'\" onclick=\"Save();\">保存</a>";
                ViewBag.Append = "<a class=\"easyui-linkbutton\" data-options=\"plain: true,iconCls: 'fa fa-plus'\" onclick=\"append();\">添加</a>";
                ViewBag.Remove = "<a class=\"easyui-linkbutton\" data-options=\"plain: true,iconCls: 'fa fa-trash'\" onclick=\"removexit();\">删除</a>";
            }
            else
            {
                ViewBag.addPermission = 0;
                ViewBag.SaveInfo = "";
                ViewBag.Append = "";
                ViewBag.Remove = "";
            }
            ViewBag.Priority = ExchPriorityBind();//提资优先级
            return View("ExchInfo", desExchModel);
        }

        /// <summary>
        /// 编辑 提资计划 页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult SpecExchEdit(int Id)
        {
            ViewBag.Priority = ExchPriorityBind();//提资优先级
            DataModel.Models.DesExch desExchModel = op.Get(Id);
            DataModel.Models.DesExchRec desRecModel = rec.FirstOrDefault(p => p.ExchId == Id);
            if (desRecModel != null)
            {
                ViewBag.ExcLastPutTime = desRecModel.ExcLastPutTime;
            }
            List<string> permission = PermissionList("ExchPlanList");
            DataModel.Models.DesTaskGroup TaskGroupModel = new DBSql.Design.DesTaskGroup().FirstOrDefault(p => p.Id == desExchModel.taskGroupId);
            ViewBag.TaskGroupModel = TaskGroupModel;
            ViewBag.EmpId = userInfo.EmpID;
            int ProjEmpId = 0;
            DataModel.Models.Project Proj = new DBSql.Project.Project().Get(desExchModel.ProjId);
            if (Proj != null)
            {
                ProjEmpId = Proj.ProjEmpId;
            }

            int TaskSpecEmpID = 0;
            DataModel.Models.DesTask desTask = new DBSql.Design.DesTask().FirstOrDefault(p => p.TaskGroupId == desExchModel.taskGroupId && p.TaskSpecId == desExchModel.ExchSpecId && p.TaskType == 1);
            if (desTask != null)
            {
                TaskSpecEmpID = desTask.TaskEmpID;
            }

            if (permission.Contains("edit") || permission.Contains("alledit") || TaskGroupModel.TaskGroupEmpID == userInfo.EmpID || ProjEmpId == userInfo.EmpID || TaskSpecEmpID == userInfo.EmpID)
            {
                ViewBag.editPermission = 1;
                ViewBag.SaveInfo = "<a class=\"easyui-linkbutton\" data-options=\"plain: true,iconCls: 'fa fa-save'\" onclick=\"Save();\">保存</a>";
                ViewBag.Append = "<a class=\"easyui-linkbutton\" data-options=\"plain: true,iconCls: 'fa fa-plus'\" onclick=\"append();\">添加</a>";
                ViewBag.Remove = "<a class=\"easyui-linkbutton\" data-options=\"plain: true,iconCls: 'fa fa-trash'\" onclick=\"removexit();\">删除</a>";

            }
            else
            {
                ViewBag.editPermission = 0;
                ViewBag.SaveInfo = "";
                ViewBag.Append = "";
                ViewBag.Remove = "";
            }
            return View("ExchInfo", desExchModel);
        }

        /// <summary>
        /// 绑定提资优先级    提资优先级：0 普通 1 重要 2 紧急
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> ExchPriorityBind()
        {
            List<SelectListItem> listItem = new List<SelectListItem>();
            listItem.Add(new SelectListItem() { Text = "普通", Value = "0" });
            listItem.Add(new SelectListItem() { Text = "重要", Value = "1" });
            listItem.Add(new SelectListItem() { Text = "紧急", Value = "2" });
            return listItem;
        }


        /// <summary>
        /// 根据专业Id获取所有的(设计：资格)人员信息
        /// </summary>
        /// <param name="SpecId"></param>
        /// <returns></returns>
        [HttpPost]
        public string GetAllPersonBySpecID()
        {
            int SpecId = TypeHelper.parseInt(Request.QueryString["SpecId"]);
            IEnumerable<DBSql.Sys.EmpQualification> list = new DBSql.Sys.BaseQualification().GetQualificationEmployee((int)DataModel.NodeType.设计, SpecId, 0, 0);
            var SpecEmpList = list.Select(p => new
            {
                p.EmpID,
                p.EmpName,
                p.EmpDepID,
                p.DepName
            }).Distinct();//查询绑定专业的所有人员(排重复)
            return JavaScriptSerializerUtil.getJson(SpecEmpList);
        }

        /// <summary>
        /// 通过提资专业来绑定信息
        /// </summary>
        /// <param name="SpecId"></param>
        /// <returns></returns>
        [HttpPost]
        public string DesSpecExchJson()
        {
            int SpecId = TypeHelper.parseInt(Request.Params["SpecId"]);
            int taskGroupId = TypeHelper.parseInt(Request.Params["taskGroupId"]);
            long taskId = TypeHelper.parseLong(Request.QueryString["taskId"]);
            int ProjId = TypeHelper.parseInt(Request.Params["projID"]);
            //var ExchList = op.GetList(p => p.ExchTaskId == taskId && p.ExchSpecId == SpecId && p.taskGroupId == taskGroupId && p.ExchType == 1 && p.ExchIsInvalid == true).ToList();
            var ExchList = op.GetList(p => p.taskGroupId == taskGroupId && p.ProjId == ProjId && p.ExchSpecId == SpecId && p.ExchType == 1 && p.ExchIsInvalid == true).ToList();
            var RecList = rec.GetList(p => p.ExchId != 0).ToList();
            var TargetList = from exch in ExchList
                             join recItem in RecList on exch.Id equals recItem.ExchId into leftTarget
                             from gcItem in leftTarget.DefaultIfEmpty()//表示外联结
                             orderby exch.Id ascending
                             select new
                             {
                                 exch.Id,
                                 SpecIdent = exch.Id,//专业标识合并单元格使用
                                 EmpIdent = exch.Id,
                                 dateIdent = exch.Id,
                                 exch.ExchEmpId,
                                 exch.ExchEmpName,
                                 exch.ExchTitle,
                                 exch.CreatorEmpId,
                                 exch.CreationTime,
                                 exch.ExchSpecId,
                                 exch.ExchSpecName,
                                 RecSpecId = gcItem == null ? 0 : gcItem.RecSpecId,
                                 RecSpecName = gcItem == null ? "" : gcItem.RecSpecName,
                                 RecEmpId = gcItem == null ? 0 : gcItem.RecEmpId,
                                 RecEmpName = gcItem == null ? "" : gcItem.RecEmpName,
                                 ExcLastPutTime = gcItem == null ? new DateTime(1900, 1, 1) : gcItem.ExcLastPutTime,
                                 RecTime = gcItem == null ? new DateTime(1900, 1, 1) : gcItem.RecTime,
                             };
            return JavaScriptSerializerUtil.getJson(new
            {
                total = TargetList.Count(),
                rows = TargetList
            });
        }



        /// <summary>
        /// 专业个人任务提资记录信息
        /// </summary>
        /// <param name="SpecId"></param>
        /// <returns></returns>
        [HttpPost]
        public string DesSpecExchInfoList()
        {
            //int SpecId = TypeHelper.parseInt(Request.QueryString["SpecId"]);
            //int taskGroupId = TypeHelper.parseInt(Request.QueryString["taskGroupId"]);
            long taskId = TypeHelper.parseLong(Request.QueryString["taskId"]);
            //int ProjId = TypeHelper.parseInt(Request.QueryString["projID"]);
            var ExchList = op.GetList(p => p.ExchTaskId == taskId && p.ExchIsInvalid == true).ToList();
            var RecList = rec.GetList(p => p.ExchId != 0).ToList();
            var TargetList = from exch in ExchList
                             join recItem in RecList on exch.Id equals recItem.ExchId into leftTarget
                             from gcItem in leftTarget.DefaultIfEmpty()//表示外联结
                             orderby exch.Id descending
                             select new
                             {
                                 exch.Id,
                                 SpecIdent = exch.Id,//专业标识合并单元格使用
                                 EmpIdent = exch.Id,
                                 dateIdent = exch.Id,
                                 exch.ExchEmpId,
                                 exch.ExchEmpName,
                                 exch.ExchTitle,
                                 exch.CreatorEmpId,
                                 exch.ExchSpecId,
                                 exch.ExchSpecName,
                                 RecSpecId = gcItem == null ? 0 : gcItem.RecSpecId,
                                 RecSpecName = gcItem == null ? "" : gcItem.RecSpecName,
                                 RecEmpId = gcItem == null ? 0 : gcItem.RecEmpId,
                                 RecEmpName = gcItem == null ? "" : gcItem.RecEmpName,
                                 ExcLastPutTime = gcItem == null ? new DateTime(1900, 1, 1) : gcItem.ExcLastPutTime,
                                 RecTime = gcItem == null ? new DateTime(1900, 1, 1) : gcItem.RecTime,
                             };
            return JavaScriptSerializerUtil.getJson(new
            {
                total = TargetList.Count(),
                rows = TargetList
            });
        }



        /// <summary>
        /// 表单提资单信息
        /// </summary>
        /// <param name="SpecId"></param>
        /// <returns></returns>
        [HttpPost]
        public string FormDesSpecExchJson()
        {
            int SpecId = TypeHelper.parseInt(Request.Params["SpecId"]);
            int taskGroupId = TypeHelper.parseInt(Request.Params["taskGroupId"]);
            var ExchList = op.GetList(p => p.ExchSpecId == SpecId && p.taskGroupId == taskGroupId && (p.ExchType == 1 || p.ExchType == 2) && p.ExchIsInvalid == true).ToList();//专业内的提资 ExchType == 1
            var RecList = rec.GetList(p => p.ExchId != 0).ToList();
            var TargetList = from exch in ExchList
                             join recItem in RecList on exch.Id equals recItem.ExchId into leftTarget
                             from gcItem in leftTarget.DefaultIfEmpty()//表示外联结
                             orderby exch.Id ascending
                             select new
                             {
                                 exch.Id,
                                 SpecIdent = exch.Id,//专业标识合并单元格使用
                                 EmpIdent = exch.Id,
                                 dateIdent = exch.Id,
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
                                 RecTime = gcItem == null ? new DateTime(1900, 1, 1) : gcItem.RecTime,
                             };
            return JavaScriptSerializerUtil.getJson(new
            {
                total = TargetList.Count(),
                rows = TargetList
            });
        }

        /// <summary>
        /// 根据提资的Id获取收资专业和收资人
        /// </summary>
        /// <param name="ExchId"></param>
        /// <returns></returns>
        [HttpPost]
        public string RecSpecAndEmp()
        {
            int ExchId = TypeHelper.parseInt(Request.QueryString["ExchId"]);
            var RecList = rec.GetList(p => p.ExchId == ExchId).Select(p => new
            {
                p.Id,
                p.RecEmpId,
                p.RecEmpName,
                p.RecSpecId,
                p.RecSpecName,
                RecTime = p.RecTime.Year > 1900 ? p.RecTime.ToString("yyyy-MM-dd") : ""
            });
            return JavaScriptSerializerUtil.getJson(new
            {
                total = RecList.Count(),
                rows = RecList
            });
        }

        /// <summary>
        /// 转到 提资策划列表 页面
        /// </summary>
        /// <returns></returns>
        [Description("提资策划列表")]
        public ActionResult ExchPlanList()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ExchPlanList")));
            return View();
        }


        /// <summary>
        /// 删除信息
        /// </summary>
        /// <returns></returns>
        [Description("删除信息")]
        [HttpPost]
        public ActionResult DeleteInfo()
        {
            int result = 0;
            int Id = TypeHelper.parseInt(Request.Params["Id"]);
            try
            {
                List<int> list = new List<int>();
                list.Add(Id);
                result = op.DeleteExchInfo(list);
                if (result > 0)
                {
                    doResult = DoResultHelper.doSuccess(1, "操作成功");
                }
                else
                {
                    doResult = DoResultHelper.doError("操作失败");
                }
            }
            catch (Exception ex)
            {
                doResult = DoResultHelper.doError(ex.Message);
            }

            return Json(doResult);
        }


        /// <summary>
        /// 导入提资模板页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ExchModelData()
        {
            ViewBag.projID = Request.Params["projID"];
            ViewBag.taskGroupId = Request.Params["taskGroupId"];
            return View();
        }

        /// <summary>
        /// 导入提资模板(仅显示对应提资专业的记录)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string ExchModelJson()
        {
            int PageNum = TypeHelper.parseInt(Request.Params["page"], 1);
            int Record = TypeHelper.parseInt(Request.Params["rows"], 10);

            string KeyJQSearch = Request.Params["KeyJQSearch"];
            int SpecId = TypeHelper.parseInt(Request.Params["SpecId"]);
            var list = exchange.GetList(p => p.ModelExchangeSpecID == SpecId);//根据专业来绑定数据

            if (!string.IsNullOrEmpty(KeyJQSearch))
            {
                list = exchange.GetList(p => p.ModelExchangeSpecID == SpecId && (p.ModelExchangeName.Contains(KeyJQSearch) || p.ModelExchangeContent.Contains(KeyJQSearch)));
            }

            var targetList = from item in list
                             select new
                             {
                                 ExchTitle = item.ModelExchangeName,//资料名称
                                 ExchContent = item.ModelExchangeContent,//内容
                                 ExchSpecId = item.ModelExchangeSpecID,
                                 ExchSpecName = item.FK_ModelExchange_ModelExchangeSpecID.BaseName,//专业名称
                                 PhaseName = item.FK_ModelExchange_ModelExchangePhaseID.BaseName,//阶段名称
                                 RecSpecIds = string.Join(",", exchRecModel.GetList(p => p.Id == item.Id).Select(p => p.ModelReceiveSpecID).ToList()),
                                 RecSpecName = string.Join(",", (from m in item.FK_ModelExchangeReceive_Id select m.FK_ModelExchangeReceive_ModelReceiveSpecID.BaseName).ToList()),
                             };

            return JavaScriptSerializerUtil.getJson(new
            {
                total = targetList.Count(),
                rows = (targetList.Skip(Record * (PageNum - 1)).Take(Record)).ToList()
            });

        }

        /// <summary>
        /// 保存  从模板中导入的数据
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public JsonResult SaveInsertExchModelData(FormCollection form)
        {
            int projID = TypeHelper.parseInt(Request.Params["projID"]);
            int taskGroupId = TypeHelper.parseInt(Request.Params["taskGroupId"]);
            string data = Request.Params["JsonRows"];
            List<TempExchRec> list = new List<TempExchRec>();
            try
            {
                if (!string.IsNullOrEmpty(data))
                {
                    list = new JavaScriptSerializer().Deserialize<List<TempExchRec>>(data);
                    foreach (TempExchRec temp in list)
                    {
                        List<DataModel.Models.DesExchRec> desExchRecList = new List<DesExchRec>();
                        DataModel.Models.DesExch model = new DesExch();
                        model.ExchTitle = temp.ExchTitle;
                        model.ExchSpecId = temp.ExchSpecId;
                        model.ExchSpecName = temp.ExchSpecName;
                        model.ExchContent = temp.ExchContent;
                        model.ProjId = projID;
                        model.taskGroupId = taskGroupId;
                        if (!string.IsNullOrEmpty(temp.RecSpecIds) && !string.IsNullOrEmpty(temp.RecSpecName))
                        {
                            string[] SpecID = temp.RecSpecIds.Split(',');
                            string[] SpecName = temp.RecSpecName.Split(',');
                            if (SpecID.Length == SpecName.Length)
                            {
                                for (int index = 0; index < SpecName.Length; index++)
                                {
                                    DataModel.Models.DesExchRec recModel = new DesExchRec();
                                    recModel.RecSpecId = TypeHelper.parseInt(SpecID[index]);
                                    recModel.RecSpecName = SpecName[index];
                                    desExchRecList.Add(recModel);
                                }
                            }
                        }
                        op.InsertOrUpdateExchRecData(model, desExchRecList, null, userInfo);
                    }
                }
                doResult = DoResultHelper.doSuccess(1, "操作成功");
            }
            catch (Exception ex)
            {
                doResult = DoResultHelper.doError(ex.Message);
            }

            return Json(doResult);
        }


        /// <summary>
        /// 待办提资列表信息
        /// </summary>
        /// <returns></returns>
        [Description("待办提资列表信息")]
        public ActionResult ExchRemindList()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ExchRemindList")));
            if (PermissionList("ExchRemindList").Contains("add"))
            {
                ViewBag.addPermission = 1;
            }
            return View();
        }

        /// <summary>
        ///  绑定设计任务的提资策划
        /// </summary>
        /// <returns></returns>
        public ActionResult ExchTaskList()
        {
            return View();
        }

        /// <summary>
        /// 提资详细信息页面
        /// </summary>
        /// <returns></returns>
        [Description("提资详细信息页面")]
        public ActionResult ExchInfoApprove(int Id)
        {
            ViewBag.Priority = ExchPriorityBind();//提资优先级
            DataModel.Models.DesExch desExchModel = op.Get(Id);
            DataModel.Models.DesExchRec desRecModel = rec.FirstOrDefault(p => p.ExchId == Id);
            if (desRecModel != null)
            {
                ViewBag.ExcLastPutTime = desRecModel.ExcLastPutTime;
            }
            int taskId = TypeHelper.parseInt(Request.Params["taskId"]);
            if (taskId != 0)
            {
                desExchModel.ExchTaskId = taskId;
            }
            if (desExchModel.ExchEmpId == 0)
            {
                desExchModel.ExchEmpId = userInfo.EmpID;
                desExchModel.ExchEmpName = userInfo.EmpName;
                desExchModel.ExchEmpDepId = userInfo.EmpDepID;
                desExchModel.ExchEmpDepName = userInfo.EmpDepName;
            }
            ViewBag.ReverseIdent = "1";
            ViewBag.RecEmpId = "0";//追加信息的时候使用
            ViewBag.RecSpecId = "0";
            ViewBag.SpecEmps = JavaScriptSerializerUtil.getJson(op.DbContext.Set<DataModel.Models.DesExchRec>().Where(p => p.ExchId == desExchModel.Id).Select(p => new
            {
                Id = p.Id,
                SpecID = p.RecSpecId,
                SpecName = p.RecSpecName,
                EmpName = p.RecEmpName,
                EmpID = p.RecEmpId,
            }).ToList());
            ViewBag.State = IsFlow(desExchModel.Id);
            BindProjInfo(desExchModel.ProjId);
            return View("ExchInfoApprove", desExchModel);
        }

        /// <summary>
        /// 待办提资Json列表信息
        /// </summary>
        [Description("待办提资Json列表信息")]
        [HttpPost]
        public string InHandExchList()
        {
            Common.SqlPageInfo sqlPage = new Common.SqlPageInfo();
            int PageNum = TypeHelper.parseInt(Request.Params["page"], 1);
            int Record = TypeHelper.parseInt(Request.Params["rows"], 20);

            if (!string.IsNullOrEmpty(Request.Params["KeyText"]))
            {
                string KeyText = Request.Params["KeyText"];
                sqlPage.TextCondtion = KeyText;
            }
            if (!string.IsNullOrEmpty(Request.Params["startTime"]) && TypeHelper.isDateTime(Request.Params["startTime"]))
            {
                DateTime startTime = TypeHelper.parseDateTime(Request.Params["startTime"]);
                sqlPage.SelectCondtion.Add("DateLower", startTime);
            }
            if (!string.IsNullOrEmpty(Request.Params["endTime"]) && TypeHelper.isDateTime(Request.Params["endTime"]))
            {
                DateTime endTime = TypeHelper.parseDateTime(Request.Params["endTime"]);
                sqlPage.SelectCondtion.Add("DateUpper", endTime);
            }
            if (!string.IsNullOrEmpty(Request.Params["SpecId"]))
            {
                int SpecId = TypeHelper.parseInt(Request.Params["SpecId"]);
                sqlPage.SelectCondtion.Add("ExchSpecID", SpecId);
            }
            var RecList = rec.GetList(p => p.ExchId != 0).ToList();
            Expression<Func<DataModel.Models.DesExch, bool>> func = op.GetFunc(sqlPage);//关联时候的分页
            List<string> permission = PermissionList("ExchRemindList");
            List<int> SpecList = op.QualificationSpec((int)DataModel.NodeType.专业, userInfo);//当前人员负责的专业
            if (!(permission.Contains("allview") || permission.Contains("alledit")))
            {
                if (permission.Contains("dep"))
                {
                    //if (SpecList.Count > 0)
                    //{
                    //    func = func.And(p => p.ExchEmpDepId == userInfo.EmpDepID || (SpecList.Contains(p.ExchSpecId) && p.ExchEmpDepId == 0));//查看自己的和没有指定提资人的（专业负责人）
                    //}
                    //else
                    //{
                    //    func = func.And(p => p.ExchEmpDepId == userInfo.EmpDepID);//部门
                    //}
                    func = func.And(p => p.ExchEmpDepId == userInfo.EmpDepID);//部门
                }
                else
                {
                    //if (SpecList.Count > 0)
                    //{
                    //    func = func.And(p => p.ExchEmpId == userInfo.EmpID || (SpecList.Contains(p.ExchSpecId) && p.ExchEmpId == 0));//查看自己的和没有指定提资人的（专业负责人）
                    //}
                    //else
                    //{
                    //    func = func.And(p => p.ExchEmpId == userInfo.EmpID);//只能查看自己的
                    //}
                    func = func.And(p => p.ExchEmpId == userInfo.EmpID);//只能查看自己的
                }
            }

            if (string.IsNullOrEmpty(Request.Params["IsAll"]))
            {
                func = func.And(p => p.ExchIsInvalid == true);//显示有效信息
            }
            var list = from exch in op.GetList(func)
                       join recItem in RecList on exch.Id equals recItem.ExchId into leftTarget
                       from gcItem in leftTarget.DefaultIfEmpty()//表示外联结
                       let proj = project.Get(exch.ProjId)
                       orderby exch.Id descending
                       select new
                       {
                           exch.Id,
                           SpecIdent = exch.Id,//专业标识合并单元格使用
                           EmpIdent = exch.Id,
                           dateIdent = exch.Id,
                           exch.ExchIsInvalid,
                           exch.ExchEmpId,
                           exch.ExchEmpName,
                           exch.ExchTitle,
                           exch.ExchSpecId,
                           exch.ExchTaskId,
                           exch.ExchSpecName,
                           RecSpecId = gcItem == null ? 0 : gcItem.RecSpecId,
                           RecSpecName = gcItem == null ? "" : gcItem.RecSpecName,
                           RecEmpId = gcItem == null ? 0 : gcItem.RecEmpId,
                           RecEmpName = gcItem == null ? "" : gcItem.RecEmpName,
                           ExcLastPutTime = gcItem == null ? new DateTime(1900, 1, 1) : gcItem.ExcLastPutTime,
                           RecTime = gcItem == null ? new DateTime(1900, 1, 1) : gcItem.RecTime,
                           ProjNumber = proj == null ? "" : proj.ProjNumber,
                           ProjName = proj == null ? "" : proj.ProjName,
                           Phasename = exch.taskGroupId == 0 ? PhaseName(exch.ProjId) : GetPhaseNameByTaskgroupId(exch.taskGroupId),
                           State = (gcItem == null ? "未处理" : (gcItem.RecStatus == 0 ? "未处理" : (gcItem.RecStatus == 1 ? "回退" : "通过"))),
                           RecContent = gcItem == null ? "" : gcItem.RecContent,//处理意见
                           NumberIdent = exch.Id,
                           projIdent = exch.Id,
                           process = exch.Id,
                           count = new DBSql.PubFlow.Flow().GetList(p => p.FlowRefID == exch.Id && p.FlowRefTable == "DesExch").Count(),
                           recCount = op.DbContext.Set<DataModel.Models.DesExchRec>().Where(p => p.ExchId == exch.Id && p.RecStatus == 1).Count()
                       };
            if (!string.IsNullOrEmpty(Request.Params["ExchState"]))
            {
                if (Request.Params["ExchState"] == "1")
                {
                    list = list.Where(p => p.count > 0).ToList();
                }
                else
                {
                    list = list.Where(p => p.count <= 0).ToList();
                }

            }

            return JavaScriptSerializerUtil.getJson(new
            {
                total = list.Count(),
                rows = (list.Skip(Record * (PageNum - 1)).Take(Record)).ToList()
            });

        }


        public string InHandExchTaskList()
        {
            Common.SqlPageInfo sqlPage = new Common.SqlPageInfo();
            int PageNum = TypeHelper.parseInt(Request.Params["page"], 1);
            int Record = TypeHelper.parseInt(Request.Params["rows"], 20);

            if (!string.IsNullOrEmpty(Request.Params["KeyText"]))
            {
                string KeyText = Request.Params["KeyText"];
                sqlPage.TextCondtion = KeyText;
            }
            if (!string.IsNullOrEmpty(Request.Params["startTime"]) && TypeHelper.isDateTime(Request.Params["startTime"]))
            {
                DateTime startTime = TypeHelper.parseDateTime(Request.Params["startTime"]);
                sqlPage.SelectCondtion.Add("DateLower", startTime);
            }
            if (!string.IsNullOrEmpty(Request.Params["endTime"]) && TypeHelper.isDateTime(Request.Params["endTime"]))
            {
                DateTime endTime = TypeHelper.parseDateTime(Request.Params["endTime"]);
                sqlPage.SelectCondtion.Add("DateUpper", endTime);
            }
            if (!string.IsNullOrEmpty(Request.Params["SpecId"]))
            {
                int SpecId = TypeHelper.parseInt(Request.Params["SpecId"]);
                sqlPage.SelectCondtion.Add("ExchSpecID", SpecId);
            }
            if (!string.IsNullOrEmpty(Request.Params["projID"]))
            {
                int projId = TypeHelper.parseInt(Request.Params["projID"]);
                sqlPage.SelectCondtion.Add("ProjId", projId);
            }
            if (!string.IsNullOrEmpty(Request.Params["taskGroupId"]))
            {
                int taskGroupId = TypeHelper.parseInt(Request.Params["taskGroupId"]);
                sqlPage.SelectCondtion.Add("taskGroupId", taskGroupId);
            }
            var RecList = rec.GetList(p => p.ExchId != 0).ToList();
            Expression<Func<DataModel.Models.DesExch, bool>> func = op.GetFunc(sqlPage);//关联时候的分页
            int taskId = TypeHelper.parseInt(Request.Params["taskId"]);
            if (string.IsNullOrEmpty(Request.Params["IsProcess"]))
            {
                func = func.And(p => p.ExchEmpId == userInfo.EmpID || p.ExchEmpId == 0);
                if (taskId != 0)
                {
                    func = func.And(p => p.ExchTaskId == taskId || p.ExchTaskId == 0);
                }
                else
                {
                    func = func.And(p => p.ExchTaskId == 0);
                }

            }
            if (string.IsNullOrEmpty(Request.Params["IsAll"]))
            {
                func = func.And(p => p.ExchIsInvalid == true);//显示有效信息
            }
            var list = from exch in op.GetList(func)
                       join recItem in RecList on exch.Id equals recItem.ExchId into leftTarget
                       from gcItem in leftTarget.DefaultIfEmpty()//表示外联结
                       let proj = project.Get(exch.ProjId)
                       orderby exch.Id descending
                       select new
                       {
                           exch.Id,
                           SpecIdent = exch.Id,//专业标识合并单元格使用
                           EmpIdent = exch.Id,
                           dateIdent = exch.Id,
                           exch.ExchIsInvalid,
                           exch.ExchEmpId,
                           exch.ExchEmpName,
                           exch.ExchTitle,
                           exch.ExchSpecId,
                           exch.ExchSpecName,
                           exch.CreationTime,
                           RecSpecId = gcItem == null ? 0 : gcItem.RecSpecId,
                           RecSpecName = gcItem == null ? "" : gcItem.RecSpecName,
                           RecEmpId = gcItem == null ? 0 : gcItem.RecEmpId,
                           RecEmpName = gcItem == null ? "" : gcItem.RecEmpName,
                           ExcLastPutTime = gcItem == null ? new DateTime(1900, 1, 1) : gcItem.ExcLastPutTime,
                           RecTime = gcItem == null ? new DateTime(1900, 1, 1) : gcItem.RecTime,
                           ProjNumber = proj == null ? "" : proj.ProjNumber,
                           ProjName = proj == null ? "" : proj.ProjName,
                           Phasename = GetPhaseNameByTaskgroupId(exch.taskGroupId),
                           State = (gcItem == null ? "未处理" : (gcItem.RecStatus == 0 ? "未处理" : (gcItem.RecStatus == 1 ? "回退" : "通过"))),
                           RecContent = gcItem == null ? "" : gcItem.RecContent,//处理意见
                           NumberIdent = exch.Id,
                           projIdent = exch.Id,
                           process = exch.Id,
                           count = new DBSql.PubFlow.Flow().GetList(p => p.FlowRefID == exch.Id && p.FlowRefTable == "DesExch").Count(),
                           recCount = op.DbContext.Set<DataModel.Models.DesExchRec>().Where(p => p.ExchId == exch.Id && p.RecStatus == 1).Count()
                       };

            return JavaScriptSerializerUtil.getJson(new
            {
                total = list.Count(),
                rows = (list.Skip(Record * (PageNum - 1)).Take(Record)).ToList()
            });

        }

        /// <summary>
        /// 展示附件列表信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string DesExchAttachList(int Id)
        {
            var list = op.GetDesTaskAttachData(Id);
            return JavaScriptSerializerUtil.getJson(new
            {
                total = list.Count(),
                rows = list
            });

        }

        /// <summary>
        /// 通过taskGroupId获取阶段名称
        /// </summary>
        /// <param name="taskGroupId"></param>
        /// <returns></returns>
        public string GetPhaseNameByTaskgroupId(int taskGroupId)
        {
            string PhaseName = string.Empty;
            DataModel.Models.DesTask task = desTask.FirstOrDefault(p => p.TaskGroupId == taskGroupId && p.TaskType == 1);
            if (task != null)
            {
                DataModel.Models.BaseData model = data.Get(task.TaskPhaseId);
                if (model != null)
                {
                    PhaseName = model.BaseName;
                }
            }
            return PhaseName;
        }

        /// <summary>
        /// 专业提资记录列表
        /// </summary>
        /// <returns></returns>
        [Description("专业提资记录列表")]
        public ActionResult DesSpecExchList()
        {
            var divid = Common.ModelConvert.ConvertToDefaultType<string>(Request.Params["divid"]); // 所在对话框ID
            ViewBag._DialogId = divid;
            ViewBag.taskId = Request.Params["taskId"] == null ? "0" : Request.Params["taskId"].ToString();
            if (ViewBag.taskId != "0")
            {
                DataModel.Models.DesTask _TaskModel = new DBSql.Design.DesTask().Get(Convert.ToInt32(ViewBag.taskId));
                ViewBag.TaskModel = _TaskModel;
            }
            return View();
        }


        /// <summary>
        /// 专业提资记录列表 查询所有项目
        /// </summary>
        /// <returns></returns>
        [Description("专业提资记录列表 查询所有项目")]
        public ActionResult IsoExchList()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("IsoExchList")));
            return View();
        }



        /// <summary>
        /// 项目提资列表信息
        /// </summary>
        [Description("项目提资列表信息")]
        [HttpPost]
        public string ProjExchList()
        {
            Common.SqlPageInfo sqlPage = new Common.SqlPageInfo();
            int PageNum = TypeHelper.parseInt(Request.Params["page"], 20);
            int Record = TypeHelper.parseInt(Request.Params["rows"], 1);

            if (!string.IsNullOrEmpty(Request.Params["KeyText"]))
            {
                string KeyText = Request.Params["KeyText"];
                sqlPage.TextCondtion = KeyText;
            }
            if (!string.IsNullOrEmpty(Request.Params["startTime"]) && TypeHelper.isDateTime(Request.Params["startTime"]))
            {
                DateTime startTime = TypeHelper.parseDateTime(Request.Params["startTime"]);
                sqlPage.SelectCondtion.Add("DateLower", startTime);
            }
            if (!string.IsNullOrEmpty(Request.Params["endTime"]) && TypeHelper.isDateTime(Request.Params["endTime"]))
            {
                DateTime endTime = TypeHelper.parseDateTime(Request.Params["endTime"]);
                sqlPage.SelectCondtion.Add("DateUpper", endTime);
            }
            if (!string.IsNullOrEmpty(Request.Params["SpecId"]))
            {
                string[] SpecId = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<string[]>(Request.Params["SpecId"].ToString());
                sqlPage.SelectCondtion.Add("ExchSpecID", string.Join(",", SpecId));

            }
            var RecList = rec.GetList(p => p.ExchId != 0).ToList();
            Expression<Func<DataModel.Models.DesExch, bool>> func = op.GetFunc(sqlPage);//关联时候的分页
            List<string> permission = PermissionList("IsoExchList");
            List<int> SpecList = op.QualificationSpec((int)DataModel.NodeType.专业, userInfo);//当前人员负责的专业
            if (!permission.Contains("allview") && !permission.Contains("dep"))
            {
                if (SpecList.Count > 0)
                {
                    func = func.And(p => p.ExchEmpId == userInfo.EmpID || (SpecList.Contains(p.ExchSpecId) && p.ExchEmpId == 0));//查看自己的和没有指定提资人的（专业负责人）
                }
                else
                {
                    func = func.And(p => p.ExchEmpId == userInfo.EmpID);//只能查看自己的
                }

            }
            else
            {
                if (!permission.Contains("allview") && permission.Contains("dep"))
                {
                    if (SpecList.Count > 0)
                    {
                        func = func.And(p => p.ExchEmpDepId == userInfo.EmpDepID || (SpecList.Contains(p.ExchSpecId) && p.ExchEmpDepId == 0));//查看自己的和没有指定提资人的（专业负责人）
                    }
                    else
                    {
                        func = func.And(p => p.ExchEmpDepId == userInfo.EmpDepID);//
                    }

                }
            }
            if (!string.IsNullOrEmpty(Request.Params["IsAll"]))
            {
                if (Request.Params["IsAll"].ToString() == "new")
                {
                    func = func.And(p => p.ExchIsInvalid == true);//显示有效信息
                }
            }
            var list = from exch in op.GetList(func)
                       join recItem in RecList on exch.Id equals recItem.ExchId into leftTarget
                       from gcItem in leftTarget.DefaultIfEmpty()//表示外联结
                       let proj = project.Get(exch.ProjId)
                       orderby exch.Id descending
                       select new
                       {
                           exch.Id,
                           SpecIdent = exch.Id,//专业标识合并单元格使用
                           EmpIdent = exch.Id,
                           dateIdent = exch.Id,
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
                           RecTime = gcItem == null ? new DateTime(1900, 1, 1) : gcItem.RecTime,
                           ProjNumber = proj == null ? "" : proj.ProjNumber,
                           ProjName = proj == null ? "" : proj.ProjName,
                           Phasename = GetPhaseNameByTaskgroupId(exch.taskGroupId),
                           NumberIdent = exch.Id,
                           projIdent = exch.Id,
                           process = exch.Id,
                           count = new DBSql.PubFlow.Flow().GetList(p => p.FlowRefID == exch.Id && p.FlowRefTable == "DesExch").Count(),
                           CreationTime = new DBSql.PubFlow.Flow().GetList(p => p.FlowRefID == exch.Id && p.FlowRefTable == "DesExch").Select(p => p.CreationTime.ToString("yyyy-MM-dd hh:mm:ss")),
                           exch.ExchIsInvalid
                       };
            return JavaScriptSerializerUtil.getJson(new
            {
                total = list.Count(),
                rows = (list.Skip(Record * (PageNum - 1)).Take(Record)).ToList()
            });

        }


        /// <summary>
        /// 专业间互提资料交接单
        /// </summary>
        /// <returns></returns>
        [Description("专业间互提资料交接单")]
        public ActionResult IsoExchangeInfo()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjectCenterList")));
            if (ViewBag.permission.Contains("allDown"))
            {
                ViewBag.Permission = 1;
            }
            else
            {
                ViewBag.Permission = 0;
            }

            ViewBag.desExchId = Request.QueryString["Id"] == null ? "0" : Request.QueryString["Id"].ToString();
            int recSpecId = Request.QueryString["RecSpecId"] == null ? 0 :Convert.ToInt32(Request.QueryString["RecSpecId"].ToString());
            if (ViewBag.desExchId != "0")
            {
                DataModel.Models.DesExch _DesExchModel = new DBSql.Design.DesExch().Get(Convert.ToInt32(ViewBag.desExchId));
                ViewBag.DesExchModel = _DesExchModel;
                if (_DesExchModel != null)
                {
                    var projModel = new DBSql.Project.Project().Get(Convert.ToInt32(_DesExchModel.ProjId));
                    if (projModel != null)
                    {
                        ViewBag.ProjName = _DesExchModel.ProjId == 0 ? "" : projModel.ProjName;
                        ViewBag.ProjNumber = _DesExchModel.ProjId == 0 ? "" : projModel.ProjNumber;
                    }
                    else
                    {
                        ViewBag.ProjName = "";
                        ViewBag.ProjNumber = "";
                    }

                    ViewBag.ExchPhaseIds = _DesExchModel.taskGroupId == 0 ? PhaseName(_DesExchModel.ProjId) : GetPhaseNameByTaskgroupId(_DesExchModel.taskGroupId);//
                    ViewBag.ExchPriority = _DesExchModel.ExchPriority == 0 ? "普通" : _DesExchModel.ExchPriority == 1 ? "重要" : "紧急";//优先级别;

                    ViewBag.CreationTime = new DBSql.PubFlow.Flow().GetList(p => p.FlowRefID == _DesExchModel.Id && p.FlowRefTable == "DesExch").Select(p => p.CreationTime.ToString("yyyy-MM-dd")).FirstOrDefault();

                    int taskId = Convert.ToInt32(_DesExchModel.ExchTaskId);
                    DataTable dt = new DBSql.Design.DesTask().GetSpecHeaderByTaskId(taskId);
                    if (dt.Rows.Count > 0)
                    {
                        ViewBag.TGSpecHeader = dt.Rows[0]["TaskEmpName"].ToString();
                    }
                    else
                    {
                        ViewBag.TGSpecHeader = "";
                    }

                    if (recSpecId != 0)
                    {
                        DataModel.Models.DesTask taskModel = new DBSql.Design.DesTask().GetList(p => p.TaskGroupId == _DesExchModel.taskGroupId && p.TaskSpecId == recSpecId && p.TaskType == 1).FirstOrDefault();
                        if (taskModel != null)
                        {
                            ViewBag.JSSpecHeader = taskModel.TaskEmpName;
                        }
                        else
                        {
                            ViewBag.JSSpecHeader = "";
                        }
                    }
                    else
                    {
                        ViewBag.JSSpecHeader = "";
                    }
                    DataModel.Models.DesTask task = new DBSql.Design.DesTask().Get(_DesExchModel.ExchTaskId);
                    GetFlowName(task);
                }
                ViewBag.TableNumber = "SCX02-11";
            }
            return View();
        }

        public void GetFlowName(DataModel.Models.DesTask model)
        {
            if (model != null)
            {
                var xDoc = XDocument.Parse(model.TaskFlowModel);
                var a = xDoc.Element("root").Elements();
                var list = from item in xDoc.Element("root").Elements()
                           select new DBSql.Design.Dto.DesFlowNodeXmlInput()
                           {
                               rownum = item.Attribute("rownum") == null ? "" : item.Attribute("rownum").Value,
                               ID = item.Attribute("ID") == null ? "" : item.Attribute("ID").Value,
                               FlowNodeName = item.Attribute("FlowNodeName") == null ? "" : item.Attribute("FlowNodeName").Value,
                               FlowNodeTypeID = item.Attribute("FlowNodeTypeID") == null ? "" : item.Attribute("FlowNodeTypeID").Value,
                               FlowNodeEmpName = item.Attribute("FlowNodeEmpName") == null ? "" : item.Attribute("FlowNodeEmpName").Value,
                               FlowNodeEmpID = item.Attribute("FlowNodeEmpID") == null ? "" : item.Attribute("FlowNodeEmpID").Value,
                               FlowNodeFinishTime = item.Attribute("FlowNodeFinishTime") == null ? "" : item.Attribute("FlowNodeFinishTime").Value
                           };
                StringBuilder html = new StringBuilder();
                if (model.TaskStatus == 3)
                {
                    var empName = list.Where(p => p.FlowNodeTypeID == "20").FirstOrDefault();
                    if (empName != null)
                    {
                        ViewBag.JHR = empName.FlowNodeEmpName;
                        ViewBag.JHTime = Common.Tools.FormatTime(empName.FlowNodeFinishTime, "yyyy-MM-dd", false);
                    }
                    else
                    {
                        ViewBag.JHR = "";
                        ViewBag.JHTime ="";
                    }
                    empName = list.Where(p => p.FlowNodeTypeID == "21").FirstOrDefault();
                    if (empName != null)
                    {
                        ViewBag.SHR = empName.FlowNodeEmpName;
                        ViewBag.SHTime = Common.Tools.FormatTime(empName.FlowNodeFinishTime, "yyyy-MM-dd", false);
                    }
                    else
                    {
                        ViewBag.SHR = "";
                        ViewBag.SHTime = "";
                    }
                    empName = list.Where(p => p.FlowNodeTypeID == "22").FirstOrDefault();
                    if (empName != null)
                    {
                        ViewBag.SDR = empName.FlowNodeEmpName;
                        ViewBag.SDTime = Common.Tools.FormatTime(empName.FlowNodeFinishTime, "yyyy-MM-dd", false);
                    }
                    else
                    {
                        DataModel.Models.DesTask hz = new DBSql.Design.DesTask().GetQuery(p => p.Id == model.TaskParentId).FirstOrDefault();
                        if (hz != null)
                        {
                            xDoc = XDocument.Parse(hz.TaskFlowModel);
                            a = xDoc.Element("root").Elements();
                            list = from item in xDoc.Element("root").Elements()
                                       select new DBSql.Design.Dto.DesFlowNodeXmlInput()
                                       {
                                           rownum = item.Attribute("rownum") == null ? "" : item.Attribute("rownum").Value,
                                           ID = item.Attribute("ID") == null ? "" : item.Attribute("ID").Value,
                                           FlowNodeName = item.Attribute("FlowNodeName") == null ? "" : item.Attribute("FlowNodeName").Value,
                                           FlowNodeTypeID = item.Attribute("FlowNodeTypeID") == null ? "" : item.Attribute("FlowNodeTypeID").Value,
                                           FlowNodeEmpName = item.Attribute("FlowNodeEmpName") == null ? "" : item.Attribute("FlowNodeEmpName").Value,
                                           FlowNodeEmpID = item.Attribute("FlowNodeEmpID") == null ? "" : item.Attribute("FlowNodeEmpID").Value,
                                           FlowNodeFinishTime = item.Attribute("FlowNodeFinishTime") == null ? "" : item.Attribute("FlowNodeFinishTime").Value
                                       };
                            empName = list.Where(p => p.FlowNodeTypeID == "22").FirstOrDefault();
                            if (empName != null)
                            {
                                ViewBag.SDR = empName.FlowNodeEmpName;
                                ViewBag.SDTime = Common.Tools.FormatTime(empName.FlowNodeFinishTime, "yyyy-MM-dd", false);
                            }
                            else
                            {
                                ViewBag.SDR = list.FirstOrDefault().FlowNodeName;
                                ViewBag.SDTime = Common.Tools.FormatTime(list.FirstOrDefault().FlowNodeFinishTime, "yyyy-MM-dd", false);
                            }
                        }
                        else
                        {
                            ViewBag.SDR = "";
                            ViewBag.SDTime = "";
                        }
                    }
                }
                else
                {
                    ViewBag.JHR = "";
                    ViewBag.JHTime = "";
                    ViewBag.SHR = "";
                    ViewBag.SHTime = "";
                    ViewBag.SDR = "";
                    ViewBag.SDTime = "";
                }
            }
            else
            {
                ViewBag.JHR = "";
                ViewBag.JHTime = "";
                ViewBag.SHR = "";
                ViewBag.SHTime = "";
                ViewBag.SDR = "";
                ViewBag.SDTime = "";
            }
        }


        [Description("提资流程进度")]
        public ActionResult ProjectProgressInfoExch()
        {
            return View();
        }

        /// <summary>
        /// 资料重提
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult ReUseExch(int id)
        {
            ViewBag.Priority = ExchPriorityBind();//提资优先级
            DataModel.Models.DesExch desExchModel = op.Get(id);
            DataModel.Models.DesExchRec desRecModel = rec.FirstOrDefault(p => p.ExchId == id);
            if (desRecModel != null)
            {
                ViewBag.ExcLastPutTime = desRecModel.ExcLastPutTime;
            }
            ViewBag.ReverseIdent = "4";
            ViewBag.RecEmpId = "0";//追加信息的时候使用
            ViewBag.RecSpecId = "0";
            BindProjInfo(desExchModel.ProjId);
            ViewBag.SpecEmps = JavaScriptSerializerUtil.getJson(op.DbContext.Set<DataModel.Models.DesExchRec>().Where(p => p.ExchId == desExchModel.Id).Select(p => new
            {
                Id = 0,
                SpecID = p.RecSpecId,
                SpecName = p.RecSpecName,
                EmpName = p.RecEmpName,
                EmpID = p.RecEmpId,
            }).ToList());

            return View("ExchInfoApprove", GetModel(desExchModel));
        }


        /// <summary>
        /// 反提
        /// </summary>
        /// <param name="ExchId"></param>
        /// <param name="RecId"></param>
        /// <returns></returns>
        public ActionResult ReverseExch(int ExchId, int RecId)
        {
            ViewBag.Priority = ExchPriorityBind();//提资优先级
            DataModel.Models.DesExch desExchModel = op.Get(ExchId);
            DataModel.Models.DesExchRec recModel = rec.Get(RecId);
            DataModel.Models.DesExch newDesExchModel = new DesExch();
            DataModel.Models.DesExchRec newRecModel = new DesExchRec();
            ViewBag.ReverseIdent = Request.Params["ReverseIdent"];//反提标识
            if (desExchModel != null && recModel != null)
            {
                // 交换收资、提资的人员信息
                newDesExchModel.ProjId = desExchModel.ProjId;
                newDesExchModel.taskGroupId = desExchModel.taskGroupId;
                newDesExchModel.ExchEmpId = recModel.RecEmpId;//收资人变提资人
                newDesExchModel.ExchEmpName = recModel.RecEmpName;
                newDesExchModel.ExchSpecId = recModel.RecSpecId;
                newDesExchModel.FlowId = RecId;
                ViewBag.RecEmpId = desExchModel.ExchEmpId;//追加信息的时候使用
                ViewBag.RecSpecId = desExchModel.ExchSpecId;

            }
            BindProjInfo(desExchModel.ProjId);
            ViewBag.State = IsFlow(newDesExchModel.Id);
            ViewBag.SpecEmps = JavaScriptSerializerUtil.getJson(op.DbContext.Set<DataModel.Models.DesExch>().Where(p => p.Id == ExchId).Select(p => new
            {
                Id = 0,
                SpecID = p.ExchSpecId,
                SpecName = p.ExchSpecName,
                EmpName = p.ExchEmpName,
                EmpID = p.ExchEmpId
            }).ToList());

            return View("ExchInfoApprove", newDesExchModel);
        }

        /// <summary>
        /// 对象重新赋值   重提
        /// </summary>
        /// <param name="desExchModel"></param>
        /// <returns></returns>
        public DataModel.Models.DesExch GetModel(DataModel.Models.DesExch desExchModel)
        {
            DataModel.Models.DesExch newModel = new DesExch();
            //newModel.Id = 0;//重置判定为新增

            newModel.ExchTitle = desExchModel.ExchTitle;
            newModel.ExchContent = desExchModel.ExchContent;
            newModel.ProjId = desExchModel.ProjId;
            long taskId = TypeHelper.parseLong(Request.Params["taskId"]);
            newModel.taskGroupId = desExchModel.taskGroupId;
            newModel.ExchSpecId = desExchModel.ExchSpecId;
            newModel.ExchEmpId = desExchModel.ExchEmpId;
            if (taskId != 0)
            {
                newModel.ExchTaskId = taskId;
            }
            else
            {
                newModel.ExchTaskId = desExchModel.ExchTaskId;
            }
            newModel.FlowId = desExchModel.Id;//暂时赋值用于过渡更新信息
            ViewBag.State = IsFlow(newModel.Id);

            return newModel;
        }

        /// <summary>
        /// 催资
        /// </summary>
        /// <param name="ExchId"></param>
        /// <returns></returns>
        public ActionResult UrgenExch(int ExchId)
        {
            int result = 0;
            DataModel.Models.DesExch model = op.Get(ExchId);
            int flowNodeId = TypeHelper.parseInt(Request.Params["flowNodeId"]);
            try
            {
                result = op.SendMessage(model, userInfo, flowNodeId);
                if (result == 1)//给审批人发送消息
                {
                    doResult = DoResultHelper.doSuccess(1, "向审批人发送消息成功");
                }
                else
                {
                    if (result == 2)
                    {
                        doResult = DoResultHelper.doSuccess(1, "待收资的任务在列表中,请及时处理");
                    }
                    else
                    {
                        doResult = DoResultHelper.doSuccess(1, "向提资人发送消息成功");
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
        /// 新增提资
        /// </summary>
        /// <returns></returns>
        public ActionResult AddExchPlan()
        {
            ViewBag.Priority = ExchPriorityBind();//提资优先级
            int taskId = TypeHelper.parseInt(Request.Params["taskId"]);
            DataModel.Models.DesExch desExchModel = new DesExch();
            ViewBag.DefaultNextEmpID = 0;
            ViewBag.DefaultNextEmpName = "";
            if (taskId != 0)
            {
                ViewBag.ReverseIdent = "1";//在后台判断时使用

                DataModel.Models.DesTask desTask = new DBSql.Design.DesTask().Get(taskId);
                if (desTask != null)
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(desTask.TaskFlowModel);
                    var firstNode = (XmlElement)xmlDocument.SelectSingleNode("root/item[@rownum=\"1\"]");
                    if (firstNode != null)
                    {
                        var nexNode = (XmlElement)xmlDocument.SelectSingleNode("root/item[@ID=\"" + firstNode.GetAttribute("FlowNodeNextID") + "\"]");
                        if (nexNode != null)
                        {
                            ViewBag.DefaultNextEmpID = JQ.Util.TypeParse.parse<int>(nexNode.GetAttribute("FlowNodeEmpID"));
                            ViewBag.DefaultNextEmpName = nexNode.GetAttribute("FlowNodeEmpName");
                        }
                    }
                    //获取出
                    desExchModel.ProjId = desTask.ProjId;
                    desExchModel.taskGroupId = (int)desTask.TaskGroupId;
                    desExchModel.ExchSpecId = desTask.TaskSpecId;
                    desExchModel.ExchTaskId = taskId;
                }
            }
            else
            {
                ViewBag.ReverseIdent = "3";//在后台判断时使用
            }
            ViewBag.RecEmpId = "0";//追加信息的时候使用
            ViewBag.RecSpecId = "0";
            //获取收资人
            ViewBag.SpecEmps = JavaScriptSerializerUtil.getJson(op.DbContext.Set<DataModel.Models.DesExchRec>().Where(p => p.ExchId == desExchModel.Id).Select(p => new
            {
                Id = p.Id,
                SpecID = p.RecSpecId,
                SpecName = p.RecSpecName,
                EmpName = p.RecEmpName,
                EmpID = p.RecEmpId,
            }).ToList());

            ViewBag.State = IsFlow(desExchModel.Id);
            desExchModel.ExchEmpId = userInfo.EmpID;//默认为当前用户
            BindProjInfo(desExchModel.ProjId);
            return View("ExchInfoApprove", desExchModel);

        }

        /// <summary>
        /// 绑定项目信息
        /// </summary>
        /// <param name="Id"></param>
        public void BindProjInfo(int Id)
        {
            DataModel.Models.Project proj = project.Get(Id);
            if (proj != null)
            {
                ViewBag.ProjNumber = proj.ProjNumber;
                ViewBag.ProjEmpName = proj.ProjEmpName;
                ViewBag.DatePlanStart = proj.DatePlanStart;
                ViewBag.DatePlanFinish = proj.DatePlanFinish;
            }
            else
            {
                ViewBag.ProjNumber = "";
                ViewBag.ProjEmpName = "";
                ViewBag.DatePlanStart = "1900-1-1";
                ViewBag.DatePlanFinish = "1900-1-1";

            }
        }

        /// <summary>
        /// 索资 页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AskingExch()
        {
            return View();
        }


        /// <summary>
        /// 开始索资料
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public JsonResult PostAskingExch(FormCollection form)
        {
            string title = form["ExchContent"];
            int ExchSpecId = TypeHelper.parseInt(form["ExchSpecId"]);
            int ExchEmpId = TypeHelper.parseInt(form["ExchEmpId"]);
            IEnumerable<DBSql.Sys.EmpQualification> list = new DBSql.Sys.BaseQualification().GetQualificationEmployee(0, ExchSpecId, 0, 0);
            List<int> EmpList = new List<int>();
            if (ExchEmpId != 0)
            {
                EmpList.Add(ExchEmpId);
            }
            else
            {
                EmpList = list.Select(p => p.EmpID).Distinct().ToList();
            }
            try
            {
                int result = op.AskingExch(EmpList, userInfo, title);
                if (result == 0)
                {
                    doResult = DoResultHelper.doSuccess(1, "没有找到发送消息的人员");
                }
                else
                {
                    doResult = DoResultHelper.doSuccess(1, "消息发送成功");
                }
            }
            catch (Exception ex)
            {
                doResult = DoResultHelper.doSuccess(ex.Message);
            }

            return Json(doResult);
        }
        /// <summary>
        /// 下拉筛选
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult getDropList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = "未提资", Value = "0" });
            list.Add(new SelectListItem() { Text = "已提资", Value = "1" });
            return Json(list);
        }

        /// <summary>
        /// 接收状态下拉筛选
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult getRecStateList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = "全部", Value = "-1" });
            list.Add(new SelectListItem() { Text = "未处理", Value = "0" });
            list.Add(new SelectListItem() { Text = "回退", Value = "1" });
            list.Add(new SelectListItem() { Text = "通过", Value = "2" });
            return Json(list);
        }


        /// <summary>
        /// 提资文件 设计文件列表信息
        /// </summary>
        /// <param name="ExchId"></param>
        /// <returns></returns>
        public string ExchAttach(int ExchId)
        {
            try
            {
                var ExchAttachList = op.GetDesTaskAttachVerByExch(ExchId).ToList();
                return JavaScriptSerializerUtil.getJson(new
                {
                    total = ExchAttachList.Count,
                    rows = ExchAttachList
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

        public string DesignAttachByTask(long taskId)
        {
            try
            {
                if (taskId == 0) throw new Exception();

                var listSpecPlan = op.DesignAttachByTask(taskId).ToList();
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
        /// 提资退回重提的列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult BackExchTaskList()
        {
            return View();
        }

        /// <summary>
        /// 退回需要重提的列表信息
        /// </summary>
        /// <returns></returns>
        public string InHandBackExchTaskList()
        {
            Common.SqlPageInfo sqlPage = new Common.SqlPageInfo();
            int PageNum = TypeHelper.parseInt(Request.Params["page"], 1);
            int Record = TypeHelper.parseInt(Request.Params["rows"], 20);

            if (!string.IsNullOrEmpty(Request.Params["KeyText"]))
            {
                string KeyText = Request.Params["KeyText"];
                sqlPage.TextCondtion = KeyText;
            }
            if (!string.IsNullOrEmpty(Request.Params["startTime"]) && TypeHelper.isDateTime(Request.Params["startTime"]))
            {
                DateTime startTime = TypeHelper.parseDateTime(Request.Params["startTime"]);
                sqlPage.SelectCondtion.Add("DateLower", startTime);
            }
            if (!string.IsNullOrEmpty(Request.Params["endTime"]) && TypeHelper.isDateTime(Request.Params["endTime"]))
            {
                DateTime endTime = TypeHelper.parseDateTime(Request.Params["endTime"]);
                sqlPage.SelectCondtion.Add("DateUpper", endTime);
            }
            var RecList = rec.GetList(p => p.ExchId != 0&&p.RecStatus==1).ToList();// 提资退回的状态
            Expression<Func<DataModel.Models.DesExch, bool>> func = op.GetFunc(sqlPage);//关联时候的分页
            int taskId = TypeHelper.parseInt(Request.Params["taskId"]);
            func = func.And(p => p.ExchEmpId == userInfo.EmpID&& p.ExchIsInvalid == true);
            if (taskId != 0)
            {
                func = func.And(p => p.ExchTaskId == taskId);// 仅显示当前设计任务下的退回需要重新提资料
            }
            else
            {
                func = func.And(p => p.ExchTaskId == 0);
            }
            var list = from exch in op.GetList(func)
                       join gcItem in RecList on exch.Id equals gcItem.ExchId
                       let proj = project.Get(exch.ProjId)
                       orderby exch.Id descending
                       select new
                       {
                           exch.Id,
                           SpecIdent = exch.Id,//专业标识合并单元格使用
                           EmpIdent = exch.Id,
                           dateIdent = exch.Id,
                           exch.ExchIsInvalid,
                           exch.ExchEmpId,
                           exch.ExchEmpName,
                           exch.ExchTitle,
                           exch.ExchSpecId,
                           exch.ExchSpecName,
                           exch.CreationTime,
                           RecSpecId = gcItem == null ? 0 : gcItem.RecSpecId,
                           RecSpecName = gcItem == null ? "" : gcItem.RecSpecName,
                           RecEmpId = gcItem == null ? 0 : gcItem.RecEmpId,
                           RecEmpName = gcItem == null ? "" : gcItem.RecEmpName,
                           ExcLastPutTime = gcItem == null ? new DateTime(1900, 1, 1) : gcItem.ExcLastPutTime,
                           RecTime = gcItem == null ? new DateTime(1900, 1, 1) : gcItem.RecTime,
                           ProjNumber = proj == null ? "" : proj.ProjNumber,
                           ProjName = proj == null ? "" : proj.ProjName,
                           Phasename = GetPhaseNameByTaskgroupId(exch.taskGroupId),
                           State = (gcItem == null ? "未处理" : (gcItem.RecStatus == 0 ? "未处理" : (gcItem.RecStatus == 1 ? "回退" : "通过"))),
                           RecContent = gcItem == null ? "" : gcItem.RecContent,//处理意见
                           NumberIdent = exch.Id,
                           projIdent = exch.Id,
                           process = exch.Id,
                           count = new DBSql.PubFlow.Flow().GetList(p => p.FlowRefID == exch.Id && p.FlowRefTable == "DesExch").Count(),
                           recCount = op.DbContext.Set<DataModel.Models.DesExchRec>().Where(p => p.ExchId == exch.Id && p.RecStatus == 1).Count()
                       };

            return JavaScriptSerializerUtil.getJson(new
            {
                total = list.Count(),
                rows = (list.Skip(Record * (PageNum - 1)).Take(Record)).ToList()
            });

        }

        /// <summary>
        /// 判断提资是否已经提交 （不考虑回退状态）
        /// </summary>
        /// <param name="ExchId"></param>
        /// <returns></returns>
        public int IsFlow(int ExchId)
        {
            int Count = new DBSql.PubFlow.Flow().GetList(p => p.FlowRefTable == "DesExch" && p.FlowRefID == ExchId).Count();
            return Count;
        }

        public string PhaseName(int projId)
        {
            DataModel.Models.Project project = new DBSql.Project.Project().Get(projId);
            string PhaseName = string.Empty;
            if (project != null)
            {
                PhaseName = string.Join(",", GetBaseName(project.ProjPhaseIds));
            }
            return PhaseName;
        }


    }

    //需要过渡类
    public class TempExchRec : DataModel.Models.DesExch
    {
        public string RecEmpId { get; set; }
        public int RecSpecId { get; set; }
        public string RecSpecName { get; set; }
        public string RecEmpName { get; set; }
        public string PhaseName { get; set; }
        /// <summary>
        /// 对应多个收资专业
        /// </summary>
        public string RecSpecIds { get; set; }
        public TempExchRec()
        {
            RecEmpId = "0";
            RecEmpName = "";
            RecSpecId = 0;
            RecSpecName = "";
            PhaseName = "";
            RecSpecIds = "";
        }
    }
}
