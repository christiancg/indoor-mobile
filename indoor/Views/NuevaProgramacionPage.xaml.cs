using System;

using indoor.Models;

using Xamarin.Forms;

namespace indoor
{
    public partial class NuevaProgramacionPage : ContentPage
    {
        public Programacion Prog { get; set; }

        NuevaProgramacionPage viewModel;

        public NuevaProgramacionPage()
        {
            InitializeComponent();

            //var gpio = ConfigGPIO.LUZ;
            //var hora1 = DateTime.Now.TimeOfDay;
            //var prender = true;
            //var descripcion = "programacionPrueba";
            //var habilitado = true;
            //Prog = new Programacion(gpio,hora1,prender,descripcion,habilitado);

            viewModel = new NuevaProgramacionPage();
            BindingContext = viewModel;
        }

        public NuevaProgramacionPage(NuevaProgramacionPage viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddProgramacion", Prog);
            await Navigation.PopToRootAsync();
        }
    }
}
