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
    public partial class DesExchMobileController : MobileController
    {
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
        /// 判断提资是否已经提交 （不考虑回退状态）
        /// </summary>
        /// <param name="ExchId"></param>
        /// <returns></returns>
        public int IsFlow(int ExchId)
        {
            int Count = new DBSql.PubFlow.Flow().GetList(p => p.FlowRefTable == "DesExch" && p.FlowRefID == ExchId).Count();
            return Count;
        }


        public ActionResult AddExchPlan()
        {
            ViewBag.Priority = ExchPriorityBind();//提资优先级
            int taskId = TypeHelper.parseInt(Request.Params["taskId"]);
            DataModel.Models.DesExch desExchModel = new DesExch();
           
          //  ViewBag.ExcLastPutTime = DateTime.Now;
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






    }

}
