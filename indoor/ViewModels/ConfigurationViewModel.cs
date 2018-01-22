using System;

using indoor.Config;
using indoor.Models;
using indoor.Services;

using Xamarin.Forms;

namespace indoor.ViewModels
{
    public class ConfigurationViewModel : BaseViewModel
    {
        public string RestURLBase
        {
            get;
            set;
        }

        public string Usuario
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public bool Recordar
        {
            get;
            set;
        }

        public bool UsarComunicacionRest
        {
            get;
            set;
        }

        public Command NuevaProgramacionCommand { get; set; }

        private INavigation _navigation;

        public ConfigurationViewModel(INavigation navigation)
        {
            _navigation = navigation; // AND HERE
            Title = "LogIn";
            MessagingCenter.Subscribe<ConfigurationPage>(this, "LogIn", async (obj) =>
            {
                Configuracion.Instancia.restBaseUrl = "http://" + this.RestURLBase;
                Configuracion.Instancia.usuario = this.Usuario;
                Configuracion.Instancia.contrasenia = this.Password;
                Configuracion.Instancia.saveConfiguration = this.Recordar;
                Configuracion.Instancia.useRestComunicationSchema = this.UsarComunicacionRest;

                EstadoIndoor estado = await IndoorComunicaitionFactory.GetInstance().GetEstado();
                if (estado != null)
                {
                    await _navigation.PushAsync(new NavigationPage(new MainPage()));
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Error al realizar login, revise los datos", "Error");
                }
            });
        }
    }
}
