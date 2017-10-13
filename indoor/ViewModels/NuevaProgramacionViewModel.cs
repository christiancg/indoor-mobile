using System;
using Xamarin.Forms;
using indoor.Models;

namespace indoor.ViewModels
{
    public class NuevaProgramacionViewModel: BaseViewModel
    {
        public Command NuevaProgramacionCommand { get; set; }

        public NuevaProgramacionViewModel()
        {
            Title = "Añadir programación";
            MessagingCenter.Subscribe<NuevaProgramacionPage, Programacion>(this, "AddProgramacion", async (obj, prog) =>
            {
                var _programacion = prog as Programacion;
                await DataStore.AddProgramacion(_programacion);
            });
        }
    }
}
