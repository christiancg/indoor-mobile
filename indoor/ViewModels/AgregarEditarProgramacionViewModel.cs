using System;
using Xamarin.Forms;
using System.Collections.Generic;

using System.Threading.Tasks;

using indoor.Models;

namespace indoor.ViewModels
{
    public class AgregarEditarProgramacionViewModel: BaseViewModel
    {
        public Boolean IsEdit { get; set; }

        public Programacion Prog { get; set; }

        public List<ConfigGPIO> LConfig { get; set; }

        public Command DeleteProgramacionCommand { get; set; }

        public AgregarEditarProgramacionViewModel(Programacion toEdit)
        {
            IsEdit = true;
            Prog = toEdit;
            setListaConfig();
            DeleteProgramacionCommand = new Command(async () => await ExecuteDeleteProgramacionCommand());
            Title = "Editar programación";
        }

        public AgregarEditarProgramacionViewModel()
        {
            IsEdit = false;
            Prog = new Programacion();
            setListaConfig();
            Title = "Añadir programación";
        }

        private void setListaConfig()
        {
            LConfig = new List<ConfigGPIO>();
            LConfig.Add(ConfigGPIO.LUZ);
            LConfig.Add(ConfigGPIO.FAN_INTRA);
            LConfig.Add(ConfigGPIO.FAN_EXTRA);
            LConfig.Add(ConfigGPIO.SENSOR_HUM_Y_TEMP);
            LConfig.Add(ConfigGPIO.BOMBA);
        }

        public void setMensajes()
        {
            MessagingCenter.Subscribe<AgregarEditarProgramacionPage>(this, "AddProgramacion", async (obj) =>
            {
                await DataStore.AddProgramacion(Prog);
            });

            MessagingCenter.Subscribe<AgregarEditarProgramacionPage>(this, "EditProgramacion", async (obj) =>
            {
                await DataStore.EditProgramacion(Prog);
            });
        }

        public void unsetMensajes()
        {
            MessagingCenter.Unsubscribe<AgregarEditarProgramacionPage>(this, "AddProgramacion");
            MessagingCenter.Unsubscribe<AgregarEditarProgramacionPage>(this, "EditProgramacion");
        }

        public async Task<bool> ExecuteDeleteProgramacionCommand(){
            var answer = await Application.Current.MainPage.DisplayAlert("Borrar", "Esta seguro que desea borrar la programacion actual", "Si", "No");
            if (answer)
            {
                var estado = await DataStore.BorrarProgramacion(Prog.id);
                if (estado)
                    await Application.Current.MainPage.DisplayAlert("Borrar", "Se ha borrado la programacion", "Ok");
                else
                    await Application.Current.MainPage.DisplayAlert("Borrar", "Error al borrar la programacion", "Ok");
                return estado;
            }
            return answer;
        }
    }
}
