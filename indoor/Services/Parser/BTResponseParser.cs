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
				JArray ja = JArray.Parse(toParse);
				foreach (var item in ja)
				{
					string ssid = item["n"].ToString();
					string securityType = item["s"].ToString();
					WifiSecurityType auxSecTyp = WifiSecurityType.NONE;
					if (securityType == null)
						auxSecTyp = WifiSecurityType.NONE;
					else if (securityType.ToLower().StartsWith("wpa2", StringComparison.InvariantCultureIgnoreCase))
						auxSecTyp = WifiSecurityType.WPA2;
					else if (securityType.ToLower().StartsWith("wpa", StringComparison.InvariantCultureIgnoreCase))
						auxSecTyp = WifiSecurityType.WPA;
					else
						auxSecTyp = WifiSecurityType.WEP;
					toReturn.Add(new Wifi(ssid, auxSecTyp));
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
