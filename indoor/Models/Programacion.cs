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

        public TimeSpan hora2
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

        public Programacion(int id, ConfigGPIO gpio, TimeSpan hora1, TimeSpan hora2 , Boolean prender, String descripcion, Boolean habilitado)
        {
            if (hora1 > hora2)
                throw new ArgumentException("El valor hora2 debe ser mayor a hora1");
            this.id = id;
            this.gpio = gpio;
            this.hora1 = hora1;
            this.hora2 = hora2;
            this.prender = prender;
            this.descripcion = descripcion;
            this.habilitado = habilitado;
        }

    }
}
