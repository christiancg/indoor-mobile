using System;

using System.Collections.Generic;

using indoor.Config;
using indoor.Models;
using indoor.Services;
using indoor.Views;
using Robotics.Mobile.Core.Bluetooth.LE;
using Xamarin.Forms;

namespace indoor.ViewModels
{
    public class ConnectionViewModel : BaseViewModel
    {
        private bool _ShowHttpLabel;
        public bool ShowHttpLabel
        {
            get
            {
                return _ShowHttpLabel;
            }
            set
            {
                _ShowHttpLabel = value;
                OnPropertyChanged();
            }
        }

        private string _URLText;
        public string URLText
        {
            get
            {
                return _URLText;
            }
            set
            {
                _URLText = value;
                OnPropertyChanged();
            }
        }

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

        private bool _BotonHabilitado;
        public bool BotonHabilitado
        {
            get
            {
                return _BotonHabilitado;
            }
            set
            {
                _BotonHabilitado = value;
                OnPropertyChanged();
            }
        }

        private bool _UsarComunicacionRest;
        public bool UsarComunicacionRest
        {
            get
            {
                return _UsarComunicacionRest;
            }
            set
            {
                _UsarComunicacionRest = value;
                ShowHttpLabel = value;
                if (value)
                {
                    URLText = "URL: ";
                }
                else
                {
                    URLText = "Nombre indoor: ";
                }
            }
        }

        public Command NuevaProgramacionCommand { get; set; }

        private INavigation _navigation;

        public ConnectionViewModel(INavigation navigation)
        {
            _navigation = navigation; // AND HERE
            Title = "LogIn";
            URLText = "Nombre indoor: ";
            this.BotonHabilitado = true;
        }

        public void setMensajes()
        {
            MessagingCenter.Subscribe<ConnectionPage>(this, "LogIn", async (obj) =>
            {
                if (!string.IsNullOrEmpty(this.RestURLBase) && !string.IsNullOrEmpty(this.Usuario) && !string.IsNullOrEmpty(this.Password))
                {
                    Configuracion.Instancia.restBaseUrl = this.UsarComunicacionRest ? "http://" + this.RestURLBase : this.RestURLBase;
                    Configuracion.Instancia.usuario = this.Usuario;
                    Configuracion.Instancia.contrasenia = this.Password;
                    Configuracion.Instancia.saveConfiguration = this.Recordar;
                    Configuracion.Instancia.useRestComunicationSchema = this.UsarComunicacionRest;

                    this.BotonHabilitado = false;
                    List<ConfigGPIO> configs = await IndoorComunicaitionFactory.GetInstance().GetConfiguraciones();
                    if (configs != null)
                    {
                        if (this.Recordar)
                            ConfiguracionSaverRetriever.SaveProperties();
                        await _navigation.PushAsync(new NavigationPage(new MainPage(configs)));
                    }
                    else
                    {
                        ConfiguracionSaverRetriever.DeleteProperties();
                        await Application.Current.MainPage.DisplayAlert("Error", "Error al realizar login, revise los datos", "Error");
                    }
                    this.BotonHabilitado = true;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Debe completar todos los datos", "Error");
                }
            });
            MessagingCenter.Subscribe<ConnectionPage, IAdapter>(this, "Configurar", async (obj, arg) =>
            {
                await _navigation.PushAsync(new NavigationPage(new ConfigurationPage(arg)));
            });
        }

        public void unsetMensajes()
        {
            MessagingCenter.Unsubscribe<ConnectionPage>(this, "LogIn");
            MessagingCenter.Unsubscribe<ConnectionPage, IAdapter>(this, "Configurar");
        }

    }
}
