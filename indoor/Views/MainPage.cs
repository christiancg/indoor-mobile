using System;
using indoor.Views;
using Xamarin.Forms;

namespace indoor
{
    public class MainPage : TabbedPage
    {
        public MainPage()
        {
            Page eventosPage, aboutPage, statusPage, cameraPage, programacionesPage = null;
            eventosPage = new NavigationPage(new EventosPage())
            {
                Title = "Eventos"
            };
            aboutPage = new NavigationPage(new AboutPage())
            {
                Title = "About"
            };
            statusPage = new NavigationPage(new StatusPage())
            {
                Title = "Estado"
            };
            programacionesPage = new NavigationPage(new ProgramacionesPage())
            {
                Title = "Programaciones"
            };
            cameraPage = new NavigationPage(new CameraPage()) { 
                Title = "Camara"
            };
            eventosPage.Icon = "tab_eventos.png";
            aboutPage.Icon = "tab_about.png";
            statusPage.Icon = "tab_estado.png";
            cameraPage.Icon = "tab_camara.png";
            programacionesPage.Icon = "tab_programaciones.png";
            Children.Add(statusPage);
            Children.Add(cameraPage);
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
