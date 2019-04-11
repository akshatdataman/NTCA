using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using WiseThink.NTCA.BAL.Authorization;
using WiseThink.NTCA.BAL;
using System.Data;
using WiseThink.NTCA.DataEntity.Entities;
using System.Configuration;

namespace WiseThink.Scheduler
{
    public class ScheduledTasks : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            string alertMessage = string.Empty;
            alertMessage = ConfigurationManager.AppSettings["ApoSubmissionAlertMessage"];
            SendAlerts(alertMessage);
        }
        private void SendAlerts(string alertMessage)
        {
            try
            {
                int n = 0;
                string EmailAddress = string.Empty;
                string userName = string.Empty;
                Alert alert = new Alert();
                DataSet dsEmail = UserBAL.Instance.GetAllNTCAUsersEmail();
                List<string> Emailid = new List<string>();
                for (int i = 0; i < dsEmail.Tables[1].Rows.Count; i++)
                {
                    if (dsEmail != null)
                    {
                        DataRow dr = dsEmail.Tables[1].Rows[0];
                        if (dr[0] != DBNull.Value)
                        {
                            alert.UserId = Convert.ToInt32(dsEmail.Tables[1].Rows[i]["UserId"]);
                            alert.SentTo = Convert.ToString(dsEmail.Tables[1].Rows[i]["UserName"]);
                            EmailAddress = Convert.ToString(dsEmail.Tables[1].Rows[i]["Email"]);
                        }
                    }
                    Emailid.Add(EmailAddress);
                    alert.Subject = ConfigurationManager.AppSettings["AlertSubject"];
                    alert.Body = alertMessage;
                   
                    alert.LoggedInUser = ConfigurationManager.AppSettings["AlertSentBy"];
                    n = AlertBAL.Instance.SendAlerts(alert);

                    if (n != 0)
                    {
                        string subject = alert.Subject;
                        string body = alert.Body;
                        SendEmailService sendEmail = new SendEmailService();
                        sendEmail.SendEmails(Emailid, subject, body);
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
}
