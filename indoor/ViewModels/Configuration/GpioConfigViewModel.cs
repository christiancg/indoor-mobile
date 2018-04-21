using System;
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

		public GpioConfig gpioConfig
		{
			get;
			set;
		}

		public GpioConfigViewModel(IndoorConfigurationServices services)
		{
			this.services = services;
			gpioConfig = services.ReadGpioConfig();
			WriteGpioConfigCommand = new Command(() => WriteGpioConfig());
		}

		public bool WriteGpioConfig()
		{
			return services.WriteGpioConfig(gpioConfig);
		}
	}
}
