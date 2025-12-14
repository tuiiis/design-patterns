namespace CreationalPatterns.Models;

/// <summary>
/// Класс Cargo, наследуется от Car
/// </summary>
public class Cargo : Car
{
    public double Tonnage { get; set; }
    public double TankVolume { get; set; }
    public int AxlesAmount { get; set; }

    public Cargo(double weight, double length, double maxSpeed,
                 double tonnage, double tankVolume, int axlesAmount)
        : base(weight, length, maxSpeed)
    {
        Tonnage = tonnage;
        TankVolume = tankVolume;
        AxlesAmount = axlesAmount;
    }

    protected Cargo(Cargo other) : base(other)
    {
        Tonnage = other.Tonnage;
        TankVolume = other.TankVolume;
        AxlesAmount = other.AxlesAmount;
    }

    public override Car Clone()
    {
        return new Cargo(this);
    }

    public override string ToString()
    {
        return $"{base.ToString()}, Tonnage: {Tonnage} t, TankVolume: {TankVolume} L, AxlesAmount: {AxlesAmount}";
    }
}

