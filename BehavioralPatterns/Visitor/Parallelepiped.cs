namespace BehavioralPatterns.Visitor;

// класс параллелепипеда
public class Parallelepiped : IShape
{
    public double Length { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }

    public Parallelepiped(double length, double width, double height)
    {
        Length = length;
        Width = width;
        Height = height;
    }

    public void Accept(IVisitor visitor)
    {
        visitor.VisitParallelepiped(this);
    }
}
