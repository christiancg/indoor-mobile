using System;
using System.Collections.Generic;
using indoor.Services;
using indoor.ViewModels.Configuration.DetailViewModels;
using Xamarin.Forms;

namespace indoor.Views.Configuration.DetailPages
{
	public partial class ServerConfigPage : BaseDetailPage
    {
		private ServerConfigViewModel viewModel = null;

		public ServerConfigPage()
        {
			InitializeComponent();
        }

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
