using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQClient1
{
    class Program
    {
        private const string HostName = "localhost";
        private const string UserName = "guest";
        private const string Password = "guest";
        private const string QueueName = "Module1.Sample3";
        private const string ExchangeName = "";

        static void Main(string[] args)
        {
            Console.WriteLine("Startig RabbitMQ Message Sender...\n\n");

            var connectionFactory = new ConnectionFactory
            {
                HostName = HostName,
                UserName = UserName,
                Password = Password
            };

            var connection = connectionFactory.CreateConnection();
            var model = connection.CreateModel();

            //Create message properties..
            var properties = model.CreateBasicProperties();
            properties.Persistent = false;

            //Create the message....
            byte[] message = Encoding.Default.GetBytes("Hello World");

            //Send Message...
            model.BasicPublish(ExchangeName, QueueName, properties, message);

            Console.WriteLine("Message Sent...");
            Console.ReadLine();


        }
    }
}
