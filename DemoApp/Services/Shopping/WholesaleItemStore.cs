using MetIit.Helpers.DI;
using Microsoft.Extensions.DependencyInjection;

namespace DemoApp.Services.Shopping;

[AppService(ServiceLifetime.Transient, Key = "other")]
public class WholesaleItemStore : IDataAccessLayer
{
    public ItemInfo? FindItem(string? name)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(name);
        return File.ReadAllLines("data/wholesale.csv")
            .Skip(1)
            .Where(line => line.StartsWith(name))
            .Select(line =>
            {
                string[] segs = line.Split(',');
                return new ItemInfo(
                    Id: name,
                    Cost: double.Parse(segs[1].Trim()),
                    Stock: int.Parse(segs[2].Trim())
                );
            })
            .FirstOrDefault();

    }
}
