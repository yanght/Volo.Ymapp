using System;
using System.Security.Cryptography;
using System.Text;

namespace Volo.Ymapp.Task
{
    public class MD5Help
    {
        /// <summary>
        /// 获取32位md5加密
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string Get32MD5(string source)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(source)));
            t2 = t2.Replace("-", "");
            return t2;
        }

        /// <summary>
        　　/// 获取16位md5加密
        　　/// </summary>
        　　/// <param name="source">待解密的字符串</param>
        　　/// <returns></returns>
        public static string Get16MD5(string source)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(source)), 4, 8);
            t2 = t2.Replace("-", "");
            t2 = t2.ToLower();
            return t2;
        }
    }
}
