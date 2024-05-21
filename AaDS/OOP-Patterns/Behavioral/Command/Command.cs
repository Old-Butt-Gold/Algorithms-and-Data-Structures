namespace AaDS.OOP_Patterns.Behavioral.Command;

/*
Также известен как: Действие, Транзакция, Action, Command

Команда — это поведенческий паттерн, позволяющий заворачивать запросы
или простые операции в отдельные объекты.

Это позволяет откладывать выполнение команд, выстраивать их в очереди,
а также хранить историю и делать отмену.

Применимость: Паттерн можно часто встретить в C#-коде, 
особенно когда нужно откладывать выполнение команд, 
выстраивать их в очереди, а также хранить историю и делать отмену.

Признаки применения паттерна: Классы команд построены вокруг одного действия 
и имеют очень узкий контекст. Объекты команд часто подаются в обработчики событий элементов GUI. 
Практически любая реализация отмены использует принципа команд.
*/

interface ICommand
{
    //Еще есть метод CanExecute
    void Execute();
    void Undo();
}
 
class TV //Получатель
{ 
    public void On()
    {
        Console.WriteLine("Телевизор включен!");
    }
 
    public void Off()
    {
        Console.WriteLine("Телевизор выключен...");
    }
}
 
class TVOnCommand : ICommand
{
    TV tv;
    public TVOnCommand(TV tvSet)
    {
        tv = tvSet;
    }
    public void Execute()
    {
        tv.On();
    }
    public void Undo()
    {
        tv.Off();
    }
}
class Volume //Получатель
{
    public const int OFF = 0;
    public const int HIGH = 20;
    private int level;
 
    public Volume()
    {
        level = OFF;
    }
 
    public void RaiseLevel()
    {
        if (level < HIGH)
            level++;
        Console.WriteLine("Уровень звука {0}", level);
    }
    public void DropLevel()
    {
        if (level > OFF)
            level--;
        Console.WriteLine("Уровень звука {0}", level);
    }
}
 
class VolumeCommand : ICommand
{
    Volume volume;
    public VolumeCommand(Volume v)
    {
        volume = v;
    }
    public void Execute()
    {
        volume.RaiseLevel();
    }
 
    public void Undo()
    {
        volume.DropLevel();
    }
}
 
class MultiPult //Отправитель
{
    ICommand[] buttons;
    Stack<ICommand> commandsHistory;
 
    public MultiPult()
    {
        buttons = new ICommand[2];
        commandsHistory = new Stack<ICommand>();
    }
 
    public void SetCommand(int number, ICommand com)
    {
        buttons[number] = com;
    }
 
    public void PressButton(int number)
    {
        if (buttons[number] != null)
        {
            buttons[number].Execute();
            // добавляем выполненную команду в историю команд
            commandsHistory.Push(buttons[number]);
        }
    }
    public void PressUndoButton()
    {
        if (commandsHistory.Count > 0)
        {
            ICommand undoCommand = commandsHistory.Pop();
            undoCommand.Undo();
        }
    }
}

class Demo
{
    public static void Test()
    {
        TV tv = new TV();
        Volume volume = new Volume();
        MultiPult mPult = new MultiPult();
        mPult.SetCommand(0, new TVOnCommand(tv));
        mPult.SetCommand(1, new VolumeCommand(volume));
        // включаем телевизор
        mPult.PressButton(0);
        // увеличиваем громкость
        mPult.PressButton(1);
        mPult.PressButton(1);
        mPult.PressButton(1);
        // действия отмены
        mPult.PressUndoButton();
        mPult.PressUndoButton();
        mPult.PressUndoButton();
        mPult.PressUndoButton();
 
        Console.Read();
    }
}

/*
Отношения с другими паттернами
Цепочка обязанностей, Команда, Посредник и Наблюдатель показывают различные 
способы работы отправителей запросов с их получателями:

Цепочка обязанностей передаёт запрос последовательно через цепочку потенциальных 
получателей, ожидая, что какой-то из них обработает запрос.

Команда устанавливает косвенную одностороннюю связь от отправителей к получателям.

Посредник убирает прямую связь между отправителями и получателями, 
заставляя их общаться опосредованно, через себя.

Наблюдатель передаёт запрос одновременно всем заинтересованным получателям, 
но позволяет им динамически подписываться или отписываться от таких оповещений.

Обработчики в Цепочке обязанностей могут быть выполнены в виде Команд. 
В этом случае множество разных операций может быть выполнено над одним и тем же контекстом, 
коим является запрос.

Но есть и другой подход, в котором сам запрос является Командой, посланной по цепочке объектов. В этом случае одна и та же операция может быть выполнена над множеством разных контекстов, представленных в виде цепочки.

Команду и Снимок можно использовать сообща для реализации отмены операций. 
В этом случае объекты команд будут отвечать за выполнение действия над объектом, 
а снимки будут хранить резервную копию состояния этого объекта, 
сделанную перед самым запуском команды.

Команда и Стратегия похожи по духу, но отличаются масштабом и применением:

1. Команду используют, чтобы превратить любые разнородные действия в объекты. 
Параметры операции превращаются в поля объекта. Этот объект теперь можно логировать, 
хранить в истории для отмены, передавать во внешние сервисы и так далее.

2. С другой стороны, Стратегия описывает разные способы произвести одно и то же действие, 
позволяя взаимозаменять эти способы в каком-то объекте контекста.

Если Команду нужно копировать перед вставкой в историю выполненных команд, вам может помочь Прототип.

Посетитель можно рассматривать как расширенный аналог Команды, 
который способен работать сразу с несколькими видами получателей.
*/