using System;
using System.Linq;
using FntSize = FirstFloor.ModernUI.Presentation.FontSize;
namespace genericwav
{
	public enum TimeFormatString
	{
		/// <summary>
		/// Just <tt>{seconds}</tt>
		/// </summary>
		SECONDS,
		/// <summary>
		/// MILLISECONDS
		/// </summary>
		MILLISECONDS,
		/// <summary>
		/// <tt>{day}:{hour}:{minute}:{second}</tt>
		/// </summary>
		DHMS,
		/// <summary>
		/// <tt>{hour}:{minute}:{second}</tt>
		/// </summary>
		HMS,
		/// <summary>
		/// <tt>{hour}:{minute}:{second}:{frame}</tt>
		/// </summary>
		HMSF,
		/// <summary>
		/// <tt>{hh}:{mm}:{ss}:{ttt}</tt>
		/// </summary>
		HMS3,
		/// <summary>
		/// <tt>{hh}:{mm}:{sss}:{tttttt}</tt>
		/// </summary>
		HMS5,
		/// <summary>
		/// Winamp Mode: <tt>{minute:00}:{second:00}</tt>
		/// </summary>
		MS,
		/// <summary>
		/// {MINUTES:000}:{SECONDS:00}.{THOUSANDS:000}
		/// </summary>
		MST,
		/// <summary>
		/// {MINUTES:000}:{SECONDS:00}.{MILLIONS:000000}
		/// </summary>
		MSM,
	}
}






