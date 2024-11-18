using App.Domain.Event;
using MassTransit;

namespace App.Bus.Consumer;
public class ProductAddedEventConsumer : IConsumer<ProductAddedEvent>
{
	public Task Consume(ConsumeContext<ProductAddedEvent> context)
	{
		Console.WriteLine($"Gelen event:{context.Message.Id} - {context.Message.Name} - {context.Message.Price}");
		return Task.CompletedTask;
	}
}
