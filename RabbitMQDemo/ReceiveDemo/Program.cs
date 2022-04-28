using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory();
factory.HostName = "localhost";
factory.DispatchConsumersAsync = true;
string exchangeName = "demoExchange";
string eventName = "demoEvent";
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();
string queueName = "demoQueue1";
channel.ExchangeDeclare(exchange: exchangeName, type: "direct");
channel.QueueDeclare(
    queue: queueName,
    durable: true,
    exclusive: false,
    autoDelete: false,
    arguments: null);
channel.QueueBind(
    queue: queueName, exchange: exchangeName, routingKey: eventName);
var consumer = new AsyncEventingBasicConsumer(channel);
consumer.Received += Consumer_Received;
channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
Console.ReadLine();

async Task Consumer_Received(object sender, BasicDeliverEventArgs args)
{
    try
    {
        var bytes = args.Body.ToArray();
        string message = Encoding.UTF8.GetString(bytes);
        Console.WriteLine("received: " + message);
        channel.BasicAck(args.DeliveryTag, multiple: false);
        await Task.Delay(800);
    }
    catch (Exception e)
    {
        channel.BasicReject(args.DeliveryTag, true);
        Console.WriteLine("error: " + e);
    }
}