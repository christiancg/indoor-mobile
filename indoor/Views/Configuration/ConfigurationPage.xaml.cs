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

			listView.IsVisible = false;
			IDevice device = e.SelectedItem as IDevice;

			Navigation.PushAsync(new MasterConfigPage(device));
			//if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.iOS)
            //{
            //    await Navigation.PopToRootAsync();
            //}
			//Application.Current.MainPage = new MasterConfigPage(device);

			((ListView)sender).SelectedItem = null; // clear selection
		}

		protected override void OnDisappearing()
		{
			viewModel.StopScan();
		}      
	}
}
