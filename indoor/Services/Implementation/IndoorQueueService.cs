using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using indoor.Models;
using indoor.Utils;
using indoor.Config;

namespace indoor.Services.Implementation
{
    public class IndoorQueueService : IIndoorComunicationService
    {
        public IndoorQueueService()
        {
        }

        public async Task<bool> AddProgramacion(Programacion aAgregar)
        {
            bool response = false;
            QueueMessageRequest request = null;
            try
            {
                request = new QueueMessageRequest(Configuracion.Instancia.usuario, Configuracion.Instancia.contrasenia, "AddProgramacion", aAgregar);
                QueueMessageSenderReciever<Boolean> client = new QueueMessageSenderReciever<Boolean>(request);
                response = await client.CallForResponse();
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace); 
            }
            return response;
        }

        public Task<bool> EditProgramacion(Programacion aEditar)
        {
            throw new NotImplementedException();
        }

        public Task<bool> FanExtra(bool prender)
        {
            throw new NotImplementedException();
        }

        public Task<bool> FanIntra(bool prender)
        {
            throw new NotImplementedException();
        }

        public Task<EstadoIndoor> GetEstado()
        {
            throw new NotImplementedException();
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

        public Task<bool> HabilitarDeshabilitarProgramacion(int idProgramacion, bool estado)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Luz(bool prender)
        {
            throw new NotImplementedException();
        }

        public Task<ImagenIndoor> ObtenerImagen()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegarSegundos(int segundos)
        {
            throw new NotImplementedException();
        }
    }
}
