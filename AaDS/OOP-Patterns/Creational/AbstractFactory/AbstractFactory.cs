namespace AaDS.OOP_Patterns.Creational.AbstractFactory;

/*
Абстрактная фабрика — это порождающий паттерн проектирования, 
который решает проблему создания целых семейств связанных продуктов, без указания конкретных классов продуктов.

Абстрактная фабрика задаёт интерфейс создания всех доступных типов продуктов, а каждая конкретная реализация 
фабрики порождает продукты одной из вариаций. Клиентский код вызывает методы фабрики 
для получения продуктов, вместо самостоятельного создания с помощью оператора new. 
При этом фабрика сама следит за тем, чтобы создать продукт нужной вариации.

Применимость: Паттерн можно часто встретить в C#-коде, особенно там, 
где требуется создание семейств продуктов (например, внутри фреймворков).

Признаки применения паттерна: Паттерн можно определить по методам, 
возвращающим фабрику, которая, в свою очередь, используется для создания конкретных продуктов, 
возвращая их через абстрактные типы или интерфейсы.
*/

interface IButton //может быть абстрактным классом
{
    void Paint();
}

interface ICheckBox //может быть абстрактным классом
{
    void Paint();
}

class WinButton : IButton
{
    public void Paint()
    {
        Console.WriteLine("Рисуем WinButton");   
    }
}

class MacButton : IButton
{
    public void Paint()
    {
        Console.WriteLine("Рисуем MacButton");
    }
}

class WinCheckbox : ICheckBox
{
    public void Paint()
    {
        Console.WriteLine("Рисуем WinCheckBox");
    }
}

class MacCheckbox : ICheckBox
{
    public void Paint()
    {
        Console.WriteLine("Рисуем MacCheckBox");
    }
}

abstract class AbstractFactory //Может быть интерфейсом
{
    public abstract IButton CreateButton();
    public abstract ICheckBox CreateCheckBox();
}

class WinFactory : AbstractFactory
{
    public override IButton CreateButton()
    {
        return new WinButton();
    }

    public override ICheckBox CreateCheckBox()
    {
        return new WinCheckbox();
    }
} 

class MacFactory : AbstractFactory
{
    public override IButton CreateButton()
    {
        return new MacButton();
    }

    public override ICheckBox CreateCheckBox()
    {
        return new MacCheckbox();
    }
}

class Application
{
    AbstractFactory _factory;

    public Application(AbstractFactory factory) => _factory = factory;

    public void Initialize()
    {
        _factory.CreateButton().Paint();
        _factory.CreateCheckBox().Paint();
    }
}

class Client
{
    
    public static void Test()
    {
        Console.WriteLine(Environment.OSVersion);
        if (Environment.OSVersion.VersionString == "Windows")
        {
            Application application = new Application(new WinFactory());
        }

        if (Environment.OSVersion.VersionString == "Mac")
        {
            Application application = new Application(new MacFactory());
        }
    }
}

/* 

Отношения с другими паттернами
Многие архитектуры начинаются с применения Фабричного метода (более простого и расширяемого через подклассы) 
и эволюционируют в сторону Абстрактной фабрики, Прототипа или Строителя 
(более гибких, но и более сложных).

Строитель концентрируется на построении сложных объектов шаг за шагом. 
Абстрактная фабрика специализируется на создании семейств связанных продуктов. 
Строитель возвращает продукт только после выполнения всех шагов, 
а Абстрактная фабрика возвращает продукт сразу же.

Классы Абстрактной фабрики чаще всего реализуются с помощью Фабричного метода, 
хотя они могут быть построены и на основе Прототипа.

Абстрактная фабрика может быть использована вместо Фасада для того, 
чтобы скрыть платформо-зависимые классы.

Абстрактная фабрика может работать совместно с Мостом. 
Это особенно полезно, если у вас есть абстракции, 
которые могут работать только с некоторыми из реализаций. 
В этом случае фабрика будет определять типы создаваемых абстракций и реализаций.

Абстрактная фабрика, Строитель и Прототип могут быть реализованы при помощи Одиночки.

*/