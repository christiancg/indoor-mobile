using System;

using Xamarin.Forms;

namespace indoor
{
    public class MainPage : TabbedPage
    {
        public MainPage()
        {
            Page itemsPage, aboutPage, statusPage = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    itemsPage = new NavigationPage(new EventosPage())
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
                    itemsPage.Icon = "tab_feed.png";
                    aboutPage.Icon = "tab_about.png";
                    break;
                default:
                    itemsPage = new EventosPage()
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
                    break;
            }

            Children.Add(statusPage);
            Children.Add(itemsPage);
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
