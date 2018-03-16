using System;

namespace DataModel.Models
{
    /// <summary>
    /// 项目周报
    /// </summary>
    public class Weekly
    {
        #region 构造函数

        public Weekly()
        {
            Id = 0;
            TaskGroupId = 0;
            Title = string.Empty;
            Detail = string.Empty;
            StartTime = DateTime.Now;
            EndTime = DateTime.Now.AddDays(6);
            CreateEmpId = 0;
            CreateEmpName = string.Empty;
            CreateTime = DateTime.Now;
        }

        #endregion

        #region 公共成员

        /// <summary>周报Id</summary>
        public int Id { get; set; }

        /// <summary>项目任务Id</summary>
        public int TaskGroupId { get; set; }

        /// <summary>标题</summary>
        public string Title { get; set; }

        /// <summary>内容</summary>
        public string Detail { get; set; }

        /// <summary>开始时间</summary>
        public DateTime StartTime { get; set; }

        /// <summary>结束时间</summary>
        public DateTime EndTime { get; set; }

        /// <summary>创建人id</summary>
        public int CreateEmpId { get; set; }

        /// <summary>创建人名称</summary>
        public string CreateEmpName { get; set; }

        /// <summary>创建时间</summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}
