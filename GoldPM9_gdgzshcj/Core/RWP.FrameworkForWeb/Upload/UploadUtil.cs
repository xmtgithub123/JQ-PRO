/********************************************************************
*           Created by 万里涛 at 2014/4/15
********************************************************************/

using JQ.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JQ.Web
{
    /// <summary>
    /// 上传核心服务
    /// </summary>
    public class UploadUtil
    {
        #region 校验文件大小
        /// <summary>
        /// 校验文件大小
        /// </summary>
        /// <param name="fileBytes">文件二进制数组</param>
        /// <param name="maxFileSize">最大文件大小</param>
        /// <returns></returns>
        public static bool validFileSize(byte[] fileBytes, int maxFileSize)
        {
            int fileByteLength = 0;
            if (fileBytes != null)
            {
                fileByteLength = fileBytes.Length;
            }
            if (maxFileSize > 0 && fileByteLength > maxFileSize * 1024)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region 校验文件扩展名
        /// <summary>
        /// 校验文件扩展名
        /// </summary>
        /// <param name="fileNameExt">原始文件名中扩展名</param>
        /// <param name="allowFileExtStr">允许文件扩展名列表</param>
        /// <returns></returns>
        public static bool validFileExt(string fileNameExt, string allowFileExtStr)
        {
            if (!string.IsNullOrEmpty(allowFileExtStr))
            {
                fileNameExt = fileNameExt ?? string.Empty;
                List<string> allowFileExts = new List<string>();
                allowFileExts.AddRange(allowFileExtStr.Split(StringUtil.splitChar).Where(m => !string.IsNullOrEmpty(m)).Select(m => m.ToUpper()));
                return allowFileExts.Contains(fileNameExt.ToUpper());
            }
            return false;
        }

        /// <summary>
        /// 校验文件头
        /// </summary>
        /// <param name="fileBytes">文件二进制数组</param>
        /// <param name="fileNameExt">文件名中扩展名</param>
        /// <returns></returns>
        public static bool validFileHeader(byte[] fileBytes, string fileNameExt)
        {
            if (!string.IsNullOrEmpty(fileNameExt))
            {
                fileNameExt = fileNameExt.ToUpper();  //转化为大写
                if (Enum.IsDefined(typeof(FileExtType), fileNameExt))
                {
                    string headerFileExt = getFileExtFromHeader(fileBytes);  //获取文件头类型字符串
                    if (!string.IsNullOrEmpty(headerFileExt))
                    {
                        var fileNameExtType = EnumHelper.getValue<FileExtType>(fileNameExt);  //文件名扩展名类型
                        var headerFileExtType = EnumHelper.getValue<FileExtType>(headerFileExt);  //文件头类型                        
                        return (int)fileNameExtType == (int)headerFileExtType;  //对扩展名类型int值进行比较
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 通过文件头获取文件类型
        /// </summary>
        /// <param name="fileBytes"></param>
        /// <returns></returns>
        public static string getFileExtFromHeader(byte[] fileBytes)
        {
            if (fileBytes.isNotEmpty() && fileBytes.Length >= 2)
            {
                string fileHeaderStr = fileBytes[0].ToString() + fileBytes[1].ToString();
                if (Enum.IsDefined(typeof(FileExtType), TypeHelper.parseInt(fileHeaderStr)))
                {
                    var fileExtType = EnumHelper.getValue<FileExtType>(fileHeaderStr);
                    return fileExtType.ToString();
                }
            }
            return string.Empty;
        }
        #endregion

        #region 保存上传文件
        /// <summary>
        /// 保存上传文件
        /// </summary>
        /// <param name="fileBytes">文件二进制数组</param>
        /// <param name="saveVirtualPath">上传虚拟路径</param>
        /// <param name="saveFileName">保存文件名</param>
        /// <returns></returns>
        public static DoResult saveUploadFile(byte[] fileBytes, string saveVirtualPath, string saveFileName)
        {
            if (fileBytes.isEmpty() || fileBytes.Length < 2)
            {
                return DoResultHelper.doError("未获取到上传文件内容");
            }
            DoResult dr = validUploadPath(saveVirtualPath, saveFileName);
            if (dr.stateType != DoResultType.success)
            {
                return dr;
            }
            string saveFullPath = TypeHelper.parseString(dr.stateValue);
            IOUtil.createFile(saveFullPath, fileBytes);
            return DoResultHelper.doSuccess(string.Format("{0}/{1}", saveVirtualPath, saveFileName), "文件上传成功");
        }

        /// <summary>
        /// 校验上传路径
        /// </summary>
        /// <param name="saveVirtualPath">保存虚拟目录</param>
        /// <param name="saveFileName">保存文件名</param>
        /// <returns></returns>
        private static DoResult validUploadPath(string saveVirtualPath, string saveFileName)
        {
            if (string.IsNullOrEmpty(saveVirtualPath))
            {
                return DoResultHelper.doError("未设置上传目录");
            }
            string saveDirPath = WebRoot.getMapPath(saveVirtualPath);  //获取物理路径
            if (string.IsNullOrEmpty(saveFileName))
            {
                return DoResultHelper.doError("未设置保存文件名");
            }
            if (!IOUtil.creatDirectory(saveDirPath))
            {
                return DoResultHelper.doError("没有文件夹操作权限");
            }
            return DoResultHelper.doSuccess(string.Format("{0}\\{1}", saveDirPath, saveFileName), string.Empty);
        }
        #endregion
    }

    /// <summary>
    /// 文件扩展名枚举
    /// </summary>
    public enum FileExtType
    {
        JPG = 255216,
        GIF = 7173,
        BMP = 6677,
        PNG = 13780,
        DOC = 208207,
        DOCX = 8075,
        XLS = 208207,
        XLSX = 8075,
        JS = 239187,
        SWF = 6787,
        TXT = 7067,
        MP3 = 7368,
        WMA = 4838,
        MID = 7784,
        RAR = 8297,
        ZIP = 8075,
        XML = 6063
    }
}
