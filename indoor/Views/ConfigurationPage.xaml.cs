using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace indoor
{
    public partial class ConfigurationPage : ContentPage
    {
        public string RestURLBase
        {
            get;
            set;
        }

        public string Usuario
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public ConfigurationPage()
        {
            InitializeComponent();
        }

        void OnClick(object sender, EventArgs e){
            //MessagingCenter.Send(this, "AddProgramacion", Prog);
            //await Navigation.PopToRootAsync();
        }
    }
}
