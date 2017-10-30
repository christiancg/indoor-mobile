using System;
using indoor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace indoor.Services
{
    public interface IIndoorRestService
    {
        Task<EstadoIndoor> GetEstado();
        Task<List<Evento>> GetEventosPorFecha(DateTime desde, DateTime hasta);
        Task<List<Evento>> GetEventosPorFechaYTipo(DateTime desde, DateTime hasta, ConfigGPIO tipo);
        Task<HumedadYTemperatura> GetHumedadYTemperatura();
        Task<List<Programacion>> GetProgramaciones();

        Task<Boolean> AddProgramacion(Programacion aAgregar);
        Task<Boolean> Luz(bool prender);
        Task<Boolean> FanIntra(bool prender);
        Task<Boolean> FanExtra(bool prender);
        Task<Boolean> RegarSegundos(int segundos);
        Task<ImagenIndoor> ObtenerImagen();
    }
}
