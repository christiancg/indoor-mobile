using System;
namespace indoor.Models
{
	public sealed class BluetoothWriteResponse
    {
		public String Name { get; }
        public String Value { get; }

		public static readonly BluetoothWriteResponse NO_RESPONSE = new BluetoothWriteResponse("NO_RESPONSE", "no_response");
		public static readonly BluetoothWriteResponse OK = new BluetoothWriteResponse("OK", "ok");
		public static readonly BluetoothWriteResponse ERROR = new BluetoothWriteResponse("ERROR", "error");
		public static readonly BluetoothWriteResponse BAD_REQUEST = new BluetoothWriteResponse("BAD_REQUEST", "bad_request");
		public static readonly BluetoothWriteResponse HARD_RESET = new BluetoothWriteResponse("HARD_RESET", "requires_hard_reset");      

		private BluetoothWriteResponse(String value, String name)
        {
            this.Name = name;
            this.Value = value;
        }

        public override String ToString()
        {
            return Name;
        }

		public static explicit operator BluetoothWriteResponse(string str)
        {
            switch (str)
            {
				case "no_response":
					return NO_RESPONSE;
				case "ok":
					return OK;
				case "error":
					return ERROR;
				case "bad_request":
					return BAD_REQUEST;
				case "requires_hard_reset":
					return HARD_RESET;
                default:
                    throw new InvalidCastException();
            }
        }
    }
}
