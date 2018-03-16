using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSql.Design.Dto
{
    /// <summary>
    /// 签名文件文件信息
    /// </summary>
    public class DesTaskAttachSignInfo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DesTaskAttachSignInfo()
        {
            MuiltiSigns = new SerializableDictionary<string, string>();
        }

        public long ID { get; set; }

        public string Name { get; set; }

        public string Extension { get; set; }

        public long Size { get; set; }

        public string Path { get; set; }

        /// <summary>
        /// 服务器文件路径
        /// </summary>
        public string LocalPath { get; set; }

        public long BaseAttachID { get; set; }

        public long BaseAttachVerID { get; set; }

        public int BaseAttachVer { get; set; }

        /// <summary>
        /// 会签人员的参数
        /// </summary>
        public SerializableDictionary<string, string> MuiltiSigns
        {
            get; set;
        }

        /// <summary>
        /// 设校审批人员参数
        /// </summary>
        public SerializableDictionary<string, string> ApproveSigns
        {
            get; set;
        }
    }
}
