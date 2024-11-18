namespace App.Domain.Options;
public class ServiceBusOption
{
	public const string Key = "ServiceBusOption";
	public string RabbitMQURL { get; set; } = default!;
}
