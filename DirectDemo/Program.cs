// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using System.Collections;
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

channel.ExchangeDeclare(
    "ex.direct",
    "direct",
    true,
    false,
    null 
    );


channel.QueueDeclare(
    "my.infos",
    true,
    false,
    false,
    null
    );
channel.QueueDeclare(
    "my.warnings",
    true,
    false,
    false,
    null
    );
channel.QueueDeclare(
    "my.errors",
    true,
    false,
    false,
    null
    );


channel.QueueBind("my.infos", "ex.direct", "info");
channel.QueueBind("my.warnings", "ex.direct", "info");
channel.QueueBind("my.errors", "ex.direct", "info");

channel.BasicPublish(
    "ex.direct",
    "info",
    null,
     Encoding.UTF8.GetBytes("Message with routing key")
     );
channel.BasicPublish(
    "ex.direct",
    "warning",
    null,
     Encoding.UTF8.GetBytes("Message with warning key")
     );

channel.BasicPublish(
    "ex.direct",
    "error",
    null,
     Encoding.UTF8.GetBytes("Message with Error key")
     );




