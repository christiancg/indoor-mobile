using System;
namespace indoor.Queue
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

        public string Result
        {
            get;
            set;
        }

        public QueueMessageResponse(int StatusCode, Boolean Success, string Result)
        {
            this.StatusCode = StatusCode;
            this.Success = Success;
            this.Result = Result;
        }
    }
}
