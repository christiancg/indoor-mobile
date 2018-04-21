using System;
using System.Collections.ObjectModel;
using System.Text;
using indoor.Bluetooth;
using indoor.Models;
using Plugin.BluetoothLE;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace indoor.Services
{
	public class IndoorConfigurationServices
	{
		private const int cantCharacteristics = 6;
		private readonly Guid readServerConfigGuid = new Guid("570c9f73-6b43-4adf-90d2-5120b0c20d57");
		private readonly Guid writeServerConfigGuid = new Guid("bdd7bdb8-a503-40cb-b7b0-4114a6d943bc");
		private readonly Guid readGpioConfigGuid = new Guid("00002a38-0000-1000-8000-00805f9b34fb");
		private readonly Guid writeGpioConfigGuid = new Guid("08cf333e-353f-4c82-b0dc-ad2d57d3a018");
		private readonly Guid readUserConfigGuid = new Guid("211ae4c5-7df9-4361-9712-f72ee77a7e9b");
		private readonly Guid writeUserConfigGuid = new Guid("a5b1e27c-a685-41ce-98e2-e361cd122bde");

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
			while (bT.Characteristics.Count < cantCharacteristics) { }
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
			while (bT.Characteristics.Count < cantCharacteristics) { }
			IGattCharacteristic charToReadWrite = bT.Characteristics.Find(x => x.Uuid == writeServerConfigGuid);
			if (charToReadWrite != null)
			{
				JObject json = JObject.FromObject(serverConfig);
				byte[] messageToSend = Encoding.UTF8.GetBytes(json.ToString());
				bool result = bT.Write(charToReadWrite, messageToSend);
				if (result)
				{
					string writeResult = bT.Read(charToReadWrite);
					if (writeResult == "ok")
						return true;
					else
						return false;
				}
                else
                    return false;
			}
			else
				return false;
		}

		public GpioConfig ReadGpioConfig()
		{
			while (bT.Characteristics.Count < cantCharacteristics) { }
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
			while (bT.Characteristics.Count < cantCharacteristics) { }
			IGattCharacteristic charToReadWrite = bT.Characteristics.Find(x => x.Uuid == writeGpioConfigGuid);
			if (charToReadWrite != null)
			{
				JObject json = JObject.FromObject(gpioConfig);
				byte[] messageToSend = Encoding.UTF8.GetBytes(json.ToString());
				bool result = bT.Write(charToReadWrite, messageToSend);
				if (result)
				{
					string writeResult = bT.Read(charToReadWrite);
					if (writeResult == "ok")
						return true;
					else
						return false;
				}
				else
					return false;
			}
			else
				return false;
		}

		public List<User> ReadUserConfig()
		{
			while (bT.Characteristics.Count < cantCharacteristics) { }
			IGattCharacteristic charToRead = bT.Characteristics.Find(x => x.Uuid == readUserConfigGuid);
			if (charToRead != null)
			{
				List<User> toReturn = new List<User>();
				string response = bT.Read(charToRead);
				JObject jOResponse = JObject.Parse(response);
				foreach (var item in jOResponse.Properties())
				{
					User auxUser = new User(item.Name, (string)item.Value);
					toReturn.Add(auxUser);
				}
				return toReturn;
			}
			else
				return null;
		}

		public bool WriteUserConfig(ObservableCollection<User> users)
		{
			while (bT.Characteristics.Count < cantCharacteristics) { }
			IGattCharacteristic charToReadWrite = bT.Characteristics.Find(x => x.Uuid == writeUserConfigGuid);
			if (charToReadWrite != null)
			{
				JObject json = new JObject();
				foreach (var item in users)
				{
					json.Add(item.Username, item.Password);
				}
				byte[] messageToSend = Encoding.UTF8.GetBytes(json.ToString());
				bool result = bT.Write(charToReadWrite, messageToSend);
				if (result)
                {
                    string writeResult = bT.Read(charToReadWrite);
                    if (writeResult == "ok")
                        return true;
                    else
                        return false;
                }
                else
                    return false;
			}
			else
				return false;
		}
	}
}
