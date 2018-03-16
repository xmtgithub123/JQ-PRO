using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSql.Design.Dto
{
    /// <summary>
    /// 设计文件签名信息
    /// </summary>
    public class DesTaskAttachSigns
    {
        /// <summary>
        /// 任务
        /// </summary>
        public long TaskId
        {
            get; set;
        }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName
        {
            get; set;
        }
        /// <summary>
        /// 当前操作人员ID
        /// </summary>
        public int EmpID
        {
            get; set;
        }
        /// <summary>
        /// 当前操作人员名称
        /// </summary>
        public string EmpName
        {
            get; set;
        }
        /// <summary>
        /// 设计文件转换后的PDF文件 签名信息
        /// </summary>
        public DesSignPDF SignPDF { get; set; }

        /// <summary>
        /// 设计文件转换后的DWG文件 签名信息
        /// </summary>
        public DesSignDWG SignDWG { get; set; }

        /// <summary>
        /// 设计源文件DWG文件 签名信息
        /// </summary>
        public DesSignDWG SignMain { get; set; }
    }
}
