using System;
using System.Collections.Generic;
using System.Net.Http;
using indoor.Models;
using indoor.Config;
using indoor.Parser;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace indoor.Services
{
    public class IndoorRestService : IIndoorRestService
    {
        HttpClient cliente;
        String baseUrl;
        const String formatoFecha = "dd-MM-yyyyThh:mm:ss";

        public IndoorRestService()
        {
            cliente = new HttpClient();
            this.baseUrl = Configuracion.Instancia.restBaseUrl;
        }

        public Task<bool> AddProgramacion(Programacion aAgregar)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ApagarFanExtra()
        {
            throw new NotImplementedException();
        }

        public Task<bool> ApagarFanIntra()
        {
            throw new NotImplementedException();
        }

        public Task<bool> ApagarLuz()
        {
            throw new NotImplementedException();
        }

        public async Task<EstadoIndoor> GetEstado()
        {
            EstadoIndoor resultado = null;
            try
            {
                Uri uri = new Uri(this.baseUrl + "/obtenerConfiguraciones");
                var response = await cliente.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    String contenido = await response.Content.ReadAsStringAsync();
                    resultado = RestResponseParser.parseEstadoIndoor(contenido);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return resultado;
        }

        public async Task<List<Evento>> GetEventosPorFecha(DateTime desde, DateTime hasta)
        {
            List<Evento> resultado = null;
            try
            {
                Uri uri = new Uri(this.baseUrl + "/obtenerEventosPorFecha/" + desde.ToString(formatoFecha) + "/" + hasta.ToString(formatoFecha));
                var response = await cliente.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    String contenido = await response.Content.ReadAsStringAsync();

                    Boolean estadoLuz = (from jel in json.Children() where jel["desc"].ToString() == "luz" select Boolean.Parse(jel["estado"].ToString())).FirstOrDefault();
                    Boolean estadoFanIntra = (from jel in json.Children() where jel["desc"].ToString() == "fanintra" select Boolean.Parse(jel["estado"].ToString())).FirstOrDefault();
                    Boolean estadoFanExtra = (from jel in json.Children() where jel["desc"].ToString() == "fanextra" select Boolean.Parse(jel["estado"].ToString())).FirstOrDefault();
                    resultado = new EstadoIndoor(estadoLuz, estadoFanIntra, estadoFanExtra);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return resultado;
        }

        public Task<List<Evento>> GetEventosPorFechaYTipo(DateTime desde, DateTime hasta, ConfigGPIO tipo)
        {
            throw new NotImplementedException();
        }

        public Task<HumedadYTemperatura> GetHumedadYTemperatura()
        {
            throw new NotImplementedException();
        }

        public Task<List<Programacion>> GetProgramaciones()
        {
            throw new NotImplementedException();
        }

        public Task<bool> PrenderFanExtra()
        {
            throw new NotImplementedException();
        }

        public Task<bool> PrenderFanIntra()
        {
            throw new NotImplementedException();
        }

        public Task<bool> PrenderLuz()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegarSegundos(int segundos)
        {
            throw new NotImplementedException();
        }

        void IIndoorRestService.GetEstado()
        {
            throw new NotImplementedException();
        }
    }
}
