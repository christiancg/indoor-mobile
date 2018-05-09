using System;
using System.Collections.ObjectModel;
using indoor.Services;
using Plugin.BluetoothLE;

namespace indoor.ViewModels.Configuration
{
	public class ConfigurationViewModel : BaseViewModel
	{
		public IndoorConfigurationServices IndoorConfigurationServices
		{
			get;
		} = IndoorConfigurationServices.Instance;

		public ObservableCollection<IDevice> DispositivosEncontrados
		{
			get;
			set;
		}

		public ConfigurationViewModel()
		{
			IndoorConfigurationServices.StartScan();
			DispositivosEncontrados = IndoorConfigurationServices.DispositivosEncontrados;
		}

		public void StopScan()
		{
			IndoorConfigurationServices.StopScan();
		}
	}
}
