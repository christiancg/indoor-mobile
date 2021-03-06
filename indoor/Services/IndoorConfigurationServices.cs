﻿using System;
using System.Collections.ObjectModel;
using System.Text;
using indoor.Bluetooth;
using indoor.Models;
using Plugin.BluetoothLE;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using indoor.Services.Parser;

namespace indoor.Services
{
	public class IndoorConfigurationServices
	{
		private static readonly IndoorConfigurationServices instance = new IndoorConfigurationServices();

		private IndoorConfigurationServices() { }

		public static IndoorConfigurationServices Instance
		{
			get
			{
				return instance;
			}
		}

		// Guids para la escritura y lectura de configuraciones
		public readonly Guid configurationServiceGuid = new Guid("1266b5fd-b35d-4337-bd61-e2159dfa6633");

		private readonly Guid readServerConfigGuid = new Guid("570c9f73-6b43-4adf-90d2-5120b0c20d57");
		private readonly Guid writeServerConfigGuid = new Guid("bdd7bdb8-a503-40cb-b7b0-4114a6d943bc");
		private readonly Guid readGpioConfigGuid = new Guid("00002a38-0000-1000-8000-00805f9b34fb");
		private readonly Guid writeGpioConfigGuid = new Guid("08cf333e-353f-4c82-b0dc-ad2d57d3a018");
		private readonly Guid readUserConfigGuid = new Guid("211ae4c5-7df9-4361-9712-f72ee77a7e9b");
		private readonly Guid writeUserConfigGuid = new Guid("a5b1e27c-a685-41ce-98e2-e361cd122bde");

		// Guids para la conexion y escaneo de redes wifi
		public readonly Guid wlanServiceGuid = new Guid("2c238ce1-3911-4f28-9b14-07c838d4484d");

		private readonly Guid wlanScanCharGuid = new Guid("bed8a9ea-9abe-45e1-803f-3f5df41b49fb");
		private readonly Guid wlanGetConnectedCharGuid = new Guid("26e260af-d8f3-42cf-b48e-385eb5af5a9f");
		private readonly Guid wlanConnectCharGuid = new Guid("e38f9f14-c984-495f-9eb2-162e2b914a0e");

		// Guids para la iniciar, parar o reiniciar el servicio
		public readonly Guid startStopRestartServiceGuid = new Guid("45b3dfe8-e976-4928-b671-b11754553d5b");

		private readonly Guid serverStatusCharGuid = new Guid("a9d7f22f-5ab4-4d0e-8487-0f5cc6b29bcc");
		private readonly Guid startStopRebootCharGuid = new Guid("00fa5ebb-5093-44cb-b251-cb35c59ded7a");

		private readonly BTCommunication bT = new BTCommunication();

		//private ObservableCollection<IDevice> _DispositivosEncontrados;
		//public ObservableCollection<IDevice> DispositivosEncontrados
		//{
		//	get
		//	{
		//		return _DispositivosEncontrados;
		//	}
		//}

		public ObservableCollection<IDevice> DispositivosEncontrados()
		{
			return bT.ScanResult;
		}

		public void Conectar(Guid device)
		{
			try
			{
				bT.Connect(device);
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
				//this._DispositivosEncontrados = bT.ScanResult;
				bT.StartScanning();
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
				//DispositivosEncontrados.Clear();            
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

		public async Task<BluetoothWriteResponse> WriteServerConfig(ServerConfig serverConfig)
		{
			try
			{
				JObject json = JObject.FromObject(serverConfig);
				byte[] messageToSend = Encoding.UTF8.GetBytes(json.ToString());
				bool result = await bT.Write(configurationServiceGuid, writeServerConfigGuid, messageToSend);
				if (result)
				{
					string writeResult = await bT.Read(configurationServiceGuid, writeServerConfigGuid);
					return (BluetoothWriteResponse)writeResult;
				}
				else
					return BluetoothWriteResponse.NO_RESPONSE;
			}
			catch (Exception ex)
			{
				Console.Write(ex);
				return BluetoothWriteResponse.ERROR;
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

		public async Task<BluetoothWriteResponse> WriteGpioConfig(GpioConfig gpioConfig)
		{
			try
			{
				JObject json = JObject.FromObject(gpioConfig);
				byte[] messageToSend = Encoding.UTF8.GetBytes(json.ToString());
				bool result = await bT.Write(configurationServiceGuid, writeGpioConfigGuid, messageToSend);
				if (result)
				{
					string writeResult = await bT.Read(configurationServiceGuid, writeGpioConfigGuid);
					return (BluetoothWriteResponse)writeResult;
				}
				else
					return BluetoothWriteResponse.NO_RESPONSE;
			}
			catch (Exception ex)
			{
				Console.Write(ex);
				return BluetoothWriteResponse.ERROR;
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

		public async Task<BluetoothWriteResponse> WriteUserConfig(ObservableCollection<User> users)
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
					return (BluetoothWriteResponse)writeResult;
				}
				else
					return BluetoothWriteResponse.NO_RESPONSE;
			}
			catch (Exception ex)
			{
				Console.Write(ex);
				return BluetoothWriteResponse.ERROR;
			}
		}

		public async Task<string> ServerStatus()
		{
			try
			{
				return await bT.Read(startStopRestartServiceGuid, serverStatusCharGuid);
			}
			catch (Exception ex)
			{
				Console.Write(ex);
				return null;
			}
		}

		public async Task<BluetoothWriteResponse> StartStopReboot(StartStopReboot action)
		{
			try
			{
				byte[] messageToSend = Encoding.UTF8.GetBytes(((int)action).ToString());
				bool result = await bT.Write(startStopRestartServiceGuid, startStopRebootCharGuid, messageToSend);
				if (result)
				{
					string writeResult = await bT.Read(startStopRestartServiceGuid, startStopRebootCharGuid);
					return (BluetoothWriteResponse)writeResult;
				}
				else
					return BluetoothWriteResponse.NO_RESPONSE;
			}
			catch (Exception ex)
			{
				Console.Write(ex);
				return BluetoothWriteResponse.ERROR;
			}
		}

		public async Task<List<Wifi>> GetWifiNetworks()
		{
			List<Wifi> response = null;
			try
			{
				response = new List<Wifi>();
				string result = await bT.Read(wlanServiceGuid, wlanScanCharGuid);
				response = BTResponseParser.ParseAvaliableNetworks(result);
			}
			catch (Exception ex)
			{
				Console.Write(ex);
			}
			return response;
		}

		public async Task<BluetoothWriteResponse> ConnectToWifi(string ssid, string password)
		{
			try
			{
				JObject auxObj = new JObject();
				auxObj.Add("ssid", ssid);
				auxObj.Add("password", password);
				byte[] messageToSend = Encoding.UTF8.GetBytes(auxObj.ToString());
				bool result = await bT.Write(wlanServiceGuid, wlanConnectCharGuid, messageToSend);
				if (result)
				{
					string writeResult = await bT.Read(wlanServiceGuid, wlanConnectCharGuid);
					return (BluetoothWriteResponse)writeResult;
				}
				else
					return BluetoothWriteResponse.NO_RESPONSE;
			}
			catch (Exception ex)
			{
				Console.Write(ex);
				return BluetoothWriteResponse.ERROR;
			}
		}

		public async Task<string> GetConnectedWifi()
		{
			try
			{
				string result = await bT.Read(wlanServiceGuid, wlanGetConnectedCharGuid);
				if (result == "error")
					return null;
				else
					return result;
			}
			catch (Exception ex)
			{
				Console.Write(ex);
				return null;
			}
		}
	}
}
