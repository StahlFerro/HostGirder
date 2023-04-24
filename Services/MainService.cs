using System.Text;
using Microsoft.Extensions.Logging;

namespace HostGirder;

public interface IMainService : ISingletonService { }
public class MainService : IMainService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger _logger;
    private readonly ElectricalService _electricalService;

    public MainService(IServiceProvider serviceProvider, ILogger<MainService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _electricalService = (ElectricalService)(_serviceProvider.GetService(typeof(IElectricalService)) ?? throw new ArgumentNullException());
    }

    public string GetSystemEval()
    {
        _logger.LogDebug("GetSystemEval");
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("\n--- System Eval ---");
        sb.Append(_electricalService.GetStatus());
        return sb.ToString();
    }
}