namespace After.hour.support.roaster.api.Model.Utils
{
    public class RoasterDetails : IRoasterObserver
    {
        public void Update(IRoasterSubject subject)
        {
            subject.ToString();
        }
    }
}
