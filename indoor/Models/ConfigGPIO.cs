using System;
namespace indoor.Models
{
    public sealed class ConfigGPIO
    {
        public String Name { get; }
        public String Value { get; }

        public static readonly ConfigGPIO LUZ = new ConfigGPIO("LUZ", "luz");
        public static readonly ConfigGPIO FAN_INTRA = new ConfigGPIO("FAN_INTRA", "fanintra");
        public static readonly ConfigGPIO FAN_EXTRA = new ConfigGPIO("FAN_EXTRA", "fanextra");
        public static readonly ConfigGPIO BOMBA = new ConfigGPIO("BOMBA", "bomba");
        public static readonly ConfigGPIO SENSOR_HUM_Y_TEMP = new ConfigGPIO("SENSOR_HUM_Y_TEMP", "humytemp");
        public static readonly ConfigGPIO SENSOR_HUM_TIERRA = new ConfigGPIO("SENSOR_HUM_TIERRA", "humtierra");
        public static readonly ConfigGPIO CAMARA = new ConfigGPIO("CAMARA", "camara");

        private ConfigGPIO(String value, String name)
        {
            this.Name = name;
            this.Value = value;
        }

        public override String ToString()
        {
            return Name;
        }

        public static explicit operator ConfigGPIO(string str)
        {
            switch (str)
            {
                case "luz":
                    return LUZ;
                case "fanintra":
                    return FAN_INTRA;
                case "fanextra":
                    return FAN_EXTRA;
                case "bomba":
                    return BOMBA;
                case "humytemp":
                    return SENSOR_HUM_Y_TEMP;
                case "humtierra":
                    return SENSOR_HUM_TIERRA;
                case "camara":
                    return CAMARA;
                default:
                    throw new InvalidCastException();
            }
        }
    }
}
