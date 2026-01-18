using BehavioralPatterns.Visitor;
using BehavioralPatterns.Memento;
using BehavioralPatterns.Observer;
using Bogus;

Console.WriteLine("=== Behavioral Patterns Demonstration ===\n");

// демонстрация visitor pattern
Console.WriteLine("Task 1: Visitor Pattern - Volume Calculation\n");

// создаём фигуры
var shapes = new IShape[]
{
    new Sphere(5.0),
    new Parallelepiped(3.0, 4.0, 5.0),
    new Torus(5.0, 2.0),
    new Cube(4.0)
};

var visitor = new VolumeVisitor();

// вычисляем объём каждой фигуры
foreach (var shape in shapes)
{
    shape.Accept(visitor);
    var shapeName = shape.GetType().Name;
    Console.WriteLine($"{shapeName} Volume: {visitor.Volume:F2}");
}

Console.WriteLine("\n" + new string('-', 50) + "\n");

// демонстрация memento pattern
Console.WriteLine("Task 2: Memento Pattern - State Management\n");

var originator = new Originator();
var caretaker = new Caretaker();

// начальное состояние
originator.State = "Initial State";
caretaker.SaveState(originator.CreateMemento());

// изменяем состояние несколько раз
var faker = new Faker();
var states = new[]
{
    faker.Lorem.Sentence(),
    faker.Lorem.Sentence(),
    faker.Lorem.Sentence()
};

foreach (var state in states)
{
    originator.State = state;
    caretaker.SaveState(originator.CreateMemento());
}

Console.WriteLine("\nPerforming Undo Operations:\n");

// делаем undo
while (caretaker.CanUndo())
{
    var memento = caretaker.Undo();
    if (memento != null)
    {
        originator.RestoreMemento(memento);
    }
}

Console.WriteLine("\nPerforming Redo Operations:\n");

// делаем redo
while (caretaker.CanRedo())
{
    var memento = caretaker.Redo();
    if (memento != null)
    {
        originator.RestoreMemento(memento);
    }
}

Console.WriteLine("\n" + new string('-', 50) + "\n");

// демонстрация observer pattern
Console.WriteLine("Task 3: Observer Pattern - Car Container\n");

var container = new Container();
var carFaker = new Faker();

// создаём автомобили разных типов с помощью bogus
var sedanFaker = new Faker<Sedan>()
    .RuleFor(c => c.Brand, f => f.Vehicle.Manufacturer())
    .RuleFor(c => c.Model, f => f.Vehicle.Model())
    .RuleFor(c => c.Year, f => f.Date.Past(5).Year)
    .RuleFor(c => c.Color, f => f.Commerce.Color())
    .RuleFor(c => c.Price, f => f.Finance.Amount(10000, 50000, 2));

var suvFaker = new Faker<SUV>()
    .RuleFor(c => c.Brand, f => f.Vehicle.Manufacturer())
    .RuleFor(c => c.Model, f => f.Vehicle.Model())
    .RuleFor(c => c.Year, f => f.Date.Past(5).Year)
    .RuleFor(c => c.Color, f => f.Commerce.Color())
    .RuleFor(c => c.Price, f => f.Finance.Amount(20000, 80000, 2));

var sportsCarFaker = new Faker<SportsCar>()
    .RuleFor(c => c.Brand, f => f.Vehicle.Manufacturer())
    .RuleFor(c => c.Model, f => f.Vehicle.Model())
    .RuleFor(c => c.Year, f => f.Date.Past(5).Year)
    .RuleFor(c => c.Color, f => f.Commerce.Color())
    .RuleFor(c => c.Price, f => f.Finance.Amount(30000, 150000, 2));

Console.WriteLine("Adding Cars To Container:\n");

// добавляем несколько автомобилей
var sedan = sedanFaker.Generate();
container.AddCar(sedan);

var suv = suvFaker.Generate();
container.AddCar(suv);

var sportsCar = sportsCarFaker.Generate();
container.AddCar(sportsCar);

Console.WriteLine("\nModifying Car Properties:\n");

// изменяем свойства автомобилей
sedan.Color = carFaker.Commerce.Color();
sedan.Price = carFaker.Finance.Amount(10000, 50000, 2);

suv.Brand = carFaker.Vehicle.Manufacturer();
suv.Year = carFaker.Date.Past(3).Year;

sportsCar.Model = carFaker.Vehicle.Model();
sportsCar.Color = carFaker.Commerce.Color();

Console.WriteLine("\n=== Demonstration Complete ===");
