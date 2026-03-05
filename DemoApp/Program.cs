using MetIit.Helpers.DI;
using DemoApp.Services.Common;
using Microsoft.Extensions.Hosting;

static class Program
{
    [AppService]
    public static ICountGenerator CreateCounter(IServiceProvider sp, object? key)
    {
        return new SequentialCounter(1000);
    }

    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        builder.Services.AddAppServices();
        var app = builder.Build();
        app.Run();
    }
}

