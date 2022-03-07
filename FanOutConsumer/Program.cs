// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

Console.WriteLine("Hello, World!");
IConnection conn;
IModel channel;
ConnectionFactory factory = new ConnectionFactory();
factory.HostName = "192.168.2.2";
factory.VirtualHost = "/";
factory.Port = 5672;
factory.UserName = "docampo";
factory.Password = "Flowers!@#4";

conn = factory.CreateConnection();

channel = conn.CreateModel();


var consumer = new EventingBasicConsumer(channel);

consumer.Received += Consumer_Recevied;

var consumerTag = channel.BasicConsume("my.Queue1",true,consumer);
Console.WriteLine("Waiting for messages> press any key to exit.");
Console.ReadKey();

 static void Consumer_Recevied(object sender, BasicDeliverEventArgs e)
{
    var body = e.Body.Span;
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine("[x] Received {0}", message);
}