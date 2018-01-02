using System;
using Xamarin.Forms;
using Foundation;

namespace indoor.CustomControls
{
    public class CustomSwitch : Switch
    {
        public object SelectedItem
        {
            get;
            set;
        }

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
            propertyName: "SelectedItem",
            returnType: typeof(object),
            declaringType: typeof(CustomSwitch),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: SelectedItemPropertyChanged);

        private static void SelectedItemPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomSwitch)bindable;
            control.SelectedItem = newValue;
        }

        public CustomSwitch()
        {
        }
    }
}
