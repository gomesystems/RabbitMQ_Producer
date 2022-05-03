using RabbitMQ.Client;
using System;
using System.Text;

namespace RProducer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "queuecls",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                int count =0;

                while (true)
                {
                    string message = $"{count++} Mensagens!";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "queuecls",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [x] Enviando  {0}", message);

                    System.Threading.Thread.Sleep(200);
                }

                
            }

          
        }
    }
}
