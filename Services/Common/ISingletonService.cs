using Microsoft.Extensions.DependencyInjection;

namespace HostGirder;

public interface ISingletonService : IBaseService
{
    ServiceLifetime IBaseService.Lifetime => ServiceLifetime.Singleton;
}