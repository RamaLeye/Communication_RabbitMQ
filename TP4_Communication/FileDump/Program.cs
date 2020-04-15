using System;
using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Client.Events;
using System.IO;

namespace FileDump
{
    class Program
    {
        static void Main(string[] args)
        {
            //liste des severites 
            string[] severitiesNeeded = { "warning", "error", "critical" };

            //paramétrage channel
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {

                //declaration channel et type
                channel.ExchangeDeclare(exchange: "direct_logs",
                                        type: "direct");
                var queueName = channel.QueueDeclare().QueueName;
             

                //binding queues
                foreach (var severity in severitiesNeeded)
                {
                    channel.QueueBind(queue: queueName,
                                      exchange: "direct_logs",
                                      routingKey: severity);
                }

                Console.WriteLine(" [*] Waiting for logs. All logs are written in FileDump.log file, which is in the .exe folder. ");


                //reception des logs
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    var routingKey = ea.RoutingKey;


                    // ecriture dans le file FileDump.log qui se trouve dans le dossier du .exe
                    string logToWrite = "["+ DateTime.UtcNow.ToString("yyyy-MM-dd HH':'mm':'ss") + "][" + routingKey.ToUpper() + "] : " + message;
                    File.AppendAllText(@".\FileDump.log" , logToWrite+"\n");

                    Console.WriteLine(" Received and Writing ... ");
                };
                channel.BasicConsume(queue: queueName,
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
