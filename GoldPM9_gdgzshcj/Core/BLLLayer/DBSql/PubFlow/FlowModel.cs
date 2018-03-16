using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Extensions;
using System.Data;

namespace DBSql.PubFlow
{
    public class FlowModel : EFRepository<DataModel.Models.FlowModel>
    {
        /// <summary>
        /// 创建新的流程模版
        /// </summary>
        public void CreateOrUpdate(DataModel.Models.FlowModel flowModel)
        {
            if (flowModel.Id > 0)
            {
                base.DbContext.SaveChanges();
            }
            else
            {
                using (var tran = base.DbContext.Database.BeginTransaction())
                {
                    try
                    {
                        base.DbContext.Set<DataModel.Models.FlowModel>().Add(flowModel);
                        base.DbContext.SaveChanges();
                        //插入
                        var node0 = new DataModel.Models.FlowModelNode()
                        {
                            FK_FlowModelNode_FlowModelID = flowModel,
                            NodeName = "开始",
                            NodeOrder = 0,
                            NodeValidateGroup = "0"
                        };
                        base.DbContext.Set<DataModel.Models.FlowModelNode>().Add(node0);
                        base.DbContext.SaveChanges();
                        var node1 = new DataModel.Models.FlowModelNode()
                        {
                            FK_FlowModelNode_FlowModelID = flowModel,
                            NodeName = "步骤1",
                            NodeOrder = 1,
                            NodeParentID = node0.Id,
                            NodeAppIsLastTime = true,
                            NodeValidateGroup = "0"
                        };
                        base.DbContext.Set<DataModel.Models.FlowModelNode>().Add(node1);
                        base.DbContext.SaveChanges();
                        var node2 = new DataModel.Models.FlowModelNode()
                        {
                            FK_FlowModelNode_FlowModelID = flowModel,
                            NodeName = "步骤2",
                            NodeOrder = 2,
                            NodeParentID = node1.Id,
                            NodeAppIsLastTime = true,
                            NodeValidateGroup = "0"
                        };
                        base.DbContext.Set<DataModel.Models.FlowModelNode>().Add(node2);
                        base.DbContext.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                    }
                }
            }
        }

        public bool Delete(int[] idSet)
        {
            using (var tran = base.DbContext.Database.BeginTransaction())
            {
                try
                {
                    base.DbContext.Set<DataModel.Models.FlowModel>().Where(m => idSet.Contains(m.Id)).Delete();
                    base.DbContext.Set<DataModel.Models.FlowModelNode>().Where(m => idSet.Contains(m.FlowModelID)).Delete();
                    tran.Commit();
                    return true;
                }
                catch
                {
                    tran.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// 获取出项目流程
        /// </summary>
        /// <returns></returns>
        public DataTable GetProjectFlow()
        {
            //return DAL.DBExecute.ExecuteDataTable("SELECT Id AS id,ModelName as text,ModelUrl as url,ModelRefTable FROM FlowModel WHERE ModeModular='1' AND ModelXml.value('(Root/@IsRefProject)[1]','nvarchar')='1'");
            return DAL.DBExecute.ExecuteDataTable(@"SELECT  Id AS id ,
                                            ModelName AS text,
                                            ModelUrl AS url,
                                            ModelRefTable
                                    FROM    FlowModel
                                    WHERE   ModelRefTable IN('IsoSJCH', 'IsoRemark', 'IsoZYZDJYB', 'IsoSJPSJYB',
                                                               'IsoBCD', 'IsoDesignReturn', 'IsoDWGZLXD',
                                                               'IsoBGSJRWTZD', 'IsoBCRWTZD', 'IsoRWPSTZD',
                                                               'IsoGCDZKCTJDJZ', 'IsoConstructionSet',
                                                               'IsoConstructionCoordination', 'IsoConsultRecord',
                                                               'IsoTechnologyChange', 'IsoSJBGD', 'IsoGCCLTJD')");
        }

        /// <summary>
        /// 根据别名查找
        /// </summary>
        /// <param name="eng"></param>
        /// <returns></returns>
        public DataModel.Models.FlowModel GetModelByRefTable(string reftable)
        {
            string sql = string.Format("select id from FlowModel where ModelRefTable='{0}'", reftable);
            var obj = DAL.DBExecute.ExecuteScalar(sql);
            if (obj == null)
            {
                return null;
            }
            return GetQuery(x => x.Id == (int)obj).First();
        }
    }
}
