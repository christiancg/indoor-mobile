using System;
namespace indoor.Models
{
    public class HumedadYTemperatura
    {
        public Decimal humedad
        {
            get;
            set;
        }

        public Decimal temperatura
        {
            get;
            set;
        }

        public HumedadYTemperatura(Decimal humedad, Decimal temperatura)
        {
            this.humedad = humedad;
            this.temperatura = temperatura;
        }
    }
}
