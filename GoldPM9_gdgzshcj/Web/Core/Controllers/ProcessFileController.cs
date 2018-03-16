using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using JQ.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace Core.Controllers
{
    public class ProcessFileController : CoreController
    {
        public string Upload()
        {
            var offset = (Request.Form["chunk"] != null ? int.Parse(Request.Form["chunk"]) : 0); //获取当前的块ID，如果不是分块上传的。chunk则为0
            var total = (Request.Form["chunks"] != null ? int.Parse(Request.Form["chunks"]) : 0);//总的分块数量
            var fileID = Request.Form["fileID"];
            var filePath = "";
            if (offset == 0 || Session[fileID] == null)
            {
                filePath = Path.Combine(JQ.Util.IOUtil.GetTempPath(), Guid.NewGuid().ToString());
                if (total > 1)
                {
                    Session[fileID] = filePath;
                }
            }
            else
            {
                filePath = Session[fileID].ToString();
                if (total == offset + 1)
                {
                    Session.Remove(fileID);
                }
            }
            byte[] buffer = null;
            if (Request.ContentType == "application/octet-stream" && Request.ContentLength > 0)
            {
                buffer = new Byte[Request.InputStream.Length];
                Request.InputStream.Read(buffer, 0, buffer.Length);
            }
            else if (Request.ContentType.Contains("multipart/form-data") && Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                buffer = new Byte[Request.Files[0].InputStream.Length];
                Request.Files[0].InputStream.Read(buffer, 0, buffer.Length);//上传文件的流读入buffer中
            }
            using (var fs = new FileStream(filePath, FileMode.Append))
            {
                fs.Write(buffer, 0, buffer.Length);//把buffer中的字节数写入文件流中，保存到指定的路径
            }
            if (total == offset + 1)
            {
                var refID = Common.ModelConvert.ConvertToDefault<int>(Request.Form["refID"]);
                if (refID == 0)
                {
                    return "{\"Name\": \"" + Path.GetFileName(filePath) + "\",\"UploadDate\":\"" + DateTime.Now.ToString("yyyy-MM-dd") + "\",\"EmpName\":\"" + HttpUtility.UrlEncode(userInfo.EmpName) + "\"}";
                }
                else
                {
                    using (var ba = new DBSql.Sys.BaseAttach())
                    {
                        try
                        {
                            var baseAttach = ba.SaveFile(refID, (Request.Form["refTable"] ?? ""), (Request.Form["name"] ?? ""), filePath, long.Parse(Request.Form["parentID"] ?? "0"), DateTime.Parse(Request.Form["lastModifiedTime"]), userInfo.EmpID, userInfo.EmpName);
                            if (baseAttach != null)
                            {
                                return "{\"Status\":1,\"AttachVersionID\":" + baseAttach.AttachVerId + ", \"AttachID\":" + baseAttach.AttachID + ",\"Version\":" + baseAttach.AttachVer + ",\"UploadDate\":\"" + DateTime.Now.ToString("yyyy-MM-dd") + "\",\"EmpName\":\"" + HttpUtility.UrlEncode(userInfo.EmpName) + "\"}";
                            }
                        }
                        catch (Common.JQException)
                        {
                            //文件最后修改时间一致
                            return "{\"Status\":2}";
                        }

                    }
                }
            }
            return "";
        }

        public JsonResult GetAttachFiles()
        {
            var refID = Common.ModelConvert.ConvertToDefault<int>(Request.Form["refID"]);
            var refTable = Request.Form["refTable"];
            if (refID == 0 || string.IsNullOrEmpty(refTable))
            {
                return Json(new { total = 0, rows = new object[] { } });
            }
            var mode = Common.ModelConvert.ConvertToDefault<int>(Request.Form["mode"]);
            if (mode == 0)
            {
                //显示最新版本
                using (var dbBA = new DBSql.Sys.BaseAttach())
                {
                    var list = dbBA.GetBaseAttachList(refID, refTable).
                        Select(m => new
                        {
                            ID = m.AttachID,
                            Name = m.AttachName,
                            Size = m.AttachSize,
                            LastModifyDate = m.AttachDateChange,
                            UploadDate = m.AttachDateUpload,
                            EmpName = m.AttachEmpName,
                            Version = m.AttachVer,
                            Type = "attach"
                        }).ToList();
                    return Json(new { total = list.Count, rows = list });
                }
            }
            else
            {
                //显示所有版本
                using (var dbBA = new DBSql.Sys.BaseAttach())
                {
                    var list = dbBA.GetBaseAttachVersionList(refID, refTable);
                    return Json(new { total = list.Count, rows = list });
                }
            }

        }

        public JsonResult Delete()
        {
            var idSet = Request.Form["idSet"];
            List<long> ids = new List<long>();
            foreach (var id in idSet.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var temp = 0L;
                if (long.TryParse(id, out temp) && temp > 0)
                {
                    ids.Add(temp);
                }
            }
            //删除文件
            var mode = Common.ModelConvert.ConvertToDefault<int>(Request.Form["mode"]);
            using (var ba = new DBSql.Sys.BaseAttach())
            {
                if (mode == 0)
                {
                    ba.Delete(ids);
                }
                else
                {
                    ba.DeleteVersions(ids);
                }
            }
            return Json(new { Result = true });
        }

        public void DeleteTemp()
        {
            var nameSet = Request.Form["nameSet"];
            if (string.IsNullOrEmpty(nameSet))
            {
                return;
            }
            var localGoldFile = JQ.Util.IOUtil.GetTempPath();
            foreach (var name in nameSet.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                JQ.Util.IOUtil.TryDeleteFile(Path.Combine(localGoldFile, name));
            }
        }

        public void CancelUpload()
        {
            var fileID = Request.Form["fileID"];
            if (string.IsNullOrEmpty(fileID) || Session[fileID] == null)
            {
                return;
            }
            JQ.Util.IOUtil.TryDeleteFile(Session[fileID].ToString());
        }

        public FileResult Download()
        {
            var id = Common.ModelConvert.ConvertToDefault<int>(Request.QueryString["id"]);
            if (id == 0)
            {
                var name = HttpUtility.UrlDecode(Request.QueryString["name"] ?? "");
                if (string.IsNullOrEmpty(name))
                {
                    throw new HttpException(404, "Not found");
                }
                var realName = name;
                if (!string.IsNullOrEmpty(Request.QueryString["realName"]))
                {
                    realName = HttpUtility.UrlDecode(Request.QueryString["realName"]);
                }
                return ResponseFile(Path.Combine(JQ.Util.IOUtil.GetTempPath(), name), realName);
            }
            else
            {
                var type = Request.QueryString["type"];
                if (type == "attach")
                {
                    if (Request.QueryString["version"] != null)
                    {
                        var version = Common.ModelConvert.ConvertToDefault<int>(Request.QueryString["version"]);
                        if (version > 0)
                        {
                            using (var ba = new DBSql.Sys.BaseAttachVer())
                            {
                                var data = ba.FirstOrDefault(m => m.AttachID == id && m.AttachVer == version);
                                if (data != null && data.FK_BaseAttachVer_AttachID != null)
                                {
                                    return ResponseFile(Path.Combine(JQ.Util.ConfigUtil.GetConfigValue("UpFileRoot"), data.AttachDir), data.FK_BaseAttachVer_AttachID.AttachName);
                                }
                            }
                            throw new HttpException(404, "Not found");
                        }
                    }
                    using (var ba = new DBSql.Sys.BaseAttach())
                    {
                        var data = ba.FirstOrDefault(m => m.AttachID == id);
                        if (data != null)
                        {
                            return ResponseFile(Path.Combine(JQ.Util.ConfigUtil.GetConfigValue("UpFileRoot"), data.AttachDir), data.AttachName);
                        }
                        throw new HttpException(404, "Not found");
                    }
                }
                else if (type == "attachversion")
                {
                    using (var ba = new DBSql.Sys.BaseAttachVer())
                    {
                        var data = ba.FirstOrDefault(m => m.AttachVerId == id);
                        if (data != null && data.FK_BaseAttachVer_AttachID != null)
                        {
                            return ResponseFile(Path.Combine(JQ.Util.ConfigUtil.GetConfigValue("UpFileRoot"), data.AttachDir), data.FK_BaseAttachVer_AttachID.AttachName);
                        }
                    }
                }
                else if (type == "DesignSplitFile")
                {
                    using (var db = new DBSql.Design.DesTaskSplitFile())
                    {
                        var data = db.FirstOrDefault(m => m.ID == id);
                        if (data != null)
                        {
                            return ResponseFile(Path.Combine(JQ.Util.ConfigUtil.GetConfigValue("UpFileRoot"), data.Path), data.Name);
                        }
                    }
                }
                else if (type == "DesignSplitFileSign")
                {
                    using (var db = new DBSql.Design.DesTaskSplitFile())
                    {
                        var data = db.FirstOrDefault(m => m.ID == id);
                        if (data != null)
                        {
                            var path = Path.Combine(JQ.Util.ConfigUtil.GetConfigValue("UpFileRoot"), data.Path);
                            return ResponseFile(Path.Combine(Path.GetDirectoryName(path), "Sign", Path.GetFileName(path)), Path.GetFileNameWithoutExtension(data.Name) + "（签名）" + Path.GetExtension(data.Name));
                        }
                    }
                }
            }
            throw new HttpException(404, "Not found");
        }

        private FileResult ResponseFile(string path, string realName)
        {
            if (!System.IO.File.Exists(path))
            {
                throw new HttpException(404, "Not found");
            }
            return File(path, "application/octet-stream", realName);
        }

        public JsonResult BatchDownload()
        {
            var data = Request.Form["data"] ?? "";
            if (string.IsNullOrEmpty(data))
            {
                return Json(new { Result = false, Message = "参数错误" });
            }
            XmlDocument xmlDocument = new XmlDocument();
            try
            {
                xmlDocument.LoadXml(HttpUtility.HtmlDecode(data));
            }
            catch
            {
                return Json(new { Result = false, Message = "参数错误" });
            }
            var realName = "";
            Dictionary<string, string> toZipFiles = new Dictionary<string, string>();
            var loadGoldFile = JQ.Util.ConfigUtil.GetConfigValue("UpFileRoot");
            var xNodes = xmlDocument.SelectNodes("Root/File[@Type=\"attach\"]");
            if (xNodes.Count > 0)
            {
                var ids = new List<long>();
                var groups = new Dictionary<long, string>();
                foreach (XmlElement xAttachNode in xNodes)
                {
                    var temp = 0L;
                    if (long.TryParse(xAttachNode.GetAttribute("ID"), out temp))
                    {
                        ids.Add(temp);
                        groups.Add(temp, xAttachNode.GetAttribute("Group"));
                    }
                }
                if (ids.Count > 0)
                {
                    using (var dt = DAL.DBExecute.ExecuteDataTable("SELECT AttachID,AttachName,AttachDir,ISNULL(dbo.F_GetBaseAttachPathText(AttachID, '\\'), '') AS AttachPath FROM BaseAttach WHERE AttachID IN (" + string.Join(",", ids.ToArray()) + ")"))
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (string.IsNullOrEmpty(realName))
                            {
                                realName = row["AttachName"].ToString();
                            }
                            InToZipFiles(toZipFiles, Path.Combine(groups[row.Field<long>("AttachID")], row["AttachPath"].ToString(), row["AttachName"].ToString()), Path.Combine(loadGoldFile, row["AttachDir"].ToString()));
                        }
                    }
                }
            }
            xNodes = xmlDocument.SelectNodes("Root/File[@Type=\"attachversion\"]");
            if (xNodes.Count > 0)
            {
                var ids = new List<long>();
                foreach (XmlElement xAttachNode in xNodes)
                {
                    var temp = 0L;
                    if (long.TryParse(xAttachNode.GetAttribute("ID"), out temp))
                    {
                        ids.Add(temp);
                    }
                }
                if (ids.Count > 0)
                {
                    using (var dt = DAL.DBExecute.ExecuteDataTable("SELECT AttachName,AttachDir FROM BaseAttachVer WHERE AttachVerID IN (" + string.Join(",", ids.ToArray()) + ")"))
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (string.IsNullOrEmpty(realName))
                            {
                                realName = row["AttachName"].ToString();
                            }
                            InToZipFiles(toZipFiles, row["AttachName"].ToString(), Path.Combine(loadGoldFile, row["AttachDir"].ToString()));
                        }
                    }
                }
            }
            xNodes = xmlDocument.SelectNodes("Root/File[@Type=\"temp\"]");
            var tempPath = JQ.Util.IOUtil.GetTempPath();
            if (xNodes.Count > 0)
            {
                foreach (XmlElement xNode in xNodes)
                {
                    if (string.IsNullOrEmpty(realName))
                    {
                        realName = xNode.GetAttribute("RealName");
                    }
                    InToZipFiles(toZipFiles, xNode.GetAttribute("RealName"), Path.Combine(tempPath, xNode.InnerText));
                }
            }
            if (toZipFiles.Count == 0)
            {
                return Json(new { Result = false, Message = "未找到需要下载的文件" });
            }
            if (toZipFiles.Count > 1)
            {
                realName += " 等" + toZipFiles.Count + "个文件";
            }
            var name = Guid.NewGuid().ToString();
            ZipFiles(toZipFiles, Path.Combine(tempPath, name));
            return Json(new { Result = true, RemoteName = name, RealName = realName + ".zip" });
        }

        private void ZipFiles(Dictionary<string, string> files, string targetPath)
        {
            using (ZipOutputStream outPutStream = new ZipOutputStream(new FileStream(targetPath, FileMode.OpenOrCreate)))
            {
                outPutStream.SetLevel(6);
                Crc32 crc = new Crc32();
                foreach (var file in files)
                {
                    if (!System.IO.File.Exists(file.Value))
                    {
                        continue;
                    }
                    using (var fs = System.IO.File.OpenRead(file.Value))
                    {
                        crc.Reset();
                        var ent = new ZipEntry(file.Key);
                        ent.DateTime = DateTime.Now;
                        ent.Size = fs.Length;
                        outPutStream.PutNextEntry(ent);
                        var readedLength = 0L;
                        byte[] buffer = null;
                        while (readedLength < fs.Length)
                        {
                            buffer = new byte[20480];
                            var rl = fs.Read(buffer, 0, buffer.Length);
                            readedLength += rl;
                            crc.Update(buffer, 0, rl);
                            outPutStream.Write(buffer, 0, rl);
                        }
                        ent.Crc = crc.Value;
                        ent = null;
                    }
                }
                crc = null;
            }
            GC.Collect();
            GC.Collect(1);
        }

        private void InToZipFiles(Dictionary<string, string> files, string name, string path)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(path) || files.ContainsValue(path) || !System.IO.File.Exists(path))
            {
                //重复的文件无需压缩
                return;
            }
            if (files.ContainsKey(name))
            {
                var i = 1;
                while (files.ContainsKey("（" + i.ToString() + "）" + name))
                {
                    i++;
                }
                name = "（" + i.ToString() + "）" + name;
            }
            files.Add(name, path);
        }


        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <returns></returns>
        public void TaskCreateFolder(int AttchId)
        {
            long reuslt = 0;
            DBSql.Sys.BaseAttach attachOp = new DBSql.Sys.BaseAttach();
            DataModel.Models.BaseAttach attachModel = new DataModel.Models.BaseAttach();
            if (AttchId > 0)
            {
                attachModel = attachOp.Get(AttchId);
                attachOp.MvcSetValue();
                attachOp.MvcDefaultEdit(userInfo.EmpID);
                attachModel.AttachName = JQ.Util.StringUtil.ReplaceStr(attachModel.AttachName);
                attachOp.Edit(attachModel);
                attachOp.UnitOfWork.SaveChanges();
                reuslt = attachModel.AttachID;
            }
            else
            {
                attachModel.MvcSetValue();
                attachModel.MvcDefaultSave(userInfo.EmpID);
                attachModel.AttachName = JQ.Util.StringUtil.ReplaceStr(attachModel.AttachName);
                attachModel.AttachExt = ".";

                if (attachModel.AttachParentID > 0)
                {
                    var prtAttach = attachOp.Get(attachModel.AttachParentID);
                    attachModel.AttachParentID = prtAttach.AttachID;
                    attachModel.AttachLevel = prtAttach == null ? 0 : prtAttach.AttachLevel + 1;
                    attachModel.AttachOrderNum = attachOp.GetQuery(x => x.AttachRefID == attachModel.AttachRefID && x.AttachRefTable == attachModel.AttachRefTable && x.AttachParentID == prtAttach.AttachID).Count() + 1;
                    attachModel.AttachOrderPath = prtAttach == null ? attachModel.AttachOrderNum.ToString("0000") : prtAttach.AttachOrderPath + "_" + attachModel.AttachOrderNum.ToString("0000");
                    attachModel.AttachPathIDs = String.IsNullOrWhiteSpace(prtAttach.AttachPathIDs) ? prtAttach.AttachID.ToString() : prtAttach.AttachPathIDs + "," + prtAttach.AttachID.ToString();
                }
                else
                {
                    attachModel.AttachParentID = 0;
                    attachModel.AttachLevel = 0;
                    attachModel.AttachOrderNum = attachOp.GetQuery(x => x.AttachRefID == attachModel.AttachRefID && x.AttachRefTable == attachModel.AttachRefTable && x.AttachParentID == 0).Count() + 1;
                    attachModel.AttachOrderPath = attachModel.AttachOrderNum.ToString("0000");
                    attachModel.AttachPathIDs = "";
                }
                attachModel.ColAttXml = "";
                attachOp.Add(attachModel);
                attachOp.UnitOfWork.SaveChanges();
                reuslt = attachModel.AttachID;
            }
        }

    }
}
