using System;
using System.Collections.Generic;
using indoor.Services;
using Plugin.BluetoothLE;
using Xamarin.Forms;
using indoor.Views.Configuration.DetailPages;

namespace indoor.Views.Configuration
{
	public class MasterConfigPage : MasterDetailPage
	{
		private readonly IndoorConfigurationServices btServices;
		private SidePanelMasterPage masterPage;

		public MasterConfigPage(IndoorConfigurationServices btServices)
		{
			this.btServices = btServices;
			Title = "Configuracion";

			Master = masterPage = new SidePanelMasterPage();
			Detail = new NavigationPage(new GpioConfigPage(btServices));
			masterPage.ListView.ItemSelected += OnItemSelected;
		}

		void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var item = e.SelectedItem as MasterPageItem;
			if (item != null)
			{
				if (item.IsExit)
				{
					Application.Current.MainPage = new NavigationPage(new ConnectionPage());
				}
				else
				{
					Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType, btServices));
                    masterPage.ListView.SelectedItem = null;
                    IsPresented = false;
				}            
			}
		}

		protected override void OnDisappearing()
        {
			btServices.Desconectar();
        }     
	}
}

