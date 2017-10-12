using System;
using System.Collections.Generic;
using System.Net.Http;
using indoor.Models;
using indoor.Config;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace indoor.Services
{
    public class IndoorRestService : IIndoorRestService
    {
        HttpClient cliente;
        String baseUrl;

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
                    var contenido = await response.Content.ReadAsStringAsync();
                    JObject json = JObject.Parse(contenido);
                    resultado = new EstadoIndoor((Boolean)json[""]) 
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return resultado;
        }

        public Task<List<Evento>> GetEventosPorFecha(DateTime desde, DateTime hasta)
        {
            throw new NotImplementedException();
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
