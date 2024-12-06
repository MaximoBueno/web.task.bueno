using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace web.task.bueno.Common.Session
{
    public class Captcha
    {
        private const string token = "0123456789aeiu";

        public byte[] ConvertBitmpaToArray(Bitmap bmp)
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    bmp.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    return memoryStream.ToArray();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.ToString());
                return null;
            }
        }

        public Bitmap CreateBitmapWithColor(int width, int height, ColorBitmapRgb rgb, string text)
        {
            try
            {
                Bitmap Bmp = new Bitmap(Math.Abs(width), Math.Abs(height));

                StringFormat format = new StringFormat()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                RectangleF rectf = new RectangleF(0, 0, Math.Abs(width), Math.Abs(height));

                using (Graphics gfx = Graphics.FromImage(Bmp))
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(rgb.red, rgb.green, rgb.blue)))
                {
                    gfx.FillRectangle(brush, 0, 0, Math.Abs(width), Math.Abs(height));
                    gfx.DrawString(text, new Font("Arial", 14), Brushes.White, rectf, format);
                }

                return Bmp;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.ToString());  
                return null;
            }
        }

        public string GetRandomCaptcha(int length = 3)
        {
            try
            {
                Random rnd = new Random();
                string captcha = string.Empty;
                int pivot;

                for (int i = 0; i <= length; i++)
                {
                    pivot = rnd.Next(0, token.Length - 1);
                    captcha += token[pivot];
                }

                return captcha;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.ToString());
                return string.Empty;
            }
        }
    }

    public class ColorBitmapRgb { 
        
        public int red { get; set; }
        public int green { get; set; }
        public int blue { get; set; }
    }

}