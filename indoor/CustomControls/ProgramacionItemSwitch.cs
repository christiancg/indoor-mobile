using System;
using Xamarin.Forms;
using indoor.ViewModels;
using indoor.Models;
using indoor.Services;

namespace indoor.CustomControls
{
    public class ProgramacionItemSwitch : Switch
    {
        public Programacion SelectedItem
        {
            get;
            set;
        }

        private IIndoorRestService DataStore;

        private Boolean cambiandoEstado = false;

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
            propertyName: "SelectedItem",
            returnType: typeof(Programacion),
            declaringType: typeof(ProgramacionItemSwitch),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: SelectedItemPropertyChanged);

        private static void SelectedItemPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ProgramacionItemSwitch)bindable;
            control.SelectedItem = (Programacion)newValue;
        }

        public ProgramacionItemSwitch()
        {
            this.Toggled += CambiarHabilitacion;
            DataStore = new IndoorRestService();
        }

        async void CambiarHabilitacion(object sender, ToggledEventArgs args)
        {
            var control = sender as ProgramacionItemSwitch;
            if (cambiandoEstado || control == null || control.SelectedItem == null || args.Value != SelectedItem.habilitado)
                return;
            cambiandoEstado = true;
            bool resultado = await DataStore.HabilitarDeshabilitarProgramacion(SelectedItem.id, args.Value);
            if (!resultado)
            {
                control.IsToggled = !args.Value;
            }
            cambiandoEstado = false;
        }

    }
}
