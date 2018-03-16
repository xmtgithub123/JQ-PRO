using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System;

namespace Iso.Controllers
{

    [Description("校审查询")]
    public class IsoTaskCheckController : CoreController
    {
        private DBSql.Design.DesTaskCheck op = new DBSql.Design.DesTaskCheck();

        private DoResult doResult = DoResultHelper.doError("未知错误!");


        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            return View();
        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string json()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();

            List<string> result = PermissionList("IsoTaskCheckList");
            if (!(result.Contains("allview") || result.Contains("alledit")))
            {
                if (result.Contains("dep"))
                {
                    PageModel.SelectCondtion.Add("QueryDeptID", this.userInfo.EmpDepID);
                }
                else
                {
                    PageModel.SelectCondtion.Add("QueryEmpID", this.userInfo.EmpID);
                }
            }

            var list = op.GetDesTaskCheckList(PageModel);


            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list,
            });
        }


        #endregion


        #region 校审单明细
        public ActionResult desTaskCheckDetail(int taskId, int type)
        {
            DesTask model = new DBSql.Design.DesTask().Get(taskId);
            DataModel.Models.Project dmProj = new DBSql.Project.Project().Get(model.ProjId);
            ViewBag.ProjModel = dmProj;
            if (model.ColAttXml == "")
            {
                ViewBag.Perpage = new System.Collections.Hashtable();
            }
            else
            {
                ViewBag.Perpage = Common.XmlConvert.XmlToHash(model.ColAttXml);
            }


            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (Request.Params["taskId"] != null)
            {
                PageModel.SelectCondtion.Add("TaskID", taskId);
            }
            var dataT = op.DesTaskCheckList(PageModel);

            int i = 1;
            StringBuilder jsyj = new StringBuilder();
            StringBuilder hfyj = new StringBuilder();
            ViewBag.JSTime = DateTime.Now.ToString("yyyy-MM-dd");

            DataRow[] js_row;
            DataRow[] alljs_row = dataT.Select("CheckNodeTypeID=0");

            XmlDocument document = new XmlDocument();
            document.LoadXml(model.TaskFlowModel);

            var hzNote = new DBSql.Design.DesTask().FirstOrDefault(p => p.Id == model.TaskParentId);

            var empId = 0;

            switch (type)
            {
                case 1:
                    var jhry = document.SelectNodes("root/item[@FlowNodeTypeID='20']/@FlowNodeEmpID");
                    empId = jhry.Count == 0 ? 0 : Convert.ToInt32(jhry[0].Value);
                    js_row = dataT.Select("CheckNodeTypeID=20 or (CheckNodeTypeID=0 and CheckEmpId=" + empId + ")");
                    break;
                case 2:
                    var shry = document.SelectNodes("root/item[@FlowNodeTypeID='21']/@FlowNodeEmpID");
                    empId = shry.Count == 0 ? 0 : Convert.ToInt32(shry[0].Value);
                    js_row = dataT.Select("CheckNodeTypeID=21 or (CheckNodeTypeID=0 and CheckEmpId=" + empId + ")");
                    break;
                case 3:
                    var sdry = document.SelectNodes("root/item[@FlowNodeTypeID='22']/@FlowNodeEmpID");

                    empId = sdry.Count == 0 ? 0 : Convert.ToInt32(sdry[0].Value);
                    if (empId == 0)
                    {
                        if (hzNote != null)
                        {
                            XmlDocument hz_doc = new XmlDocument();
                            hz_doc.LoadXml(hzNote.TaskFlowModel);
                            var nodes = hz_doc.SelectNodes("root/item[@rownum='1']/@FlowNodeEmpID");
                            if (nodes.Count > 0)
                            {
                                empId =Convert.ToInt32(nodes[0].Value);
                            }
                        }
                    }
                    js_row = dataT.Select("CheckNodeTypeID=22 or (CheckNodeTypeID=0 and CheckEmpId=" + empId + ")");
                    break;
                default:
                    js_row = dataT.Select("1=1");
                    break;
            }

            foreach (DataRow row in js_row)
            {
                string hf = row["HFNote"].ToString() == "" ? row["CheckIsCorrectiveTypeName"].ToString() : row["HFNote"].ToString();
                hf = hf == "" ? "无" : hf;
                jsyj.Append("（" + i.ToString() + "）" + row["CheckNote"].ToString() + "<br/>");
                hfyj.Append("（" + i.ToString() + "）" + hf + "<br/>");
                i++;
            }

            ViewBag.jsyj = jsyj.ToString();
            ViewBag.hfyj = hfyj.ToString();
            ViewBag.ZY = new DBSql.Sys.BaseData().GetBaseDataInfo(model.TaskSpecId).BaseName;
            ViewBag.FlowEmp = GetFlowName(model, type);
            ViewBag.SSTime = model.CreationTime.ToString("yyyy-MM-dd");
            ViewBag.SJR = model.TaskEmpName;
            ViewBag.DesignPhaseName = new DBSql.Sys.BaseData().GetBaseDataInfo(model.TaskPhaseId).BaseName;

            var per = JavaScriptSerializerUtil.getJson((PermissionList("ProjectCenterList")));
            if (per.Contains("allDown"))
            {
                ViewBag.Permission = 1;
            }
            else
            {
                ViewBag.Permission = 0;
            }
            return View("info", model);
        }

        public String GetFlowName(DataModel.Models.DesTask model, int type)
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
                       };
            StringBuilder html = new StringBuilder();
            string empName = "";

            var hzNote = new DBSql.Design.DesTask().FirstOrDefault(p => p.Id == model.TaskParentId);

            DBSql.Design.Dto.DesFlowNodeXmlInput flowModel = null;

            if (model.TaskStatus == 3)
            {
                switch (type)
                {
                    case 1:
                        html.Append("<th>校核人</th>");
                        flowModel = list.Where(p => p.FlowNodeTypeID == "20").FirstOrDefault();
                        if (flowModel != null)
                        {
                            empName = flowModel.FlowNodeEmpName;
                        }
                        else
                        {
                            empName = "";
                        }
                        html.AppendFormat("<td><span id='JHR_EmpName' name='JHR_EmpName' bookmark='JHR_EmpName'>{0}</span></td>", empName);
                        html.Append("<th>审核人</th>");
                        empName = "";
                        html.AppendFormat("<td><span id='SHR_EmpName' name='SHR_EmpName' bookmark='SHR_EmpName'>{0}</span></td>", empName);
                        html.Append("<th>审定人</th>");
                        empName = "";
                        html.AppendFormat("<td><span id='SDR_EmpName' name='SDR_EmpName' bookmark='SDR_EmpName'>{0}</span></td>", empName);
                        break;
                    case 2:
                        html.Append("<th>校核人</th>");
                        empName = "";
                        html.AppendFormat("<td><span id='JHR_EmpName' name='JHR_EmpName' bookmark='JHR_EmpName'>{0}</span></td>", empName);
                        html.Append("<th>审核人</th>");
                        flowModel = list.Where(p => p.FlowNodeTypeID == "21").FirstOrDefault();
                        if (flowModel != null)
                        {
                            empName = flowModel.FlowNodeEmpName;
                        }
                        else
                        {
                            empName = "";
                        }
                        html.AppendFormat("<td><span id='SHR_EmpName' name='SHR_EmpName' bookmark='SHR_EmpName'>{0}</span></td>", empName);
                        html.Append("<th>审定人</th>");
                        empName = "";
                        html.AppendFormat("<td><span id='SDR_EmpName' name='SDR_EmpName' bookmark='SDR_EmpName'>{0}</span></td>", empName);
                        break;
                    case 3:
                        html.Append("<th>校核人</th>");
                        empName = "";
                        html.AppendFormat("<td><span id='JHR_EmpName' name='JHR_EmpName' bookmark='JHR_EmpName'>{0}</span></td>", empName);
                        html.Append("<th>审核人</th>");
                        empName = "";
                        html.AppendFormat("<td><span id='SHR_EmpName' name='SHR_EmpName' bookmark='SHR_EmpName'>{0}</span></td>", empName);
                        html.Append("<th>审定人</th>");
                        flowModel = list.Where(p => p.FlowNodeTypeID == "22").FirstOrDefault();
                        if (flowModel != null)
                        {
                            empName = flowModel.FlowNodeEmpName;
                        }
                        else
                        {
                            if (hzNote != null)
                            {
                                XmlDocument document = new XmlDocument();
                                document.LoadXml(hzNote.TaskFlowModel);
                                var nodes = document.SelectNodes("root/item[@rownum='1']/@FlowNodeEmpName");
                                if (nodes.Count > 0)
                                {
                                    empName = nodes[0].Value;
                                }
                            }
                        }
                        html.AppendFormat("<td><span id='SDR_EmpName' name='SDR_EmpName' bookmark='SDR_EmpName'>{0}</span></td>", empName);
                        break;
                    default:
                        html.Append("<th>校核人</th>");
                        flowModel = list.Where(p => p.FlowNodeTypeID == "20").FirstOrDefault();
                        if (flowModel != null)
                        {
                            empName = flowModel.FlowNodeEmpName;
                        }
                        else
                        {
                            empName = "";
                        }
                        html.AppendFormat("<td><span id='JHR_EmpName' name='JHR_EmpName' bookmark='JHR_EmpName'>{0}</span></td>", empName);
                        html.Append("<th>审核人</th>");
                        flowModel = list.Where(p => p.FlowNodeTypeID == "21").FirstOrDefault();
                        if (flowModel != null)
                        {
                            empName = flowModel.FlowNodeEmpName;
                        }
                        else
                        {
                            empName = "";
                        }
                        html.AppendFormat("<td><span id='SHR_EmpName' name='SHR_EmpName' bookmark='SHR_EmpName'>{0}</span></td>", empName);
                        html.Append("<th>审定人</th>");
                        flowModel = list.Where(p => p.FlowNodeTypeID == "22").FirstOrDefault();
                        if (flowModel != null)
                        {
                            empName = flowModel.FlowNodeEmpName;
                        }
                        else
                        {
                            if (hzNote != null)
                            {
                                XmlDocument document = new XmlDocument();
                                document.LoadXml(hzNote.TaskFlowModel);
                                var nodes = document.SelectNodes("root/item[@rownum='1']/@FlowNodeEmpName");
                                if (nodes.Count > 0)
                                {
                                    empName = nodes[0].Value;
                                }
                            }
                        }
                        html.AppendFormat("<td><span id='SDR_EmpName' name='SDR_EmpName' bookmark='SDR_EmpName'>{0}</span></td>", empName);
                        break;
                }
            }
            else
            {
                html.Append("<th>校核人</th>");
                html.Append("<td></td>");
                html.Append("<th>审核人</th>");
                html.Append("<td></td>");
                html.Append("<th>审定人</th>");
                html.Append("<td></td>");
            }

            return html.ToString();
        }

        public string getTableTr(DataTable dataT)
        {
            var list = dataT.AsEnumerable().Select(p => new
            {
                Id = Common.ExtensionMethods.Value<int>(p["Id"]),
                TaskID = Common.ExtensionMethods.Value<int>(p["TaskID"]),
                CheckAttachID = Common.ExtensionMethods.Value<int>(p["Id"].ToString()),
                CheckNote = p["CheckNote"].ToString(),
                CheckErrTypeName = p["CheckErrTypeName"].ToString(),
                CheckEmpIDName = p["CheckEmpIDName"].ToString(),
                CheckDate = Common.ExtensionMethods.Value<System.DateTime>(p["CheckDate"]).ToString("yyyy-MM-dd HH:mm"),
                AttachName = p["AttachName"].ToString(),
                CheckIsCorrectiveTypeName = p["CheckIsCorrectiveTypeName"].ToString(),
                DesignCheckAdvice = p["CheckIsCorrectiveTypeName"].ToString(),
                CheckIsExamine = p["CheckIsExamine"].ToString() == "True" ? "√" : "",
            }).ToList();
            string trStr = "";
            int index = 0;
            int rowspanTaskId = 0;
            int hasAddTd = 0;
            foreach (var item in list)
            {
                trStr += "<tr>";
                index++;
                //重复
                trStr += string.Format("<td>{1}</td>", hasAddTd, index);//序号
                if (hasAddTd == 0)
                {
                    hasAddTd = list.Where(p => p.CheckAttachID == item.CheckAttachID).Count();
                    rowspanTaskId = item.TaskID;
                    //trStr += string.Format("<td rowspan=\"{0}\" colspan='6'><div style=\"display:block;text-align:left\">{1}</div><div style=\"display:block;text-align:right\">{3}&nbsp; &nbsp; &nbsp;{2} &nbsp; &nbsp; &nbsp;{4}</div></td>", hasAddTd, item.CheckNote, item.CheckEmpIDName, item.CheckErrTypeName, item.CheckDate);
                    trStr += string.Format("<td  rowspan=\"{0}\" colspan='6'>", hasAddTd);
                    trStr += string.Format("<div style=\"display:block;text-align:left\">{0}</div>", item.CheckNote);
                    trStr += string.Format("<div style=\"display:block;text-align:right\">{1}&nbsp; &nbsp; &nbsp;{0} &nbsp; &nbsp; &nbsp;{2}</div>", item.CheckEmpIDName, item.CheckErrTypeName, item.CheckDate);
                    #region 插入图片
                    List<DataModel.Models.DesTaskCheckImage> ImageList = op.DbContext.Set<DataModel.Models.DesTaskCheckImage>().Where(p => p.CheckID == item.Id).ToList();
                    foreach (DataModel.Models.DesTaskCheckImage Img in ImageList)
                    {
                        trStr += string.Format("<div style=\"display:block;text-align:left;\"><image src=\"{0}\" style='width:230px;height:120px;' /></div>", Url.Action("GetImg", "DesTaskCheck", new { @area = "Design" }).ToString() + "?Id=" + Img.Id);
                    }
                    #endregion
                    trStr += "</td>";
                }
                trStr += string.Format("<td colspan='2' style='text-align:left'>{0} </td>", item.AttachName.Trim());
                trStr += string.Format("<td>{0} </td>", item.DesignCheckAdvice);
                trStr += string.Format("<td >{0}</td>", item.CheckIsExamine);
                if (hasAddTd > 0)
                {
                    hasAddTd--;
                }
                trStr += "</tr>";
            }
            return trStr;
        }

        /// <summary>
        /// 获取设计校审单
        /// </summary>
        /// <returns></returns>
        public string getTableDesign(int taskId, DataTable dt)
        {
            string str = "";
            DesTask model = new DBSql.Design.DesTask().Get(taskId);

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
                       };
            list = list.OrderBy(p => p.ID).ToList();
            foreach (var item in list)
            {
                str += "<tr>";
                str += string.Format("<td colspan='2'>{0}</td>", item.FlowNodeName);
                if (item.FlowNodeTypeID != ((int)DataModel.NodeType.设计).ToString())
                {
                    //校对或者 同一个人的
                    DataRow[] drXiao = dt.Select(" CheckNodeTypeID=" + item.FlowNodeTypeID + " or CheckEmpId=" + item.FlowNodeEmpID + " ");

                    str += string.Format("<td colspan='2'>{0}</td>", drXiao.Where(p => p["CheckErrTypeID"].ToString() == ((int)DataModel.DesignErrorType.原则性差错).ToString()).Count());
                    str += string.Format("<td colspan='2'>{0}</td>", drXiao.Where(p => p["CheckErrTypeID"].ToString() == ((int)DataModel.DesignErrorType.技术性差错).ToString()).Count());
                    str += string.Format("<td colspan='2'>{0}</td>", drXiao.Where(p => p["CheckErrTypeID"].ToString() == ((int)DataModel.DesignErrorType.一般性差错).ToString()).Count());
                    dt = rowRomove(dt, drXiao);
                }
                else
                {
                    str += string.Format("<td colspan='2'>{0}</td>", "0");
                    str += string.Format("<td colspan='2'>{0}</td>", "0");
                    str += string.Format("<td colspan='2'>{0}</td>", "0");
                }

                str += string.Format("<td>{0}</td>", "");
                str += string.Format("<td colspan='2'>{0}</td>", item.FlowNodeEmpName);
                str += "</tr>";
            }
            return str;
        }

        private DataTable rowRomove(DataTable dt, DataRow[] drs)
        {
            foreach (DataRow dr in drs)
            {
                dt.Rows.Remove(dr);
            }
            return dt;
        }
        #endregion
    }
}
