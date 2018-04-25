using System;
namespace indoor.Models
{
    public class Alert
    {      
        public string Titulo
		{
			get;
			set;
		}

        public string Mensaje
		{
			get;
			set;         
		}

        public string TextoBoton
		{
			get;
			set;
		}

		public Alert(string Titulo, string  Mensaje, string TextoBoton)
		{
			this.Titulo = Titulo;
			this.Mensaje = Mensaje;
			this.TextoBoton = TextoBoton;
        }

		public Alert(string Titulo, string Mensaje)
        {
            this.Titulo = Titulo;
            this.Mensaje = Mensaje;
            this.TextoBoton = "Ok";
        }
    }
}
