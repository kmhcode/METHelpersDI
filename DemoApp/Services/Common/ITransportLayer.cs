using System.Net.Sockets;

namespace DemoApp.Services.Common;

public interface ITransportLayer
{
    Socket StartListening();
}
