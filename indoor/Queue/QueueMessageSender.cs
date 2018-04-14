using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
namespace indoor.Queue
{
    public class QueueMessageSenderReciever : RpcClient
    {

        public static string ParseSendingMessage(String user, String password, String endpoint, List<string> queryStringParameters, string body)
        {
            QueueMessageRequest message = new QueueMessageRequest(user, password, endpoint, queryStringParameters, body);
            return message.ToString();
        }

        public static string ParseSendingMessage(QueueMessageRequest message)
        {
            return JObject.FromObject(message).ToString();
        }

        public QueueMessageSenderReciever(QueueMessageRequest message) : base(ParseSendingMessage(message)) { }

        public QueueMessageSenderReciever(String user, String password, String endpoint, List<string> queryStringParameters, string body) : base(ParseSendingMessage(user, password, endpoint, queryStringParameters, body)) { }

        async public Task<QueueMessageResponse> CallForResponse()
        {
            string response = await base.Call();
            QueueMessageResponse objResponse = JObject.Parse(response).ToObject<QueueMessageResponse>();
            return objResponse;
        }

    }
}
