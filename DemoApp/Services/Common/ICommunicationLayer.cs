namespace DemoApp.Services.Common;

public interface ICommunicationLayer
{
    Task HandleRequestAsync(Stream remote);
}
