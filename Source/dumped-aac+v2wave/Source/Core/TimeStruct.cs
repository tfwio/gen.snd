using System;

namespace AvUtil.Core
{
	/// <summary>
	/// A string representation of time, provided in standard DD:HH:MM:SS,
	/// though other formats may be represented (returned).
	/// </summary>
	public struct TimeStruct
	{
		public const string NullSec  = "0";
		public const string NullHms  = "00:00:00";
		public const string NullDhms = "00:00:00:00";
		const string Z = "00";
		
		#region Properties

		public string Day { get; set; }
		public string Hour { get; set; }
		public string Minute { get; set; }
		public string Second { get; set; }
		
		/// <summary>
		/// This would actually represent (in most cases FRAMES).
		/// See SMPTE format for more information on this.
		/// </summary>
		public String Frame { get; set; }
		
		#endregion
		
		public string GetString(TimeFormatString type)
		{
			switch (type) {
				case TimeFormatString.DHMS:
					return string.Format(Strings.TimeOutputFormat_01_DHMS,Day,Hour??Z,Minute??Z,Second??Z);
				case TimeFormatString.HMS:
					return string.Format(Strings.TimeOutputFormat_02_HMS,Hour??Z,Minute??Z,Second??Z);
				case TimeFormatString.HMSF:
					return string.Format(Strings.TimeOutputFormat_02_HMS,Hour??Z,Minute??Z,Second??Z,Frame??Z);
			}
			return "00:00";
		}
		
		public string GetDHMS()
		{
			return string.Format("{0}:{1}:{2}:{3}",Day??Z,Hour,Minute,Second);
		}
		
		public string GetHMS()
		{
			return string.Format("{0}:{1}:{2}",Hour,Minute,Second);
		}
	}
}

