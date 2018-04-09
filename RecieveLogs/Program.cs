using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecieveLogs
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: "logs", type: "fanout");

                    var queueName = channel.QueueDeclare().QueueName;

                    channel.QueueBind(queue: queueName,
                        exchange: "logs",
                        routingKey: "");

                    Console.WriteLine(" [*] waiting for logs. ");

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += MessageRecieved;

                    channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit. ");
                    Console.ReadLine();
                }
            }
        }

        private static void MessageRecieved(object sender, BasicDeliverEventArgs e)
        {
            byte[] body = e.Body;

            var message = Encoding.UTF8.GetString(body);

            Console.WriteLine($" Message {message}");
        }
    }
}
