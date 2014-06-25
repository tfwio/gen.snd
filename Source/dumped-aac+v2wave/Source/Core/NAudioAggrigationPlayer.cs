/* oio * 6/23/2014 * Time: 7:10 AM
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using NAudio.Wave;

namespace AvUtil.Core
{
	
	/// <summary>
	/// Description of MefAggrigator.
	/// </summary>
	public class NAudioAggrigationPlayer : IWavePlayer
	{
		
		const string EmptyTime = "00:00:00.000";
		
		int offset = 0;
		Thread thd;
		WaveStream reader { get { return waveProvider as WaveStream; } }
		IWaveProvider waveProvider;
		WaveFormat fmt;
		bool isPlaying = false;
		int bufferSize;
		object _threadLock = new object();
		
		public int DesiredLatencyInMilloseconds = 300;
		public int FauxLatency = 100;
		public event EventHandler<StoppedEventArgs> PlaybackStopped;
		
		/// <summary>H:M.S</summary>
		public string TimeStatusStringShort {
			get
			{
				if (waveProvider == null) return "00:00.000/00:00.000";
				TimeFormat current, total;
				current = new TimeFormat(0, 0, 0, reader.CurrentTime.TotalSeconds, 0);
				total = new TimeFormat(0, 0, 0, reader.TotalTime.TotalSeconds, 0);
				return string.Format("{0}/{1}", current.ToString(TimeFormatString.HMS), total.ToString(TimeFormatString.HMS));
			}
		}
		
		public float Volume {
			get { return volume; }
			set { volume = value; }
		} float volume = 1.0f;

		#region IWavePlayer implementation

		protected virtual void OnPlaybackStopped(StoppedEventArgs e)
		{
			var handler = PlaybackStopped;
			if (handler != null)
				handler(this, e);
		}
		
		/// <summary>
		/// Gets the size of a wave buffer equivalent to the latency in milliseconds.
		/// </summary>
		/// <param name="milliseconds">The milliseconds.</param>
		/// <returns></returns>
		public int ConvertLatencyToByteSize(int milliseconds, int abps, int blockAlign)
		{
			int bytes = (int) ((abps/1000.0)*milliseconds);
			if ((bytes%blockAlign) != 0)
			{
				// Return the upper BlockAligned
				bytes = bytes + blockAlign - (bytes % blockAlign);
			}
			return bytes;
		}
		
		public void Play()
		{
			if (isPlaying) return;
			if (thd!=null) { thd.Abort(); thd = null; }
			thd = new Thread(ThreadAction){ Priority=ThreadPriority.Highest };
			thd.Start();
		}
		
		AggrigationCalculator Calculator { get;set; }
		
		void ThreadAction()
		{
			if (Calculator!=null) { Calculator = null; }
			Calculator = new AggrigationCalculator(
				reader.WaveFormat,
				reader.Length);
			int bufferLen = ConvertLatencyToByteSize(
				300,
				waveProvider.WaveFormat.AverageBytesPerSecond,
				waveProvider.WaveFormat.BlockAlign
			);
			offset = 0;
			isPlaying=true;
			byte[] buffer = new byte[bufferLen];
			lock (_threadLock) while (isPlaying && reader.Position < reader.Length)
			{
				waveProvider.Read(buffer,0,bufferLen);
			}
			Console.WriteLine("Last line in thread.");
		}

		public void Stop()
		{
			isPlaying = false;
			offset = 0;
			if (thd!=null)
			{
				thd.Join();
				thd.Abort();
				thd = null;
			}
			OnPlaybackStopped(new StoppedEventArgs());
		}

		public void Pause()
		{
			isPlaying = false;
			thd.Suspend();
		}
		
		public void Init(IWaveProvider waveProvider)
		{
			this.waveProvider = waveProvider;
			this.fmt = waveProvider.WaveFormat;
			int bufferSize = waveProvider.WaveFormat.ConvertLatencyToByteSize(DesiredLatencyInMilloseconds);
			this.bufferSize = bufferSize;
			// int bufferSize = waveProvider.WaveFormat.ConvertLatencyToByteSize((DesiredLatency + NumberOfBuffers - 1) / NumberOfBuffers);
		}

		public PlaybackState PlaybackState {
			get { return playbackState; }
		} PlaybackState playbackState = PlaybackState.Stopped;

		#endregion

		#region IDisposable implementation

		public void Dispose()
		{
//			//throw new NotImplementedException();
//			if (waveProvider!=null)
//			{
//				waveProvider.Close();
//				waveProvider.();
//			}
			
		}

		#endregion

		public NAudioAggrigationPlayer()
		{
		}
	}
	
}
