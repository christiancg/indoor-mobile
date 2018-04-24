using System;
using System.Collections.Generic;
using indoor.Services;
using indoor.ViewModels.Configuration;
using Xamarin.Forms;

namespace indoor.Views.Configuration
{
    public partial class ServerConfigPage : ContentPage
    {
		private ServerConfigViewModel viewModel = null;

		public ServerConfigPage(IndoorConfigurationServices btServices)
        {
            InitializeComponent();
			BindingContext = viewModel = new ServerConfigViewModel(btServices);
        }
        
		protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadCommand.Execute(null);
        }
    }
}
