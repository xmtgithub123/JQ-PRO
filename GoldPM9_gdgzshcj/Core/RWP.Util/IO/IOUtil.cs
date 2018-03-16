/********************************************************************
*           CLR Version:     v4.0        
*           Created by 吴俊鹏 at 2010/5/1
*           175417739@qq.com                   
********************************************************************/

using System;
using System.IO;
using System.Text;

namespace JQ.Util
{
    /// <summary>
    /// 磁盘操作类
    /// </summary>
    public abstract class IOUtil
    {
        /// <summary>
        /// 验证文件夹
        /// </summary>
        /// <param name="directory">文件夹</param>
        /// <returns></returns>
        public static bool directoryExists(string directory)
        {
            return Directory.Exists(@directory);
        }

        /// <summary>
        /// 验证文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static bool fileExists(string filePath)
        {
            return File.Exists(@filePath);
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        public static bool creatDirectory(string directory)
        {
            //判断文件夹是否存在
            if (Directory.Exists(directory))
            {
                return true;
            }
            try
            {
                Directory.CreateDirectory(directory);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="includeChildDir">是否包含子目录和文件</param>
        /// <returns></returns>
        public static bool TryDeleteDirectory(string directory, bool includeChildDir = true)
        {
            try
            {
                Directory.Delete(directory, includeChildDir);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 写文本文件
        /// </summary>
        /// <param name="Str"></param>
        public static void writeTxt(string Str, string FilePath)
        {
            writeTxt(Str, FilePath, true);   //默认追加文本
        }

        /// <summary>
        /// 写文本文件
        /// </summary>
        /// <param name="Str"></param>
        /// <param name="isAppend">是否追加</param>
        public static void writeTxt(string Str, string FilePath, bool isAppend)
        {
            writeTxt(Str, FilePath, isAppend, Encoding.UTF8);   //默认UTF8保存
        }

        /// <summary>
        /// 写文本文件
        /// </summary>
        /// <param name="Str"></param>
        /// <param name="isAppend">是否追加</param>
        public static void writeTxt(string Str, string FilePath, bool isAppend, Encoding strEncode)
        {
            FileStream fsapp;
            if (File.Exists(FilePath))
            {
                if (isAppend)
                    fsapp = new FileStream(@FilePath, FileMode.Append, FileAccess.Write);
                else
                {
                    fsapp = new FileStream(@FilePath, FileMode.Open, FileAccess.Write);
                    fsapp.SetLength(0);   //清空文件内容
                }
            }
            else
            {
                fsapp = new FileStream(@FilePath, FileMode.Create);

            }
            StreamWriter sw = new StreamWriter(fsapp, strEncode);
            sw.WriteLine(Str);
            sw.Flush();
            sw.Close();
            fsapp.Close();
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="filebyte"></param>
        /// <returns></returns>
        public static bool createFile(string filePath, byte[] filebyte)
        {
            TryDeleteFile(filePath);
            if (filebyte.isEmpty())
                return false;
            FileStream fs = new FileStream(filePath, FileMode.Create);
            fs.Write(filebyte, 0, filebyte.Length);
            fs.Close();
            return true;
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static void TryDeleteFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch { }
        }

        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static string readTxt(string filePath)
        {
            if (File.Exists(filePath))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static string readTxt(string filePath, Encoding encoding)
        {
            if (File.Exists(filePath))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fs, encoding))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取文件列表
        /// </summary>
        /// <param name="filePath">文件夹物理路径</param>
        /// <param name="searchPattern">文件名条件</param>
        /// <returns></returns>
        public static string[] getFilesForDirectory(string filePath, string searchPattern)
        {
            if (directoryExists(filePath))
            {
                return Directory.GetFiles(filePath, searchPattern);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取一级子文件夹列表
        /// </summary>
        /// <param name="parentDirPath">父文件夹物理路径</param>
        /// <param name="searchPattern">文件夹名称条件</param>
        /// <returns></returns>
        public static string[] getDirectoriesForDirectory(string parentDirPath, string searchPattern)
        {
            if (directoryExists(parentDirPath))
            {
                return Directory.GetDirectories(parentDirPath, searchPattern, SearchOption.TopDirectoryOnly);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 过滤文件名称
        /// </summary>
        /// <returns></returns>
        public static string FilterFileName(string str)
        {
            return str.Replace(@"/", "／").Replace(@"\", "＼").Replace(@":", "：").Replace(@"?", "？").Replace(@"*", "＊").Replace(@"<", "〈").Replace(@">", "〉").Replace("\"", "“").Replace(@"|", "｜").Replace("'", "＇");
        }

        /// <summary>
        /// 获取出网站的临时目录位置
        /// </summary>
        /// <returns></returns>
        public static string GetTempPath()
        {
            var temp = Path.Combine(JQ.Util.ConfigUtil.GetConfigValue("UpFileRoot"), "Temp");
            if (!Directory.Exists(temp))
            {
                Directory.CreateDirectory(temp);
            }
            return temp;
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="log"></param>
        public static void WriteLog(string log)
        {
            using (var sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DateTime.Now.ToString("yyyy-MM-dd") + ".txt"), true))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + log);
            }
        }
    }
}
