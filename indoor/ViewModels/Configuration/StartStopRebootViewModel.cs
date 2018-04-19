using System;
using indoor.Services;

namespace indoor.ViewModels.Configuration
{
	public class StartStopRebootViewModel : BaseViewModel
    {
		private readonly IndoorConfigurationServices services;

		public StartStopRebootViewModel(IndoorConfigurationServices services)
        {
			this.services = services;
        }
    }
}
