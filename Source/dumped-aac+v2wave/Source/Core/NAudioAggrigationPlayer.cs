/* oio * 6/23/2014 * Time: 7:10 AM
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using NAudio.Wave;

namespace AvUtil.Core
{
	class AggrigationCalculator
	{
		[Flags]
		public enum Mode {
			Mono = 0,
			Left = 1,
			Right = 2,
			Stereo = Left|Right
		}
		
		public List<FloatPoint> Points = new List<FloatPoint>();
		
		public static FloatPoint DefaultWindowSize { get { return new FloatPoint(1920,1080); } }

		public FloatPoint WindowSize {
			get { return windowSize; }
			set { windowSize = value; }
		} FloatPoint windowSize = null;

		public FloatPoint WindowRatio {
			get { return windowRatio; }
			set { windowRatio = value; }
		} FloatPoint windowRatio = null;
		
		WaveFormat WaveInfo { get;set; }
		
		long SampleSize, SampleCount, WaveLength, MaximumSamples;
		
		public FloatPoint[] GetSpline()
		{
			return null;
		}
		#region Range(...), Merge(...) (Unused)
		public FloatPoint Range(FloatPoint range)
		{
			var z = DefaultWindowSize / range;
			return z%FloatPoint.One >= new FloatPoint(0.5f) ? z+1 : z;  // ratio
		}
		/// <summary>
		/// <para>increment unit 'count' X min, Y max;</para>
		/// <para><tt>samples.Length % count = 0</tt>.</para>
		/// </summary>
		/// <remarks></remarks>
		/// <param name="count">sample-block length</param>
		/// <param name="f">callback 'function(FloatPoint);'</param>
		/// <param name="samples"></param>
		/// <returns>array[count](n+1)>={ [count](n+1)></> X min, Y max };</returns>
		public static FloatPoint Merge(int count, Func<FloatPoint,FloatPoint> f, params FloatPoint[] block)
		{
			var Avg = FloatPoint.Empty;
			for (int r =0; r < block.Length; r += 1) if (f!=null) Avg = f(Avg);
			return Avg;
		}
		#endregion
		
		public static FloatPoint MinMax16Mono(FloatPoint a, FloatPoint b)
		{
			return new FloatPoint(Math.Max(a.X,b.X),Math.Min(a.Y,b.Y));
		}
		/// <summary>
		/// Result depends on the number of channels requested and the Mode.
		/// If we are looking for L channel
		/// </summary>
		/// <param name="points"></param>
		/// <param name="channels"></param>
		/// <returns></returns>
		public static FloatPoint[] MinMax16(List<FloatPoint> points, int channels)
		{
			var Accum = new FloatPoint[channels];
			for (int i=0; i < points.Count; i+=channels) Accum[i] = FloatPoint.Empty;
			for (int i=0; i < points.Count; i+=channels)
				for (int j=i; j < channels; j++)
					Accum[j] = MinMax16Mono(points[i+j],Accum[i+j]);
			return Accum;
		}
		/// <summary>
		/// <para>We send to this method a buffer</para>
		/// <para>Buffer segments are multiples of </para>
		/// <para></para>
		/// <para></para>
		/// <para></para>
		/// </summary>
		/// <param name="width"></param>
		/// <param name="chanels"></param>
		/// <param name="buffer"></param>
		/// <returns></returns>
		public List<FloatPoint> GetInt16(int width, int chanels, params byte[] buffer)
		{
			// per sample:
			// byte-count: Int16 (mono)   = 2
			// byte-count: Int16 (stereo) = 2 * 2
			// -----------------------------------------
			// samplerate * bytecount
			// -----------------------------------------
			// window-width = 1920PX (width is window width in one usage)
			// (width)1920 (rounded to the nearest 1) is our buffer increment
			// 5M-Stereo = 44100 * 2 * 2 * 5 = 459.375 = (rounded) 459
			// Say our length of time in time is:
			// HMS = 00:05:23 = 5+(23/60) = 5.3833333333333333333333333333333 *2*2*44100 = 949,620 (samples) or 949,619.9999999999999999 (not rounded?)
			// -----------------------------------------
			// samples / width = 949,260 / 1920 = (ratio) 494.59375 or rounded as 495
			// -----------------------------------------
			// 459 frames @5M -- 20 samples per frame
			// 460 frames at 5M:23S -- 
			// -----------------------------------------
			// var z = DefaultWindowSize.X / r; // r
			// var r = Range(range);
			var list = new List<FloatPoint>();
			const int bits = 4;
			for  (int i=0; i < buffer.Length;)
			{
				list.Add(new FloatPoint( BitConverter.ToInt16(buffer, i), BitConverter.ToInt16(buffer, i+1) ));
				list.Add(new FloatPoint( BitConverter.ToInt16(buffer, i+2), BitConverter.ToInt16(buffer, i+3) ));
				i+=bits;
			}
			return list;
		}
		
		public AggrigationCalculator(WaveFormat info, long waveLength)
		{
			Load(info,waveLength); // hr
		}
		public AggrigationCalculator Load(WaveFormat info, long waveLength)
		{
			WaveInfo = info;
			WaveLength = waveLength;
			// note via bitspersample, 16bit is 2 bytes, hence the 2 in place of 16-bit.
			int bytesPerSample = WaveInfo.BitsPerSample >> 1;
			SampleSize = WaveInfo.Channels * (WaveInfo.BitsPerSample / 8) * WaveInfo.SampleRate;
			SampleCount = waveLength / SampleSize;
			MaximumSamples = SampleSize * 3600; // hr
			return this;
		}
	}
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
