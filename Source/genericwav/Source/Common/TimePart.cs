using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using Microsoft.Win32;
using NAudio.Wave;
using FntSize = FirstFloor.ModernUI.Presentation.FontSize;
namespace genericwav
{
	public enum TimePart
	{
		/// <summary>
		/// 
		/// </summary>
		Millionth,
		/// <summary>
		/// 
		/// </summary>
		Thousanth,
		/// <summary>
		/// 
		/// </summary>
		Second,
		/// <summary>
		/// 
		/// </summary>
		Minute,
		/// <summary>
		/// 
		/// </summary>
		Hour,
		/// <summary>
		/// 
		/// </summary>
		Day
	}
}






