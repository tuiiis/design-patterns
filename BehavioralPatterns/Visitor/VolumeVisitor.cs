namespace BehavioralPatterns.Visitor;

// посетитель для вычисления объёма фигур
public class VolumeVisitor : IVisitor
{
    public double Volume { get; private set; }

    public void VisitSphere(Sphere sphere)
    {
        // объём сферы: (4/3)πr³
        Volume = (4.0 / 3.0) * Math.PI * Math.Pow(sphere.Radius, 3);
    }

    public void VisitParallelepiped(Parallelepiped parallelepiped)
    {
        // объём параллелепипеда: длина × ширина × высота
        Volume = parallelepiped.Length * parallelepiped.Width * parallelepiped.Height;
    }

    public void VisitTorus(Torus torus)
    {
        // объём тора: 2π²Rr², где R - большой радиус, r - малый радиус
        Volume = 2 * Math.PI * Math.PI * torus.MajorRadius * Math.Pow(torus.MinorRadius, 2);
    }

    public void VisitCube(Cube cube)
    {
        // объём куба: сторона³
        Volume = Math.Pow(cube.SideLength, 3);
    }
}
