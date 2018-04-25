using System;
using System.Threading.Tasks;
using indoor.Models;
using indoor.Services;
using Xamarin.Forms;

namespace indoor.ViewModels.Configuration.DetailViewModels
{
	public class StartStopRebootViewModel : BaseDetailViewModel
	{
		private readonly IndoorConfigurationServices services;

		public Command StartCommand
		{
			get;
			set;
		}

		public Command StopCommand
		{
			get;
			set;
		}

		public Command RestartCommand
		{
			get;
			set;
		}

		public Command HardRestartCommand
		{
			get;
			set;
		}

		public StartStopRebootViewModel(IndoorConfigurationServices services)
		{
			this.services = services;
			StartCommand = new Command(async () => await StartServer());
			StopCommand = new Command(async () => await StopServer());
			RestartCommand = new Command(async () => await RestartServer());
			HardRestartCommand = new Command(async () => await HardRestartServer());
		}

		private async Task StartServer()
		{
			Alert toSend = null;
			bool status = await services.StartStopReboot(StartStopReboot.START);         
			if (status)
				toSend = new Alert("Indoor inciado correctamente", "Se ha iniciado correctamente el indoor. El mismo podria demorar unos segundos para encontrarse listo");
			else
				toSend = new Alert("Error al inciar indoor", "Ha ocurrido un error al iniciar el indoor");
			SendMessage(toSend);
		}

		private async Task StopServer()
		{
			Alert toSend = null;
			bool status = await services.StartStopReboot(StartStopReboot.STOP);         
			if (status)
				toSend = new Alert("Indoor parado exitosamente", "Se ha detenido exitosamente el indoor");
			else
				toSend = new Alert("Error al detener indoor", "Ha ocurrido un error al detener el indoor el mismo se encuentra corriendo");
			SendMessage(toSend);
		}

		private async Task RestartServer()
		{
			Alert toSend = null;
			bool status = await services.StartStopReboot(StartStopReboot.REBOOT);         
			if (status)
				toSend = new Alert("Indoor reiniciado correctamente", "Se ha reiniciado correctamente el indoor. El mismo podria demorar unos segundos para encontrarse listo");
			else
				toSend = new Alert("Error al reiniciar indoor", "Ha ocurrido un error al reiniciar el indoor");
			SendMessage(toSend);
		}

		private async Task HardRestartServer()
		{
			Alert toSend = null;
			bool status = await services.StartStopReboot(StartStopReboot.HARD_REBOOT);         
			if (status)
				toSend = new Alert("Indoor reiniciado completamente", "Se ha reiniciado completamente el indoor. El mismo podria demorar hasta un minuto para encontrarse listo");
			else

				toSend = new Alert("Error al reiniciar completamente el indoor", "Ha ocurrido un error al reiniciar completamente el indoor");
			SendMessage(toSend);
		}
	}
}
