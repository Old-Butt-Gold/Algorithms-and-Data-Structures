namespace AaDS.OOP_Patterns.Structural.Adapter_or_Wrapper_or_обертка;

/*
Адаптер — это структурный паттерн, который позволяет подружить несовместимые объекты.

Адаптер выступает прослойкой между двумя объектами, 
превращая вызовы одного в вызовы понятные другому.

Применимость: Паттерн можно часто встретить в C#-коде, 
особенно там, где требуется конвертация разных типов данных или совместная 
работа классов с разными интерфейсами.

Признаки применения паттерна: Адаптер получает конвертируемый объект в конструкторе или через параметры своих методов. 
Методы Адаптера обычно совместимы с интерфейсом одного объекта. Они делегируют вызовы вложенному объекту, 
превратив перед этим параметры вызова в формат, поддерживаемый вложенным объектом.
*/

class RoundHole
{
    double Radius { get; set; }
    public RoundHole(double radius) => Radius = radius;

    public double GetRadius() => Radius;
    
    public bool Fits(RoundPeg peg) => Radius >= peg.GetRadius();
}

class RoundPeg
{
    double Radius { get; set; }
    
    public RoundPeg() { }
    
    public RoundPeg(double radius) => Radius = radius;

    public virtual double GetRadius() => Radius;
}

class SquarePeg
{
    double Width { get; set; }
    public SquarePeg(double width) => Width = width;

    public double GetWidth() => Width;

    public double GetSquare() => Math.Pow(Width, 2);
}

class SquarePegAdapter : RoundPeg
{
    SquarePeg _peg;

    public SquarePegAdapter(SquarePeg peg)
    {
        _peg = peg;
    }

    public override double GetRadius() 
        => Math.Sqrt(Math.Pow(_peg.GetWidth() / 2, 2) * 2);
}

public class Demo
{
    public static void Test()
    {
        RoundHole hole = new RoundHole(5);
        RoundPeg roundPeg = new RoundPeg(5);
        if (hole.Fits(roundPeg))
        {
            Console.WriteLine($"RoundPeg {roundPeg.GetRadius()} fits RoundHole {hole.GetRadius()}");
        }

        SquarePeg smallSqPeg = new SquarePeg(2);
        SquarePeg largeSqPeg = new SquarePeg(20);
        
        //hole.Fits(smallSqPeg); //не скомпилируется

        SquarePegAdapter smallSqPegAdapter = new SquarePegAdapter(smallSqPeg);
        SquarePegAdapter largeSqPegAdapter = new SquarePegAdapter(largeSqPeg);

        if (hole.Fits(smallSqPegAdapter))
        {
            Console.WriteLine($"SquarePeg {smallSqPegAdapter.GetRadius()} fits RoundHole {hole.GetRadius()}");
        }
        
        if (!hole.Fits(largeSqPegAdapter))
        {
            Console.WriteLine($"SquarePeg {largeSqPegAdapter.GetRadius()} doesn't fit RoundHole {hole.GetRadius()}");
        }
    }
}

/*
Отношения с другими паттернами
Мост проектируют загодя, чтобы развивать большие части приложения отдельно 
друг от друга. Адаптер применяется постфактум, чтобы заставить несовместимые классы работать вместе.

Адаптер предоставляет совершенно другой интерфейс для доступа к существующему объекту. 
С другой стороны, при использовании паттерна Декоратор интерфейс либо остается прежним, 
либо расширяется. Причём Декоратор поддерживает рекурсивную вложенность, 
чего не скажешь об Адаптере.

С Адаптером вы получаете доступ к существующему объекту через другой интерфейс. 
Используя Заместитель, интерфейс остается неизменным. Используя Декоратор, 
вы получаете доступ к объекту через расширенный интерфейс.

Фасад задаёт новый интерфейс, тогда как Адаптер повторно использует старый. 
Адаптер оборачивает только один класс, а Фасад оборачивает целую подсистему. 
Кроме того, Адаптер позволяет двум существующим интерфейсам работать сообща, 
вместо того, чтобы задать полностью новый.

Мост, Стратегия и Состояние (а также слегка и Адаптер) имеют схожие структуры классов
— все они построены на принципе «композиции», то есть делегирования работы другим объектам. 
Тем не менее, они отличаются тем, что решают разные проблемы. Помните, что паттерны — 
это не только рецепт построения кода определённым образом, но и описание проблем, 
которые привели к данному решению.
*/