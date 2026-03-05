using MetIit.Helpers.DI;
using System.Net.Sockets;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DemoApp.Services.Common;

[AppService]
public class MainWorker(ITransportLayer transport, ILogger<MainWorker> logging, ICommunicationLayer handler) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var server = transport.StartListening();
        logging.LogInformation("Server started, listening on {e}", server.LocalEndPoint);
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var client = await server.AcceptAsync(stoppingToken);
                OnConnect(client);
            }
        }
        catch (OperationCanceledException) { }
    }

    private async void OnConnect(Socket connection)
    {
        try
        {
            await handler.HandleRequestAsync(new NetworkStream(connection));
        }
        catch (Exception ex)
        {
            logging.LogError(ex, "Communication failure");
        }
        connection.Close();
    }
}
