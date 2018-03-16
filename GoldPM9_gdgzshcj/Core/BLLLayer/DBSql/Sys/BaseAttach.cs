/* --------------------------------------------------------------------------
 * 编写日期：2012-06-15
 * 编写人：梅鹏
 * 实现功能：通用附件操作类
-------------------------------------------------------------------------- */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Text;
using System.IO;
using Common.Data;
using Common.Data.Extenstions;
using DAL;
using System.Web;
using System.Xml;
using System.Data.Entity;

namespace DBSql.Sys
{
    public class BaseAttach : EFRepository<DataModel.Models.BaseAttach>, IDisposable
    {
        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="refTable"></param>
        /// <param name="refID"></param>
        /// <param name="empID"></param>
        public void UploadFileToBaseAttach(string refTable, int refID, int empID)
        {
            string UpFileRoot = ConfigurationManager.AppSettings["UpFileRoot"].ToString();
            string path = ConfigurationManager.AppSettings["FtpLocalPath"].ToString() + "\\Emp" + empID.ToString();
            string AttachPath = refTable + @"/P" + refID.ToString();

            if (!Directory.Exists(path) || !Directory.Exists(UpFileRoot)) return;
            string GoldFile = string.Format(@"{0}\{1}\P{2}", UpFileRoot, refTable, refID.ToString());

            if (!Directory.Exists(GoldFile)) Directory.CreateDirectory(GoldFile);

            var FileList = Directory.GetFiles(path).ToList();

            LoadChildDir(path, FileList);


            foreach (var file in FileList)
            {
                if (file.EndsWith(".JqTmp")) continue;

                System.IO.FileInfo fi = new System.IO.FileInfo(file);
                string FileName = file.Substring(file.LastIndexOf("\\") + 1);

                string NewFile = string.Format(@"{0}\{1}", GoldFile, FileName);
                int i = 1;
                while (File.Exists(NewFile))
                {
                    NewFile = GoldFile + string.Format(@"\({0}){1}", i.ToString(), FileName);
                    i++;
                }

                FileName = NewFile.Substring(NewFile.LastIndexOf("\\") + 1);

                var ba = new DataModel.Models.BaseAttach();
                ba.AttachDateUpload = DateTime.Now;
                ba.AttachDateChange = DateTime.Now;
                ba.AttachEmpID = empID;
                ba.AttachName = FileName;
                ba.AttachDir = AttachPath;
                ba.AttachRefID = refID;
                ba.AttachRefTable = refTable;
                ba.AttachSize = fi.Length;
                File.Move(file, GoldFile + "\\" + FileName);
                //判断文件是否存在
                Add(ba);
            }
        }

        private void LoadChildDir(string Dir, List<string> arr)
        {
            var childArr = Directory.GetDirectories(Dir);
            foreach (string item in childArr)
            {
                var childFile = Directory.GetFiles(item).Where(s => !s.EndsWith(".JqTmp"));
                if (childFile.Count() > 0) arr.AddRange(childFile);

                var childDir = Directory.GetDirectories(item);
                if (childDir.Count() > 0)
                {
                    foreach (var child in childDir)
                    {
                        LoadChildDir(child, arr);
                    }
                }
            }
        }

        /// <summary>
        /// 更新 文件备注信息
        /// </summary>
        /// <param name="attachID"></param>
        /// <param name="note"></param>
        public void UpdateNote(long attachID, string note)
        {
            var data = this.DbContext.Set<DataModel.Models.BaseAttach>().FirstOrDefault(m => m.AttachID == attachID);
            if (data == null)
            {
                return;
            }
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(data.ColAttXml);
            var xNote = (XmlElement)xmlDocument.SelectSingleNode("Root/Note");
            if (xNote == null)
            {
                xNote = xmlDocument.CreateElement("Note");
                xmlDocument.DocumentElement.AppendChild(xNote);
            }
            xNote.InnerText = note;
            data.ColAttXml = xmlDocument.OuterXml;
            this.DbContext.SaveChanges();
        }

        /// <summary>
        /// 删除附件表对象
        /// </summary>
        /// <param name="refIDArr"></param>
        /// <param name="refTable"></param>
        /// <returns></returns>
        public bool DeleteBaseAttach(List<long> refIDArr, string refTable)
        {
            var TWhere = QueryBuild<DataModel.Models.BaseAttach>.True();
            TWhere = TWhere.And(p => refIDArr.Contains(p.AttachID));
            TWhere = TWhere.And(p => p.AttachRefTable == refTable);
            var arr = GetList(TWhere);
            string strPath = ConfigurationManager.AppSettings["UpFileRoot"].ToString();
            foreach (var item in arr)
            {
                string FilePath = String.Format(@"{0}\{1}\{2}", strPath, item.AttachDir, item.AttachName);
                if (File.Exists(FilePath))
                {
                    try
                    {
                        File.Delete(FilePath);
                    }
                    catch { return false; }
                }
            }
            Delete(TWhere);
            return true;
        }


        /// <summary>
        /// 获取文件列表
        /// </summary>
        /// <param name="refID"></param>
        /// <param name="refTable"></param>
        /// <returns></returns>
        public IEnumerable<DataModel.Models.BaseAttach> GetBaseAttachList(int refID, string refTable)
        {
            var TWhere = QueryBuild<DataModel.Models.BaseAttach>.True();
            TWhere = TWhere.And(p => p.AttachRefID == refID);
            TWhere = TWhere.And(p => p.AttachRefTable == refTable);
            return GetList(TWhere);
        }

        public List<dynamic> GetBaseAttachVersionList(int refID, string refTable)
        {
            return this.DbContext.Set<DataModel.Models.BaseAttachVer>().Join(this.DbContext.Set<DataModel.Models.BaseAttach>(), t => t.AttachID, t1 => t1.AttachID, (t, t1) => new { t.AttachVer, t.AttachSize, t.AttachEmpName, t.AttachDateUpload, t.AttachDateChange, t.AttachVerId, t1.AttachName, t1.AttachRefID, t1.AttachRefTable }).Where(m => m.AttachRefTable == refTable && m.AttachRefID == refID).Select(m => new { ID = m.AttachVerId, Name = m.AttachName, Size = m.AttachSize, LastModifyDate = m.AttachDateChange, UploadDate = m.AttachDateUpload, EmpName = m.AttachEmpName, Version = m.AttachVer, Type = "attachversion" }).ToList<dynamic>();
        }

        public List<dynamic> GetBaseAttachAndVersionList(int refID, string refTable)
        {
            var ss = this.DbContext.Set<DataModel.Models.BaseAttachVer>()
                .Join(this.DbContext.Set<DataModel.Models.BaseAttach>(), t => t.AttachID, t1 => t1.AttachID, (t, t1) =>
                new {
                    t.AttachID,
                    t.AttachVer,
                    t.AttachSize,
                    t.AttachEmpName,
                    t.AttachDateUpload,
                    t.AttachDateChange,
                    t.AttachVerId,
                    t1.AttachName,
                    t1.AttachRefID,
                    t1.AttachRefTable,
                    t.AttachDir
                }).Where(m => m.AttachRefTable == refTable && m.AttachRefID == refID)
                .ToList<dynamic>();
            return ss;
        }

        /// <summary>
        /// 获取文件列表
        /// </summary>
        /// <param name="refID"></param>
        /// <param name="refTable"></param>
        /// <returns></returns>
        public IEnumerable<DataModel.Models.BaseAttach> GetBaseAttachList(List<int> refID, string refTable)
        {
            var TWhere = QueryBuild<DataModel.Models.BaseAttach>.True();
            TWhere = TWhere.And(p => refID.Contains(p.AttachRefID));
            TWhere = TWhere.And(p => p.AttachRefTable == refTable);
            return GetList(TWhere);
        }

        /// <summary>
        /// 获取扩展附件信息
        /// </summary>
        /// <param name="refID"></param>
        /// <param name="refTable"></param>
        /// <returns></returns>
        public IEnumerable<DataModel.Models.BaseAttach> GetExtendBaseAttachList(List<int> refID, string refTable)
        {
            IEnumerable<DataModel.Models.BaseAttach> ExtendList = null;
            switch (refTable)
            {
                case "DesMutiSign":
                    #region 获取会签时候的文件
                    refID = refID.Distinct().ToList();
                    List<DataModel.Models.DesMutiSignAttach> attachList = this.DbContext.Set<DataModel.Models.DesMutiSignAttach>().Where(p => refID.Contains(p.MutiSignID)).ToList();
                    if (attachList.Count() > 0)
                    {
                        List<long> attachIds = attachList.Select(p => p.AttachID).ToList();
                        ExtendList = GetList(p => attachIds.Contains(p.AttachID)).ToList();
                        var VerList = new DBSql.Sys.BaseAttachVer().GetList(p => attachIds.Contains(p.AttachID));
                        if (ExtendList != null)
                        {
                            ExtendList = (from n in attachList
                                          join p in ExtendList on n.AttachID equals p.AttachID
                                          select new DataModel.Models.BaseAttach()
                                          {
                                              AttachID = p.AttachID,
                                              AttachRefID = n.MutiSignID,
                                              AttachName = p.AttachName,
                                              //下面显示 版本文件
                                              //AttachDir = VerList.FirstOrDefault(x => x.AttachID == n.AttachID && x.AttachVer == n.AttachVer) == null ? p.AttachDir : VerList.FirstOrDefault(x => x.AttachID == n.AttachID && x.AttachVer == n.AttachVer).AttachDir,
                                              AttachDir = p.AttachDir,
                                              AttachDateChange = p.AttachDateChange,
                                              AttachVer = n.AttachVer,
                                          }).ToList();

                            //ExtendList = ExtendList.Select(p => new DataModel.Models.BaseAttach()
                            //{
                            //    AttachID = p.AttachID,
                            //    AttachRefID = attachList.FirstOrDefault(x => x.AttachID == p.AttachID) == null ? p.AttachRefID : attachList.FirstOrDefault(x => x.AttachID == p.AttachID).MutiSignID,
                            //    AttachName = p.AttachName,
                            //    AttachDir = p.AttachDir,
                            //    AttachDateChange = p.AttachDateChange,
                            //});
                        }
                    }
                    #endregion
                    break;
                default:
                    break;
            }


            return ExtendList;
        }

        /// <summary>
        /// 移动文件到临时目录
        /// </summary>
        /// <param name="refID"></param>
        /// <param name="refTable"></param>
        /// <param name="empID"></param>
        public string CopyFileToTemp(int refID, string refTable, int empID)
        {
            var tempPath = JQ.Util.IOUtil.GetTempPath();
            var localRoot = JQ.Util.ConfigUtil.GetConfigValue("UpFileRoot");
            //将文件移动到临时目录
            var result = new List<dynamic>();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml("<Root></Root>");
            foreach (var item in GetBaseAttachList(refID, refTable))
            {
                var realPath = Path.Combine(localRoot, item.AttachDir);
                if (File.Exists(realPath))
                {
                    var newFileName = Guid.NewGuid().ToString();
                    File.Copy(realPath, Path.Combine(tempPath, newFileName));
                    var xFile = xmlDocument.CreateElement("File");
                    xmlDocument.DocumentElement.AppendChild(xFile);
                    xFile.SetAttribute("FileName", newFileName);
                    xFile.SetAttribute("RealName", item.AttachName);
                    xFile.SetAttribute("Size", item.AttachSize.ToString());
                    xFile.SetAttribute("LastModifiedTime", item.AttachDateChange.ToString("yyyy-MM-dd HH:mm:ss"));
                    xFile.SetAttribute("UploadTime", item.AttachDateUpload.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
                    xFile.SetAttribute("EmpID", item.AttachEmpID.ToString());
                    xFile.SetAttribute("EmpName", item.AttachEmpName);
                }
            }
            return xmlDocument.OuterXml;
        }

        public DataModel.Models.BaseAttachVer SaveFile(int refID, string refTable, string fileName, string filePath, long parentID, DateTime lastModifyTime, int empID, string empName)
        {
            if (refID == 0 || string.IsNullOrEmpty(refTable))
            {
                return null;
            }
            var fileInfo = new FileInfo(filePath);
            if (!fileInfo.Exists)
            {
                return null;
            }
            var prtAttach = this.Get(parentID);
            var baseAttach = this.DbContext.Set<DataModel.Models.BaseAttach>().FirstOrDefault(m => m.AttachRefID == refID && m.AttachRefTable == refTable && m.AttachName == fileName && m.AttachParentID == parentID);
            if (baseAttach != null && baseAttach.AttachDateChange == lastModifyTime)
            {
                JQ.Util.IOUtil.TryDeleteFile(filePath);
                throw new Common.JQException("01", "该文件已经存在（最后修改时间相同）");
            }
            if (baseAttach == null)
            {
                //当前文件不存在，直接插入最新的
                baseAttach = new DataModel.Models.BaseAttach();
                baseAttach.AttachGroupID = 0;
                if (prtAttach != null)
                {
                    baseAttach.AttachParentID = prtAttach.AttachID;
                    baseAttach.AttachLevel = prtAttach == null ? 0 : prtAttach.AttachLevel + 1;
                    baseAttach.AttachOrderNum = this.GetQuery(x => x.AttachRefID == refID && x.AttachRefTable == refTable && x.AttachParentID == prtAttach.AttachID).Count() + 1;
                    baseAttach.AttachOrderPath = prtAttach == null ? baseAttach.AttachOrderNum.ToString("0000") : prtAttach.AttachOrderPath + "_" + baseAttach.AttachOrderNum.ToString("0000");
                    baseAttach.AttachPathIDs = String.IsNullOrWhiteSpace(prtAttach.AttachPathIDs) ? prtAttach.AttachID.ToString() : prtAttach.AttachPathIDs + "," + prtAttach.AttachID.ToString();
                }
                else
                {
                    baseAttach.AttachParentID = 0;
                    baseAttach.AttachLevel = 0;
                    baseAttach.AttachOrderNum = this.GetQuery(x => x.AttachRefID == refID && x.AttachRefTable == refTable && x.AttachParentID == 0).Count() + 1;
                    baseAttach.AttachOrderPath = baseAttach.AttachOrderNum.ToString("0000");
                    baseAttach.AttachPathIDs = "";
                }
                baseAttach.AttachRefID = refID;
                baseAttach.AttachRefTable = refTable;
                baseAttach.AttachEmpID = empID;
                baseAttach.AttachEmpName = empName;
                baseAttach.AttachName = fileName;
                baseAttach.AttachExt = Path.GetExtension(fileName);
                baseAttach.AttachDateChange = lastModifyTime;
                baseAttach.AttachDateUpload = DateTime.Now;
                baseAttach.AttachGrade = 0;
                baseAttach.AttachTag = "";
                baseAttach.AttachVer = 0;
                baseAttach.ColAttType1 = 0;
                baseAttach.ColAttType2 = 0;
                baseAttach.ColAttXml = "<Root />";
            }
            baseAttach.AttachDir = Path.Combine(JQ.Util.IOUtil.FilterFileName(refTable), (refID / 1000).ToString(), refID.ToString(), Guid.NewGuid().ToString() + baseAttach.AttachExt);
            baseAttach.AttachDateChange = lastModifyTime;
            baseAttach.AttachVer = baseAttach.AttachVer + 1;
            baseAttach.AttachSize = fileInfo.Length;
            //转移文件
            var targetPath = Path.Combine(JQ.Util.ConfigUtil.GetConfigValue("UpFileRoot"), baseAttach.AttachDir);
            if (File.Exists(targetPath))
            {
                JQ.Util.IOUtil.TryDeleteFile(targetPath);
            }
            else if (!Directory.Exists(Path.GetDirectoryName(targetPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(targetPath));
            }
            DataModel.Models.BaseAttachVer baseAttachVersion = null;
            if (this.DbContext.Database.CurrentTransaction == null)
            {
                using (var tran = this.DbContext.Database.BeginTransaction())
                {
                    try
                    {
                        baseAttachVersion = SaveFile(baseAttach);
                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                    }
                }
                File.Move(filePath, targetPath);
            }
            else
            {
                baseAttachVersion = SaveFile(baseAttach);
                File.Move(filePath, targetPath);
            }
            return baseAttachVersion;
        }

        private DataModel.Models.BaseAttachVer SaveFile(DataModel.Models.BaseAttach baseAttach)
        {
            if (baseAttach.AttachID == 0)
            {
                this.DbContext.Set<DataModel.Models.BaseAttach>().Add(baseAttach);
                this.DbContext.SaveChanges();
            }
            var baseAttachVer = new DataModel.Models.BaseAttachVer()
            {
                AttachDateChange = baseAttach.AttachDateChange,
                AttachDateUpload = baseAttach.AttachDateUpload,
                AttachDir = baseAttach.AttachDir,
                AttachEmpID = baseAttach.AttachEmpID,
                AttachEmpName = baseAttach.AttachEmpName,
                AttachSize = baseAttach.AttachSize,
                AttachVer = baseAttach.AttachVer,
                AttachID = baseAttach.AttachID
            };
            this.DbContext.Set<DataModel.Models.BaseAttachVer>().Add(baseAttachVer);
            this.DbContext.SaveChanges();
            return baseAttachVer;
        }


        /// <summary>
        /// 保存临时文件
        /// </summary>
        /// <param name="refID"></param>
        public void MoveFile(int refID, int empID, string empName = null)
        {
            if (HttpContext.Current == null)
            {
                return;
            }
            var xmlContent = HttpUtility.HtmlDecode(HttpContext.Current.Request.Form["$uplohad$_cache_y12$t1m"]);
            if (string.IsNullOrEmpty(xmlContent))
            {
                return;
            }
            XmlDocument xmlDocument = new XmlDocument();
            try
            {
                xmlDocument.LoadXml(xmlContent);
            }
            catch
            {
                return;
            }
            if (string.IsNullOrEmpty(empName))
            {
                var emp = new DBSql.Sys.BaseEmployee().GetBaseEmployeeInfo(empID);
                if (emp == null)
                {
                    return;
                }
                empName = emp.EmpName;
            }
            var tempPath = JQ.Util.IOUtil.GetTempPath();
            foreach (XmlElement xFiles in xmlDocument.SelectNodes("Root/Files"))
            {
                var refTable = xFiles.GetAttribute("RefTable");
                if (string.IsNullOrEmpty(refTable))
                {
                    continue;
                }
                foreach (XmlElement xFile in xFiles.SelectNodes("File"))
                {
                    DateTime lastModifiedTime;
                    if (!DateTime.TryParse(xFile.GetAttribute("LastModifiedTime"), out lastModifiedTime))
                    {
                        continue;
                    }
                    var localPath = Path.Combine(tempPath, xFile.InnerText);
                    if (!File.Exists(localPath))
                    {
                        continue;
                    }
                    var fileName = xFile.GetAttribute("FileName");
                    if (string.IsNullOrEmpty(fileName))
                    {
                        continue;
                    }
                    try
                    {
                        SaveFile(refID, refTable, fileName, localPath, 0, lastModifiedTime, empID, empName);
                    }
                    catch (Common.JQException) { }
                }
            }
        }

        public void Delete(List<long> attachIDs)
        {
            if (attachIDs.Count == 0)
            {
                return;
            }
            var attachDB = this.DbContext.Set<DataModel.Models.BaseAttach>();
            var baseAttaches = attachDB.Where(m => attachIDs.Contains(m.AttachID)).ToList();
            if (baseAttaches.Count == 0)
            {
                return;
            }
            var attachVersionDB = this.DbContext.Set<DataModel.Models.BaseAttachVer>();
            List<string> toDeleteFiles = new List<string>();
            var toDeleteSplittedFiles = new List<long>();
            var isSuccess = false;
            using (var tran = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var baseAttach in baseAttaches)
                    {
                        toDeleteFiles.Add(baseAttach.AttachDir);
                        //验证版本
                        var versions = baseAttach.FK_BaseAttachVer_AttachID.ToList();
                        if (versions.Count == 0)
                        {
                            attachDB.Remove(baseAttach);
                            continue;
                        }
                        if (versions.Count == 1)
                        {
                            //删除文件
                            attachDB.Remove(baseAttach);
                            if (baseAttach.AttachRefTable == "DesTaskAttach")
                            {
                                toDeleteSplittedFiles.Add(versions[0].AttachVerId);
                            }
                            attachVersionDB.Remove(versions[0]);
                        }
                        else
                        {
                            //删除相同版本号的文件
                            var toRemove = versions.FirstOrDefault(m => m.AttachVer == baseAttach.AttachVer);
                            if (toRemove != null)
                            {
                                if (baseAttach.AttachRefTable == "DesTaskAttach")
                                {
                                    toDeleteSplittedFiles.Add(toRemove.AttachVerId);
                                }
                                attachVersionDB.Remove(toRemove);
                                //获取出当前最大的版本号作为当前版本
                                var current = versions.Where(m => m.AttachVer != baseAttach.AttachVer).OrderByDescending(m => m.AttachVer).FirstOrDefault();
                                if (current != null)
                                {
                                    baseAttach.AttachVer = current.AttachVer;
                                    baseAttach.AttachDir = current.AttachDir;
                                    baseAttach.AttachSize = current.AttachSize;
                                    baseAttach.AttachDateUpload = current.AttachDateUpload;
                                    baseAttach.AttachDateChange = current.AttachDateChange;
                                    baseAttach.AttachEmpID = current.AttachEmpID;
                                    baseAttach.AttachEmpName = current.AttachEmpName;
                                }
                            }
                        }
                    }
                    if (toDeleteSplittedFiles.Count > 0)
                    {
                        var splitedFileDB = this.DbContext.Set<DataModel.Models.DesTaskSplitFile>();
                        var splitedFiles = splitedFileDB.Where(m => toDeleteSplittedFiles.Contains(m.BaseAttachVerID)).ToList();
                        foreach (var splitFile in splitedFiles)
                        {
                            toDeleteFiles.Add(splitFile.Path);
                            splitedFileDB.Remove(splitFile);
                        }
                    }
                    this.DbContext.SaveChanges();
                    tran.Commit();
                    isSuccess = true;
                }
                catch
                {
                    tran.Rollback();
                }
            }
            if (isSuccess && toDeleteFiles.Count > 0)
            {
                var localGoldFile = JQ.Util.ConfigUtil.GetConfigValue("UpFileRoot");
                foreach (var file in toDeleteFiles)
                {
                    JQ.Util.IOUtil.TryDeleteFile(Path.Combine(localGoldFile, file));
                }
            }
        }

        public void DeleteVersions(List<long> attachVersionIDs)
        {
            if (attachVersionIDs == null || attachVersionIDs.Count == 0)
            {
                return;
            }
            var attachVersionDB = this.DbContext.Set<DataModel.Models.BaseAttachVer>();
            var attachDB = this.DbContext.Set<DataModel.Models.BaseAttach>();
            var versions = attachVersionDB.Where(m => attachVersionIDs.Contains(m.AttachVerId)).ToList();
            List<string> toDeleteFiles = new List<string>();
            var isSuccess = false;
            using (var tran = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var version in versions)
                    {
                        toDeleteFiles.Add(version.AttachDir);
                        //获取出当前同一个attachID的version
                        var baseAttach = version.FK_BaseAttachVer_AttachID;
                        attachVersionDB.Remove(version);
                        if (baseAttach == null)
                        {
                            continue;
                        }
                        var sames = attachVersionDB.Where(m => m.AttachID == version.AttachID).ToList();
                        if (sames.Count == 1)
                        {
                            attachDB.Remove(version.FK_BaseAttachVer_AttachID);
                        }
                        else
                        {
                            //获取出最新的版本
                            var current = sames.Where(m => m.AttachVerId != version.AttachVerId).OrderByDescending(m => m.AttachVer).FirstOrDefault();
                            if (current != null)
                            {
                                baseAttach.AttachVer = current.AttachVer;
                                baseAttach.AttachDir = current.AttachDir;
                                baseAttach.AttachSize = current.AttachSize;
                                baseAttach.AttachDateUpload = current.AttachDateUpload;
                                baseAttach.AttachDateChange = current.AttachDateChange;
                                baseAttach.AttachEmpID = current.AttachEmpID;
                                baseAttach.AttachEmpName = current.AttachEmpName;
                            }
                        }
                    }
                    this.DbContext.SaveChanges();
                    tran.Commit();
                    isSuccess = true;
                }
                catch
                {
                    tran.Rollback();
                }
            }
            if (isSuccess && toDeleteFiles.Count > 0)
            {
                var localGoldFile = JQ.Util.ConfigUtil.GetConfigValue("UpFileRoot");
                foreach (var file in toDeleteFiles)
                {
                    JQ.Util.IOUtil.TryDeleteFile(Path.Combine(localGoldFile, file));
                }
            }
        }


        public void Delete(int[] ids, string refTable)
        {
            var attachDB = this.DbContext.Set<DataModel.Models.BaseAttach>();
            var baseAttaches = attachDB.Where(m => m.AttachRefTable == refTable && ids.Contains(m.AttachRefID)).ToList();
            var attachVersionDB = this.DbContext.Set<DataModel.Models.BaseAttachVer>();
            List<string> toDeleteFiles = new List<string>();
            var isSuccess = false;
            using (var tran = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var baseAttach in baseAttaches)
                    {
                        foreach (var version in baseAttach.FK_BaseAttachVer_AttachID.ToList())
                        {
                            toDeleteFiles.Add(version.AttachDir);
                            attachVersionDB.Remove(version);
                        }
                        attachDB.Remove(baseAttach);
                    }
                    this.DbContext.SaveChanges();
                    tran.Commit();
                    isSuccess = true;
                }
                catch
                {
                    tran.Rollback();
                }
            }
            if (isSuccess && toDeleteFiles.Count > 0)
            {
                var localGoldFile = JQ.Util.ConfigUtil.GetConfigValue("UpFileRoot");
                foreach (var file in toDeleteFiles)
                {
                    JQ.Util.IOUtil.TryDeleteFile(Path.Combine(localGoldFile, file));
                }
            }
        }

        public void SaveDWGFromGCADQM(int taskID,int fileId, string originalFilePath, string originalRealName, List<string> splitedFiles, int empID, string empName)
        {
            if (taskID == 0 || empID == 0)
            {
                return;
            }
            var localGoldFile = JQ.Util.ConfigUtil.GetConfigValue("UpFileRoot");
            using (var tran = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    var baseAttachVersion = GetBaseAttachAndVersionList(taskID, "DesTaskAttach").Where(p=>p.AttachID==fileId).OrderBy(p => p.AttachVer).LastOrDefault();
                    //var baseAttachVersion = SaveFile(taskID, "DesTaskAttach", originalRealName, originalFilePath, 0, DateTime.Now, empID, empName);
                    if (baseAttachVersion == null)
                    {
                        throw new Common.JQException("获取文件失败！");
                    }
                    //插入表
                    var root = Path.Combine(Path.GetDirectoryName(baseAttachVersion.AttachDir), Path.GetFileNameWithoutExtension(baseAttachVersion.AttachDir));
                    if (!Directory.Exists(Path.Combine(localGoldFile, root)))
                    {
                        Directory.CreateDirectory(Path.Combine(localGoldFile, root));
                    }
                    foreach (var file in splitedFiles)
                    {
                        if (!File.Exists(file))
                        {
                            continue;
                        }
                        var fileInfo = new FileInfo(file);
                        var splitFile = new DataModel.Models.DesTaskSplitFile()
                        {
                            Attributes = "<Root />",
                            BaseAttachID = baseAttachVersion.AttachID,
                            BaseAttachVerID = baseAttachVersion.AttachVerId,
                            BaseAttachVer = baseAttachVersion.AttachVer,
                            CreationTime = DateTime.Now,
                            CreatorEmpId = empID,
                            CreatorEmpName = empName,
                            Name = fileInfo.Name.Substring(fileInfo.Name.IndexOf("_") + 1),
                            Size = fileInfo.Length
                        };
                        splitFile.Extension = Path.GetExtension(splitFile.Name).TrimStart('.').ToLower();
                        //转移文件
                        splitFile.Path = Path.Combine(root, Guid.NewGuid().ToString() + (splitFile.Extension == "" ? "" : ("." + splitFile.Extension)));
                        var targetPath = Path.Combine(localGoldFile, splitFile.Path);
                        if (File.Exists(targetPath))
                        {
                            JQ.Util.IOUtil.TryDeleteFile(targetPath);
                        }
                        File.Move(file, targetPath);
                        int result = new Design.DesTaskSplitFile().DeleteSplitFile(splitFile.BaseAttachID, splitFile.BaseAttachVerID, splitFile.BaseAttachVer);

                        this.DbContext.Set<DataModel.Models.DesTaskSplitFile>().Add(splitFile);
                    }
                    this.DbContext.SaveChanges();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
            }
            JQ.Util.IO.MessageMonitor.Notify(empID, delegate (int _empID)
            {
                return new { action = "DWGSubmited", taskID = taskID.ToString() };
            });
        }

        public void SaveDWGFromGCAD(int taskID, string originalFilePath, string originalRealName, List<string> splitedFiles, int empID, string empName)
        {
            if (taskID == 0 || empID == 0)
            {
                return;
            }
            var localGoldFile = JQ.Util.ConfigUtil.GetConfigValue("UpFileRoot");
            using (var tran = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    var baseAttachVersion = SaveFile(taskID, "DesTaskAttach", originalRealName, originalFilePath, 0, DateTime.Now, empID, empName);
                    if (baseAttachVersion == null)
                    {
                        throw new Common.JQException("无法保存！");
                    }
                    //插入表
                    var root = Path.Combine(Path.GetDirectoryName(baseAttachVersion.AttachDir), Path.GetFileNameWithoutExtension(baseAttachVersion.AttachDir));
                    if (!Directory.Exists(Path.Combine(localGoldFile, root)))
                    {
                        Directory.CreateDirectory(Path.Combine(localGoldFile, root));
                    }
                    foreach (var file in splitedFiles)
                    {
                        if (!File.Exists(file))
                        {
                            continue;
                        }
                        var fileInfo = new FileInfo(file);
                        var splitFile = new DataModel.Models.DesTaskSplitFile()
                        {
                            Attributes = "<Root />",
                            BaseAttachID = baseAttachVersion.AttachID,
                            BaseAttachVerID = baseAttachVersion.AttachVerId,
                            BaseAttachVer = baseAttachVersion.AttachVer,
                            CreationTime = DateTime.Now,
                            CreatorEmpId = empID,
                            CreatorEmpName = empName,
                            Name = fileInfo.Name.Substring(fileInfo.Name.IndexOf("_") + 1),
                            Size = fileInfo.Length
                        };
                        splitFile.Extension = Path.GetExtension(splitFile.Name).TrimStart('.').ToLower();
                        //转移文件
                        splitFile.Path = Path.Combine(root, Guid.NewGuid().ToString() + (splitFile.Extension == "" ? "" : ("." + splitFile.Extension)));
                        var targetPath = Path.Combine(localGoldFile, splitFile.Path);
                        if (File.Exists(targetPath))
                        {
                            JQ.Util.IOUtil.TryDeleteFile(targetPath);
                        }
                        File.Move(file, targetPath);
                        this.DbContext.Set<DataModel.Models.DesTaskSplitFile>().Add(splitFile);
                    }
                    this.DbContext.SaveChanges();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
            }
            JQ.Util.IO.MessageMonitor.Notify(empID, delegate (int _empID)
             {
                 return new { action = "DWGSubmited", taskID = taskID.ToString() };
             });
        }

        private bool _isDisposed = false;
        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }
            _isDisposed = true;
            if (this._dbContext != null)
            {
                this._dbContext.Dispose();
            }
        }
    }
}
