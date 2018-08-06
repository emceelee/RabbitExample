using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Worker
{
    public class Worker
    {
        public static void Main()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    //Durable queue - Rabbit won't lose queue if Rabbit crashes
                    channel.QueueDeclare(queue: "task_queue",
                                        durable: true,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

                    //prefetchCount = 1, fair dispatching
                    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] Received {0}", message);

                        int dots = message.Split('.').Length - 1;
                        Thread.Sleep(dots * 1000);

                        Console.WriteLine(" [x] Done");

                        //manual acknowledge - message not lost if consumer dies while processing
                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    };

                    //autoAck = false - message not lost if consumer dies while processing
                    channel.BasicConsume(queue: "task_queue",
                                        autoAck: false,
                                        consumer: consumer);

                    Console.WriteLine("Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}
