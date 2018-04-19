using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using indoor.Models;

using Xamarin.Forms;

namespace indoor.ViewModels
{
    public class EventosViewModel : BaseViewModel
    {
        public ObservableCollection<Evento> Eventos { get; set; }
        public Command LoadEventosCommand { get; set; }

        public EventosViewModel()
        {
            //Title = "Browse";
            Eventos = new ObservableCollection<Evento>();
            LoadEventosCommand = new Command(async () => await ExecuteLoadEventosCommand());

            //MessagingCenter.Subscribe<NewItemPage, Evento>(this, "AddEvento", async (obj, evento) =>
            //{
            //    var _evento = evento as Evento;
            //    Eventos.Add(_evento);
            //    await DataStore.AddItemAsync(_item);
            //});
        }

        async Task ExecuteLoadEventosCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Eventos.Clear();
                var items = await DataStore.GetEventosPorFecha(DateTime.Now.AddDays(-5),DateTime.Now);
                foreach (var item in items)
                {
                    Eventos.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
