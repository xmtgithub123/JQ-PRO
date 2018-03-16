﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-07-12 14:56:40
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Common.Data;
using Common.Data.Extenstions;
using DAL;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Xml;
using JQ.Util;
using System.Data.Entity;

namespace DBSql.Pay
{
    public class PayBalanceEngineering : EFRepository<DataModel.Models.PayBalanceEngineering>
    {
        /// <summary>
        /// 根据项目Id获取已分配产值
        /// </summary>
        /// <param name="ProjID"></param>
        /// <returns></returns>
        public decimal GetDistributeFee(int ProjID)
        {
            decimal fee = 0;
            var list = GetList(p => p.BalanceState == (int)DataModel.BalanceStatus.已结算 && p.EngineeringID == ProjID);
            if (list.Count() > 0)
            {
                fee = list.Select(p => p.EngAmountArrange).Sum();
            }
            return fee;
        } 

        /// <summary>
        /// 撤销结算信息
        /// </summary>
        /// <param name="list"></param>
        public void CancelBalance(List<int> list)
        {
            var payUsercount = new PayBalanceUserAccount();
            payUsercount.DbContextRepository(UnitOfWork,DbContext);
            var payEngi = new PayBalanceEngineering();
            payEngi.DbContextRepository(UnitOfWork,DbContext);
            try
            {
                UnitOfWork.BeginTransaction();
                payUsercount.Delete(p=>list.Contains(p.BalanceEngineeringID));
                payEngi.Delete(p=>list.Contains(p.Id));
                UnitOfWork.CommitTransaction();
            }
            catch(Exception ex)
            {
                UnitOfWork.RollBackTransaction();
            }
            
        }
        /// <summary>
        /// 获取待结算的项目数量
        /// </summary>
        /// <returns></returns>
        public int GetWaitingBalanceProjectCount()
        {
            return GetList(p => p.BalanceState == (int)DataModel.BalanceStatus.预结算&&p.BalanceLotID==0).Select(p => p.EngineeringID).Distinct().Count();
        }


        /// <summary>
        /// 获取 设校审 人员列表
        /// </summary>
        /// <param name="TaskGroupId"></param>
        /// <returns></returns>
        public DataTable GetEmpInfoByTask(long TaskGroupId)
        {     
            int DesignType=(int)DataModel.NodeType.设计;  //设计节点
            int CheckType = (int)DataModel.NodeType.校对; //校对节点
            int ReviewType = (int)DataModel.NodeType.审核;//审核节点
            string sql=
               @"     SELECT  s.TaskName  AS SpecName,"
               + "      s.TaskSpecId AS SpecId,"
               +"      t.Id AS TaskId ,"
               +"      t.TaskName,"
               +"      f.node.value('./@FlowNodeName', 'varchar(50)') AS FlowNodeName,"
               +"      f.node.value('./@FlowNodeTypeID', 'int') AS FlowNodeTypeID,"
               +"      f.node.value('./@FlowNodeEmpID', 'int') AS FlowNodeEmpID,"
               +"      f.node.value('./@FlowNodeEmpName', 'nvarchar(50)') AS FlowNodeEmpName"
               +"      FROM dbo.DesTask AS t"
               + "     INNER JOIN dbo.DesTask AS s ON t.TaskSpecId = s.TaskSpecId AND s.TaskType = 1 AND s.TaskGroupId = @TaskGroupId"
               + "     CROSS APPLY t.TaskFlowModel.nodes('root/item[@FlowNodeTypeID=sql:variable(\"@DesignType\") or @FlowNodeTypeID=sql:variable(\"@CheckType\") or @FlowNodeTypeID=sql:variable(\"@ReviewType\")]')"
               + "     AS f (node)"
               + "     WHERE   t.DeleterEmpId = 0"
               + "     AND t.TaskType = 0"                // 任务类型： 0 普通任务 1 专业任务
               + "     AND t.TaskSpecId <> 0"            //  排除汇总专业
               + "     AND t.TaskLevelType IN(0, 1)"    // 层级类型：0 无层级 1 子层级 2 父层级
                      //AND t.TaskStatus IN(3)      -- 任务状态：0 未安排（灰） 1 已安排（黄） 2 进行中（绿色） 3 完成（蓝色） 4 回退（红色）
               + "     AND t.TaskGroupId = @TaskGroupId"
               + "     ORDER BY s.TaskSpecId,"
               + "     t.Id,"
               + "     f.node.value('./@FlowNodeTypeID', 'int')";
            SqlParameter[] paramters = {
                new SqlParameter("@TaskGroupId",SqlDbType.BigInt,8),
                new SqlParameter("@DesignType",SqlDbType.Int),
                new SqlParameter("@CheckType",SqlDbType.Int),
                new SqlParameter("@ReviewType",SqlDbType.Int)
            };
            paramters[0].Value = TaskGroupId;
            paramters[1].Value = DesignType;
            paramters[2].Value = CheckType;
            paramters[3].Value = ReviewType;
            DataTable dt = DBExecute.ExecuteDataTable(DBExecute.ConnectionString,sql,paramters);
            return dt;
        }

        /// <summary>
        /// 获取设、校、审人员
        /// </summary>
        /// <param name="ProjId"></param>
        /// <param name="PhaseIdList"></param>
        /// <param name="isSingle"></param>
        /// <returns></returns>
        public DataTable GetEmpListByProjIdOrPhase(int ProjId,List<int>PhaseIdList,bool isSingle=true)
        {
            List<long> TaskGroupList = new List<long>();
            DBSql.Design.DesTask task = new Design.DesTask();
            if(isSingle)
            {
                TaskGroupList = task.GetList(p=>p.ProjId==ProjId&&PhaseIdList.Contains(p.TaskPhaseId)&&p.DeleterEmpId==0&&
                p.TaskType==0&&p.TaskSpecId!=0&&(p.TaskLevelType==0||p.TaskLevelType==1)).Select(p=>p.TaskGroupId).Distinct().ToList();
            }
            else
            {
                TaskGroupList = task.GetList(p => p.ProjId == ProjId  && p.DeleterEmpId == 0 &&
                p.TaskType == 0 && p.TaskSpecId != 0 && (p.TaskLevelType == 0 || p.TaskLevelType == 1)).Select(p => p.TaskGroupId).Distinct().ToList();

            }
            DataTable mergeTable = new DataTable();
            foreach(long Taskgroup in TaskGroupList)
            {
                DataTable data = GetEmpInfoByTask(Taskgroup);
                mergeTable.Merge(data, true);
            }
            return mergeTable;
        }
    }
}
