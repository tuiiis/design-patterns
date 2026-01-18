namespace BehavioralPatterns.Observer;

// аргументы события с информацией о старом и новом значении
public class PropertyChangedWithValuesEventArgs : EventArgs
{
    public string PropertyName { get; }
    public string OldValue { get; }
    public string NewValue { get; }

    public PropertyChangedWithValuesEventArgs(string propertyName, string oldValue, string newValue)
    {
        PropertyName = propertyName;
        OldValue = oldValue;
        NewValue = newValue;
    }
}
