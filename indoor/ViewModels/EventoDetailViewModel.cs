using System;
using indoor.Models;

namespace indoor.ViewModels
{
    public class EventoDetailViewModel : BaseViewModel
    {
        public Evento Evento { get; set; }
        public EventoDetailViewModel(Evento evento = null)
        {
            Title = evento?.descripcion;
            Evento = evento;
        }
    }
}
