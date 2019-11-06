﻿using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using RabbitBroker.Core;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitBroker.Emitter
{
    class Program
    {
        static void Main(string[] args)
        {
            Producer producer = new Producer();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 10; i++)
            {
                producer.Emit();
                //Thread.Sleep(300);
            }
            sw.Stop();

            Console.WriteLine("Emitter task completed for {0}", sw.Elapsed);
            Console.ReadLine();
        }
    }
}
