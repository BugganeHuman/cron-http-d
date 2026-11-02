namespace cron_http_d;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Net.Http;


public class Program
{
    public static int Interval { get; set; } = 5;
    public static string Endpoint { get; set; } = "";
    public static HttpClient Client = new();

    public static void Main(string[] args)
    {
        if (File.Exists("config.json"))
        {
            string json = File.ReadAllText("config.json");
            using JsonDocument document = JsonDocument.Parse(json);
            int interval = document.RootElement.GetProperty("Interval").GetInt32();
            if (interval > 0)
            {
                Interval = interval;
            }
            Endpoint = document.RootElement.GetProperty("Endpoint").GetString() ?? "";
        }

        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
        builder.Services.AddHostedService<Worker>();
        IHost host = builder.Build();
        host.Run();
    }
}