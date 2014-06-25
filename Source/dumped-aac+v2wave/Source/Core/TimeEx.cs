using System;
using System.Text.RegularExpressions;

namespace AvUtil.Core
{
	/// <summary>
	/// Time calculator utility class.
	/// </summary>
	public class TimeEx : Time
	{
		/// <summary>
		/// This produces a time in Minutes and Seconds, 'unflattend'.
		/// To convert to a time format, this has to be initialized as a TimeSpan again.
		/// </summary>
		/// <param name="time"></param>
		/// <returns></returns>
		static public TimeEx ToMS(TimeSpan time)
		{
			TimeEx flat = new TimeEx(time);
			int d=time.Days, h=time.Hours, m=time.Minutes, s=time.Seconds, ms=time.Milliseconds;
			//
			flat.Days    = 0;
			flat.Hours   = 0;
			//
			flat.Minutes = (((d*24) + (h)) * 60)+m;
			flat.Seconds = time.Seconds;
			return flat;
		}
		
		/// <summary>
		/// This produces a time in Minutes and Seconds, 'unflattend'.
		/// To convert to a time format, this has to be initialized as a TimeSpan again.
		/// </summary>
		/// <param name="time"></param>
		/// <returns></returns>
		static public TimeEx ToHMS(TimeSpan time)
		{
			TimeEx flat = new TimeEx();
			flat.Hours = (time.Days*24) + time.Hours;
			flat.Minutes = time.Minutes;
			flat.Seconds = time.Seconds;
			return flat;
		}
	
		/// <summary>
		/// To the nearest second (no milliseconds are accounted for here)
		/// </summary>
		public double TotalSeconds
		{
			get {
				return new TimeSpan(this.Days,this.Hours,this.Minutes,this.Seconds).TotalSeconds;
			}
		}
	
		public string ShortString
		{
			get {
				return ((TimeSpan)this).ToString(@"hh\:mm\:ss");
			}
		}
	
		public TimeSpan GetTimeSpan()
		{
			return GetTimeSpan(this);
		}
	
		public TimeEx() : this(0,0,0,0,0)
		{
		}
		
		// NaN will prove to be trouble
		public TimeEx(decimal dd, decimal hh, decimal mm, decimal ss, decimal tt)
			: this(Convert.ToInt32(dd),Convert.ToInt32(hh),Convert.ToInt32(mm),Convert.ToInt32(ss),Convert.ToInt32(tt))
		{
		}
	
	
		public TimeEx(string dd, string hh, string mm, string ss, string ttt)
		{
			if (dd==string.Empty) Days = 0;
			else Days = int.Parse(dd);
			if (hh==string.Empty) Hours = 0;
			else Hours = int.Parse(hh);
			if (mm==string.Empty) Minutes = 0;
			else Minutes = int.Parse(mm);
			if (ss==string.Empty) Seconds = 0;
			else Seconds = int.Parse(ss);
			if (ttt==string.Empty) Milliseconds = 0;
			else Milliseconds = int.Parse(ttt);
		}
		public TimeEx(int dd, int hh, int mm, int ss, int ttt)
		{
			Days = dd;
			Hours = hh;
			Minutes = mm;
			Seconds = ss;
			Milliseconds = ttt;
		}
		public TimeEx(double seconds) : this(TimeSpan.FromSeconds(seconds)) {}
		public TimeEx(TimeSpan span) : this(span.Days,span.Hours,span.Minutes,span.Seconds,span.Milliseconds) {}
		
		/// <summary>
		/// This ToString method ignores the Days Field.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format(
				Strings.Format_Time_String,
				this.Hours,
				this.Minutes,
				this.Seconds,
				this.Milliseconds
			);
		}
	
		#region Static (utility methods,
	
		public static TimeEx operator -(TimeEx a, TimeEx b)
		{
			return new TimeEx(a.Days-b.Days,a.Hours-b.Hours,a.Minutes-b.Minutes,a.Seconds-b.Seconds,0M);
		}
		public static TimeEx operator +(TimeEx a, TimeEx b)
		{
			return new TimeEx(a.Days+b.Days,a.Hours+b.Hours,a.Minutes+b.Minutes,a.Seconds+b.Seconds,0M);
		}
	
		static public implicit operator TimeSpan(TimeEx input)
		{
			return GetTimeSpan(input);
		}
		static public implicit operator TimeEx(string input)
		{
			return TimeEx.Parse(input);
		}
		static public implicit operator string(TimeEx input)
		{
			return input.ToString();
		}
		static public implicit operator TimeEx(int input)
		{
			return new TimeEx(input);
		}
	
		static public TimeEx Empty
		{
			get {
				return new TimeEx(0,0,0,0,0);
			}
		}
	
		static public TimeSpan GetTimeSpan(TimeEx time)
		{
			return TimeSpan.FromSeconds(time.TotalSeconds)/*(time.Hours,time.Minutes,time.Seconds,time.Milliseconds)*/;
		}
		
		static public TimeEx Parse(string inputString)
		{
			if (inputString==null) return Empty;
			string[] arr = inputString.Split(':');
			int count = arr.Length;
			Array.Clear(arr,0,arr.Length);
			Match mc;
			switch (count) {
				case 2:
					mc = rexMST.Match(inputString);
					break;
				case 3:
					mc = rexHMST.Match(inputString);
					break;
				case 4:
					mc = rexDHMST.Match(inputString);
					break;
				default:
					mc = rexST.Match(inputString);
					break;
			}
			TimeEx data = new TimeEx(
				mc.Groups["dd"].Value==null?  "00" : mc.Groups["dd"].Value,
				mc.Groups["hh"].Value==null?  "00" : mc.Groups["hh"].Value,
				mc.Groups["mm"].Value==null?  "00" : mc.Groups["mm"].Value,
				mc.Groups["ss"].Value==null?  "00" : mc.Groups["ss"].Value,
				mc.Groups["ttt"].Value==null? "00" : mc.Groups["ttt"].Value
			);
			mc = null;
			return data;
		}
	
		static readonly Regex rexST    = new Regex(Strings.Regex_Time_Expression_1);
		static readonly Regex rexMST   = new Regex(Strings.Regex_Time_Expressoin_2);
		static readonly Regex rexHMST  = new Regex(Strings.Regex_Time_Expression_3);
		static readonly Regex rexDHMST = new Regex(Strings.Regex_Time_Expression_4);
	
		/// <summary>
		/// Returns a simple structure mildly parsed.
		/// </summary>
		/// <param name="inputString"></param>
		/// <returns></returns>
		static public TimeStruct? GetStruct(string inputString)
		{
			if (inputString==null) return null;
			try {
				string[] arr = inputString.Split(':');
				int count = arr.Length;
				Array.Clear(arr, 0, arr.Length);
				Match mc;
				switch (count) {
					case 2:
						mc = rexMST.Match(inputString);
						break;
					case 3:
						mc = rexHMST.Match(inputString);
						break;
					case 4:
						mc = rexDHMST.Match(inputString);
						break;
					default:
						mc = rexST.Match(inputString);
						break;
				}
				TimeStruct result = new TimeStruct {
					Day =    string.IsNullOrEmpty(mc.Groups["dd"].Value) ? "00" : mc.Groups["dd"].Value.PadLeft(2, '0'),
					Hour =   string.IsNullOrEmpty(mc.Groups["hh"].Value) ? "00" : mc.Groups["hh"].Value.PadLeft(2, '0'),
					Minute = string.IsNullOrEmpty(mc.Groups["mm"].Value) ? "00" : mc.Groups["mm"].Value.PadLeft(2, '0'),
					Second = string.IsNullOrEmpty(mc.Groups["ss"].Value) ? "00" : mc.Groups["ss"].Value.PadLeft(2, '0'),
					Frame = string.IsNullOrEmpty(mc.Groups["ttt"].Value) ? "000" : mc.Groups["ttt"].Value.PadLeft(3, '0')
				};
				mc = null;
				return result;
			} catch {
			}
			return null;
		}
	
		#endregion
	
	}
}

