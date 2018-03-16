/********************************************************************
*           CLR Version:     v4.0        
*           Created by 吴俊鹏 at 2010/5/1
*           175417739@qq.com                   
********************************************************************/

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace JQ.Util
{
    /// <summary>
    /// 图像助手
    /// </summary>
    public abstract class ImageUtil
    {
        /// <summary>
        /// 将图片转为Base64字符串
        /// </summary>
        /// <param name="imageFilePath">图片路径</param>
        /// <returns></returns>
        public static string imageToBase64String(string imageFilePath)
        {
            string base64String = string.Empty;

            if (IOUtil.fileExists(imageFilePath))
            {
                Image bmp = new Bitmap(imageFilePath);
                if (bmp != null)
                {
                    base64String = imageToBase64String(bmp, ImageFormat.Jpeg);
                }
                bmp.Dispose();
            }
            return base64String;
        }

        /// <summary>
        /// 将图片转为Base64字符串
        /// </summary>
        /// <param name="image">image对象</param>
        /// <param name="imageFormat">后缀</param>
        /// <returns></returns>
        public static string imageToBase64String(Image image, ImageFormat imageFormat)
        {
            string base64String = string.Empty;

            byte[] arr = imageToBytes(image, imageFormat);

            if (arr != null)
            {
                base64String = Convert.ToBase64String(arr);
            }

            return base64String;
        }

        /// <summary>
        /// 将图片Image转换成Byte[]
        /// </summary>
        /// <param name="image">image对象</param>
        /// <param name="imageFormat">后缀名</param>
        /// <returns></returns>
        public static byte[] imageToBytes(Image image, ImageFormat imageFormat)
        {
            if (image == null) { return null; }
            byte[] data = null;

            using (MemoryStream ms = new MemoryStream())
            {
                using (Bitmap Bitmap = new Bitmap(image))
                {
                    Bitmap.Save(ms, imageFormat);
                    ms.Position = 0;
                    data = new byte[ms.Length];
                    ms.Read(data, 0, Convert.ToInt32(ms.Length));
                    ms.Flush();
                }
            }
            return data;
        }

        /// <summary>
        /// 将base64字符串转变为图像
        /// </summary>
        /// <param name="base64Str">base64字符串</param>
        /// <returns></returns>
        public static Image base64StringToImage(string base64Str)
        {
            Image image = null;
            byte[] arr = base64StringToBytes(base64Str);
            if (arr != null)
            {
                image = byteArrayToImage(arr);
            }
            return image;

        }

        /// <summary>
        /// 将base64字符串保存为JPG格式图像
        /// </summary>
        /// <param name="base64Str">base64字符串</param>
        /// <param name="filePath">文件路径</param>
        public static void base64StringToSaveImageForJpg(string base64Str, string filePath)
        {
            base64StringToSaveImage(base64Str, filePath, ImageFormat.Jpeg);
        }
        /// <summary>
        /// 将base64字符串保存为文件
        /// </summary>
        /// <param name="base64Str">base64字符串</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="imageFormat">后缀</param>
        /// <returns></returns>
        public static void base64StringToSaveImage(string base64Str, string filePath, ImageFormat imageFormat)
        {

            byte[] arr = base64StringToBytes(base64Str);
            MemoryStream ms = new MemoryStream(arr);
            Bitmap bmp = new Bitmap(ms);
            bmp.Save(filePath, imageFormat);
            ms.Close();
        }

        /// <summary>
        /// 将base64字符串转换为byte[]
        /// </summary>
        /// <param name="base64Str">base64字符串</param>
        /// <returns></returns>
        private static byte[] base64StringToBytes(string base64Str)
        {
            byte[] arr = Convert.FromBase64String(base64Str);
            return arr;
        }

        /// <summary>
        /// byte[]转换成Image
        /// </summary>
        /// <param name="byteArrayIn">二进制图片流</param>
        /// <returns>Image</returns>
        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            if (byteArrayIn == null)
                return null;
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                Image returnImage = Image.FromStream(ms);
                ms.Flush();
                return returnImage;
            }
        }
    }
}
