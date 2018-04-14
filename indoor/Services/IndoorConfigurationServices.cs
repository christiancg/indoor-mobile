using System;
using System.Collections.ObjectModel;
using indoor.Bluetooth;
using Plugin.BluetoothLE;

namespace indoor.Services
{
	public class IndoorConfigurationServices
	{
		private readonly BTCommunication bT = new BTCommunication();

		public ObservableCollection<IDevice> DispositivosEncontrados
		{
			get;
			set;
		}

		public IndoorConfigurationServices()
		{

		}

		public void Conectar(IDevice aConectar)
		{
			bT.Connect(aConectar);
		}

		public void StartScan()
		{
			DispositivosEncontrados = bT.scanResult;
			Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
			{
				bT.StartScanning();
			});         
		}

		public void StopScan()
		{
			bT.StopScanning();
		}
	}
}
