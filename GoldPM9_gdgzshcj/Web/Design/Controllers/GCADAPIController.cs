using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DBSql.PubFlow;
using System.IO;
using System.Data;
using System.Xml;
using System.ComponentModel;

namespace Design.Controllers
{

    public class GCADAPIController : Controller
    {
        private class RequestResult
        {
            /// <summary>
            /// 状态
            /// </summary>
            public int status { get; set; }
            /// <summary>
            /// 成功时返回数据
            /// </summary>
            public object data { get; set; }
            /// <summary>
            /// 错误代码
            /// </summary>
            public int code { get; set; }
            /// <summary>
            /// 错误说明
            /// </summary>
            public string msg { get; set; }

            public string empid
            {
                get; set;
            }

            public List<OptionItem> types
            {
                get; set;
            }

            public List<OptionColumn> columns
            {
                get; set;
            }
        }

        private class TaskDetail
        {
            public string TaskID
            {
                get;
                set;
            }
            public string TaskName
            {
                get; set;
            }

            public string TaskEmpName
            {
                get; set;
            }

            public string TaskEmpID
            {
                get; set;
            }
        }

        private class TaskNode
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Group { get; set; }
            public string GroupName { get; set; }
            public string Role { get; set; }
            public string RoleName { get; set; }
            public string Type { get; set; }
            public string TypeName { get; set; }
            public string TaskEmpId { get; set; }
            public string TaskEmpName { get; set; }
        }

        private class ItemPath
        {
            public int rownum
            {
                get; set;
            }

            public int value
            {
                get; set;
            }

            public string text
            {
                get; set;
            }

            public string active
            {
                get; set;
            }
        }

        private class OptionItem
        {
            public string id
            {
                get; set;
            }

            public string name
            {
                get; set;
            }
        }

        private class OptionColumn
        {
            public string field
            {
                get; set;
            }

            public string name
            {
                get; set;
            }

            public int width
            {
                get; set;
            }

            /// <summary>
            /// left:0 right:1 center:2
            /// </summary>
            public int align
            {
                get; set;
            }
        }

        public string Process()
        {
            var command = Request.Params["command"];
            if (string.IsNullOrEmpty(command))
            {
                return JQ.Util.JavaScriptSerializerUtil.getJson(new RequestResult() { status = 0, msg = "未获取到可执行的命令" });
            }
            switch (command)
            {
                case "gettask":
                    return GetTaskList();
                case "UploadFile":
                case "filesync":
                    return UploadFile();
                case "GetNodesXML":
                    GetNodesXML();
                    return null;
                case "GetDrawingsXML":
                    GetDrawingsXML();
                    return null;
                case "GetSignsXML":
                    GetSignsXML();
                    return null;
                case "UploadAttach":
                    return UploadAttach();
                case "UploadAttachQM":
                    return UploadAttachQM();
                default:
                    return JQ.Util.JavaScriptSerializerUtil.getJson(new RequestResult() { status = 0, msg = "未识别到可用的命令" });
            }
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        private DataModel.Models.BaseEmployee CheckEmpLogin()
        {
            string account = Request.Params["a"] ?? "";
            string password = Request.Params["p"] ?? "";
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                throw new Common.JQException("登录账户无效");
            }
            var beDAL = new DBSql.Sys.BaseEmployee();
            if (!beDAL.CheckEmpPassword(account.Trim(), password.Trim().ToUpper()))
            {
                throw new Common.JQException("账户登录失败");
            }
            var emp = beDAL.GetBaseEmployeeInfo(account.Trim());
            return emp;
        }

        // ============== CAD工具包接口 ===============

        /// <summary>
        /// 获取 当前用户任务列表（含列定义，类型定义等）
        /// </summary>
        /// <returns></returns>
        private string GetTaskList()
        {
            //获取出姓名
            var result = new RequestResult();
            try
            {
                DataModel.Models.BaseEmployee emp = CheckEmpLogin();
                result.data = GetTaskData(emp.EmpID, Request.Params["s"] ?? "", Request.Params["typeId"] ?? ""); // s 查询文本内容 typeId 列表数据筛选类型
                result.status = 1;
                result.code = 0;
                result.empid = emp.EmpID.ToString();
                result.types = new List<OptionItem>();
                result.types.Add(new OptionItem() { id = "1", name = "设计任务" });
                result.types.Add(new OptionItem() { id = "2", name = "变更单" });
                result.columns = new List<OptionColumn>();
                result.columns.Add(new OptionColumn() { field = "TaskName", width = 90, name = "任务名称", align = 0 });
            }
            catch (Common.JQException cex)
            {
                result.status = 0;
                result.code = 1;
                result.msg = cex.Message;
            }
            catch (Exception ex)
            {
                result.status = 0;
                result.code = 2;
                result.msg = ex.Message;
            }
            return JQ.Util.JavaScriptSerializerUtil.getJson(result);
        }

        /// <summary>
        /// 获取 当前用户任务列表数据
        /// </summary>
        /// <param name="empID"></param>
        /// <param name="textCondition"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private List<TaskDetail> GetTaskData(int empID, string textCondition, string type)
        {
            if (type == "1" || type == "")
            {
                //type=0的时候为第一次请求，默认返回为第一项数据
                Common.SqlPageInfo queryContext = new Common.SqlPageInfo();
                if (!string.IsNullOrEmpty(textCondition))
                {
                    queryContext.TextCondtion = textCondition;
                }
                queryContext.ToPageData = false;
                string DesTaskApproveMode = new DBSql.Sys.BaseConfig().GetBaseConfigInfo("DesTaskApproveMode").ConfigValue; // 任务校审模式   单步 0；同步 1
                queryContext.SelectCondtion.Add("SessionEmpId", empID);
                queryContext.SelectCondtion.Add("ItemStatus", DesTaskApproveMode == "0" ? String.Format("{0}", DBSql.Design.FlowNodeStatus.进行中.ToString("D")) : String.Format("{0},{1}", DBSql.Design.FlowNodeStatus.已安排.ToString("D"), DBSql.Design.FlowNodeStatus.进行中.ToString("D")));
                List<TaskDetail> taskDetails = new List<TaskDetail>();
                using (var dt = new DBSql.Design.DesTask().GetTaskRemindDesignList(queryContext))
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        var taskDetail = new TaskDetail();
                        taskDetail.TaskID = row["TaskId"].ToString();
                        taskDetail.TaskEmpID = row["TaskEmpID"].ToString();
                        taskDetail.TaskEmpName = row["ItemEmpName"].ToString();
                        var itemPaths = JQ.Util.JavaScriptSerializerUtil.objectToEntity<List<ItemPath>>(row["ItemPath1"]).OrderBy(m => m.rownum);
                        foreach (var item in itemPaths)
                        {
                            taskDetail.TaskName += item.text + " >";
                        }
                        itemPaths = JQ.Util.JavaScriptSerializerUtil.objectToEntity<List<ItemPath>>(row["ItemPath2"]).OrderBy(m => m.rownum);
                        foreach (var item in itemPaths)
                        {
                            taskDetail.TaskName += item.text + " >";
                        }
                        taskDetail.TaskName = taskDetail.TaskName.TrimEnd('>').TrimEnd(' ');
                        taskDetails.Add(taskDetail);
                    }
                }
                return taskDetails;
            }
            else
            {
                return new List<TaskDetail>();
            }
            //return new DBSql.Design.DesTask().GetTaskRemindList(queryContext).ToList<TaskDetail>();
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        private string UploadFile()
        {
            var fileID = Request.Params["FileId"];
            var chunks = JQ.Util.TypeParse.parse<int>(Request.Params["chunks"]);
            var chunk = JQ.Util.TypeParse.parse<int>(Request.Params["chunk"]);
            var savePath = "";
            if (chunk == 0 || Session[fileID] == null)
            {
                if (Request.Params["name"] == null)
                {
                    return JQ.Util.JavaScriptSerializerUtil.getJson(new RequestResult() { status = 0, msg = "未找到可用的文件名称" });
                }
                var path = JQ.Util.IOUtil.GetTempPath();
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                savePath = Path.Combine(path, Guid.NewGuid().ToString());
                if (chunks > 1)
                {
                    Session[fileID] = savePath;
                }
            }
            else
            {
                savePath = Session[fileID].ToString();
                if (chunks == chunk + 1)
                {
                    Session.Remove(fileID);
                }
            }
            byte[] buffer = null;
            if (Request.ContentType == "application/octet-stream" && Request.ContentLength > 0)
            {
                buffer = new Byte[Request.InputStream.Length];
                Request.InputStream.Read(buffer, 0, buffer.Length);
            }
            else if (Request.ContentType.Contains("multipart/form-data") && Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                buffer = new Byte[Request.Files[0].InputStream.Length];
                Request.Files[0].InputStream.Read(buffer, 0, buffer.Length);
            }
            using (var fs = new FileStream(savePath, FileMode.Append))
            {
                fs.Write(buffer, 0, buffer.Length);
            }
            if (chunks == chunk + 1)
            {
                //文件真实名字
                var realName = Request.Params["name"];
                if (string.IsNullOrEmpty(realName))
                {
                    return JQ.Util.JavaScriptSerializerUtil.getJson(new RequestResult() { status = 0, msg = "未找到可用的文件名称" });
                }
                var parentID = Request.Params["ParentId"];
                if (string.IsNullOrEmpty(parentID))
                {
                    //为原图上传完毕，开始处理所有的图纸
                    UploadFileFinish(fileID, savePath, realName);
                }
                else
                {
                    System.IO.File.Move(savePath, Path.Combine(Path.GetDirectoryName(savePath), parentID + "_" + realName));
                }
            }
            return JQ.Util.JavaScriptSerializerUtil.getJson(new RequestResult() { status = 1 });
        }

        /// <summary>
        /// 为原图上传完毕，开始处理所有的图纸
        /// </summary>
        /// <param name="fileID"></param>
        /// <param name="filePath"></param>
        /// <param name="realName"></param>
        private void UploadFileFinish(string fileID, string filePath, string realName)
        {
            TaskDetail taskDetail = null;
            if (Request.Params["TaskInfo"] != null)
            {
                try
                {
                    taskDetail = JQ.Util.JavaScriptSerializerUtil.parseFormJson<TaskDetail>(Request.Params["TaskInfo"]);
                }
                catch
                {
                    return;
                }
            }
            if (taskDetail == null)
            {
                return;
            }
            var splitedFiles = new List<string>();
            foreach (var file in Directory.GetFiles(Path.GetDirectoryName(filePath), fileID + "_*.*"))
            {
                //处理数据
                if (!System.IO.File.Exists(file))
                {
                    continue;
                }
                //插入数据
                splitedFiles.Add(file);
            }
            using (var ba = new DBSql.Sys.BaseAttach())
            {
                string[] ids = taskDetail.TaskID.Split('_');
                if (ids.Length > 1)
                {
                    taskDetail.TaskID = ids[1];
                }
                else
                {
                    taskDetail.TaskID = ids[0];
                }
                ba.SaveDWGFromGCAD(JQ.Util.TypeParse.parse<int>(taskDetail.TaskID), filePath, realName, splitedFiles, JQ.Util.TypeParse.parse<int>(taskDetail.TaskEmpID), taskDetail.TaskEmpName);
            }
        }


        // ============== CAD电子校审接口 ===============

        /// <summary>
        /// 获取 当前用户任务信息
        /// </summary>
        /// <returns></returns>
        public void GetNodesXML()
        {
            //192.168.0.222/JQWebMVC/Design/GCADAPI/Process?command=GetNodesXML&a=周军&p=1A1DC91C907325C69271DDF0C944BC72
            var result = new RequestResult();
            DataTable dt = null;
            try
            {
                DataModel.Models.BaseEmployee emp = CheckEmpLogin();

                var type = Request.Params["typeId"] ?? "1";  // （可选）1 设计任务 2 变更单
                if (type == "1")
                {
                    string DesTaskApproveMode = new DBSql.Sys.BaseConfig().GetBaseConfigInfo("DesTaskApproveMode").ConfigValue; // 任务校审模式   单步 0；同步 1
                    Common.SqlPageInfo queryContext = new Common.SqlPageInfo();
                    queryContext.TextCondtion = Request.Params["s"] ?? "";
                    queryContext.ToPageData = false;
                    queryContext.SelectCondtion.Add("SessionEmpId", emp.EmpID);
                    queryContext.SelectCondtion.Add("ItemStatus", DesTaskApproveMode == "0" ? String.Format("{0}", DBSql.Design.FlowNodeStatus.进行中.ToString("D")) : String.Format("{0},{1}", DBSql.Design.FlowNodeStatus.已安排.ToString("D"), DBSql.Design.FlowNodeStatus.进行中.ToString("D")));
                    dt = new DBSql.Design.DesTask().GetTaskRemindDesignList(queryContext);
                }
                result.data = dt;
                result.status = 1;
                result.code = 0;
                result.msg = "";
            }
            catch (Exception ex)
            {
                result.status = -1;
                result.code = ex.GetHashCode();
                result.msg = ex.Message;
            }
            GetNodesXMLSerializer(result);
        }

        /// <summary>
        /// 解析 任务信息结果 为 CAD工具支持的 Nodes.xml 格式
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private void GetNodesXMLSerializer(RequestResult result)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.AppendChild(xdoc.CreateXmlDeclaration("1.0", Encoding.UTF8.WebName, "yes"));

            XmlElement rootNode = xdoc.CreateElement("Nodes");
            rootNode.SetAttribute("status", result.status.ToString());
            rootNode.SetAttribute("code", result.code.ToString());
            rootNode.SetAttribute("msg", result.msg.ToString());
            xdoc.AppendChild(rootNode);

            if (result.data != null)
            {
                DataTable dt = (DataTable)result.data;
                foreach (DataRow row in dt.Rows)
                {
                    XmlElement parentNode = rootNode;
                    var itemPaths = JQ.Util.JavaScriptSerializerUtil.objectToEntity<List<ItemPath>>(row["ItemPath1"]).OrderBy(m => m.rownum);
                    foreach (var item in itemPaths)
                    {
                        parentNode = GetNodesXMLSerializerNode(xdoc, parentNode, item, row);
                    }
                    itemPaths = JQ.Util.JavaScriptSerializerUtil.objectToEntity<List<ItemPath>>(row["ItemPath2"]).OrderBy(m => m.rownum);
                    foreach (var item in itemPaths)
                    {
                        parentNode = GetNodesXMLSerializerNode(xdoc, parentNode, item, row);
                    }
                }
            }

            Response.Clear();
            Response.ContentType = "text/xml";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            xdoc.Save(Response.Output);
            Response.End();
        }

        /// <summary>
        /// 解析 任务信息路径中的 某节点
        /// </summary>
        /// <param name="xdoc"></param>
        /// <param name="parentNode"></param>
        /// <param name="item"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private XmlElement GetNodesXMLSerializerNode(XmlDocument xdoc, XmlElement parentNode, ItemPath item, DataRow row)
        {
            XmlElement thisNode = xdoc.CreateElement("Node");
            thisNode.SetAttribute("Id", item.active.ToString() + "_" + item.value.ToString());
            thisNode.SetAttribute("Name", item.text);
            thisNode.SetAttribute("Group", item.active);

            Dictionary<string, string> groupNames = new Dictionary<string, string>();
            groupNames.Add("Project", "项目");
            groupNames.Add("ProjPhase", "阶段");
            groupNames.Add("TaskGroup", "分组");
            groupNames.Add("TaskSpec", "专业");
            groupNames.Add("TaskPath", "父任务");
            groupNames.Add("Task", "任务");

            switch (item.active)
            {
                case "Project":
                case "ProjPhase":
                case "TaskGroup":
                case "TaskSpec":
                case "TaskPath":
                    {
                        // 从父节点中查找是否已经有当前要插入的节点，没有的话插入一个新的
                        XmlNode hasNode = parentNode.SelectSingleNode(String.Format("./Node[@Id='{0}']", item.active + "_" + item.value.ToString()));
                        if (hasNode == null)
                        {
                            thisNode.SetAttribute("GroupName", groupNames[item.active]);
                            parentNode.AppendChild(thisNode);
                        }
                        else
                        {
                            thisNode = (XmlElement)hasNode;
                        }
                    }
                    break;
                case "Task":
                    {
                        // 从父节点中查找是否已经有当前要插入的节点，没有的话插入一个新的
                        XmlNode hasNode = parentNode.SelectSingleNode(String.Format("./Node[@Id='{0}']", item.active + "_" + item.value.ToString()));
                        if (hasNode == null)
                        {
                            thisNode.SetAttribute("GroupName", groupNames[item.active]);
                            thisNode.SetAttribute("TaskEmpId", row.Field<int>("TaskEmpID").ToString());  // 任务执行人id
                            thisNode.SetAttribute("TaskEmpName", row.Field<string>("ItemEmpName").ToString());  // 任务执行人姓名
                            thisNode.SetAttribute("Role", row.Field<int>("ItemType").ToString());  // 3 提交设计 4 设计校审
                            thisNode.SetAttribute("RoleName", row.Field<string>("ItemAction").ToString());
                            thisNode.SetAttribute("Type", "1");
                            thisNode.SetAttribute("TypeName", "设计任务");
                            parentNode.AppendChild(thisNode);
                        }
                    }
                    break;
            }

            return thisNode;
        }



        /// <summary>
        /// 获取 当前任务中的文件信息
        /// </summary>
        private void GetDrawingsXML()
        {
            // 192.168.0.222/JQWebMVC/Design/GCADAPI/Process?command=GetDrawingsXML&a=周军&p=1A1DC91C907325C69271DDF0C944BC72&TaskInfo={Id:"295",Role:"3"}&status=finished

            var result = new RequestResult();
            IEnumerable<dynamic> dt = null;
            try
            {
                DataModel.Models.BaseEmployee emp = CheckEmpLogin();

                var type = Request.Params["typeId"] ?? "1"; // （可选）1 设计任务 2 变更单

                if (type == "1")
                {
                    var taskInfo = JQ.Util.JavaScriptSerializerUtil.parseFormJson<TaskNode>(Request.Params["TaskInfo"]);
                    string[] taskIds = taskInfo.Id.Split('_');
                    var Id = "";
                    if (taskIds.Length > 1)
                    {
                        Id = taskIds[1];
                    }
                    else
                    {
                        Id = taskIds[0];
                    }
                    var taskId = Common.ModelConvert.ConvertToDefaultType<long>(Id);   // 任务id
                    var status = Request.Params["status"];   // 文件状态
                    if (String.IsNullOrWhiteSpace(status) || status == "now")
                    {
                        var role = taskInfo.Role;   // 3 提交设计 4 设计校审
                        if (role == "3")
                        {
                            dt = new DBSql.Design.DesTask().GetDesTaskAttachData(taskId);
                        }
                        else if (role == "4")
                        {
                            dt = new DBSql.Design.DesTask().GetDesTaskApproveAllAttachData(taskId, emp.EmpID);
                        }
                    }
                    else if (status == "finished")
                    {
                        dt = new DBSql.Design.DesTask().GetDesTaskFinishAttachData(taskId);
                    }
                }
                result.data = dt;
                result.status = 1;
                result.code = 0;
                result.msg = "";
            }
            catch (Exception ex)
            {
                result.status = -1;
                result.code = ex.GetHashCode();
                result.msg = ex.Message;
            }
            GetDrawingsXMLSerializer(result);
        }

        /// <summary>
        /// 解析 当前任务中的文件信息 为 CAD工具支持的 Drawings.xml 格式
        /// </summary>
        /// <param name="result"></param>
        private void GetDrawingsXMLSerializer(RequestResult result)
        {
            //string weburl = String.Format("{0}://{1}:{2}{3}", Request.Url.Scheme, Request.Url.Host, Request.Url.Port, Url.Content("~"));
            string weburl= String.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Headers["Host"], Url.Content("~"));

            XmlDocument xdoc = new XmlDocument();
            xdoc.AppendChild(xdoc.CreateXmlDeclaration("1.0", Encoding.UTF8.WebName, "yes"));

            XmlElement rootNode = xdoc.CreateElement("Attachs");
            rootNode.SetAttribute("status", result.status.ToString());
            rootNode.SetAttribute("code", result.code.ToString());
            rootNode.SetAttribute("msg", result.msg.ToString());
            xdoc.AppendChild(rootNode);

            if (result.data != null)
            {
                rootNode.SetAttribute("WebUrl", weburl);
                rootNode.SetAttribute("AttachUrl", weburl + "core/ProcessFile/Download?type=attach&id="); // 上传附件下载路径

                IEnumerable<dynamic> dtAttachs = (IEnumerable<dynamic>)result.data;

                var rootAttachs = from a in dtAttachs
                                  where TypeDescriptor.GetProperties(a).Find("_parentId", true).GetValue(a) == 0
                                  select a;
                foreach (var rootAttach in rootAttachs)
                {
                    XmlElement parentNode = rootNode;
                    GetDrawingsXMLSerializerNode(xdoc, parentNode, dtAttachs, rootAttach);
                }
            }

            Response.Clear();
            Response.ContentType = "text/xml";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            xdoc.Save(Response.Output);
            Response.End();
        }

        /// <summary>
        /// 解析 当前任务中的文件信息中的 某节点
        /// </summary>
        /// <param name="xdoc"></param>
        /// <param name="parentNode"></param>
        /// <param name="dtAttachs"></param>
        /// <param name="attach"></param>
        private void GetDrawingsXMLSerializerNode(XmlDocument xdoc, XmlElement parentNode, IEnumerable<dynamic> dtAttachs, dynamic attach)
        {
            PropertyDescriptorCollection attachProp = TypeDescriptor.GetProperties(attach);

            XmlElement thisNode = xdoc.CreateElement("Attach");
            thisNode.SetAttribute("ParentId", attachProp.Find("_parentId", true).GetValue(attach).ToString());
            thisNode.SetAttribute("AttachID", attachProp.Find("AttachID", true).GetValue(attach).ToString());
            thisNode.SetAttribute("AttachName", attachProp.Find("AttachName", true).GetValue(attach).ToString());
            thisNode.SetAttribute("AttachExt", attachProp.Find("AttachExt", true).GetValue(attach).ToString());
            //thisNode.SetAttribute("AttachOrderPath",attachProp.Find("AttachOrderPath", true).GetValue(attach).ToString());
            //thisNode.SetAttribute("AttachPath",attachProp.Find("AttachPath", true).GetValue(attach).ToString());
            //thisNode.SetAttribute("AttachPathIDs",attachProp.Find("AttachPathIDs", true).GetValue(attach).ToString());
            thisNode.SetAttribute("AttachEmpID", attachProp.Find("AttachEmpID", true).GetValue(attach).ToString());
            thisNode.SetAttribute("AttachEmpName", attachProp.Find("AttachEmpName", true).GetValue(attach).ToString());
            thisNode.SetAttribute("AttachDateChange", ((DateTime)attachProp.Find("AttachDateChange", true).GetValue(attach)).ToString("yyyy-MM-dd HH:mm:ss"));
            thisNode.SetAttribute("AttachDateUpload", ((DateTime)attachProp.Find("AttachDateUpload", true).GetValue(attach)).ToString("yyyy-MM-dd HH:mm:ss"));
            thisNode.SetAttribute("AttachSize", attachProp.Find("AttachSize", true).GetValue(attach).ToString());
            thisNode.SetAttribute("AttachTag", attachProp.Find("AttachTag", true).GetValue(attach).ToString());
            thisNode.SetAttribute("AttachVer", attachProp.Find("AttachVer", true).GetValue(attach).ToString());
            //thisNode.SetAttribute("AttachGrade",attachProp.Find("AttachGrade", true).GetValue(attach).ToString());
            var attachFlowNodeJson = attachProp.Find("AttachFlowNodeJson", true).GetValue(attach).ToString();

            if (attachProp.Find("AttachExt", true).GetValue(attach).ToString() != ".")
            {
                if (String.IsNullOrWhiteSpace(attachFlowNodeJson))
                {
                    thisNode.SetAttribute("DesignEmpId", "");
                    thisNode.SetAttribute("DesignEmpName", "");
                    thisNode.SetAttribute("DesignNodeType", "");
                    thisNode.SetAttribute("DesignNodeName", "");
                    thisNode.SetAttribute("ApproveNowEmpId", "");
                    thisNode.SetAttribute("ApproveNowEmpName", "");
                    thisNode.SetAttribute("ApproveNowNodeType", "");
                    thisNode.SetAttribute("ApproveNowNodeName", "");
                }
                else
                {
                    DBSql.Design.Dto.DesTaskAttachFlowNodeJson desTaskAttachFlowNodeJson = JQ.Util.JavaScriptSerializerUtil.parseFormJson<DBSql.Design.Dto.DesTaskAttachFlowNodeJson>(attachFlowNodeJson);
                    DBSql.Design.Dto.DesTaskAttachFlowNodeItem[] desTaskAttachFlowNodeItems = desTaskAttachFlowNodeJson.root.item;
                    DBSql.Design.Dto.DesTaskAttachFlowNodeItem[] desTaskAttachFlowNodeDesignItem = (desTaskAttachFlowNodeItems.Where(x => x.FlowNodeTypeID == DBSql.Design.FlowNodeType.设计.ToString("D"))).ToArray();
                    DBSql.Design.Dto.DesTaskAttachFlowNodeItem[] desTaskAttachFlowNodeApproveNowItem = (desTaskAttachFlowNodeItems.Where(x => x.FlowNodeStatus == DBSql.Design.FlowNodeStatus.进行中.ToString("D"))).ToArray();
                    thisNode.SetAttribute("DesignEmpId", desTaskAttachFlowNodeDesignItem.Length == 0 ? "" : desTaskAttachFlowNodeDesignItem[0].FlowNodeEmpID.ToString());
                    thisNode.SetAttribute("DesignEmpName", desTaskAttachFlowNodeDesignItem.Length == 0 ? "" : desTaskAttachFlowNodeDesignItem[0].FlowNodeEmpName.ToString());
                    thisNode.SetAttribute("DesignNodeType", desTaskAttachFlowNodeDesignItem.Length == 0 ? "" : desTaskAttachFlowNodeDesignItem[0].FlowNodeTypeID.ToString());
                    thisNode.SetAttribute("DesignNodeName", desTaskAttachFlowNodeDesignItem.Length == 0 ? "" : desTaskAttachFlowNodeDesignItem[0].FlowNodeName.ToString());
                    thisNode.SetAttribute("ApproveNowEmpId", desTaskAttachFlowNodeApproveNowItem.Length == 0 ? "" : desTaskAttachFlowNodeApproveNowItem[0].FlowNodeEmpID.ToString());
                    thisNode.SetAttribute("ApproveNowEmpName", desTaskAttachFlowNodeApproveNowItem.Length == 0 ? "" : desTaskAttachFlowNodeApproveNowItem[0].FlowNodeEmpName.ToString());
                    thisNode.SetAttribute("ApproveNowNodeType", desTaskAttachFlowNodeApproveNowItem.Length == 0 ? "" : desTaskAttachFlowNodeApproveNowItem[0].FlowNodeTypeID.ToString());
                    thisNode.SetAttribute("ApproveNowNodeName", desTaskAttachFlowNodeApproveNowItem.Length == 0 ? "" : desTaskAttachFlowNodeApproveNowItem[0].FlowNodeName.ToString());
                }

                //thisNode.SetAttribute("AttachFlowNodeJson", attachFlowNodeJson);
            }

            parentNode.AppendChild(thisNode);

            var childAttachs = from a in dtAttachs
                               where TypeDescriptor.GetProperties(a).Find("_parentId", true).GetValue(a).ToString() == attachProp.Find("AttachID", true).GetValue(attach).ToString()
                               select a;
            foreach (var childAttach in childAttachs)
            {
                GetDrawingsXMLSerializerNode(xdoc, thisNode, dtAttachs, childAttach);
            }
        }



        /// <summary>
        /// 获取 当前文件审批信息
        /// </summary>
        private void GetSignsXML()
        {
            //192.168.0.222/JQWebMVC/Design/GCADAPI/Process?command=GetSignsXML&a=周军&p=1A1DC91C907325C69271DDF0C944BC72&TaskId=295&Attachs=&DesignDate=2017-02-08
            //192.168.0.222/JQWebMVC/Design/GCADAPI/Process?command=GetSignsXML&a=周军&p=1A1DC91C907325C69271DDF0C944BC72&TaskId=295&Attachs=[{AttachID:"501381", AttachVer:"1"}]&DesignDate=2017-02-08

            var result = new RequestResult();
            DataTable dtMain = null;
            DataTable dtSplit = null;

            try
            {
                DataModel.Models.BaseEmployee emp = CheckEmpLogin();
                string[] ids = Request.Params["TaskId"].Split('_');
                string id = "";
                if (ids.Length > 1)
                {
                    id = ids[1];
                }
                else
                {
                    id = ids[0];
                }


                // 获取参数
                var signTaskId = Common.ModelConvert.ConvertToDefaultType<long>(id);   // 任务id
                var signAttachs = JQ.Util.JavaScriptSerializerUtil.parseFormJson<List<DataModel.Models.BaseAttach>>(Request.Params["Attachs"]); // 要签名的附件，空的话返回任务中所有已校审完毕的文件的签名信息
                var taskInfo = new DBSql.Design.DesTask().Get(signTaskId);
                var taskName = new DBSql.Design.DesTask().GetTaskTextPath(taskInfo.Id);
                var designDate = Common.ModelConvert.ConvertToDefaultType<string>(Request.Params["DesignDate"]);   // 制图日期

                if (string.IsNullOrWhiteSpace(designDate))
                {
                    //获取出制图日期
                    if (!string.IsNullOrEmpty(taskInfo.ColAttXml))
                    {
                        var design_date = "";
                        design_date = Common.XmlConvert.GetXmlValue(taskInfo.ColAttXml, "Design_Date");
                        if (String.IsNullOrWhiteSpace(design_date))
                        {
                            designDate = DateTime.Now.ToString("yyyy-MM-dd");
                        }
                    }
                }

                // 获取签名文件
                if (signAttachs == null)
                {
                    // 按 格式 获取任务内已走完流程文件的拆封文件
                    dtMain = new DBSql.Design.DesSign().GetTaskAttachSignData(signTaskId, "'.dwg'");
                    //dtSplit = new DBSql.Design.DesSign().GetTaskSplitAttachSignData(signTaskId, "'pdf','dwg'");
                }
                else
                {
                    // 获取任务内用户选定的已走完流程文件的拆封文件
                    dtMain = new DBSql.Design.DesSign().GetAttachsSignData(String.Join(",", signAttachs.Select(a => a.AttachID).ToArray()));
                    //dtSplit = new DBSql.Design.DesSign().GetSplitAttachsSignData(String.Join(",", signAttachs.Select(a => a.AttachID).ToArray()));
                }

                // 创建 设计文件统一PDF签名处理程序
                DBSql.Design.DesSignPDF signPDF = new DBSql.Design.DesSignPDF();

                // 创建 设计文件统一DWG签名处理程序
                DBSql.Design.DesSignDWG signDWG = new DBSql.Design.DesSignDWG();

                // 创建 设计文件统一DWG签名处理程序(未拆封源文件)
                DBSql.Design.DesSignDWG signMain = new DBSql.Design.DesSignDWG();

                if (dtSplit != null)
                {
                    for (int i = 0; i < dtSplit.Rows.Count; i++)
                    {
                        var signAttach = dtSplit.Rows[i];
                        var ext = signAttach["Extension"].ToString().ToLower();

                        // 获取签名信息
                        DBSql.Design.Dto.DesTaskAttachSignInfo signInfo = new DBSql.Design.DesSign().GetTaskAttachSignInfo(signAttach, designDate);

                        DBSql.Design.DesTask dbt = new DBSql.Design.DesTask();

                        var desModel = dbt.FirstOrDefault(x => x.TaskSpecId == taskInfo.TaskSpecId && x.ProjId == taskInfo.ProjId && x.TaskType == 1 && x.TaskGroupId == taskInfo.TaskGroupId);

                        DataModel.Models.Project proj = new DBSql.Project.Project().Get(taskInfo.ProjId);

                        signInfo.ApproveSigns.Add("项目负责", proj.ProjEmpName);
                        signInfo.ApproveSigns.Add("项目负责人1", proj.FProjEmpName);
                        signInfo.ApproveSigns.Add("专业负责", desModel.TaskEmpName);
                        signInfo.ApproveSigns.Add("HQ项目负责", proj.ProjEmpName);
                        signInfo.ApproveSigns.Add("HQ专业负责", desModel.TaskEmpName);
                        signInfo.ApproveSigns.Add("HQ项目负责人1", proj.FProjEmpName);
                        // 放到对应的签名处理程序中
                        if (ext == "pdf")
                        {

                            //signPDF.EmpID = emp.EmpID;
                            //signPDF.EmpName = emp.EmpName;
                            //signPDF.TaskId = signTaskId;
                            //signPDF.TaskName = taskName;
                            signPDF.SignFiles.Add(signInfo);
                        }
                        else if (ext == "dwg")
                        {
                            //signDWG.EmpID = emp.EmpID;
                            //signDWG.EmpName = emp.EmpName;
                            //signDWG.TaskId = signTaskId;
                            //signDWG.TaskName = taskName;
                            signDWG.SignFiles.Add(signInfo);
                        }
                    }
                }

                if (dtMain != null)
                {
                    for (int i = 0; i < dtMain.Rows.Count; i++)
                    {
                        var signAttach = dtMain.Rows[i];
                        var ext = signAttach["AttachExt"].ToString().ToLower();

                        // 获取签名信息
                        DBSql.Design.Dto.DesTaskAttachSignInfo signInfo = new DBSql.Design.DesSign().GetTaskAttachSignInfo(signAttach, designDate);

                        DBSql.Design.DesTask dbt = new DBSql.Design.DesTask();

                        var desModel = dbt.FirstOrDefault(x => x.TaskSpecId == taskInfo.TaskSpecId && x.ProjId == taskInfo.ProjId && x.TaskType == 1 && x.TaskGroupId == taskInfo.TaskGroupId);

                        DataModel.Models.Project proj = new DBSql.Project.Project().Get(taskInfo.ProjId);

                        signInfo.ApproveSigns.Add("项目负责", proj.ProjEmpName);
                        signInfo.ApproveSigns.Add("项目负责人1", proj.FProjEmpName);
                        signInfo.ApproveSigns.Add("专业负责", desModel.TaskEmpName);
                        signInfo.ApproveSigns.Add("HQ项目负责", proj.ProjEmpName);
                        signInfo.ApproveSigns.Add("HQ专业负责", desModel.TaskEmpName);
                        signInfo.ApproveSigns.Add("HQ项目负责人1", proj.FProjEmpName);


                        // 放到对应的签名处理程序中
                        if (ext == ".dwg")
                        {
                            //signDWG.EmpID = emp.EmpID;
                            //signDWG.EmpName = emp.EmpName;
                            //signDWG.TaskId = signTaskId;
                            //signDWG.TaskName = taskName;
                            signMain.SignFiles.Add(signInfo);
                        }
                    }
                }

                result.data = new DBSql.Design.Dto.DesTaskAttachSigns
                {
                    EmpID = emp.EmpID,
                    EmpName = emp.EmpName,
                    TaskId = signTaskId,
                    TaskName = taskName,
                    SignPDF = signPDF,
                    SignDWG = signDWG,
                    SignMain = signMain
                };
                result.status = 1;
                result.code = 0;
                result.msg = "";
            }
            catch (Exception ex)
            {
                result.status = -1;
                result.code = ex.GetHashCode();
                result.msg = ex.Message;
            }

            GetSignsXMLSerializer(result);
        }

        /// <summary>
        /// 解析 当前文件审批信息 为 CAD工具支持的 Drawings.xml 格式
        /// </summary>
        /// <param name="result"></param>
        private void GetSignsXMLSerializer(RequestResult result)
        {
            //string weburl = String.Format("{0}://{1}:{2}{3}", Request.Url.Scheme, Request.Url.Host, Request.Url.Port, Url.Content("~"));
            string weburl = String.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Headers["Host"], Url.Content("~"));

            XmlDocument xdoc = new XmlDocument();
            xdoc.AppendChild(xdoc.CreateXmlDeclaration("1.0", Encoding.UTF8.WebName, "yes"));
            XmlElement rootNode = xdoc.CreateElement("Signs");
            rootNode.SetAttribute("status", result.status.ToString());
            rootNode.SetAttribute("code", result.code.ToString());
            rootNode.SetAttribute("msg", result.msg.ToString());
            xdoc.AppendChild(rootNode);

            if (result.data != null)
            {
                rootNode.SetAttribute("WebUrl", weburl);
                rootNode.SetAttribute("AttachUrl", weburl + "core/ProcessFile/Download?type=attach&id="); // 上传附件下载路径
                rootNode.SetAttribute("DesignSplitFileUrl", weburl + "core/ProcessFile/Download?type=DesignSplitFile&id="); // 拆图文件下载路径
                rootNode.SetAttribute("DesignSplitFileSignUrl", weburl + "core/ProcessFile/Download?type=DesignSplitFileSign&id="); // 拆图文件签名结果文件下载路径
                rootNode.SetAttribute("SignImageUrl", weburl + ConfigurationManager.AppSettings["SignImageDirectory"].ToString() + "/"); // 签名人员图片下载路径

                var signs = (DBSql.Design.Dto.DesTaskAttachSigns)result.data;
                rootNode.SetAttribute("TaskId", signs.TaskId.ToString());
                rootNode.SetAttribute("TaskName", signs.TaskName.ToString());
                rootNode.SetAttribute("EmpID", signs.EmpID.ToString());
                rootNode.SetAttribute("EmpName", signs.EmpName.ToString());

                var signPDF = signs.SignPDF;
                XmlElement xSignPDF = xdoc.CreateElement("SignPDF");
                rootNode.AppendChild(xSignPDF);
                GetSignsXMLSerializerNode(xdoc, xSignPDF, signPDF.SignFiles);

                var signDWG = signs.SignDWG;
                XmlElement xSignDWG = xdoc.CreateElement("SignDWG");
                rootNode.AppendChild(xSignDWG);
                GetSignsXMLSerializerNode(xdoc, xSignDWG, signDWG.SignFiles);

                var signMain = signs.SignMain;
                XmlElement xSignMain = xdoc.CreateElement("SignMain");
                rootNode.AppendChild(xSignMain);
                GetSignsXMLSerializerNode(xdoc, xSignMain, signMain.SignFiles);
            }

            Response.Clear();
            Response.ContentType = "text/xml";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            xdoc.Save(Response.Output);
            Response.End();
        }

        /// <summary>
        /// 解析 当前文件审批信息 某节点
        /// </summary>
        /// <param name="xdoc"></param>
        /// <param name="parentNode"></param>
        /// <param name="signFiles"></param>
        private void GetSignsXMLSerializerNode(XmlDocument xdoc, XmlElement parentNode, List<DBSql.Design.Dto.DesTaskAttachSignInfo> signFiles)
        {
            if (signFiles != null)
            {
                foreach (var signFile in signFiles)
                {
                    XmlElement xSignFile = xdoc.CreateElement("SignFile");
                    xSignFile.SetAttribute("ID", signFile.ID.ToString());
                    xSignFile.SetAttribute("Name", signFile.Name.ToString());
                    xSignFile.SetAttribute("Size", signFile.Size.ToString());
                    xSignFile.SetAttribute("Path", signFile.Path.ToString());
                    xSignFile.SetAttribute("Extension", signFile.Extension.ToString());
                    xSignFile.SetAttribute("BaseAttachID", signFile.BaseAttachID.ToString());
                    xSignFile.SetAttribute("BaseAttachVer", signFile.BaseAttachVer.ToString());
                    xSignFile.SetAttribute("BaseAttachVerID", signFile.BaseAttachVerID.ToString());
                    parentNode.AppendChild(xSignFile);

                    XmlElement xApproveSigns = xdoc.CreateElement("ApproveSigns");
                    xSignFile.AppendChild(xApproveSigns);
                    foreach (var signInfo in signFile.ApproveSigns)
                    {
                        XmlElement xSignInfo = xdoc.CreateElement("SignInfo");
                        string txt = signInfo.Key.ToString().Contains("人") ? signInfo.Key : signInfo.Key + "人";
                        xSignInfo.SetAttribute("Key", txt);
                        xSignInfo.SetAttribute("Value", signInfo.Value.ToString());
                        xApproveSigns.AppendChild(xSignInfo);
                    }


                    XmlElement xMuiltiSigns = xdoc.CreateElement("MuiltiSigns");
                    xSignFile.AppendChild(xMuiltiSigns);
                    foreach (var signInfo in signFile.MuiltiSigns)
                    {
                        XmlElement xSignInfo = xdoc.CreateElement("SignInfo");
                        xSignInfo.SetAttribute("Key", signInfo.Key.Replace("会签","专业").ToString());
                        xSignInfo.SetAttribute("Value", signInfo.Value.ToString());
                        xApproveSigns.AppendChild(xSignInfo);
                    }
                }
            }
        }



        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        private string UploadAttach()
        {
            var fileID = Request.Params["FileId"];
            var chunks = JQ.Util.TypeParse.parse<int>(Request.Params["chunks"]);
            var chunk = JQ.Util.TypeParse.parse<int>(Request.Params["chunk"]);
            var savePath = "";
            if (chunk == 0 || Session[fileID] == null)
            {
                if (Request.Params["name"] == null)
                {
                    return JQ.Util.JavaScriptSerializerUtil.getJson(new RequestResult() { status = 0, msg = "未找到可用的文件名称" });
                }
                var path = JQ.Util.IOUtil.GetTempPath();
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                savePath = Path.Combine(path, Guid.NewGuid().ToString());
                if (chunks > 1)
                {
                    Session[fileID] = savePath;
                }
            }
            else
            {
                savePath = Session[fileID].ToString();
                if (chunks == chunk + 1)
                {
                    Session.Remove(fileID);
                }
            }
            byte[] buffer = null;
            if (Request.ContentType == "application/octet-stream" && Request.ContentLength > 0)
            {
                buffer = new Byte[Request.InputStream.Length];
                Request.InputStream.Read(buffer, 0, buffer.Length);
            }
            else if (Request.ContentType.Contains("multipart/form-data") && Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                buffer = new Byte[Request.Files[0].InputStream.Length];
                Request.Files[0].InputStream.Read(buffer, 0, buffer.Length);
            }
            using (var fs = new FileStream(savePath, FileMode.Append))
            {
                fs.Write(buffer, 0, buffer.Length);
            }
            if (chunks == chunk + 1)
            {
                //文件真实名字
                var realName = Request.Params["name"];
                if (string.IsNullOrEmpty(realName))
                {
                    return JQ.Util.JavaScriptSerializerUtil.getJson(new RequestResult() { status = 0, msg = "未找到可用的文件名称" });
                }
                var parentID = Request.Params["ParentId"];
                if (string.IsNullOrEmpty(parentID))
                {
                    //为原图上传完毕，开始处理所有的图纸
                    UploadAttachFinish(fileID, savePath, realName);
                }
                else
                {
                    System.IO.File.Move(savePath, Path.Combine(Path.GetDirectoryName(savePath), parentID + "_" + realName));
                }
            }
            return JQ.Util.JavaScriptSerializerUtil.getJson(new RequestResult() { status = 1 });
        }


        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        private string UploadAttachQM()
        {
            var fileID = Request.Params["FileId"];
            var chunks = JQ.Util.TypeParse.parse<int>(Request.Params["chunks"]);
            var chunk = JQ.Util.TypeParse.parse<int>(Request.Params["chunk"]);
            var savePath = "";
            if (chunk == 0 || Session[fileID] == null)
            {
                if (Request.Params["name"] == null)
                {
                    return JQ.Util.JavaScriptSerializerUtil.getJson(new RequestResult() { status = 0, msg = "未找到可用的文件名称" });
                }
                var path = JQ.Util.IOUtil.GetTempPath();
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                savePath = Path.Combine(path, Guid.NewGuid().ToString());
                if (chunks > 1)
                {
                    Session[fileID] = savePath;
                }
            }
            else
            {
                savePath = Session[fileID].ToString();
                if (chunks == chunk + 1)
                {
                    Session.Remove(fileID);
                }
            }
            byte[] buffer = null;
            if (Request.ContentType == "application/octet-stream" && Request.ContentLength > 0)
            {
                buffer = new Byte[Request.InputStream.Length];
                Request.InputStream.Read(buffer, 0, buffer.Length);
            }
            else if (Request.ContentType.Contains("multipart/form-data") && Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                buffer = new Byte[Request.Files[0].InputStream.Length];
                Request.Files[0].InputStream.Read(buffer, 0, buffer.Length);
            }
            using (var fs = new FileStream(savePath, FileMode.Append))
            {
                fs.Write(buffer, 0, buffer.Length);
            }
            if (chunks == chunk + 1)
            {
                //文件真实名字
                var realName = Request.Params["name"];
                if (string.IsNullOrEmpty(realName))
                {
                    return JQ.Util.JavaScriptSerializerUtil.getJson(new RequestResult() { status = 0, msg = "未找到可用的文件名称" });
                }
                var parentID = Request.Params["ParentId"];
                if (string.IsNullOrEmpty(parentID))
                {
                    //为原图上传完毕，开始处理所有的图纸
                    UploadAttachFinishQM(fileID, savePath, realName);
                }
                else
                {
                    System.IO.File.Move(savePath, Path.Combine(Path.GetDirectoryName(savePath), parentID + "_" + realName));
                }
            }
            return JQ.Util.JavaScriptSerializerUtil.getJson(new RequestResult() { status = 1 });
        }

        /// <summary>
        /// 为原图上传完毕，开始处理所有的图纸
        /// </summary>
        /// <param name="fileID"></param>
        /// <param name="filePath"></param>
        /// <param name="realName"></param>
        private void UploadAttachFinishQM(string fileID, string filePath, string realName)
        {
            TaskNode taskDetail = null;
            if (Request.Params["TaskInfo"] != null)
            {
                try
                {
                    taskDetail = JQ.Util.JavaScriptSerializerUtil.parseFormJson<TaskNode>(Request.Params["TaskInfo"]);
                }
                catch
                {
                    return;
                }
            }
            if (taskDetail == null)
            {
                return;
            }
            var splitedFiles = new List<string>();
            foreach (var file in Directory.GetFiles(Path.GetDirectoryName(filePath), fileID + "_*.*"))
            {
                //处理数据
                if (!System.IO.File.Exists(file))
                {
                    continue;
                }
                //插入数据
                splitedFiles.Add(file);
            }

            using (var ba = new DBSql.Sys.BaseAttach())
            {
                string[] ids = taskDetail.Id.Split('_');
                if (ids.Length > 1)
                {
                    taskDetail.Id = ids[1];
                }
                else
                {
                    taskDetail.Id = ids[0];
                }
                ba.SaveDWGFromGCADQM(JQ.Util.TypeParse.parse<int>(taskDetail.Id), JQ.Util.TypeParse.parse<int>(fileID), filePath, realName, splitedFiles, JQ.Util.TypeParse.parse<int>(taskDetail.TaskEmpId), taskDetail.TaskEmpName);
            }
        }



        /// <summary>
        /// 为原图上传完毕，开始处理所有的图纸
        /// </summary>
        /// <param name="fileID"></param>
        /// <param name="filePath"></param>
        /// <param name="realName"></param>
        private void UploadAttachFinish(string fileID, string filePath, string realName)
        {
            TaskNode taskDetail = null;
            if (Request.Params["TaskInfo"] != null)
            {
                try
                {
                    taskDetail = JQ.Util.JavaScriptSerializerUtil.parseFormJson<TaskNode>(Request.Params["TaskInfo"]);
                }
                catch
                {
                    return;
                }
            }
            if (taskDetail == null)
            {
                return;
            }
            var splitedFiles = new List<string>();
            foreach (var file in Directory.GetFiles(Path.GetDirectoryName(filePath), fileID + "_*.*"))
            {
                //处理数据
                if (!System.IO.File.Exists(file))
                {
                    continue;
                }
                //插入数据
                splitedFiles.Add(file);
            }
            using (var ba = new DBSql.Sys.BaseAttach())
            {
                string[] ids = taskDetail.Id.Split('_');
                if (ids.Length > 1)
                {
                    taskDetail.Id = ids[1];
                }
                else
                {
                    taskDetail.Id = ids[0];
                }
                ba.SaveDWGFromGCAD(JQ.Util.TypeParse.parse<int>(taskDetail.Id), filePath, realName, splitedFiles, JQ.Util.TypeParse.parse<int>(taskDetail.TaskEmpId), taskDetail.TaskEmpName);
            }
        }
    }
}