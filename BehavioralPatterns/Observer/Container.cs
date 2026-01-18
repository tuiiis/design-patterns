namespace BehavioralPatterns.Observer;

// контейнер для хранения автомобилей с поддержкой observer pattern
public class Container
{
    private readonly List<Car> _cars = new();

    public IReadOnlyList<Car> Cars => _cars.AsReadOnly();

    // добавляет автомобиль в коллекцию
    public void AddCar(Car car)
    {
        _cars.Add(car);

        // выводим сообщение о добавлении
        Console.WriteLine($"Car Added: {car.GetType().Name}");

        // подписываемся на изменения свойств
        car.PropertyChangedWithValues += OnCarPropertyChanged;
    }

    // обработчик изменения свойства автомобиля
    private void OnCarPropertyChanged(object? sender, PropertyChangedWithValuesEventArgs e)
    {
        if (sender is Car car)
        {
            Console.WriteLine($"Property Changed - Car: {car.GetType().Name}, Property: {e.PropertyName}, Old Value: {e.OldValue}, New Value: {e.NewValue}");
        }
    }

    // удаляет автомобиль из коллекции
    public void RemoveCar(Car car)
    {
        if (_cars.Remove(car))
        {
            car.PropertyChangedWithValues -= OnCarPropertyChanged;
        }
    }
}
