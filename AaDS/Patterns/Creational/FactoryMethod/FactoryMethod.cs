namespace AaDS.Patterns.Creational.FactoryMethod;

/*
Фабричный метод — это порождающий паттерн проектирования, 
который решает проблему создания различных продуктов, без указания конкретных классов продуктов.

Фабричный метод задаёт метод, который следует использовать 
вместо вызова оператора new для создания объектов-продуктов. 
Подклассы могут переопределить этот метод, чтобы изменять тип создаваемых продуктов.

Применимость: Паттерн можно часто встретить в любом C#-коде, где требуется гибкость при создании продуктов.

Признаки применения паттерна: Фабричный метод можно определить по создающим методам, 
которые возвращают объекты продуктов через абстрактные типы или интерфейсы.
Это позволяет переопределять типы создаваемых продуктов в подклассах.
*/

public class Client
{
    private static Creator Creator { get; set; }
    
    public static void Test(string type)
    {
        if (type is "circle")
        {
            Creator = new CircleCreator();
        }
        if (type is "rectangle")
        {
            Creator = new RectangleCreator();
        }

        var shape = Creator.FactoryMethod("#FFFFFF");
        Console.WriteLine(shape.ToString());
    }
}

public abstract class Creator
{
    //Могут быть какие нибудь еще поля и к конструктору Creator
    public abstract Shape FactoryMethod(string color);
}

public class CircleCreator : Creator
{
    public override Shape FactoryMethod(string color) => new Circle(color);
}

public class RectangleCreator : Creator
{
    public override Shape FactoryMethod(string color) => new Rectangle(color);
}

public abstract class Shape
{
    public string Color { get; set; }
    public Shape(string color) => Color = color;
}

public class Circle : Shape
{
    public Circle(string color) : base(color) { }

    public override string ToString() => "Круг";
} 

public class Rectangle : Shape
{
    public Rectangle(string color) : base(color) { }

    public override string ToString() => "Прямоугольник";
}

/*

Отношения с другими паттернами
Многие архитектуры начинаются с применения Фабричного метода 
(более простого и расширяемого через подклассы) и эволюционируют в сторону Абстрактной фабрики, 
Прототипа или Строителя (более гибких, но и более сложных).

Классы Абстрактной фабрики чаще всего реализуются с помощью Фабричного метода, 
хотя они могут быть построены и на основе Прототипа.

Фабричный метод можно использовать вместе с Итератором, 
чтобы подклассы коллекций могли создавать подходящие им итераторы.

Прототип не опирается на наследование, 
но ему нужна сложная операция инициализации. 
Фабричный метод, наоборот, построен на наследовании, но не требует сложной инициализации.

Фабричный метод можно рассматривать как частный случай Шаблонного метода. 
Кроме того, Фабричный метод нередко бывает частью большого класса с 
Шаблонными методами.

*/