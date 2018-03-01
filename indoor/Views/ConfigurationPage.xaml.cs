using System;
using System.Collections.Generic;

using indoor.ViewModels;
using indoor.Config;

using Xamarin.Forms;

namespace indoor
{
    public partial class ConfigurationPage : ContentPage
    {

        ConfigurationViewModel viewModel;

        public ConfigurationPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ConfigurationViewModel(this.Navigation); // HERE
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
