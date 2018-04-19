using System;
using System.Collections.Generic;
using Plugin.BluetoothLE;
using Xamarin.Forms;

namespace indoor.Views.Configuration
{
	public class MasterConfigPage : MasterDetailPage
	{
		private SidePanelMasterPage masterPage;
		private IDevice device;

		public MasterConfigPage(IDevice device)
		{
			this.device = device; 
			Master = masterPage = new SidePanelMasterPage();
			masterPage.ListView.ItemSelected += OnItemSelected;
		}

		void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                masterPage.ListView.SelectedItem = null;
                IsPresented = false;
            }
        }
	}
}

