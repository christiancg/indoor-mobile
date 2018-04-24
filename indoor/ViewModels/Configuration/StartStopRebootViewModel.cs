using System;
using System.Threading.Tasks;
using indoor.Models;
using indoor.Services;
using Xamarin.Forms;

namespace indoor.ViewModels.Configuration
{
	public class StartStopRebootViewModel : BaseViewModel
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
			await services.StartStopReboot(StartStopReboot.START);
		}

		private async Task StopServer()
        {
			await services.StartStopReboot(StartStopReboot.STOP);
        }

		private async Task RestartServer()
        {
			await services.StartStopReboot(StartStopReboot.REBOOT);
        }

		private async Task HardRestartServer()
        {
			await services.StartStopReboot(StartStopReboot.HARD_REBOOT);
        }
	}
}
