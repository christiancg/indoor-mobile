using System;

using indoor.Services;

using Xamarin.Forms;

using Robotics.Mobile.Core.Bluetooth.LE;

namespace indoor
{
    public partial class App : Application
    {
        static IAdapter Adapter;

        public App()
        {
            InitializeComponent();
            DependencyService.Register<IIndoorComunicationService>();
            MainPage = new NavigationPage(new ConnectionPage(Adapter));
        }

        public static void SetAdapter(IAdapter adapter)
        {
            Adapter = adapter;
        }
    }
}
