using System;
using Plugin.Settings;
namespace indoor.Config
{
    public static class ConfiguracionSaverRetriever
    {
        public static bool SaveProperties()
        {
            try
            {
                CrossSettings.Current.AddOrUpdateValue("saveConfiguration", true);
                string restBaseUrl = Configuracion.Instancia.restBaseUrl.Contains("/") ? Configuracion.Instancia.restBaseUrl.Substring(Configuracion.Instancia.restBaseUrl.LastIndexOf('/') + 1) : Configuracion.Instancia.restBaseUrl;
                CrossSettings.Current.AddOrUpdateValue("restBaseUrl", restBaseUrl);
                CrossSettings.Current.AddOrUpdateValue("usuario", Configuracion.Instancia.usuario);
                CrossSettings.Current.AddOrUpdateValue("contrasenia", Configuracion.Instancia.contrasenia);
                CrossSettings.Current.AddOrUpdateValue("useRestComunicationSchema", Configuracion.Instancia.useRestComunicationSchema);
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }

        }

        public static bool RetrieveProperties()
        {
            try
            {
                Configuracion.Instancia.saveConfiguration = CrossSettings.Current.GetValueOrDefault("saveConfiguration", false);
                Configuracion.Instancia.restBaseUrl = CrossSettings.Current.GetValueOrDefault("restBaseUrl", "");
                Configuracion.Instancia.usuario = CrossSettings.Current.GetValueOrDefault("usuario", "");
                Configuracion.Instancia.contrasenia = CrossSettings.Current.GetValueOrDefault("contrasenia", "");
                Configuracion.Instancia.useRestComunicationSchema = CrossSettings.Current.GetValueOrDefault("useRestComunicationSchema", false);
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }

        public static bool DeleteProperties()
        {
            try
            {
                CrossSettings.Current.AddOrUpdateValue("saveConfiguration", false);
                CrossSettings.Current.Remove("restBaseUrl");
                CrossSettings.Current.Remove("usuario");
                CrossSettings.Current.Remove("contrasenia");
                CrossSettings.Current.Remove("useRestComunicationSchema");
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }
    }
}
