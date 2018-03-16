/********************************************************************
*           CLR Version:     v4.0        
*           Created by 吴俊鹏 at 2010/5/1
*           175417739@qq.com                   
********************************************************************/

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;

namespace JQ.Web
{
    /// <summary>
    /// 验证码 线条干扰(蓝色)
    /// </summary>
    public class ValidateCode
    {
        //验证码美化
        private bool fontTextRenderingHint = false;
        //验证码长度(默认4个验证码的长度)
        private int validataCodeLength = 4;
        //验证码字体大小(默认15像素，可以自行修改)
        private int validataCodeSize = 16;
        //图片高度
        private int imageHeight = 22;
        //边框补(默认1像素)
        //private int padding = 1;
        //是否输出燥点(默认不输出)
        private bool chaos = true;
        //输出燥点的颜色
        private Color chaosColor = Color.FromArgb(0xaa, 0xaa, 0x33);
        //自定义背景色(默认白色)
        private Color backgroundColor = Color.White;
        //自定义颜色
        private Color drawColor = Color.FromArgb(0x32, 0x99, 0xcc);
        //自定义字体
        private string validateCodeFont = "Arial";

        public byte[] createImage(out string validataCode)
        {
            string strFormat = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
            getRandom(strFormat, this.validataCodeLength, out validataCode);
            Bitmap bitmap;
            MemoryStream stream = new MemoryStream();
            imageBmp(out bitmap, validataCode);
            bitmap.Save(stream, ImageFormat.Png);
            bitmap.Dispose();

            bitmap = null;
            stream.Close();
            stream.Dispose();

            return stream.GetBuffer();
        }

        public void imageBmp(out Bitmap bitMap, string validataCode)
        {
            int width = (int)(((this.validataCodeLength * this.validataCodeSize) * 1.3) + 4);
            bitMap = new Bitmap(width, this.imageHeight);
            disposeImageBmp(ref bitMap);
            createImageBmp(ref bitMap, validataCode);
        }

        //混淆验证码图片
        void disposeImageBmp(ref Bitmap bitmap)
        {
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);

            Pen pen = new Pen(this.drawColor, 1f);
            Random random = new Random();
            System.Drawing.Point[] pointArray = new System.Drawing.Point[2];

            Random rand = new Random();

            if (this.chaos)
            {
                pen = new Pen(chaosColor, 1);
                for (int i = 0; i < this.validataCodeLength * 2; i++)
                {
                    pointArray[0] = new System.Drawing.Point(rand.Next(bitmap.Width), rand.Next(bitmap.Height));
                    pointArray[1] = new System.Drawing.Point(rand.Next(bitmap.Width), rand.Next(bitmap.Height));
                    graphics.DrawLine(pen, pointArray[0], pointArray[1]);
                }
            }
            graphics.Dispose();
        }

        //绘制验证码图片
        void createImageBmp(ref Bitmap bitMap, string validateCode)
        {
            Graphics graphics = Graphics.FromImage(bitMap);
            if (this.fontTextRenderingHint)
                graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
            else
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            Font font = new Font(this.validateCodeFont, this.validataCodeSize, FontStyle.Regular);
            Brush brush = new SolidBrush(this.drawColor);
            int maxValue = Math.Max((this.imageHeight - this.validataCodeSize) - 5, 0);
            Random random = new Random();
            for (int i = 0; i < this.validataCodeLength; i++)
            {
                int[] numArray = new int[] { (i * this.validataCodeSize) + random.Next(1) + 3, random.Next(maxValue) - 4 };
                System.Drawing.Point point = new System.Drawing.Point(numArray[0], numArray[1]);
                graphics.DrawString(validateCode[i].ToString(), font, brush, (PointF)point);
            }
            graphics.Dispose();
        }

        //获取随机数
        private void getRandom(string formatString, int len, out string codeString)
        {
            int j1;
            codeString = string.Empty;
            string[] strResult = formatString.Split(new char[] { ',' });//把上面字符存入数组
            Random rnd = new Random();
            for (int i = 0; i < len; i++)
            {
                j1 = rnd.Next(100000) % strResult.Length;
                codeString = codeString + strResult[j1].ToString();
            }
        }
    }
}
