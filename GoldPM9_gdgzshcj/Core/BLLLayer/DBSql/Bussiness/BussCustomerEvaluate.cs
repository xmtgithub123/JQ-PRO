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
namespace DBSql.Bussiness
{
    public class BussCustomerEvaluate : EFRepository<DataModel.Models.BussCustomerEvaluate>
    {
        private DBSql.Sys.BaseEmployee emp = new Sys.BaseEmployee();
        /// <summary>
        /// 根据客户编号获取其被评价的次数
        /// </summary>
        /// <param name="CustID"></param>
        /// <returns></returns>
        public int GetBussCustomerEvaluateCount(int CustID)
        {
            return Count(p=>p.CustID==CustID);
        }

        /// <summary>
        /// 删除评价信息以及详细评价
        /// </summary>
        /// <param name="IDs"></param>
        /// <returns></returns>
        public int DeleteInfo(List<int> IDs)
        {
            int res =0;
            var bussEvaluate = new BussCustomerEvaluate();
            bussEvaluate.DbContextRepository(UnitOfWork,DbContext);

            var bussDetail = new BussCustomerEvaluateDetail();
            bussDetail.DbContextRepository(UnitOfWork,DbContext);
            try
            {
                UnitOfWork.BeginTransaction();
                foreach(int ID in IDs)
                {
                    bussDetail.Delete(p=>p.BussCustomerEvaluateID==ID);
                }

                bussEvaluate.Delete(p=>IDs.Contains(p.Id));
                UnitOfWork.CommitTransaction();
                res++;
            }
            catch
            {
                UnitOfWork.RollBackTransaction();
            }
            
            return res;
        }

        /// <summary>
        /// 发送客户评价的消息（选择的接收人）
        /// </summary>
        /// <param name="model"></param>
        /// <param name="EmpList"></param>
        /// <param name="empSession"></param>
        /// <returns></returns>
        public int SendEvaluateInfo(DataModel.Models.BussCustomerEvaluate model,List<int>EmpList,DataModel.EmpSession empSession)
        {
            int sendResult = 0;
            if (EmpList.Count <= 0)
                return 0;//没有选择接收人
            DataModel.Models.OaMess oaMessModel = new DataModel.Models.OaMess();
            string ProjName = string.Empty;
            string CustName = string.Empty;
            DBSql.Oa.OaMessRead read = new Oa.OaMessRead();
            DBSql.OA.OaMess mess = new OA.OaMess();
            read.DbContextRepository(this.UnitOfWork, this.DbContext);
            read.DbContextRepository(this.UnitOfWork, this.DbContext);
            DataModel.Models.Project projectModel = new DBSql.Project.Project().Get(model.ProjID);
            if(projectModel!=null)
            {
                ProjName = projectModel.ProjName;
            }
            DataModel.Models.BussCustomer custModel = new DBSql.Bussiness.BussCustomer().Get(model.CustID);
            if(custModel!=null)
            {
                CustName = custModel.CustName;
            }
            oaMessModel.MessDate = DateTime.Now;
            oaMessModel.MessEmpId = empSession.EmpID;
            oaMessModel.MessEmpName = empSession.EmpName;
            oaMessModel.MessIsAutoReturn = false;
            oaMessModel.MessIsDeleted = false;
            oaMessModel.MessIsSystem = true;
            oaMessModel.MessLinkTitle = "[" + ProjName + "]---客户评价";
            oaMessModel.MessNote = "[" + ProjName + "]---["+CustName+"]---客户评价"; ;
            oaMessModel.MessTitle = "[" + ProjName + "]---客户评价";
            oaMessModel.MessRefTable = "CustomerEvaluate";
            oaMessModel.MessRefID = model.Id;
            oaMessModel.MessLinkUrl = string.Format("Bussiness/BussCustomerEvaluate/edit?id="+model.Id.ToString()+"&Read=1");
            try
            {
                UnitOfWork.BeginTransaction();
                mess.Add(oaMessModel);
                mess.DbContext.SaveChanges();
                foreach (int EmpId in EmpList)
                {
                    DataModel.Models.OaMessRead oaMessRead = new DataModel.Models.OaMessRead();
                    oaMessRead.Id = oaMessModel.Id;
                    oaMessRead.MessReadDate = new DateTime(1900, 1, 1);
                    oaMessRead.MessReadEmpId = EmpId;
                    oaMessRead.MessReadEmpName = emp.Get(EmpId) == null ? "" : emp.Get(EmpId).EmpName;
                    oaMessRead.MessReadIsDeleted = false;
                    oaMessRead.MessReadNote = oaMessModel.MessNote;
                    read.Add(oaMessRead);
                }
                UnitOfWork.CommitTransaction();
                sendResult++;
            }
            catch
            {
                UnitOfWork.RollBackTransaction();
            }
            Oa.OaMessRead.CacheRemove();
            var t = JQ.Util.IO.MessageMonitor.NotifyAsync(EmpList, delegate (int empID)
            {
                return new Oa.OaMessRead().GetNotifyDatas(empID);
            });

            return sendResult;
        }
    }
}
