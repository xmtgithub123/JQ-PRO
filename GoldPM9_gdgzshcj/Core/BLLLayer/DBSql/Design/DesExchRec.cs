﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-07-19 21:50:51
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Common.Data;
using Common.Data.Extenstions;
using DAL;
using DataModel.Models;
using System.Data.SqlClient;
using System.Data;

namespace DBSql.Design
{
    public class DesExchRec : EFRepository<DataModel.Models.DesExchRec>
    {
        DBSql.PubFlow.Flow flow = new DBSql.PubFlow.Flow();
        DBSql.Design.DesExch exch = new DesExch();

        /// <summary>
        /// 收资信息的筛选条件
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Expression<Func<DataModel.Models.DesExchRec, bool>> GetFunc(Common.SqlPageInfo condition)
        {
            var TWhere = QueryBuild<DataModel.Models.DesExchRec>.True();
            #region   筛选条件
            foreach (System.Collections.DictionaryEntry de in condition.SelectCondtion)
            {
                if (de.Value == null || de.Value.ToString().Trim() == "") continue;
                if (de.Value.ToString().Trim() == "0") continue;
                switch (de.Key.ToString())
                {
                    case "ExchID":
                        var ExchID = Common.ModelConvert.ConvertToDefaultType<int>(de.Value);
                        TWhere = TWhere.And(p => p.Id == ExchID);//根据收资来查找信息
                        break;
                    case "DateLower":
                        var DateLower = Common.ModelConvert.ConvertToDefaultType<DateTime>(de.Value);
                        TWhere = TWhere.And(p => p.RecTime >= DateLower);//接收时间
                        break;
                    case "DateUpper":
                        var DateUpper = Common.ModelConvert.ConvertToDefaultType<DateTime>(de.Value).AddDays(1);
                        TWhere = TWhere.And(p => p.RecTime < DateUpper);//接收时间上限
                        break;
                    case "RecSpecId":
                        var RecSpecId = Common.ModelConvert.ConvertToDefaultType<int>(de.Value);
                        TWhere = TWhere.And(p => p.RecSpecId == RecSpecId);//收资专业
                        break;
                    case "RecEmpDepId":
                        var RecEmpDepId = Common.ModelConvert.ConvertToDefaultType<int>(de.Value);
                        TWhere = TWhere.And(p => p.RecEmpDepId == RecEmpDepId);//收资部门
                        break;
                    case "RecStatus":
                        //0 表示未接收  1表示退回  2 表示通过
                        int RecStatus = Common.ModelConvert.ConvertToDefaultType<int>(de.Value);
                        TWhere = TWhere.And(p => p.RecStatus == RecStatus);//接收状态
                        break;
                }
            }
            #endregion
            if (String.IsNullOrEmpty(condition.SelectOrder)) condition.SelectOrder = "Id desc";//默认倒序
            return TWhere;
        }



        /// <summary>
        /// 退回收提资文件
        /// </summary>
        /// <returns></returns>
        public int RefuseRecieve(DataModel.Models.DesExchRec desExchRecModel, DataModel.EmpSession empSession)
        {
            int result = 0;
            var desExchRec = new DBSql.Design.DesExchRec();
            var flow = new DBSql.PubFlow.Flow();
            string dbSql = "Update FlowNode set FlowNodeStatusID=@FlowNodeStatusID where " +
    "FlowID in (select app.Id from Flow app where app.FlowRefTable='DesExch' and app.FlowRefID=@ExchangeID)" +
    " and FlowNodeParentID=0";


            SqlParameter[] paras = new SqlParameter[]{
                        new SqlParameter("@ExchangeID",SqlDbType.Int),
                        new SqlParameter("@FlowNodeStatusID",SqlDbType.Int),
                        };
            paras[0].Value = desExchRecModel.ExchId;
            paras[1].Value = Convert.ToInt32(DataModel.NodeStatus.轮到);
            DAL.DBExecute.ExecuteNonQuery(dbSql, paras);




            desExchRec.DbContextRepository(this.UnitOfWork, this.DbContext);
            flow.DbContextRepository(this.UnitOfWork, this.DbContext);
            try
            {
                UnitOfWork.BeginTransaction();

                DataModel.Models.DesExchRec model = desExchRec.Get(desExchRecModel.Id);
                model.RecStatus = 1;//退回
                model.RecTime = DateTime.Now;
                model.RecContent = desExchRecModel.RecContent;//意见
                desExchRec.Edit(model);

                // 更新时间
                flow.Edit(p => p.FlowRefID == desExchRecModel.ExchId && p.FlowRefTable == "DesExch",
                    u => new DataModel.Models.Flow() { FlowFinishDate = new DateTime(1900, 1, 1) });

                UnitOfWork.CommitTransaction();

                DataModel.Models.Flow flowModel = flow.FirstOrDefault(p => p.FlowRefTable == "DesExch" && p.FlowRefID == desExchRecModel.ExchId);
                if (flowModel != null)
                {
                    DataModel.Models.FlowNodeExe exeModel = new FlowNodeExe();
                    exeModel.FlowID = flowModel.Id;
                    exeModel.ExeActionID = (int)DataModel.NodeAction.回退;
                    exeModel.ExeActionDate = System.DateTime.Now;
                    exeModel.ExeEmpId = empSession.EmpID;
                    exeModel.ExeEmpName = empSession.EmpName;
                    if (desExchRecModel.RecContent != "")
                    {
                        exeModel.ExeNote = desExchRecModel.RecContent;
                    }
                    else
                    {
                        exeModel.ExeNote = "不同意";
                    }
                    this.DbContext.Set<DataModel.Models.FlowNodeExe>().Add(exeModel);
                    this.DbContext.SaveChanges();
                }
                var t = JQ.Util.IO.MessageMonitor.NotifyAsync(desExchRecModel.RecEmpId, delegate (int _empID, JQ.Util.IO.MessageMonitor.NotifyOption _option)
                {
                    return new { action = "ChangeToDoExchRecAmount", amount = -1 };
                });
                result++;
            }
            catch
            {
                UnitOfWork.RollBackTransaction();
            }
            return result;
        }

        /// <summary>
        /// 接收文件
        /// </summary>
        /// <param name="recId"></param>
        /// <param name="empSession"></param>
        /// <returns></returns>
        public int AcceptDesExchRec(int recId, DataModel.EmpSession empSession)
        {
            int result = 0;
            try
            {
                DataModel.Models.DesExchRec desExchRecModel = this.Get(recId);
                desExchRecModel.RecTime = DateTime.Now;
                if (empSession != null && desExchRecModel.RecEmpId == 0)//没有指定接收人，则把点击接收当前用户作为收资人
                {
                    desExchRecModel.RecEmpId = empSession.EmpID;
                    desExchRecModel.RecEmpName = empSession.EmpName;
                    desExchRecModel.RecEmpDepId = empSession.EmpDepID;
                    desExchRecModel.RecEmpDepName = empSession.EmpDepName;
                }
                Edit(desExchRecModel);
                UnitOfWork.SaveChanges();
                result++;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 同意接收文件（添加流转记录以及更新状态）
        /// </summary>
        /// <param name="RecID"></param>
        /// <param name="empSession"></param>
        /// <returns></returns>
        public int AgreeRec(DataModel.Models.DesExchRec model, DataModel.EmpSession empSession)
        {
            int result = 0;
            try
            {
                DataModel.Models.DesExchRec desExchRecModel = this.Get(model.Id);
                desExchRecModel.RecStatus = 2;
                desExchRecModel.RecContent = model.RecContent;
                Edit(desExchRecModel);
                UnitOfWork.SaveChanges();

                DataModel.Models.Flow flowModel = flow.FirstOrDefault(p => p.FlowRefTable == "DesExch" && p.FlowRefID == desExchRecModel.ExchId);
                if (flowModel != null)
                {
                    DataModel.Models.FlowNodeExe exeModel = new FlowNodeExe();
                    exeModel.FlowID = flowModel.Id;
                    exeModel.ExeActionID = (int)DataModel.NodeAction.完成;
                    exeModel.ExeActionDate = System.DateTime.Now;
                    exeModel.ExeEmpId = empSession.EmpID;
                    exeModel.ExeEmpName = empSession.EmpName;
                    if (desExchRecModel.RecContent != "")
                    {
                        exeModel.ExeNote = desExchRecModel.RecContent;
                    }
                    else
                    {
                        exeModel.ExeNote = "同意";
                    }
                    this.DbContext.Set<DataModel.Models.FlowNodeExe>().Add(exeModel);
                    this.DbContext.SaveChanges();
                }
                var t = JQ.Util.IO.MessageMonitor.NotifyAsync(desExchRecModel.RecEmpId, delegate (int _empID, JQ.Util.IO.MessageMonitor.NotifyOption _option)
                {
                    return new { action = "ChangeToDoExchRecAmount", amount = -1 };
                });
                result++;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;

        }


        /// <summary>
        /// 获取当前用户收资料的数量
        /// </summary>
        /// <param name="EmpID"></param>
        /// <returns></returns>
        public int GetRecCountByEmpID(int EmpID, int agentTypeID, string agentFlow)
        {
            if (agentTypeID == 0 || ((agentTypeID == -1 || agentTypeID == 2) && ("," + agentFlow + ",").IndexOf(",DesExch,") > -1))
            {
                int Count = 0;
                var list = from item in this.GetList(p => p.RecEmpId == EmpID && p.RecStatus == 0)
                           join desExch in exch.GetList(p => p.ExchIsInvalid == true) on item.ExchId equals desExch.Id
                           join flowItem in flow.GetList(p => p.FlowRefTable == "DesExch" && p.FlowStatusID == 3) on desExch.Id equals flowItem.FlowRefID
                           select 1;
                Count = list.Count();
                return Count;
            }
            else
            {
                return 0;
            }
        }
    }
}