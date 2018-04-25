using System;
using System.Threading.Tasks;
using indoor.Models;
using indoor.Services;
using Xamarin.Forms;

namespace indoor.ViewModels.Configuration.DetailViewModels
{
	public class ServerConfigViewModel : BaseDetailViewModel
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

		private ServerConfig _ServerConfig;
		public ServerConfig ServerConfig
		{
			get
			{
				return _ServerConfig;
			}
			set
			{
				_ServerConfig = value;
				OnPropertyChanged();
			}
		}

		public ServerConfigViewModel(IndoorConfigurationServices services)
		{
			this.services = services;
			SaveCommand = new Command(async () => await Save());
			LoadCommand = new Command(async () => ServerConfig = await Load());
		}

		private async Task<bool> Save()
		{
			bool status = await services.WriteServerConfig(ServerConfig);
			Alert toSend = null;
			if (status)
				toSend = new Alert("Configuracion de indoor guardada exitosamente", "Se ha guardado exitosamente la configuracion del indoor. Se requiere reinicio del indoor para que la misma surta efecto");
			else
				toSend = new Alert("Error al escribir configuracion del indoor ", "Ha ocurrido un error al escribir la configuracion del indoor, la misma no se ha guardado");
			SendMessage(toSend);
			return status;
		}

		private async Task<ServerConfig> Load()
		{
			return await services.ReadServerConfig();
		}
	}
}
