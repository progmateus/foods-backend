namespace Domain
{
  public static class Configuration
  {
    public static string BaseUrl { get; set; } = "http://192.168.15.8:5062";
    public static DatabaseConfiguration Database { get; set; } = new();
    public static RabbitMqConfiguration RabbitMq { get; set; } = new();

    public class DatabaseConfiguration
    {
      public string ConnectionString { get; set; } = string.Empty;
    }

    public class RabbitMqConfiguration
    {
      public string Uri { get; set; } = "amqp://localhost:5672";
      public string Username { get; set; } = "guest";
      public string Password { get; set; } = "guest";
    }
  }
}



