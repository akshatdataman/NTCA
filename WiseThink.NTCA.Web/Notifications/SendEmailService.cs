using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

/*Class SendEmailService
 *Author: Indu
 *Purpose: Sending email to single or multiple recipients
 *
 */
public class SendEmailService
{
    /// <summary>
    /// Method for sending notification one or more people at the same time via email
    /// </summary>
    /// <param name="to"></param>
    /// <param name="subject"></param>
    /// <param name="body"></param>
    /// <returns></returns>
    public bool SendEmails(IEnumerable<string> to, string subject, string body)
    {
        MailMessage mes = new MailMessage();

        var emails = to.Where(x => !string.IsNullOrEmpty(x)).Distinct().ToList();
        foreach (var email in emails)
        {
            mes.To.Add(new MailAddress(email));
        }

        mes.Subject = subject;
        mes.Body = body;

        SmtpClient client = new SmtpClient();
        client.EnableSsl = client.Port == 587 || client.Port == 465 || client.Port == 25;

        try
        {
            client.Send(mes);
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }
}