using System;
using System.Collections.Generic;
using indoor.Models;
using indoor.ViewModels;

using Xamarin.Forms;

namespace indoor.Views
{
    public partial class AgregarEditarProgramacionPage : ContentPage
    {
        private bool isEdit;

        AgregarEditarProgramacionViewModel viewModel;

        private AgregarEditarProgramacionPage()
        {
            InitializeComponent();
        }

        public AgregarEditarProgramacionPage(List<ConfigGPIO> configs)
        {
            InitializeComponent();
            this.isEdit = false;
            BindingContext = this.viewModel = new AgregarEditarProgramacionViewModel(configs);
            viewModel.setMensajes();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        public AgregarEditarProgramacionPage(Programacion aEditar, List<ConfigGPIO> configs)
        {
            InitializeComponent();
            this.isEdit = true;
            BindingContext = this.viewModel = new AgregarEditarProgramacionViewModel(aEditar, configs);
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
            if (isEdit)
                MessagingCenter.Send(this, "EditProgramacion");
            else
                MessagingCenter.Send(this, "AddProgramacion");
            viewModel.unsetMensajes();
            await Navigation.PopToRootAsync();
        }

        async void Borrar_Clicked(object sender, EventArgs e)
        {
            var result = await viewModel.ExecuteDeleteProgramacionCommand();
            if (result)
            {
                viewModel.unsetMensajes();
                await Navigation.PopToRootAsync();
            }
        }

        protected void StepperValueChanged(object sender, ValueChangedEventArgs args)
        {
            MessagingCenter.Send(this, "CambiarTextoLabel");
        }
    }
}
