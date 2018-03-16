using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DBSql.PubFlow
{
    public class FlowModelNodeHtml
    {
        #region 定义变量
        private string[,] array = new string[999, 999];
        private int allRows;
        private int allCols;
        public string ImgPath { set; get; }
        #endregion

        #region 获取流程节点图
        public string GetNodeHtml(int flowModelId)
        {
            var list = new FlowModelNode().GetList(m => m.FlowModelID == flowModelId).ToList();
            GetNodeTree(0, 0, list);
            AutoFill();
            return GetTable();
        }
        private void GetNodeTree(int parentId, int colIndex, List<DataModel.Models.FlowModelNode> list)
        {
            if (allCols < colIndex)
            {
                allCols = colIndex;
            }
            var nList = (from l in list
                         where l.NodeParentID == parentId
                         select l).ToList();
            if (nList.Count == 0)
            {
                return;
            }
            for (int i = 1; i < nList.Count + 1; i++)
            {
                var imgName = "";
                var node = nList[i - 1];
                //如果是开始节点
                if (node.NodeParentID == 0)
                {
                }
                else
                {
                    //当前节点是否有兄弟节点

                    //当前节点没有兄弟节点 添加 “─”
                    if (nList.Count == 1)
                    {
                        imgName = "we.gif";
                    }
                    else
                    {
                        //当前节点有兄弟节点
                        if (i == 1)   //第一个 添加“┬”
                        {
                            imgName = "nse.gif";
                        }
                        else if (i == list.Count) //当前节点是最后一个，添加“└”
                        {
                            imgName = "ne.gif";
                        }
                        else    //当前节点是中间节点，添加“├”
                        {
                            imgName = "nse.gif";
                        }
                    }
                    array[allRows, colIndex - 1] = string.Format("<td><img src=\"{0}{1}\" /></td>", ImgPath, imgName);
                }
                array[allRows, colIndex] = string.Format("<th data=\"{0}_{1}\">{2}</th>", node.Id, node.NodeParentID, node.NodeName);
                GetNodeTree(node.Id, colIndex + 2, list);
                if (nList.Count > i)
                {
                    allRows++;
                }
            }
        }
        private void AutoFill()
        {
            var str = "";
            bool needFill = false;
            for (int i = 0; i < this.allCols; i++)      //循环所有列
            {
                needFill = false;
                for (int j = 0; j < this.allRows; j++)  //循环所有行
                {
                    str = array[j, i];
                    if (str != null && (str.IndexOf("/swe.gif") >= 0 || str.IndexOf("/nse.gif") >= 0))
                    {
                        needFill = true;
                    }
                    else if (str != null && (str.IndexOf("/ne.gif") >= 0))
                    {
                        needFill = false;
                    }
                    else if (str == null || str.Equals(""))
                    {
                        if (needFill)
                        {
                            //填充“│”
                            array[j, i] = string.Format("<td><img src=\"{0},{1}\" /></td>", ImgPath, "ns.gif");
                        }
                    }
                }
            }
        }
        private string GetTable()
        {
            var sb = new StringBuilder();
            for (int i = 0; i <= allRows; i++)
            {
                sb.Append("<tr>");
                for (int j = 0; j <= allCols; j++)
                {
                    var str = array[i, j];
                    if (str == null || str.Length == 0)
                    {
                        sb.Append("<td>&nbsp;</td>");
                    }
                    else
                    {
                        sb.Append(str);
                    }
                }
                sb.Append("</tr>");
            }
            var table = string.Format("<table class=\"table1\">{0}</table>", sb.ToString());
            return table;
        }
        #endregion
    }


    public class FlowNodeHtml : IDisposable
    {
        public FlowNodeHtml(int flowID)
        {
            this.FlowID = flowID;
        }
        private DateTime DefaultDateTime = DateTime.Parse("1900-01-01");
        private DAL.JQPM9_DefaultContext _dbContext;
        private DAL.JQPM9_DefaultContext DbContext
        {
            get
            {
                if (_dbContext == null)
                {
                    _dbContext = new DAL.JQPM9_DefaultContext();
                }
                return _dbContext;
            }
        }

        private int FlowID
        {
            get; set;
        }
        public string ImagePath
        {
            get; set;
        }
        #region 定义变量
        private string[,] array = new string[999, 999];
        private int allRows;
        private int allCols;
        #endregion

        #region 获取流程节点图
        public string GetNodeHtml()
        {
            List<DataModel.Models.FlowNode> NodeList = this.DbContext.FlowNodes.Where(m => m.FlowID == FlowID).ToList();
            DataModel.Models.Flow FlowModel = new DBSql.PubFlow.Flow().Get(FlowID);
            if (FlowModel != null)
            {

                switch (FlowModel.FlowRefTable)
                {
                    case "DesMutiSign":
                        #region 会签
                        var lastNode = NodeList.OrderBy(p => p.FlowNodeOrder).LastOrDefault();
                        if (lastNode != null)
                        {
                            List<DataModel.Models.DesMutiSignRec> RecList = this.DbContext.Set<DataModel.Models.DesMutiSignRec>().Where(p => p.MutiSignId == FlowModel.FlowRefID).ToList();
                            foreach (var item in RecList)
                            {
                                NodeList.Add(new DataModel.Models.FlowNode()
                                {
                                    Id = -1,
                                    FlowID = FlowModel.Id,
                                    FlowNodeName = "会签人  " + (item.RecStatus == 0 ? "" : (item.RecStatus == 1 ? "[ 不同意 ]" : "[ 同意 ]")) + "",
                                    FlowNodeParentID = lastNode.Id,
                                    FlowNodeOrder = lastNode.FlowNodeOrder + 1,
                                    FlowNodeEmpId = item.RecEmpId,
                                    FlowNodeEmpName = item.RecEmpName,
                                    FlowNodeStatusID = FlowModel.FlowStatusID == 3 ? (item.RecStatus == 0 ? (int)DataModel.NodeStatus.轮到 : (int)DataModel.NodeStatus.完成) : 0,
                                    FlowNodeNote = item.RecStatus == 0 ? "" : (item.RecStatus == 1 ? "不同意" : "同意"),
                                    FlowNodeDate = item.RecDealDate
                                });
                            }
                        }
                        break;
                    #endregion
                    case "DesExch":
                        #region 提资
                        var LastNode = NodeList.OrderBy(p => p.FlowNodeOrder).LastOrDefault();
                        if (LastNode != null)
                        {
                            List<DataModel.Models.DesExchRec> RecList = this.DbContext.Set<DataModel.Models.DesExchRec>().Where(p => p.ExchId == FlowModel.FlowRefID && p.RecEmpId != 0).ToList();
                            foreach (var item in RecList)
                            {
                                DataModel.Models.FlowNode model = new DataModel.Models.FlowNode();
                                model.Id = -1;
                                model.FlowID = FlowModel.Id;
                                model.FlowNodeName = item.RecSpecName;
                                model.FlowNodeParentID = LastNode.Id;
                                model.FlowNodeOrder = LastNode.FlowNodeOrder + 1;
                                model.FlowNodeEmpId = item.RecEmpId;
                                model.FlowNodeEmpName = item.RecEmpName;
                                model.FlowNodeNote = item.RecStatus == 0 ? "" : (item.RecStatus == 1 ? "不同意" : "同意");
                                model.FlowNodeDate = item.RecTime;
                                if (LastNode.FlowNodeStatusID == (int)DataModel.NodeStatus.完成)
                                {
                                    model.FlowNodeStatusID = (int)DataModel.NodeStatus.轮到;
                                }
                                else
                                {
                                    model.FlowNodeStatusID = (int)DataModel.NodeStatus.未安排;
                                }
                                if (item.RecStatus == 1)
                                {
                                    model.FlowNodeStatusID = (int)DataModel.NodeStatus.回退;
                                }
                                if (item.RecStatus == 2)
                                {
                                    model.FlowNodeStatusID = (int)DataModel.NodeStatus.完成;
                                }


                                NodeList.Add(model);

                            }
                        }
                        break;
                    #endregion
                    default:
                        break;
                }


            }
            GetNodeTree(0, 0, NodeList);
            AutoFill();
            return GetTable();
        }
        private void GetNodeTree(int parentId, int colIndex, List<DataModel.Models.FlowNode> list)
        {
            if (allCols < colIndex)
            {
                allCols = colIndex;
            }
            var nList = (from l in list
                         where l.FlowNodeParentID == parentId
                         select l).ToList();
            if (nList.Count == 0)
            {
                return;
            }
            for (int i = 1; i < nList.Count + 1; i++)
            {
                var imgName = "";
                var node = nList[i - 1];
                //如果是开始节点
                if (node.FlowNodeParentID == 0)
                {
                }
                else
                {
                    //当前节点是否有兄弟节点

                    //当前节点没有兄弟节点 添加 “─”
                    if (nList.Count == 1)
                    {
                        imgName = "we.gif";
                    }
                    else
                    {
                        //当前节点有兄弟节点
                        if (i == 1)   //第一个 添加“┬”
                        {
                            //imgName = "nse.gif";
                            imgName = "swe.gif";
                        }
                        else if (i == nList.Count) //当前节点是最后一个，添加“└”
                        {
                            imgName = "ne.gif";
                        }
                        else    //当前节点是中间节点，添加“├”
                        {
                            imgName = "nse.gif";
                        }
                    }
                    array[allRows, colIndex - 1] = string.Format("<td><img src=\"{0}{1}\" /></td>", ImagePath, imgName);
                }
                array[allRows, colIndex] = string.Format("<th data=\"{0}_{1}\" style=\"background-color:" + GetColor(node.FlowNodeStatusID) + "\" title=\"" + GetNodeTitle(node) + "\">{2}</th>", node.Id, node.FlowNodeParentID, FormatName(node));
                GetNodeTree(node.Id, colIndex + 2, list);
                if (nList.Count > i)
                {
                    allRows++;
                }
            }
        }
        private void AutoFill()
        {
            var str = "";
            bool needFill = false;
            for (int i = 0; i < this.allCols; i++)      //循环所有列
            {
                needFill = false;
                for (int j = 0; j < this.allRows; j++)  //循环所有行
                {
                    str = array[j, i];
                    if (str != null && (str.IndexOf("/swe.gif") >= 0 || str.IndexOf("/nse.gif") >= 0))
                    {
                        needFill = true;
                    }
                    else if (str != null && (str.IndexOf("/ne.gif") >= 0))
                    {
                        needFill = false;
                    }
                    else if (str == null || str.Equals(""))
                    {
                        if (needFill)
                        {
                            //填充“│”
                            array[j, i] = string.Format("<td><img src=\"{0},{1}\" /></td>", ImagePath, "ns.gif");
                        }
                    }
                }
            }
        }
        private string GetTable()
        {
            var sb = new StringBuilder();
            for (int i = 0; i <= allRows; i++)
            {
                sb.Append("<tr>");
                for (int j = 0; j <= allCols; j++)
                {
                    var str = array[i, j];
                    if (str == null || str.Length == 0)
                    {
                        sb.Append("<td>&nbsp;</td>");
                    }
                    else
                    {
                        sb.Append(str);
                    }
                }
                sb.Append("</tr>");
            }
            var table = string.Format("<table class=\"table1\" cellSpacing=\"5\">{0}</table>", sb.ToString());
            return table;
        }
        #endregion


        /// <summary>
        /// 根据颜色ID,得到颜色值
        /// </summary>
        /// <param name="colorID"></param>
        /// <returns></returns>
        private string GetColor(int statusID)
        {
            switch (statusID)
            {
                case (int)DataModel.NodeStatus.安排:
                    return "#DDDDDD";
                case (int)DataModel.NodeStatus.轮到:
                    return "#FFFF99";
                case (int)DataModel.NodeStatus.回退:
                    return "#FF9966";
                case (int)DataModel.NodeStatus.完成:
                    return "#66CCFF";
                case (int)DataModel.NodeStatus.未安排:
                    return "#FFFFFF";
                case (int)DataModel.NodeStatus.会签中:
                    return "#81E727";
                case (int)DataModel.NodeStatus.跳过:
                    return "#B6AAFC";
                default:
                    return "#FFFFFF";
            }
        }


        /// <summary>
        /// 格式名称
        /// </summary>
        /// <param name="AllString"></param>
        /// <returns></returns>
        private string FormatName(DataModel.Models.FlowNode flowNode)
        {
            var result = flowNode.FlowNodeName;
            if (flowNode.FlowNodeTypeID == -1)
            {
                result += " 会签";
            }
            result += "<br />" + flowNode.FlowNodeEmpName;
            if (flowNode.FlowNodeDate != this.DefaultDateTime)
            {
                result += " " + flowNode.FlowNodeDate.ToString("yyyy-MM-dd");
            }
            return result;
        }


        /// <summary>
        /// 2008-2-16 黄海文 
        /// 获得节点的Title ToolTip提示信息
        /// </summary>
        /// <param name="node">需要获得信息的节点对象</param>
        /// <returns>title字串</returns>
        private string GetNodeTitle(DataModel.Models.FlowNode node)
        {
            string title = "";
            if (node.FlowNodeTypeID == 0)
            {
                //普通节点
                title += "经办人：" + node.FlowNodeEmpName;
                if (node.FlowNodeDate != this.DefaultDateTime)
                {
                    title += "\r" + node.FlowNodeDate.ToString("yyyy-MM-dd");
                }
            }
            else
            {
                //获取出该节点的会签信息
                foreach (var item in this.DbContext.FlowMultiSigns.Where(m => m.FlowNodeID == node.Id))
                {
                    if (!string.IsNullOrEmpty(title))
                    {
                        title += "\r";
                    }
                    title += "经办人：" + item.SignEmpName;
                    if (item.SignDate != this.DefaultDateTime)
                    {
                        title += " 审批时间：" + item.SignDate.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        title += " 未签";
                    }
                }
            }
            return HttpUtility.HtmlEncode(title);
        }


        private bool _isDisponsed;
        public void Dispose()
        {
            if (_isDisponsed)
            {
                return;
            }
            _isDisponsed = true;
            if (_dbContext != null)
            {
                _dbContext.Dispose();
            }
        }
    }
}
