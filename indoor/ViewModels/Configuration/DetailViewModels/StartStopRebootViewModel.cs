using System;
using System.Threading.Tasks;
using indoor.Models;
using indoor.Services;
using Xamarin.Forms;

namespace indoor.ViewModels.Configuration.DetailViewModels
{
	public class StartStopRebootViewModel : BaseDetailViewModel
	{
		private IndoorConfigurationServices services = IndoorConfigurationServices.Instance;

		private string _ServerStatus;
		public string ServerStatus
		{
			get
			{
				return _ServerStatus;
			}
			set
			{
				_ServerStatus = "status: " + value;
				OnPropertyChanged();
			}
		}

		private string _StartStopText;
		public string StartStopText
		{
			get
			{
				return _StartStopText;
			}
			set
			{
				_StartStopText = value;
				OnPropertyChanged();
			}
		}

		public bool IsStarted
		{
			get;
			set;
		}

		public Command StatusCommand
		{
			get;
			set;
		}

		public Command StartStopCommand
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

		public StartStopRebootViewModel()
		{
			StatusCommand = new Command(async () => await Status());
			StartStopCommand = new Command(async () => await StartStop());
			RestartCommand = new Command(async () => await RestartServer());
			HardRestartCommand = new Command(async () => await HardRestartServer());
		}

		private async Task StartStop()
		{
			Alert toSend = null;
			BluetoothWriteResponse status = BluetoothWriteResponse.ERROR;
			if (IsStarted)
			{
				status = await services.StartStopReboot(StartStopReboot.STOP);
                if (status == BluetoothWriteResponse.OK)
                    toSend = new Alert("Indoor parado exitosamente", "Se ha detenido exitosamente el indoor");
                else
                    toSend = new Alert("Error al detener indoor", "Ha ocurrido un error al detener el indoor el mismo se encuentra corriendo");
			}
			else
			{
				status = await services.StartStopReboot(StartStopReboot.START);
                if (status == BluetoothWriteResponse.OK)
                    toSend = new Alert("Indoor inciado correctamente", "Se ha iniciado correctamente el indoor. El mismo podria demorar unos segundos para encontrarse listo");
                else
                    toSend = new Alert("Error al inciar indoor", "Ha ocurrido un error al iniciar el indoor");
			}
			await Status();
			SendMessage(toSend);
		}

		private async Task RestartServer()
		{
			Alert toSend = null;
			BluetoothWriteResponse status = await services.StartStopReboot(StartStopReboot.REBOOT);
			if (status == BluetoothWriteResponse.OK)
				toSend = new Alert("Indoor reiniciado correctamente", "Se ha reiniciado correctamente el indoor. El mismo podria demorar unos segundos para encontrarse listo");
			else
				toSend = new Alert("Error al reiniciar indoor", "Ha ocurrido un error al reiniciar el indoor");
			await Status();
			SendMessage(toSend);
		}

		private async Task HardRestartServer()
		{
			Alert toSend = null;
			BluetoothWriteResponse status = await services.StartStopReboot(StartStopReboot.HARD_REBOOT);
			if (status == BluetoothWriteResponse.OK)
				toSend = new Alert("Indoor reiniciado completamente", "Se ha reiniciado completamente el indoor. El mismo podria demorar hasta un minuto para encontrarse listo");
			else
				toSend = new Alert("Error al reiniciar completamente el indoor", "Ha ocurrido un error al reiniciar completamente el indoor");
			SendMessage(toSend);
		}

		private async Task Status()
		{
			string status = await services.ServerStatus();
			switch (status)
			{
				case "True":
					IsStarted = true;
					StartStopText = "Stop";
					ServerStatus = "Activo";
					break;
				case "False":
					IsStarted = false;
					StartStopText = "Start";
					ServerStatus = "Inactivo";
					break;
				default:
					IsStarted = false;
					StartStopText = "Start";
					ServerStatus = "Error";
					break;
			}
		}
	}
}
