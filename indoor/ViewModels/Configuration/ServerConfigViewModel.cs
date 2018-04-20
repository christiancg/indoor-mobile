using System;
using indoor.Models;
using indoor.Services;

namespace indoor.ViewModels.Configuration
{
	public class ServerConfigViewModel : BaseViewModel
    {
		private readonly IndoorConfigurationServices services;

		public ServerConfig serverConfig
		{
			get;
			set;
		}

		public ServerConfigViewModel(IndoorConfigurationServices services)
        {
			this.services = services;
        }
    }
}
