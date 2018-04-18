using System;
namespace indoor.Models
{
	public class GpioConfig
	{
		public bool luz
		{
			get;
			set;
		}
		public bool bomba
		{
			get;
			set;
		}
		public bool humytemp
		{
			get;
			set;
		}
		public bool fanintra
		{
			get;
			set;
		}
		public bool fanextra
		{
			get;
			set;
		}
		public bool humtierra
		{
			get;
			set;
		}
		public bool camara
		{
			get;
			set;
		}
		private GpioConfig() { }
		public GpioConfig(bool luz, bool bomba, bool humytemp, bool fanintra, bool fanextra, bool humtierra, bool camara)
		{
			this.luz = luz;
			this.bomba = bomba;
			this.humytemp = humytemp;
			this.fanintra = fanintra;
			this.fanextra = fanextra;
			this.humtierra = humtierra;
			this.camara = camara;
		}
	}
}
