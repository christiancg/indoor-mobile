using System;

using indoor.Models;

using Xamarin.Forms;

namespace indoor
{
    public partial class EventoDetailPage : ContentPage
    {
        EventoDetailViewModel viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public EventoDetailPage()
        {
            InitializeComponent();

            var fecha = DateTime.Now;
            var dispositivo = ConfigGPIO.LUZ;
            var estado = false;
            var descripcion = "evento prueba";

            var evento = new Evento(fecha,dispositivo,estado,descripcion);

            viewModel = new EventoDetailViewModel(evento);
            BindingContext = viewModel;
        }

        public EventoDetailPage(EventoDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }
    }
}
