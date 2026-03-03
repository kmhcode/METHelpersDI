namespace DemoApp.Services.Shopping;

public interface IDataAccessLayer
{
    ItemInfo? FindItem(string? name);
}
