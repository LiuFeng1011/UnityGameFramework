using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Text;

/// <summary>
/// author:li zong bo
/// </summary>
namespace ToolBox
{
    public class MD5Sum
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="strToEncrypt"></param>
        /// <returns></returns>
        public static string GetMD5HashFromString(string p_str)
        {
            System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
            byte[] bytes = ue.GetBytes(p_str);

            // encrypt bytes
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashBytes = md5.ComputeHash(bytes);

            // Convert the encrypted bytes back to a string (base 16)
            string hashString = "";

            for (int i = 0; i < hashBytes.Length; i++)
            {
                hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
            }

            return hashString.PadLeft(32, '0');
        }

        //获取文件的md5值
        public static string GetMD5HashFromFile(string p_path)
        {
            try
            {
                if (!File.Exists(p_path))
                {
                    return "";
                }
                FileStream file = new FileStream(p_path, FileMode.Open);

                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }
    }
}