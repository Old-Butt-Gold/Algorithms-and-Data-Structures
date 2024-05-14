using System.ComponentModel.Design;

namespace AaDS.Patterns.Behavioral.State;

/*
Состояние — это поведенческий паттерн, позволяющий динамически 
изменять поведение объекта при смене его состояния.

Поведения, зависящие от состояния, переезжают в отдельные классы. 
Первоначальный класс хранит ссылку на один из таких объектов-состояний 
и делегирует ему работу.

Применимость: Паттерн Состояние часто используют в C# для превращения в 
объекты громоздких стейт-машин, построенных на операторах switch.

Признаки применения паттерна: Методы класса делегируют работу одному вложенному объекту.
*/

class Water
{
    WaterState WaterState { get; set; }

    public Water(WaterState waterState) => SetState(waterState);

    public void SetState(WaterState waterState)
    {
        WaterState = waterState;
        waterState.SetWater(this);
    }

    public void Heat() => WaterState.Heat();
    public void Frost() => WaterState.Frost();

}

abstract class WaterState
{
    protected Water Water { get; set; }
    
    public void SetWater(Water water) => Water = water;
    
    public abstract void Heat();
    public abstract void Frost();
}

class SolidWaterState : WaterState
{
    public override void Heat()
    {
        Console.WriteLine("Превращаем лед в жидкость");
        Water.SetState(new LiquidWaterState());
    }
 
    public override void Frost()
    {
        Console.WriteLine("Продолжаем заморозку льда");
    }
}

class LiquidWaterState : WaterState
{
    public override void Heat()
    {
        Console.WriteLine("Превращаем жидкость в пар");
        Water.SetState(new GasWaterState());
    }
 
    public override void Frost()
    {
        Console.WriteLine("Превращаем жидкость в лед");
        Water.SetState(new SolidWaterState());
    }
}
class GasWaterState : WaterState
{

    public override void Heat()
    {
        Console.WriteLine("Повышаем температуру водяного пара");
    }

    public override void Frost()
    {
        Console.WriteLine("Превращаем водяной пар в жидкость");
        Water.SetState(new LiquidWaterState());
    }
}

class Demo
{
    public static void Test()
    {
        Water water = new(new LiquidWaterState());
        water.Heat();
        water.Frost();
        water.Frost();
        water.Frost();
 
        Console.Read();
    }
}

/*
Отношения с другими паттернами
Мост, Стратегия и Состояние (а также слегка и Адаптер) имеют схожие структуры классов — 
все они построены на принципе «композиции», то есть делегирования работы другим объектам. 
Тем не менее, они отличаются тем, что решают разные проблемы. 
Помните, что паттерны — это не только рецепт построения кода определённым образом, но и описание проблем, 
которые привели к данному решению.

Состояние можно рассматривать как надстройку над Стратегией. 
Оба паттерна используют композицию, чтобы менять поведение основного объекта, делегируя работу вложенным 
объектам-помощникам. Однако в Стратегии эти объекты не знают друг о друге и никак не связаны. 
В Состоянии сами конкретные состояния могут переключать контекст.
*/