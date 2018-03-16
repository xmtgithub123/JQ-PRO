using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Upload
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int EmpID { get; set; }


        /// <summary>
        /// 业务表的主键ID
        /// </summary>
        public int AttachRefID { get; set; }


        /// <summary>
        /// 业务表标识
        /// </summary>
        public string AttachRefTable { get; set; }


        /// <summary>
        /// FTP的服务器地址
        /// </summary>
        public string FtpServer { get; set; }

        /// <summary>
        /// FTP的服务器端口
        /// </summary>
        public string FtpServerPort { get; set; }


        /// <summary>
        /// FTP的登录名
        /// </summary>
        public string FtpLogin { get; set; }


        /// <summary>
        /// FTP的登录密码
        /// </summary>
        public string FtppassWord { get; set; }

        /// <summary>
        /// 可下载的文件格式
        /// </summary>
        public string FileFilter { get; set; }

        /// <summary>
        /// 文件最大kb
        /// </summary>
        public string FileMaxLength { get; set; }


        /// <summary>
        /// 是否是金曲的dwg0否1是
        /// </summary>
        public string IsJinquDwg { get; set; }


        private bool _isUpload = true;
        /// <summary>
        /// 是否显示上传
        /// </summary>
        public bool isUpload
        {
            get { return _isUpload; }
            set { _isUpload = value; }
        }


        private bool _isDownload = true;

        /// <summary>
        /// 是否显示下载
        /// </summary>
        public bool isDownload
        {
            get { return _isDownload; }
            set { _isDownload = value; }
        }

        private bool _isDelete = true;
        /// <summary>
        /// 是否显示删除
        /// </summary>
        public bool isDelete
        {
            get { return _isDelete; }
            set { _isDelete = value; }
        }


        private bool _isOpn = false;
        /// <summary>
        /// 是否显示打开目录
        /// </summary>
        public bool isOpn
        {
            get { return _isOpn; }
            set { _isOpn = value; }
        }


        private bool _isSync = false;
        /// <summary>
        /// 是否显示同步目录
        /// </summary>
        public bool isSync
        {
            get { return _isSync; }
            set { _isSync = value; }
        }

        /// <summary>
        /// 控件标题
        /// </summary>
        public string title { get; set; }


        public string _controlID = "ttByuploadFile";
        /// <summary>
        /// 生成控件的ID
        /// </summary>
        public string controlID
        {
            get { return _controlID; }
            set { _controlID = value; }
        }

        /// <summary>
        /// 控件名称
        /// grid:tt+name
        /// toolbar:tb+name
        /// </summary>
        public string Name
        {
            get; set;
        }
    }


    public class FileUploader
    {
        public FileUploader()
        {
            this.AllowDelete = true;
            this.AllowDownload = true;
            this.AllowUpload = true;
        }

        /// <summary>
        /// RefTable 
        /// </summary>
        public string RefTable
        {
            get; set;
        }

        /// <summary>
        /// RefID
        /// </summary>
        public int RefID
        {
            get; set;
        }

        /// <summary>
        /// 控件名称
        /// </summary>
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// 是否允许上传，默认为true
        /// </summary>
        public bool AllowUpload
        {
            get; set;
        }

        /// <summary>
        /// 是否允许下载，默认为true
        /// </summary>
        public bool AllowDownload
        {
            get; set;
        }

        /// <summary>
        /// 是否允许删除，默认为false
        /// </summary>
        public bool AllowDelete
        {
            get; set;
        }
    }
}
