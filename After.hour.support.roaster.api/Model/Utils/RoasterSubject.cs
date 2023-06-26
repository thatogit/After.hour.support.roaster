namespace After.hour.support.roaster.api.Model.Utils
{
    public class RoasterSubject : IRoasterSubject
    {
        private List<IRoasterObserver> _roasterObservers;
        private readonly INotification _notification;
        private readonly IReminder _reminder;

        public RoasterSubject(INotification notification, IReminder reminder)
        {
            _roasterObservers = new List<IRoasterObserver>();
            _notification = notification;
            _reminder = reminder;
        }
        public void Attach(IRoasterObserver roasterObserver)
        {
            _roasterObservers.Add(roasterObserver);
        }

        public void Detach(IRoasterObserver roasterObserver)
        {
            _roasterObservers.Remove(roasterObserver);
        }

        public void Notify()
        {
            int count = 0;
            int observersCount = _roasterObservers.Count;

            foreach (var roasterObserver in _roasterObservers)
            {
                if (observersCount <= count)
                {
                    roasterObserver.Update(this);
                }
                count++;
            }
        }

        public void BusinessLogic()
        {
            if (_reminder.BuildReminder().Length > 0)
            {
                Notify();
            }

        }


    }
}
