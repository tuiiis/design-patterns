namespace BehavioralPatterns.Visitor;

// класс тора
public class Torus : IShape
{
    public double MajorRadius { get; set; } // большой радиус
    public double MinorRadius { get; set; } // малый радиус

    public Torus(double majorRadius, double minorRadius)
    {
        MajorRadius = majorRadius;
        MinorRadius = minorRadius;
    }

    public void Accept(IVisitor visitor)
    {
        visitor.VisitTorus(this);
    }
}
