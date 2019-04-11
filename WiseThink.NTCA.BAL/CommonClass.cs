//Created By Zahir

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Configuration;
using System.Data;
using WiseThink.NTCA.DataEntity;
using WiseThink.NTCA.DataEntity.Entities;
using WiseThink.NTCA.DAL;

namespace WiseThink.NTCA.BAL
{
    public class CommonClass
    {
        readonly Random random = new Random();

        public string GenerateRandomCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
        }

        public string RandomPassword(int PasswordLength)
        {
            string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzA%$@*&BCDEFGHJKLMNOPQRSTUVWXYZ";
            Random randNum = new Random();
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }

        public bool SendMail(string pwd, string email)
        {
            try
            {
                
                //string strMailForm = ConfigurationManager.AppSettings["MailFrom"].ToString();
                string strEmail = email;
                string strMailSubject = ConfigurationManager.AppSettings["MailSubjectForDefaultPwd"].ToString();
                MailMessage msgMail = new MailMessage();

                SmtpClient smptpClient = new SmtpClient(ConfigurationManager.AppSettings["SmtpClientServer"].ToString());
                smptpClient.EnableSsl = true;
                smptpClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpClientHost"].ToString());
                smptpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smptpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["MailFrom"].ToString(), ConfigurationManager.AppSettings["MailFromPwd"].ToString());
                if (msgMail.From == null || string.IsNullOrEmpty(msgMail.From.Address))
                    msgMail.From = new MailAddress(ConfigurationManager.AppSettings["MailFrom"].ToString());
                msgMail.Subject = ConfigurationManager.AppSettings["MailSubjectForDefaultPwd"].ToString();
                msgMail.Body = "Your Temporary Password : " + pwd;
                msgMail.To.Add(email);

                smptpClient.Send(msgMail);
                
                return true;
            }
            catch
            {
                return false;
            }
        }
        public string GetPreviousFinancialYear(string currentFinancialYear)
        {
            string previousFinancialYear = string.Empty;
            int a1 = 0; int b1 = 0;
            if (currentFinancialYear.Contains('-'))
            {
                string[] tokens = currentFinancialYear.Split('-');
                int a = Convert.ToInt32(tokens[0]);
                int b = Convert.ToInt32(tokens[1]);
                a1 = a - 1;
                b1 = b - 1;
            }
            previousFinancialYear = a1.ToString() + "-" + b1.ToString();

            return previousFinancialYear;
        }

        public DataSet GetRelevantAPODocument()
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spRelevantAPODocumentList, System.Data.CommandType.StoredProcedure);
        }

        public DataSet getCurrentYear()
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetCurrentYear, System.Data.CommandType.StoredProcedure);
        }

        public DataSet getGPSByITEMIDandTigerReserveID(int ItemID,int TigerReserveID)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetGPSDetailsByItemID, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name="@ItemID",Type=System.Data.DbType.Int32,value=ItemID},
                new CommanParameter{Name="@TigerReserveID",Type=System.Data.DbType.Int32,value=TigerReserveID},
             });
        }
    }
}
