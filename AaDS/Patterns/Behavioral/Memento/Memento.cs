namespace AaDS.Patterns.Behavioral.Memento;

//Также известен как: Хранитель, Memento, Снимок

/*
Снимок — это поведенческий паттерн, позволяющий делать 
снимки внутреннего состояния объектов, а затем восстанавливать их.

При этом Снимок не раскрывает подробностей реализации объектов, 
и клиент не имеет доступа к защищённой информации объекта.

Применимость: Снимок на C# чаще всего реализуют с помощью сериализации. 
Но это не единственный, да и не самый эффективный 
метод сохранения состояния объектов во время выполнения программы. 
*/

//P.S. Снимок с повышенной защитой

// Интерфейс для снимка
interface IMemento
{
    void Restore(); // Метод для восстановления состояния
}

// Интерфейс для создателя снимка
interface IOriginator
{
    IMemento Save(); // Метод для сохранения состояния
}

class ConcreteOriginator : IOriginator
{
    string _state;

    public ConcreteOriginator(string state)
    {
        _state = state;
        Console.WriteLine("Originator: My initial state is: " + state);
    }

    public void ChangeState()
    {
        Console.WriteLine("Originator: I'm doing something important.");
        _state = GenerateRandomString(30);
        Console.WriteLine($"Originator: and my state has changed to: {_state}");

        string GenerateRandomString(int length = 10)
        {
            string allowedSymbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string result = string.Empty;

            while (length > 0)
            {
                result += allowedSymbols[new Random().Next(0, allowedSymbols.Length)];

                Thread.Sleep(12);

                length--;
            }

            return result;
        }
    }

    public IMemento Save() => new ConcreteMemento(this);

    class ConcreteMemento : IMemento
    {
        private readonly ConcreteOriginator _concreteOriginator;
        private readonly string _state;
        
        public ConcreteMemento(ConcreteOriginator concreteOriginator)
        {
            _concreteOriginator = concreteOriginator;
            _state = concreteOriginator._state;
        }
        
        public void Restore()
        {
            _concreteOriginator._state = _state;
            Console.WriteLine("Memento: Restoring state to: " + _state);
        }
    }
}

class Caretaker
{
    Stack<IMemento> _mementos = [];

    //Можно создать несколько объектов, которые будут делать снимки и помещать в стек, 
    //однако при откате сам снимок будет восстанавливать исходное состояние объекта (по ссылке)
    IOriginator _originator;

    public Caretaker(IOriginator originator) => _originator = originator;

    public void Backup()
    {
        Console.WriteLine("\nCaretaker: Saving Originator's state...");
        _mementos.Push(_originator.Save());
    }

    public void Undo()
    {
        if (_mementos.Count is 0) return;

        var memento = _mementos.Pop();
        memento.Restore();
    }
}

public class Demo
{
    public static void Test()
    {
        ConcreteOriginator originator = new ConcreteOriginator("Super-duper-super-puper-super.");
        Caretaker caretaker = new Caretaker(originator);

        caretaker.Backup();
        originator.ChangeState();

        caretaker.Backup();
        originator.ChangeState();

        caretaker.Backup();
        originator.ChangeState();

        Console.WriteLine("\nClient: Now, let's rollback!\n");
        caretaker.Undo();

        Console.WriteLine("\n\nClient: Once more!\n");
        caretaker.Undo();
        
        Console.WriteLine("\nClient: One last time!");
        caretaker.Undo();
    }
}
    
/*
Команду и Снимок можно использовать сообща для реализации отмены операций. 
В этом случае объекты команд будут отвечать за выполнение действия над объектом, 
а снимки будут хранить резервную копию состояния этого объекта, сделанную перед самым запуском команды.

Снимок можно использовать вместе с Итератором, чтобы сохранить текущее состояние обхода 
структуры данных и вернуться к нему в будущем, если потребуется.

Снимок иногда можно заменить Прототипом, если объект, 
состояние которого требуется сохранять в истории, довольно простой, 
не имеет активных ссылок на внешние ресурсы либо их можно легко восстановить.
*/
