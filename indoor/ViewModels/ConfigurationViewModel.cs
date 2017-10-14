using System;

using indoor.Config;
using indoor.Models;

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

        public Command NuevaProgramacionCommand { get; set; }

        private INavigation _navigation;

        public ConfigurationViewModel(INavigation navigation)
        {
            _navigation = navigation; // AND HERE
            Title = "LogIn";
            MessagingCenter.Subscribe<ConfigurationPage>(this, "LogIn", async (obj) =>
            {
                Configuracion.Instancia.restBaseUrl = this.RestURLBase;
                Configuracion.Instancia.usuario = this.Usuario;
                Configuracion.Instancia.contrasenia = this.Password;

                EstadoIndoor estado = await DataStore.GetEstado();
                if(estado != null)
                {
                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        await _navigation.PushAsync(new NavigationPage(new MainPage()));
                    }
                    else
                    {
                        await _navigation.PushAsync(new MainPage());
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Error al realizar login, revise los datos", "Error"); 
                }
            });
        }
    }
}
