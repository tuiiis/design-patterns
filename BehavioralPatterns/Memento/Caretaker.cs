namespace BehavioralPatterns.Memento;

// класс-хранитель истории состояний
public class Caretaker
{
    private readonly List<Memento> _mementos = new();
    private int _currentIndex = -1;

    // сохраняет состояние
    public void SaveState(Memento memento)
    {
        // удаляем все состояния после текущего индекса (если делали undo)
        if (_currentIndex < _mementos.Count - 1)
        {
            _mementos.RemoveRange(_currentIndex + 1, _mementos.Count - _currentIndex - 1);
        }

        _mementos.Add(memento);
        _currentIndex = _mementos.Count - 1;
    }

    // отменяет последнее изменение (undo)
    public Memento? Undo()
    {
        if (_currentIndex > 0)
        {
            _currentIndex--;
            return _mementos[_currentIndex];
        }
        return null;
    }

    // повторяет отменённое изменение (redo)
    public Memento? Redo()
    {
        if (_currentIndex < _mementos.Count - 1)
        {
            _currentIndex++;
            return _mementos[_currentIndex];
        }
        return null;
    }

    // проверяет, можно ли сделать undo
    public bool CanUndo() => _currentIndex > 0;

    // проверяет, можно ли сделать redo
    public bool CanRedo() => _currentIndex < _mementos.Count - 1;
}
