namespace After.hour.support.roaster.api.Model.Utils.emailSetup
{
    public class EmailsService
    {
        private readonly IReminder _reminder;
        private RoasterSubject _roaster;

        public EmailsService(IReminder reminder)
        {
            _reminder = reminder;
        }
        public void SendReport(string emailRecipients)
        {
            MailSender mailSender = new MailSender();
            string messageBody;
            messageBody = GetBodyContentForEmail();
            mailSender.SendMail(messageBody, "After hours support", emailRecipients);

        }
        private string GetBodyContentForEmail()
        {
            return _reminder.BuildReminder();
        }
    }
}
