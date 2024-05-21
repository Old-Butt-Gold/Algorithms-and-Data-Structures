namespace AaDS.OOP_Patterns.Structural.Bridge;

/*
Мост — это структурный паттерн, который разделяет бизнес-логику или 
большой класс на несколько отдельных иерархий, которые потом можно 
развивать отдельно друг от друга.

Одна из этих иерархий (абстракция) получит ссылку на объекты другой иерархии 
(реализация) и будет делегировать им основную работу. 
Благодаря тому, что все реализации будут следовать общему интерфейсу, 
их можно будет взаимозаменять внутри абстракции.

Применимость: Паттерн Мост особенно полезен когда вам приходится делать кросс-платформенные приложения, 
поддерживать несколько типов баз данных или работать с 
разными поставщиками похожего API (например, cloud-сервисы, социальные сети и т. д.)

Признаки применения паттерна: Если в программе чётко выделены классы «управления» 
и несколько видов классов «платформ», причём управляющие объекты делегируют выполнение платформам, 
то можно сказать, что у вас используется Мост.
*/

public interface IDevice
{
    bool IsEnabled();
    void Enable();
    void Disable();
    int GetVolume();
    void SetVolume(int percent);
    int GetChannel();
    void SetChannel(int channel);
    void PrintStatus();
} //Реализация (приборы)

public class Radio : IDevice
{
    bool On { get; set; }
    int Volume { get; set; } = 30;
    int Channel { get; set; } = 1;

    public bool IsEnabled() => On;

    public void Enable() => On = true;

    public void Disable() => On = false;

    public int GetVolume() => Volume;

    public void SetVolume(int percent)
    {
        Volume = percent;
        switch (Volume)
        {
            case > 100:
                Volume = 100;
                break;
            case < 0:
                Volume = 0;
                break;
        }
    }

    public int GetChannel() => Channel;

    public void SetChannel(int channel) => Channel = channel;

    public void PrintStatus()
    {
        Console.WriteLine("------------------------------------");
        Console.WriteLine("| I'm radio.");
        Console.WriteLine("| I'm " + (On ? "enabled" : "disabled"));
        Console.WriteLine("| Current volume is " + Volume + "%");
        Console.WriteLine("| Current channel is " + Channel);
        Console.WriteLine("------------------------------------\n");
    }
}

public class Tv : IDevice
{
    bool On { get; set; }
    int Volume { get; set; } = 30;
    int Channel { get; set; } = 1;

    public bool IsEnabled() => On;

    public void Enable() => On = true;

    public void Disable() => On = false;

    public int GetVolume() => Volume;

    public void SetVolume(int percent)
    {
        Volume = percent;
        switch (Volume)
        {
            case > 100:
                Volume = 100;
                break;
            case < 0:
                Volume = 0;
                break;
        }
    }

    public int GetChannel() => Channel;

    public void SetChannel(int channel) => Channel = channel;

    public void PrintStatus()
    {
        Console.WriteLine("------------------------------------");
        Console.WriteLine("| I'm TV set.");
        Console.WriteLine("| I'm " + (On ? "enabled" : "disabled"));
        Console.WriteLine("| Current volume is " + Volume + "%");
        Console.WriteLine("| Current channel is " + Channel);
        Console.WriteLine("------------------------------------\n");
    }
}

public interface IRemote
{
    void Power();
    void VolumeDown();
    void VolumeUp();
    void ChannelDown();
    void ChannelUp();
} //Абстракция (пульты управления)

public class BasicRemote : IRemote
{
    public IDevice Device { get; set; }
    
    public BasicRemote(IDevice device) => Device = device;

    public void Power()
    {
        Console.WriteLine("Remote: power toggle");
        if (Device.IsEnabled())
        {
            Device.Disable();
        }
        else
        {
            Device.Enable();
        }
    }

    public void VolumeDown()
    {
        Console.WriteLine("Remote: volume down");
        Device.SetVolume(Device.GetVolume() - 1);
    }

    public void VolumeUp()
    {
        Console.WriteLine("Remote: volume up");
        Device.SetVolume(Device.GetVolume() + 1);
    }

    public void ChannelDown()
    {
        Console.WriteLine("Remote: channel down");
        Device.SetChannel(Device.GetChannel() - 1);
    }

    public void ChannelUp()
    {
        Console.WriteLine("Remote: channel up");
        Device.SetChannel(Device.GetChannel() + 1);
    }
}

public class AdvancedRemote : BasicRemote
{
    public AdvancedRemote(IDevice device) : base(device)
    { }

    public void Mute()
    {
        Console.WriteLine("Remote: mute");
        Device.SetVolume(0);
    }
}

public class Demo
{
    public static void Test()
    {
        TestDevice(new Tv());
        TestDevice(new Radio());
    }
    
    public static void TestDevice(IDevice device) {
        Console.WriteLine("Tests with basic remote.");
        BasicRemote basicRemote = new BasicRemote(device);
        basicRemote.Power();
        device.PrintStatus();

        Console.WriteLine("Tests with advanced remote.");
        AdvancedRemote advancedRemote = new AdvancedRemote(device);
        advancedRemote.Power();
        advancedRemote.Mute();
        device.PrintStatus();
    }
}

/*
Отношения с другими паттернами
Мост проектируют загодя, чтобы развивать большие части приложения отдельно друг от друга. 
Адаптер применяется постфактум, чтобы заставить несовместимые классы работать вместе.

Мост, Стратегия и Состояние (а также слегка и Адаптер) имеют схожие структуры классов — 
все они построены на принципе «композиции», то есть делегирования работы другим объектам. 
Тем не менее, они отличаются тем, что решают разные проблемы. 
Помните, что паттерны — это не только рецепт построения кода определённым образом, 
но и описание проблем, которые привели к данному решению.

Абстрактная фабрика может работать совместно с Мостом. Это особенно полезно, если у вас есть абстракции, которые могут работать только с некоторыми из реализаций. В этом случае фабрика будет определять типы создаваемых абстракций и реализаций.

Паттерн Строитель может быть построен в виде Моста: 
директор будет играть роль абстракции, а строители — реализации.
*/
