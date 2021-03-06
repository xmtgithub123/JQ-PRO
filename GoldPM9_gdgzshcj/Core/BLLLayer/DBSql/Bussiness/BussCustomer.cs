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
namespace DBSql.Bussiness
{

    public class BussCustomer : EFRepository<DataModel.Models.BussCustomer>
    {
        private BussCustomerLinkMan linkMan = new BussCustomerLinkMan();
        private BussCustomerEvaluate cusEval = new BussCustomerEvaluate();
        private BussCustomerRemember rem = new BussCustomerRemember();

        /// <summary>
        /// 获取记录的数量
        /// </summary>
        /// <param name="CustID"></param>
        /// <returns></returns>
        public int RecordCount(int CustID)
        {
            return rem.GetRecordCount(CustID);
        }

        /// <summary>
        /// 获取联系人数量
        /// </summary>
        /// <param name="CustID"></param>
        /// <returns></returns>
        public int LinkManCount(int CustID)
        {
            return linkMan.GetLinkManCount(CustID);
        }
        /// <summary>
        /// 获取评价数量
        /// </summary>
        /// <param name="CustID"></param>
        /// <returns></returns>
        public int EvaluateCount(int CustID)
        {
            return cusEval.GetBussCustomerEvaluateCount(CustID);
        }


        /// <summary>
        /// 收款合同的筛选条件
        /// </summary>
        /// <param name="CustName"></param>
        /// <returns></returns>
        public Expression<Func<DataModel.Models.BussContract, bool>> GetContractFun(string CustName, Common.SqlPageInfo condition)
        {
            //Expression<Func<TEntity, bool>> predicate
            var TWhere = QueryBuild<DataModel.Models.BussContract>.True();
            TWhere = TWhere.And(p => p.CustName == CustName && p.DeleterEmpId == 0);
            foreach (System.Collections.DictionaryEntry de in condition.SelectCondtion)
            {
                if (de.Value == null || de.Value.ToString().Trim() == "") continue;
                if (de.Value.ToString().Trim() == "0") continue;
                switch (de.Key.ToString())
                {
                    case "ConStatus":
                        var ConStatus = Common.ModelConvert.ConvertToDefaultType<int>(de.Value);
                        if (ConStatus != 0)//合同签订状态
                        {
                            TWhere = TWhere.And(p => p.ConStatus == ConStatus);
                        }
                        break;
                    case "ConDateUpper":
                        var ConDateUpper = Common.ModelConvert.ConvertToDefaultType<DateTime>(de.Value).AddDays(1);
                        TWhere = TWhere.And(p => p.ConDate < ConDateUpper);
                        break;
                    case "ConDateLower":
                        var ConDateLower = Common.ModelConvert.ConvertToDefaultType<DateTime>(de.Value);
                        TWhere = TWhere.And(p => p.ConDate >= ConDateLower);
                        break;


                }
            }
            return TWhere;
        }

        /// <summary>
        /// 获取客户的签订合同的数量
        /// </summary>
        /// <param name="CustName"></param>
        /// <returns></returns>
        public int GetContractCount(string CustName, Common.SqlPageInfo condition)
        {
            return DbContext.Set<DataModel.Models.BussContract>().Count(GetContractFun(CustName, condition));
        }
        /// <summary>
        /// 获取签订合同的金额
        /// </summary>
        /// <param name="CustName"></param>
        /// <returns></returns>
        public decimal GetContractMoney(string CustName, Common.SqlPageInfo condition)
        {
            if (GetContractCount(CustName, condition) != 0)
            {
                //ConFulfilType 开口时--合同金额   闭口---结算金额
                int open = (int)DataModel.ConDealWays.开口;
                return DbContext.Set<DataModel.Models.BussContract>().Where(GetContractFun(CustName, condition)).
                    Select(p => p.ConFulfilType == open ? p.ConFee : p.ConBalanceFee).Sum();
            }
            return (decimal)0.0000;
        }
        /// <summary>
        /// 付款合同的筛选条件
        /// </summary>
        /// <param name="custName"></param>
        /// <returns></returns>
        public Expression<Func<DataModel.Models.BussContractSub, bool>> GetContractSubFun(int CustNameID, Common.SqlPageInfo condition)
        {
            var TWhere = QueryBuild<DataModel.Models.BussContractSub>.True();
            TWhere = TWhere.And(p => p.ConSubCustId == CustNameID && p.DeleterEmpId == 0);
            foreach (System.Collections.DictionaryEntry de in condition.SelectCondtion)
            {
                if (de.Value == null || de.Value.ToString().Trim() == "") continue;
                if (de.Value.ToString().Trim() == "0") continue;
                switch (de.Key.ToString())
                {
                    case "ConSubStatus":
                        var ConSubStatus = Common.ModelConvert.ConvertToDefaultType<int>(de.Value);
                        if (ConSubStatus != 0)//外委合同签订状态
                        {
                            TWhere = TWhere.And(p => p.ConSubStatus == ConSubStatus);
                        }
                        break;
                    case "ConSubDateUpper":
                        var ConSubDateUpper = Common.ModelConvert.ConvertToDefaultType<DateTime>(de.Value).AddDays(1);
                        TWhere = TWhere.And(p => p.ConSubDate < ConSubDateUpper);
                        break;
                    case "ConSubDateLower":
                        var ConSubDateLower = Common.ModelConvert.ConvertToDefaultType<DateTime>(de.Value);
                        TWhere = TWhere.And(p => p.ConSubDate >= ConSubDateLower);
                        break;


                }
            }
            return TWhere;
        }


        /// <summary>
        /// 获取外委单位的合同数量
        /// </summary>
        /// <param name="CustName"></param>
        /// <returns></returns>
        public int GetContractSubCount(int CustNameID, Common.SqlPageInfo pageModel)
        {
            return DbContext.Set<DataModel.Models.BussContractSub>().Count(GetContractSubFun(CustNameID, pageModel));
        }

        /// <summary>
        /// 获取付款合同的金额
        /// </summary>
        /// <param name="CustName"></param>
        /// <returns></returns>
        public decimal GetContractSubMoney(int CustNameID, Common.SqlPageInfo pageModel)
        {
            if (GetContractSubCount(CustNameID, pageModel) != 0)
            {
                return DbContext.Set<DataModel.Models.BussContractSub>().Where(GetContractSubFun(CustNameID, pageModel)).Select(p => p.ConSubFee).Sum();
            }
            return (decimal)0.0000;
        }

        /// <summary>
        /// 删除客户信息,需要删除联系人、记事、评价、评价的详细信息
        /// </summary>
        /// <param name="IDs"></param>
        /// <returns></returns>
        public int DeleteCustomerInfo(List<int> IDs)
        {
            int res = 0;
            var cust = new BussCustomer();
            cust.DbContextRepository(this.UnitOfWork, this.DbContext);

            var linkMan = new BussCustomerLinkMan();
            linkMan.DbContextRepository(this.UnitOfWork, this.DbContext);

            var log = new BussCustomerRemember();
            log.DbContextRepository(this.UnitOfWork, this.DbContext);

            var Evalute = new BussCustomerEvaluate();
            Evalute.DbContextRepository(this.UnitOfWork, this.DbContext);

            var detail = new BussCustomerEvaluateDetail();
            detail.DbContextRepository(this.UnitOfWork, this.DbContext);
            try
            {
                UnitOfWork.BeginTransaction();//开始事物
                foreach (int ID in IDs)
                {
                    List<int> EvaluteIDs = Evalute.GetList(p => p.CustID == ID).Select(m => m.Id).ToList();//查询当前所有的评价记录ID 
                    foreach (int EvaluteID in EvaluteIDs)
                    {
                        detail.Delete(p => p.BussCustomerEvaluateID == EvaluteID);
                    }
                    Evalute.Delete(p => p.CustID == ID);//删除评价记录
                    linkMan.Delete(p => p.CustID == ID);//删除联系人记录
                    log.Delete(p => p.CustID == ID);//删除日志
                }
                cust.Delete(s => IDs.Contains(s.Id));
                UnitOfWork.CommitTransaction();//提交事物
                res++;
            }
            catch
            {
                UnitOfWork.RollBackTransaction();
            }

            return res;
        }


        /// <summary>
        /// 得到该客户是否有被使用过有则不能删除
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public DataTable GetUserCustomer(string  custId)
        {
            using (var dt = DBExecute.ExecuteDataTable(string.Format(@"   select  COUNT(*) as custCount from BussProjInfoRecords
  where CustID in ({0})
  union all
   select  COUNT(*) as custCount from BussBiddingInfo
  where CustID in ({0})
    union all
  select  COUNT(*) as custCount  from Project 
   where CustID in ({0}) 
  union all
select  COUNT(*) as custCount  from ProjSub 
   where ColAttType2 in({0})
     union all
  select  COUNT(*) as custCount  from BussContractOther 
   where CustID in ({0})", custId)))
            {
                return dt;
            }
        }


    }

    public class AddCustmer
    {
        private string _CustName;
        private string _CustLinkMan;
        private string _CustLinkTel;
        private string _CustLinkMail;
        private string _CustBankAccount;
        private string _CustBankName;
        private int _Type = 0;

        public string CustName
        {
            get { return _CustName == null ? "" : _CustName.Trim(); }
            set { _CustName = value; }
        }
        public string CustLinkMan
        {
            get { return _CustLinkMan == null ? "" : _CustLinkMan.Trim(); }
            set { _CustLinkMan = value; }
        }
        public string CustLinkTel
        {
            get { return _CustLinkTel == null ? "" : _CustLinkTel.Trim(); }
            set { _CustLinkTel = value; }
        }
        public string CustLinkMail
        {
            get { return _CustLinkMail == null ? "" : _CustLinkMail.Trim(); }
            set { _CustLinkMail = value; }
        }


        /// <summary>
        /// 0客户单位类别、1外委单位类别、2其他单位
        /// </summary>
        public int TypeID
        {
            get { return string.IsNullOrEmpty(_Type.ToString()) ? 0 : _Type; }
            set { _Type = value; }
        }

        public string CustBankAccount
        {
            get { return _CustBankAccount == null ? "" : _CustBankAccount.Trim(); }
            set { _CustBankAccount = value; }
        }
        public string CustBankName
        {
            get { return _CustBankName == null ? "" : _CustBankName.Trim(); }
            set { _CustBankName = value; }
        }

        public DataModel.EmpSession EmpModel { get; set; }

        /// <summary>
        /// 新增方法
        /// </summary>
        public int AddCust()
        {
            int resultId = -1;
            BussCustomer custDB = new BussCustomer();
            BussCustomerLinkMan linkDB = new BussCustomerLinkMan();
            custDB.DbContextRepository(linkDB.DbContext);
            if (CustName.Trim() != "")
            {
                DataModel.Models.BussCustomer dmCust = custDB.FirstOrDefault(p => p.CustName == CustName);
                if (dmCust == null)
                {
                    #region 新增客户
                    using (var tran = custDB.DbContext.Database.BeginTransaction())
                    {
                        try
                        {
                            dmCust = new DataModel.Models.BussCustomer();
                            dmCust.TypeID = TypeID;
                            dmCust.CustName = CustName;
                            dmCust.CustBankName = CustBankName == null ? "" : CustBankName;
                            dmCust.CustBankAccount = CustBankAccount == null ? "" : CustBankAccount;

                            Common.ModelConvert.MvcDefaultSave(dmCust, EmpModel);
                            custDB.Add(dmCust);
                            custDB.DbContext.SaveChanges();
                            //联系人
                            if (CustLinkMan != "")
                            {
                                DataModel.Models.BussCustomerLinkMan dmLink = new DataModel.Models.BussCustomerLinkMan(); ;
                                dmLink.CustLinkManName = CustLinkMan;
                                dmLink.CustLinkManTel = CustLinkTel;
                                dmLink.CustLinkManWeb = CustLinkMail;
                                dmLink.CustID = dmCust.Id;
                                Common.ModelConvert.MvcDefaultSave(dmLink, EmpModel);
                                custDB.DbContext.Set<DataModel.Models.BussCustomerLinkMan>().Add(dmLink);
                                custDB.DbContext.SaveChanges();
                            }
                            tran.Commit();
                            resultId = dmCust.Id;
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            resultId = -1;
                        }
                    }
                    #endregion
                }
                else
                {
                    #region 修改客户
                    using (var tran = custDB.DbContext.Database.BeginTransaction())
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(CustBankName))
                            {
                                dmCust.CustBankName = CustBankName;
                            }
                            if (!string.IsNullOrEmpty(CustBankAccount))
                            {
                                dmCust.CustBankAccount = CustBankAccount;
                            }
                            Common.ModelConvert.MvcDefaultEdit(dmCust, EmpModel);
                            custDB.DbContext.Set<DataModel.Models.BussCustomer>().Add(dmCust);
                            custDB.DbContext.Entry<DataModel.Models.BussCustomer>(dmCust).State = System.Data.Entity.EntityState.Modified;
                            custDB.DbContext.SaveChanges();

                            if (CustLinkMan != "")
                            {
                                DataModel.Models.BussCustomerLinkMan dmLink = linkDB.FirstOrDefault(p => p.CustLinkManName == CustLinkMan && p.CustID == dmCust.Id);
                                if (dmLink == null)
                                {
                                    dmLink = new DataModel.Models.BussCustomerLinkMan();
                                    dmLink.CustLinkManName = CustLinkMan;
                                    dmLink.CustLinkManTel = CustLinkTel;
                                    dmLink.CustLinkManWeb = CustLinkMail;
                                    dmLink.CustID = dmCust.Id;
                                    Common.ModelConvert.MvcDefaultSave(dmLink, EmpModel);
                                    custDB.DbContext.Set<DataModel.Models.BussCustomerLinkMan>().Add(dmLink);
                                    custDB.DbContext.SaveChanges();
                                }
                                else
                                {
                                    dmLink.CustLinkManName = CustLinkMan;
                                    dmLink.CustLinkManTel = CustLinkTel;
                                    dmLink.CustLinkManWeb = CustLinkMail;
                                    dmLink.CustID = dmCust.Id;
                                    Common.ModelConvert.MvcDefaultEdit(dmLink, EmpModel);
                                    custDB.DbContext.Entry<DataModel.Models.BussCustomerLinkMan>(dmLink).State = System.Data.Entity.EntityState.Modified;
                                    custDB.DbContext.SaveChanges();
                                }
                            }
                            tran.Commit();
                            resultId = dmCust.Id;
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            resultId = -1;
                        }
                    }
                    #endregion
                }
            }
            return resultId;
        }
    }
}
