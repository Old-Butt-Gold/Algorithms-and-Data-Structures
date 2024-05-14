namespace AaDS.Patterns.Behavioral.Visitor;

/*

Посетитель — это поведенческий паттерн, который позволяет добавить новую операцию
для целой иерархии классов, не изменяя код этих классов.

P.S. почему нельзя передавать тип – Посетитель и Double Dispatch 
https://refactoring.guru/ru/design-patterns/visitor-double-dispatch

*/

// Интерфейс Посетителя объявляет набор методов посещения, соответствующих
// классам компонентов. Сигнатура метода посещения позволяет посетителю
// определить конкретный класс компонента, с которым он имеет дело.
interface IVisitor
{
    //В идеале их можно назвать по разному, а можно назвать одинаково, чтобы работала перегрузка методов
    void VisitAcc(Person acc);
    void VisitAcc(Company acc);
}
 
// сериализатор в HTML
class HtmlVisitor : IVisitor
{
    public void VisitAcc(Person acc)
    {
        string result = "<table><tr><td>Свойство<td><td>Значение</td></tr>";
        result += "<tr><td>Name<td><td>" + acc.Name + "</td></tr>";
        result += "<tr><td>Number<td><td>" + acc.Number + "</td></tr></table>";
        Console.WriteLine(result);
    }
 
    public void VisitAcc(Company acc)
    {
        string result = "<table><tr><td>Свойство<td><td>Значение</td></tr>";
        result += "<tr><td>Name<td><td>" + acc.Name + "</td></tr>";
        result += "<tr><td>RegNumber<td><td>" + acc.RegNumber + "</td></tr>";
        result += "<tr><td>Number<td><td>" + acc.Number + "</td></tr></table>";
        Console.WriteLine(result);
    }
}
 
// сериализатор в XML
class XmlVisitor : IVisitor
{
    public void VisitAcc(Person acc)
    {
        string result = "<Person><Name>"+acc.Name+"</Name>"+
            "<Number>"+acc.Number+"</Number><Person>";
        Console.WriteLine(result);
    }
 
    public void VisitAcc(Company acc)
    {
        string result = "<Company><Name>" + acc.Name + "</Name>" + 
            "<RegNumber>" + acc.RegNumber + "</RegNumber>" + 
            "<Number>" + acc.Number + "</Number><Company>";
        Console.WriteLine(result);
    }
}
 
class Bank
{
    List<IAccount> accounts = [];
    public void Add(IAccount acc) => accounts.Add(acc);
    public void Remove(IAccount acc) => accounts.Remove(acc);
    public void Accept(IVisitor visitor) 
        => accounts.ForEach(x => x.Accept(visitor));
}
 
// Интерфейс Аккаунта объявляет метод Accept, который в качестве аргумента
// может получать любой объект, реализующий интерфейс посетителя.
interface IAccount
{
    void Accept(IVisitor visitor);
}
 
// Каждый Конкретный Компонент должен реализовать метод Accept таким
// образом, чтобы он вызывал метод посетителя, соответствующий классу
// компонента.
class Person : IAccount
{
    public string Name { get; set; }
    public string Number { get; set; }
 
    public void Accept(IVisitor visitor) => visitor.VisitAcc(this);
}
 
class Company : IAccount
{
    public string Name { get; set; }
    public string RegNumber { get; set; }
    public string Number { get; set; }
 
    public void Accept(IVisitor visitor) => visitor.VisitAcc(this);
}

class Demo
{
    public static void Test()
    {
        var bank = new Bank();
        bank.Add(new Person(){Name = "Алексеев", Number = "123"});
        bank.Add(new Company(){Name = "Microsoft", Number = "Mf567"});
        bank.Accept(new HtmlVisitor());
        bank.Accept(new XmlVisitor());
    }
}

/*
Отношения с другими паттернами
Посетитель можно рассматривать как расширенный аналог Команды, 
который способен работать сразу с несколькими видами получателей.

Вы можете выполнить какое-то действие над всем деревом Компоновщика при помощи Посетителя.

Посетитель можно использовать совместно с Итератором. 
Итератор будет отвечать за обход структуры данных, 
а Посетитель — за выполнение действий над каждым её компонентом.
*/