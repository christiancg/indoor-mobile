using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using indoor.Models;
using indoor.Utils;
using indoor.Config;
using indoor.Services.Parser;

namespace indoor.Services.Implementation
{
    public class IndoorQueueService : IIndoorComunicationService
    {
        const String formatoFecha = "dd-MM-yyyyThh:mm:ss";

        public IndoorQueueService()
        {
        }

        public async Task<bool> AddProgramacion(Programacion aAgregar)
        {
            bool toReturn = false;
            QueueMessageResponse response = null;
            QueueMessageRequest request = null;
            try
            {
                string toAdd = RequestParser.parseAgregarProgramacionRequest(aAgregar).ReadAsStringAsync().Result;
                request = new QueueMessageRequest(Configuracion.Instancia.usuario, Configuracion.Instancia.contrasenia, "agregarProgramacion", toAdd);
                QueueMessageSenderReciever client = new QueueMessageSenderReciever(request);
                response = await client.CallForResponse();
                toReturn = response.Success;
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
            return toReturn;
        }

        public async Task<bool> EditProgramacion(Programacion aEditar)
        {
            bool toReturn = false;
            QueueMessageResponse response = null;
            QueueMessageRequest request = null;
            try
            {
                string toEdit = RequestParser.parseEditarProgramacionRequest(aEditar).ReadAsStringAsync().Result;
                request = new QueueMessageRequest(Configuracion.Instancia.usuario, Configuracion.Instancia.contrasenia, "editarProgramacion", toEdit);
                QueueMessageSenderReciever client = new QueueMessageSenderReciever(request);
                response = await client.CallForResponse();
                toReturn = response.Success;
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
            return toReturn;
        }

        public async Task<bool> FanExtra(bool prender)
        {
            bool toReturn = false;
            QueueMessageResponse response = null;
            QueueMessageRequest request = null;
            List<string> getParameters = new List<string>() { prender.ToString() };
            try
            {
                request = new QueueMessageRequest(Configuracion.Instancia.usuario, Configuracion.Instancia.contrasenia, "fanExtra", getParameters);
                QueueMessageSenderReciever client = new QueueMessageSenderReciever(request);
                response = await client.CallForResponse();
                toReturn = response.Success;
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
            return toReturn;
        }

        public async Task<bool> FanIntra(bool prender)
        {
            bool toReturn = false;
            QueueMessageResponse response = null;
            QueueMessageRequest request = null;
            List<string> getParameters = new List<string>() { prender.ToString() };
            try
            {
                request = new QueueMessageRequest(Configuracion.Instancia.usuario, Configuracion.Instancia.contrasenia, "fanIntra", getParameters);
                QueueMessageSenderReciever client = new QueueMessageSenderReciever(request);
                response = await client.CallForResponse();
                toReturn = response.Success;
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
            return toReturn;
        }

        public async Task<EstadoIndoor> GetEstado()
        {
            EstadoIndoor toReturn = null;
            QueueMessageResponse response = null;
            QueueMessageRequest request = null;
            try
            {
                request = new QueueMessageRequest(Configuracion.Instancia.usuario, Configuracion.Instancia.contrasenia, "obtenerConfiguraciones");
                QueueMessageSenderReciever client = new QueueMessageSenderReciever(request);
                response = await client.CallForResponse();
                toReturn = ResponseParser.parseEstadoIndoor(response.Result);
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
            return toReturn;
        }

        public async Task<List<Evento>> GetEventosPorFecha(DateTime desde, DateTime hasta)
        {
            List<Evento> toReturn = null;
            QueueMessageResponse response = null;
            QueueMessageRequest request = null;
            List<string> getParameters = new List<string>() { desde.ToString(formatoFecha), hasta.ToString(formatoFecha) };
            try
            {
                request = new QueueMessageRequest(Configuracion.Instancia.usuario, Configuracion.Instancia.contrasenia, "obtenerEventosPorFecha", getParameters);
                QueueMessageSenderReciever client = new QueueMessageSenderReciever(request);
                response = await client.CallForResponse();
                toReturn = ResponseParser.parseListaEventos(response.Result);
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
            return toReturn;
        }

        public async Task<List<Evento>> GetEventosPorFechaYTipo(DateTime desde, DateTime hasta, ConfigGPIO tipo)
        {
            List<Evento> toReturn = null;
            QueueMessageResponse response = null;
            QueueMessageRequest request = null;
            List<string> getParameters = new List<string>() { desde.ToString(formatoFecha), hasta.ToString(formatoFecha), tipo.ToString() };
            try
            {
                request = new QueueMessageRequest(Configuracion.Instancia.usuario, Configuracion.Instancia.contrasenia, "obtenerEventosPorFecha", getParameters);
                QueueMessageSenderReciever client = new QueueMessageSenderReciever(request);
                response = await client.CallForResponse();
                toReturn = ResponseParser.parseListaEventos(response.Result);
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
            return toReturn;
        }

        public async Task<HumedadYTemperatura> GetHumedadYTemperatura()
        {
            HumedadYTemperatura toReturn = null;
            QueueMessageResponse response = null;
            QueueMessageRequest request = null;
            try
            {
                request = new QueueMessageRequest(Configuracion.Instancia.usuario, Configuracion.Instancia.contrasenia, "humedadYTemperatura");
                QueueMessageSenderReciever client = new QueueMessageSenderReciever(request);
                response = await client.CallForResponse();
                toReturn = ResponseParser.parseHumedadYTemperatura(response.Result);
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
            return toReturn;
        }

        public async Task<List<Programacion>> GetProgramaciones()
        {
            List<Programacion> toReturn = null;
            QueueMessageResponse response = null;
            QueueMessageRequest request = null;
            try
            {
                request = new QueueMessageRequest(Configuracion.Instancia.usuario, Configuracion.Instancia.contrasenia, "obtenerProgramaciones");
                QueueMessageSenderReciever client = new QueueMessageSenderReciever(request);
                response = await client.CallForResponse();
                toReturn = ResponseParser.parseListaProgramaciones(response.Result);
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
            return toReturn;
        }

        public async Task<bool> HabilitarDeshabilitarProgramacion(int idProgramacion, bool estado)
        {
            bool toReturn = false;
            QueueMessageResponse response = null;
            QueueMessageRequest request = null;
            List<string> getParameters = new List<string>() { idProgramacion.ToString(), estado.ToString() };
            try
            {
                request = new QueueMessageRequest(Configuracion.Instancia.usuario, Configuracion.Instancia.contrasenia, "cambiarEstadoProgramacion", getParameters);
                QueueMessageSenderReciever client = new QueueMessageSenderReciever(request);
                response = await client.CallForResponse();
                toReturn = response.Success;
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
            return toReturn;
        }

        public async Task<bool> Luz(bool prender)
        {
            bool toReturn = false;
            QueueMessageResponse response = null;
            QueueMessageRequest request = null;
            List<string> getParameters = new List<string>() { prender.ToString() };
            try
            {
                request = new QueueMessageRequest(Configuracion.Instancia.usuario, Configuracion.Instancia.contrasenia, "luz", getParameters);
                QueueMessageSenderReciever client = new QueueMessageSenderReciever(request);
                response = await client.CallForResponse();
                toReturn = response.Success;
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
            return toReturn;
        }

        public async Task<ImagenIndoor> ObtenerImagen()
        {
            ImagenIndoor toReturn = null;
            QueueMessageResponse response = null;
            QueueMessageRequest request = null;
            try
            {
                request = new QueueMessageRequest(Configuracion.Instancia.usuario, Configuracion.Instancia.contrasenia, "obtenerImagen");
                QueueMessageSenderReciever client = new QueueMessageSenderReciever(request);
                response = await client.CallForResponse();
                toReturn = ResponseParser.parseImagenIndoor(response.Result);
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
            return toReturn;
        }

        public async Task<bool> RegarSegundos(int segundos)
        {
            bool toReturn = false;
            QueueMessageResponse response = null;
            QueueMessageRequest request = null;
            List<string> getParameters = new List<string>() { segundos.ToString() };
            try
            {
                request = new QueueMessageRequest(Configuracion.Instancia.usuario, Configuracion.Instancia.contrasenia, "regarSegundos", getParameters);
                QueueMessageSenderReciever client = new QueueMessageSenderReciever(request);
                response = await client.CallForResponse();
                toReturn = response.Success;
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
            return toReturn;
        }
    }
}
