using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSql.Design
{
    public class FlowNodeHtml
    {
        private string[,] array = new string[999, 999];
        private int allRows;
        private int allCols;
        public string ImgPath { set; get; }

        public string GetNodeHtml(int flowID)
        {
            var list = new DesFlowNode().GetList(m => m.FlowID == flowID && m.DeleterEmpId == 0).ToList();
            var temp = list.FirstOrDefault(m => m.FlowNodeOrderNum == 1);
            if (temp != null)
            {
                GetNodeTree(temp.ID, 0, list);
                AutoFill();
            }
            return GetTable();
        }
        private void GetNodeTree(int nodeID, int colIndex, List<DataModel.Models.DesFlowNode> list)
        {
            if (allCols < colIndex)
            {
                allCols = colIndex;
            }
            var nList = (from l in list
                         where l.ID == nodeID
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
                if (node.FlowNodeOrderNum == 1)
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
                array[allRows, colIndex] = string.Format("<th data=\"{0}_{1}\">{2}</th>", node.ID, node.FlowNodeNextID, node.FlowNodeName);
                GetNodeTree(node.FlowNodeNextID, colIndex + 2, list);
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
    }
}