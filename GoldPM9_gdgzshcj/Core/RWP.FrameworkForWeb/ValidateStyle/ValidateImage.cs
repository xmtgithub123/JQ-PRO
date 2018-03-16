/********************************************************************
*           CLR Version:     v4.0        
*           Created by 吴俊鹏 at 2010/5/1
*           175417739@qq.com                   
********************************************************************/

using System;
using System.Web;

namespace JQ.Web
{
    /// <summary>
    /// 生成图片
    /// </summary>
    public abstract class ValidateImage
    {
        /// <summary>
        /// 生成验证图片
        /// </summary>
        /// <param name="CodeName"></param>
        /// <returns></returns>
        public static byte[] validatebyte(string codeName)
        {
            string Code;
            ValidateCode bll = new ValidateCode();
            byte[] byteContent = bll.createImage(out Code);
            codeName = string.IsNullOrEmpty(codeName) ? DateTime.Now.ToString("yyyyMMddHHmmss") : codeName;
            HttpContext.Current.Session[codeName] = Code;
            return byteContent;
        }
    }
}
