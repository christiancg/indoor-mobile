using System;
using System.Collections.Generic;
using indoor.Services;
using indoor.ViewModels.Configuration;
using Xamarin.Forms;

namespace indoor.Views.Configuration
{
	public partial class UsersConfigPage : ContentPage
	{
		

		private UsersConfigViewModel viewModel;

		public UsersConfigPage(IndoorConfigurationServices btServices)
		{
			InitializeComponent();
			BindingContext = viewModel = new UsersConfigViewModel(btServices);

		}
	}
}
