using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmitLog
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };

            Console.WriteLine(" Press [enter] to send a message.");

            while (true)
            {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Q)
                {
                    break;
                }

                if (key.Key == ConsoleKey.Enter)
                {
                    using (var connection = factory.CreateConnection())
                    {
                        using (var channel = connection.CreateModel())
                        {
                            channel.ExchangeDeclare(exchange: "logs", type: "fanout");

                            string message = GetMessage(args);
                            var body = Encoding.UTF8.GetBytes(message);

                            channel.BasicPublish(exchange: "logs",
                                                routingKey: "",
                                                basicProperties: null,
                                                body: body);

                            Console.WriteLine($" [x] Sent {message}");
                        }
                    }
                }
            }
            

            Console.ReadLine();
        }

        private static string GetMessage(string[] args)
        {
            return args.Length > 0 ? string.Join(" ", args) : "Hello World!!"; 
        }
    }
}
