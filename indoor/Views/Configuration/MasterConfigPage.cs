using System;
using System.Collections.Generic;
using indoor.Services;
using Plugin.BluetoothLE;
using Xamarin.Forms;
using indoor.Views.Configuration.DetailPages;
using indoor.Models;

namespace indoor.Views.Configuration
{
	public class MasterConfigPage : MasterDetailPage
	{
		private readonly IndoorConfigurationServices btServices = IndoorConfigurationServices.Instance;
		private SidePanelMasterPage masterPage;
		private RequiresRestart hasToBeRestarted = RequiresRestart.NO;

		public MasterConfigPage()
		{
			Title = "Configuracion";

			Master = masterPage = new SidePanelMasterPage();
			Detail = new NavigationPage(new WlanConfigPage());
			masterPage.ListView.ItemSelected += OnItemSelected;
		}

		async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var item = e.SelectedItem as MasterPageItem;
			if (item != null)
			{
				RequiresRestart auxReqRes = ((BaseDetailPage)((NavigationPage)Detail).RootPage).RequiresRestart;
				int intAuxReqRes = (int)auxReqRes;
				int intHasToBeRestarted = (int)hasToBeRestarted;
				if (intHasToBeRestarted < intAuxReqRes)
					hasToBeRestarted = auxReqRes;
				if (item.IsExit)
				{
					if(hasToBeRestarted == RequiresRestart.SOFT_RESTART || hasToBeRestarted == RequiresRestart.HARD_RESTART){
						await DisplayAlert("Reinicio requerido", "Los cambios que realizo requieren el reinicio del indoor, el mismo se reiniciara a continuacion. Por favor espere unos segundos mientras el mismo inicia", "Ok");
						if (hasToBeRestarted == RequiresRestart.SOFT_RESTART)
							await btServices.StartStopReboot(StartStopReboot.REBOOT);
						else
							await btServices.StartStopReboot(StartStopReboot.HARD_REBOOT);
					}
					await btServices.StartStopReboot(StartStopReboot.DISCONNECT_BLUETOOTH);
					Application.Current.MainPage = new NavigationPage(new ConnectionPage());
				}
				else
				{
					Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
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

