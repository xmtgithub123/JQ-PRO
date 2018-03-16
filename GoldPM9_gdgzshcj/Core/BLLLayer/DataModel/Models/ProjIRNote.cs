using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Models
{
    public class ProjIRNote
    {
        #region 构造函数

        public ProjIRNote()
        {
            Id = 0;
            BussProjInfoRecordsId = 0;
            Title = "";
            Detail = "";
            Time = DateTime.Now;
            BaseDataId = 0;
            CreateId = 0;
            CreateName = "";
            CreateTime = DateTime.Now;
        }

        #endregion

        #region 公共成员

        /// <summary>项目信息备案记事Id</summary>
        public int Id { get; set; }

        /// <summary>项目信息备案Id</summary>
        public int BussProjInfoRecordsId { get; set; }

        /// <summary>标题</summary>
        public string Title { get; set; }

        /// <summary>内容</summary>
        public string Detail { get; set; }

        /// <summary>事件发生时间</summary>
        public DateTime Time { get; set; }

        /// <summary>事件类型id</summary>
        public int BaseDataId { get; set; }

        /// <summary>创建人id</summary>
        public int CreateId { get; set; }

        /// <summary>创建人名称</summary>
        public string CreateName { get; set; }

        /// <summary>创建时间</summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}
