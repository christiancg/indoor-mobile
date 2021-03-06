﻿using System;
using System.Threading.Tasks;
using indoor.Models;
using indoor.Services;

using Xamarin.Forms;

namespace indoor.ViewModels.Configuration.DetailViewModels
{
	public class GpioConfigViewModel : BaseDetailViewModel
	{
		private IndoorConfigurationServices services = IndoorConfigurationServices.Instance;

		public Command WriteGpioConfigCommand
		{
			get;
			set;
		}

		public Command ReadGpioConfigCommand
		{
			get;
			set;
		}

		private GpioConfig _GpioConfig;
		public GpioConfig GpioConfig
		{
			get
			{
				return _GpioConfig;
			}
			set
			{
				_GpioConfig = value;
				OnPropertyChanged();
			}
		}

		public GpioConfigViewModel()
		{
			WriteGpioConfigCommand = new Command(async () => await WriteGpioConfig());
			ReadGpioConfigCommand = new Command(async () => GpioConfig = await ReadGpioConfig());
		}

		public async Task<bool> WriteGpioConfig()
		{
			Alert toSend = null;
			BluetoothWriteResponse status = await services.WriteGpioConfig(GpioConfig);
			if (status != BluetoothWriteResponse.OK)
			{
				toSend = new Alert("Error al escribir config GPIO", "Ha ocurrido un error al escribir la configuracion GPIO, la misma no se ha guardado");
				GpioConfig = await ReadGpioConfig();
			}
			else
			{
				SendRequiresRestart(RequiresRestart.SOFT_RESTART);
				toSend = new Alert("Config GPIO guardada exitosamente", "Se ha guardado exitosamente la configuracion GPIO. Se requiere reinicio del indoor para que la misma surta efecto");
			}         
			SendMessage(toSend);
			return status == BluetoothWriteResponse.OK;
		}

		public async Task<GpioConfig> ReadGpioConfig()
		{
			return await services.ReadGpioConfig();
		}
	}
}
