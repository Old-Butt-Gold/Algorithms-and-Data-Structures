namespace AaDS.OOP_Patterns.Creational.Builder;

/*
Строитель — это порождающий паттерн проектирования, 
который позволяет создавать объекты пошагово.

В отличие от других порождающих паттернов, Строитель позволяет производить 
различные продукты, используя один и тот же процесс строительства.

Применимость: Паттерн можно часто встретить в C#-коде, 
особенно там, где требуется пошаговое создание продуктов или конфигурация сложных объектов.

Признаки применения паттерна: Строителя можно узнать в классе, который имеет один создающий метод 
и несколько методов настройки создаваемого продукта. Обычно, методы настройки вызывают 
для удобства цепочкой (например, someBuilder.setValueA(1).setValueB(2).create()).
*/

class Car
{
    public override string ToString() => "Car";
    public int Seats { get; set; }
    public string Engine { get; set; }
    public string Name { get; set; }
}

class Manual
{
    public override string ToString() => "Manual";
    
    public int Seats { get; set; }
    public string Engine { get; set; }
    public string Name { get; set; }
}

interface IBuilder 
    //Получение результата определяется уже конкретный строитель, чтобы Director не привязывался к типу класса
{
    void Reset();
    void SetSeats(int num);
    void SetEngine(string engine);
    void SetName(string name);
}

class CarBuilder : IBuilder
{
    Car _car = new();

    public void Reset() => _car = new();

    public void SetSeats(int num)
    {
        _car.Seats = num;
    }

    public void SetEngine(string engine)
    {
        _car.Engine = engine;
    }

    public void SetName(string name)
    {
        _car.Name = name;
    }

    public Car GetResult()
    {
        var temp = _car;
        Reset();
        return temp;
    }
}

class ManualBuilder : IBuilder
{
    Manual _manual = new();

    public void Reset() => _manual = new();

    public void SetSeats(int num)
    {
        _manual.Seats = num;
    }

    public void SetEngine(string engine)
    {
        _manual.Engine = engine;
    }

    public void SetName(string name)
    {
        _manual.Name = name;
    }

    public Manual GetResult()
    {
        var temp = _manual;
        Reset();
        return temp;
    }
}

class Director
{
    /*IBuilder _builder;

//если после получения продукта строителя, будет рефрешиться продукт внутри

    public IBuilder Builder
    {
        set => _builder = value;
    }*/

    public void Construct(IBuilder builder)
    {
        builder.Reset();
        builder.SetEngine("test");
        builder.SetSeats(4);
        builder.SetName("Xiaomi");
    }
}

class Client
{
    static void Test()
    {
        var director = new Director();
        var builder = new CarBuilder();
        
        director.Construct(builder);

        var item = builder.GetResult();
        Console.WriteLine($"{item}: {item.Engine}:{item.Name}");

        var newBuilder = new ManualBuilder();
        director.Construct(newBuilder);
        var newItem = newBuilder.GetResult();
        Console.WriteLine($"{newItem}: {newItem.Engine}:{newItem.Name}");
    } 
}

/*
Отношения с другими паттернами
Многие архитектуры начинаются с применения Фабричного метода 
(более простого и расширяемого через подклассы) и эволюционируют в сторону Абстрактной фабрики,
Прототипа или Строителя (более гибких, но и более сложных).

Строитель концентрируется на построении сложных объектов шаг за шагом. 
Абстрактная фабрика специализируется на создании семейств связанных продуктов. 
Строитель возвращает продукт только после выполнения всех шагов, 
а Абстрактная фабрика возвращает продукт сразу же.

Строитель позволяет пошагово сооружать дерево Компоновщика.

Паттерн Строитель может быть построен в виде Моста: 
директор будет играть роль абстракции, а строители — реализации.

Абстрактная фабрика, Строитель и Прототип могут быть реализованы при помощи Одиночки.
*/