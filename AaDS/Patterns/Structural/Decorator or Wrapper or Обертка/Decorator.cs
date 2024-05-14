using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace AaDS.Patterns.Structural.Decorator_or_Wrapper_or_Обертка;

/*
Декоратор — это структурный паттерн, 
который позволяет добавлять объектам новые поведения на лету, 
помещая их в объекты-обёртки.

Декоратор позволяет оборачивать объекты бесчисленное количество раз благодаря 
тому, что и обёртки, и реальные оборачиваемые объекты имеют общий интерфейс. 
 
Применимость: Паттерн можно часто встретить в C#-коде, 
особенно в коде, работающем с потоками данных.

Признаки применения паттерна: Декоратор можно распознать по создающим методам, 
которые принимают в параметрах объекты того же абстрактного типа или интерфейса, что и текущий класс.
 
Адаптер предоставляет совершенно другой интерфейс для доступа
к существующему объекту. С другой стороны, при использовании паттерна
Декоратор интерфейс либо остается прежним, либо расширяется. 
Причём Декоратор поддерживает рекурсивную вложенность, 
чего не скажешь об Адаптере.
*/

//Используются в большинстве своем с потоками данных

/* Применимость

    Когда вам нужно добавлять обязанности объектам на лету, незаметно для кода, который их использует.

    Когда нельзя расширить обязанности объекта с помощью наследования.
 
*/

interface IDataSource
{
    void WriteData(Stream data);

    Stream ReadData();
}

class FileDataSource : IDataSource
{
    string _fileName;

    public FileDataSource(string fileName) => _fileName = fileName;
    
    public void WriteData(Stream data)
    {
        using FileStream fileStream = new FileStream(_fileName, FileMode.Create);
        data.CopyTo(fileStream);
    }

    public Stream ReadData()
    {
        if (!File.Exists(_fileName))
        {
            throw new FileNotFoundException("File not found", _fileName);
        }

        return new FileStream(_fileName, FileMode.Open, FileAccess.Read);
    }
}

class DataSourceDecorator : IDataSource //Базовый декоратор
{
    private IDataSource _wrappee;

    public DataSourceDecorator(IDataSource source) => _wrappee = source;
    
    public virtual void WriteData(Stream data) => _wrappee.WriteData(data);

    public virtual Stream ReadData() => _wrappee.ReadData();
}

class EncryptionDecorator : DataSourceDecorator
{
    private readonly Aes _aes = Aes.Create();

    public EncryptionDecorator(IDataSource source) : base(source)
    {
        _aes.Key = Encoding.UTF8.GetBytes("0123456789abcdef");
        _aes.IV = Encoding.UTF8.GetBytes("abcdef9876543210");
    }

    public override void WriteData(Stream data)
    {
        base.WriteData(Encode(data));
    }

    public override Stream ReadData()
    {
        return Decode(base.ReadData());
    }
    
    Stream Encode(Stream data)
    {
        MemoryStream encryptedStream = new MemoryStream();
        using (var cryptoStream = new CryptoStream(encryptedStream, _aes.CreateEncryptor(), CryptoStreamMode.Write, true))
        {
            data.CopyTo(cryptoStream);
        }
        encryptedStream.Seek(0, SeekOrigin.Begin);
        return encryptedStream;
    }

    Stream Decode(Stream data)
    {
        MemoryStream decryptedStream = new MemoryStream();
        using (var cryptoStream = new CryptoStream(data, _aes.CreateDecryptor(), CryptoStreamMode.Read, true))
        {
            cryptoStream.CopyTo(decryptedStream);
        }
        decryptedStream.Seek(0, SeekOrigin.Begin);
        return decryptedStream;
    }
}

class CompressionDecorator : DataSourceDecorator
{
    public CompressionDecorator(IDataSource source) : base(source) 
    { }

    public override void WriteData(Stream data)
    {
        base.WriteData(Compress(data));
    }

    private Stream Compress(Stream data)
    {
        MemoryStream compressedStream = new MemoryStream();
        using (var gzipStream = new GZipStream(compressedStream, CompressionMode.Compress, true))
        {
            data.CopyTo(gzipStream);
        }
        compressedStream.Seek(0, SeekOrigin.Begin);
        return compressedStream;
    }

    public override Stream ReadData() => Decompress(base.ReadData());

    private Stream Decompress(Stream data)
    {
        MemoryStream decompressedStream = new MemoryStream();
        using (var gzipStream = new GZipStream(data, CompressionMode.Decompress))
        {
            gzipStream.CopyTo(decompressedStream);
        }
        decompressedStream.Seek(0, SeekOrigin.Begin);
        return decompressedStream;
    }
}

class Client
{
    public static void Demo()
    {
        string salaryRecords = "Name,Salary\nJohn Smith,100000\nSteven Jobs,912000";

        DataSourceDecorator encoded = 
            new CompressionDecorator(
                new EncryptionDecorator(
                    new FileDataSource("D://OutputDemo.txt")));
        
        byte[] salaryBytes = Encoding.UTF8.GetBytes(salaryRecords);
        MemoryStream salaryStream = new MemoryStream(salaryBytes);
        
        encoded.WriteData(salaryStream);
        
        IDataSource plain = new FileDataSource("D://OutputDemo.txt");

        Console.WriteLine("- Input ----------------");
        Console.WriteLine(salaryRecords);
        Console.WriteLine("- Encoded --------------");
        Console.WriteLine(ReadStreamToString(plain.ReadData()));
        Console.WriteLine("- Decoded --------------");
        Console.WriteLine(ReadStreamToString(encoded.ReadData()));
    }

    private static string ReadStreamToString(Stream stream)
    {
        using StreamReader reader = new StreamReader(stream, Encoding.UTF8);
        return reader.ReadToEnd();
    }
}

/*
Отношения с другими паттернами
Адаптер предоставляет совершенно другой интерфейс для доступа к существующему объекту. 
С другой стороны, при использовании паттерна Декоратор интерфейс либо остается прежним, 
либо расширяется. Причём Декоратор поддерживает рекурсивную вложенность, чего не скажешь об Адаптере.

С Адаптером вы получаете доступ к существующему объекту через другой интерфейс. 
Используя Заместитель, интерфейс остается неизменным. 
Используя Декоратор, вы получаете доступ к объекту через расширенный интерфейс.

Цепочка обязанностей и Декоратор имеют очень похожие структуры. 
Оба паттерна базируются на принципе рекурсивного выполнения операции через серию связанных объектов. 
Но есть и несколько важных отличий:

Обработчики в Цепочке обязанностей могут выполнять произвольные действия, 
независимые друг от друга, а также в любой момент прерывать дальнейшую передачу по цепочке. 
С другой стороны Декораторы расширяют какое-то определённое действие, 
не ломая интерфейс базовой операции и не прерывая выполнение остальных декораторов.

Компоновщик и Декоратор имеют похожие структуры классов из-за того, 
что оба построены на рекурсивной вложенности. Она позволяет связать в одну 
структуру бесконечное количество объектов.

Декоратор оборачивает только один объект, а узел Компоновщика может иметь много детей. 
Декоратор добавляет вложенному объекту новую функциональность, а Компоновщик 
не добавляет ничего нового, но «суммирует» результаты всех своих детей.

Но они могут и сотрудничать: Компоновщик может использовать Декоратор, 
чтобы переопределить функции отдельных частей дерева компонентов.

Архитектура, построенная на Компоновщиках и Декораторах, часто может быть улучшена за счёт 
внедрения Прототипа. Он позволяет клонировать сложные структуры объектов, 
а не собирать их заново.

Стратегия меняет поведение объекта «изнутри», а Декоратор изменяет его «снаружи».

Декоратор и Заместитель имеют схожие структуры, но разные назначения. 
Они похожи тем, что оба построены на принципе композиции и делегируют работу другим объектам. 
Паттерны отличаются тем, что Заместитель сам управляет жизнью сервисного объекта, 
а обёртывание Декораторов контролируется клиентом.
*/