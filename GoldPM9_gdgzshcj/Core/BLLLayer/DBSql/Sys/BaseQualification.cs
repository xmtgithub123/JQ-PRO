using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Data;
using Common.Data.Extenstions;
namespace DBSql.Sys
{
    public class BaseQualification : EFRepository<DataModel.Models.BaseQualification>
    {
        /// <summary>
        /// 移除缓存
        /// </summary>
        public void CacheRemove()
        {
            Common.CacheManager.CacheRemove("BaseQualification");
        }

        /// <summary>
        /// 使用缓存
        /// </summary>
        public IEnumerable<EmpQualification> AllData
        {
            get
            {
                IEnumerable<EmpQualification> data;
                if (Common.CacheManager.GetCache("BaseQualification") == null)
                {
                    data = GetQualificationEmployee().ToList();
                }
                else
                {
                    data = (IEnumerable<EmpQualification>)Common.CacheManager.GetCache("BaseQualification");
                }
                return Common.CacheManager.CacheTable<EmpQualification>("BaseQualification", data);
            }
        }

        /// <summary>
        /// 获取所有资格人员表
        /// </summary>
        /// <returns></returns>
        IEnumerable<EmpQualification> GetQualificationEmployee()
        {
            var TWhere = QueryBuild<DataModel.Models.BaseQualification>.True();
            TWhere = TWhere.And(p => p.FK_BaseQualification_QualificationEmpID != null);
            TWhere = TWhere.And(p => p.FK_BaseQualification_QualificationEmpID.EmpIsDeleted == false);
            var data = (from s1 in  GetQuery(TWhere)
                        select new EmpQualification
                                         {
                                             QualificationSpecID = s1.QualificationSpecID,
                                             QualificationValue = s1.QualificationValue,
                                             EmpID = s1.FK_BaseQualification_QualificationEmpID.EmpID,
                                             EmpName = s1.FK_BaseQualification_QualificationEmpID.EmpName,
                                             EmpDepID = s1.FK_BaseQualification_QualificationEmpID.EmpDepID,
                                             DepName = s1.FK_BaseQualification_QualificationEmpID.FK_BaseEmployee_EmpDepID.BaseName,
                                             EmpOrder = s1.FK_BaseQualification_QualificationEmpID.EmpOrder,
                                             DepOrder = s1.FK_BaseQualification_QualificationEmpID.FK_BaseEmployee_EmpDepID.BaseOrder
                                         });
            return data;
        }

        /// <summary>
        /// 获取资格人员表
        /// </summary>
        /// <param name="QualificationID"></param>
        /// <param name="SpecialID"></param>
        /// <param name="DepID"></param>
        public IEnumerable<EmpQualification> GetQualificationEmployee(int QualificationID, int SpecialID, int DepID = 0,int EmpID=0)
        {
            IEnumerable<EmpQualification> AllList = AllData;
            if (QualificationID > 0) AllList = AllList.Where(p => p.QualificationValue == QualificationID);
            if (SpecialID != 0) AllList = AllList.Where(p => p.QualificationSpecID == SpecialID) ;
            if (DepID != 0) AllList = AllList.Where(p => p.EmpDepID == DepID);
            if (EmpID != 0) AllList = AllList.Where(p => p.EmpID == EmpID && p.QualificationSpecID != -1);
            return AllList;
        }

        /// <summary>
        /// 更新资格信息
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="departID"></param>
        /// <param name="specialID"></param>
        /// <returns></returns>
        public bool UpdateQualificationEmployee(List<DataModel.BaseQualification> arr, int departID, int specialID,int EmpID=0)
        {
            //step1  删除对应条件资格信息
            var TWhere = QueryBuild<DataModel.Models.BaseQualification>.True();
            if (departID != 0) TWhere = TWhere.And(p => p.FK_BaseQualification_QualificationEmpID.EmpDepID == departID);
            if (specialID != 0) TWhere = TWhere.And(p => p.QualificationSpecID == specialID);
            if (EmpID != 0) TWhere = TWhere.And(p => p.QualificationEmpID == EmpID && p.QualificationSpecID != -1);
            Delete(TWhere);
            //step2  新增对象里的资格信息
            foreach (var item in arr)
            {
                var model = new DataModel.Models.BaseQualification() {
                     QualificationEmpID = item.QualificationEmpID,
                    QualificationSpecID = item.QualificationSpecID,
                    QualificationValue = item.QualificationValue,
                };
                Add(model);
            }
            try
            {
                UnitOfWork.SaveChanges();
                CacheRemove();
                return true;
            }
            catch { }
            return false;
        }

        public IEnumerable<EmpQualification> GetQualificationEmpName()
        {
            return AllData.Where(p => p.QualificationSpecID == -1);
        }

    }

    public class EmpQualification
    {
        public int EmpID { get; set; }
        public string EmpName { get; set; }
        public int EmpDepID { get; set; }
        public string DepName { get; set; }
        public string DepOrder { get; set; }
        public int EmpOrder { get; set; }
        public int QualificationSpecID { get; set; }
        public int QualificationValue { get; set; }

    }
}
