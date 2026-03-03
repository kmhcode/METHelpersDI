using MetIit.Helpers.DI;
using DemoApp.Services.Common;
using Microsoft.Extensions.DependencyInjection;

namespace DemoApp.Services.Shopping;

[AppService]
public class InventoryLookup(IDataAccessLayer major, [FromKeyedServices("other")] IDataAccessLayer minor, IServiceScopeFactory scopping) : ICommunicationLayer
{
    public async Task HandleRequestAsync(Stream remote)
    {
        using var writer = new StreamWriter(remote) { AutoFlush = true };
        await writer.WriteLineAsync("Welcome to MET-DIGITAL shop.");
        using var reader = new StreamReader(remote);
        var name = await reader.ReadLineAsync();
        var info = major.FindItem(name) ?? minor.FindItem(name);
        if (info != null)
        {
            await writer.WriteLineAsync($"COST is {info.Cost} with STOCK of {info.Stock}");
            using var scope = scopping.CreateScope();
            var registry = scope.ServiceProvider.GetRequiredService<InquiryRegistry>();
            registry.Submit(info);
        }
    }
}
