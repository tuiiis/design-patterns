namespace BehavioralPatterns.Memento;

// класс-хранитель состояния (memento)
public class Memento
{
    public string State { get; private set; }

    public Memento(string state)
    {
        State = state;
    }
}
