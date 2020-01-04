using System;
using System.Collections.Generic;
using System.Text;

namespace CC.Helper
{
    public static class StringRandom
    {

        /// <summary>
        /// 获取随机数字
        /// </summary>
        /// <param name="num">长度（默认4）</param>
        /// <returns></returns>
        public static string Num(int num = 4)
        {
            Random rnd = new Random();
            string chkCode = string.Empty;
            //验证码的字符集，去掉了一些容易混淆的字符 
            char[] character = { '2', '3', '4', '5', '6', '8', '9' };
            //生成验证码字符串 
            for (int i = 0; i < num; i++)
            {
                chkCode += character[rnd.Next(character.Length)];
            }
            return chkCode;
        }

        /// <summary>
        /// 获取随机字母
        /// </summary>
        /// <param name="num">长度（默认4）</param>
        /// <returns></returns>
        public static string Letter(int num = 4)
        {
            Random rnd = new Random();
            string chkCode = string.Empty;
            //验证码的字符集，去掉了一些容易混淆的字符 
            char[] character = {'a', 'b', 'd', 'e', 'f', 'h', 'k', 'm', 'n', 'r', 'x', 'y', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y' };
            //生成验证码字符串 
            for (int i = 0; i < num; i++)
            {
                chkCode += character[rnd.Next(character.Length)];
            }
            return chkCode;
        }

        /// <summary>
        /// 获取随机数字字母
        /// </summary>
        /// <param name="num">长度（默认4）</param>
        /// <returns></returns>
        public static string NumOrLetter(int num = 4)
        {
            Random rnd = new Random();
            string chkCode = string.Empty;
            //验证码的字符集，去掉了一些容易混淆的字符 
            char[] character = { '2', '3', '4', '5', '6', '8', '9', 'a', 'b', 'd', 'e', 'f', 'h', 'k', 'm', 'n', 'r', 'x', 'y', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y' };
            //生成验证码字符串 
            for (int i = 0; i < num; i++)
            {
                chkCode += character[rnd.Next(character.Length)];
            }
            return chkCode;
        }
    }
}
