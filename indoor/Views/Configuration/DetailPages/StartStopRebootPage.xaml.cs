﻿using System;
using System.Collections.Generic;
using indoor.Services;
using indoor.ViewModels.Configuration.DetailViewModels;
using Xamarin.Forms;

namespace indoor.Views.Configuration.DetailPages
{
	public partial class StartStopRebootPage : BaseDetailPage
    {
		private StartStopRebootViewModel viewModel = null;

		public StartStopRebootPage()
        {
            InitializeComponent();
			BindingContext = viewModel = new StartStopRebootViewModel();
        }

		protected override void OnAppearing()
        {
            base.OnAppearing();
			viewModel.StatusCommand.Execute(null);
        }
    }
}
