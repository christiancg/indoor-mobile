using System;
using System.Threading.Tasks;
using indoor.Models;
using indoor.Services;

using Xamarin.Forms;

namespace indoor.ViewModels.Configuration
{
	public class GpioConfigViewModel : BaseViewModel
	{
		private readonly IndoorConfigurationServices services;

		public Command WriteGpioConfigCommand
		{
			get;
			set;
		}

		public bool WriteStatus
		{
			get;
			set;
		}

		public Command ReadGpioConfigCommand
		{
			get;
			set;
		}

		public GpioConfig GpioConfig
		{
			get;
			set;
		}

		public GpioConfigViewModel(IndoorConfigurationServices services)
		{
			this.services = services;
			WriteGpioConfigCommand = new Command(async () => WriteStatus = await WriteGpioConfig());
			ReadGpioConfigCommand = new Command(async () => GpioConfig = await ReadGpioConfig());
		}

		public async Task<bool> WriteGpioConfig()
		{
			bool status = await services.WriteGpioConfig(GpioConfig);
			if (!status)
				GpioConfig = await ReadGpioConfig();
			return status;
		}

		public async Task<GpioConfig> ReadGpioConfig()
		{
			return await services.ReadGpioConfig();
		}
	}
}
