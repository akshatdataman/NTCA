using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Notifications
{
    /*Class NotificationsSender
     *Author: Indu
     *Purpose: Sending email notifications
     *
     */
    public class NotificationsSender
    {
        // shouldn't send same exception more than 1 time during 1 hour
        readonly TimeSpan sameNotificationTime = TimeSpan.FromHours(1);
        readonly TimeSpan sameApplicationNotificationTime = TimeSpan.FromMinutes(2);

        SendEmailService emailSender;

        public NotificationsSender()
        {
            this.emailSender = new SendEmailService();
        }

        private static Dictionary<int, DateTime> apoLog = new Dictionary<int, DateTime>();
        private static Dictionary<int, DateTime> ucLog = new Dictionary<int, DateTime>();
        static Dictionary<Type, DateTime> applicationLog = new Dictionary<Type, DateTime>();
        /// <summary>
        /// Send the notifications on application exception
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="methodCall"></param>
        public void SendApplicationExceptionNotifications(Exception ex, string methodCall)
        {
            if (applicationLog.ContainsKey(ex.GetType()))
            {
                var timeDiff = DateTime.Now - applicationLog[ex.GetType()];
                if (timeDiff > sameNotificationTime)
                    applicationLog.Remove(ex.GetType());
                else
                    return;
            }

            var emails = GetApplicationNotificationEmails();
            if (emails.Any())
            {
                string subject = "Application Exception";
                string message = string.Format("Method: {0} \n{1}", methodCall, ex.ToString());
                emailSender.SendEmails(emails, subject, message);
                applicationLog.Add(ex.GetType(), DateTime.Now);
            }
        }
        /// <summary>
        /// Send the notifications on application exception along with ipAddress
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="ipAddress"></param>
        /// <param name="request"></param>
        public void SendApplicationExceptionNotifications(Exception ex, string ipAddress, string request)
        {
            var emails = GetApplicationNotificationEmails();
            if (emails.Any())
            {
                string subject = "application Exception";
                string message = string.Format("application IP: {0} \n Server Time: {1} \n Raw Request: {2} \n {3} ", ipAddress, DateTime.Now, request, ex.ToString());
                emailSender.SendEmails(emails, subject, message);
                applicationLog.Add(ex.GetType(), DateTime.Now);
            }

        }
        /// <summary>
        /// Send Notification when any user registered into the application
        /// </summary>
        /// <param name="UserName"></param>
        public void SendUserRegisterNotifications(string UserName)
        {
            var emails = GetApplicationNotificationEmails();
            if (emails.Any())
            {
                string subject = "Notification For New User Registration";
                string message = string.Format("User Name: {0}", UserName);
                emailSender.SendEmails(emails, subject, message);
            }

        }
        /// <summary>
        /// Send the notification for APO/Application Status
        /// </summary>
        /// <param name="apoId"></param>
        /// <param name="apoTitle"></param>
        /// <param name="status"></param>
        public void NotifyApoStatus(int apoId, string apoTitle, string status)
        {
            var notify = true;

            if (apoLog.ContainsKey(apoId))
            {
                var lastSendDate = apoLog[apoId];
                var timeDiff = DateTime.Now - lastSendDate;

                if (timeDiff < sameNotificationTime)
                    notify = false;
            }

            if (notify)
            {
                NotifyApoInternal(apoId, apoTitle, status);
            }
        }
        /// <summary>
        /// Send the internal notifations of the application
        /// </summary>
        /// <param name="apoId"></param>
        /// <param name="ApoTitle"></param>
        /// <param name="status"></param>
        private void NotifyApoInternal(int apoId, string ApoTitle, string status)
        {
            var emails = GetApplicationNotificationEmails();

            emailSender.SendEmails(emails, string.Format("Apo '{0}' {1}", ApoTitle, status), "FYI");

            apoLog[apoId] = DateTime.Now;
        }
        /// <summary>
        /// Get the list of application user's email address to whome notification would be sent.
        /// </summary>
        /// <returns></returns>
        private List<string> GetApplicationNotificationEmails()
        {
            string emails = ConfigurationManager.AppSettings["ApplicationNotificationEmails"];
            if (string.IsNullOrEmpty(emails))
                return new List<string>();
            return emails.Split(',').Where(x => !string.IsNullOrEmpty(x)).ToList();
        }
        /// <summary>
        /// Notification based on certain action ferformed by the user
        /// </summary>
        /// <param name="name"></param>
        /// <param name="notifyType"></param>
        public void NotifyAction(string name, string notifyType)
        {
            emailSender.SendEmails(GetApplicationNotificationEmails(), string.Format("Action {0} {1}", name, notifyType), "FYI");
        }
    }
}
