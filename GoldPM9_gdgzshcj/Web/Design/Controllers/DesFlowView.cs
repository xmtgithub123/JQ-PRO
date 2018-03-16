using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
namespace Design.Controllers
{
    public partial class DesFlowController : CoreController
    {
        [Description("转到 生产流程列表 页面")]
        public ActionResult FlowList()
        {
            return View();
        }

        [Description("转到 生成流程详情 页面")]
        public ActionResult FlowInfo(int ID = 0)
        {
            DataModel.Models.DesFlow model = null;
            if (ID == 0)
            {
                model = new DesFlow();
            }
            else
            {
                model = desFlowDB.Get(ID);
            }

            return View("FlowInfo", model);
        }

        [Description("转到 生成流程节点列表 页面")]
        public ActionResult FlowNodeList(int flowID)
        {
            ViewBag.flowID = flowID;
            return View();
        }

        [Description("转到 生成流程节点详情 页面")]
        public ActionResult FlowNodeOption(int ID)
        {
            DataModel.Models.DesFlowNode model = null;
            if (ID == 0)
            {
                model = new DesFlowNode();
            }
            else
            {
                model = desFlowNodeDB.Get(ID);
            }
            //获取出节点类型 过滤掉专业（黄一鸣）
            ViewBag.NodeTypes = new DBSql.Sys.BaseDataSystem().GetDataSetByEngName("NodeType").Where(m => m.BaseAtt1 == "Qualification" && m.BaseID != 16);
            //获取出可回退节点
            var previousNodes = desFlowNodeDB.GetList(m => m.FlowID == model.FlowID && m.DeleterEmpId == 0 && m.FlowNodeOrderNum < model.FlowNodeOrderNum).OrderBy(m => m.FlowNodeOrderNum).ToList();
            Dictionary<int, string> dic = new Dictionary<int, string>();
            foreach (var item in previousNodes)
            {
                dic.Add(item.ID, item.FlowNodeName);
            }
            ViewBag.PreviousNodes = dic;
            if (model.FlowNodeEmpType == 2)
            {
                //选中人为1
                if (!string.IsNullOrEmpty(model.FlowNodeEmpIDs))
                {
                    ViewBag.EmpNames = new DBSql.Sys.BaseEmployee().GetEmpName(model.FlowNodeEmpIDs);
                }
            }
            return View("FlowNodeOption", model);
        }

    }
}
