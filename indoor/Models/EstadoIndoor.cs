using System;
namespace indoor.Models
{
    public class EstadoIndoor
    {
        public Boolean luz
        {
            get;
            set;
        }

        public Boolean fanIntra
        {
            get;
            set;
        }

        public Boolean fanExtra
        {
            get;
            set;
        }

        public EstadoIndoor(Boolean luz, Boolean fanIntra, Boolean fanExtra)
        {
            this.luz = luz;
            this.fanIntra = fanIntra;
            this.fanExtra = fanExtra;
        }
    }
}
