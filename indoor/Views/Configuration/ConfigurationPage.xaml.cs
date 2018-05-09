using System;

using Xamarin.Forms;

using indoor.ViewModels;
using Plugin.BluetoothLE;
using indoor.ViewModels.Configuration;

namespace indoor.Views.Configuration
{
	public partial class ConfigurationPage : ContentPage
	{      
		private ConfigurationViewModel viewModel = null;      

		public ConfigurationPage()
		{
			InitializeComponent();         
			BindingContext = viewModel = new ConfigurationViewModel();
		}

		public void OnItemSelectedAsync(object sender, SelectedItemChangedEventArgs e)
		{
			if (((ListView)sender).SelectedItem == null)
			{
				return;
			}
			IsBusy = false;
			viewModel.StopScan();
			listView.IsVisible = false;
			IDevice device = e.SelectedItem as IDevice;
			viewModel.IndoorConfigurationServices.Conectar(device);
			Application.Current.MainPage = new MasterConfigPage();
		}

		protected override void OnDisappearing()
		{
			viewModel.StopScan();
		}      
	}
}
