using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using indoor.Models;
using indoor.Services;
using Xamarin.Forms;

namespace indoor.ViewModels.Configuration.DetailViewModels
{
	public class UsersConfigViewModel : BaseDetailViewModel
	{
		public ObservableCollection<User> Usuarios
		{
			get;
			set;
		} = new ObservableCollection<User>();

		public string NewUser
		{
			get;
			set;
		}

		public string NewPassword
		{
			get;
			set;
		}

		public Command WriteUsersCommand
		{
			get;
			set;
		}

		public Command ReloadUserListCommand
		{
			get;
			set;
		}

		private readonly IndoorConfigurationServices btServices;

		public UsersConfigViewModel(IndoorConfigurationServices btServices)
		{
			this.btServices = btServices;
			WriteUsersCommand = new Command(async () => await WriteUsers());
			ReloadUserListCommand = new Command(async () => await ReloadUserList());
		}

		private async Task<bool> WriteUsers()
		{
			Alert toSend = null;
			User toAdd = new User(NewUser, NewPassword);
			Usuarios.Add(toAdd);
			bool status = await btServices.WriteUserConfig(Usuarios);
			if (!status)
			{
				Usuarios.Remove(toAdd);
				toSend = new Alert("Error al inciar guardar usuarios", "Ha ocurrido un error al iniciar guardar los usuarios del indoor");
			}
			else
				toSend = new Alert("Usuarios guardos exitosamente", "Se han guardado exitosamente los usuarios del indoor. Se requiere reinicio del indoor para que los mismos se encuentren disponibles");
			SendMessage(toSend);
			return status;
		}

		private async Task ReloadUserList()
		{
			if (IsBusy)
				return;
			IsBusy = true;
			Usuarios.Clear();
			List<User> result = await btServices.ReadUserConfig();
			foreach (var item in result)
			{
				Usuarios.Add(item);
			}
			IsBusy = false;
		}
	}
}
