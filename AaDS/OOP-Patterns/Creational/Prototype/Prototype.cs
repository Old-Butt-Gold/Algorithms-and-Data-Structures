﻿namespace AaDS.OOP_Patterns.Creational.Prototype;

/*
Прототип — это порождающий паттерн, который позволяет копировать объекты 
любой сложности без привязки к их конкретным классам.

Все классы — Прототипы имеют общий интерфейс. Поэтому вы можете копировать объекты,
не обращая внимания на их конкретные типы и всегда быть уверены, 
что получите точную копию. Клонирование совершается самим объектом-прототипом, 
что позволяет ему скопировать значения всех полей, даже приватных.

Применимость: Паттерн Прототип реализован в базовой библиотеке C# посредством интерфейса ICloneable.

Признаки применения паттерна: Прототип легко определяется в коде по наличию методов clone, copy и прочих.
*/

public class Client
{
    public static void Test()
    {
        Circle shape = new Circle();
        var newShape = shape.Clone();
    }
}

interface IClone
{
    Shape Clone();
}

public abstract class Shape : IClone
{
    public string Color { get; set; }
    
    public Shape() { }

    protected Shape(Shape source)
    {
        Color = source.Color;
    }

    public abstract Shape Clone();
}

public class Rectangle : Shape
{
    public int Width { get; set; }
    public int Height { get; set; }

    public Rectangle() { }

    protected Rectangle(Rectangle shape) : base(shape) 
    //либо может быть публичным конструктором, если передается много параметров
    {
        Width = shape.Width;
        Height = shape.Height;
    }
    
    public override Shape Clone() => new Rectangle(this);
}

public class Circle : Shape
{
    public int Radius { get; set; }

    public Circle() { }
    
    protected Circle(Circle shape) : base(shape)
    {
        Radius = shape.Radius;
    }
    
    public override Shape Clone() => new Circle(this);
}

/*
Отношения с другими паттернами
Многие архитектуры начинаются с применения Фабричного метода (более простого и расширяемого через подклассы) 
и эволюционируют в сторону Абстрактной фабрики, Прототипа или Строителя 
(более гибких, но и более сложных).

Классы Абстрактной фабрики чаще всего реализуются с помощью Фабричного метода, 
хотя они могут быть построены и на основе Прототипа.

Если Команду нужно копировать перед вставкой в историю выполненных команд, 
вам может помочь Прототип.

Архитектура, построенная на Компоновщиках и Декораторах, часто может быть 
улучшена за счёт внедрения Прототипа. Он позволяет клонировать сложные структуры 
объектов, а не собирать их заново.

Прототип не опирается на наследование, но ему нужна сложная операция инициализации. 
Фабричный метод, наоборот, построен на наследовании, но не требует сложной инициализации.

Снимок иногда можно заменить Прототипом, если объект, состояние которого 
требуется сохранять в истории, довольно простой, 
не имеет активных ссылок на внешние ресурсы либо их можно легко восстановить.

Абстрактная фабрика, Строитель и Прототип могут быть реализованы при помощи Одиночки.
*/