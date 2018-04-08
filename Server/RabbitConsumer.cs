using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Server
{
    class RabbitConsumer
    {
        public bool Enabled { get; set; }

        private const string HostName = "localhost";
        private const string UserName = "guest";
        private const string Password = "guest";
        private const string QueueName = "Module2.Sample1";
        private const string ExchangeName = "";

        private const bool IsDurable = true;

        private const string VirtualHost = "";
        private int Port = 0;

        private ConnectionFactory connectionFactory;
        private IConnection connection;
        private IModel model;

        public RabbitConsumer()
        {
            DisplaySettings();
            SetupRabbitMq();
        }

        private void DisplaySettings()
        {
            Console.WriteLine("Host: {0}", HostName);
            Console.WriteLine("Username: {0}", UserName);
            Console.WriteLine("Password: {0}", Password);
            Console.WriteLine("QueueName: {0}", QueueName);
            Console.WriteLine("ExchangeName: {0}", ExchangeName);
            Console.WriteLine("VirtualHost: {0}", VirtualHost);
            Console.WriteLine("Port: {0}", Port);
            Console.WriteLine("Is Durable: {0}", IsDurable);
        }

        private void SetupRabbitMq()
        {
            connectionFactory = new ConnectionFactory
            {
                HostName = HostName,
                UserName = UserName,
                Password = Password
            };

            if (!string.IsNullOrWhiteSpace(VirtualHost))
            {
                connectionFactory.VirtualHost = VirtualHost;
            }

            if (Port > 0)
            {
                connectionFactory.Port = Port;
            }

            connection = connectionFactory.CreateConnection();
            model = connection.CreateModel();

            model.QueueDeclare(QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            model.BasicQos(0, 1, false);
        }


        internal void Start()
        {
            var consumer = new EventingBasicConsumer(model);

            //Handle message recieve events.
            consumer.Received += RecievedEvent;

            model.BasicConsume(QueueName, false, consumer);

        }

        private void RecievedEvent(object sender, BasicDeliverEventArgs e)
        {
            Console.WriteLine($"Message Recieved: {Encoding.Default.GetString(e.Body)}");
            model.BasicAck(e.DeliveryTag, false);
        }
    }
}