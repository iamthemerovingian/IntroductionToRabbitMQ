using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPCClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting RabbitMQ Message Sender....\n\n");

            var rpcClient = new RpcClientService();

            Console.WriteLine("Press the enter key to send a message");

            while (true)
            {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Q)
                {
                    rpcClient.Close();
                }

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine(" [x] Requesting fib(30)");
                    var response = rpcClient.Call("30");
                    Console.WriteLine(" [.] Got '{0}'", response);
                }
            }

            Console.ReadLine();
        }
    }
}
