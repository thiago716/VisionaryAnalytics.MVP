using Core.Interfaces;
using MassTransit;

namespace Infraestructure.Message;

public class MessagePublisher : IMessagePublisher
{
     private readonly IPublishEndpoint _publish;
     private readonly ISendEndpointProvider _send;
     public MessagePublisher(IPublishEndpoint publish, ISendEndpointProvider send)
     {
          _publish = publish ?? throw new ArgumentNullException(nameof(publish));
          _send = send ?? throw new ArgumentNullException(nameof(send));
     }
     

     // Eventos (fanout/topic) -> 1..N consumers
    public Task Publish<T>(T message, CancellationToken cancellationToken = default) where T : class
        => _publish.Publish(message, cancellationToken);

     // Comandos (fila específica) -> 1 destino
    public async Task Send<T>(Uri destinationAddress, T message, CancellationToken cancellationToken = default) where T : class
    {
        var endpoint = await _send.GetSendEndpoint(destinationAddress);
        await endpoint.Send(message, cancellationToken);
    }
}
