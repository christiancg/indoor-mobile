using System;
using System.Collections.Generic;
using indoor.Services;
using indoor.ViewModels.Configuration;
using Xamarin.Forms;

namespace indoor.Views.Configuration
{
    public partial class StartStopRebootPage : ContentPage
    {
		private StartStopRebootViewModel viewModel = null;

		public StartStopRebootPage(IndoorConfigurationServices btServices)
        {
            InitializeComponent();
			BindingContext = viewModel = new StartStopRebootViewModel(btServices);
        }
    }
}
