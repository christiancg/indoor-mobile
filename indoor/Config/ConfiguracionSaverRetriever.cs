using System;
using Xamarin.Forms;

namespace indoor.Config
{
    public static class ConfiguracionSaverRetriever
    {
        public static bool readConfiguration()
        {
            Boolean result = false;
            try
            {
                var url = Application.Current.Properties["restBaseUrl"].ToString();
                var usr = Application.Current.Properties["usuario"].ToString();
                var pass = Application.Current.Properties["contrasenia"].ToString();
                Configuracion.Instancia.restBaseUrl = url;
                Configuracion.Instancia.usuario = usr;
                Configuracion.Instancia.contrasenia = pass;
                if (!string.IsNullOrEmpty(url) && !string.IsNullOrEmpty(usr) && !string.IsNullOrEmpty(pass))
                    result = true;
                else
                    result = false;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return result;
        }

        public static bool saveConfiguration()
        {
            Boolean result = false;
            try
            {
                Application.Current.Properties["restBaseUrl"] = Configuracion.Instancia.restBaseUrl;
                Application.Current.Properties["usuario"] = Configuracion.Instancia.usuario;
                Application.Current.Properties["contrasenia"] = Configuracion.Instancia.contrasenia;
                result = true;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return result;
        }
    }
}
