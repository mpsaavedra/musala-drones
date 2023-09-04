namespace Musala.Drones.Api.Settings;

public class DbSettings
{
    public bool UseInMemory { get; set; } = true;

    public string InMemoryDbName { get; set; } = "MusalaDrones";

    public string PostgreSqlConnectionString { get; set; } = "Host=160.100.10.80;Port=5432;User Id=postgres;Password=postgres;Database=musala-drones";
}