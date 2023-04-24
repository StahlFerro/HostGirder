using Microsoft.Extensions.DependencyInjection;

namespace HostGirder;
public interface IBaseService
{
    ServiceLifetime Lifetime { get; }
}