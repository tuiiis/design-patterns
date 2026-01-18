using System.ComponentModel;

namespace BehavioralPatterns.Observer;

// базовый класс автомобиля
public abstract class Car : INotifyPropertyChanged
{
    private string _brand = string.Empty;
    private string _model = string.Empty;
    private int _year;
    private string _color = string.Empty;
    private decimal _price;

    public string Brand
    {
        get => _brand;
        set
        {
            if (_brand != value)
            {
                var oldValue = _brand;
                _brand = value;
                OnPropertyChanged(nameof(Brand), oldValue, value);
            }
        }
    }

    public string Model
    {
        get => _model;
        set
        {
            if (_model != value)
            {
                var oldValue = _model;
                _model = value;
                OnPropertyChanged(nameof(Model), oldValue, value);
            }
        }
    }

    public int Year
    {
        get => _year;
        set
        {
            if (_year != value)
            {
                var oldValue = _year;
                _year = value;
                OnPropertyChanged(nameof(Year), oldValue.ToString(), value.ToString());
            }
        }
    }

    public string Color
    {
        get => _color;
        set
        {
            if (_color != value)
            {
                var oldValue = _color;
                _color = value;
                OnPropertyChanged(nameof(Color), oldValue, value);
            }
        }
    }

    public decimal Price
    {
        get => _price;
        set
        {
            if (_price != value)
            {
                var oldValue = _price;
                _price = value;
                OnPropertyChanged(nameof(Price), oldValue.ToString(), value.ToString());
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public event EventHandler<PropertyChangedWithValuesEventArgs>? PropertyChangedWithValues;

    protected virtual void OnPropertyChanged(string propertyName, string oldValue, string newValue)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        // дополнительное событие с информацией о старом и новом значении
        PropertyChangedWithValues?.Invoke(this, new PropertyChangedWithValuesEventArgs(propertyName, oldValue, newValue));
    }
}
