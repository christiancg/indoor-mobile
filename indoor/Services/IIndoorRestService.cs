using System;
using indoor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace indoor.Services
{
    public interface IIndoorRestService
    {
        Task<EstadoIndoor>> GetEstado();
        Task<List<Evento>> GetEventosPorFecha(DateTime desde, DateTime hasta);
        Task<List<Evento>> GetEventosPorFechaYTipo(DateTime desde, DateTime hasta, ConfigGPIO tipo);
        Task<HumedadYTemperatura> GetHumedadYTemperatura();
        Task<List<Programacion>> GetProgramaciones();

        Task<Boolean> AddProgramacion(Programacion aAgregar);
        Task<Boolean> PrenderLuz();
        Task<Boolean> ApagarLuz();
        Task<Boolean> PrenderFanIntra();
        Task<Boolean> ApagarFanIntra();
        Task<Boolean> PrenderFanExtra();
        Task<Boolean> ApagarFanExtra();
        Task<Boolean> RegarSegundos(int segundos);
    }
}
