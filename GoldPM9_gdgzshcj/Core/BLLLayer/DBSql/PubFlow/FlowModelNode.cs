using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSql.PubFlow
{
    public class FlowModelNode : EFRepository<DataModel.Models.FlowModelNode>
    {
        public int AppendPreviousNode(int modelNodeID)
        {
            var modelNode = this.Get(modelNodeID);
            if (modelNode == null)
            {
                return 0;
            }
            var toCreateNode = new DataModel.Models.FlowModelNode()
            {
                FlowModelID = modelNode.FlowModelID,
                NodeName = "新节点",
                NodeOrder = modelNode.NodeOrder,
                NodeParentID = modelNode.NodeParentID,
                NodeAppIsLastTime = true,
                NodeValidateGroup = "0"
            };
            using (var tran = DbContext.Database.BeginTransaction())
            {
                try
                {
                    //获取出后续d的模版节点
                    var nextNodes = this.DbContext.Set<DataModel.Models.FlowModelNode>().Where(m => m.FlowModelID == modelNode.FlowModelID && m.NodeOrder >= modelNode.NodeOrder).ToList();
                    //插入新的模版节点
                    this.DbContext.Set<DataModel.Models.FlowModelNode>().Add(toCreateNode);
                    this.DbContext.SaveChanges();
                    var temp = nextNodes.FirstOrDefault(m => m.Id == modelNodeID);
                    if (temp != null)
                    {
                        temp.NodeParentID = toCreateNode.Id;
                        while (temp != null)
                        {
                            temp.NodeOrder++;
                            temp = nextNodes.FirstOrDefault(m => m.NodeParentID == temp.Id);
                        }
                        this.DbContext.SaveChanges();
                    }
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                }
            }
            return toCreateNode.FlowModelID;
        }

        /// <summary>
        /// 添加下级节点
        /// </summary>
        /// <param name="modelNodeID"></param>
        public int AppendNextNode(int modelNodeID)
        {
            var modelNode = this.Get(modelNodeID);
            if (modelNode == null)
            {
                return 0;
            }
            var toCreateNode = new DataModel.Models.FlowModelNode()
            {
                FlowModelID = modelNode.FlowModelID,
                NodeName = "新节点",
                NodeOrder = modelNode.NodeOrder + 1,
                NodeParentID = modelNode.Id,
                NodeAppIsLastTime = true,
                NodeValidateGroup = "0"
            };
            using (var tran = DbContext.Database.BeginTransaction())
            {
                try
                {
                    var nextNodes = this.DbContext.Set<DataModel.Models.FlowModelNode>().Where(m => m.FlowModelID == modelNode.FlowModelID && m.NodeOrder > modelNode.NodeOrder).ToList();
                    this.DbContext.Set<DataModel.Models.FlowModelNode>().Add(toCreateNode);
                    this.DbContext.SaveChanges();
                    var temp = nextNodes.FirstOrDefault(m => m.NodeParentID == modelNode.Id);
                    if (temp != null)
                    {
                        temp.NodeParentID = toCreateNode.Id;
                        while (temp != null)
                        {
                            temp.NodeOrder++;
                            temp = nextNodes.FirstOrDefault(m => m.NodeParentID == temp.Id);
                        }
                        this.DbContext.SaveChanges();
                    }
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                }
            }
            return toCreateNode.FlowModelID;
        }

        public int DeleteNode(int modelNodeID)
        {
            var modelNode = this.Get(modelNodeID);
            if (modelNode == null)
            {
                throw new Common.JQException("未找到节点！");
            }
            else if (modelNode.NodeOrder == 0)
            {
                throw new Common.JQException("开始节点不允许被删除！");
            }
            using (var tran = DbContext.Database.BeginTransaction())
            {
                try
                {
                    //获取后续的节点
                    var nodes = DbContext.Set<DataModel.Models.FlowModelNode>().Where(m => m.FlowModelID == modelNode.FlowModelID && m.NodeOrder > modelNode.NodeOrder);
                    var temp = nodes.FirstOrDefault(m => m.NodeParentID == modelNode.Id);
                    if (temp != null)
                    {
                        temp.NodeParentID = modelNode.NodeParentID;
                        while (temp != null)
                        {
                            temp.NodeOrder--;
                            temp = nodes.FirstOrDefault(m => m.NodeParentID == temp.Id);
                        }
                    }
                    DbContext.Set<DataModel.Models.FlowModelNode>().Remove(modelNode);
                    DbContext.SaveChanges();
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                }
            }
            return modelNode.FlowModelID;
        }
    }
}
