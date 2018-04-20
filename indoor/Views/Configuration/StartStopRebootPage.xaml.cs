using System;
using System.Collections.Generic;
using indoor.Services;
using Xamarin.Forms;

namespace indoor.Views.Configuration
{
    public partial class StartStopRebootPage : ContentPage
    {
		public StartStopRebootPage(IndoorConfigurationServices btServices)
        {
            InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
        }

		void Start(object sender, EventArgs ea)
        {

        }

		void Stop(object sender, EventArgs ea)
        {

        }

		void Restart(object sender, EventArgs ea)
        {

        }      
    }
}
