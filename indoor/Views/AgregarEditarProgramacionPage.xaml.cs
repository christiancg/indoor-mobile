using System;

using indoor.Models;
using indoor.ViewModels;

using Xamarin.Forms;

namespace indoor
{
    public partial class AgregarEditarProgramacionPage : ContentPage
    {
        private bool isEdit;

        AgregarEditarProgramacionViewModel viewModel;

        public AgregarEditarProgramacionPage()
        {
            InitializeComponent();

            //var gpio = ConfigGPIO.LUZ;
            //var hora1 = DateTime.Now.TimeOfDay;
            //var prender = true;
            //var descripcion = "programacionPrueba";
            //var habilitado = true;
            //Prog = new Programacion(gpio,hora1,prender,descripcion,habilitado);

            BindingContext = viewModel = new AgregarEditarProgramacionViewModel();
            viewModel.setMensajes();
        }

        public AgregarEditarProgramacionPage(AgregarEditarProgramacionViewModel viewModel, bool isEdit)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
            this.isEdit = isEdit;
            viewModel.setMensajes();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            viewModel.unsetMensajes();
            await Navigation.PopToRootAsync();
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            if(isEdit)
                MessagingCenter.Send(this, "EditProgramacion");
            else
                MessagingCenter.Send(this, "AddProgramacion");
            viewModel.unsetMensajes();
            await Navigation.PopToRootAsync();
        }
    }
}
