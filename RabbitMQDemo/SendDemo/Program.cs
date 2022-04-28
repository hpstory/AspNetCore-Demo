using RabbitMQ.Client;
using System.Text;

var connectionFactory = new ConnectionFactory();
connectionFactory.HostName = "localhost";
connectionFactory.DispatchConsumersAsync = true;
string exchangeName = "demoExchange";
string exchangeType = "direct";
var connection = connectionFactory.CreateConnection();
while (true)
{
    using var channel = connection.CreateModel();
    var property = channel.CreateBasicProperties();
    property.DeliveryMode = 2;
    channel.ExchangeDeclare(exchangeName, exchangeType);
    byte[] body = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
    channel.BasicPublish(
        exchange: exchangeName,
        routingKey: "demoEvent",
        mandatory: true,
        basicProperties: property,
        body: body);
    Console.WriteLine("send");
    Thread.Sleep(5000);
}

