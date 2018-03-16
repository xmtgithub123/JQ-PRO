/********************************************************************
*           CLR Version:     v4.0        
*           Created by 吴俊鹏 at 2010/5/1
*           175417739@qq.com                   
********************************************************************/

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace JQ.Util
{
    /// <summary>
    /// Rijndael算法加密解密类
    /// </summary>
    public abstract class RijndaelUtil
    {
        /// <summary>
        /// 初始化密钥
        /// </summary>
        /// <returns>密钥</returns>
        private static byte[] getLegalKey()
        {
            string sTemp = "(RWPCore)WooXNGX";
            int KeyLength = 16;
            //将密钥调整为16位，多减少加
            if (sTemp.Length > KeyLength)
            {
                sTemp = sTemp.Substring(0, KeyLength);
            }
            else if (sTemp.Length < KeyLength)
            {
                sTemp = sTemp.PadRight(KeyLength, ' ');
            }
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }

        /// <summary>
        /// 初始化向量
        /// </summary>
        /// <returns>向量</returns>
        private static byte[] getLegalIV()
        {
            string sTemp = "EnShiDeWoHenNiuA";
            int IVLength = 16;
            //将向量调整为16位，多减少加
            if (sTemp.Length > IVLength)
            {
                sTemp = sTemp.Substring(0, IVLength);
            }
            else if (sTemp.Length < IVLength)
            {
                sTemp = sTemp.PadRight(IVLength, ' ');
            }
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }

        /// <summary>
        /// 加密Rijndael
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string encrypt(string encryptString)
        {
            try
            {
                //将要加密的字符换成UTF-8编码
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                //获取密钥
                byte[] Key = getLegalKey();
                //获取向量
                byte[] IV = getLegalIV();

                //实例化Rijndael算法
                RijndaelManaged rCSP = new RijndaelManaged();
                //创建储存到内存的流
                MemoryStream mStream = new MemoryStream();
                //加密
                CryptoStream cStream = new CryptoStream(mStream, rCSP.CreateEncryptor(Key, IV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }

        /// <summary>
        /// 解密密Rijndael
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string decrypt(string decryptString)
        {
            try
            {
                //获取密钥
                byte[] Key = getLegalKey();
                //获取向量
                byte[] IV = getLegalIV();
                //转换编码
                byte[] inputByteArray = Convert.FromBase64String(decryptString);


                //实例化Rijndael算法
                RijndaelManaged rCSP = new RijndaelManaged();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, rCSP.CreateDecryptor(Key, IV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }
    }
}
