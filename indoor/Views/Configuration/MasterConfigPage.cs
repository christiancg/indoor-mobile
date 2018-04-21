using System;
using System.Collections.Generic;
using indoor.Services;
using Plugin.BluetoothLE;
using Xamarin.Forms;

namespace indoor.Views.Configuration
{
	public class MasterConfigPage : MasterDetailPage
	{
		private readonly IndoorConfigurationServices btServices = new IndoorConfigurationServices();      
		private SidePanelMasterPage masterPage;
		private IDevice device;

		public MasterConfigPage(IDevice device)
		{
			this.device = device;
			Title = "Configuracion";
			btServices.Conectar(device);
			Master = masterPage = new SidePanelMasterPage();
			Detail = new NavigationPage(new GpioConfigPage(btServices));
			masterPage.ListView.ItemSelected += OnItemSelected;
		}

		void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            {
				Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType, btServices));
                masterPage.ListView.SelectedItem = null;
                IsPresented = false;
            }
        }
	}
}

