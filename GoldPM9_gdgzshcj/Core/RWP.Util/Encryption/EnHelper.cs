/********************************************************************
*           CLR Version:     v4.0        
*           Created by 吴俊鹏 at 2010/5/1
*           175417739@qq.com                   
********************************************************************/

namespace JQ.Util
{
    /// <summary>
    /// 加密助手
    /// </summary>
    public static class EnHelper
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="enType">加密类型</param>
        /// <returns></returns>
        public static string encrypt(string value, EncryptionType enType)
        {
            switch (enType)
            {
                case EncryptionType.Des:
                    return DesUtil.encrypt(value);
                case EncryptionType.Rijndael:
                    return RijndaelUtil.encrypt(value);
                default:
                    return value;
            }
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="deType">解密类型</param>
        /// <returns></returns>
        public static string decrypt(string value, EncryptionType deType)
        {
            switch (deType)
            {
                case EncryptionType.Des:
                    return DesUtil.decrypt(value);
                case EncryptionType.Rijndael:
                    return RijndaelUtil.decrypt(value);
                default:
                    return value;
            }
        }
    }
}
