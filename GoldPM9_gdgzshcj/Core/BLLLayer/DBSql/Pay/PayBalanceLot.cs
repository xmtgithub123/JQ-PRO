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
    public class PayBalanceLot : EFRepository<DataModel.Models.PayBalanceLot>
    {

        /// <summary>
        ///  结算批次
        /// </summary>
        /// <param name="PerProductMoney">每个产值的金额</param>
        public void BalanceLot(decimal PerProductMoney,DataModel.Models.PayBalanceLot modelLot)
        {
            PayBalanceLot lot = new PayBalanceLot();
            PayBalanceEngineering payEngi = new PayBalanceEngineering();
            PayBalanceUserAccount payCount = new PayBalanceUserAccount();

            lot.DbContextRepository(this.UnitOfWork, this.DbContext);
            payEngi.DbContextRepository(this.UnitOfWork, this.DbContext);
            payCount.DbContextRepository(this.UnitOfWork, this.DbContext);
            try
            {
                UnitOfWork.BeginTransaction();//开始事物 
                modelLot.AllAmount = payCount.GetTotalAmount();//总产值
                modelLot.AllMoney = PerProductMoney * payCount.GetTotalAmount();//总钱数
                modelLot.BalanceDate = DateTime.Now;
                modelLot.ManageAmount = payCount.GetManagerAmount();
                modelLot.ManageBase = payCount.GetAverage();
                modelLot.MoneyPerAmount = PerProductMoney;
                modelLot.TechAmount = payCount.GetTechSumAcount();
                modelLot.TechEmpCount = payCount.GetTotalTechPerson();
                lot.Add(modelLot);
                lot.DbContext.SaveChanges();

                // 更新PayBalanceEngineering中的信息
                payEngi.Edit(p=>p.BalanceLotID==0&&p.BalanceState==(int)DataModel.BalanceStatus.预结算,
                    u=>new DataModel.Models.PayBalanceEngineering() {BalanceState=(int)DataModel.BalanceStatus.已结算,BalanceLotID=modelLot.Id });
                //更新明细表中的信息(EF语句操作不方便)
                //payCount.Edit(p => p.BalanceLotID == 0 && p.BalanceType == 1,
                //    u => new DataModel.Models.PayBalanceUserAccount() { BalanceLotID = modelLot.Id, BalanceMoney = PerProductMoney * BalanceAmount });
                foreach (DataModel.Models.PayBalanceUserAccount pay in payCount.GetList(p => p.BalanceLotID == 0 && p.BalanceType == 1))//技术人员
                {
                    pay.BalanceLotID = modelLot.Id;
                    pay.BalanceMoney = PerProductMoney * pay.BalanceAmount;
                    payCount.Edit(pay);
                }
                foreach (DataModel.Models.PayBalanceUserAccount pay in payCount.GetList(p => p.BalanceLotID == 0 && p.BalanceType == 2))//管理人员
                {
                    pay.BalanceLotID = modelLot.Id;
                    pay.BalanceAmount = payCount.GetMangerAmountBySelf(pay.EmpId);
                    pay.BalanceMoney = PerProductMoney * payCount.GetMangerAmountBySelf(pay.EmpId);
                    payCount.Edit(pay);
                }
                DataModel.Models.PayBalanceLot payLotModel = Get(1);
                payLotModel.TechEmpCount = 0;
                lot.Edit(payLotModel);
                UnitOfWork.CommitTransaction();

            }

            catch
            {
                UnitOfWork.RollBackTransaction();
            }

        }
    }
}