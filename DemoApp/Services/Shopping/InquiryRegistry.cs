using MetIit.Helpers.DI;
using DemoApp.Services.Common;
using Microsoft.Extensions.DependencyInjection;

namespace DemoApp.Services.Shopping;

[AppService(ServiceLifetime.Scoped)]
public class InquiryRegistry(ICountGenerator counting) : IDisposable
{
    private readonly List<string> entries = [];

    public void Submit(ItemInfo info)
    {
        entries.Add($"{counting.NextCount(1)}\t{DateTime.Now}\t{info.Id}");
    }

    public void Dispose()
    {
        File.AppendAllLines("data/inqreg.tsv", [.. entries]);
    }
}
