using System;

using indoor.Models;

using Xamarin.Forms;

namespace indoor
{
    public partial class NuevaProgramacionPage : ContentPage
    {
        public Programacion Prog { get; set; }

        public NuevaProgramacionPage()
        {
            InitializeComponent();

            var gpio = ConfigGPIO.LUZ;
            var hora1 = DateTime.Now.TimeOfDay;
            var prender = true;
            var descripcion = "programacionPrueba";
            var habilitado = true;

            Prog = new Programacion(gpio,hora1,prender,descripcion,habilitado);

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddProgramacion", Prog);
            await Navigation.PopToRootAsync();
        }
    }
}
