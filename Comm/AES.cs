using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Comm
{
    public static class AES
    {
        #region ========加密========
        private static string AesKey = Config.GetValue<string>("AesKey");
        private static Byte[] key;
        private static Byte[] IV = new Byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string UrlEncrypt(string strToEncrypt)
        {
            return UrlEncrypt(strToEncrypt, AesKey);
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="strToEncrypt">要加密的字符串</param>
        /// <param name="strEncryptKey">密钥</param>
        /// <returns>加密后的字符串</returns>
        public static string UrlEncrypt(string strToEncrypt, string strEncryptKey)
        {
            if (!string.IsNullOrEmpty(strToEncrypt))
            {
                try
                {
                    key = Encoding.UTF8.GetBytes(strEncryptKey.Substring(0, 8));
                    Byte[] inputByteArray = Encoding.UTF8.GetBytes(strToEncrypt);
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    return Convert.ToBase64String(ms.ToArray());
                }
                catch (Exception ex)
                {
                    //return ex.Message;
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region ========解密========
        public static string UrlDecrypt(string strToDecrypt)
        {
            return UrlDecrypt(strToDecrypt, AesKey);
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="strToDecrypt">要解密的字符串</param>
        /// <param name="strEncryptKey">密钥，必须与加密的密钥相同</param>
        /// <returns>解密后的字符串</returns>
        public static string UrlDecrypt(string strToDecrypt, string strEncryptKey)
        {
            if (!string.IsNullOrEmpty(strToDecrypt))
            {
                strToDecrypt = strToDecrypt.Replace(" ", "+");//如果去除此部分的代码就会出现上面出现所说的情况，出错或者解密出来的数据变成空值。
                try
                {
                    key = Encoding.UTF8.GetBytes(strEncryptKey.Substring(0, 8));
                    Byte[] inputByteArray = Convert.FromBase64String(strToDecrypt);
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
                catch (Exception ex)
                {
                    //return ex.Message;
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}