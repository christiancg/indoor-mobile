using System;
using System.Collections.ObjectModel;
using System.Text;
using indoor.Bluetooth;
using indoor.Models;
using Plugin.BluetoothLE;
using Newtonsoft.Json.Linq;

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

		public ServerConfig ReadServerConfig()
		{
			IGattCharacteristic charToRead = bT.Characteristics.Find(x => x.Uuid == readServerConfigGuid);
			if (charToRead != null)
			{
				string result = bT.Read(charToRead);
				ServerConfig response = JObject.Parse(result).ToObject<ServerConfig>();
				return response;
			}
			else
				return null;
		}

		public bool WriteServerConfig(ServerConfig serverConfig)
		{
			IGattCharacteristic charToReadWrite = bT.Characteristics.Find(x => x.Uuid == writeServerConfigGuid);
			if (charToReadWrite != null)
			{
				JObject json = JObject.FromObject(serverConfig);
				byte[] messageToSend = Encoding.UTF8.GetBytes(json.ToString());
				TransactionStatus result = bT.Write(charToReadWrite, messageToSend);
				string writeResult = bT.Read(charToReadWrite);
				if (writeResult == "ok")
					return true;
				else
					return false;
			}
			else
				return false;
		}

		public GpioConfig ReadGpioConfig()
		{
			if(bT.Characteristics.Count == 0)
				while(bT.Characteristics.Count<5){}
			IGattCharacteristic charToRead = bT.Characteristics.Find(x => x.Uuid == readGpioConfigGuid);
			if (charToRead != null)
			{
				string result = bT.Read(charToRead);
				GpioConfig response = JObject.Parse(result).ToObject<GpioConfig>();
				return response;
			}
			else
				return null;
		}

		public bool WriteGpioConfig(GpioConfig gpioConfig)
		{
			IGattCharacteristic charToReadWrite = bT.Characteristics.Find(x => x.Uuid == writeGpioConfigGuid);
			if (charToReadWrite != null)
			{
				JObject json = JObject.FromObject(gpioConfig);
				byte[] messageToSend = Encoding.UTF8.GetBytes(json.ToString());
				TransactionStatus result = bT.Write(charToReadWrite, messageToSend);
				string writeResult = bT.Read(charToReadWrite);
				if (writeResult == "ok")
					return true;
				else
					return false;
			}
			else
				return false;
		}
	}
}
