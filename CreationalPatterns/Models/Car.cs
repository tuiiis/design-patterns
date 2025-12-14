namespace CreationalPatterns.Models;

/// <summary>
/// Базовый класс Car с паттерном Prototype
/// </summary>
public abstract class Car : ICloneable
{
    public double Weight { get; set; }
    public double Length { get; set; }
    public double MaxSpeed { get; set; }

    protected Car(double weight, double length, double maxSpeed)
    {
        Weight = weight;
        Length = length;
        MaxSpeed = maxSpeed;
    }

    protected Car(Car other)
    {
        Weight = other.Weight;
        Length = other.Length;
        MaxSpeed = other.MaxSpeed;
    }

    public abstract Car Clone();
    
    object ICloneable.Clone() => Clone();

    public override string ToString()
    {
        return $"Weight: {Weight} kg, Length: {Length} m, MaxSpeed: {MaxSpeed} km/h";
    }
}

