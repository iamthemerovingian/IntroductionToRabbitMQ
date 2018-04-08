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
        }
    }
}
