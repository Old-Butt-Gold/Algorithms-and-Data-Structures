using System.Collections.Concurrent;

namespace AaDS.OOP_Patterns.Creational.Object_Pool;

//Пул объектов — порождающий шаблон проектирования, набор инициализированных и готовых к использованию объектов.

/*
применимость пула ограничивается следующими критериями:
дорогие для создания и/или уничтожения объекты (примеры: сокеты, потоки, неуправляемые ресурсы);
очистка объектов для переиспользования дешевле создания нового (или ничего не стоит);
объекты очень большого размера.

Немного поясню последний пункт. Если ваш объект занимает в памяти 85 000 байт и более, он попадает в кучу больших объектов (large object heap) во втором поколении сборки мусора, что автоматически делает его «долгоживущим» объектом. Прибавим к этому фрагментированность (эта куча не сжимается) и получим потенциальную проблему нехватки памяти при постоянных выделениях/уничтожениях.
Идея пула состоит в том, чтобы организовать переиспользование «дорогих» объектов

var obj = pool.Take(); // нам потребовался рабочий объект. Вместо создания мы запрашиваем его из пула
obj.DoSomething();
pool.Release(obj); // возвращаем ("освобождаем") объект в пул, когда он становится не нужным

Проблемы такого подхода:
после выполнения работы с объектом может потребоваться его сброс в начальное состояние, чтобы предыдущее использование никак не влияло на последующие;
пул должен обеспечивать потокобезопасность, ведь применяется он, как правило, в многопоточных системах;
пул должен обрабатывать ситуацию, когда в нем не осталось доступных для выдачи объектов.
*/

// Интерфейс для сбрасываемых объектов
public interface IResettable
{
    void Reset();
}

// Пример класса, который будет помещаться в пул объектов
public class PooledObject : IResettable
{
    public void DoSomething() => Console.WriteLine("Doing something...");

    public void Reset() => Console.WriteLine("Resetting object...");
}

// Класс пула объектов
public class ObjectPool<T> where T : class, IResettable, new()
{
    private readonly ConcurrentStack<T> _objects;
    private readonly int _maxSize;

    public ObjectPool(int maxSize)
    {
        _objects = [];
        _maxSize = maxSize;
    }

    public T Take()
    {
        if (_objects.Count > 0)
        {
            _objects.TryPop(out var obj);
            return obj!;
        }
        else
        {
            return new T();
        }
    }

    public void Release(T item)
    {
        if (_objects.Count < _maxSize)
        {
            item.Reset();
            _objects.Push(item);
        }
    }
}

// Пример использования пула объектов
class Test
{
    public static void Demo()
    {
        var pool = new ObjectPool<PooledObject>(10);

        var obj1 = pool.Take(); // Запрашиваем объект из пула
        var obj2 = pool.Take(); // Запрашиваем объект из пула
        obj1.DoSomething();
        obj2.DoSomething();

        pool.Release(obj1);
        pool.Release(obj2);

        var obj3 = pool.Take();
        Console.WriteLine(obj1 == obj3);
        Console.WriteLine(obj2 == obj3);
    }
}