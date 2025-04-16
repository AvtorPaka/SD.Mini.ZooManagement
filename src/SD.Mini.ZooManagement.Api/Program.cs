using System.Net;

namespace SD.Mini.ZooManagement.Api;

internal sealed class Program
{
    public static async Task Main()
    {
        var builder = Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(webHostBuilder => webHostBuilder.UseStartup<Startup>())
            .ConfigureWebHost(webHostBuilder =>
            {
                webHostBuilder.ConfigureKestrel(serverOptions =>
                    {
                        serverOptions.Listen(IPAddress.Any, 7070);
                    }
                );
            });

        await builder
            .Build()
            .RunAsync();
    }
}