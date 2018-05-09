using System;
using System.Collections.Generic;
using indoor.Services;
using indoor.ViewModels.Configuration.DetailViewModels;
using Xamarin.Forms;

namespace indoor.Views.Configuration.DetailPages
{
	public partial class GpioConfigPage : BaseDetailPage
	{
		private GpioConfigViewModel viewModel = null;

		public GpioConfigPage()
		{
			InitializeComponent();
			BindingContext = viewModel = new GpioConfigViewModel();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			viewModel.ReadGpioConfigCommand.Execute(null);
		}
	}
}
