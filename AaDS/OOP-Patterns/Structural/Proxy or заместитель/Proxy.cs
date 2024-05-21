namespace AaDS.OOP_Patterns.Structural.Proxy_or_заместитель;

/*

Заместитель — это объект, который выступает прослойкой между клиентом и реальным сервисным объектом. 
Заместитель получает вызовы от клиента, выполняет свою функцию 
(контроль доступа, кеширование, изменение запроса и прочее), а затем передаёт вызов сервисному объекту.

Заместитель имеет тот же интерфейс, что и реальный объект, 
поэтому для клиента нет разницы — работать через заместителя или напрямую.

Применимость: Паттерн Заместитель применяется в C# коде тогда, когда надо заменить настоящий объект 
его суррогатом, причём незаметно для клиентов настоящего объекта. Это позволит выполнить какие-то добавочные поведения 
до или после основного поведения настоящего объекта.

Признаки применения паттерна: Класс заместителя чаще всего делегирует всю настоящую работу 
своему реальному объекту. Заместители часто сами следят за жизненным циклом своего реального объекта.

P.S. позволяет создавать объект сервиса тогда, когда необходимо

*/

class Video
{
    public string Name { get; set; }
    public string Id { get; set; }
    public Video(string id, string name) => (Name, Id) = (name, id);
}

interface IThirdPartyYoutubeLib
{
    Dictionary<string, Video> PopularVideos();

    Video GetVideo(string videoId);
}

class ThirdPartyYouTubeClass : IThirdPartyYoutubeLib
{
    public Dictionary<string, Video> PopularVideos()
    {
        ConnectToServer("http://www.youtube.com");
        return GetRandomVideos();
    }

    public Video GetVideo(string videoId)
    {
        ConnectToServer("http://www.youtube.com/" + videoId);
        return GetSomeVideo(videoId);
    }

    int Random(int min, int max) => System.Random.Shared.Next(min, max);

    void ExperienceNetworkLatency()
    {
        int randomLatency = System.Random.Shared.Next(5, 10);
        for (int i = 0; i < randomLatency; i++)
        {
            try
            {
                Thread.Sleep(100);
            }
            catch (ThreadInterruptedException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    void ConnectToServer(string server)
    {
        Console.WriteLine("Connecting to " + server + "... ");
        ExperienceNetworkLatency();
        Console.WriteLine("Connected!");
    }
    
    Dictionary<string, Video> GetRandomVideos() {
        Console.WriteLine("Downloading populars... ");

        ExperienceNetworkLatency();
        Dictionary<string, Video> dictionary = new();
        dictionary.Add("catzzzzzzzzz", new Video("sadgahasgdas", "Catzzzz.avi"));
        dictionary.Add("mkafksangasj", new Video("mkafksangasj","Dog play with ball.mp4"));
        dictionary.Add("dancesvideoo", new Video("asdfas3ffasd", "Dancing video.mpq"));
        dictionary.Add("dlsdk5jfslaf", new Video("dlsdk5jfslaf", "Barcelona vs RealM.mov"));
        dictionary.Add("3sdfgsd1j333", new Video("3sdfgsd1j333", "Programing lesson#1.avi"));

        Console.WriteLine("Done!");
        return dictionary;
    }

    Video GetSomeVideo(string videoId) {
        Console.WriteLine("Downloading video... ");

        ExperienceNetworkLatency();
        Video video = new Video(videoId, "Some video title");

        Console.WriteLine("Done!" + "\n");
        return video;
    }
}

class YoutubeCacheProxy : IThirdPartyYoutubeLib
{
    private readonly Lazy<ThirdPartyYouTubeClass> _youtubeService;
    Dictionary<string, Video> _cachePopular = new();
    Dictionary<string, Video> _cacheAll = new();

    //public YoutubeCacheProxy(IThirdPartyYoutubeLib youtubeService)
    //Если заносится интерфейс, то отложенную инициализацию нет смысла создавать
    public YoutubeCacheProxy()
    {
        _youtubeService = new();
    }

    public Dictionary<string, Video> PopularVideos()
    {
        if (_cachePopular.Count == 0)
        {
            _cachePopular = _youtubeService.Value.PopularVideos();
        }
        else
        {
            Console.WriteLine("Retrieved list from cache.");
        }

        return _cachePopular;
    }

    public Video GetVideo(string videoId)
    {
        _cacheAll.TryGetValue(videoId, out var video);
        if (video == null)
        {
            video = _youtubeService.Value.GetVideo(videoId);
            _cacheAll[videoId] = video;
        }
        else
        {
            Console.WriteLine("Retrieved video '" + videoId + "' from cache.");
        }

        return video;
    }
    
    public void Reset() {
        _cachePopular.Clear();
        _cacheAll.Clear();
    }
}

class YoutubeDownloader
{
    IThirdPartyYoutubeLib _api;

    public YoutubeDownloader(IThirdPartyYoutubeLib api) => _api = api;

    public void RenderVideoPage(string videoId)
    {
        var video = _api.GetVideo(videoId);
        Console.WriteLine("\n-------------------------------");
        Console.WriteLine("Video page (imagine fancy HTML)");
        Console.WriteLine("ID: " + video.Id);
        Console.WriteLine("Title: " + video.Name);
        Console.WriteLine("-------------------------------\n");
    }

    public void RenderPopularVideos()
    {
        var dictionary = _api.PopularVideos();
        Console.WriteLine("\n-------------------------------");
        Console.WriteLine("Most popular videos on YouTube (imagine fancy HTML)");
        foreach (Video video in dictionary.Values) {
            Console.WriteLine("ID: " + video.Id + " / Title: " + video.Name);
        }
    }
}

class Demo
{
    public static void Test()
    {
        YoutubeDownloader naiveDownloader = new (new ThirdPartyYouTubeClass());
        YoutubeDownloader smartDownloader = new (new YoutubeCacheProxy());

        long naive = test(naiveDownloader);
        long smart = test(smartDownloader);
        Console.WriteLine("Time saved by caching proxy: " + (naive - smart) + "ms");
    }

    public static long test(YoutubeDownloader downloader)
    {
        long startTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        // User behavior in our app:
        downloader.RenderPopularVideos();
        downloader.RenderVideoPage("catzzzzzzzzz");
        downloader.RenderPopularVideos();
        downloader.RenderVideoPage("dancesvideoo");
        // Users might visit the same page quite often.
        downloader.RenderVideoPage("catzzzzzzzzz");
        downloader.RenderVideoPage("someothervid");

        long estimatedTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - startTime;
        Console.WriteLine("Time elapsed: " + estimatedTime + "ms\n");
        return estimatedTime;
    }
}

/*
Отношения с другими паттернами
С Адаптером вы получаете доступ к существующему объекту через другой интерфейс.
Используя Заместитель, интерфейс остается неизменным. 
Используя Декоратор, вы получаете доступ к объекту через расширенный интерфейс.

Фасад похож на Заместитель тем, что замещает сложную подсистему и может сам её инициализировать. 
Но в отличие от Фасада, Заместитель имеет тот же интерфейс, что его служебный объект, 
благодаря чему их можно взаимозаменять.

Декоратор и Заместитель имеют схожие структуры, но разные назначения. 
Они похожи тем, что оба построены на принципе композиции и делегируют работу другим объектам. 
Паттерны отличаются тем, что Заместитель сам управляет жизнью сервисного объекта, 
а обёртывание Декораторов контролируется клиентом.
*/

