using System.Net;
using System.Net.Sockets;
using MetIit.Helpers.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DemoApp.Services.Common;

[AppService(ServiceLifetime.Transient)]
public class TcpConnectivity(IConfiguration config) : ITransportLayer
{
    public Socket StartListening()
    {
        int port = config.GetValue("Server:TcpPort", 4000);
        var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        listener.Bind(new IPEndPoint(IPAddress.Any, port));
        listener.Listen();
        return listener;
    }
}
