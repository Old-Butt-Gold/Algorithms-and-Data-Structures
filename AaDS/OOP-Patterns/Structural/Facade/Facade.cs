namespace AaDS.OOP_Patterns.Structural.Facade;

/*

Применимость: Паттерн часто встречается в клиентских приложениях, 
написанных на C#, которые используют классы-фасады
 для упрощения работы со сложными библиотеки или API.

Признаки применения паттерна: Фасад угадывается в классе,
который имеет простой интерфейс, но делегирует основную часть
работы другим классам. Чаще всего, фасады сами следят
за жизненным циклом объектов сложной системы.

Применимость: Паттерн часто встречается в клиентских приложениях, 
написанных на C#, которые используют классы-фасады для упрощения работы со сложными библиотеки или API.

Признаки применения паттерна: Фасад угадывается в классе, который имеет простой интерфейс, 
но делегирует основную часть работы другим классам. 
Чаще всего, фасады сами следят за жизненным циклом объектов сложной системы.
*/

class VideoFile
{ 
    public string Name { get; }
    public string CodecType { get; }

    public VideoFile(string name)
    {
        Name = name;
        CodecType = name.Substring(name.LastIndexOf('.') + 1);
    }
}

interface ICodec
{
    string type { get; }
}

class MPEG4CompressionCodec : ICodec
{
    public string type { get; } = "mp4";
}

class OggCompressionCodec : ICodec 
{
    public string type { get; } = "ogg";
}

class CodecFactory
{
    public static ICodec Extract(VideoFile file)
    {
        string type = file.CodecType;
        if (type.Equals("mp4"))
        {
            Console.WriteLine("CodecFactory: extracting mpeg audio...");
            return new MPEG4CompressionCodec();
        }
        else
        {
            Console.WriteLine("CodecFactory: extracting ogg audio...");
            return new OggCompressionCodec();
        }
    }
}

class BitrateReader
{
    public static VideoFile Read(VideoFile file, ICodec codec)
    {
        Console.WriteLine("BitrateReader: reading file...");
        return file;
    }

    public static VideoFile Convert(VideoFile buffer, ICodec codec)
    {
        Console.WriteLine("BitrateReader: writing file...");
        return buffer;
    }
}

class AudioMixer {
    public FileInfo Fix(VideoFile result){
        Console.WriteLine("AudioMixer: fixing audio...");
        return new FileInfo("tmp");
    }
}


class VideoConversionFacade //фасад
{
    public FileInfo ConvertVideo(string fileName, string format)
    {
        Console.WriteLine("VideoConversionFacade: conversion started.");
        VideoFile file = new VideoFile(fileName);
        ICodec sourceCodec = CodecFactory.Extract(file);
        ICodec destinationCodec = format.Equals("mp4") 
            ? new MPEG4CompressionCodec() 
            : new OggCompressionCodec();
        VideoFile buffer = BitrateReader.Read(file, sourceCodec);
        VideoFile intermediateResult = BitrateReader.Convert(buffer, destinationCodec);
        FileInfo result = new AudioMixer().Fix(intermediateResult);
        Console.WriteLine("VideoConversionFacade: conversion completed.");
        return result;
    }
}

public class Demo
{
    public static void Test()
    {
        VideoConversionFacade converter = new VideoConversionFacade();
        FileInfo mp4Video = converter.ConvertVideo("youtubevideo.ogg", "mp4");
    }
}

/*
Отношения с другими паттернами
Фасад задаёт новый интерфейс, тогда как Адаптер повторно использует старый. Адаптер оборачивает только один класс, 
а Фасад оборачивает целую подсистему. Кроме того, Адаптер позволяет двум существующим интерфейсам работать сообща, 
вместо того, чтобы задать полностью новый.

Абстрактная фабрика может быть использована вместо Фасада для того, 
чтобы скрыть платформо-зависимые классы.

Легковес показывает, как создавать много мелких объектов, а Фасад показывает, 
как создать один объект, который отображает целую подсистему.

Посредник и Фасад похожи тем, что пытаются организовать работу множества 
существующих классов.

Фасад создаёт упрощённый интерфейс к подсистеме, не внося в неё никакой добавочной функциональности. 
Сама подсистема не знает о существовании Фасада. Классы подсистемы общаются друг с другом напрямую.

Посредник централизует общение между компонентами системы. 
Компоненты системы знают только о существовании Посредника, 
у них нет прямого доступа к другим компонентам.

Фасад можно сделать Одиночкой, так как обычно нужен только один объект-фасад.

Фасад похож на Заместитель тем, что замещает сложную подсистему и может сам её инициализировать. 
Но в отличие от Фасада, Заместитель имеет тот же интерфейс,
что его служебный объект, благодаря чему их можно взаимозаменять.
*/