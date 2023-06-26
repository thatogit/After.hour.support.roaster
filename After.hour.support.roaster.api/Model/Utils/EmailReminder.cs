namespace After.hour.support.roaster.api.Model.Utils
{
    public class EmailReminder : IReminder
    {

        private INotification _notification;
        public EmailReminder(INotification notification)
        {
            _notification = notification;
        }
        public string BuildReminder()
        {
            var dateTime = DateTime.Parse(DateTime.Now.ToString("dd/MMM/yy"));
            LineSupport.SupportDate = DateTime.Parse(LineSupport.SupportDate.ToString("dd/MMM/yy"));
            Console.WriteLine("Roaster notifying the observers.");
            string reminder = "";
            if (LineSupport.SupportDate >= dateTime)
            {
                var numberOfDayToSupport = LineSupport.SupportDate.Day - dateTime.Day;

                if (numberOfDayToSupport == 2)
                {
                    reminder = _notification.SecondReminder();

                }
                if (numberOfDayToSupport == 1)
                {
                    reminder = _notification.FirstReminder();
                }
                if (numberOfDayToSupport == 0)
                {
                    reminder = _notification.FinalReminder();
                }

            }
            return reminder;
        }
    }
}
