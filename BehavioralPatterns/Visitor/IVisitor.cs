namespace BehavioralPatterns.Visitor;

// интерфейс посетителя для работы с фигурами
public interface IVisitor
{
    void VisitSphere(Sphere sphere);
    void VisitParallelepiped(Parallelepiped parallelepiped);
    void VisitTorus(Torus torus);
    void VisitCube(Cube cube);
}
