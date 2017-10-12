using System;
namespace indoor.Models
{
    public class Evento
    {
        public DateTime fecha
        {
            get;
            set;
        }

        public ConfigGPIO dispositivo
        {
            get;
            set;
        }

        public Boolean estado
        {
            get;
            set;
        }

        public HumedadYTemperatura estadoHumYTemp
        {
            get;
            set;
        }

        public String descripcion
        {
            get;
            set;
        }

        public Evento(DateTime fecha, ConfigGPIO dispositivo, Boolean estado, String descripcion)
        {
            this.fecha = fecha;
            this.dispositivo = dispositivo;
            this.estado = estado;
            this.descripcion = descripcion;
        }

        public Evento(DateTime fecha, HumedadYTemperatura estadoHumYTemp)
        {
            this.fecha = fecha;
            this.estadoHumYTemp = estadoHumYTemp;
            if (estadoHumYTemp != null)
            {
                this.descripcion = "Medicion OK";
                this.estado = true;
            }
            else
            {
                this.descripcion = "Error al realizar medicion";
                this.estado = false;
            }
        }
    }
}
