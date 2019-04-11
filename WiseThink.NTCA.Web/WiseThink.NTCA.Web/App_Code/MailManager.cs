using System;
using System.Collections;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Xml;
using System.Configuration;

namespace WiseThink.NTCA.App_Code
{
    public class MailManager
    {
        #region Property

        static string pv_ErrorMessage = "";
        public static string ErrorMessage
        {
            get { return pv_ErrorMessage; }
            set { pv_ErrorMessage = value; }
        }


        #endregion Property

        #region Member function

        #region SendMail

        public static bool SendMail(MailMessage oMailMessage)
        {
            try
            {
                SmtpClient oMailClient = new SmtpClient(ConfigurationManager.AppSettings["SmtpClientServer"].ToString());
                oMailClient.EnableSsl = true;
                oMailClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpClientHost"].ToString());
                oMailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                oMailClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["MailFrom"].ToString(), ConfigurationManager.AppSettings["MailFromPwd"].ToString());
                if (oMailMessage.From == null || string.IsNullOrEmpty(oMailMessage.From.Address))
                    oMailMessage.From = new MailAddress(ConfigurationManager.AppSettings["MailFrom"].ToString());
                oMailMessage.IsBodyHtml = true;
                oMailClient.Send(oMailMessage);
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }


        #endregion SendMail

        #endregion

    
    }
}