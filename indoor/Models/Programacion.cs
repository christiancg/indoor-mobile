using System;
namespace indoor.Models
{
    public class Programacion
    {
        public int id
        {
            get;
            set;
        }

        public ConfigGPIO gpio
        {
            get;
            set;
        }

        public TimeSpan hora1
        {
            get;
            set;
        }

        public int duracion
        {
            get;
            set;
        }

        public Boolean prender
        {
            get;
            set;
        }

        public String descripcion
        {
            get;
            set;
        }

        public Boolean habilitado
        {
            get;
            set;
        }

        public Programacion()
        {
        }

        public Programacion(int id, ConfigGPIO gpio, TimeSpan hora1, Boolean prender, String descripcion, Boolean habilitado)
        {
            this.id = id;
            this.gpio = gpio;
            this.hora1 = hora1;
            this.prender = prender;
            this.descripcion = descripcion;
            this.habilitado = habilitado;
        }

        public Programacion(int id, ConfigGPIO gpio, TimeSpan hora1, int duracion , Boolean prender, String descripcion, Boolean habilitado)
        {
            if (duracion <= 0)
                throw new ArgumentException("La duracion debe ser mayor a 0");
            this.id = id;
            this.gpio = gpio;
            this.hora1 = hora1;
            this.duracion = duracion;
            this.prender = prender;
            this.descripcion = descripcion;
            this.habilitado = habilitado;
        }

    }
}
