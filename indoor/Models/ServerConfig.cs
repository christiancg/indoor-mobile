using System;
namespace indoor.Models
{
	public class ServerConfig
	{
		public string queueUrl
		{
			get;
			set;
		}
		public string queueName
		{
			get;
			set;
		}
		public string queueUser
		{
			get;
			set;
		}
		public string queuePassword
		{
			get;
			set;
		}

		private ServerConfig()
		{
		}

		public ServerConfig(string queueUrl, string queueName, string queueUser, string queuePassword)
		{
			this.queueUrl = queueUrl;
			this.queueName = queueName;
			this.queueUser = queueUser;
			this.queuePassword = queuePassword;
		}

	}
}
