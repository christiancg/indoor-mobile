using System;
using System.Collections.Generic;
using indoor.Services;
using indoor.ViewModels.Configuration;
using Xamarin.Forms;

namespace indoor.Views.Configuration
{
	public partial class GpioConfigPage : ContentPage
	{
		private GpioConfigViewModel viewModel = null;

		public GpioConfigPage(IndoorConfigurationServices btServices)
		{
			InitializeComponent();
			BindingContext = viewModel = new GpioConfigViewModel(btServices);
			NavigationPage.SetHasNavigationBar(this, false);
		}

		void Save(object sender, EventArgs ea)
		{

		}

	}
}
