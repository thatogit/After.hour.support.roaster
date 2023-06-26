namespace After.hour.support.roaster.api.Model.Utils
{
    public interface IRoasterSubject
    {
        void Attach(IRoasterObserver roasterObserver);
        void Detach(IRoasterObserver roasterObserver);
        void Notify();
    }
}
