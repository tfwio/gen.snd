using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using Microsoft.Win32;
using NAudio.Wave;
using FntSize = FirstFloor.ModernUI.Presentation.FontSize;
namespace genericwav
{
	/// <summary>
	/// Storage of time information into a long-integer (Int64).
	/// This class has the ability to also yield different time-formats
	/// in a ToString overload.
	/// </summary>
	public class TimeFormat
	{
		public const long perMillion = 1;

		public const long perThousand = 1000;

		public const long perSecond = 1000000;

		public const long perMinute = 60000000;

		public const long perHour = 3600000000;

		public const long perDay = 86400000000;

		public long Milliseconds {
			get;
			set;
		}

		// 1000 = 1 second
		//		return string.Format("{0:00}:{1:00}:{2:00}.{3:000}",Remainder(TimePart.Hour),Remainder(TimePart.Minute),Remainder(TimePart.Second),Remainder(TimePart.Thousanth))
		public int Hour {
			get {
				return (int)Remainder(TimePart.Hour);
			}
		}

		public int Minute {
			get {
				return (int)Remainder(TimePart.Minute);
			}
		}

		public int Second {
			get {
				return (int)Remainder(TimePart.Second);
			}
		}

		public int Thou {
			get {
				return (int)Remainder(TimePart.Thousanth);
			}
		}

		public int Mill {
			get {
				return (int)Remainder(TimePart.Millionth);
			}
		}

		public TimeFormat(double day, double hour, double min, double sec, double mil) : this(Convert.ToInt64(Math.Round(day * TimeFormat.perDay, MidpointRounding.AwayFromZero)) + Convert.ToInt64(Math.Round(hour * TimeFormat.perHour, MidpointRounding.AwayFromZero)) + Convert.ToInt64(Math.Round(min * TimeFormat.perMinute, MidpointRounding.AwayFromZero)) + Convert.ToInt64(Math.Round(sec * TimeFormat.perSecond, MidpointRounding.AwayFromZero)) + Convert.ToInt64(Math.Round(mil, MidpointRounding.AwayFromZero)))
		{
		}

		public TimeFormat(int day, int hour, int min, int sec, int mil) : this((day * TimeFormat.perDay) + (hour * TimeFormat.perHour) + (min * TimeFormat.perMinute) + (sec * TimeFormat.perSecond) + mil)
		{
		}

		public TimeFormat(int value, TimePart part)
		{
			switch (part) {
				case TimePart.Millionth:
					Milliseconds = (value);
					break;
				case TimePart.Thousanth:
					Milliseconds = (value * perThousand);
					break;
				case TimePart.Second:
					Milliseconds = (value * perSecond);
					break;
				case TimePart.Minute:
					Milliseconds = (value * perMinute);
					break;
				case TimePart.Hour:
					Milliseconds = (value * perHour);
					break;
				case TimePart.Day:
					Milliseconds = (value * perDay);
					break;
				default:
					break;
			}
		}

		public TimeFormat(long value)
		{
			Milliseconds = value;
		}

		public TimeFormat() : this(0)
		{
		}

		static public implicit operator long(TimeFormat input) {
			return input.Milliseconds;
		}

		static public implicit operator TimeFormat(long input) {
			return new TimeFormat(input);
		}

		static public TimeFormat operator +(TimeFormat a, TimeFormat b) {
			return new TimeFormat(a.Milliseconds + b.Milliseconds);
		}

		static public TimeFormat operator -(TimeFormat a, TimeFormat b) {
			return new TimeFormat(a.Milliseconds - b.Milliseconds);
		}

		static public TimeFormat operator /(TimeFormat a, TimeFormat b) {
			return new TimeFormat(a.Milliseconds / b.Milliseconds);
		}

		static public TimeFormat operator *(TimeFormat a, TimeFormat b) {
			return new TimeFormat(a.Milliseconds * b.Milliseconds);
		}

		public override string ToString()
		{
			return ToString(TimeFormatString.HMS);
		}

		public string ToString(TimeFormatString format)
		{
			switch (format) {
				case TimeFormatString.DHMS:
					return string.Format("{0:00} day(s) {1:00}:{2:00}:{3:00}", Remainder(TimePart.Day), Remainder(TimePart.Hour), Remainder(TimePart.Minute), Remainder(TimePart.Second));
				case TimeFormatString.HMS:
					return string.Format("{0:00}:{1:00}:{2:00}", Remainder(TimePart.Hour), Remainder(TimePart.Minute), Remainder(TimePart.Second));
				case TimeFormatString.MST:
					return string.Format("{1:00}:{2:00}.{3:000}", null, Dividend(TimePart.Minute), Remainder(TimePart.Second), Remainder(TimePart.Thousanth));
				case TimeFormatString.HMS3:
					return string.Format("{0:00}:{1:00}:{2:00}.{3:000}", Remainder(TimePart.Hour), Remainder(TimePart.Minute), Remainder(TimePart.Second), Remainder(TimePart.Thousanth));
				case TimeFormatString.HMS5:
					return string.Format("{0:00}:{1:00}:{2:00}:{3:00000}", Remainder(TimePart.Hour), Remainder(TimePart.Minute), Remainder(TimePart.Second), Remainder(TimePart.Millionth));
				case TimeFormatString.MS:
					return string.Format("{0:##,###,000}:{1:00}", Dividend(TimePart.Minute), Remainder(TimePart.Second));
				case TimeFormatString.MILLISECONDS:
					return string.Format("{0}", Milliseconds);
				case TimeFormatString.SECONDS:
				default:
					return string.Format("{0}", Dividend(TimePart.Second));
			}
		}

		/// returns a clone
		public TimeFormat Add(int value, TimePart part)
		{
			long time = Milliseconds;
			switch (part) {
				case TimePart.Millionth:
					time += (value);
					break;
				case TimePart.Thousanth:
					time += (value * perThousand);
					break;
				case TimePart.Second:
					time += (value * perSecond);
					break;
				case TimePart.Minute:
					time += (value * perMinute);
					break;
				case TimePart.Hour:
					time += (value * perHour);
					break;
				case TimePart.Day:
					time += (value * perDay);
					break;
				default:
					break;
			}
			return new TimeFormat(time);
		}

		public TimeFormat Divide(long value, TimePart part)
		{
			long time = Milliseconds;
			switch (part) {
				case TimePart.Millionth:
					time /= (value);
					break;
				case TimePart.Thousanth:
					time /= (value * perThousand);
					break;
				case TimePart.Second:
					time /= (value * perSecond);
					break;
				case TimePart.Minute:
					time /= (value * perMinute);
					break;
				case TimePart.Hour:
					time /= (value * perHour);
					break;
				case TimePart.Day:
					time /= (value * perDay);
					break;
				default:
					break;
			}
			return new TimeFormat(time);
		}

		public long Multiply(long value, TimePart part)
		{
			long time = Milliseconds;
			switch (part) {
				case TimePart.Millionth:
					time *= (value);
					break;
				case TimePart.Thousanth:
					time *= (value * perThousand);
					break;
				case TimePart.Second:
					time *= (value * perSecond);
					break;
				case TimePart.Minute:
					time *= (value * perMinute);
					break;
				case TimePart.Hour:
					time *= (value * perHour);
					break;
				case TimePart.Day:
					time *= (value * perDay);
					break;
				default:
					break;
			}
			return new TimeFormat(time);
		}

		public long Dividend(TimePart format)
		{
			long time = Milliseconds;
			if (format == TimePart.Millionth) {
				time = this.Milliseconds / perMillion;
			}
			else
				if (format == TimePart.Thousanth) {
					time = this.Milliseconds / perThousand;
				}
				else
					if (format == TimePart.Second) {
						time = this.Milliseconds / perSecond;
					}
					else
						if (format == TimePart.Minute) {
							time = this.Milliseconds / perMinute;
						}
						else
							if (format == TimePart.Hour) {
								time = this.Milliseconds / perHour;
							}
							else
								if (format == TimePart.Day) {
									time = this.Milliseconds / perDay;
								}
			return new TimeFormat(time);
		}

		public long Remainder(TimePart format)
		{
			long tday, thour, tmin, tsec, ttsec, tmsec;
			//
			tday = Dividend(TimePart.Day);
			thour = Dividend(TimePart.Hour);
			tmin = Dividend(TimePart.Minute);
			tsec = Dividend(TimePart.Second);
			ttsec = Dividend(TimePart.Thousanth);
			tmsec = Dividend(TimePart.Millionth);
			if (format == TimePart.Millionth)
				return this - (tsec * perSecond);
			else
				if (format == TimePart.Thousanth)
					return (this - (tsec * perSecond)) / perThousand;
				else
					if (format == TimePart.Second)
						return tsec % 60;
					else
						if (format == TimePart.Minute)
							return tmin % 60;
						else
							if (format == TimePart.Hour)
								return thour % 24;
							else
								if (format == TimePart.Day)
									return tday;
			return 0;
		}

		static public TimeFormat Parse(string inputString)
		{
			if (inputString == null)
				return 0;
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
			TimeFormat data = new TimeFormat(Convert.ToInt32(mc.Groups["dd"].Value == null ? "00" : mc.Groups["dd"].Value), Convert.ToInt32(mc.Groups["hh"].Value == null ? "00" : mc.Groups["hh"].Value), Convert.ToInt32(mc.Groups["mm"].Value == null ? "00" : mc.Groups["mm"].Value), Convert.ToInt32(mc.Groups["ss"].Value == null ? "00" : mc.Groups["ss"].Value), Convert.ToInt32(mc.Groups["ttt"].Value == null ? "00" : mc.Groups["ttt"].Value));
			mc = null;
			return data;
		}

		static readonly Regex rexST = new Regex(@"(?<ss>[0-9]*)\.?(?<ttt>[0-9]*)");

		static readonly Regex rexMST = new Regex(@"(?<mm>[0-9]*):?(?<ss>[0-9]*)\.?(?<ttt>[0-9]*)");

		static readonly Regex rexHMST = new Regex(@"(?<hh>[0-9]*):?(?<mm>[0-9]*):?(?<ss>[0-9]*)\.?(?<ttt>[0-9]*)");

		static readonly Regex rexDHMST = new Regex(@"(?<dd>[0-9]*):?(?<hh>[0-9]*):?(?<mm>[0-9]*):?(?<ss>[0-9]*)\.?(?<ttt>[0-9]*)");
	}
}






