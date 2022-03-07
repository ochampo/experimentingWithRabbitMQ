// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

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
    "ex.fanout",
    "fanout",
    true,
    false,
    null);
channel.QueueDeclare(

    "my.Queue1",
    true,
    false,
    false,
    null);
channel.QueueDeclare(

    "my.Queue2",
    true,
    false,
    false,
    null);


channel.QueueBind("my.Queue1", "ex.fanout", "");
channel.QueueBind("my.Queue2", "ex.fanout", "");

channel.BasicPublish("ex.fanout", "", null, Encoding.UTF8.GetBytes("Message1"));
channel.BasicPublish("ex.fanout", "", null, Encoding.UTF8.GetBytes("Message2"));

Console.WriteLine("Test Line: press any key to exit");
Console.ReadLine();

channel.QueueDelete("my.Queue");
channel.QueueDelete("my.Queue1");
channel.QueueDelete("my.Queue2");
channel.ExchangeDelete("ex.fanout");

channel.Close();
conn.Close();

