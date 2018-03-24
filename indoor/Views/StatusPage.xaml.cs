using System;
using System.Collections.Generic;

using indoor.ViewModels;

using Xamarin.Forms;

namespace indoor
{
    public partial class StatusPage : ContentPage
    {
        StatusViewModel viewModel;

        bool cambiandoEstadoLuz = false;
        bool cambiandoEstadoFanIntra = false;
        bool cambiandoEstadoFanExtra = false;

        public StatusPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new StatusViewModel();
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

        protected async void OnToggleLuz(object sender, ToggledEventArgs args)
        {
            bool toggleValue = args.Value;
            var control = sender as Switch;
            if (control == null || cambiandoEstadoLuz)
                return;
            else
            {
                cambiandoEstadoLuz = true;
                if (!await viewModel.DataStore.Luz(toggleValue))
                    control.IsToggled = !toggleValue;
            }
            cambiandoEstadoLuz = false;
        }

        protected async void OnToggleFanIntra(object sender, ToggledEventArgs args)
        {
            bool toggleValue = args.Value;
            var control = sender as Switch;
            if (control == null || cambiandoEstadoFanIntra)
                return;
            else
            {
                cambiandoEstadoFanIntra = true;
                if(!await viewModel.DataStore.FanIntra(toggleValue))
                    control.IsToggled = !toggleValue;
            }
            cambiandoEstadoFanIntra = false;
        }

        protected async void OnToggleFanExtra(object sender, ToggledEventArgs args)
        {
            bool toggleValue = args.Value;
            var control = sender as Switch;
            if (control == null || cambiandoEstadoFanExtra)
                return;
            else
            {
                cambiandoEstadoFanExtra = true;
                if (!await viewModel.DataStore.FanExtra(toggleValue))
                    control.IsToggled = !toggleValue;
            }
            cambiandoEstadoFanExtra = false;
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
