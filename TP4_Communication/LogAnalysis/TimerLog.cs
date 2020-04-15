using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace LogAnalysis
{
    class TimerLog
    {
        private static Timer logTimer ;
        private static  int compteurInfo;
        private static int compteurWarning;
        private static int compteurError;
        private static int compteurCritical;

        public TimerLog()
        {
            compteurInfo = compteurWarning = compteurError = compteurCritical = 0;
            logTimer = new Timer(10000);
        }

        //parametrage du timer 
        private static void SetTimer()
        {
            // Create a timer with a 10 seconds interval.
            logTimer = new System.Timers.Timer(10000);

            logTimer.Elapsed += displayLogsNumber;
            logTimer.AutoReset = true;
            logTimer.Enabled = true;
        }

        //fonciton qui start le timer et qui execute loadingLogsNumber
        public  void Execute()
        {
            SetTimer();
            Console.WriteLine("___________LogAnalysis has started at {0:HH:mm:ss.fff} ___________", DateTime.Now);

            loadingLogsNumber();

            Console.ReadLine();
            logTimer.Stop();
            logTimer.Dispose();

            Console.WriteLine("Terminating the application...");
        }

        // connection avec l'exchange pour recuperer les infos des differentes queues
        public void loadingLogsNumber()
        {
            string[] severitiesNeeded = { "info", "warning", "error", "critical" };
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "direct_logs",
                                        type: "direct");
                var queueName = channel.QueueDeclare().QueueName;


                //on fait un binding pour chaque level de severité
                foreach (var severity in severitiesNeeded)
                {
                    channel.QueueBind(queue: queueName,
                                      exchange: "direct_logs",
                                      routingKey: severity);
                }
              

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    // recuperation des infos necessaires 
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    var routingKey = ea.RoutingKey;


                    //comptage
                    switch (routingKey.ToLower())
                    {
                        case "info":
                             compteurInfo++;
                            break;

                        case "warning":
                            compteurWarning++;
                            break;

                        case "error":
                            compteurError++;
                            break;

                        case "critical":
                            compteurCritical++;
                            break;
                    }

                };
                channel.BasicConsume(queue: queueName,
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }


        //Affichage du nombre de logs par level
        private static void displayLogsNumber(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Last display at {0:HH:mm:ss.fff}",
                              e.SignalTime);
            Console.WriteLine(" Info : " + compteurInfo + (compteurInfo > 1 ? " logs" : " log"));
            Console.WriteLine(" Warning : " + compteurWarning + (compteurWarning > 1 ? " logs" : " log"));
            Console.WriteLine(" Error : " + compteurError + (compteurError > 1 ? " logs" : " log"));
            Console.WriteLine(" Critical : " + compteurCritical + (compteurCritical > 1 ? " logs" : " log"));
            Console.WriteLine();
        }
    }
}
