using System;

namespace indoor.ViewModels
{
    public class ConfigurationViewModel : BaseViewModel
    {
        public bool TieneLuz
        {
            get;
            set;
        }

        public bool TieneBomba
        {
            get;
            set;
        }

        public bool TieneFanIntra
        {
            get;
            set;
        }

        public bool TieneFanExtra
        {
            get;
            set;
        }

        public bool TieneHumYTemp
        {
            get;
            set;
        }

        public bool TieneCamara
        {
            get;
            set;
        }

        public string NombreIndoor
        {
            get;
            set;
        }

        public string UsuarioAdm
        {
            get;
            set;
        }

        public string PasswordAdm
        {
            get;
            set;
        }

        public ConfigurationViewModel()
        {
        }
    }
}
