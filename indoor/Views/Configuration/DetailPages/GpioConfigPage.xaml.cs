﻿using System;
using System.Collections.Generic;
using indoor.Services;
using indoor.ViewModels.Configuration.DetailViewModels;
using Xamarin.Forms;

namespace indoor.Views.Configuration.DetailPages
{
	public partial class GpioConfigPage : BaseDetailPage
	{
		private GpioConfigViewModel viewModel = null;

		private GpioConfigPage()
		{
			InitializeComponent();
		}

		public GpioConfigPage(IndoorConfigurationServices btServices)
		{
			InitializeComponent();
			BindingContext = viewModel = new GpioConfigViewModel(btServices);
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			viewModel.ReadGpioConfigCommand.Execute(null);
		}
	}
}