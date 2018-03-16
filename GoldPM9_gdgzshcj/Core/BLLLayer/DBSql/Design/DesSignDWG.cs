using Common;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;
using JQ.Util;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;

namespace DBSql.Design
{
    /// <summary>
    /// DWG签名的类
    /// </summary>
    public class DesSignDWG
    {
        private string signImageDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigUtil.GetConfigValue("SignImageDirectory"));
        private string pdfPIEPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigUtil.GetConfigValue("PDFPIEPath"));

        /// <summary>
        /// 二维码的位置
        /// <para>LeftTop</para> 
        /// <para>LeftBottom</para> 
        /// </summary>
        public string QRCodePosition
        {
            get; set;
        }

        private BaseFont _SignFont;
        private BaseFont SignFont
        {
            get
            {
                if (_SignFont == null)
                {
                    _SignFont = BaseFont.CreateFont(ConfigUtil.GetConfigValue("SignFontPath"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                }
                return _SignFont;
            }
        }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName
        {
            get; set;
        }

        public long TaskId
        {
            get; set;
        }

        public int EmpID
        {
            get; set;
        }

        public string EmpName
        {
            get; set;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public DesSignDWG()
        {
            this.SignFiles = new List<DBSql.Design.Dto.DesTaskAttachSignInfo>();
        }

        /// <summary>
        /// 签名数据
        /// </summary>
        public List<DBSql.Design.Dto.DesTaskAttachSignInfo> SignFiles
        {
            get; set;
        }
    }
}
