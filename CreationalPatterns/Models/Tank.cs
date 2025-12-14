namespace CreationalPatterns.Models;

/// <summary>
/// Класс Tank, наследуется от Car
/// </summary>
public class Tank : Car
{
    public double ProjectileCaliber { get; set; }
    public int ShotsPerMinute { get; set; }
    public int CrewSize { get; set; }

    public Tank(double weight, double length, double maxSpeed,
                double projectileCaliber, int shotsPerMinute, int crewSize)
        : base(weight, length, maxSpeed)
    {
        ProjectileCaliber = projectileCaliber;
        ShotsPerMinute = shotsPerMinute;
        CrewSize = crewSize;
    }

    protected Tank(Tank other) : base(other)
    {
        ProjectileCaliber = other.ProjectileCaliber;
        ShotsPerMinute = other.ShotsPerMinute;
        CrewSize = other.CrewSize;
    }

    public override Car Clone()
    {
        return new Tank(this);
    }

    public override string ToString()
    {
        return $"{base.ToString()}, ProjectileCaliber: {ProjectileCaliber} mm, ShotsPerMinute: {ShotsPerMinute}, CrewSize: {CrewSize}";
    }
}

