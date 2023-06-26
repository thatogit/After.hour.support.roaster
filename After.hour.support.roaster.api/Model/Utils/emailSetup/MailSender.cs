using System.Net.Mail;

namespace After.hour.support.roaster.api.Model.Utils.emailSetup
{
    public class MailSender
    {
        public MailSender()
        {
            FromEmail = "CSTechActimizeTesters@investec.co.za";
            SMTPServer = "roaming.investec.co.za";
            ToEmail = "CSTechActimizeTesters@investec.co.za";

        }

        private string FromEmail { get; set; }

        private string ToEmail { get; set; }

        private string SMTPServer { get; set; }

        public void SendMail(string body, string emailHeader, string emailRecipient)
        {
            var client = new SmtpClient();
            MailMessage mail;
            mail = new MailMessage(FromEmail, emailRecipient);

            client.Host = SMTPServer;
            mail.IsBodyHtml = true;
            mail.Subject = emailHeader;
            mail.Body = body;

            client.Send(mail);
        }
    
    }
}
