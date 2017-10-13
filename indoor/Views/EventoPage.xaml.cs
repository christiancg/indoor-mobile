using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using indoor.Models;

using Xamarin.Forms;

namespace indoor
{
    public partial class EventosPage : ContentPage
    {
        EventosViewModel viewModel;

        public EventosPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new EventosViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var evento = args.SelectedItem as Evento;
            if (evento == null)
                return;

            await Navigation.PushAsync(new EventoDetailPage(new EventoDetailViewModel(evento)));

            // Manually deselect item
            EventosListView.SelectedItem = null;
        }

        //async void AddItem_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new NewItemPage());
        //}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Eventos.Count == 0)
                viewModel.LoadEventosCommand.Execute(null);
        }
    }
}
