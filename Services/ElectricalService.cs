using System.Text;
using Microsoft.Extensions.Options;

namespace HostGirder;

public interface IElectricalService : ISingletonService { }
public class ElectricalService : IElectricalService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ElectricalOptions _options;

    public ElectricalService(IServiceProvider serviceProvider, IOptions<ElectricalOptions> options)
    {
        _serviceProvider = serviceProvider;
        _options = options.Value;
    }

    public string GetStatus()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Battery Mode: {_options.BatteryMode}");
        return sb.ToString();
    }
}