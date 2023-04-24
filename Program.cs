using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace HostGirder;

public class Program
{
    public static async Task Main(string[] args)
    {
        var hostBuilder = Host.CreateApplicationBuilder(args);

        AddConfiguration(ref hostBuilder, args);
        AddLogging(ref hostBuilder);
        AddOptions(ref hostBuilder);
        AddServices(ref hostBuilder);

        var host = hostBuilder.Build();
        EvaluateServices(host.Services);

        await host.RunAsync();
    }

    public static void AddConfiguration(ref HostApplicationBuilder hostBuilder, string[] args)
    {
        hostBuilder.Configuration.Sources.Clear();
        var configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddCommandLine(args)
            .AddJsonFile("./appsettings.json")
            .Build();
        hostBuilder.Configuration.AddConfiguration(configuration);
    }

    public static void AddOptions(ref HostApplicationBuilder hostBuilder)
    {
        hostBuilder.Services.Configure<MainOptions>(hostBuilder.Configuration.GetSection(nameof(MainOptions)));
        hostBuilder.Services.Configure<ElectricalOptions>(hostBuilder.Configuration.GetSection(nameof(ElectricalOptions)));
    }

    public static void AddServices(ref HostApplicationBuilder hostBuilder)
    {
        hostBuilder.Services.AddSingleton<IMainService, MainService>();
        hostBuilder.Services.AddSingleton<IElectricalService, ElectricalService>();
    }

    public static void AddLogging(ref HostApplicationBuilder hostBuilder)
    {
        hostBuilder.Logging.ClearProviders();
        var nlogOptions = new NLogProviderOptions
        {
             ReplaceLoggerFactory = true,
        };
        var config = new ConfigurationBuilder()
            // .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
        hostBuilder.Logging.SetMinimumLevel(LogLevel.Trace);
        hostBuilder.Logging.AddNLog(config);
    }

    public static void EvaluateServices(IServiceProvider serviceProvider)
    {
        // var serviceProvider = serviceCollection.BuildServiceProvider();
        var MainService = (MainService)(serviceProvider.GetService<IMainService>() ?? throw new ArgumentNullException());
        var logger = serviceProvider.GetService<ILogger<Program>>() ?? throw new ArgumentNullException();
        logger.LogDebug(MainService.GetSystemEval());
        // Console.WriteLine(MainService.GetSystemEval());
    }
}