using System;
using System.Collections.Generic;
using indoor.Models;
using indoor.Views;
using Xamarin.Forms;

namespace indoor
{
    public class MainPage : TabbedPage
    {
        private List<ConfigGPIO> configs = null;

        public MainPage(List<ConfigGPIO> configs)
        {
            this.configs = configs;
            Page eventosPage, aboutPage, statusPage, cameraPage, programacionesPage = null;
            eventosPage = new NavigationPage(new EventosPage())
            {
                Title = "Eventos"
            };
            aboutPage = new NavigationPage(new AboutPage())
            {
                Title = "About"
            };
            statusPage = new NavigationPage(new StatusPage(configs))
            {
                Title = "Estado"
            };
            programacionesPage = new NavigationPage(new ProgramacionesPage(configs))
            {
                Title = "Programaciones"
            };
            eventosPage.Icon = "tab_eventos.png";
            aboutPage.Icon = "tab_about.png";
            statusPage.Icon = "tab_estado.png";
            programacionesPage.Icon = "tab_programaciones.png";
            Children.Add(statusPage);
            if (configs.Contains(ConfigGPIO.CAMARA))
            {
                cameraPage = new NavigationPage(new CameraPage())
                {
                    Title = "Camara"
                };
                cameraPage.Icon = "tab_camara.png";
                Children.Add(cameraPage);
            }
            Children.Add(programacionesPage);
            Children.Add(eventosPage);
            Children.Add(aboutPage);
            Title = Children[0].Title;
            BarBackgroundColor = Color.FromHex("#e1e1e1");
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title ?? string.Empty;
        }
    }
}
