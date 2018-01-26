using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using indoor.Models;
using indoor.Config;
using indoor.Services.Parser;
using System.Threading.Tasks;
using System.Text;

namespace indoor.Services.Implementation
{
    public class IndoorRestService : IIndoorComunicationService
    {
        HttpClient cliente;
        const String formatoFecha = "dd-MM-yyyyThh:mm:ss";

        public IndoorRestService()
        {
            cliente = new HttpClient();
            var authData = string.Format("{0}:{1}", Configuracion.Instancia.usuario , Configuracion.Instancia.contrasenia);
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        }

        public async Task<bool> AddProgramacion(Programacion aAgregar)
        {
            Boolean resultado = false;
            try
            {
                Uri uri = new Uri(Configuracion.Instancia.restBaseUrl + "/agregarProgramacion");
                var toSend = RequestParser.parseAgregarProgramacionRequest(aAgregar);
                var response = await cliente.PostAsync(uri, toSend);
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

        public async Task<bool> EditProgramacion(Programacion aEditar)
        {
            Boolean resultado = false;
            try
            {
                Uri uri = new Uri(Configuracion.Instancia.restBaseUrl + "/editarProgramacion");
                var toSend = RequestParser.parseEditarProgramacionRequest(aEditar);
                var response = await cliente.PutAsync(uri, toSend);
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

        public async Task<bool> HabilitarDeshabilitarProgramacion(int idProgramacion, bool estado)
        {
            Boolean resultado = false;
            try
            {
                Uri uri = new Uri(Configuracion.Instancia.restBaseUrl + "/cambiarEstadoProgramacion/" + idProgramacion + "/" + estado.ToString());
                var response = await cliente.PutAsync(uri, null);
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
                    resultado = ResponseParser.parseEstadoIndoor(contenido);
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
                    resultado = ResponseParser.parseListaEventos(contenido);
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
                    resultado = ResponseParser.parseListaEventos(contenido);
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
                    resultado = ResponseParser.parseHumedadYTemperatura(contenido);
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
                    resultado = ResponseParser.parseListaProgramaciones(contenido);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return resultado;
        }

        public async Task<bool> FanExtra(bool prender)
        {
            return await EjecutarGetBasico("fanExtra", prender);
        }

        public async Task<bool> FanIntra(bool prender)
        {
            return await EjecutarGetBasico("fanIntra", prender);
        }

        public async Task<bool> Luz(bool prender)
        {
            return await EjecutarGetBasico("luz", prender);
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

        private async Task<bool> EjecutarGetBasico(String rutaRelativa, bool prender)
        {
            Boolean resultado = false;
            try
            {
                Uri uri = new Uri(Configuracion.Instancia.restBaseUrl + "/" + rutaRelativa + "/" + (prender == true ? "true" : "false"));
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

        public async Task<ImagenIndoor> ObtenerImagen()
        {
            ImagenIndoor resultado = null;
            try
            {
                Uri uri = new Uri(Configuracion.Instancia.restBaseUrl + "/obtenerImagenIndoor");
                var response = await cliente.GetAsync(uri);
                String contenido = await response.Content.ReadAsStringAsync();
                resultado = ResponseParser.parseImagenIndoor(contenido);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                resultado = new ImagenIndoor(ex.ToString());
            }
            return resultado;
        }

    }
}
