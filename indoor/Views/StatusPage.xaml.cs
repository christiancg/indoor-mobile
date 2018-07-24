using System;
using System.Collections.Generic;

using indoor.ViewModels;
using indoor.Models;

using Xamarin.Forms;

namespace indoor.Views
{
    public partial class StatusPage : ContentPage
    {
        StatusViewModel viewModel;

        public StatusPage(List<ConfigGPIO> configgpios)
        {
            InitializeComponent();
            BindingContext = viewModel = new StatusViewModel(configgpios);
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.setMensajes();
            viewModel.GetEstadoCommand.Execute(null);
        }

        protected override void OnDisappearing()
        {
            viewModel.unsetMensajes();
        }

        protected void StepperValueChanged(object sender, ValueChangedEventArgs args)
        {
            MessagingCenter.Send(this, "CambiarTextoLabel");
        }

        protected void OnClick(object sender, EventArgs args)
        {
            viewModel.RegarCommand.Execute(null);
        }
    }
}
