/*
 * User: oio
 * Date: 04/27/2011
 * Time: 07:36
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
namespace AvUtil.Core
{
	class TimeCalc
	{
		const string sempty="00:00:00";
		
		public TimeFormatString SelectedFormat { get;set; }
		public TimeEx TimeBegin { get; set; }
		public TimeEx TimeEnd { get; set; }

		public TimeCalc(TimeEx t1, TimeEx t2, TimeFormatString sf=TimeFormatString.HMS) {
			SelectedFormat = sf;
			Reset(t1,t2);
		}
		
		public TimeCalc Reset(TimeEx t1, TimeEx t2)
		{
			this.TimeBegin = t1;
			this.TimeEnd = t2;
			return this;
		}
		
		public string Calculate() { return Calculate(SelectedFormat); }
		
		public string Calculate(TimeFormatString format)
		{
			TimeSpan tbegin = TimeBegin;
			TimeSpan tend   = TimeEnd;
			TimeSpan tlen   = TimeEnd-TimeBegin;

			string sbegin = sempty;
			string send   = sempty;
			string slen   = sempty;
			
			TimeEx temp = 0;
			
			switch (format) {
				case TimeFormatString.SECONDS:
					//
					temp=TimeEx.ToMS(tbegin);
					sbegin = string.Format("{0:00}:{1:00}",temp.Minutes,temp.Seconds);
					//
					temp=TimeEx.ToMS(tend);
					send = string.Format("{0:00}:{1:00}",temp.Minutes,temp.Seconds);
					//
					temp=TimeEx.ToMS(tlen);
					slen = string.Format("{0:00}:{1:00}",temp.Minutes,temp.Seconds);
					//
					break;
				case TimeFormatString.MS:
					//
					temp=TimeEx.ToMS(tbegin);
					sbegin = string.Format("{0:00}:{1:00}",temp.Minutes,temp.Seconds);
					temp=TimeEx.ToMS(tend);
					send = string.Format("{0:00}:{1:00}",temp.Minutes,temp.Seconds);
					temp=TimeEx.ToMS(tlen);
					slen = string.Format("{0:00}:{1:00}",temp.Minutes,temp.Seconds);
					//
					break;
				case TimeFormatString.DHMS:
					sbegin = string.Format(Strings.TimeOutputFormat_01_DHMS, tbegin);
					send   = string.Format(Strings.TimeOutputFormat_01_DHMS, tend);
					slen   = string.Format(Strings.TimeOutputFormat_01_DHMS, tlen);
					//
					break;
				case TimeFormatString.HMS:
					temp =TimeEx.ToHMS(tbegin);
					sbegin = string.Format("{0:00}:{1:00}:{2:00}",temp.Hours,temp.Minutes,temp.Seconds);
					temp=TimeEx.ToHMS(tend);
					send   = string.Format("{0:00}:{1:00}:{2:00}",temp.Hours,temp.Minutes,temp.Seconds);
					temp=TimeEx.ToHMS(tlen);
					slen   = string.Format("{0:00}:{1:00}:{2:00}",temp.Hours,temp.Minutes,temp.Seconds);
					break;
				case TimeFormatString.HMSF:
					sbegin = string.Format(Strings.TimeOutputFormat_03_HMSF, tbegin);
					send   = string.Format(Strings.TimeOutputFormat_03_HMSF, tend);
					slen   = string.Format(Strings.TimeOutputFormat_03_HMSF, tlen);
					break;
			}

			//1227:42

			return Strings.Time_Calculation_String
				.Replace("{time1}",sbegin)
				.Replace("{time2}",send)
				.Replace("{difference}", slen)
				;
		}
		
	}
}
