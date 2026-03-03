using System.Text.Json;
using MetIit.Helpers.DI;

namespace DemoApp.Services.Shopping;

[AppService]
public class RetailItemStore : IDataAccessLayer
{
    private readonly ItemInfo[] items;

    public RetailItemStore()
    {
        using var document = new FileStream("data/retail.json", FileMode.Open);
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        items = JsonSerializer.Deserialize<ItemInfo[]>(document, options) ?? [];
    }

    public ItemInfo? FindItem(string? name) => items.FirstOrDefault(item => item.Id == name);
}
