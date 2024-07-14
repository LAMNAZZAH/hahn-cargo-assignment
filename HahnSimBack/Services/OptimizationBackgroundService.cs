using HahnSimBack.Dtos;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

public class OptimizationBackgroundService : BackgroundService
{
    private readonly ILogger<OptimizationBackgroundService> _logger;
    private IConnection _connection;
    private IModel _channel;
    private const string QueueName = "HahnCargoSim_NewOrders";

    public OptimizationBackgroundService(ILogger<OptimizationBackgroundService> logger)
    {
        _logger = logger;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = "host.docker.internal",
            UserName = "guest", 
            Password = "guest"  
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: QueueName,
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        _logger.LogInformation("RabbitMQ connection established. Waiting for messages...");

        return base.StartAsync(cancellationToken);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            _logger.LogInformation($"Received message: {message}");

            try
            {
                var order = JsonConvert.DeserializeObject<OrderDto>(message);
                ProcessOrder(order);
                _channel.BasicAck(ea.DeliveryTag, false);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error deserializing message");
                _channel.BasicNack(ea.DeliveryTag, false, requeue: false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing message");
                _channel.BasicNack(ea.DeliveryTag, false, requeue: true);
            }
        };

        _channel.BasicConsume(queue: QueueName,
                             autoAck: false,
                             consumer: consumer);

        return Task.CompletedTask;
    }

    private void ProcessOrder(OrderDto order)
    {
        _logger.LogInformation($"Processing order: Id={order.Id}, " +
            $"OriginNodeId={order.OriginNodeId}, TargetNodeId={order.TargetNodeId}, " +
            $"Load={order.Load}, Value={order.Value}, " +
            $"DeliveryDate={order.DeliveryDateUtc}, ExpirationDate={order.ExpirationDateUtc}");
        Console.WriteLine($"OrderIDDDDDDDDDDDDDDD:   {order.Id}");
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("RabbitMQ consumer is stopping...");

        _channel?.Close();
        _connection?.Close();

        await base.StopAsync(cancellationToken);
    }
}
