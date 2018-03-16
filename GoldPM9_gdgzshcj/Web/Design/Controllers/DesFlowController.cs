using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System;

namespace Design.Controllers
{
    [Description("DesFlow")]
    public partial class DesFlowController : CoreController
    {
        private DBSql.Design.DesFlow desFlowDB = new DBSql.Design.DesFlow();
        private DBSql.Design.DesFlowNode desFlowNodeDB = new DBSql.Design.DesFlowNode();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        /// <summary>
        /// 获取 生产流程列表 数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string FlowListGetListJson()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "ID desc";
            }
            var list = desFlowDB.GetPagedList(PageModel).ToList().Select(m => new { m.ID, m.FlowName, m.CreatorEmpName, m.LastModificationTime, FlowSpecTypeText = GetFlowSpecTypeText(m.FlowSpecType), FlowLevelTypeText = GetFlowLevelTypeText(m.FlowLevelType) });
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }

        private string GetFlowSpecTypeText(int flowSpecType)
        {
            switch (flowSpecType)
            {
                case 0:
                    return "普通专业";
                case 1:
                    return "汇总专业";
                default:
                    return "";
            }
        }

        private string GetFlowLevelTypeText(int flowLevelType)
        {
            switch (flowLevelType)
            {
                case 0:
                    return "无层级";
                case 1:
                    return "子层级";
                case 2:
                    return "父层级";
                default:
                    return "";
            }
        }

        /// <summary>
        ///  保存 生成流程详情 数据
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult FlowListSaveInfo(int ID)
        {
            var model = new DesFlow();
            if (ID > 0)
            {
                model = desFlowDB.Get(ID);
            }
            model.MvcSetValue();
            int reuslt = 0;
            if (model.ID > 0)
            {
                model.MvcDefaultEdit(userInfo.EmpID);
            }
            else
            {
                model.MvcDefaultSave(userInfo);
            }
            desFlowDB.CreateOrUpdate(model, userInfo);
            reuslt = model.ID;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.ID, "操作成功") : DoResultHelper.doSuccess(model.ID, "操作失败");
            return Json(dr);
        }

        /// <summary>
        /// 删除 生产流程详情 数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult FlowListDelInfo(int[] id)
        {
            int reuslt = 0;
            desFlowDB.Delete(s => id.Contains(s.ID));
            desFlowDB.UnitOfWork.SaveChanges();
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }

        /// <summary>
        /// 获取 生成流程节点列表 数据
        /// </summary>
        /// <param name="flowID"></param>
        /// <returns></returns>
        [HttpPost]
        public string FlowNodeListGetListJson(int flowID)
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "ID desc";
            }
            var list = desFlowNodeDB.GetPagedList(x => x.FlowID == flowID, PageModel).ToList();

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }

        /// <summary>
        /// 保存 生产流程节点详情 数据
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult FlowNodeSaveInfo(int ID)
        {
            var model = new DesFlowNode();
            if (ID > 0)
            {
                model = desFlowNodeDB.Get(ID);
            }
            model.MvcSetValue();

            int reuslt = 0;
            if (model.ID > 0)
            {
                desFlowNodeDB.Edit(model);
            }
            else
            {
                desFlowNodeDB.Add(model);
            }
            desFlowNodeDB.UnitOfWork.SaveChanges();
            reuslt = model.ID;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.ID, "操作成功") : DoResultHelper.doSuccess(model.ID, "操作失败");
            return Json(dr);
        }

        /// <summary>
        /// 删除 生产流程节点详情 数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult FlowNodeDelInfo(int[] id)
        {
            int reuslt = 0;
            desFlowNodeDB.Delete(s => id.Contains(s.ID));
            desFlowNodeDB.UnitOfWork.SaveChanges();
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }

        public string GetNodesHtml()
        {
            var flowID = Common.ModelConvert.ConvertToDefault<int>(Request.Form["flowID"]);
            if (flowID == 0)
            {
                return "";
            }
            var dsHtml = new DBSql.Design.FlowNodeHtml();
            dsHtml.ImgPath = Url.Content("~/Content/Images/Flow/");
            return dsHtml.GetNodeHtml(flowID);
        }

        /// <summary>
        /// 删除流程
        /// </summary>
        /// <returns></returns>
        public JsonResult Delete(int[] id)
        {
            var result = desFlowDB.Delete(id);
            return Json(result ? DoResultHelper.doSuccess(result, "操作成功") : DoResultHelper.doError("操作失败"));
        }

        /// <summary>
        /// 追加节点（包含添加下级和上级）
        /// </summary>
        /// <returns></returns>
        public JsonResult AppendNode()
        {
            var nodeID = Common.ModelConvert.ConvertToDefault<int>(Request.Form["nodeID"]);
            if (nodeID == 0)
            {
                return Json(new { Result = false });
            }
            var mode = Request.Form["mode"] ?? "";
            var flowModelID = 0;
            if (mode == "next")
            {
                flowModelID = desFlowNodeDB.AppendNextNode(nodeID, userInfo);
            }
            else if (mode == "previous")
            {
                flowModelID = desFlowNodeDB.AppendPreviousNode(nodeID, userInfo);
            }
            if (flowModelID == 0)
            {
                return Json(new { Result = false });
            }
            var dsHtml = new DBSql.Design.FlowNodeHtml();
            dsHtml.ImgPath = Url.Content("~/Content/Images/Flow/");
            return Json(new { Result = true, HTML = dsHtml.GetNodeHtml(flowModelID) });
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <returns></returns>
        public JsonResult DeleteNode()
        {
            var nodeID = Common.ModelConvert.ConvertToDefault<int>(Request.Form["nodeID"]);
            if (nodeID == 0)
            {
                return Json(new { Result = false, Message = "参数错误" });
            }
            try
            {
                var flowID = desFlowNodeDB.DeleteNode(nodeID, userInfo);
                var dsHtml = new DBSql.Design.FlowNodeHtml();
                dsHtml.ImgPath = Url.Content("~/Content/Images/Flow/");
                return Json(new { Result = true, HTML = dsHtml.GetNodeHtml(flowID) });
            }
            catch (Common.JQException jqe)
            {
                return Json(new { Result = false, Message = jqe.Message });
            }
            catch (Exception)
            {
                return Json(new { Result = false, Message = "未知异常" });
            }
        }

        public JsonResult SaveNode(int Id)
        {
            if (Id == 0)
            {
                return Json(DoResultHelper.doError("参数错误"));
            }
            var node = desFlowNodeDB.Get(Id);
            if (node == null)
            {
                return Json(DoResultHelper.doError("找不到该节点"));
            }
            node.MvcSetValue();
            node.MvcDefaultEdit(userInfo);
            if (node.FlowNodeEmpType == 2)
            {
                node.FlowNodeEmpIDs = Request.Form["FlowNodeEmpType2"] ?? "";
            }
            else if (node.FlowNodeEmpType == 3)
            {
                node.FlowNodeEmpIDs = Request.Form["FlowNodeEmpType3"] ?? "";
            }
            else if (node.FlowNodeEmpType == 4)
            {
                node.FlowNodeEmpIDs = Request.Form["FlowNodeEmpType4"] ?? "";
            }
            desFlowNodeDB.Edit(node);
            desFlowNodeDB.UnitOfWork.SaveChanges();
            return Json(node.ID > 0 ? DoResultHelper.doSuccess(node.ID, "操作成功") : DoResultHelper.doError("操作失败"));
        }
    }
}
