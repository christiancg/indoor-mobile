using System;

using Xamarin.Forms;

using indoor.ViewModels;
using Plugin.BluetoothLE;

namespace indoor.Views.Configuration
{
	public partial class ConfigurationPage : ContentPage
	{      
		private ConfigurationViewModel viewModel = null;      

		public ConfigurationPage()
		{
			InitializeComponent();
			BindingContext = viewModel = new ConfigurationViewModel();
			NavigationPage.SetHasNavigationBar(this, false);
		}

		public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (((ListView)sender).SelectedItem == null)
			{
				return;
			}
			IsBusy = false;

			listView.IsVisible = false;
			IDevice device = e.SelectedItem as IDevice;

			Navigation.PushAsync(new NavigationPage(new MasterConfigPage(device)));
            //En realidad aca se esta conectando, aca tengo que instanciar el master detail page
			//viewModel.ItemSeleccionado(device);

			((ListView)sender).SelectedItem = null; // clear selection
		}

		protected override void OnDisappearing()
		{
			viewModel.StopScan();
		}      
	}
}
