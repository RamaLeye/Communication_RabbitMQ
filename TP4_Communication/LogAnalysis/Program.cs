using System;
using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Client.Events;

namespace LogAnalysis
{
    class Program
    {
        
        static void Main(string[] args)
        {

            TimerLog timerLog = new TimerLog();

            timerLog.Execute();

        }
    }
}
