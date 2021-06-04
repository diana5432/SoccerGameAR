public interface ISubject
{
    public void RegisterObserver(IObserver observer);

    public void Notify();    
}
