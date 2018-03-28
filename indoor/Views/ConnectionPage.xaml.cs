using System;
using System.Collections.Generic;

using indoor.ViewModels;
using indoor.Config;

using Xamarin.Forms;
using Robotics.Mobile.Core.Bluetooth.LE;

namespace indoor
{
    public partial class ConnectionPage : ContentPage
    {

        ConnectionViewModel viewModel;
        private IAdapter adapter;


        public ConnectionPage(IAdapter Adapter)
        {
            InitializeComponent();
            this.adapter = Adapter;
            BindingContext = viewModel = new ConnectionViewModel(this.Navigation); // HERE
            ConfiguracionSaverRetriever.RetrieveProperties();
            this.txtURL.Text = Configuracion.Instancia.restBaseUrl;
            this.txtUsuario.Text = Configuracion.Instancia.usuario;
            this.txtPassword.Text = Configuracion.Instancia.contrasenia;
            this.swRest.IsToggled = Configuracion.Instancia.useRestComunicationSchema;
            this.swRemember.IsToggled = Configuracion.Instancia.saveConfiguration;
        }

        void OnClick(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "LogIn");
        }

        void AbrirConfiguracion(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "Configurar", adapter);
        }

        protected override void OnAppearing()
        {
            viewModel.setMensajes();
        }

        protected override void OnDisappearing()
        {
            viewModel.unsetMensajes();
        }

    }
}
