using System;
using RabbitMQ.Client;
using System.Text;
using System.Collections.Generic;

namespace LogEmitter
{
    class Program
    {
        static void Main(string[] args)
        {


            RandomObjects randomObjects = new RandomObjects();

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {

                //création de l'échangeur
                channel.ExchangeDeclare(exchange: "direct_logs",
                                        type: "direct");

                while (Console.ReadKey().Key == ConsoleKey.Enter)
                {


                    //création du message et du level
                    string message = randomObjects.randomMsg();

                    string severity = randomObjects.randomSeverity();

                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: "direct_logs",
                                         routingKey: severity,
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" LogEmitter Sent '{0}':'{1}'", severity, message);
                }

            }


        }


    }
    


    
}
