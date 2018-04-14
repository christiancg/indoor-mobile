using System;

using Xamarin.Forms;

using indoor.ViewModels;
using Plugin.BluetoothLE;

namespace indoor.Views
{
	public partial class ConfigurationPage : ContentPage
	{      
		private ConfigurationViewModel viewModel = null;      

		public ConfigurationPage()
		{
			InitializeComponent();
			BindingContext = viewModel = new ConfigurationViewModel();
			grilla.IsVisible = false;
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
			grilla.IsVisible = true;
			IDevice device = e.SelectedItem as IDevice;
			viewModel.ItemSeleccionado(device);
			((ListView)sender).SelectedItem = null; // clear selection
		}

		protected override void OnDisappearing()
		{
			viewModel.StopScan();
		}

		void Save(object sender, EventArgs e)
		{

		}


	}
}
