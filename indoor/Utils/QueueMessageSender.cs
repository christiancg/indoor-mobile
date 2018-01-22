using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
namespace indoor.Utils
{
    public class QueueMessageSenderReciever<T> : RpcClient
    {

        public static string ParseSendingMessage(String user, String password, String endpoint, List<string> queryStringParameters, object body)
        {
            QueueMessageRequest message = new QueueMessageRequest(user, password, endpoint, queryStringParameters, JObject.FromObject(body));
            return message.ToString();
        }

        public static string ParseSendingMessage(QueueMessageRequest message)
        {
            return JObject.FromObject(message).ToString();
        }

        public QueueMessageSenderReciever(QueueMessageRequest message) : base(ParseSendingMessage(message)) { }

        public QueueMessageSenderReciever(String user, String password, String endpoint, List<string> queryStringParameters, object body) : base(ParseSendingMessage(user, password, endpoint, queryStringParameters, body)) { }

        async public Task<T> CallForResponse()
        {
            string response = await base.Call();
            QueueMessageResponse objResponse = JObject.Parse(response).ToObject<QueueMessageResponse>();
            if (typeof(T) == typeof(Boolean))
                return (T)(object)(objResponse.StatusCode >= 200 && objResponse.StatusCode < 300);
            return (T)objResponse.Result;
        }

    }
}
