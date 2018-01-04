using System;

using indoor.Services;

using Xamarin.Forms;

namespace indoor
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DependencyService.Register<IndoorRestService>();
            MainPage = new NavigationPage(new ConfigurationPage());
        }
    }
}
