using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MetIit.Helpers.DI;

public static class AppServiceExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        var types = Assembly.GetCallingAssembly().GetTypes();
        types.Where(t => t.IsClass && !t.IsAbstract)
            .Select(t => (Type: t, Attr: t.GetCustomAttribute<AppServiceAttribute>()))
            .Where(s => s.Attr != null)
            .SelectMany(s => s.Type.GetInterfaces()
                .Where(i => i != typeof(IDisposable) && i != typeof(IAsyncDisposable))
                .DefaultIfEmpty(s.Type)
                .Select(t => new ServiceDescriptor(t, s.Attr!.Key, s.Type, s.Attr!.Lifetime)))
            .Concat(types.Where(t => t != typeof(AppServiceExtensions) && t.IsClass && t.IsAbstract && t.IsSealed)
                .SelectMany(t => t.GetMethods(BindingFlags.Public | BindingFlags.Static))
                .Select(m => (Method: m, Attr: m.GetCustomAttribute<AppServiceAttribute>()))
                .Where(s => s.Attr != null && s.Method.GetParameters().Length == 2 && s.Method.GetParameters()[0].ParameterType == typeof(IServiceProvider) && !s.Method.ReturnType.IsValueType)
                .Select(s => new ServiceDescriptor(s.Method.ReturnType, s.Attr!.Key, s.Method.CreateDelegate<Func<IServiceProvider, object?, object>>(), s.Attr.Lifetime)))
            .ToList()
            .ForEach(services.Add);        
        return services;
    }
}
