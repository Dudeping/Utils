using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeping.Utils
{
    public static partial class Util
    {
        public static (string, byte[]) VerifyCode(int length = 4)
        {
            int codeW = 80;
            int codeH = 30;
            int fontSize = 16;
            string chkCode = RandomEx.GenerateString(length);
            //颜色列表, 用于验证码、噪线、噪点 
            Color[] color = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.Brown, Color.DarkBlue };
            //字体列表, 用于验证码 
            string[] font = { "Times New Roman" };

            //创建画布
            Bitmap bmp = new Bitmap(codeW, codeH);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            //画噪线 
            for (int i = 0; i < 3; i++)
            {
                int x1 = RandomEx.GenerateInt(codeW);
                int y1 = RandomEx.GenerateInt(codeH);
                int x2 = RandomEx.GenerateInt(codeW);
                int y2 = RandomEx.GenerateInt(codeH);
                Color clr = color[RandomEx.GenerateInt(color.Length)];
                g.DrawLine(new Pen(clr), x1, y1, x2, y2);
            }
            //画验证码字符串 
            for (int i = 0; i < chkCode.Length; i++)
            {
                string fnt = font[RandomEx.GenerateInt(font.Length)];
                Font ft = new Font(fnt, fontSize);
                Color clr = color[RandomEx.GenerateInt(color.Length)];
                g.DrawString(chkCode[i].ToString(), ft, new SolidBrush(clr), (float)i * 18, 0);
            }
            //将验证码图片写入内存流, 并将其以 "image/Png" 格式输出 
            MemoryStream ms = new MemoryStream();
            try
            {
                bmp.Save(ms, ImageFormat.Png);
                return (chkCode, ms.ToArray());
            }
            catch (Exception)
            {
                return (chkCode, null);
            }
            finally
            {
                g.Dispose();
                bmp.Dispose();
            }
        }
    }
}
