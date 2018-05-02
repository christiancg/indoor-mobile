using System;
using System.Collections.Generic;

using Xamarin.Forms;
using indoor.Views.Configuration.DetailPages;

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
				Title = "Exit",
				//IconSource = "contacts.png",
				Color = Color.Red,
				IsExit = true
			});
			masterPageItems.Add(new MasterPageItem
            {
                Title = "WLAN Config",
                //IconSource = "contacts.png",
                TargetType = typeof(WlanConfigPage),
                Color = Color.Black,
                IsExit = false
            });
			masterPageItems.Add(new MasterPageItem
			{
				Title = "Config GPIO",
				//IconSource = "contacts.png",
				TargetType = typeof(GpioConfigPage),
				Color = Color.Black,
				IsExit = false
			});
			masterPageItems.Add(new MasterPageItem
			{
				Title = "Users config",
				//IconSource = "todo.png",
				TargetType = typeof(UsersConfigPage),
				Color = Color.Black,
				IsExit = false
			});
			masterPageItems.Add(new MasterPageItem
			{
				Title = "Server config",
				//IconSource = "todo.png",
				TargetType = typeof(ServerConfigPage),
				Color = Color.Black,
				IsExit = false
			});
			masterPageItems.Add(new MasterPageItem
			{
				Title = "Start/Stop/Reboot",
				//IconSource = "reminders.png",
				TargetType = typeof(StartStopRebootPage),
				Color = Color.Black,
				IsExit = false
			});

			listView = new ListView
			{
				ItemsSource = masterPageItems,
				ItemTemplate = new DataTemplate(() =>
				{
					var grid = new Grid { Padding = new Thickness(5, 10) };
					grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30) });
					grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

					//var image = new Image();
					//image.SetBinding(Image.SourceProperty, "IconSource");
					var label = new Label { VerticalOptions = LayoutOptions.FillAndExpand };
					label.SetBinding(Label.TextProperty, "Title");
					label.SetBinding(Label.TextColorProperty, "Color");

					//grid.Children.Add(image);
					grid.Children.Add(label, 1, 0);

					return new ViewCell { View = grid };
				}),
				SeparatorVisibility = SeparatorVisibility.None
			};

			Icon = "menu.png";
			Title = "Configuracion";
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

		public Color Color { get; set; }

		public bool IsExit { get; set; }
	}
}
