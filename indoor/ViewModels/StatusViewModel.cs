using System;
using System.Threading.Tasks;
using System.Diagnostics;
using Xamarin.Forms;

namespace indoor.ViewModels
{
    public class StatusViewModel : BaseViewModel
    {
        private bool _Luz;
        public bool Luz
        {
            get
            {
                return _Luz;
            }
            set
            {
                _Luz = value;
                OnPropertyChanged();
            }
        }

        private bool _FanIntra;
        public bool FanIntra
        {
            get
            {
                return _FanIntra;
            }
            set
            {
                _FanIntra = value;
                OnPropertyChanged();
            }
        }

        private bool _FanExtra;
        public bool FanExtra
        {
            get
            {
                return _FanExtra;
            }
            set
            {
                _FanExtra = value;
                OnPropertyChanged();
            }
        }

        public int CantSegundos
        {
            get;
            set;
        }

        private String _TxtCantSegundos;
        public String TxtCantSegundos
        {
            get
            {
                return _TxtCantSegundos;
            }
            set
            {
                _TxtCantSegundos = value;
                OnPropertyChanged();
            }
        }

        private bool regando;

        public Command GetEstadoCommand { get; set; }

        public Command RegarCommand { get; set; }

        public StatusViewModel()
        {
            GetEstadoCommand = new Command(async () => await ExecuteLoadEstadoCommand());
            RegarCommand = new Command(async () => await ExecuteRegarCommand());
            TxtCantSegundos = "Se regará durante 10 segundos";


            MessagingCenter.Subscribe<StatusPage>(this, "CambiarTextoLabel", (obj) =>
            {
                TxtCantSegundos = "Se regará durante " + CantSegundos + " segundos";
            });
        }

        async Task ExecuteLoadEstadoCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var estado = await DataStore.GetEstado();
                Luz = estado.luz;
                FanExtra = estado.fanExtra;
                FanIntra = estado.fanIntra;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteRegarCommand()
        {
            if (regando)
                return;
            regando = true;
            try
            {
                var answer = await Application.Current.MainPage.DisplayAlert("Regar", "Esta seguro que desea regar " + CantSegundos + " segundos?", "Si" , "No");
                if(answer)
                {
                    var estado = await DataStore.RegarSegundos(CantSegundos);
                    if(estado)
                        await Application.Current.MainPage.DisplayAlert("Regar", "Se regó " + CantSegundos + " segundos", "Ok");
                    else
                        await Application.Current.MainPage.DisplayAlert("Regar", "Error al regar " + CantSegundos + " segundos", "Ok");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                regando = false;
            }
        }

    }
}
