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

        private String _TxtCantSegundos;
        public String TxtCantSegundos
        {
            get
            {
                return _TxtCantSegundos;
            }
            set
            {
                _TxtCantSegundos = value;
                OnPropertyChanged();
            }
        }

        public List<ConfigGPIO> LConfig
        {
            get;
            set;
        }

        public Command DeleteProgramacionCommand { get; set; }

        public AgregarEditarProgramacionViewModel(Programacion toEdit, List<ConfigGPIO> configs)
        {
            IsEdit = true;
            Prog = toEdit;
            LConfig = configs;
            DeleteProgramacionCommand = new Command(async () => await ExecuteDeleteProgramacionCommand());
            Title = "Editar programación";
            TxtCantSegundos = "La duracion será de " + Prog.duracion + " segundos";
        }

        public AgregarEditarProgramacionViewModel(List<ConfigGPIO> configs)
        {
            IsEdit = false;
            Prog = new Programacion();
            LConfig = configs;
            Title = "Añadir programación";
            TxtCantSegundos = "La duracion será de 10 segundos";
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

            MessagingCenter.Subscribe<AgregarEditarProgramacionPage>(this, "CambiarTextoLabel", (obj) =>
            {
                TxtCantSegundos = "La duracion será de " + Prog.duracion + " segundos";
            });
        }

        public void unsetMensajes()
        {
            MessagingCenter.Unsubscribe<AgregarEditarProgramacionPage>(this, "AddProgramacion");
            MessagingCenter.Unsubscribe<AgregarEditarProgramacionPage>(this, "EditProgramacion");
            MessagingCenter.Unsubscribe<AgregarEditarProgramacionPage>(this, "CambiarTextoLabel");
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
