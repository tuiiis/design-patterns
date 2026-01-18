namespace BehavioralPatterns.Visitor;

// интерфейс для геометрических фигур
public interface IShape
{
    // метод для принятия посетителя
    void Accept(IVisitor visitor);
}
