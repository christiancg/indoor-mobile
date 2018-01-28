﻿using System;

using indoor.Config;
using indoor.Models;
using indoor.Services;

using Xamarin.Forms;

namespace indoor.ViewModels
{
    public class ConfigurationViewModel : BaseViewModel
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
                if(value)
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

        public ConfigurationViewModel(INavigation navigation)
        {
            _navigation = navigation; // AND HERE
            Title = "LogIn";
            URLText = "Nombre indoor: ";
            MessagingCenter.Subscribe<ConfigurationPage>(this, "LogIn", async (obj) =>
            {
                if (!string.IsNullOrEmpty(this.RestURLBase) && !string.IsNullOrEmpty(this.Usuario) && !string.IsNullOrEmpty(this.Password))
                {
                    Configuracion.Instancia.restBaseUrl =  this.UsarComunicacionRest ?  "http://" + this.RestURLBase : this.RestURLBase;
                    Configuracion.Instancia.usuario = this.Usuario;
                    Configuracion.Instancia.contrasenia = this.Password;
                    Configuracion.Instancia.saveConfiguration = this.Recordar;
                    Configuracion.Instancia.useRestComunicationSchema = this.UsarComunicacionRest;

                    EstadoIndoor estado = await IndoorComunicaitionFactory.GetInstance().GetEstado();
                    if (estado != null)
                    {
                        if (this.Recordar)
                            ConfiguracionSaverRetriever.SaveProperties();
                        await _navigation.PushAsync(new NavigationPage(new MainPage()));
                    }
                    else
                    {
                        ConfiguracionSaverRetriever.DeleteProperties();
                        await Application.Current.MainPage.DisplayAlert("Error", "Error al realizar login, revise los datos", "Error");
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Debe completar todos los datos", "Error");
                }
            });
        }

    }
}
