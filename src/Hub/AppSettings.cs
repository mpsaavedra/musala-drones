namespace Musala.Drones.Hub;

public class AppSettings
{
    public Urls Urls { get; set; } = new();
}

public class Urls
{
    public string Https { get; set; } = "https://localhost:7066";
    public string Http { get; set; } = "http://localhost:5102";
}