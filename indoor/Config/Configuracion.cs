using System;
namespace indoor.Config
{
    public sealed class Configuracion
    {
        private static readonly Configuracion instance = new Configuracion();

        private Configuracion()
        {
        }

        public static Configuracion Instancia{ get { return instance; }}

        public String restBaseUrl
        {
            get;
            set;
        }

        public String usuario
        {
            get;
            set;
        }

        public String contrasenia
        {
            get;
            set;
        }

        public Boolean useRestComunicationSchema
        {
            get;
            set;
        }

        public Boolean saveConfiguration
        {
            get;
            set;
        }
    }
}