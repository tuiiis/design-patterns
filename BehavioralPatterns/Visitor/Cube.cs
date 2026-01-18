namespace BehavioralPatterns.Visitor;

// класс куба
public class Cube : IShape
{
    public double SideLength { get; set; }

    public Cube(double sideLength)
    {
        SideLength = sideLength;
    }

    public void Accept(IVisitor visitor)
    {
        visitor.VisitCube(this);
    }
}
