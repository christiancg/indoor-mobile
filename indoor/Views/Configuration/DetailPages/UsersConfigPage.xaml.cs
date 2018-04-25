using System;
using System.Collections.Generic;
using indoor.Services;
using indoor.ViewModels.Configuration.DetailViewModels;
using Xamarin.Forms;

namespace indoor.Views.Configuration.DetailPages
{
	public partial class UsersConfigPage : BaseDetailPage
	{      
		private UsersConfigViewModel viewModel;

		public UsersConfigPage(IndoorConfigurationServices btServices)
		{
			InitializeComponent();
			BindingContext = viewModel = new UsersConfigViewModel(btServices);         
		}

		protected override void OnAppearing()
        {
            base.OnAppearing();
			viewModel.ReloadUserListCommand.Execute(null);
        }
	}
}
