using System;
using System.Collections.ObjectModel;
using System.Text;
using indoor.Bluetooth;
using indoor.Models;
using Plugin.BluetoothLE;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace indoor.Services
{
	public class IndoorConfigurationServices
	{
		public static readonly int cantCharacteristics = 6;

        // Guids para la escritura y lectura de configuraciones
		private readonly Guid configurationServiceGuid = new Guid("1266b5fd-b35d-4337-bd61-e2159dfa6633");

		private readonly Guid readServerConfigGuid = new Guid("570c9f73-6b43-4adf-90d2-5120b0c20d57");
		private readonly Guid writeServerConfigGuid = new Guid("bdd7bdb8-a503-40cb-b7b0-4114a6d943bc");
		private readonly Guid readGpioConfigGuid = new Guid("00002a38-0000-1000-8000-00805f9b34fb");
		private readonly Guid writeGpioConfigGuid = new Guid("08cf333e-353f-4c82-b0dc-ad2d57d3a018");
		private readonly Guid readUserConfigGuid = new Guid("211ae4c5-7df9-4361-9712-f72ee77a7e9b");
		private readonly Guid writeUserConfigGuid = new Guid("a5b1e27c-a685-41ce-98e2-e361cd122bde");

		// Guids para la conexion y escaneo de redes wifi
		private readonly Guid wlanServiceGuid = new Guid("2c238ce1-3911-4f28-9b14-07c838d4484d");
        
		private readonly Guid wlanScanCharGuid = new Guid("bed8a9ea-9abe-45e1-803f-3f5df41b49fb");

		// Guids para la iniciar, parar o reiniciar el servicio
        private readonly Guid startStopRestartServiceGuid = new Guid("45b3dfe8-e976-4928-b671-b11754553d5b");

		private readonly Guid startStopRebootCharGuid = new Guid("00fa5ebb-5093-44cb-b251-cb35c59ded7a");

		private readonly BTCommunication bT = new BTCommunication();

		public IDevice SelectedDevice
		{
			get;
			set;
		}

		public ObservableCollection<IDevice> DispositivosEncontrados
		{
			get;
			set;
		}

		public IndoorConfigurationServices()
		{

		}

		public void Conectar()
		{
			try
			{
				bT.Connect(SelectedDevice);
			}
			catch (Exception ex)
			{
				Console.Write(ex);
			}
		}

		public void Desconectar()
		{
			try
			{
				bT.Disconnect();
			}
			catch (Exception ex)
			{
				Console.Write(ex);
			}
		}

		public void StartScan()
		{
			try
			{
				DispositivosEncontrados = bT.scanResult;
				Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
				{
					bT.StartScanning();
				});
			}
			catch (Exception ex)
			{
				Console.Write(ex);
			}
		}

		public void StopScan()
		{
			try
			{
				bT.StopScanning();
			}
			catch (Exception ex)
			{
				Console.Write(ex);
			}
		}

		public async Task<ServerConfig> ReadServerConfig()
		{
			ServerConfig response = null;
			try
			{
				string result = await bT.Read(configurationServiceGuid, readServerConfigGuid);
				response = JObject.Parse(result).ToObject<ServerConfig>();
			}
			catch (Exception ex)
			{
				Console.Write(ex);
			}
			return response;
		}

		public async Task<bool> WriteServerConfig(ServerConfig serverConfig)
		{
			try
			{
				JObject json = JObject.FromObject(serverConfig);
				byte[] messageToSend = Encoding.UTF8.GetBytes(json.ToString());
				bool result = await bT.Write(configurationServiceGuid, writeServerConfigGuid, messageToSend);
				if (result)
				{
					string writeResult = await bT.Read(configurationServiceGuid, writeServerConfigGuid);
					if (writeResult == "ok")
						return true;
					else
						return false;
				}
				else
					return false;
			}
			catch (Exception ex)
			{
				Console.Write(ex);
				return false;
			}
		}

		public async Task<GpioConfig> ReadGpioConfig()
		{
			GpioConfig response = null;
			try
			{
				string result = await bT.Read(configurationServiceGuid, readGpioConfigGuid);
				response = JObject.Parse(result).ToObject<GpioConfig>();
			}
			catch (Exception ex)
			{
				Console.Write(ex);
			}
			return response;
		}

		public async Task<bool> WriteGpioConfig(GpioConfig gpioConfig)
		{
			try
			{
				JObject json = JObject.FromObject(gpioConfig);
				byte[] messageToSend = Encoding.UTF8.GetBytes(json.ToString());
				bool result = await bT.Write(configurationServiceGuid, writeGpioConfigGuid, messageToSend);
				if (result)
				{
					string writeResult = await bT.Read(configurationServiceGuid, writeGpioConfigGuid);
					if (writeResult == "ok")
						return true;
					else
						return false;
				}
				else
					return false;
			}
			catch (Exception ex)
			{
				Console.Write(ex);
				return false;
			}
		}

		public async Task<List<User>> ReadUserConfig()
		{
			List<User> toReturn = null;
			try
			{
				toReturn = new List<User>();
				string response = await bT.Read(configurationServiceGuid, readUserConfigGuid);
				JObject jOResponse = JObject.Parse(response);
				foreach (var item in jOResponse.Properties())
				{
					User auxUser = new User(item.Name, (string)item.Value);
					toReturn.Add(auxUser);
				}
			}
			catch (Exception ex)
			{
				Console.Write(ex);
			}
			return toReturn;
		}

		public async Task<bool> WriteUserConfig(ObservableCollection<User> users)
		{
			try
			{
				JObject json = new JObject();
				foreach (var item in users)
				{
					json.Add(item.Username, item.Password);
				}
				byte[] messageToSend = Encoding.UTF8.GetBytes(json.ToString());
				bool result = await bT.Write(configurationServiceGuid, writeUserConfigGuid, messageToSend);
				if (result)
				{
					string writeResult = await bT.Read(configurationServiceGuid, writeUserConfigGuid);
					if (writeResult == "ok")
						return true;
					else
						return false;
				}
				else
					return false;
			}
			catch (Exception ex)
			{
				Console.Write(ex);
				return false;
			}
		}

		public async Task<bool> StartStopReboot(StartStopReboot action)
        {
            try
            {
				byte[] messageToSend = Encoding.UTF8.GetBytes(((int)action).ToString());
				bool result = await bT.Write(startStopRestartServiceGuid, startStopRebootCharGuid, messageToSend);
                if (result)
                {
					string writeResult = await bT.Read(startStopRestartServiceGuid, startStopRebootCharGuid);
                    if (writeResult == "ok")
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return false;
            }
        }
	}
}
