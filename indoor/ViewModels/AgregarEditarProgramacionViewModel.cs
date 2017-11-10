using System;
using Xamarin.Forms;
using System.Collections.Generic;

using indoor.Models;

namespace indoor.ViewModels
{
    public class AgregarEditarProgramacionViewModel: BaseViewModel
    {
        public Programacion Prog { get; set; }

        public List<ConfigGPIO> LConfig { get; set; }

        public Command NuevaProgramacionCommand { get; set; }

        public AgregarEditarProgramacionViewModel(Programacion toEdit)
        {
            Prog = toEdit;
            setListaConfig();
            setMensajes();
            Title = "Editar programación";
        }

        public AgregarEditarProgramacionViewModel()
        {
            Prog = new Programacion();
            setListaConfig();
            setMensajes();
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

        private void setMensajes()
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
    }
}
