namespace AaDS.OOP_Patterns.Behavioral.Observer
{

//Также известен как: Издатель-подписчик, Слушатель, Observer

/*
Наблюдатель — это поведенческий паттерн, который позволяет объектам оповещать другие объекты об изменениях своего состояния.

При этом наблюдатели могут свободно подписываться и отписываться от этих оповещений. 
 
Применимость: Наблюдатель можно часто встретить в C# коде, особенно там,
где применяется событийная модель отношений между компонентами.
Наблюдатель позволяет отдельным компонентам реагировать на события, происходящие в других компонентах.

Признаки применения паттерна: Наблюдатель можно определить по механизму подписки
и методам оповещения, которые вызывают компоненты программы.
*/

//Классический пример Издатель-Подписчик
    namespace Classical
    {
        interface IObserver //Подписчик
        {
            void Update(IObservable observable);
        }
        
        interface IObservable //Издатель
        {
            // Присоединяет наблюдателя к издателю.
            void Attach(IObserver observer);

            // Отсоединяет наблюдателя от издателя.
            void Detach(IObserver observer);

            // Уведомляет всех наблюдателей о событии.
            void Notify();
        }

        class Observable : IObservable
        {
            //Бизнес-логика
            public int State { get; set; }

            List<IObserver> _observers = [];

            public void Attach(IObserver observer)
            {
                Console.WriteLine("Observable: Attached an observer.");
                _observers.Add(observer);
            }

            public void Detach(IObserver observer)
            {
                if (_observers.Remove(observer))
                {
                    Console.WriteLine("Observable: Detached an observer.");
                }
            }

            public void Notify()
            {
                Console.WriteLine("Observable: Notifying observers...");

                foreach (var observer in _observers)
                {
                    observer.Update(this);
                }
            }

            public void Work()
            {
                Console.WriteLine("\nSubject: I'm doing something important.");
                State = new Random().Next(0, 10);

                Thread.Sleep(15);

                Console.WriteLine("Observable: My state has just changed to: " + State);
                Notify();
            }
        }

        class ObserverA : IObserver
        {
            public void Update(IObservable observable)
            {
                if (observable is Observable { State: < 3 })
                {
                    Console.WriteLine("ConcreteObserverA: Reacted to the event.");
                }
            }
        }

        class ObserverB : IObserver
        {
            public void Update(IObservable observable)
            {
                if (observable is Observable { State: 0 or >= 2 })
                {
                    Console.WriteLine("ConcreteObserverA: Reacted to the event.");
                }
            }
        }

        class DemoClassic
        {
            public static void Test()
            {
                var subject = new Observable();
                var observerA = new ObserverA();
                subject.Attach(observerA);

                var observerB = new ObserverB();
                subject.Attach(observerB);

                subject.Work();
                subject.Work();

                subject.Detach(observerB);

                subject.Work();
            }
        }
    }

//Версия C# с event
    namespace WithEventWork
    {
        class ObservableArgs
        {
            public int State { get; set; }
            public ObservableArgs(int state)
            {
                State = state;
            }
        }
        class Observable
        {
            public event EventHandler<ObservableArgs> StateChanged;
            
            public int State { get; set; }
            
            public void Work()
            {
                Console.WriteLine("\nSubject: I'm doing something important.");
                State = new Random().Next(0, 10);

                Thread.Sleep(15);

                Console.WriteLine("Observable: My state has just changed to: " + State);
                OnStateChanged();
            }   
            
            // Метод для вызова события StateChanged
            protected virtual void OnStateChanged()
            {
                StateChanged?.Invoke(this, new(State));
            }
        }

        class MyObserver
        {
            public string Name { get; private set; }

            public MyObserver(string name) => Name = name;

            // Метод, который вызывается при получении уведомления об изменении состояния
            public void Update(object? sender, ObservableArgs observable)
            {
                Console.WriteLine($"Observer {Name}: Reacted to the event. Observable state is {observable.State}");
            }
        }

        class Demo
        {
            public static void Test()
            {
                Observable observable = new Observable();

                // Создание подписчиков
                MyObserver observer1 = new MyObserver("Observer 1");
                MyObserver observer2 = new MyObserver("Observer 2");

                // Подписка подписчиков на событие
                observable.StateChanged += observer1.Update;
                observable.StateChanged += observer2.Update;

                // Выполнение работы издателем
                observable.Work();

                // Отписка одного из подписчиков
                observable.StateChanged -= observer2.Update;

                // Еще одна работа издателя
                observable.Work();
                
            }
        }
    }
}



