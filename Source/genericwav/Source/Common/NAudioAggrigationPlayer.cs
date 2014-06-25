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
	/// <summary>
	/// Description of MefAggrigator.
	/// </summary>
	public class NAudioAggrigationPlayer : IWavePlayer
	{
		#region IWavePlayer implementation
		public event EventHandler<StoppedEventArgs> PlaybackStopped;

		protected virtual void OnPlaybackStopped(StoppedEventArgs e)
		{
			var handler = PlaybackStopped;
			if (handler != null)
				handler(this, e);
		}

		IWaveProvider waveProvider;

		WaveFormat fmt;

		bool isPlaying = false;

		public int DesiredLatencyInMilloseconds = 300;

		int CalculateNext(int sampleSeconds)
		{
			return waveProvider.WaveFormat.SampleRate * waveProvider.WaveFormat.BitsPerSample * waveProvider.WaveFormat.Channels * sampleSeconds;
		}

		/// <summary>
		/// Gets the size of a wave buffer equivalent to the latency in milliseconds.
		/// </summary>
		/// <param name="milliseconds">The milliseconds.</param>
		/// <returns></returns>
		public int ConvertLatencyToByteSize(int milliseconds, int abps, int blockAlign)
		{
			int bytes = (int)((abps / 1000.0) * milliseconds);
			if ((bytes % blockAlign) != 0) {
				// Return the upper BlockAligned
				bytes = bytes + blockAlign - (bytes % blockAlign);
			}
			return bytes;
		}

		int offset = 0;

		Thread thd;

		public void Play()
		{
			if (isPlaying)
				return;
			if (thd != null) {
				thd.Abort();
				thd = null;
			}
			thd = new Thread(ThreadAction);
			thd.Start();
		}

		WaveStream reader {
			get {
				return waveProvider as WaveStream;
			}
		}

		const string EmptyTime = "00:00:00.000";

		/// <summary>
		/// gets H:M.S format (TimeFormatString.HMS)
		/// </summary>
		public string TimeStatusStringShort {
			get {
				if (waveProvider == null)
					return "00:00.000/00:00.000";
				TimeFormat current, total;
				current = new TimeFormat(0, 0, 0, reader.CurrentTime.TotalSeconds, 0);
				total = new TimeFormat(0, 0, 0, reader.TotalTime.TotalSeconds, 0);
				return string.Format("{0}/{1}", current.ToString(TimeFormatString.HMS), total.ToString(TimeFormatString.HMS));
			}
		}

		object _threadLock = new object();

		void ThreadAction()
		{
			//			long seek = 0;
			//			long max = 65536;
			lock (_threadLock) {
				int bufferLen = ConvertLatencyToByteSize(300, waveProvider.WaveFormat.AverageBytesPerSecond, waveProvider.WaveFormat.BlockAlign);
				offset = 0;
				isPlaying = true;
				//			int off= Convert.ToInt32(offset);
				byte[] buffer = new byte[bufferLen];
				long len = (waveProvider as WaveStream).Length;
				while (isPlaying && (offset + bufferLen) < len) {
					int next = offset + bufferLen;
					offset += this.waveProvider.Read(buffer, 0, bufferLen);
					Console.WriteLine(TimeStatusStringShort);
					System.Threading.Thread.Sleep(DesiredLatencyInMilloseconds);
				}
				;
			}
		}

		public void Stop()
		{
			isPlaying = false;
			offset = 0;
			thd.Abort();
			thd.Join();
			thd = null;
			OnPlaybackStopped(new StoppedEventArgs());
		}

		public void Pause()
		{
			isPlaying = false;
			thd.Suspend();
		}

		int bufferSize;

		public void Init(IWaveProvider waveProvider)
		{
			this.waveProvider = waveProvider;
			this.fmt = waveProvider.WaveFormat;
			int bufferSize = waveProvider.WaveFormat.ConvertLatencyToByteSize(DesiredLatencyInMilloseconds);
			this.bufferSize = bufferSize;
			// int bufferSize = waveProvider.WaveFormat.ConvertLatencyToByteSize((DesiredLatency + NumberOfBuffers - 1) / NumberOfBuffers);
		}

		public PlaybackState PlaybackState {
			get {
				return playbackState;
			}
		}

		PlaybackState playbackState = PlaybackState.Stopped;

		public float Volume {
			get {
				return volume;
			}
			set {
				volume = value;
			}
		}

		float volume = 1.0f;

		#endregion
		#region IDisposable implementation
		public void Dispose()
		{
			//throw new NotImplementedException();
		}

		#endregion
		public NAudioAggrigationPlayer()
		{
		}
	}
}




