using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Receiver
{
    public class Consumer
    {
        public static void Main()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                RequestedHeartbeat = 30
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "logs", type: "fanout");

                var queueName = channel.QueueDeclare().QueueName;
                channel.QueueBind(queue: queueName,
                                  exchange: "logs",
                                  routingKey: "");

                Console.WriteLine(" [*] Waiting ...");

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" || {0}", message);
                };
                channel.BasicConsume(queue: queueName,
                                        autoAck: true,
                                        consumer: consumer);

                Console.WriteLine(" Press any key to exit.");
                Console.ReadKey();
            }
        }
    }
}