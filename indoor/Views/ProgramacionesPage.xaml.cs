using System;

using Xamarin.Forms;

using indoor.ViewModels;
using indoor.Models;
using indoor.CustomControls;

namespace indoor
{
    public partial class ProgramacionesPage : ContentPage
    {
        ProgramacionesViewModel viewModel;

        public ProgramacionesPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ProgramacionesViewModel();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var prog = args.SelectedItem as Programacion;
            if (prog == null)
                return;

            await Navigation.PushAsync(new AgregarEditarProgramacionPage(new AgregarEditarProgramacionViewModel(prog), true));

            // Manually deselect item
            ProgramacionesListView.SelectedItem = null;
        }

        async void ClickAddProgramacion(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AgregarEditarProgramacionPage(new AgregarEditarProgramacionViewModel(), false));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadProgramacionesCommand.Execute(null);
        }

    }
}
