using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Microsoft.Win32;
using NAudio.Wave;
using FntSize = FirstFloor.ModernUI.Presentation.FontSize;
namespace genericwav
{
	public struct SampleSetting
	{
		public int Index;

		/// <summary>Typically 1 or 2.</summary>
		public int NumberOfChannels;

		/// <summary>11.025k, 22.05k, 44.1k, 48k, 96k</summary>
		public int SampleRate;

		/// <summary>Such as 16bit samples</summary>
		public int BitDepth;

		/// <summary>Human friendly</summary>
		public string Title {
			get {
				return title;
			}
			set {
				title = value;
			}
		}

		string title;

		/// <summary>audio/aac, audio/mpeg-layer3, audio/mp3, audio/m4a, ...</summary>
		public SampleSetting(int index, string name, int rate, int channels, int depth)
		{
			this.Index = index;
			this.title = name;
			this.SampleRate = rate;
			this.NumberOfChannels = channels;
			this.BitDepth = depth;
			this.title = name;
		}
	}
}




