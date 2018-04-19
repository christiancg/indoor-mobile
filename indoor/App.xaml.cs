using System;

using indoor.Services;
using indoor.Views;
using Xamarin.Forms;

namespace indoor
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            DependencyService.Register<IIndoorComunicationService>();
            MainPage = new NavigationPage(new ConnectionPage());
        }

    }
}
