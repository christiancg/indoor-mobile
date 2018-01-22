using System;
using System.Collections.Concurrent;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using indoor.Config;

namespace indoor.Utils
{
    public abstract class RpcClient
    {
        private readonly IConnection connection;
        private readonly IModel channel;
        private readonly string replyQueueName;
        private readonly EventingBasicConsumer consumer;
        private readonly BlockingCollection<string> respQueue = new BlockingCollection<string>();
        private readonly IBasicProperties props;
        private readonly string message;

        public RpcClient(string message)
        {
            var factory = new ConnectionFactory() { HostName = "alfrescas.cipres.io", UserName = "test", Password = "ratablanca" };

            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            replyQueueName = channel.QueueDeclare().QueueName;
            consumer = new EventingBasicConsumer(channel);

            props = channel.CreateBasicProperties();
            var correlationId = Guid.NewGuid().ToString();
            props.CorrelationId = correlationId;
            props.ReplyTo = replyQueueName;
            this.message = message;

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var response = Encoding.UTF8.GetString(body);
                if (ea.BasicProperties.CorrelationId == correlationId)
                {
                    respQueue.Add(response);
                }
            };
        }

        public Task<string> Call() => Task.Run(() =>
                                                {
                                                    var messageBytes = Encoding.UTF8.GetBytes(message);
                                                    channel.BasicPublish(
                                                        exchange: "",
                                                        routingKey: "rpc_queue",
                                                        basicProperties: props,
                                                        body: messageBytes);

                                                    channel.BasicConsume(
                                                        consumer: consumer,
                                                        queue: replyQueueName,
                                                        autoAck: true);
                                                    connection.Close();
                                                    return respQueue.Take();
                                                });
    }
}
