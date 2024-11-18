using App.Domain.Event;

namespace App.Application.Contracts.ServiceBus;
public interface IServiceBus
{
	Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : IEventOrMessage; //bu metot exchange'e gonderecek (rabbitmq)
	Task SendAsync<T>(T message, string queueName, CancellationToken cancellationToken = default) where T : IEventOrMessage; //bu metot direkt queue'ye gonderecek (rabbitmq)
}
