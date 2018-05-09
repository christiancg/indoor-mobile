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
			BindingContext = viewModel = new ServerConfigViewModel();
        }
        
		protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadCommand.Execute(null);
        }
    }
}
