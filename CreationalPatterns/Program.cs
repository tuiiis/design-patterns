using CreationalPatterns.Models;

// Демонстрация паттерна Prototype

// Создаем оригинальные объекты
var vehicle = new Vehicle(1500, 4.5, 180, WheelDrive.Front, VehicleClass.Sedan, "Red");
var cargo = new Cargo(8000, 12.0, 100, 15.0, 200.0, 3);
var tank = new Tank(45000, 9.5, 70, 125.0, 10, 4);

Console.WriteLine("=== Оригинальные объекты ===");
Console.WriteLine($"Vehicle: {vehicle}");
Console.WriteLine($"Cargo: {cargo}");
Console.WriteLine($"Tank: {tank}");
Console.WriteLine();

// Клонируем объекты используя паттерн Prototype
var vehicleClone = vehicle.Clone() as Vehicle;
var cargoClone = cargo.Clone() as Cargo;
var tankClone = tank.Clone() as Tank;

Console.WriteLine("=== Клонированные объекты ===");
Console.WriteLine($"Vehicle Clone: {vehicleClone}");
Console.WriteLine($"Cargo Clone: {cargoClone}");
Console.WriteLine($"Tank Clone: {tankClone}");
Console.WriteLine();

// Изменяем клоны, чтобы показать, что это независимые объекты
if (vehicleClone != null)
{
    vehicleClone.Color = "Blue";
    vehicleClone.Weight = 1600;
}

if (cargoClone != null)
{
    cargoClone.Tonnage = 20.0;
    cargoClone.AxlesAmount = 4;
}

if (tankClone != null)
{
    tankClone.CrewSize = 5;
    tankClone.ShotsPerMinute = 12;
}

Console.WriteLine("=== После изменения клонов ===");
Console.WriteLine($"Original Vehicle: {vehicle}");
Console.WriteLine($"Modified Vehicle Clone: {vehicleClone}");
Console.WriteLine();
Console.WriteLine($"Original Cargo: {cargo}");
Console.WriteLine($"Modified Cargo Clone: {cargoClone}");
Console.WriteLine();
Console.WriteLine($"Original Tank: {tank}");
Console.WriteLine($"Modified Tank Clone: {tankClone}");
