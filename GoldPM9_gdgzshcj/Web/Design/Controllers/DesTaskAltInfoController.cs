using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;
using System.Collections;

namespace Design.Controllers
{
    [Description("设计图副、技经费用填写")]
    public partial class DesTaskAltInfoController : CoreController
    {
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        DBSql.Design.DesTaskFeeDetails op = new DBSql.Design.DesTaskFeeDetails();

        [Description("图副填写")]
        public ActionResult DesignInfo(int taskId)
        {
            ViewBag.taskId = taskId;
            DBSql.Design.DesTask _db = new DBSql.Design.DesTask();
            DataModel.Models.DesTask dmTask = _db.Get(taskId);
            Hashtable ht = Common.XmlConvert.XmlToHash(dmTask.ColAttXml);
            foreach (DictionaryEntry de in ht)
            {
                ViewData[de.Key.ToString()] = de.Value;
            }
            var divid = Common.ModelConvert.ConvertToDefaultType<string>(Request.Params["divid"]); // 所在对话框ID
            ViewBag._DialogId = divid;
            ViewBag.IsView = Request.Params["View"] == null ? 0 : 1;
            return View();
        }


        public string DesignSave(int taskId)
        {
            DBSql.Design.DesTask _db = new DBSql.Design.DesTask();
            DataModel.Models.DesTask dmTask = _db.Get(taskId);
            if (dmTask == null)
            {
                return JavaScriptSerializerUtil.getJson(DoResultHelper.doSuccess(-1, "缺少关键主键数据！！！"));
            }

            System.Collections.Hashtable ht = new Hashtable();
            foreach (string keyName in Request.Form.AllKeys)
            {
                ht.Add(keyName, Request.Form[keyName]);
            }
            if (ht.Count == 0)
            {
                return JavaScriptSerializerUtil.getJson(DoResultHelper.doSuccess(-1, "缺少关键数据！！！"));
            }
            try
            {
                dmTask.ColAttXml = Common.XmlConvert.HashTableToXml(ht);
                _db.Edit(dmTask);
                _db.UnitOfWork.SaveChanges();
                return JavaScriptSerializerUtil.getJson(DoResultHelper.doSuccess(1, "任务备注保存成功！！！"));
            }
            catch (Exception)
            {
                return JavaScriptSerializerUtil.getJson(DoResultHelper.doSuccess(-1, "任务备注保存失败！！！"));
            }

        }

        [Description("技经填写")]
        public ActionResult FeeDetailInfo(int taskId)
        {
            ViewBag.taskId = taskId;
            DataModel.Models.DesTask dmTask = op.DbContext.Set<DataModel.Models.DesTask>().FirstOrDefault(p => p.Id == taskId);
            DataModel.Models.DesTaskFeeDetails model = op.FirstOrDefault(p => p.TaskGroupId == dmTask.TaskGroupId);

            var divid = Common.ModelConvert.ConvertToDefaultType<string>(Request.Params["divid"]); // 所在对话框ID
            ViewBag._DialogId = divid;
            ViewBag.IsView = Request.Params["View"] == null ? 0 : 1;

            // 读取制图日期
            Hashtable ht = Common.XmlConvert.XmlToHash(dmTask.ColAttXml);
            if (ht.ContainsKey("Design_Date"))
            {
                ViewData["Design_Date"] = ht["Design_Date"].ToString();
            }

            if (model == null)
            {
                model = new DesTaskFeeDetails();
                model.CreatorEmpName = userInfo.EmpName;
                model.CreationTime = System.DateTime.Now;
                return View(model);
            }
            else
            {
                return View(model);
            }

        }


        public string FeeDesignSave(int taskId)
        {
            DataModel.Models.DesTask dmTask = op.DbContext.Set<DataModel.Models.DesTask>().FirstOrDefault(p => p.Id == taskId);
            if (dmTask == null)
            {
                return JavaScriptSerializerUtil.getJson(DoResultHelper.doSuccess(-1, "保存失败！！！"));
            }
            DataModel.Models.DesTaskFeeDetails model = op.FirstOrDefault(p => p.TaskGroupId == dmTask.TaskGroupId);
            try
            {
                //保存制图日期
                Hashtable ht = new Hashtable();
                ht.Add("Design_Date", Request.Form["Design_Date"]);
                dmTask.ColAttXml = Common.XmlConvert.HashTableToXml(ht);
                op.DbContext.Entry<DataModel.Models.DesTask>(dmTask).State = System.Data.Entity.EntityState.Modified;
                op.UnitOfWork.SaveChanges();

                if (model == null)
                {

                    model = new DesTaskFeeDetails();
                    model.MvcSetValue();
                    model.MvcDefaultSave(userInfo.EmpID);
                    model.TaskID = taskId;
                    model.ProjId = dmTask.ProjId;
                    model.TaskGroupId = dmTask.TaskGroupId;
                    model.PhaseID = dmTask.TaskPhaseId;
                    op.Add(model);
                    op.UnitOfWork.SaveChanges();
                    return JavaScriptSerializerUtil.getJson(DoResultHelper.doSuccess(1, "技经费用保存成功！！！"));
                }
                else
                {
                    model.MvcSetValue();
                    model.MvcDefaultEdit(userInfo.EmpID);
                    op.Edit(model);
                    op.UnitOfWork.SaveChanges();
                    return JavaScriptSerializerUtil.getJson(DoResultHelper.doSuccess(1, "技经费用保存成功！！！"));
                }
            }
            catch (Exception ex)
            {

                return JavaScriptSerializerUtil.getJson(DoResultHelper.doSuccess(-1, "保存失败！！！"));
            }

        }

    }
}
