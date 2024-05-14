using System.Collections.Specialized;

namespace AaDS.Patterns.Behavioral.Iterator;

/*
Итератор — это поведенческий паттерн, позволяющий последовательно обходить сложную коллекцию, 
без раскрытия деталей её реализации.

Благодаря Итератору, клиент может обходить разные коллекции одним и тем же способом, 
используя единый интерфейс итераторов.

Применимость: Паттерн можно часто встретить в C#-коде, особенно в программах, работающих с 
разными типами коллекций, и где требуется обход разных сущностей.

Признаки применения паттерна: Итератор легко определить по методам навигации 
(например, получения следующего/предыдущего элемента и т. д.). Код использующий итератор зачастую вообще не имеет ссылок на коллекцию, 
с которой работает итератор. Итератор либо принимает коллекцию в параметрах конструктора при создании, либо возвращается самой коллекцией.
*/

interface IEnumerator
{
    object Current { get; }
    bool MoveNext();
    void Reset();
}

interface IEnumerator<out T> : IDisposable, IEnumerator
{
    new T Current { get; }
}

interface IEnumerable
{
    IEnumerator GetEnumerator();
}

interface IEnumerable<out T> : IEnumerable
{
    new IEnumerator<T> GetEnumerator();
}

//Реализованы в .NET

class StringCollection : IEnumerable<string>
{
    string[] _strings;
    private bool _direction;

    public void ReverseDirection() => _direction = !_direction;
    
    public StringCollection(string[] strings)
    {
        _strings = strings;
    }

    public IEnumerator<string> GetEnumerator() => new StringEnumerator(_strings, _direction);
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

class StringEnumerator : IEnumerator<string>
{
    private readonly string[] _strings;
    private int _position;
    private bool _reverse;
    
    public StringEnumerator(string[] strings, bool reverse)
    {
        _strings = strings;
        _reverse = reverse;
        
        _position = _reverse ? _strings.Length : -1;
    }
    
    public void Dispose() { }

    object IEnumerator.Current => Current;

    public string Current => _strings[_position];

    public bool MoveNext()
    {
        int updatedPosition = _position + (_reverse ? -1 : 1);

        if (updatedPosition > -1 && updatedPosition < _strings.Length)
        {
            _position = updatedPosition;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Reset()
    {
        _position = _reverse ? _strings.Length : -1;
    }
}

class Demo
{
    public static void Test()
    {
        string[] strings = ["1", "2", "3", "4"];
        StringCollection stringCollection = new(strings);

        foreach (var str in stringCollection)
        {
            Console.WriteLine(str);
        }
        Console.WriteLine();
        
        stringCollection.ReverseDirection();

        foreach (var str in stringCollection)
        {
            Console.WriteLine(str);
        }

        //Показанный выше foreach преобразуется в 
        IEnumerator<string> e = stringCollection.GetEnumerator();
        try
        {
            while (e.MoveNext())
            {
                string s = e.Current;
                Console.WriteLine(s);
            }
        }
        finally
        {
            e.Dispose(); //можно просто using
        }

    }
}

/* 
Отношения с другими паттернами
Вы можете обходить дерево Компоновщика, используя Итератор.

Фабричный метод можно использовать вместе с Итератором, 
чтобы подклассы коллекций могли создавать подходящие им итераторы.

Снимок можно использовать вместе с Итератором, чтобы сохранить текущее 
состояние обхода структуры данных и вернуться к нему в будущем, если потребуется.

Посетитель можно использовать совместно с Итератором. Итератор будет отвечать за обход структуры данных, 
а Посетитель — за выполнение действий над каждым её компонентом.
*/