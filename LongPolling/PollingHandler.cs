namespace LongPolling;

public class PollingHandler
{
    private string _message;

    public bool Notified { get; private set; }

    public void Notify(string message)
    {
        Notified = true;
        _message = message;
    }

    public string Consume()
    {
        Notified = false;
        return _message;
    }
}