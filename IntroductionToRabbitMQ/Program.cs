using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroductionToRabbitMQ
{
    class Program
    {
        private const string HostName = "localhost";
        private const string UserName = "guest";
        private const string Passowrd = "guest";

        static void Main(string[] args)
        {
            Console.WriteLine("Starting RabbitMQ  Queue Creator... ");
            Console.WriteLine(Environment.NewLine);

            var connectionFactory = new ConnectionFactory
            {
                HostName = HostName,
                UserName = UserName,
                Password = Passowrd
            };

            var connection = connectionFactory.CreateConnection();
            var model = connection.CreateModel();

            //Creating a queue with name myqueue_codfe with durable messages but not exlusive, auto delete odd and no arguments.
            model.QueueDeclare("myqueue_code", true, false, false, null);
            Console.WriteLine("myqueue_code has been created...");

            //Crete an exchange to connect to the queue. The type of the exhange is topic, not sure what that means yet...
            model.ExchangeDeclare("myexchange_code", ExchangeType.Topic);
            Console.WriteLine("myexchange_code has been created...");

            //Binding the exchange to the queue, and adding the routing key "cars" so that only certian messages get route to this queue
            model.QueueBind("myqueue_code", "myexchange_code", "cars");
            Console.WriteLine("Exhane-Queue binding complete");

            Console.ReadLine();


        }
    }
}
