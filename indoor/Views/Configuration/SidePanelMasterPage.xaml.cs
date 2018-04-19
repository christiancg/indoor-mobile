using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace indoor.Views.Configuration
{
    public partial class SidePanelMasterPage : ContentPage
    {
		public ListView ListView { get { return listView; } }

        private ListView listView;

        public SidePanelMasterPage()
        {
            InitializeComponent();

			var masterPageItems = new List<MasterPageItem>();
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Config GPIO",
                //IconSource = "contacts.png",
                TargetType = typeof(GpioConfigPage)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Server config",
                //IconSource = "todo.png",
                TargetType = typeof(ServerConfigPage)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Start/Stop/Reboot",
                //IconSource = "reminders.png",
                TargetType = typeof(StartStopRebootPage)
            });

            listView = new ListView
            {
                ItemsSource = masterPageItems,
                ItemTemplate = new DataTemplate(() => {
                    var grid = new Grid { Padding = new Thickness(5, 10) };
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30) });
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

                    //var image = new Image();
                    //image.SetBinding(Image.SourceProperty, "IconSource");
                    var label = new Label { VerticalOptions = LayoutOptions.FillAndExpand };
                    label.SetBinding(Label.TextProperty, "Title");

                    //grid.Children.Add(image);
                    grid.Children.Add(label, 1, 0);

                    return new ViewCell { View = grid };
                }),
                SeparatorVisibility = SeparatorVisibility.None
            };

            Icon = "hamburger.png";
            Title = "Personal Organiser";
            Content = new StackLayout
            {
                Children = { listView }
            };
        }
    }

	public class MasterPageItem
    {
        public string Title { get; set; }

        //public string IconSource { get; set; }

        public Type TargetType { get; set; }
    }
}
