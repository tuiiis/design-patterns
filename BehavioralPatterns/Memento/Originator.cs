namespace BehavioralPatterns.Memento;

// класс-создатель, который сохраняет и восстанавливает состояние
public class Originator
{
    private string _state = string.Empty;

    public string State
    {
        get => _state;
        set
        {
            _state = value;
            Console.WriteLine($"State Changed To: {_state}");
        }
    }

    // создаёт memento с текущим состоянием
    public Memento CreateMemento()
    {
        return new Memento(_state);
    }

    // восстанавливает состояние из memento
    public void RestoreMemento(Memento memento)
    {
        _state = memento.State;
        Console.WriteLine($"State Restored To: {_state}");
    }
}
