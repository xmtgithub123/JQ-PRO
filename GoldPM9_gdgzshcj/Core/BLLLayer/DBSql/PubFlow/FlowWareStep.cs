using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DBSql.PubFlow
{
    [Serializable]
    public class FlowWareStep
    {
        public int NodeID
        {
            get; set;
        }

        public int NodeType
        {
            get; set;
        }

        public string NodeName
        {
            get; set;
        }

        public string ChoosedUsers
        {
            get; set;
        }

        public int Order
        {
            get; set;
        }

        public string ChoosedUserNames
        {
            get; set;
        }

        public int DefaultChoosedUser
        {
            get; set;
        }

        public static FlowWareStep GetStep(DataModel.Models.FlowModelNode modelNode, DBSql.Sys.BaseEmployee baseEmployee, DataModel.EmpSession currentUser, DbContext dbContext)
        {
            var result = new FlowWareStep();
            result.NodeID = modelNode.Id;
            result.NodeName = modelNode.NodeName + (modelNode.NodeTypeID == -1 ? "（会签）" : "");
            result.NodeType = modelNode.NodeTypeID;
            result.Order = modelNode.NodeOrder;
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(modelNode.NodeXml);
            List<DataModel.Models.BaseEmployee> emps = null;
            //为会签时由用户在界面选择人员
            if (xmlDocument.DocumentElement.GetAttribute("IsDepDirector") == "1")
            {
                //当前步骤为获取出本部门的部门主管
                var baseData = new DBSql.Sys.BaseData().GetBaseDataInfo(currentUser.EmpDepID);
                if (baseData != null)
                {
                    emps = GetEmps(baseData.BaseAtt5, baseEmployee);
                }
            }
            if (result.NodeType == -1)
            {
                if (emps != null && emps.Count > 0)
                {
                    result.ChoosedUsers = string.Join(",", emps.Select(m => m.EmpID.ToString()).ToList());
                    result.ChoosedUserNames = string.Join(",", emps.Select(m => m.EmpName.ToString()).ToList());
                }
                return result;
            }
            result.Users = new List<FlowWareUser>();
            if (emps != null && emps.Count > 0)
            {
                foreach (var emp in emps)
                {
                    result.AddToUser(emp.EmpID, emp.EmpName);
                }
            }
            else if (modelNode.NodeSameId > 0)
            {
                var samenode = dbContext.Set<DataModel.Models.FlowModelNode>().FirstOrDefault(m => m.Id == modelNode.NodeSameId);
                if (samenode != null && samenode.NodeOrder == 1)
                {
                    result.AddToUser(currentUser.EmpID, currentUser.EmpName);
                }
                else
                {
                    LoadUsers(result, modelNode, baseEmployee, currentUser);
                }
            }
            else
            {
                LoadUsers(result, modelNode, baseEmployee, currentUser);
            }
            //按照姓名拼音进行排序
            if (result.Users.Count == 0)
            {
                foreach (var emp in baseEmployee.GetListFromCache(m => 1 == 1))
                {
                    result.AddToUser(emp);
                }
            }
            result.Users = result.Users.Where(m => m.ID != 1).OrderBy(m => m.Order).ToList();
            return result;
        }

        private static void LoadUsers(FlowWareStep step, DataModel.Models.FlowModelNode modelNode, DBSql.Sys.BaseEmployee baseEmployee, DataModel.EmpSession currentUser)
        {
            if (modelNode.NodeThisDep)
            {
                //获取出当前部门的人
                foreach (var emp in baseEmployee.GetListFromCache(m => m.EmpDepID == currentUser.EmpDepID))
                {
                    step.AddToUser(emp);
                }
            }
            if (!string.IsNullOrEmpty(modelNode.NodeRoleS))
            {
                //获取角色人员
                foreach (var emp in GetByRoles(modelNode.NodeRoleS, baseEmployee))
                {
                    step.AddToUser(emp);
                }
            }
            if (!string.IsNullOrEmpty(modelNode.NodeEmpIDs))
            {
                //获取资格人员
                foreach (var emp in GetByEmps(modelNode.NodeEmpIDs, baseEmployee))
                {
                    step.AddToUser(emp);
                }
            }
        }

        public static FlowWareStep GetStep(DataModel.Models.FlowNode node, DBSql.Sys.BaseEmployee baseEmployee, int empDepID, DbContext dbContext, bool isBackMode, bool isTransferMode = false)
        {
            var result = new FlowWareStep();
            result.NodeID = node.Id;
            result.NodeName = node.FlowNodeName + (node.FlowNodeTypeID == -1 ? "（会签）" : ""); ;
            result.NodeType = node.FlowNodeTypeID;
            result.Order = node.FlowNodeOrder;
            if (result.NodeType == -1 && !isTransferMode)
            {
                //为会签时由用户在界面选择人员，先从会签表中读取出原先的人员
                var datas = dbContext.Set<DataModel.Models.FlowMultiSign>().Where(m => m.FlowNodeID == node.Id).Select(m => new { m.SignEmpId, m.SignEmpName }).ToList();
                if (datas.Count > 0)
                {
                    foreach (var item in datas)
                    {
                        if (!string.IsNullOrEmpty(result.ChoosedUsers))
                        {
                            result.ChoosedUsers += ",";
                            result.ChoosedUserNames += ",";
                        }
                        result.ChoosedUsers += item.SignEmpId.ToString();
                        result.ChoosedUserNames += item.SignEmpName;
                    }
                }
                else
                {
                    //验证是否为部门主管
                    XmlDocument xmlDocument1 = new XmlDocument();
                    xmlDocument1.LoadXml(node.FlowNodeXml);
                    if (xmlDocument1.DocumentElement.GetAttribute("IsDepDirector") == "1")
                    {
                        //当前步骤为获取出本部门的部门主管
                        var baseData = new DBSql.Sys.BaseData().GetBaseDataInfo(empDepID);
                        if (baseData != null)
                        {
                            var emps = GetEmps(baseData.BaseAtt5, baseEmployee);
                            if (emps != null && emps.Count > 0)
                            {
                                result.ChoosedUsers = string.Join(",", emps.Select(m => m.EmpID.ToString()).ToList());
                                result.ChoosedUserNames = string.Join(",", emps.Select(m => m.EmpName.ToString()).ToList());
                            }
                        }
                    }
                }
                return result;
            }
            result.NodeType = 0;
            result.Users = new List<FlowWareUser>();
            if (isBackMode && node.FlowNodeEmpId != 0)
            {
                var emp = baseEmployee.GetBaseEmployeeInfo(node.FlowNodeEmpId);
                if (emp != null)
                {
                    result.AddToUser(emp, isBackMode);
                    return result;
                }
            }
            var isContinue = true;
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(node.FlowNodeXml);
            if (xmlDocument.DocumentElement.GetAttribute("IsDepDirector") == "1")
            {
                //当前步骤为获取出本部门的部门主管
                var baseData = new DBSql.Sys.BaseData().GetBaseDataInfo(empDepID);
                if (baseData != null)
                {
                    var emps = GetEmps(baseData.BaseAtt5, baseEmployee);
                    if (emps != null && emps.Count > 0)
                    {
                        foreach (var emp in emps)
                        {
                            result.AddToUser(emp.EmpID, emp.EmpName);
                        }
                        isContinue = false;
                    }
                }
            }
            if (isContinue)
            {
                if (node.FlowNodeSameId > 0)
                {
                    var samenode = dbContext.Set<DataModel.Models.FlowNode>().Find(node.FlowNodeSameId);
                    if (samenode != null && samenode.FlowNodeEmpId > 0)
                    {
                        var emp = baseEmployee.GetBaseEmployeeInfo(samenode.FlowNodeEmpId);
                        if (emp != null)
                        {
                            result.AddToUser(emp);
                            return result;
                        }
                    }
                }
                if (node.FlowNodeNodeThisDep)
                {
                    //获取出当前部门的人
                    foreach (var emp in baseEmployee.GetListFromCache(m => m.EmpDepID == empDepID))
                    {
                        result.AddToUser(emp);
                    }
                }
                if (!string.IsNullOrEmpty(node.FlowNodeNodeRoles))
                {
                    //获取角色人员
                    foreach (var emp in GetByRoles(node.FlowNodeNodeRoles, baseEmployee))
                    {
                        result.AddToUser(emp);
                    }
                }
                if (!string.IsNullOrEmpty(node.FlowNodeEmpIDs))
                {
                    //获取资格人员
                    foreach (var emp in GetByEmps(node.FlowNodeEmpIDs, baseEmployee))
                    {
                        result.AddToUser(emp);
                    }
                }
                if (!isBackMode && !isTransferMode && node.FlowNodeEmpId > 0)
                {
                    var emp = baseEmployee.GetBaseEmployeeInfo(node.FlowNodeEmpId);
                    if (emp != null)
                    {
                        result.AddToUser(emp);
                        result.DefaultChoosedUser = emp.EmpID;
                    }
                }
            }
            //按照姓名拼音进行排序
            if (result.Users.Count == 0)
            {
                foreach (var emp in baseEmployee.GetListFromCache(m => 1 == 1))
                {
                    result.AddToUser(emp);
                }
            }
            result.Users = result.Users.Where(m => m.ID != 1).OrderBy(m => m.Order).ToList();
            return result;
        }

        /// <summary>
        /// 根据角色ID获取出人员
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<DataModel.Models.BaseEmployee> GetByRoles(string nodeRoleIDs, DBSql.Sys.BaseEmployee baseEmployee)
        {
            if (string.IsNullOrEmpty(nodeRoleIDs))
            {
                yield break;
            }
            //获取角色人员
            var roleIDs = new List<int>();
            foreach (var s in nodeRoleIDs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var temp = 0;
                if (int.TryParse(s, out temp))
                {
                    roleIDs.Add(temp);
                }
            }
            if (roleIDs.Count == 0)
            {
                yield break;
            }
            foreach (var emp in baseEmployee.GetAllBEbyRole(roleIDs))
            {
                yield return emp;
            }
        }

        /// <summary>
        /// 根据人员ID获取出人员
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<DataModel.Models.BaseEmployee> GetByEmps(string nodeEmpIDs, DBSql.Sys.BaseEmployee baseEmployee)
        {
            if (string.IsNullOrEmpty(nodeEmpIDs))
            {
                yield break;
            }
            var empIDs = new List<int>();
            foreach (var s in nodeEmpIDs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var temp = 0;
                if (int.TryParse(s, out temp))
                {
                    empIDs.Add(temp);
                }
            }
            if (empIDs.Count == 0)
            {
                yield break;
            }
            foreach (var emp in baseEmployee.GetListFromCache(m => empIDs.Contains(m.EmpID)))
            {
                yield return emp;
            }
        }

        public List<FlowWareUser> Users
        {
            get; set;
        }

        private void AddToUser(DataModel.Models.BaseEmployee baseEmployee, bool isBackMode = false)
        {
            if (this.Users.FirstOrDefault(m => m.ID == baseEmployee.EmpID) != null || (!isBackMode && baseEmployee.EmpIsDeleted))
            {
                return;
            }
            this.Users.Add(baseEmployee);
        }

        private void AddToUser(int id, string name)
        {
            if (this.Users.FirstOrDefault(m => m.ID == id) != null)
            {
                return;
            }
            this.Users.Add(new FlowWareUser(id, name));
        }

        private static List<DataModel.Models.BaseEmployee> GetEmps(string str, DBSql.Sys.BaseEmployee baseEmployeeDB)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            var result = new List<DataModel.Models.BaseEmployee>();
            foreach (var temp in str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var a = 0;
                if (int.TryParse(temp, out a))
                {
                    var emp = baseEmployeeDB.GetBaseEmployeeInfo(a);
                    if (emp != null)
                    {
                        result.Add(emp);
                    }
                }
            }
            return result;
        }
    }

    [Serializable]
    public class FlowWareUser
    {
        public FlowWareUser(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }

        public int ID
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public string Order
        {
            get; set;
        }

        public static implicit operator FlowWareUser(DataModel.Models.BaseEmployee be)
        {
            var user = new FlowWareUser(be.EmpID, be.EmpName);
            user.Order = be.FK_BaseEmployee_EmpDepID.BaseOrder + "_" + be.EmpOrder.ToString("00000000");
            return user;
        }
    }
}