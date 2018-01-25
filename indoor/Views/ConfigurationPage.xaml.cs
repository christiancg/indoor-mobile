using System;
using System.Collections.Generic;

using indoor.ViewModels;
using indoor.Config;

using Xamarin.Forms;

namespace indoor
{
    public partial class ConfigurationPage : ContentPage
    {
        public ConfigurationPage()
        {
            InitializeComponent();
            BindingContext = new ConfigurationViewModel(this.Navigation); // HERE
            ConfiguracionSaverRetriever.RetrieveProperties();
            this.txtURL.Text = Configuracion.Instancia.restBaseUrl;
            this.txtUsuario.Text = Configuracion.Instancia.usuario;
            this.txtPassword.Text = Configuracion.Instancia.contrasenia;
            this.swRest.IsToggled = Configuracion.Instancia.useRestComunicationSchema;
            this.swRemember.IsToggled = Configuracion.Instancia.saveConfiguration;
        }

        void OnClick(object sender, EventArgs e){
            MessagingCenter.Send(this, "LogIn");
        }
    }
}
