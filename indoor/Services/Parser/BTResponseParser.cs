using System;
using System.Collections.Generic;
using indoor.Models;
using Newtonsoft.Json.Linq;

namespace indoor.Services.Parser
{
	public static class BTResponseParser
	{
		public static List<Wifi> ParseAvaliableNetworks(string toParse)
		{
			List<Wifi> toReturn = null;
			try
			{
				toReturn = new List<Wifi>();
				string[] networks = toParse.Split('|');
				foreach (var item in networks)
				{
					try
					{
						string ssid = item.Substring(0, item.LastIndexOf('-'));
						string securityType = item.Substring(item.LastIndexOf('-') + 1);
						WifiSecurityType auxSecTyp = WifiSecurityType.NONE;
						if (string.IsNullOrEmpty(securityType))
							auxSecTyp = WifiSecurityType.NONE;
						else if (securityType.ToLower().StartsWith("wpa2", StringComparison.InvariantCultureIgnoreCase))
							auxSecTyp = WifiSecurityType.WPA2;
						else if (securityType.ToLower().StartsWith("wpa", StringComparison.InvariantCultureIgnoreCase))
							auxSecTyp = WifiSecurityType.WPA;
						else
							auxSecTyp = WifiSecurityType.WEP;
						toReturn.Add(new Wifi(ssid, auxSecTyp));
					}
					catch (Exception e)
					{
						Console.Write(e);
					}
				}
			}
			catch (Exception ex)
			{
				Console.Write(ex);
			}
			return toReturn;
		}
	}
}
