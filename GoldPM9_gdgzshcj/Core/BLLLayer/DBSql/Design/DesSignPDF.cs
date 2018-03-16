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
    /// PDF签名的类
    /// </summary>
    public class DesSignPDF
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
        public DesSignPDF()
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

        /// <summary>
        /// 开始签名
        /// </summary>
        public void BeginSign()
        {
            if (SignFiles == null || SignFiles.Count == 0)
            {
                return;
            }
            Thread thread = new Thread(delegate ()
            {
                using (var dataAccessor = new DAL.JQPM9_DefaultContext())
                {
                    foreach (var signFile in SignFiles)
                    {
                        if (!File.Exists(signFile.LocalPath))
                        {
                            continue;
                        }
                        var strSign = "";
                        var strMultiSign = "";
                        try
                        {
                            PdfReader reader = new PdfReader(signFile.LocalPath, Encoding.Default.GetBytes("jinqu.cn.6688"));
                            if (!reader.Info.ContainsKey("Keywords"))
                            {
                                continue;
                            }
                            string[] signInfoStrs = reader.Info["Keywords"].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                            List<SignPosition> signPositions = new List<SignPosition>();
                            foreach (string signInfoStr in signInfoStrs)
                            {
                                SignPosition info = new SignPosition(signInfoStr);
                                signPositions.Add(info);
                            }
                            var outPath = Path.Combine(Path.GetDirectoryName(signFile.LocalPath), "Sign");
                            if (!Directory.Exists(outPath))
                            {
                                Directory.CreateDirectory(outPath);
                            }
                            string outPDF = Path.Combine(outPath, Path.GetFileName(signFile.LocalPath));
                            if (File.Exists(outPDF))
                            {
                                JQ.Util.IOUtil.TryDeleteFile(outPDF);
                            }
                            using (FileStream outFile = new FileStream(outPDF, FileMode.OpenOrCreate, FileAccess.Write))
                            {
                                PdfStamper stamper = PdfStamper.CreateSignature(reader, outFile, '\0');
                                stamper.SetEncryption(PdfWriter.STANDARD_ENCRYPTION_128, null, "jinqu.cn.6688", PdfWriter.ALLOW_SCREENREADERS | PdfWriter.AllowPrinting);
                                PdfContentByte content = stamper.GetOverContent(1);
                                // 通过 PDF 中的签名区域 来找有没有签名数据
                                foreach (SignPosition info in signPositions)
                                {
                                    // 处理 设校审批 签名
                                    if (signFile.ApproveSigns.ContainsKey(info.mRole))
                                    {
                                        // info.mRole = 生产流程节点名称（如： 设计、校核、审核、批准）
                                        if (info.mRole.StartsWith("HQ"))
                                        {
                                            // 直接写入文本
                                            AddContent(content, signFile.ApproveSigns[info.mRole], info);
                                        }
                                        else
                                        {
                                            AddImage(signFile.ApproveSigns[info.mRole], info, content);
                                        }
                                    }
                                    if (signFile.ApproveSigns.ContainsKey(info.mRole))
                                    {
                                        // info.mRole = 生产流程节点名称（如： 设计、校核、审核、批准）
                                        if (strSign != "")
                                        {
                                            strSign += " ";
                                        }
                                        strSign += info.mRole + ":" + signFile.ApproveSigns[info.mRole];
                                        AddImage(signFile.ApproveSigns[info.mRole], info, content);
                                    }
                                    // 处理 会签 签名
                                    if (signFile.MuiltiSigns.ContainsKey(info.mRole))
                                    {
                                        if (strMultiSign != "")
                                        {
                                            strMultiSign += " ";
                                        }
                                        strMultiSign += info.mRole.Replace("会签", "会签人").Replace("HQMajor", "会签专业").Replace(" HQDate", "会签日期") + ":" + signFile.MuiltiSigns[info.mRole];
                                        if (info.mRole.StartsWith("会签"))
                                        {
                                            AddImage(signFile.MuiltiSigns[info.mRole], info, content);
                                        }
                                        else
                                        {
                                            // 直接写入文本
                                            // 专业名称 ： HQMajor1 HQMajor2 HQMajor3
                                            // 会签时间 ： HQDate1  HQDate2  HQDate3
                                            AddContent(content, signFile.MuiltiSigns[info.mRole], info);
                                        }
                                    }
                                }
                                //20 * 72 /25.4
                                var barcode = new BarcodeQRCode(signFile.ID.ToString(), 60, 60, null);
                                var image = barcode.GetImage();
                                image.SetAbsolutePosition(9F, 10F);
                                content.AddImage(image);
                                using (var jinqupie = new FileStream(pdfPIEPath, FileMode.Open))
                                {
                                    Pkcs12Store store = new Pkcs12Store(jinqupie, "jinquFE151F7F".ToCharArray());
                                    String alias = "";
                                    ICollection<X509Certificate> chain = new List<X509Certificate>();
                                    foreach (string al in store.Aliases)
                                    {
                                        if (store.IsKeyEntry(al) && store.GetKey(al).Key.IsPrivate)
                                        {
                                            alias = al;
                                            break;
                                        }
                                    }
                                    AsymmetricKeyEntry pk = store.GetKey(alias);
                                    foreach (X509CertificateEntry c in store.GetCertificateChain(alias))
                                    {
                                        chain.Add(c.Certificate);
                                    }
                                    stamper.SignatureAppearance.CertificationLevel = PdfSignatureAppearance.CERTIFIED_NO_CHANGES_ALLOWED;
                                    IExternalSignature pks = new PrivateKeySignature(pk.Key, DigestAlgorithms.SHA256);
                                    MakeSignature.SignDetached(stamper.SignatureAppearance, pks, chain, null, null, null, 0, CryptoStandard.CMS);
                                }
                            }
                        }
                        catch
                        //catch (Exception ex)
                        {
                            //TODO:记录日志
                            //GoldPM.Common.Logger.Default.WriteLog(ex);
                        }
                        //插入签名日志
                        var html = TaskName;
                        html += String.Format(" > {0} (v{1}) ", signFile.Name, signFile.BaseAttachVer) + strSign + "" + signFile.LocalPath;
                        if (!string.IsNullOrEmpty(strMultiSign))
                        {
                            html += "\n" + strMultiSign;
                        }
                        var baseLog = new DataModel.Models.BaseLog()
                        {
                            BaseLogDate = DateTime.Now,
                            BaseLogEmpID = EmpID,
                            BaseLogIP = "",
                            BaseLogRefHTML = html,
                            BaseLogRefID = (int)TaskId,
                            BaseLogRefTable = "ArchSign",
                            BaseLogTypeID = 10,
                            EmpName = EmpName,
                            LogGrade = 0
                        };
                        dataAccessor.BaseLogs.Add(baseLog);
                    }
                    dataAccessor.SaveChanges();
                }
            });
            thread.Start();
        }

        private void AddImage(string name, SignPosition info, PdfContentByte content)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
            }
            name = Path.Combine(signImageDirectory, name + ".png");
            if (!File.Exists(name))
            {
                return;
            }
            Image image = Image.GetInstance(name);
            image.SetAbsolutePosition(info.mPosX + 1F, info.mPosY + 1F);
            float angle = 0;
            if (info.mWidth > info.mHeight)
            {
                //横的
                image.ScaleAbsolute(info.mWidth - 2F, info.mHeight - 2F);
            }
            else
            {
                //竖的
                angle = 90;
                image.ScaleAbsolute(info.mHeight - 2F, info.mWidth - 2F);
            }
            image.RotationDegrees = angle;
            content.AddImage(image);
        }


        private void AddContent(PdfContentByte content, string text, SignPosition signInfo)
        {
            PdfGState gs = new PdfGState();
            content.BeginText();
            content.SetColorFill(BaseColor.DARK_GRAY);
            content.SetFontAndSize(SignFont, signInfo.mHeight - 1);
            content.SetTextMatrix(0, 0);
            content.ShowTextAligned(Element.ALIGN_CENTER, text, signInfo.mPosX + signInfo.mWidth / 2, signInfo.mPosY, 0);
            content.EndText();
        }

        private class SignPosition
        {
            public string mRole;
            public float mPosX;
            public float mPosY;
            public float mHeight;
            public float mWidth;

            public SignPosition(string info)
            {
                string[] infos = info.Split(',');
                mRole = infos[0];
                mPosX = (float)(Convert.ToDouble(infos[1].Substring(0)) * 72 / 25.4);
                mPosY = (float)(Convert.ToDouble(infos[2].Substring(0, infos[2].Length - 1)) * 72 / 25.4);
                float rightX = (float)(Convert.ToDouble(infos[3].Substring(0)) * 72 / 25.4);
                float rightY = (float)(Convert.ToDouble(infos[4].Substring(0, infos[4].Length - 1)) * 72 / 25.4);
                mWidth = rightX - mPosX;
                mHeight = rightY - mPosY;
            }
        }
    }
   
}
