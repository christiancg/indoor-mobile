using System;
using System.Collections.Generic;
using indoor.ViewModels;
using Xamarin.Forms;

namespace indoor.Views
{
    public partial class CameraPage : ContentPage
    {
        CameraViewModel viewModel;

        public CameraPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new CameraViewModel();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.RecargarImagenCommand.Execute(null);
        }

        protected void RegargarImagen(object sender, EventArgs args)
        {
            viewModel.RecargarImagenCommand.Execute(null);
        }
    }
}
