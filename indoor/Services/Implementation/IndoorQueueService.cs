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
                request = new QueueMessageRequest(Configuracion.Instancia.usuario, Configuracion.Instancia.contrasenia, "agregarProgramacion", aAgregar);
                QueueMessageSenderReciever<Boolean> client = new QueueMessageSenderReciever<Boolean>(request);
                response = await client.CallForResponse();
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
            return response;
        }

        public async Task<bool> EditProgramacion(Programacion aEditar)
        {
            bool response = false;
            QueueMessageRequest request = null;
            try
            {
                request = new QueueMessageRequest(Configuracion.Instancia.usuario, Configuracion.Instancia.contrasenia, "editarProgramacion", aEditar);
                QueueMessageSenderReciever<Boolean> client = new QueueMessageSenderReciever<Boolean>(request);
                response = await client.CallForResponse();
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
            return response;
        }

        public async Task<bool> FanExtra(bool prender)
        {
            bool response = false;
            QueueMessageRequest request = null;
            List<string> getParameters = new List<string>() { prender.ToString() };
            try
            {
                request = new QueueMessageRequest(Configuracion.Instancia.usuario, Configuracion.Instancia.contrasenia, "fanExtra", getParameters);
                QueueMessageSenderReciever<Boolean> client = new QueueMessageSenderReciever<Boolean>(request);
                response = await client.CallForResponse();
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
            return response;
        }

        public async Task<bool> FanIntra(bool prender)
        {
            bool response = false;
            QueueMessageRequest request = null;
            List<string> getParameters = new List<string>() { prender.ToString() };
            try
            {
                request = new QueueMessageRequest(Configuracion.Instancia.usuario, Configuracion.Instancia.contrasenia, "fanIntra", getParameters);
                QueueMessageSenderReciever<Boolean> client = new QueueMessageSenderReciever<Boolean>(request);
                response = await client.CallForResponse();
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
            return response;
        }

        public async Task<EstadoIndoor> GetEstado()
        {
            EstadoIndoor response = null;
            QueueMessageRequest request = null;
            try
            {
                request = new QueueMessageRequest(Configuracion.Instancia.usuario, Configuracion.Instancia.contrasenia, "obtenerConfiguraciones");
                QueueMessageSenderReciever<EstadoIndoor> client = new QueueMessageSenderReciever<EstadoIndoor>(request);
                response = await client.CallForResponse();
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
            return response;
        }

        public async Task<List<Evento>> GetEventosPorFecha(DateTime desde, DateTime hasta)
        {
            List<Evento> response = null;
            QueueMessageRequest request = null;
            List<string> getParameters = new List<string>() { desde.ToString(), hasta.ToString() };
            try
            {
                request = new QueueMessageRequest(Configuracion.Instancia.usuario, Configuracion.Instancia.contrasenia, "obtenerEventosPorFecha", getParameters);
                QueueMessageSenderReciever<List<Evento>> client = new QueueMessageSenderReciever<List<Evento>>(request);
                response = await client.CallForResponse();
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
            return response;
        }

        public async Task<List<Evento>> GetEventosPorFechaYTipo(DateTime desde, DateTime hasta, ConfigGPIO tipo)
        {
            List<Evento> response = null;
            QueueMessageRequest request = null;
            List<string> getParameters = new List<string>() { desde.ToString(), hasta.ToString(), tipo.ToString() };
            try
            {
                request = new QueueMessageRequest(Configuracion.Instancia.usuario, Configuracion.Instancia.contrasenia, "obtenerEventosPorFecha", getParameters);
                QueueMessageSenderReciever<List<Evento>> client = new QueueMessageSenderReciever<List<Evento>>(request);
                response = await client.CallForResponse();
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
            return response;
        }

        public async Task<HumedadYTemperatura> GetHumedadYTemperatura()
        {
            HumedadYTemperatura response = null;
            QueueMessageRequest request = null;
            try
            {
                request = new QueueMessageRequest(Configuracion.Instancia.usuario, Configuracion.Instancia.contrasenia, "humedadYTemperatura");
                QueueMessageSenderReciever<HumedadYTemperatura> client = new QueueMessageSenderReciever<HumedadYTemperatura>(request);
                response = await client.CallForResponse();
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
            return response;
        }

        public async Task<List<Programacion>> GetProgramaciones()
        {
            List<Programacion> response = null;
            QueueMessageRequest request = null;
            try
            {
                request = new QueueMessageRequest(Configuracion.Instancia.usuario, Configuracion.Instancia.contrasenia, "obtenerProgramaciones");
                QueueMessageSenderReciever<List<Programacion>> client = new QueueMessageSenderReciever<List<Programacion>>(request);
                response = await client.CallForResponse();
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
            return response;
        }

        public async Task<bool> HabilitarDeshabilitarProgramacion(int idProgramacion, bool estado)
        {
            bool response = false;
            QueueMessageRequest request = null;
            List<string> getParameters = new List<string>() { idProgramacion.ToString(), estado.ToString() };
            try
            {
                request = new QueueMessageRequest(Configuracion.Instancia.usuario, Configuracion.Instancia.contrasenia, "cambiarEstadoProgramacion", getParameters);
                QueueMessageSenderReciever<Boolean> client = new QueueMessageSenderReciever<Boolean>(request);
                response = await client.CallForResponse();
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
            return response;
        }

        public async Task<bool> Luz(bool prender)
        {
            bool response = false;
            QueueMessageRequest request = null;
            List<string> getParameters = new List<string>() { prender.ToString() };
            try
            {
                request = new QueueMessageRequest(Configuracion.Instancia.usuario, Configuracion.Instancia.contrasenia, "luz", getParameters);
                QueueMessageSenderReciever<Boolean> client = new QueueMessageSenderReciever<Boolean>(request);
                response = await client.CallForResponse();
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
            return response;
        }

        public async Task<ImagenIndoor> ObtenerImagen()
        {
            ImagenIndoor response = null;
            QueueMessageRequest request = null;
            try
            {
                request = new QueueMessageRequest(Configuracion.Instancia.usuario, Configuracion.Instancia.contrasenia, "obtenerProgramaciones");
                QueueMessageSenderReciever<ImagenIndoor> client = new QueueMessageSenderReciever<ImagenIndoor>(request);
                response = await client.CallForResponse();
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
            return response;
        }

        public async Task<bool> RegarSegundos(int segundos)
        {
            bool response = false;
            QueueMessageRequest request = null;
            List<string> getParameters = new List<string>() { segundos.ToString() };
            try
            {
                request = new QueueMessageRequest(Configuracion.Instancia.usuario, Configuracion.Instancia.contrasenia, "regarSegundos", getParameters);
                QueueMessageSenderReciever<Boolean> client = new QueueMessageSenderReciever<Boolean>(request);
                response = await client.CallForResponse();
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
            return response;
        }
    }
}
