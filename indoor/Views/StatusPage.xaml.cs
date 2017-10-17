﻿using System;
using System.Collections.Generic;

using indoor.ViewModels;

using Xamarin.Forms;

namespace indoor
{
    public partial class StatusPage : ContentPage
    {
        StatusViewModel viewModel;

        public StatusPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new StatusViewModel();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.GetEstadoCommand.Execute(null);
        }

        protected async void OnToggleLuz(object sender, ToggledEventArgs args)
        {
            bool toggleValue = args.Value;
            await viewModel.DataStore.Luz(toggleValue);
        }

        protected async void OnToggleFanIntra(object sender, ToggledEventArgs args)
        {
            bool toggleValue = args.Value;
            await viewModel.DataStore.FanIntra(toggleValue);
        }

        protected async void OnToggleFanExtra(object sender, ToggledEventArgs args)
        {
            bool toggleValue = args.Value;
            await viewModel.DataStore.FanExtra(toggleValue);
        }
    }
}
