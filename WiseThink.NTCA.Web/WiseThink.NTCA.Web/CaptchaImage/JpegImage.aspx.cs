using System;
using System.Drawing.Imaging;
using WiseThink.NTCA.BAL;
using WiseThink.NTCA.CaptchaImage;

namespace WiseThink.NTCA
{
    public partial class JpegImage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Create a CAPTCHA image using the text stored in the Session object.


            this.Session["CaptchaImageText"] = UserBAL.Instance.GenerateRandomCode();

            var ci = new WiseThink.NTCA.CaptchaImage.CaptchaImage(this.Session["CaptchaImageText"].ToString(), 150, 50, "Century Schoolbook");

            // Change the response headers to output a JPEG image.
            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";

            // Write the image to the response stream in JPEG format.
            ci.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);

            // Dispose of the CAPTCHA image object.
            ci.Dispose();

        }
    }
}
