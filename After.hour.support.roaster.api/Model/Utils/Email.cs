namespace After.hour.support.roaster.api.Model.Utils
{
    public class Email : INotification
    {
        public string FirstReminder()
        {
            string messageBody = $"Good Day " + LineSupport.FirstLine + ", <br /><br />" +
                "Reminder that you will be on first line support on the " + LineSupport.SupportDate.ToString("dd/MMM/yy") + " and 2 day remains to start your support week <br/>" +
                "Your second line support is, " + LineSupport.SecondLine + "<br/>" +
                "From Team:" + SupportTeam.getTeamName + "<br/>" +
                "Team lead:" + SupportTeam.getTeamLead + "<br/><br/>" +

                "Kind Regards <br />" +
                ":)";
            return messageBody;
        }

        public string SecondReminder()
        {
            string messageBody = $"Good Day " + LineSupport.FirstLine + ", <br /><br />" +
               "Reminder that you will be on first line support on the " + LineSupport.SupportDate.ToString("dd/MMM/yy") + "<br/>" +
               "Your second line support is, " + LineSupport.SecondLine + "<br/>" +
               "From Team:" + SupportTeam.getTeamName + "<br/>" +
               "Team lead:" + SupportTeam.getTeamLead + "<br/><br/>" +

               "Kind Regards <br />" +
               ":)";

            return messageBody;
        }

        public string FinalReminder()
        {
            string messageBody = $"Good Day " + LineSupport.FirstLine + ", <br /><br />" +
                "Reminder that you will be on first line support today " + DateTime.Now.Date.ToString("dd/MMM/yy") + " until " + DateTime.Now.AddDays(7).ToString("dd/MMM/yy") + "<br/>" +
                "Your second line support is, " + LineSupport.SecondLine + "<br/>" +
                "From Team:" + SupportTeam.getTeamName + "<br/>" +
                "Team lead:" + SupportTeam.getTeamLead + "<br/><br/>" +

                "Kind Regards <br />" +
                ":)";

            return messageBody;
        }
    }
}
