using System;
using System.Threading.Tasks;
using indoor.Models;
using indoor.Services;
using Xamarin.Forms;

namespace indoor.ViewModels.Configuration
{
	public class ServerConfigViewModel : BaseViewModel
	{
		private readonly IndoorConfigurationServices services;

		public Command SaveCommand
		{
			get;
			set;
		}

		public Command LoadCommand
        {
            get;
            set;
        }

		public bool WriteStatus
		{
			get;
			set;
		}

		private ServerConfig _ServerConfig;
		public ServerConfig ServerConfig
		{
			get{
				return _ServerConfig;
			}
			set{
				_ServerConfig = value;
				OnPropertyChanged();
			}
		}

		public ServerConfigViewModel(IndoorConfigurationServices services)
		{
			this.services = services;
			SaveCommand = new Command(async () => WriteStatus = await Save());
			LoadCommand = new Command(async () => ServerConfig = await Load());
		}

		private async Task<bool> Save()
		{
			return await services.WriteServerConfig(ServerConfig);
		}

		private async Task<ServerConfig> Load()
        {
			return await services.ReadServerConfig();
        }
	}
}
