using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Tampines.Web
{
    /// <summary>
    /// Summary description for CaptchaHandler
    /// </summary>
    public class CaptchaHandler : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            MemoryStream memStream = new MemoryStream();
            string sessionName = string.Empty;
            string type = context.Request.QueryString["type"];
            switch (type)
            {
                case "feedback":
                    sessionName = "FB";
                    break;
                case "sms":
                    sessionName = "SMS";
                    break;
                default:
                    sessionName = "BR";
                    break;
            }

            string phrase = Convert.ToString(context.Session["Captcha_" + sessionName]);

            //Generate an image from the text stored in session  
            Bitmap CaptchaImg = new Bitmap(180, 60);
            Graphics Graphic = Graphics.FromImage(CaptchaImg);
            Graphic.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            //Set height and width of captcha image  
            Graphic.FillRectangle(new SolidBrush(Color.AntiqueWhite), 0, 0, 180, 60);
            Graphic.DrawString(phrase, new Font("Thaoma", 30, FontStyle.Italic), Brushes.Chocolate, 15, 15);
            CaptchaImg.Save(memStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] imgBytes = memStream.GetBuffer();

            Graphic.Dispose();
            CaptchaImg.Dispose();
            memStream.Close();

            var base64String = "data:image/png;base64," + Convert.ToBase64String(imgBytes, 0, imgBytes.Length);

            //write image  
            context.Response.ContentType = "image/jpeg";
            if (string.IsNullOrEmpty(context.Request.QueryString["method"]))
            {
                context.Response.BinaryWrite(imgBytes);
            }
            else
            {
                context.Response.Write(base64String);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}