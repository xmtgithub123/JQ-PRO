/********
 * 编写日期：2012-06-15
 * 编写人：梅鹏
 * 实现功能：系统模块，系统配置操作
 * 包括：系统分页、更新、获取
 ********/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Data;

namespace DBSql.Sys
{
    public class BaseConfig : EFRepository<DataModel.Models.BaseConfig>
    {
        private readonly string CahceKey = "SoftWareRegistration";
        /// <summary>
        /// 移除缓存
        /// </summary>
        public void CacheRemove()
        {
            EFCacheRemove("BaseConfig");
            Common.CacheManager.CacheRemove(CahceKey);
        }

        /// <summary>
        /// 获取所有用户信息 
        /// 使用缓存
        /// </summary>
        public IEnumerable<DataModel.Models.BaseConfig> AllData
        {
            get
            {
                return GetListFromCahe(s => s.ConfigID > 0, 480, "BaseConfig");
            }
        }

        /// <summary>
        /// 得到BaseConfigInfo对象
        /// </summary>
        /// <param name="ConfigID">ConfigID自增ID</param>
        /// <returns>BaseConfigInfo对象</returns>
        public DataModel.Models.BaseConfig GetBaseConfigInfo(int ConfigID)
        {
            return AllData.SingleOrDefault(p => p.ConfigID == ConfigID);
        }

        /// <summary>
        /// 得到BaseConfigInfo对象
        /// </summary>
        /// <param name="ConfigID">Config名称</param>
        /// <returns>BaseConfigInfo对象</returns>
        public DataModel.Models.BaseConfig GetBaseConfigInfo(string engName)
        {
            return AllData.SingleOrDefault(p => p.ConfigEngName == engName);
        }

        /// <summary>
        /// 金曲公司默认注册码
        /// </summary>
        /// <returns></returns>
        public string GetJinquCode()
        {
            List<string> ListStr = new List<string>();
            ListStr.Add("200");
            ListStr.Add("上海金曲信息技术有限公司");
            ListStr.Add("2019-10-01");

            string code = new Common.SoftRemind().GetRegCode(ListStr);
            return code;
        }


        public string GetRegStatus()
        {          
            List<string> result = new List<string>();
            if (Common.CacheManager.GetCache(CahceKey) != null)
            {
                result = ((IEnumerable<string>)Common.CacheManager.GetCache(CahceKey)).ToList();
            }
            else
            {
                var SoftWareRegistration = this.GetBaseConfigInfo("SoftWareRegistration").ConfigValue;
                string[] code = new Common.SoftRemind().GetRegList(SoftWareRegistration);
                //判断code值
                if (code == null || code.Length != 4)
                {
                    result.Add("软件尚未注册或注册码异常，请联系上海金曲公司客户进行服务器注册！");
                    return string.Join(";", result);
                }

                var MU = this.GetBaseConfigInfo("MU");
                var CustName = this.GetBaseConfigInfo("CustName");
                var SoftDate = this.GetBaseConfigInfo("SoftDate");

                //获取用户最大用户数
                if (MU.ConfigValue != code[0])
                {
                    MU.ConfigValue = code[0];
                    this.Edit(MU);
                }
                //获取单位名称
                if (CustName.ConfigValue != code[1])
                {
                    CustName.ConfigValue = code[1];
                    this.Edit(CustName);
                }
                //获取服务期限
                if (SoftDate.ConfigValue != code[2])
                {
                    SoftDate.ConfigValue = code[2];
                    this.Edit(SoftDate);
                }
                this.UnitOfWork.SaveChanges();

                if (!string.IsNullOrEmpty(code[3]))
                {
                    var runserCode = new Common.SoftReg().getMNum();
                    var serCode = code[3].Trim();
                    if (runserCode != serCode)
                    {
                        result.Add(string.Format("软件服务器机器码已经改变,需要重新注册"));
                    }
                }

                if (!string.IsNullOrEmpty(MU.ConfigValue))
                {
                    var runEmpNum = new DBSql.Sys.BaseEmployee().AllData.Count(s => s.EmpIsDeleted == false);
                    var maxNum = Convert.ToInt32(MU.ConfigValue);
                    if (runEmpNum > maxNum)
                    {
                        result.Add(string.Format("软件许可用户点数：{0},已经超出：{1} 人", maxNum.ToString(), (runEmpNum - maxNum).ToString()));
                    }
                }
                if (!string.IsNullOrEmpty(SoftDate.ConfigValue))
                {
                    var runNowDate = DateTime.Now;
                    var maxDate = Convert.ToDateTime(SoftDate.ConfigValue);
                    if (runNowDate > maxDate)
                    {
                        result.Add(string.Format("软件维护到期时间：{0},已经超出：{1} 天", maxDate.ToString("yyyy-MM-dd"), (runNowDate - maxDate).Days.ToString()));
                    }
                }
                if (result.Count > 0)
                    result.Add(string.Format("请联系上海金曲客服（021-50600055）"));

                Common.CacheManager.CacheTable<string>(CahceKey, result);
            }

            if (result.Count == 0) return "";

            return "温馨提示：" + string.Join(";", result);
        }

    }
}
