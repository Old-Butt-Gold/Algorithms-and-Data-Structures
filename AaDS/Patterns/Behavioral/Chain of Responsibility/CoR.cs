namespace AaDS.Patterns.Behavioral.Chain_of_Responsibility;

/*
Цепочка обязанностей — это поведенческий паттерн, позволяющий передавать запрос 
по цепочке потенциальных обработчиков, пока один из них не обработает запрос.

Избавляет от жёсткой привязки отправителя запроса к его получателю, 
позволяя выстраивать цепь из различных обработчиков динамически. 
 
Применимость: Паттерн встречается в C# не так уж часто, 
так как для его применения нужна цепь объектов, например, связанный список.

Признаки применения паттерна: Цепочку обязанностей можно определить по спискам 
обработчиков или проверок, через которые пропускаются запросы. 
Особенно если порядок следования обработчиков важен.
 
Также как пример возвращать bool при авторизации пользователя на сервер, цепочка
почта -> логин -> пароль, и при true выполнять что-то
 */

interface IHandler
{
    IHandler SetNext(IHandler handler);
    void Handle(object? request);
}

abstract class BaseHandler : IHandler
{
    IHandler? _nextHandler;
    
    public IHandler SetNext(IHandler handler)
    {
        _nextHandler = handler;

        return handler;
    }

    public virtual void Handle(object? request)
    {
        if (_nextHandler != null)
        {
            _nextHandler.Handle(request);
        }
        else
        {
            Console.WriteLine("Не удалось обработать запрос");
        }
    }
}

class AccessRequest
{
    public bool IsAdmin { get; set; }
    public bool IsUser { get; set; }
    public bool IsGuest { get; set; }
}

class AdminHandler : BaseHandler
{
    public override void Handle(object? request)
    {
        if (request is AccessRequest { IsAdmin: true })
        {
            Console.WriteLine("Доступ разрешен администратору.");
        }
        else
        {
            base.Handle(request);
        }
    }
}

class UserHandler : BaseHandler
{
    public override void Handle(object? request)
    {
        if (request is AccessRequest { IsUser: true })
        {
            Console.WriteLine("Доступ разрешен пользователю.");
        }
        else
        {
            base.Handle(request);
        }
    }
}

class GuestHandler : BaseHandler
{
    public override void Handle(object? request)
    {
        if (request is AccessRequest { IsGuest: true })
        {
            Console.WriteLine("Доступ разрешен гостю.");
        }
        else
        {
            base.Handle(request);
        }
    }
}

class Demo
{
    public static void Test()
    {
        var adminHandler = new AdminHandler();
        var userHandler = new UserHandler();
        var guestHandler = new GuestHandler();

        // Устанавливаем цепочку обработчиков
        adminHandler.SetNext(userHandler).SetNext(guestHandler);

        // Создаем запросы на доступ с разными правами
        var request1 = new AccessRequest { IsAdmin = true };
        var request2 = new AccessRequest { IsUser = true };
        var request3 = new AccessRequest { IsGuest = true };
        var request4 = new AccessRequest { IsAdmin = false, IsUser = false, IsGuest = false }; // Нет прав

        // Посылаем запросы на обработку
        adminHandler.Handle(request1);
        adminHandler.Handle(request2);
        adminHandler.Handle(request3);
        adminHandler.Handle(request4);
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

Цепочку обязанностей часто используют вместе с Компоновщиком. 
В этом случае запрос передаётся от дочерних компонентов к их родителям.

Обработчики в Цепочке обязанностей могут быть выполнены в виде Команд. 
В этом случае множество разных операций может быть выполнено 
над одним и тем же контекстом, коим является запрос.

Но есть и другой подход, в котором сам запрос является Командой, 
посланной по цепочке объектов. В этом случае одна и та же операция может быть 
выполнена над множеством разных контекстов, представленных в виде цепочки.

Цепочка обязанностей и Декоратор имеют очень похожие структуры. 
Оба паттерна базируются на принципе рекурсивного выполнения операции через серию связанных объектов. 
Но есть и несколько важных отличий.

Обработчики в Цепочке обязанностей могут выполнять произвольные действия, 
независимые друг от друга, а также в любой момент прерывать дальнейшую передачу по цепочке. 
С другой стороны Декораторы расширяют какое-то определённое действие, 
не ломая интерфейс базовой операции и не прерывая выполнение остальных декораторов.
*/