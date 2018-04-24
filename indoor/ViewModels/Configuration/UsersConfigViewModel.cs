using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using indoor.Models;
using indoor.Services;
using Xamarin.Forms;

namespace indoor.ViewModels.Configuration
{
	public class UsersConfigViewModel : BaseViewModel
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

		public bool WriteStatus
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
			WriteUsersCommand = new Command(async () => WriteStatus = await WriteUsers());
			ReloadUserListCommand = new Command(async () => await ReloadUserList());
		}

		private async Task<bool> WriteUsers()
		{
			User toAdd = new User(NewUser, NewPassword);
			Usuarios.Add(toAdd);
			bool status = await btServices.WriteUserConfig(Usuarios);
			if (!status)
				Usuarios.Remove(toAdd);
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
