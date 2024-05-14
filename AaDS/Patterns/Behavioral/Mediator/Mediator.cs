namespace AaDS.Patterns.Behavioral.Mediator;

//Также известен как: Intermediary, Controller, Посредник

/*
Посредник — это поведенческий паттерн, который упрощает коммуникацию между компонентами системы.

Посредник убирает прямые связи между отдельными компонентами, 
заставляя их общаться друг с другом через себя.

Применимость: Пожалуй, самое популярное применение Посредника в C#-коде 
— это связь нескольких компонентов GUI одной программы.
*/

interface IMediator
{
    void Notify(object sender, string senderEvent);
    void RegisterComponent(BaseComponent component);
}

class ConcreteMediator : IMediator
{
    AddComponent _addComponent;
    RemoveComponent _removeComponent;
    
    public void Notify(object sender, string senderEvent)
    {
        if (sender == _addComponent) //Отделять по имени к примеру еще
        {
            if (senderEvent == "click")
            {
                Console.WriteLine("Add button clicked");
            }

            if (senderEvent == "help")
            {
                Console.WriteLine("Add button needs help");
            }
        }

        if (sender == _removeComponent)
        {
            Console.WriteLine("Remove button clicked");
        }
    }

    public void RegisterComponent(BaseComponent component)
    {
        if (component is AddComponent addComponent) //Или же проверять по имени компонента
        {
            _addComponent = addComponent;
            addComponent.SetMediator(this);
        }

        if (component is RemoveComponent removeComponent)
        {
            _removeComponent = removeComponent;
            removeComponent.SetMediator(this);
        }
    }
}

abstract class BaseComponent
{
    protected IMediator? _mediator;

    public BaseComponent(IMediator? mediator = null)
    {
        _mediator = mediator;
    }

    public void SetMediator(IMediator mediator) => _mediator = mediator;

    public abstract void Send(string eventDescription);
}

class AddComponent : BaseComponent
{
    public override void Send(string eventDescription)
    {
        _mediator?.Notify(this, eventDescription);
    }
}

class RemoveComponent : BaseComponent
{
    public override void Send(string eventDescription)
    {
        _mediator?.Notify(this, eventDescription);
    }
}

class Demo
{
    public static void Test()
    {
        IMediator mediator = new ConcreteMediator();

        BaseComponent addComponent = new AddComponent();
        BaseComponent removeComponent = new RemoveComponent();

        mediator.RegisterComponent(addComponent);
        mediator.RegisterComponent(removeComponent);

        addComponent.Send("click");
        addComponent.Send("help");
        removeComponent.Send("click");
    }
}

/* 
Отношения с другими паттернами
Цепочка обязанностей, Команда, Посредник и Наблюдатель показывают различные способы работы отправителей запросов с их получателями:

Цепочка обязанностей передаёт запрос последовательно через цепочку потенциальных получателей, 
ожидая, что какой-то из них обработает запрос.

Команда устанавливает косвенную одностороннюю связь от отправителей к получателям.

Посредник убирает прямую связь между отправителями и получателями, 
заставляя их общаться опосредованно, через себя.

Наблюдатель передаёт запрос одновременно всем заинтересованным получателям, 
но позволяет им динамически подписываться или отписываться от таких оповещений.

Посредник и Фасад похожи тем, что пытаются организовать работу множества существующих классов.

1. Фасад создаёт упрощённый интерфейс к подсистеме, не внося в неё никакой добавочной функциональности. 
Сама подсистема не знает о существовании Фасада. Классы подсистемы общаются друг с другом напрямую.

2. Посредник централизует общение между компонентами системы. Компоненты системы знают только о существовании Посредника, у них нет прямого доступа к другим компонентам.
Разница между Посредником и Наблюдателем не всегда очевидна. Чаще всего они выступают как конкуренты, но иногда могут работать вместе.

Цель Посредника — убрать обоюдные зависимости между компонентами системы. 
Вместо этого они становятся зависимыми от самого посредника. С другой стороны, 
цель Наблюдателя — обеспечить динамическую одностороннюю связь, в которой одни объекты косвенно зависят от других.

Довольно популярна реализация Посредника при помощи Наблюдателя. При этом объект посредника будет выступать 
издателем, а все остальные компоненты станут подписчиками и смогут динамически следить за событиями, 
происходящими в посреднике. В этом случае трудно понять, чем же отличаются оба паттерна.

Но Посредник имеет и другие реализации, когда отдельные компоненты жёстко привязаны к объекту посредника. 
Такой код вряд ли будет напоминать Наблюдателя, но всё же останется Посредником.

Напротив, в случае реализации посредника с помощью Наблюдателя представим такую программу, 
в которой каждый компонент системы становится издателем. 
Компоненты могут подписываться друг на друга, в то же время не привязываясь к конкретным классам. 
Программа будет состоять из целой сети Наблюдателей, не имея центрального объекта-Посредника.
*/