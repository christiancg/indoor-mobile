using System;
using System.Collections.Generic;

using indoor.ViewModels;

using Xamarin.Forms;

namespace indoor
{
    public partial class ConfigurationPage : ContentPage
    {
        public ConfigurationPage()
        {
            InitializeComponent();
            BindingContext = new ConfigurationViewModel(this.Navigation); // HERE
        }

        void OnClick(object sender, EventArgs e){
            MessagingCenter.Send(this, "LogIn");
            //await Navigation.PopToRootAsync();
        }
    }
}
