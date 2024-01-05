using System.Collections.Generic;

public sealed class DeathEnum
{
    private List<ICountable> observers = new List<ICountable>();

    public void Add(ICountable countable) => observers.Add(countable);

    public void Clear() => observers.Clear();

    public void Notify()
    {
        foreach (var observer in observers)
            observer.DecreaseCount();
    }
}
