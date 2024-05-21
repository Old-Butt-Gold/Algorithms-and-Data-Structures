namespace AaDS.OOP_Patterns.Structural.Composite_компоновщик_;

//Также известен как: Дерево, Composite

/*
Компоновщик — это структурный паттерн, который позволяет создавать 
дерево объектов и работать с ним так же, как и с единичным объектом.

Компоновщик давно стал синонимом всех задач, связанных с построением дерева объектов. 
Все операции компоновщика основаны на рекурсии и «суммировании» результатов на ветвях дерева.

Применимость: Паттерн Компоновщик встречается в любых задачах, 
которые связаны с построением дерева. Самый простой пример — составные элементы GUI, 
которые тоже можно рассматривать как дерево.

Признаки применения паттерна: Если из объектов строится древовидная структура, 
и со всеми объектами дерева, как и с самим деревом работают через общий интерфейс.
*/

interface IGraphic
{
    void Move(int x, int y);
    void Draw();
}

class Dot : IGraphic
{
    protected int _x;
    protected int _y;

    public Dot(int x, int y) => (_x, _y) = (x, y);
    
    public void Move(int x, int y)
    {
        _x += x;
        _y += y;
    }

    public virtual void Draw()
    {
        Console.WriteLine($"Нарисовать точку в координате {_x}:{_y}");
    }
}

class Circle : Dot
{
    int _radius;

    public Circle(int x, int y, int radius) : base(x, y)
    {
        _radius = radius;
    }

    public override void Draw()
    {
        Console.WriteLine($"Нарисовать круг с радиусом {_radius} в координатах {_x}:{_y}");
    }
}

class CompoundGraphic : IGraphic
{
    List<IGraphic> _list = new();

    public void Add(IGraphic child) => _list.Add(child);

    public void Remove(IGraphic child) => _list.Remove(child);

    public void Move(int x, int y)
    {
        foreach (var child in _list)
            child.Move(x, y);
    }

    public void Draw()
    {
        foreach (var child in _list)
            child.Draw();
    }
}

class ImageEditor
{
    static CompoundGraphic _compoundGraphic = new();

    public static void Demo()
    {
        CompoundGraphic compoundGraphic1 = new();
        compoundGraphic1.Add(new Dot(0, 0));
        compoundGraphic1.Add(new Dot(0, 10));
        compoundGraphic1.Add(new Dot(10, 10));
        compoundGraphic1.Add(new Dot(10, 0));
        compoundGraphic1.Add(new Circle(5, 5, 5));

        _compoundGraphic.Add(compoundGraphic1);
        
        _compoundGraphic.Add(new Dot(100, 100));
        
        _compoundGraphic.Draw();
        
        _compoundGraphic.Move(10, 10);
        
        _compoundGraphic.Draw();
    }
}

/*
Отношения с другими паттернами
Строитель позволяет пошагово сооружать дерево Компоновщика.

Цепочку обязанностей часто используют вместе с Компоновщиком. 
В этом случае запрос передаётся от дочерних компонентов к их родителям.

Вы можете обходить дерево Компоновщика, используя Итератор.

Вы можете выполнить какое-то действие над всем деревом Компоновщика при помощи Посетителя.

Компоновщик часто совмещают с Легковесом, 
чтобы реализовать общие ветки дерева и сэкономить при этом память.

Компоновщик и Декоратор имеют похожие структуры классов из-за того, 
что оба построены на рекурсивной вложенности. Она позволяет связать 
в одну структуру бесконечное количество объектов.

Декоратор оборачивает только один объект, а узел Компоновщика 
может иметь много детей. Декоратор добавляет вложенному объекту новую функциональность, 
а Компоновщик не добавляет ничего нового, но «суммирует» результаты всех своих детей.

Но они могут и сотрудничать: Компоновщик может использовать Декоратор, 
чтобы переопределить функции отдельных частей дерева компонентов.

Архитектура, построенная на Компоновщиках и Декораторах, часто может быть улучшена 
за счёт внедрения Прототипа. Он позволяет клонировать сложные структуры объектов, 
а не собирать их заново.
 */


