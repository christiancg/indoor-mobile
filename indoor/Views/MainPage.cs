using System;

using Xamarin.Forms;

namespace indoor
{
    public class MainPage : TabbedPage
    {
        public MainPage()
        {
            Page eventosPage, aboutPage, statusPage, programacionesPage = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
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
                    eventosPage.Icon = "tab_eventos.png";
                    aboutPage.Icon = "tab_about.png";
                    statusPage.Icon = "tab_estado.png";
                    programacionesPage.Icon = "tab_programaciones.png";
                    break;
                default:
                    eventosPage = new EventosPage()
                    {
                        Title = "Eventos"
                    };
                    aboutPage = new AboutPage()
                    {
                        Title = "About"
                    };
                    statusPage = new StatusPage()
                    {
                        Title = "Estado"
                    };
                    programacionesPage = new ProgramacionesPage()
                    {
                        Title = "Programaciones"
                    };
                    break;
            }

            Children.Add(statusPage);
            Children.Add(programacionesPage);
            Children.Add(eventosPage);
            Children.Add(aboutPage);
            Title = Children[0].Title;
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title ?? string.Empty;
        }
    }
}
