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
                    string ssid = item["ssid"].ToString();
                    bool isSecure = item["secure"].ToObject<bool>();
                    if (isSecure)
                        toReturn.Add(new Wifi(ssid, WifiSecurityType.WPA2));
                    else
                        toReturn.Add(new Wifi(ssid));
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
