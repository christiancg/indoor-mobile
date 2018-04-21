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

		private Command AddUser = null;

		private Command ReloadUserList = null;

		private readonly IndoorConfigurationServices btServices;

		public UsersConfigViewModel(IndoorConfigurationServices btServices)
		{
			this.btServices = btServices;
			AddUser = new Command(() => AddUserCommand());
			ReloadUserList = new Command(() => ReloadUserListCommand());
		}

		private void AddUserCommand(){
			User toAdd = new User(NewUser, NewPassword);
			Usuarios.Add(toAdd);
			bool writeResult = btServices.WriteUserConfig(Usuarios);
		}
        
		private void ReloadUserListCommand()
		{
			Usuarios.Clear();
			List<User> result = btServices.ReadUserConfig();
            foreach (var item in result)
			{
				Usuarios.Add(item);
			}
		}
	}
}
