﻿using System;
using indoor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace indoor.Services
{
    public interface IIndoorComunicationService
    {
        Task<List<ConfigGPIO>> GetConfiguraciones();
        Task<EstadoIndoor> GetEstado();
        Task<List<Evento>> GetEventosPorFecha(DateTime desde, DateTime hasta);
        Task<List<Evento>> GetEventosPorFechaYTipo(DateTime desde, DateTime hasta, ConfigGPIO tipo);
        Task<HumedadYTemperatura> GetHumedadYTemperatura();
        Task<List<Programacion>> GetProgramaciones();

        Task<Boolean> AddProgramacion(Programacion aAgregar);
        Task<Boolean> EditProgramacion(Programacion aEditar);
        Task<Boolean> HabilitarDeshabilitarProgramacion(int idProgramacion, bool estado);
        Task<Boolean> BorrarProgramacion(int idProgramacion);
        Task<Boolean> Luz(bool prender);
        Task<Boolean> FanIntra(bool prender);
        Task<Boolean> FanExtra(bool prender);
        Task<Boolean> RegarSegundos(int segundos);
        Task<ImagenIndoor> ObtenerImagen();
    }
}
