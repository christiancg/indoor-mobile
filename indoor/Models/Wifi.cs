using System;
namespace indoor.Models
{
    public class Wifi
    {
        public string Ssid
		{
			get;
			set;
		}

		public WifiSecurityType SecurityType
		{
			get;
			set;
		}

        public string Password
		{
			get;
			set;
		}

		public Wifi(string ssid, WifiSecurityType securityType)
		{
			this.Ssid = ssid;
			this.SecurityType = securityType;
        }      

		public Wifi(string ssid)
        {
			this.Ssid = ssid;
			this.SecurityType = WifiSecurityType.NONE;
        }
    }
}
