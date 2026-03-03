using Microsoft.Extensions.DependencyInjection;

namespace MetIit.Helpers.DI;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AppServiceAttribute(ServiceLifetime lifetime = ServiceLifetime.Singleton) : Attribute
{
    public ServiceLifetime Lifetime => lifetime;

    public object? Key {get; init;}
}
