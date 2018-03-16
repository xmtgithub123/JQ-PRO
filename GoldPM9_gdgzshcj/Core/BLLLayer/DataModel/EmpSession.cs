using System;

namespace DataModel
{
    /// <summary>
    /// EmpSession 的摘要说明。
    /// </summary>
    [Serializable]
    public class EmpSession
    {
        /// <summary>
        /// 登录用户ID
        /// </summary>
		public int EmpID { get; set; }

        /// <summary>
        /// 登录用户姓名
        /// </summary>
        public string EmpName { get; set; }

        /// <summary>
        /// 登录用户的部门
        /// </summary>
        public int EmpDepID { get; set; }
        /// <summary>
        /// 登录用户的部门
        /// </summary>
        public string EmpDepName { get; set; }
        /// <summary>
        /// 登录用户的分页
        /// </summary>
        public int EmpPageSize { get; set; }

        /// <summary>
        /// 个人设置的皮肤
        /// </summary>
        public string EmpThemesName { get; set; }

        /// <summary>
        /// 左侧菜单样式
        /// </summary>
        public string EmpMenuType { get; set; }

        /// <summary>
        /// 个人设计盘符
        /// </summary>
        public string EmpDisk { get; set; }


        /// <summary>
        /// 是否本机IP-MARK
        /// </summary>
        public string EmpMacAddress { get; set; }


        /// <summary>
        /// 代理登陆 EmpID
        /// </summary>
        public int AgenEmpID { get; set; }

        /// <summary>
        /// 代理登陆 DepID
        /// </summary>
        public int AgenDepID { get; set; }

        /// <summary>
        /// 代理登陆 EmpName
        /// </summary>
        public string AgenEmpName { get; set; }


        /// <summary>
        /// 代理登陆 DepName
        /// </summary>
        public string AgenDepName { get; set; }

        /// <summary>
        /// 代理登陆 类别 （0,1,2）
        /// -1 全部
        /// 0 非委托登录
        /// 1 菜单
        /// 2 流程
        /// </summary>
        public int AgenTypeID { get; set; }

        /// <summary>
        /// 代理菜单
        /// </summary>
        public string AgenMenu { get; set; }

        /// <summary>
        /// 代理流程
        /// </summary>
        public string AgenFlow { get; set; }

        /// <summary>
        /// 验证绩效密码
        /// </summary>
        public bool EmpIsPay { get; set; }

        /// <summary>
        /// 登录用户IP
        /// </summary>
        public string LoginIP { get; set; }

        /// <summary>
        /// 所属公司
        /// </summary>
        public int CompanyID { get; set; }
    }

}
