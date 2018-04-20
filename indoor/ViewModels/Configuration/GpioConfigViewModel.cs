using System;
using indoor.Models;
using indoor.Services;

namespace indoor.ViewModels.Configuration
{
	public class GpioConfigViewModel : BaseViewModel
    {
		private readonly IndoorConfigurationServices services;

		public GpioConfig gpioConfig
		{
			get;
			set;
		}

		public GpioConfigViewModel(IndoorConfigurationServices services)
        {
			this.services = services;
			gpioConfig = services.ReadGpioConfig();
        }
    }
}
