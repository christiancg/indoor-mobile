using System;
namespace indoor.Models
{
    public class ImagenIndoor
    {
        public bool EstadoTomaImagen
        {
            get;
            set;
        }

        public String B64Image
        {
            get;
            set;
        }

        public DateTime FechaImagen
        {
            get;
            set;
        }

        public ImagenIndoor(String mensaje)
        {
            this.EstadoTomaImagen = false;
            this.B64Image = mensaje;
        }

        public ImagenIndoor(String mensaje, DateTime fecha)
        {
            this.EstadoTomaImagen = true;
            this.B64Image = mensaje;
            this.FechaImagen = fecha;
        }
    }
}
