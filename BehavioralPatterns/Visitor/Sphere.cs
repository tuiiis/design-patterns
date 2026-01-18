namespace BehavioralPatterns.Visitor;

// класс сферы
public class Sphere : IShape
{
    public double Radius { get; set; }

    public Sphere(double radius)
    {
        Radius = radius;
    }

    public void Accept(IVisitor visitor)
    {
        visitor.VisitSphere(this);
    }
}
