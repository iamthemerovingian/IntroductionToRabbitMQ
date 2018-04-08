using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting RabbitMQ Message Sender....\n\n");

            var messageCount = 0;
            var sender = new RabbitSender();

            Console.WriteLine("Press the enter key to send a message");

            while (true)
            {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Q)
                {
                    break;
                }

                if (key.Key == ConsoleKey.Enter)
                {
                    var message = $"Message Count: {messageCount}";
                    Console.WriteLine($"Sending: {message}");

                    sender.Send(message);
                    messageCount++;
                }
            }

            Console.ReadLine();
        }
    }
}
