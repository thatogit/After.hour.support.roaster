using After.hour.support.roaster.api.Model.Utils;
using System.Globalization;

namespace support.roaster.tests
{
    public class SupportRoasterTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void FirstReminder_emailSent_pass()
        {
            //Arrange
            INotification notification = new Email();
            LineSupport.FirstLine = "Thato";
            LineSupport.SupportDate = DateTime.ParseExact("24/May/23", "dd/MMM/yy", CultureInfo.InvariantCulture);
            LineSupport.SecondLine = "Theo";
            SupportTeam.getTeamName = "SAM";
            SupportTeam.getTeamLead = "Tanja";

            //Act
            string expectReminder = $"Good Day Thato, <br /><br />" +
                            "Reminder that you will be on first line support on the" + $" {LineSupport.SupportDate.ToString("dd/MMM/yy")} " + "and 2 day remains to start your support week <br/>" +
                            "Your second line support is, Theo<br/>" +
                            "From Team:SAM<br/>" +
                            "Team lead:Tanja<br/><br/>" +

                            "Kind Regards <br />" +
                            ":)";

            //Assert
            string actReminder = notification.FirstReminder();
            Assert.AreEqual(expectReminder, actReminder);


        }

        [Test]
        public void FirstReminder_reminderBody_returnMessage_True()
        {
            //Arrange
            INotification notification = new Email();
            LineSupport.FirstLine = "Thato";
            LineSupport.SupportDate = DateTime.ParseExact("24/May/23", "dd/MMM/yy", CultureInfo.InvariantCulture);
            LineSupport.SecondLine = "Theo";
            SupportTeam.getTeamName = "SAM";
            SupportTeam.getTeamLead = "Tanja";

            //Act
            string expectReminder = $"Good Day Thato, <br /><br />" +
                            "Reminder that you will be on first line support on the" + $" {LineSupport.SupportDate.ToString("dd/MMM/yy")} " + "and 2 day remains to start your support week <br/>" +
                            "Your second line support is, Theo<br/>" +
                            "From Team:SAM<br/>" +
                            "Team lead:Tanja<br/><br/>" +

                            "Kind Regards <br />" +
                            ":)";

            //Assert
            string actReminder = notification.FirstReminder();
            Assert.AreEqual(expectReminder, actReminder);


        }

        [Test]
        public void SecondReminder_reminderBody_returnMessage_True()
        {
            //Arrange
            INotification notification = new Email();
            LineSupport.FirstLine = "Thato";
            LineSupport.SupportDate = DateTime.ParseExact("24/May/23", "dd/MMM/yy", CultureInfo.InvariantCulture);
            LineSupport.SecondLine = "Theo";
            SupportTeam.getTeamName = "SAM";
            SupportTeam.getTeamLead = "Tanja";

            //Act
            string expectReminder = $"Good Day Thato, <br /><br />" +
                            "Reminder that you will be on first line support on the" + $" {LineSupport.SupportDate.ToString("dd/MMM/yy")}" + "<br/>" +
                            "Your second line support is, Theo<br/>" +
                            "From Team:SAM<br/>" +
                            "Team lead:Tanja<br/><br/>" +

                            "Kind Regards <br />" +
                            ":)";

            //Assert
            string actReminder = notification.SecondReminder();
            Assert.AreEqual(expectReminder, actReminder);


        }

        [Test]
        public void FinalReminder_reminderBody_returnMessage_True()
        {
            //Arrange
           INotification notification = new Email();
            LineSupport.FirstLine = "Thato";
            LineSupport.SupportDate = DateTime.Parse(DateTime.Now.Date.ToString("dd/MMM/yy"));
            var lastSupportDate = LineSupport.SupportDate;
            LineSupport.SecondLine = "Theo";
            SupportTeam.getTeamName = "SAM";
            SupportTeam.getTeamLead = "Tanja";

            //Act
            string expectReminder = $"Good Day Thato, <br /><br />" +
                            "Reminder that you will be on first line support today" + $" {LineSupport.SupportDate.ToString("dd/MMM/yy")} " + "until"+ $" {lastSupportDate.AddDays(7).ToString("dd/MMM/yy")}" + "<br/>" +
                            "Your second line support is, Theo<br/>" +
                            "From Team:SAM<br/>" +
                            "Team lead:Tanja<br/><br/>" +

                            "Kind Regards <br />" +
                            ":)";

            //Assert
            string actReminder = notification.FinalReminder();
            Assert.AreEqual(expectReminder, actReminder);


        }
    }
}