using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using indoor.Models;

using Xamarin.Forms;

namespace indoor.ViewModels
{
    public class ProgramacionesViewModel : BaseViewModel
    {
        public ObservableCollection<Programacion> Programaciones { get; set; }
        public Command LoadProgramacionesCommand { get; set; }

        public ProgramacionesViewModel()
        {
            Programaciones = new ObservableCollection<Programacion>();
            LoadProgramacionesCommand = new Command(async () => await ExecuteLoadProgramacionesCommand());
        }

        async Task ExecuteLoadProgramacionesCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                Programaciones.Clear();
                var items = await DataStore.GetProgramaciones();
                foreach (var item in items)
                {
                    Programaciones.Add(item);
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

        public async Task<bool> CambiarHabilitacionProgramacion(int idProgramacion, bool estado)
        {
            bool resultado = false;
            try
            {
                resultado = await DataStore.HabilitarDeshabilitarProgramacion(idProgramacion, estado);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                resultado = false;
            }
            return resultado;
        }
    }
}
