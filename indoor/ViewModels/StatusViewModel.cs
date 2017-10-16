using System;
using Xamarin.Forms;

namespace indoor.ViewModels
{
    public class StatusViewModel : BaseViewModel
    {
        public bool Luz
        {
            get;
            set;
        }

        public bool FanIntra
        {
            get;
            set;
        }

        public bool FanExtra
        {
            get;
            set;
        }

        public Command NuevaProgramacionCommand { get; set; }

        public StatusViewModel()
        {
        }
    }
}
