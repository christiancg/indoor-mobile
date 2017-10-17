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

        public Command GetEstadoCommand { get; set; }

        public StatusViewModel()
        {
            GetEstadoCommand = new Command(async () => await ExecuteLoadEstadoCommand());
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
    }
}
