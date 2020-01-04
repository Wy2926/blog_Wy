using System;
using System.Collections.Generic;
using System.Text;
using System.DrawingCore;
using System.IO;
using System.DrawingCore.Imaging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CC.Helper.Expand
{
    /// <summary>
    /// String拓展方法
    /// </summary>
    public static class StringExpand
    {
        public static string ToAsciiTurnUtf8(this string str)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            string utf = Encoding.Default.GetString(buffer);
            return utf;
        }

        /// <summary>
        /// 根据字符串生成验证码图片
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static byte[] CreateCheckCodeImage(this string code)
        {
            int codeW = 80;
            int codeH = 30;
            int fontSize = 16;
            Random rnd = new Random();
            //颜色列表，用于验证码、噪线、噪点 
            System.DrawingCore.Color[] color = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.Brown, Color.DarkBlue };
            //字体列表，用于验证码 
            string[] font = { "Times New Roman" };

            //创建画布
            Bitmap bmp = new Bitmap(codeW, codeH);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            //画噪线 
            for (int i = 0; i < 1; i++)
            {
                int x1 = rnd.Next(codeW);
                int y1 = rnd.Next(codeH);
                int x2 = rnd.Next(codeW);
                int y2 = rnd.Next(codeH);
                Color clr = color[rnd.Next(color.Length)];
                g.DrawLine(new Pen(clr), x1, y1, x2, y2);
            }
            //画验证码字符串 
            for (int i = 0; i < code.Length; i++)
            {
                string fnt = font[rnd.Next(font.Length)];
                Font ft = new Font(fnt, fontSize);
                Color clr = color[rnd.Next(color.Length)];
                g.DrawString(code[i].ToString(), ft, new SolidBrush(clr), (float)i * 18, (float)0);
            }
            //将验证码图片写入内存流，并将其以 "image/Png" 格式输出 
            MemoryStream ms = new MemoryStream();
            try
            {
                bmp.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                g.Dispose();
                bmp.Dispose();
            }
        }

        /// <summary>
        /// 获取html中纯文本（无法排除JS代码）
        /// </summary>
        /// <param name="html">html</param>
        /// <returns>纯文本</returns>
        public static string GetHtmlText(this string html)
        {
            html = System.Text.RegularExpressions.Regex.Replace(html, @"<\/*[^<>]*>", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = html.Replace("\r\n", "").Replace("\r", "").Replace("&nbsp;", "").Replace(" ", "");
            return html;
        }

        /// <summary>
        /// 将对象以JSON格式保存
        /// </summary>
        /// <param name="path"></param>
        /// <param name="obj"></param>
        public static void SaveJson(this string path, object obj)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(obj));
        }

        /// <summary>
        /// 将对象以JSON格式保存
        /// </summary>
        /// <param name="path"></param>
        /// <param name="obj"></param>
        public static Task SaveJsonAsync(this string path, object obj)
        {
            return File.WriteAllTextAsync(path, JsonConvert.SerializeObject(obj));
        }

        /// <summary>
        /// 读取路径文件转换为实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static async Task<T> GetJsonEntityAsync<T>(this string path)
        {
            string json = await File.ReadAllTextAsync(path);
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// 读取路径文件转换为实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T GetJsonEntity<T>(this string path)
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
