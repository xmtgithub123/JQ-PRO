using JQ.Web;
using JQ.Util;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DataModel.Models;
using DataModel;
using System.Data;
using System;
using System.Linq;
using System.IO;
using System.Web;

namespace Core.Controllers
{
    [Description("公用部分")]
    public class mainController : Controller
    {
        private readonly DBSql.Sys.BaseEmployee EmpLogin = new DBSql.Sys.BaseEmployee();
        private readonly DBSql.Sys.BaseConfig opCofig = new DBSql.Sys.BaseConfig();
        private readonly DBSql.Sys.BaseLog oplog = new DBSql.Sys.BaseLog();

        [Description("后台登录")]
        public ActionResult userlogin()
        {
            UserBaseInfo model = new UserBaseInfo();
            ViewBag.IsValidateCode = Convert.ToBoolean(opCofig.GetBaseConfigInfo("LoginValidateCode").ConfigValue);
            return View(model);
        }

        [Description("提交登录")]
        [HttpPost]
        public ActionResult checkuserlogin(UserBaseInfo entity, string checkCode)
        {
            DoResult dr = entity.isValid<UserBaseInfo>();
            dr.url = Url.Content("~");
            if (dr.stateType == DoResultType.validFail)
            {
                return Json(dr);
            }

            #region 密码传输完整性
            List<ValidationResult> vrs = new List<ValidationResult>();
            // 用 AES 解密传递过的密码信息 (md5密文^明文)
            string KV = "1234567812345678";
            string md5Pass = AESHelper.Decrypt(entity.HFPassword, KV, KV);
            string[] StrArr = md5Pass.Split('^');
            string md5Str = StrArr[0];//md5密文
            string PassStr = StrArr[1];//明文

            // 密码传输完整性测试：将明文进行md5加密与传过来的md5Str比较，如果不一致为完整性破坏
            string Pwd = EncryInfo.EncrypPassword(PassStr);

            if (md5Str != Pwd)
            {
                vrs.Add(DataAnnotationHelper.validResult("数据传送过程中的完整性被破坏!", "passWord"));
                dr = DoResultHelper.doIsValid(vrs);
                return Json(dr);
            }
            #endregion

            #region 登录验证码
            var LoginValidateCode = Convert.ToBoolean(opCofig.GetBaseConfigInfo("LoginValidateCode").ConfigValue);
            if (LoginValidateCode)
            {
                if (!SessionUtil.isSession("UserValidate") || !StringUtil.compareIgnoreCase(checkCode, Session["UserValidate"].ToString()))
                {
                    List<ValidationResult> vrs1 = new List<ValidationResult>();
                    vrs1.Add(DataAnnotationHelper.validResult("验证码输入不正确", "checkCode"));
                    return Json(DoResultHelper.doIsValid(vrs1));
                }
            }
            #endregion

            #region 登录账户锁定
            var LoginLockTime = Convert.ToInt32(opCofig.GetBaseConfigInfo("LoginLockTime").ConfigValue);
            var LoginMaxCount = Convert.ToInt32(opCofig.GetBaseConfigInfo("LoginMaxCount").ConfigValue);
            var LastLockTime = oplog.GetLastLockTime(entity.userName);

            if (LastLockTime != null && LastLockTime.Value.AddMinutes(LoginLockTime) > DateTime.Now)
            {
                vrs.Add(DataAnnotationHelper.validResult(string.Format("你的账户被锁定{0}分钟，锁定时间：{1}!", LoginLockTime, LastLockTime.Value.ToString("yyyy-MM-dd HH:mm")), "passWord"));
                dr = DoResultHelper.doIsValid(vrs);
                return Json(dr);
            }
            #endregion

            bool IsValidate = EmpLogin.CheckEmpPassword(entity.userName.Trim(), Pwd);
            if (IsValidate)
            {
                DataModel.Models.BaseEmployee emp = EmpLogin.GetBaseEmployeeInfo(entity.userName.Trim());

                #region 判断绑定IP-Mark地址
                bool IPMarkOn = new DBSql.Sys.BaseConfig().GetBaseConfigInfo("IPMarkOn").ConfigValue == "True";
                bool EmpIsBind = emp.EmpIsBind;
                if (IPMarkOn && EmpIsBind)
                {
                    string IsClient = Request.Form["IsClient"];
                    if (IsClient == "0")
                    {
                        vrs.Add(DataAnnotationHelper.validResult("你的绑定IP-Mark地址，不能使用网页登陆，请在个人电脑上使用客户端登录本系统!", "passWord"));
                        dr = DoResultHelper.doIsValid(vrs);
                        return Json(dr);
                    }
                    if (string.IsNullOrEmpty(entity.markIP) || !entity.markIP.Contains(emp.EmpMacAddress.ToLower()) || !entity.markIP.Contains(emp.EmpIPAddress))
                    {
                        vrs.Add(DataAnnotationHelper.validResult("你的绑定IP-Mark地址和您本机的IP-Mark地址不符，请在个人电脑上登录本系统!", "passWord"));
                        dr = DoResultHelper.doIsValid(vrs);
                        return Json(dr);
                    }
                }
                #endregion

                #region 判断是否必须修改密码
                bool PasswordChangeOn = new DBSql.Sys.BaseConfig().GetBaseConfigInfo("PasswordChangeOn").ConfigValue == "True";
                string DefaultPwd = EncryInfo.EncrypPassword("pass");
                if (Pwd == DefaultPwd && PasswordChangeOn)
                {
                    //vrs.Add(DataAnnotationHelper.validResult("登录失败,必须修改默认密码!", "passWord"));
                    //dr = DoResultHelper.doIsValid(vrs);

                    dr.url = Url.Content("~/Core/Layout/changepwd");
                }
                #endregion
                //登录成功,将用户信息保存到Session中
                this.SaveUserInfo(emp);
            }
            else
            {
                //写入登录错误日志;
                InsertErrLog(entity);

                //多次错误登录，锁定用户在固定时间内不可登录
                oplog.LogLock(entity.userName, GetIP(), LastLockTime, LoginLockTime, LoginMaxCount);


                vrs.Add(DataAnnotationHelper.validResult("帐号名或密码不正确！！！", "passWord"));
                dr = DoResultHelper.doIsValid(vrs);
            }
            return Json(dr);
        }

        public FileResult GetUserAvatar()
        {
            var empID = Request.QueryString["id"];
            if (string.IsNullOrEmpty(empID))
            {
                return null;
            }
            var path = Path.Combine(JQ.Util.ConfigUtil.GetConfigValue("UpFileRoot"), "EmpAvatar", empID + ".jpg");
            if (!System.IO.File.Exists(path))
            {
                return File(Server.MapPath("~/Content/workcenter/images/avatar.jpg"), "image/jpeg");
            }
            return File(path, "image/jpeg");
        }




        private string GetRealPassword(string originalPassword)
        {
            string KV = "1234567812345678";
            string md5Pass = AESHelper.Decrypt(originalPassword, KV, KV);
            string[] StrArr = md5Pass.Split('^');
            string md5Str = StrArr[0];//md5密文
            string PassStr = StrArr[1];//明文
            string Pwd = EncryInfo.EncrypPassword(PassStr);
            if (md5Str != Pwd)
            {
                throw new Common.JQException("数据传送过程中的完整性被破坏!");
            }
            return Pwd;
        }

        public JsonResult checkuserlogin1(UserBaseInfo entity)
        {
            try
            {
                //var realPassword = GetRealPassword(entity.HFPassword);
                #region 登录账户锁定
                var LoginLockTime = Convert.ToInt32(opCofig.GetBaseConfigInfo("LoginLockTime").ConfigValue);
                var LoginMaxCount = Convert.ToInt32(opCofig.GetBaseConfigInfo("LoginMaxCount").ConfigValue);
                var LastLockTime = oplog.GetLastLockTime(entity.userName);
                if (LastLockTime != null && LastLockTime.Value.AddMinutes(LoginLockTime) > DateTime.Now)
                {
                    throw new Common.JQException(string.Format("你的账户被锁定{0}分钟，锁定时间：{1}!", LoginLockTime, LastLockTime.Value.ToString("yyyy-MM-dd HH:mm")));
                }
                #endregion

                bool IsValidate = EmpLogin.CheckEmpPassword(entity.userName.Trim(), EncryInfo.EncrypPassword(entity.passWord));
                if (IsValidate)
                {
                    var emp = EmpLogin.GetBaseEmployeeInfo(entity.userName.Trim());
                    //登录成功,将用户信息保存到Session中
                    this.SaveUserInfo(emp);
                    return Json(new
                    {
                        Result = true,
                        SessionID = Session.SessionID,
                        UserInfo = new { EmpID = emp.EmpID, EmpName = emp.EmpName, EmpDepID = emp.EmpDepID, EmpDepName = emp.EmpDepName },
                        BaseEmployee = new DBSql.Sys.BaseEmployee().GetAllEmpList().Select(m => new { m.EmpID, m.EmpName, m.EmpOrder, m.EmpDepID, m.EmpDepName }),
                        BaseData = new DBSql.Sys.BaseData().AllData.Where(p=>p.BaseIsDeleted==false).Select(m => new { m.BaseID, m.BaseName, m.BaseOrder, m.BaseNameEng }),
                        BaseDataSystem = new DBSql.Sys.BaseDataSystem().AllData.Select(m => new { m.BaseID, m.BaseName, m.BaseOrder, m.BaseNameEng }).OrderBy(m => m.BaseOrder)
                    });
                }
                else
                {
                    //写入登录错误日志;
                    InsertErrLog(entity);
                    //多次错误登录，锁定用户在固定时间内不可登录
                    oplog.LogLock(entity.userName, GetIP(), LastLockTime, LoginLockTime, LoginMaxCount);
                    throw new Common.JQException("帐号名或密码不正确！！！");
                }
            }
            catch (Common.JQException jqe)
            {
                return Json(new { Result = false, Message = jqe.Message });
            }
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

        private void InsertErrLog(UserBaseInfo emp)
        {
            DataModel.Models.BaseLog log = new DataModel.Models.BaseLog();
            log.BaseLogEmpID = 0;
            log.EmpName = emp.userName;
            log.BaseLogTypeID = 1;
            log.BaseLogDate = DateTime.Now;
            log.BaseLogIP = GetIP();
            log.BaseLogRefTable = emp.HFPassword;
            log.BaseLogRefID = 0;
            log.BaseLogRefHTML = "登录失败！";

            oplog.Add(log);
            oplog.UnitOfWork.SaveChanges();
        }

        [Description("退出登录")]
        [HttpPost]
        public void LogOut()
        {
            Session.Remove("Employee");
            CookieUtil.clear("UID");
            CookieUtil.clear("AgentID");
            Session.Abandon();
        }

        [Description("获取验证码")]
        public ActionResult validatecode()
        {
            return File(ValidateImage.validatebyte("UserValidate"), @"image/PNG");
        }


        [HttpPost]
        public void AutoEntityFramework()
        {
            EmpLogin.AutoEntityFramework();
        }

        [Description("客户端下载")]
        public ActionResult SysDown()
        {
            return View();
        }

        [Description("关于")]
        public ActionResult SysAbout()
        {
            return View();
        }

        [Description("帮助")]
        public ActionResult SysHelp()
        {
            return View();
        }

        #region 登录后，保存用户信息到Session中;写入登录日志；判断代理信息。

        public void SaveUserInfo(DataModel.Models.BaseEmployee emp)
        {
            if (Session["Employee"] != null) Session.Remove("Employee");
            if (emp == null) return;
            new DBSql.Sys.BaseEmployee().SaveSessionInfo(Session, emp);
            CookieUtil.saveCookie("UID", emp.EmpID.ToString(), 24);
            CookieUtil.clear("AgentID");
            InsertLog(emp);
        }

        private void InsertLog(BaseEmployee emp)
        {
            DataModel.Models.BaseLog log = new DataModel.Models.BaseLog();
            log.BaseLogEmpID = emp.EmpID;
            log.EmpName = emp.EmpName;
            log.BaseLogTypeID = 0;
            log.BaseLogDate = DateTime.Now;
            log.BaseLogIP = GetIP();
            log.BaseLogRefTable = "login";
            log.BaseLogRefID = emp.EmpID;
            log.BaseLogRefHTML = "登录成功！";

            oplog.Add(log);
            oplog.UnitOfWork.SaveChanges();
        }

        /// <summary>
        /// 获取IP
        /// </summary>
        /// <returns></returns>
        private string GetIP()
        {
            string ip = string.Empty;
            if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"]))
                ip = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]);
            if (string.IsNullOrEmpty(ip))
                ip = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            return ip == "::1" ? "127.0.0.1" : ip;
        }
        #endregion

        #region 获取头像
        public FileResult GetHeadUrl(string gid)
        {
            if (string.IsNullOrEmpty(gid))
            {
                return null;
            }
            var model = EmpLogin.GetListFromCache(p => p.EmpHead == gid).FirstOrDefault();
            if (model == null)
            {
                return File(Server.MapPath("~/Content/workcenter/images/avatar.jpg"), "image/jpeg");
            }
            var path = Path.Combine(JQ.Util.ConfigUtil.GetConfigValue("UpFileRoot"), "EmpAvatar", model.EmpID + ".jpg");
            if (!System.IO.File.Exists(path))
            {
                return File(Server.MapPath("~/Content/workcenter/images/avatar.jpg"), "image/jpeg");
            }
            return File(path, "image/jpeg");

        }
        #endregion

        [Description("获取分组头像")]
        public FileResult GetGroupHeadUrl(int Id)
        {
            //if (Id == 0)
            //{
            //    return null;
            //}
            var model = EmpLogin.DbContext.Set<DataModel.Models.BaseAttach>().FirstOrDefault(p => p.AttachID == Id);
            if (model == null)
            {
                return File(Server.MapPath("~/Content/workcenter/images/avatar.jpg"), "image/jpeg");
            }
            var path = System.IO.Path.Combine(JQ.Util.ConfigUtil.GetConfigValue("UpFileRoot"), model.AttachDir);
            if (!System.IO.File.Exists(path))
            {
                return File(Server.MapPath("~/Content/workcenter/images/avatar.jpg"), "image/jpeg");
            }
            return File(path, "image/" + model.AttachExt.Trim('.') + "");

        }


    }
}
