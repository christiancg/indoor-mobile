using System;
using System.Collections.Generic;
using System.Net.Http;
using indoor.Models;
using indoor.Config;
using indoor.Services.Parser;
using System.Threading.Tasks;

namespace indoor.Services
{
    public class IndoorRestService : IIndoorRestService
    {
        HttpClient cliente;
        const String formatoFecha = "dd-MM-yyyyThh:mm:ss";

        public IndoorRestService()
        {
            cliente = new HttpClient();
        }

        public async Task<bool> AddProgramacion(Programacion aAgregar)
        {
            Boolean resultado = false;
            try
            {
                Uri uri = new Uri(Configuracion.Instancia.restBaseUrl + "/obtenerConfiguraciones");
                var response = await cliente.PostAsync(uri,RestRequestParser.parseAgregarProgramacionRequest(aAgregar));
                if (response.IsSuccessStatusCode)
                {
                    String contenido = await response.Content.ReadAsStringAsync();
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return resultado;
        }

        public async Task<bool> ApagarFanExtra()
        {
            return await EjecutarGetBasico("apagarFanExtra");
        }

        public async Task<bool> ApagarFanIntra() 
        { 
            return await EjecutarGetBasico("apagarFanIntra"); 
        }


        public async Task<bool> ApagarLuz()
        {
            return await EjecutarGetBasico("apagarLuz");
        }

        public async Task<EstadoIndoor> GetEstado()
        {
            EstadoIndoor resultado = null;
            try
            {
                Uri uri = new Uri(Configuracion.Instancia.restBaseUrl + "/obtenerConfiguraciones");
                var response = await cliente.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    String contenido = await response.Content.ReadAsStringAsync();
                    resultado = RestResponseParser.parseEstadoIndoor(contenido);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return resultado;
        }

        public async Task<List<Evento>> GetEventosPorFecha(DateTime desde, DateTime hasta)
        {
            List<Evento> resultado = null;
            try
            {
                Uri uri = new Uri(Configuracion.Instancia.restBaseUrl + "/obtenerEventosPorFecha/" + desde.ToString(formatoFecha) + "/" + hasta.ToString(formatoFecha));
                var response = await cliente.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    String contenido = await response.Content.ReadAsStringAsync();
                    resultado = RestResponseParser.parseListaEventos(contenido);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return resultado;
        }

        public async Task<List<Evento>> GetEventosPorFechaYTipo(DateTime desde, DateTime hasta, ConfigGPIO tipo)
        {
            List<Evento> resultado = null;
            try
            {
                Uri uri = new Uri(Configuracion.Instancia.restBaseUrl + "/obtenerEventosPorFecha/" + desde.ToString(formatoFecha) + "/" + hasta.ToString(formatoFecha) + "/" + tipo.ToString());
                var response = await cliente.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    String contenido = await response.Content.ReadAsStringAsync();
                    resultado = RestResponseParser.parseListaEventos(contenido);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return resultado;
        }

        public async Task<HumedadYTemperatura> GetHumedadYTemperatura()
        {
            HumedadYTemperatura resultado = null;
            try
            {
                Uri uri = new Uri(Configuracion.Instancia.restBaseUrl + "/humedadYTemperatura");
                var response = await cliente.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    String contenido = await response.Content.ReadAsStringAsync();
                    resultado = RestResponseParser.parseHumedadYTemperatura(contenido);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return resultado;
        }

        public async Task<List<Programacion>> GetProgramaciones()
        {
            List<Programacion> resultado = null;
            try
            {
                Uri uri = new Uri(Configuracion.Instancia.restBaseUrl + "/obtenerProgramaciones");
                var response = await cliente.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    String contenido = await response.Content.ReadAsStringAsync();
                    resultado = RestResponseParser.parseListaProgramaciones(contenido);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return resultado;
        }

        public async Task<bool> PrenderFanExtra()
        {
            return await EjecutarGetBasico("prenderFanExtra");
        }

        public async Task<bool> PrenderFanIntra()
        {
            return await EjecutarGetBasico("prenderFanIntra");
        }

        public async Task<bool> PrenderLuz()
        {
            return await EjecutarGetBasico("prenderLuz");
        }

        public async Task<bool> RegarSegundos(int segundos)
        {
            bool resultado = false;
            try
            {
                Uri uri = new Uri(Configuracion.Instancia.restBaseUrl + "/regarSegundos/" + segundos.ToString());
                var response = await cliente.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    String contenido = await response.Content.ReadAsStringAsync();
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return resultado;
        }

        private async Task<bool> EjecutarGetBasico(String rutaRelativa)
        {
            Boolean resultado = false;
            try
            {
                Uri uri = new Uri(Configuracion.Instancia.restBaseUrl + "/" + rutaRelativa);
                var response = await cliente.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    String contenido = await response.Content.ReadAsStringAsync();
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return resultado;
        }
    }
}
