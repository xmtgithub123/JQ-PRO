using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using JQ.Web;
using JQ.Util;
using System.Configuration;
using System.IO;
using System.Web.UI;
using DataModel.Models;
using System.Xml;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Net;
using System.Data;
using System.Threading;
using System.Drawing;
using Aspose.Words;
using System.Text;

namespace Core.Controllers
{

    [Description("用户控件")]
    public class usercontrolController : CoreController
    {
        private DBSql.Sys.BaseData bd = new DBSql.Sys.BaseData();

        #region 基础数据控件
        [Description("基础数据控件")]
        public ActionResult basedata(string EngName = "")
        {
            string[] arr = EngName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            ViewBag.EngName = arr;
            return View();
        }
        #endregion

        #region 文件上传控件
        [Description("文件上传控件")]
        public ActionResult uploadFile(DataModel.Upload m)
        {
            EncryInfo Encry = new EncryInfo("jqFtp");
            m.EmpID = userInfo.EmpID;
            m.FtpServer = Request.ServerVariables["LOCAL_ADDR"];
            m.FtpServerPort = "21";
            m.FtpLogin = ConfigurationManager.AppSettings["FtpUserName"];
            m.FtppassWord = Encry.Encrypt(ConfigurationManager.AppSettings["FtpPassword"]);
            m.FileFilter = ConfigurationManager.AppSettings["UploadDownType"];
            m.FileMaxLength = "200000";
            m.IsJinquDwg = m.AttachRefTable == "EngNode" ? "1" : "0";
            var name = m.Name ?? Guid.NewGuid().ToString();
            ViewBag.tid = "tt" + name;
            ViewBag.tbid = "tb" + name;
            return View(m);
        }

        [Description("文件数据json单个ID")]
        public ActionResult json(string AttachRefTable, int AttachRefID = 0)
        {
            if (AttachRefID > 0)
            {
                var Path = ConfigurationManager.AppSettings["UpFileRoot"];
                var Attach = new DBSql.Sys.BaseAttach().GetBaseAttachList(AttachRefID, AttachRefTable);
                if (Attach == null) return null;
                var list = from a in Attach
                           select new
                           {
                               AttachID = a.AttachID,
                               UrL = GetGoldFileUrl(Request) + string.Format(@"/{0}/{1}", a.AttachDir, Uri.EscapeDataString(a.AttachName)),
                               FileFullName = Path + string.Format(@"\{0}\{1}", a.AttachDir, a.AttachName),
                               FileName = a.AttachName,
                               FileDate = a.AttachDateChange,
                           };
                return Json(list);
            }
            else
            {
                var FtpPath = string.Format(@"{0}\Emp{1}", ConfigurationManager.AppSettings["FtpLocalPath"], userInfo.EmpID);
                if (!Directory.Exists(FtpPath))
                {
                    Directory.CreateDirectory(FtpPath);
                }
                var arr = (from s in Directory.GetFiles(FtpPath)
                           where !s.EndsWith(".JqTmp")
                           select s).ToList();

                LoadChildDir(FtpPath, arr);
                var list = from a in arr
                           select new
                           {
                               AttachID = 0,
                               UrL = GetGoldFileUrl(Request) + string.Format(@"/ftproot/Emp{0}/{1}", userInfo.EmpID, Uri.EscapeDataString(a.Substring(a.IndexOf(string.Format("Emp{0}\\", userInfo.EmpID)) + 1))),
                               FileFullName = a,
                               FileName = new FileInfo(a).Name,
                               FileDate = new FileInfo(a).LastWriteTime,
                           };
                return Json(list);
            }
        }


        [Description("文件数据json多个ID")]
        public ActionResult jsonByRefIDs(string AttachRefTable, string AttachRefIDs)
        {
            List<DataModel.Models.BaseAttach> Attach = new List<BaseAttach>();
            var list = StringUtil.getListByString(AttachRefIDs, ',');
            var Path = ConfigurationManager.AppSettings["UpFileRoot"];
            var AttachEm = new DBSql.Sys.BaseAttach().GetBaseAttachList(list, AttachRefTable);
            if (AttachEm != null)
            {
                Attach.AddRange(AttachEm.ToList());
            }
            var ExendAttach = new DBSql.Sys.BaseAttach().GetExtendBaseAttachList(list, AttachRefTable);
            if (ExendAttach != null)
            {
                Attach.AddRange(ExendAttach.ToList());
            }
            if (Attach == null) return null;

            var result = from a in Attach
                         select new
                         {
                             AttachID = a.AttachID,
                             AttachRefID = a.AttachRefID,
                             //UrL = GetGoldFileUrl(Request) + string.Format(@"/{0}/{1}", a.AttachDir, Uri.EscapeDataString(a.AttachName)),
                             //UrL = Url.Action("Download", "ProcessFile", new { @area = "Core" }) + "?id=" + a.AttachID + "&type=attach",
                             UrL = GetUrl(AttachRefTable, a),
                             FileFullName = Path + string.Format(@"\{0}\{1}", a.AttachDir, a.AttachName),
                             FileName = a.AttachName,
                             FileDate = a.AttachDateChange,
                         };
            return Json(result);
        }

        public string GetUrl(string RefTable, DataModel.Models.BaseAttach model)
        {
            string url = "";
            switch (RefTable)
            {
                case "DesMutiSign":
                    url = Url.Action("Download", "ProcessFile", new { @area = "Core" }) + "?id=" + model.AttachID + "&type=attach&version=" + model.AttachVer;
                    break;
                default:
                    url = Url.Action("Download", "ProcessFile", new { @area = "Core" }) + "?id=" + model.AttachID + "&type=attach";
                    break;
            }
            return url;
        }

        [Description("文件上传窗体")]
        public ActionResult uploadByHttp(DataModel.Upload m)
        {
            return View(m);
        }

        [Description("保存文件")]
        public void saveFile(int RefID, int userID, string AttachRefTable)
        {
            var attach = new DBSql.Sys.BaseAttach();
            attach.UploadFileToBaseAttach(AttachRefTable, RefID, userID);
            attach.UnitOfWork.SaveChanges();
        }

        [Description("处理文件上传")]
        public ActionResult doUpload(HttpPostedFileBase file, DataModel.Upload m)
        {
            DoResult re = DoResultHelper.doSuccess("上传成功");
            Response.CacheControl = "no-cache";
            string s_rpath = GetUploadPath();
            string updir = s_rpath;
            string extname = string.Empty;
            string fullname = string.Empty;
            string filename = string.Empty;
            if (Request.Files.Count > 0)
            {
                try
                {
                    int offset = Convert.ToInt32(Request["chunk"]);
                    int total = Convert.ToInt32(Request["chunks"]);
                    string name = Request["name"];
                    //文件没有分块
                    if (total == 0)
                    {
                        #region MyRegion
                        if (file.ContentLength > 0)
                        {
                            if (!Directory.Exists(updir))
                            {
                                Directory.CreateDirectory(updir);
                            }
                            extname = Path.GetExtension(file.FileName);
                            fullname = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                            filename = file.FileName;

                            file.SaveAs(string.Format("{0}\\{1}", updir, filename));
                        }
                        #endregion
                    }
                    else
                    {
                        #region 文件 分成多块上传
                        fullname = WriteTempFile(file, offset);
                        if (total - offset == 1)
                        {
                            //如果是最后一个分块文件 ，则把文件从临时文件夹中移到上传文件 夹中
                            FileInfo fi = new FileInfo(fullname);
                            string oldFullName = string.Format("{0}\\{1}", updir, m.Name);
                            FileInfo oldFi = new FileInfo(oldFullName);
                            if (oldFi.Exists)
                            {
                                //文件名存在则删除旧文件
                                oldFi.Delete();
                            }
                            fi.MoveTo(oldFullName);
                        }
                        #endregion
                    }
                    if (m.AttachRefID > 0 && m.AttachRefTable.isNotEmpty())
                    {
                        saveFile(m.AttachRefID, this.userInfo.EmpID, m.AttachRefTable);
                    }
                }
                catch (Exception ex)
                {
                    re = DoResultHelper.doError("上传失败原因：" + ex.Message);
                }
            }
            return Json(re);
        }

        [Description("删除文件")]
        public ActionResult delFiles(string parames, string refTable)
        {
            var attach = new DBSql.Sys.BaseAttach();
            //List<UploadModel> list= JsonConvert.DeserializeObject<List<UploadModel>>(parames);
            var list = JavaScriptSerializerUtil.objectToEntity<List<UploadModel>>(parames);
            if (list.isNotEmpty())
            {
                foreach (var item in list)
                {
                    if (item.AttachID == 0)
                    {
                        System.IO.File.Delete(item.FileFullName);
                    }
                    else
                    {
                        List<long> l = new List<long>();
                        foreach (var i in list)
                        {
                            l.Add(i.AttachID);
                        }
                        attach.DeleteBaseAttach(l, refTable);
                    }
                }
                return Json(DoResultHelper.doSuccess("删除成功!!!"));
            }
            else
            {
                return Json(DoResultHelper.doError("非获取到相关的参数!!!"));
            }
        }
        #endregion

        #region 流程控件
        [Description("流程控件")]
        public ActionResult flowmain(DataModel.FlowControlPar f)
        {
            return View(f);
        }

        [Description("流程控件提交下一步")]
        public ActionResult flowsubnext(DataModel.FlowNextPar f)
        {
            //HttpContext.CurrentHandler
            //RequestContext.
            //new UrlHelper(Request).Action("");
            // Url.Action
            return View(f);
        }
        #endregion

        #region 自定义搜索控件
        [Description("自定义搜索控件")]
        public ActionResult customSearch(string parames)
        {
            ViewBag.parames = parames;
            return View();
        }

        [Description("自定义搜索控件弹出框")]
        public ActionResult customSearchDialog(CSDialog csd)
        {
            return View(csd);
        }
        #endregion

        #region 导出excel请求流
        [Description("导出excel请求流")]
        [HttpPost]
        [ValidateInput(false)]
        public void exportExcel()
        {
            string name = HttpUtility.HtmlEncode(Request["txtName"] ?? DateTime.Now.ToString("yyyyMMdd")) + ".xls\"";
            string content = Request["txtContent"];
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "gb2312";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
            Response.AppendHeader("content-disposition", "attachment;filename=\"" + name);
            Response.ContentType = "Application/ms-excel";
            Response.Write("<html>\n<head>\n");

            var meta = string.Format("<meta   http-equiv='Content-Type';content='text/html';charset='{0}'>", "gb2312");
            Response.Write(meta);

            Response.Write("<style type=\"text/css\">\n.pb{font-size:13px;border-collapse:collapse;} " +
                           "\n.pb th{font-weight:bold;text-align:center;border:0.5pt solid windowtext;padding:2px;} " +
                           "\n.pb td{border:0.5pt solid windowtext;padding:2px;mso-number-format:\"\\@\";}\n</style>\n</head>\n");
            Response.Write("<body>\n" + content.Replace("null", "") + "\n</body>");
            Response.Write("\n</html>");
            Response.Flush();
            Response.End();
        }
        #endregion

        #region 选择icon列表
        public ActionResult iconlist()
        {
            return View();
        }
        #endregion

        #region 选择人员
        [Description("根据部门选择人员")]
        public ActionResult selEmployee(SelEmpParames model)
        {
            return View(model);
        }

        [Description("根据部门选择人员1")]
        public ActionResult selEmployee1(SelEmpParames model)
        {
            return View(model);
        }

        [Description("根据部门选择人员数据")]
        public string employeeByDepJson(string state = "closed", string ids = "", int type = 0)
        {
            List<BaseEmployee> listEmp = null;
            if (type == 1)
            {
                listEmp = new DBSql.Sys.BaseEmployee().AllData.Where(o => o.EmpIsDeleted == false).Where(m => (("," + ids + ",").Contains("," + m.EmpID + ","))).ToList();
            }
            else
            {
                listEmp = new DBSql.Sys.BaseEmployee().AllData.Where(o => o.EmpIsDeleted == false).Where(m => !(("," + ids + ",").Contains("," + m.EmpID + ","))).ToList();
            }
            var listDep = bd.getDeparmentList();
            if (Request.Form["permission"] == "dep")
            {
                //部门查看权
                var depID = 0;
                if (Request.Form["empDepID"] != null)
                {
                    depID = JQ.Util.TypeParse.parse<int>(Request.Form["empDepID"]);
                }
                else
                {
                    depID = userInfo.EmpDepID;
                }
                listDep.RemoveAll(m => m.BaseID != depID);
            }
            else if (Request.Form["permission"] == "emp")
            {
                var empID = 0;
                if (Request.Form["empID"] != null)
                {
                    empID = JQ.Util.TypeParse.parse<int>(Request.Form["empID"]);
                }
                else
                {
                    empID = userInfo.EmpID;
                }
                listEmp.RemoveAll(m => m.EmpID != empID);
            }
            var result = new
            {
                id = -1,
                text = type == 1 ? "已选人员" : "请选择人员",
                state = "open",
                children = from a in listDep
                           where listEmp.Where(ss => ss.EmpDepID == a.BaseID).Count() > 0
                           select new
                           {
                               id = a.BaseID,
                               text = a.BaseName,
                               state = type == 0 ? "closed" : "open",
                               children = from c in listEmp
                                          where c.EmpDepID == a.BaseID
                                          select new
                                          {
                                              id = c.EmpID + "-" + a.BaseID,
                                              isEmp = true,
                                              text = c.EmpName,
                                              iconCls = "fa fa-user",
                                              empid = c.EmpID
                                          }
                           }
            };
            var list = new List<object>();
            list.Add(result);
            return JavaScriptSerializerUtil.getJson(list);
        }

        [Description("根据部门选择人员数据,带查询条件")]
        [HttpGet]
        public string employeeByDepJson_Search(string keys = "")
        {
            List<BaseEmployee> listEmp = null;
            if (!string.IsNullOrEmpty(keys))
            {
                listEmp = new DBSql.Sys.BaseEmployee().AllData.Where(o => o.EmpIsDeleted == false).Where(x => x.EmpName.Contains(keys)).ToList();
            }
            else
            {
                listEmp = new DBSql.Sys.BaseEmployee().AllData.Where(o => o.EmpIsDeleted == false).ToList();
            }


            var listDep = bd.getDeparmentList();
            if (Request.Form["permission"] == "dep")
            {
                //部门查看权
                var depID = 0;
                if (Request.Form["empDepID"] != null)
                {
                    depID = JQ.Util.TypeParse.parse<int>(Request.Form["empDepID"]);
                }
                else
                {
                    depID = userInfo.EmpDepID;
                }
                listDep.RemoveAll(m => m.BaseID != depID);
            }
            else if (Request.Form["permission"] == "emp")
            {
                var empID = 0;
                if (Request.Form["empID"] != null)
                {
                    empID = JQ.Util.TypeParse.parse<int>(Request.Form["empID"]);
                }
                else
                {
                    empID = userInfo.EmpID;
                }
                listEmp.RemoveAll(m => m.EmpID != empID);
            }

            var result = new
            {
                id = -1,
                text = "请选择人员",
                state = "open",
                children = from a in listDep
                           where listEmp.Where(ss => ss.EmpDepID == a.BaseID).Count() > 0
                           select new
                           {
                               id = a.BaseID,
                               text = a.BaseName,
                               state = "open",
                               children = from c in listEmp
                                          where c.EmpDepID == a.BaseID
                                          select new
                                          {
                                              id = c.EmpID + "-" + a.BaseID,
                                              isEmp = true,
                                              text = c.EmpName,
                                              iconCls = "fa fa-user",
                                              empid = c.EmpID
                                          }
                           }
            };
            var list = new List<object>();
            list.Add(result);
            return JavaScriptSerializerUtil.getJson(list);
        }


        [Description("根据选择人员数据")]
        public string employeeJson()
        {
            var listEmp = new DBSql.Sys.AllBaseEmployee().GetList(o => o.EmpIsDeleted == false).ToList();


            var result = from a in listEmp
                         select new
                         {
                             EmpID = a.EmpID,
                             EmpName = a.EmpName,
                             EmpDepName = a.DepartmentName
                         };
            return JavaScriptSerializerUtil.getJson(result);
        }

        [Description("根据选择人员数据,排除自己")]
        public string employeeQtJson()
        {

            var listEmp = new DBSql.Sys.AllBaseEmployee().GetList(o => o.EmpIsDeleted == false).ToList();
            var myobj = listEmp.Where(x => x.EmpID == userInfo.EmpID).First(); ;
            listEmp.Remove(myobj);

            var result = from a in listEmp
                         select new
                         {
                             EmpID = a.EmpID,
                             EmpName = a.EmpName,
                             EmpDepName = a.DepartmentName
                         };
            return JavaScriptSerializerUtil.getJson(result);
        }

        /// <summary>
        /// 显示选择人员（专业） 资格人员
        /// </summary>
        /// <returns></returns>
        public ActionResult SpecEmployee()
        {
            ViewBag.empids = "";
            if (Request.Params["empids"] != null)
            {
                ViewBag.empids = Request.Params["empids"].ToString();
            }
            return View();
        }

        public string getSpecEmpList()
        {
            var emplist = new DBSql.Sys.BaseQualification().AllData.GroupBy(p => new { p.QualificationSpecID, p.EmpID, p.EmpName, p.DepOrder, p.EmpOrder }).ToList();
            var speclist = new DBSql.Sys.BaseData().GetDataSetByEngName("Special");

            List<SpecEmpClass> empArray = new List<SpecEmpClass>();
            if (Request.Params["empids"] != null)
            {
                empArray = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<List<SpecEmpClass>>(Request.Params["empids"].ToString());

                speclist = speclist.Where(p => (empArray.Where(x => x.SpecID == p.BaseID).Count() > 0)).ToList();
                emplist = emplist.Where(p => (empArray.Where(x => x.SpecID == p.Key.QualificationSpecID && x.EmpID == p.Key.EmpID).Count() > 0)).ToList();

            }
            var lsit = speclist.Select(p => new
            {
                id = "T" + p.BaseID.ToString(),
                SpecID = p.BaseID,
                SpecName = p.BaseName,
                text = p.BaseName,
                EmpID = 0,
                iconCls = "",
                state = emplist.Where(x => x.Key.QualificationSpecID == p.BaseID).Count() > 0 ? "closed" : "open",
                children = emplist.Where(x => x.Key.QualificationSpecID == p.BaseID).Select(x => new
                {
                    text = x.Key.EmpName,
                    id = "C" + x.Key.QualificationSpecID + "_" + x.Key.EmpID,
                    SpecID = x.Key.QualificationSpecID,
                    SpecName = p.BaseName,
                    EmpID = x.Key.EmpID,
                    iconCls = "fa fa-user",
                })
            });

            return JavaScriptSerializerUtil.getJson(lsit);
        }

        /// <summary>
        ///  提资专业资格列表信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ExchSpecEmployee()
        {
            ViewBag.empids = "";
            ViewBag.projID = "";
            if (Request.Params["empids"] != null)
            {
                ViewBag.empids = Request.Params["empids"].ToString();
            }
            if (Request.Params["projID"] != null)
            {
                ViewBag.projID = Request.Params["projID"];
            }
            return View();
        }

        /// <summary>
        /// 提资专业资格人员（列表信息）
        /// </summary>
        /// <returns></returns>
        public string getExchSpecEmpList()
        {
            var emplist = new DBSql.Sys.BaseQualification().AllData.GroupBy(p => new { p.QualificationSpecID, p.EmpID, p.EmpName, p.DepOrder, p.EmpOrder }).ToList();
            var speclist = new DBSql.Sys.BaseData().GetDataSetByEngName("Special");
            if (!string.IsNullOrEmpty(Request.Params["projID"]) && Request.Params["projID"] != "0")
            {
                int projID = TypeHelper.parseInt(Request.Params["projID"]);
                DBSql.Design.DesExch DbExch = new DBSql.Design.DesExch();
                DataTable designEmp = DbExch.GetDesignEmpListByProjId(projID);//获取设计人员
                var rows = designEmp.AsEnumerable().Select(p => new
                {
                    SpecID = p.Field<int>("SpecId"),
                    EmpID = p.Field<int>("EmpID")
                }).Distinct().ToList();//  当前项目信息下的设计人员（专业）
                List<int> DesignSpecList = rows.Select(p => p.SpecID).Distinct().ToList();
                emplist = new DBSql.Sys.BaseQualification().AllData.Where(p => rows.Where(q => q.EmpID == p.EmpID && q.SpecID == p.QualificationSpecID).Count() > 0).
                    GroupBy(p => new { p.QualificationSpecID, p.EmpID, p.EmpName, p.DepOrder, p.EmpOrder }).ToList();
                speclist = speclist.Where(p => DesignSpecList.Contains(p.BaseID));
            }
            List<SpecEmpClass> empArray = new List<SpecEmpClass>();
            if (Request.Params["empids"] != null)
            {
                empArray = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<List<SpecEmpClass>>(Request.Params["empids"].ToString());

                speclist = speclist.Where(p => (empArray.Where(x => x.SpecID == p.BaseID).Count() > 0)).ToList();
                emplist = emplist.Where(p => (empArray.Where(x => x.SpecID == p.Key.QualificationSpecID && x.EmpID == p.Key.EmpID).Count() > 0)).ToList();

            }
            var lsit = speclist.Select(p => new
            {
                id = "T" + p.BaseID.ToString(),
                SpecID = p.BaseID,
                SpecName = p.BaseName,
                text = p.BaseName,
                EmpID = 0,
                iconCls = "",
                state = emplist.Where(x => x.Key.QualificationSpecID == p.BaseID).Count() > 0 ? "closed" : "open",
                children = emplist.Where(x => x.Key.QualificationSpecID == p.BaseID).Select(x => new
                {
                    text = x.Key.EmpName,
                    id = "C" + x.Key.QualificationSpecID + "_" + x.Key.EmpID,
                    SpecID = x.Key.QualificationSpecID,
                    SpecName = p.BaseName,
                    EmpID = x.Key.EmpID,
                    iconCls = "fa fa-user",
                })
            });

            return JavaScriptSerializerUtil.getJson(lsit);
        }

        public class SpecEmpClass
        {
            public int SpecID { get; set; }
            public int EmpID { get; set; }
        }
        #endregion

        #region 加载子目录
        /// <summary>
        /// 加载子目录
        /// </summary>
        /// <param name="Dir"></param>
        /// <param name="arr"></param>
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
        #endregion

        #region 得到文件路径
        /// <summary>
        /// 得到文件路径
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public static string GetGoldFileUrl(HttpRequestBase Request)
        {

            return string.Format("http://{0}/GoldFile", Request.ServerVariables["HTTP_Host"]);
        }
        #endregion

        #region 获取上传目录
        /// <summary>
        /// 获取上传目录
        /// </summary>
        /// <returns></returns>
        public string GetUploadPath()
        {
            string path = ConfigurationManager.AppSettings["FtpLocalPath"];
            string uploadDir = path + "\\Emp" + userInfo.EmpID;
            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }
            return uploadDir;
        }
        #endregion

        #region 获取临时目录
        /// <summary>
        /// 获取临时目录
        /// </summary>
        /// <returns></returns>
        public string GetTempPath()
        {
            string path = ConfigurationManager.AppSettings["FtpLocalPath"];
            string uploadDir = path + "\\temp\\Emp" + userInfo.EmpID;
            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }
            return uploadDir;
        }
        #endregion

        #region 保存临时文件
        /// <summary>
        /// 保存临时文件
        /// </summary>
        /// <param name="uploadFile"></param>
        /// <param name="chunk"></param>
        /// <returns></returns>
        private string WriteTempFile(HttpPostedFileBase uploadFile, int chunk)
        {
            string tempDir = GetTempPath();
            if (!Directory.Exists(tempDir))
            {
                Directory.CreateDirectory(tempDir);
            }
            string fullName = string.Format("{0}\\{1}.part", tempDir, uploadFile.FileName);
            if (chunk == 0)
            {
                //如果是第一个分块，则直接保存
                uploadFile.SaveAs(fullName);
            }
            else
            {
                //如果是其他分块文件 ，则原来的分块文件，读取流，然后文件最后写入相应的字节
                FileStream fs = new FileStream(fullName, FileMode.Append);
                if (uploadFile.ContentLength > 0)
                {
                    int FileLen = uploadFile.ContentLength;
                    byte[] input = new byte[FileLen];

                    // Initialize the stream.
                    System.IO.Stream MyStream = uploadFile.InputStream;

                    // Read the file into the byte array.
                    MyStream.Read(input, 0, FileLen);

                    fs.Write(input, 0, FileLen);
                    fs.Close();
                }
            }


            return fullName;
        }




        #endregion

        #region Word PDF 导出

        [Description("导出word请求流")]
        //[HttpPost]
        [ValidateInput(false)]
        public void ToDoc(string docName, bool IsWord = false, string ExportName = "")
        {
            List<DataModel.KeyValue> MarkList = new List<DataModel.KeyValue>();
            string content = Request.Form["MarkList"];

            if (!string.IsNullOrEmpty(content))
                MarkList = JQ.Util.JavaScriptSerializerUtil.parseFormJson<List<DataModel.KeyValue>>(content);

            HttpResponse resp = System.Web.HttpContext.Current.Response;
            resp.Clear();
            resp.ClearHeaders();
            resp.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");

            string Url = System.Web.HttpContext.Current.Server.MapPath("~/") + @"Download\{0}.doc";
            Url = string.Format(Url, docName);
            if (ExportName == "设计会签单")
            {
                WordToHQD(MarkList,Url);
            }
            else
            {
                var model = MarkList.Find(p => p.Key == "IsMultiSelect");
                if (null != model)
                {
                    #region

                    if (!System.IO.File.Exists(Url))
                    {
                        resp.Flush();
                        resp.End();
                    }
                    if (string.IsNullOrEmpty(ExportName)) ExportName = DateTime.Now.ToString("yyyyMMddHHmm");

                    Aspose.Words.Document NewDoc = new Aspose.Words.Document(Url);
                    Aspose.Words.DocumentBuilder builder = new Aspose.Words.DocumentBuilder(NewDoc);
                    Aspose.Words.BookmarkCollection bookmarkList = NewDoc.Range.Bookmarks;

                    List<BookMark> list = new List<BookMark>();
                    foreach (Aspose.Words.Bookmark bookmark in bookmarkList)
                    {
                        string boolMarkName = bookmark.Name;
                        if (boolMarkName.EndsWith("_MultiSelect"))
                        {
                            string[] array = boolMarkName.Split('_'); //专业负责人_1_Note
                            var reg = new System.Text.RegularExpressions.Regex("^" + array[0] + "_[\\w\\W]*_Note$");
                            BookMark bMark = new BookMark();
                            bMark.Key = boolMarkName;
                            var markList = MarkList.FindAll(p => reg.IsMatch(p.Key));
                            bMark.Num = (null != markList ? markList.Count : 0);
                            list.Add(bMark);

                        }
                    }
                    foreach (BookMark b in list)
                    {
                        string[] array = b.Key.Split('_');
                        builder.MoveToBookmark(b.Key);
                        for (int i = 0; i < b.Num; i++)
                        {
                            string Note = array[0] + "_" + (i + 1) + "_Note";
                            string EmpName = array[0] + "_" + (i + 1) + "_EmpName";
                            string Date = array[0] + "_" + (i + 1) + "_Date";

                            builder.RowFormat.Height = 20;
                            builder.InsertCell(); builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.First; builder.CellFormat.Width = 100; builder.Write(array[0]);
                            builder.InsertCell(); builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.First; builder.CellFormat.Width = 100; builder.StartBookmark(Note); builder.EndBookmark(Note);
                            builder.InsertCell(); builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None; builder.CellFormat.Width = 100; builder.Write("签名");
                            builder.InsertCell(); builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None; builder.CellFormat.Width = 100; builder.StartBookmark(EmpName); builder.EndBookmark(EmpName);
                            builder.EndRow();

                            builder.RowFormat.Height = 20;
                            builder.InsertCell(); builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.Previous; builder.CellFormat.Width = 100;
                            builder.InsertCell(); builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.Previous; builder.CellFormat.Width = 100;
                            builder.InsertCell(); builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None; builder.CellFormat.Width = 100; builder.Write("日期");
                            builder.InsertCell(); builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None; builder.CellFormat.Width = 100; builder.StartBookmark(Date); builder.EndBookmark(Date);
                            builder.EndRow();


                            builder.EndTable();
                        }
                    }
                    #endregion
                    SetMark(NewDoc, MarkList);

                    if (IsWord)
                    {
                        NewDoc.Save(resp, ExportName + ".doc", Aspose.Words.ContentDisposition.Attachment, Aspose.Words.Saving.SaveOptions.CreateSaveOptions(Aspose.Words.SaveFormat.Doc));
                    }
                    else
                    {
                        NewDoc.Save(resp, ExportName + ".pdf", Aspose.Words.ContentDisposition.Attachment, Aspose.Words.Saving.SaveOptions.CreateSaveOptions(Aspose.Words.SaveFormat.Pdf));
                    }
                }
                else
                {
                    Url = string.Format(Url, docName);
                    if (!System.IO.File.Exists(Url))
                    {
                        resp.Flush();
                        resp.End();
                    }
                    if (string.IsNullOrEmpty(ExportName)) ExportName = DateTime.Now.ToString("yyyyMMddHHmm");

                    Aspose.Words.Document doc = new Aspose.Words.Document(Url);

                    SetMark(doc, MarkList);

                    if (IsWord)
                    {
                        doc.Save(resp, ExportName + ".doc", Aspose.Words.ContentDisposition.Attachment, Aspose.Words.Saving.SaveOptions.CreateSaveOptions(Aspose.Words.SaveFormat.Doc));
                    }
                    else
                    {
                        doc.Save(resp, ExportName + ".pdf", Aspose.Words.ContentDisposition.Attachment, Aspose.Words.Saving.SaveOptions.CreateSaveOptions(Aspose.Words.SaveFormat.Pdf));
                    }
                }
            }
        }

        private void WordToHQD(List<DataModel.KeyValue> markList, string Url)
        {
            var id = markList.Find(p => p.Key == "Id");

            int zId = Convert.ToInt32(id.Value);
            DBSql.Design.DesMutiSign op = new DBSql.Design.DesMutiSign();
            var model = op.Get(zId);

            Aspose.Words.Document doc = new Aspose.Words.Document(Url);
            Aspose.Words.DocumentBuilder builder = new Aspose.Words.DocumentBuilder(doc);

            foreach (DataModel.KeyValue m in markList)
            {
                string MarkKey = m.Key.ToString();
                var MarkDesigner = doc.Range.Bookmarks[MarkKey];
                if (MarkDesigner == null) continue;
                builder.MoveToBookmark(MarkKey);
                builder.Write(m.Value);
            }

                var list = op.DbContext.Set<DataModel.Models.DesMutiSignRec>().Where(p => p.MutiSignId == model.Id).Select(p => new
            {
                SpecID = p.RecSpecId,
                SpecName = p.RecSpecName,
                EmpName = p.RecEmpName,
                EmpID = p.RecEmpId,
                Note = p.RecContent,
                Time = p.RecDealDate
            }).ToList();

            NodeCollection allTables = doc.GetChildNodes(NodeType.Table, true);
            Aspose.Words.Tables.Table table_a = allTables[0] as Aspose.Words.Tables.Table;

            var rowCounts = list.Count > 4 ? list.Count : 4;
            for (int i = 1; i < rowCounts; i++)
            {
                var rows = table_a.Rows[3].Clone(true);
                //将复制的行插入当前行的下方
                table_a.Rows.Insert(table_a.Rows.Count - 1, rows);
            }

            int rowIndex = 2;
            for (int i = 0; i < rowCounts; i++)
            {
                rowIndex++;
                builder.MoveToCell(0, rowIndex, 0, 0);
                StringBuilder txt = new StringBuilder();
                if (i < list.Count)
                {
                    txt.Append(" 会签意见（注明会签文件名称）：\n");
                    txt.AppendFormat(list[i].Note);
                    txt.AppendFormat("\n\n\n\n\n");
                    txt.AppendFormat("                                            会签专业：" + list[i].SpecName + "\n");
                    txt.AppendFormat("                                            专业负责人/日期：");
                    builder.Write(txt.ToString());

                    var Permission = markList.Find(p => p.Key == "Permission");
                    string filepath = System.Web.HttpContext.Current.Server.MapPath("~/") + @"SignImages\{0}.png";
                    filepath = string.Format(filepath, list[i].EmpName);
                    if (System.IO.File.Exists(filepath))
                    {
                        if (Permission.Value == "1")
                        {
                            builder.InsertImage(filepath, 96, 37);
                        }
                        else
                        {
                            builder.Write(list[i].EmpName);
                        }
                    }
                    else
                    {
                        builder.Write(list[i].EmpName);
                    }
                }
                else
                {
                    txt.Append(" 会签意见（注明会签文件名称）：\n");
                    txt.AppendFormat("");
                    txt.AppendFormat("\n\n\n\n\n");
                    txt.AppendFormat("                                            会签专业：\n");
                    txt.AppendFormat("                                            专业负责人/日期：");
                    builder.Write(txt.ToString());
                }
            }

            Aspose.Words.Saving.DocSaveOptions docoptions = new Aspose.Words.Saving.DocSaveOptions(SaveFormat.Doc);
            doc.Save(System.Web.HttpContext.Current.Response, "测试.doc", Aspose.Words.ContentDisposition.Attachment, docoptions);
        }

        private void SetMarkTable(Aspose.Words.DocumentBuilder builder, System.Data.DataTable nameList)
        {
            if (nameList == null) return;
            List<double> widthList = new List<double>();
            for (int i = 0; i < nameList.Columns.Count; i++)
            {
                builder.MoveToCell(0, 0, i, 0); //移动单元格
                double width = builder.CellFormat.Width;//获取单元格宽度
                widthList.Add(width);
            }

            builder.MoveToBookmark("table");
            for (var i = 0; i < nameList.Rows.Count; i++)
            {
                for (var j = 0; j < nameList.Columns.Count; j++)
                {
                    builder.InsertCell();// 添加一个单元格
                    builder.CellFormat.Borders.LineStyle = Aspose.Words.LineStyle.Single;
                    builder.CellFormat.Borders.Color = System.Drawing.Color.Black;
                    builder.CellFormat.Width = widthList[j];
                    builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                    //合并单元格
                    builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;
                    builder.ParagraphFormat.Alignment = Aspose.Words.ParagraphAlignment.Center;
                    builder.Write(nameList.Rows[i][j].ToString());

                }
                builder.EndRow();
            }
            builder.EndTable();
        }

        /// <summary>
        /// table中插入 tr
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="xmlValue"></param>
        /// <param name="rowKeyName"></param>
        private void SetTrMark(Aspose.Words.Document doc, string xmlValue, string rowKeyName, Aspose.Words.Tables.Table _table = null)
        {
            Aspose.Words.DocumentBuilder doct = new Aspose.Words.DocumentBuilder(doc);
            XmlDocument xml = new XmlDocument();
            var rootXml = xml.CreateElement("root");
            rootXml.InnerXml = xmlValue;

            Aspose.Words.Tables.Table firstTable = null;
            if (_table == null)
            {
                firstTable = (Aspose.Words.Tables.Table)doc.GetChild(Aspose.Words.NodeType.Table, 0, true);
            }
            else
            {
                firstTable = _table;
            }

            foreach (Aspose.Words.Tables.Row row in firstTable.Rows)
            {
                var IsContain = row.Cells.ToArray().Where(p => p.GetText().Contains(rowKeyName));
                if (IsContain.Count() > 0)
                {
                    if (rootXml.ChildNodes.Count > 0)
                    {
                        Aspose.Words.Tables.Row CurrentRow = row;
                        foreach (XmlElement xmlEle in rootXml.ChildNodes)
                        {
                            int index = 0;
                            Aspose.Words.Tables.Row Newrow = new Aspose.Words.Tables.Row(doc);
                            foreach (XmlElement cEle in xmlEle.ChildNodes)
                            {
                                Aspose.Words.Tables.Cell _cell = new Aspose.Words.Tables.Cell(doc);
                                if (cEle.ChildNodes.Count > 1)
                                {
                                    foreach (XmlElement ccEle in cEle.ChildNodes)
                                    {
                                        Aspose.Words.Paragraph _par = new Aspose.Words.Paragraph(doc);
                                        if (ccEle.GetAttribute("img") != "")
                                        {
                                            _par.ParagraphFormat.Alignment = Aspose.Words.ParagraphAlignment.Left;
                                            _cell.AppendChild(_par);

                                            Aspose.Words.Drawing.Shape shapge = new Aspose.Words.Drawing.Shape(doc, Aspose.Words.Drawing.ShapeType.Image);
                                            string FilePath = "http://" + Request.Url.Host + ":" + Request.Url.Port + ccEle.GetAttribute("img");
                                            string FileByte = GeHttpFile(FilePath);
                                            if (FileByte != "")
                                            {
                                                shapge = new Aspose.Words.Drawing.Shape(doc, Aspose.Words.Drawing.ShapeType.Image);
                                                shapge.ImageData.SetImage(FileByte);
                                                shapge.Width = 104;
                                                shapge.Height = 80;
                                                shapge.WrapType = Aspose.Words.Drawing.WrapType.TopBottom;
                                                shapge.HorizontalAlignment = Aspose.Words.Drawing.HorizontalAlignment.Left;

                                                _par.AppendChild(shapge);
                                            }
                                            continue;
                                        }
                                        else if (ccEle.GetAttribute("style").Contains("text-align:left"))
                                        {
                                            _par.ParagraphFormat.Alignment = Aspose.Words.ParagraphAlignment.Left;
                                            _par.AppendChild(new Aspose.Words.Run(doc, ccEle.InnerText));
                                        }
                                        else if (ccEle.GetAttribute("style").Contains("text-align:right"))
                                        {
                                            _par.ParagraphFormat.Alignment = Aspose.Words.ParagraphAlignment.Right;
                                            _par.AppendChild(new Aspose.Words.Run(doc, ccEle.InnerText));
                                        }

                                        else
                                        {
                                            _par.ParagraphFormat.Alignment = Aspose.Words.ParagraphAlignment.Center;
                                            _par.AppendChild(new Aspose.Words.Run(doc, ccEle.InnerText));
                                        }
                                        _cell.AppendChild(_par);
                                    }
                                }
                                else
                                {
                                    //插入单元格内容
                                    _cell.AppendChild(new Aspose.Words.Paragraph(doc));
                                    _cell.FirstParagraph.ParagraphFormat.Alignment = Aspose.Words.ParagraphAlignment.Center;
                                    _cell.FirstParagraph.AppendChild(new Aspose.Words.Run(doc, cEle.InnerText));
                                }

                                _cell.CellFormat.Borders.LineStyle = (row.Cells[index]).CellFormat.Borders.LineStyle;
                                _cell.CellFormat.Borders.Color = (row.Cells[index]).CellFormat.Borders.Color;
                                _cell.CellFormat.Width = (row.Cells[index]).CellFormat.Width;
                                _cell.CellFormat.VerticalAlignment = (row.Cells[index]).CellFormat.VerticalAlignment;


                                if (index == 0)
                                {
                                    _cell.CellFormat.Borders.Left.LineWidth = (row.Cells[index]).CellFormat.Borders.Left.LineWidth;
                                }
                                else if (index == (xmlEle.ChildNodes.Count - 1))
                                {
                                    _cell.CellFormat.Borders.Right.LineWidth = (row.Cells[index]).CellFormat.Borders.Right.LineWidth;
                                }
                                Newrow.AppendChild(_cell);
                                index++;
                            }

                            firstTable.InsertAfter(Newrow, CurrentRow);
                            CurrentRow = Newrow;
                        }
                    }
                }

            }
        }

        private string[] gridContent = new string[] { "designemergency", "designprocess", "designdanger" };
        private void SetMark(Aspose.Words.Document doc, List<DataModel.KeyValue> MarkList)
        {
            if (MarkList == null) return;
            Aspose.Words.DocumentBuilder DocBuilder = new Aspose.Words.DocumentBuilder(doc);

            var Permission = MarkList.Find(p => p.Key == "Permission");

            foreach (DataModel.KeyValue m in MarkList)
            {
                string MarkKey = m.Key.ToString();
                var MarkDesigner = doc.Range.Bookmarks[MarkKey];
                if (MarkDesigner == null) continue;
                DocBuilder.MoveToBookmark(MarkKey);
                var item = m.Value;

                //如果签名标签
                if (m.Key.ToLower().EndsWith("_empname"))
                {
                    if (Permission != null && Permission.Value == "1")
                    {
                        string filepath = System.Web.HttpContext.Current.Server.MapPath("~/") + @"SignImages\{0}.png";
                        filepath = string.Format(filepath, item);
                        if (System.IO.File.Exists(filepath))
                        {
                            DocBuilder.InsertImage(filepath, 96, 37);
                            continue;
                        }
                    }
                }
                if (m.Key.ToLower().Contains("grid") || gridContent.Contains(m.Key.ToLower()))
                {
                    //DocBuilder.InsertBreak(Aspose.Words.BreakType.LineBreak);
                    DocBuilder.InsertHtml("<br />" + item);
                    continue;
                }

                if (m.Key.ToLower().Contains("checknumber"))
                {
                    //校审意见
                    SetTrMark(doc, item, "原则性");
                    continue;
                }
                if (m.Key.ToLower().Contains("checkxiao"))
                {
                    //校审意见
                    XmlDocument xml = new XmlDocument();
                    var rootXml = xml.CreateElement("root");
                    rootXml.InnerXml = item;
                    //排除‘设计文件列’
                    foreach (XmlElement xmlItem in rootXml.ChildNodes)
                    {
                        if (xmlItem.ChildNodes.Count > 0)
                        {
                            xmlItem.RemoveChild(xmlItem.ChildNodes[2]);
                        }
                    }

                    SetTrMark(doc, rootXml.InnerXml.ToString(), "执行情况");
                    continue;
                }
                if (m.Key.ToLower().Contains("plantableinfo_vollist"))
                {
                    var a = GetParentTable(DocBuilder.CurrentNode);
                    //项目计划表--导入卷册列表
                    SetTrMark(doc, item, "出手日期", a);
                    continue;
                }
                if (m.Key.ToLower().Contains("plantableinfo_exchlist"))
                {
                    //项目计划表--导入提资列表
                    var a = GetParentTable(DocBuilder.CurrentNode);
                    SetTrMark(doc, item, "提资专业", a);
                    continue;
                }
                if (m.Key.ToLower().Contains("plantableinfo_person"))
                {
                    //工程计划表_人员组织
                    DocBuilder.InsertHtml("<br />" + "<table cellspacing=\"0\" style=\"font-size:13px;border-collapse:collapse;width:97%;\">" + item + "</table>");
                    continue;
                }

                if (item.Contains("<br />") || item.Contains("<br/>"))
                {
                    var itemList = item.Replace("<br />", "|").Replace("<br />", "|").Trim('|').Split('|').ToList();
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        DocBuilder.Write(itemList[i].ToString());
                        if (i != (itemList.Count - 1))
                            DocBuilder.InsertBreak(Aspose.Words.BreakType.LineBreak);
                    }
                }
                else
                {
                    MarkDesigner.Text = item;
                }
            }
        }

        private void LineBreakStr()
        {

        }


        private Aspose.Words.Tables.Table GetParentTable(Aspose.Words.Node DocBuilder)
        {
            if (DocBuilder.GetType().ToString() == "Aspose.Words.Tables.Table")
            {
                return (Aspose.Words.Tables.Table)DocBuilder;
            }
            else
            {
                return GetParentTable(DocBuilder.ParentNode);
            }
        }

        private string GeHttpFile(string httpUrl)
        {
            string TargetPath = Path.Combine(JQ.Util.IOUtil.GetTempPath(), Guid.NewGuid().ToString());
            WebClient client = new WebClient();
            client.Headers.Add("JQDownload", "1");
            client.DownloadFile(httpUrl, TargetPath);

            if (System.IO.File.Exists(TargetPath))
            {
                return TargetPath;
            }

            return "";
            //stream.Seek(0, SeekOrigin.Begin);
            //FileStream FileS = new FileStream("", FileMode.Create);
            //FileS.Write(bytes, 0, bytes.Length);//写入文件
            //FileS.Close();

        }

    }
    #endregion
    public class UploadModel
    {
        public int AttachID { get; set; }
        public string UrL { get; set; }
        public string FileFullName { get; set; }
        public string FileName { get; set; }
        public DateTime FileDate { get; set; }
    }

    public class CSDialog
    {
        public string ljf { get; set; }
        public string zdn { get; set; }
        public string ysf { get; set; }
        public string jgz { get; set; }

        public int index { get; set; }
    }
    public class BookMark
    {
        public string Key { get; set; }
        public int Num { get; set; }
    }
}
