using System;
using System.Collections.ObjectModel;
using System.Text;
using indoor.Bluetooth;
using Plugin.BluetoothLE;

namespace indoor.Services
{
	public class IndoorConfigurationServices
	{
		private readonly Guid readServerConfigGuid = new Guid("570c9f73-6b43-4adf-90d2-5120b0c20d57");
		private readonly Guid writeServerConfigGuid = new Guid("bdd7bdb8-a503-40cb-b7b0-4114a6d943bc");
		private readonly Guid readGpioConfigGuid = new Guid("00002a38-0000-1000-8000-00805f9b34fb");
		private readonly Guid writeGpioConfigGuid = new Guid("08cf333e-353f-4c82-b0dc-ad2d57d3a018");

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

		public bool WriteServerConfig()
		{         
			IGattCharacteristic charToWrite = bT.Characteristics.Find(x => x.Uuid == writeGpioConfigGuid);
			byte[] messageToSend = Encoding.UTF8.GetBytes("hola");
			TransactionStatus result = bT.Write(charToWrite, messageToSend);
			IGattCharacteristic charToRead = bT.Characteristics.Find(x => x.Uuid == readGpioConfigGuid);
			string writeResult = bT.Read(charToRead);
			if (writeResult == "ok")
				return true;
			else
				return false;
		}
	}
}
