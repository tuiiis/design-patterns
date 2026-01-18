# Project Structure and Requirements Implementation

## General Project Structure

The project is a .NET 8.0 console application organized by pattern:

```
BehavioralPatterns/
├── BehavioralPatterns.csproj    # Project configuration with Bogus package
├── Program.cs                    # Main entry point demonstrating all patterns
│
├── Visitor/                     # Task 1: Visitor Pattern
│   ├── IShape.cs               # Interface for geometric shapes
│   ├── IVisitor.cs             # Visitor interface
│   ├── Sphere.cs               # Sphere shape implementation
│   ├── Parallelepiped.cs       # Parallelepiped shape implementation
│   ├── Torus.cs                # Torus shape implementation
│   ├── Cube.cs                 # Cube shape implementation
│   └── VolumeVisitor.cs        # Visitor that calculates volumes
│
├── Memento/                     # Task 2: Memento Pattern
│   ├── Memento.cs              # State storage class
│   ├── Originator.cs           # Creates and restores mementos
│   └── Caretaker.cs            # Manages memento history (undo/redo)
│
└── Observer/                    # Task 3: Observer Pattern
    ├── Car.cs                   # Base Car class with property change notifications
    ├── Sedan.cs                 # Derived car class
    ├── SUV.cs                   # Derived car class
    ├── SportsCar.cs             # Derived car class
    ├── Container.cs             # Container that observes car changes
    └── PropertyChangedWithValuesEventArgs.cs  # Event args with old/new values
```

Each pattern is organized in its own folder. The `Program.cs` file demonstrates all three patterns sequentially.

---

## Task 1: Visitor Pattern - Volume Calculation

### Requirement
Create a set of classes corresponding to geometric shapes: Sphere, Parallelepiped, Torus, Cube. Apply the Visitor pattern to calculate the volume of each shape.

### Implementation

**1. Shape Interface (`IShape.cs`)**
- Defines `Accept(IVisitor visitor)` method that all shapes must implement
- This is the entry point for the visitor pattern

**2. Visitor Interface (`IVisitor.cs`)**
- Defines visit methods for each shape type:
  - `VisitSphere(Sphere sphere)`
  - `VisitParallelepiped(Parallelepiped parallelepiped)`
  - `VisitTorus(Torus torus)`
  - `VisitCube(Cube cube)`

**3. Shape Classes**
- **`Sphere`**: Stores radius property
- **`Parallelepiped`**: Stores length, width, and height properties
- **`Torus`**: Stores major radius and minor radius properties
- **`Cube`**: Stores side length property
- Each class implements `Accept()` method that calls the corresponding visitor method

**4. Volume Visitor (`VolumeVisitor.cs`)**
- Implements `IVisitor` interface
- Calculates volumes using correct mathematical formulas:
  - **Sphere**: `(4/3)πr³`
  - **Parallelepiped**: `length × width × height`
  - **Torus**: `2π²Rr²` (where R = major radius, r = minor radius)
  - **Cube**: `side³`
- Stores the calculated volume in a `Volume` property

**How It Works:**
```csharp
var visitor = new VolumeVisitor();
shape.Accept(visitor);  // Shape calls visitor.Visit[ShapeType](this)
// Volume is now calculated and stored in visitor.Volume
```

This pattern allows adding new operations (like volume calculation) without modifying the shape classes themselves, following the Open/Closed Principle.

---

## Task 2: Memento Pattern - State Management

### Requirement
Implement the Memento behavioral pattern (variant 8).

### Implementation

**1. Memento (`Memento.cs`)**
- Stores a snapshot of state (in this case, a string)
- Immutable - once created, the state cannot be changed
- Acts as a "time capsule" for state

**2. Originator (`Originator.cs`)**
- Manages the current state
- `CreateMemento()`: Creates a memento from the current state
- `RestoreMemento(Memento)`: Restores state from a memento
- Logs state changes to console

**3. Caretaker (`Caretaker.cs`)**
- Manages the history of mementos
- `SaveState(Memento)`: Saves a memento to history (handles undo/redo cleanup)
- `Undo()`: Returns previous memento in history
- `Redo()`: Returns next memento in history
- `CanUndo()` / `CanRedo()`: Check if undo/redo operations are possible

**How It Works:**
```csharp
var originator = new Originator();
var caretaker = new Caretaker();

// Save states
originator.State = "State 1";
caretaker.SaveState(originator.CreateMemento());

// Undo/Redo
var memento = caretaker.Undo();
originator.RestoreMemento(memento);
```

This pattern enables undo/redo functionality by saving and restoring state snapshots without exposing the internal structure of the state.

---

## Task 3: Observer Pattern - Car Container

### Requirement
Create a `Container` class that:
1. Contains a collection of instances of classes derived from the Car class
2. Outputs to console a message about which class instance was added to the collection
3. Outputs the property name, old value, and new value when a property of a Car-derived instance is changed

### Implementation

**1. Base Car Class (`Car.cs`)**
- Properties: `Brand`, `Model`, `Year`, `Color`, `Price`
- Implements `INotifyPropertyChanged` interface
- Each property setter:
  - Compares old and new values
  - Raises standard `PropertyChanged` event
  - Raises custom `PropertyChangedWithValues` event with old/new values
- Custom event provides detailed information for logging

**2. Derived Car Classes**
- **`Sedan.cs`**: Empty class inheriting from `Car`
- **`SUV.cs`**: Empty class inheriting from `Car`
- **`SportsCar.cs`**: Empty class inheriting from `Car`
- These can be extended with specific properties if needed

**3. Property Changed Event Args (`PropertyChangedWithValuesEventArgs.cs`)**
- Custom event arguments that carry:
  - `PropertyName`: Name of the changed property
  - `OldValue`: Previous value (as string)
  - `NewValue`: New value (as string)

**4. Container Class (`Container.cs`)**
- Maintains a private `List<Car>` collection
- `AddCar(Car car)` method:
  - Adds car to collection
  - Outputs: `"Car Added: [ClassName]"`
  - Subscribes to car's `PropertyChangedWithValues` event
- `OnCarPropertyChanged` event handler:
  - Outputs: `"Property Changed - Car: [ClassName], Property: [Name], Old Value: [Old], New Value: [New]"`
- `RemoveCar(Car car)`: Removes car and unsubscribes from events

**How It Works:**
```csharp
var container = new Container();
var sedan = new Sedan { Brand = "Toyota" };
container.AddCar(sedan);  // Outputs: "Car Added: Sedan"

sedan.Brand = "Honda";     // Outputs: "Property Changed - Car: Sedan, 
                           //           Property: Brand, Old Value: Toyota, New Value: Honda"
```

The Container acts as an observer, automatically notified when cars are added or their properties change, and logs all relevant information to the console.

---

## Additional Features

1. **Bogus Library Integration**: Used to generate fake car data (brands, models, colors, prices, years) for demonstration purposes
2. **Console Output**: All console outputs are in English with proper capitalization
3. **Comments**: All code comments are in Russian, casual style, no capitalization
4. **Separation of Concerns**: Each pattern is in its own namespace and folder for better organization

---

## Program Flow

The `Program.cs` file demonstrates all three patterns:

1. **Visitor Pattern**: Creates various shapes, calculates their volumes using the visitor, and displays results
2. **Memento Pattern**: Creates multiple states, demonstrates undo/redo functionality
3. **Observer Pattern**: Creates cars with fake data using Bogus, adds them to container, and modifies properties to show observer notifications

Each demonstration is separated by visual dividers for clarity.

This structure makes it easy to understand each pattern individually and see how they work together in a single application.
