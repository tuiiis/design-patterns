namespace CreationalPatterns.Models;

/// <summary>
/// Класс Vehicle, наследуется от Car
/// </summary>
public class Vehicle : Car
{
    public WheelDrive WheelDrive { get; set; }
    public VehicleClass Class { get; set; }
    public string Color { get; set; }

    public Vehicle(double weight, double length, double maxSpeed, 
                   WheelDrive wheelDrive, VehicleClass vehicleClass, string color)
        : base(weight, length, maxSpeed)
    {
        WheelDrive = wheelDrive;
        Class = vehicleClass;
        Color = color;
    }

    protected Vehicle(Vehicle other) : base(other)
    {
        WheelDrive = other.WheelDrive;
        Class = other.Class;
        Color = other.Color;
    }

    public override Car Clone()
    {
        return new Vehicle(this);
    }

    public override string ToString()
    {
        return $"{base.ToString()}, WheelDrive: {WheelDrive}, Class: {Class}, Color: {Color}";
    }
}

