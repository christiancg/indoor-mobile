using System;
namespace indoor.Utils
{
    public class QueueMessageResponse
    {
        public int StatusCode
        {
            get;
            set;
        }

        public Boolean Success
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

        public object Result
        {
            get;
            set;
        }

        public QueueMessageResponse(int StatusCode, Boolean Success, string Message, object Result)
        {
            this.StatusCode = StatusCode;
            this.Success = Success;
            this.Message = Message;
            this.Result = Result;
        }
    }
}
