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
	/// This class isn't quite working or implemented yet.
	/// Once it is, will become a part of the common-sound library (gen.snd.common).
	/// </summary>
	class AggrigationCalculator
	{
		long SampleSize, SampleCount, WaveLength, MaximumSamples;
		public List<FloatPoint> Points = new List<FloatPoint>();
		
		/// <summary>
		/// What data is actually aggrigated.
		/// </summary>
		[Flags] public enum AggrigationMode
		{
			Mono = 0,
			Left = 1,
			Right = 2,
			Stereo = Left | Right
		}
		
		public static FloatPoint DefaultWindowSize {
			get { return new FloatPoint(1920, 1080); }
		}
		
		public FloatPoint WindowSize {
			get { return windowSize; } set { windowSize = value; }
		} FloatPoint windowSize = null;
		
		public FloatPoint WindowRatio {
			get { return windowRatio; }
			set { windowRatio = value; }
		} FloatPoint windowRatio = null;

		WaveFormat WaveInfo { get; set; }
		
		public FloatPoint[] GetSpline() { throw new NotImplementedException(); }

		#region Range(...), Merge(...) (Unused)
		public FloatPoint Range(FloatPoint range)
		{
			var z = DefaultWindowSize / range;
			return z % FloatPoint.One >= new FloatPoint(0.5f) ? z + 1 : z;
		}
		public static FloatPoint Merge(int count, Func<FloatPoint, FloatPoint> f, params FloatPoint[] block)
		{
			var Avg = FloatPoint.Empty;
			for (int r = 0; r < block.Length; r += 1) if (f != null) Avg = f(Avg);
			return Avg;
		}
		#endregion
		
		#region 16 bit Frame Aggrigator
		public static FloatPoint MinMax16Mono(FloatPoint a, FloatPoint b)
		{
			return new FloatPoint(Math.Max(a.X, b.X), Math.Min(a.Y, b.Y));
		}

		public static FloatPoint[] MinMax16(List<FloatPoint> points, int channels)
		{
			var Accum = new FloatPoint[channels];
			for (int i = 0; i < points.Count; i += channels) Accum[i] = FloatPoint.Empty;
			for (int i = 0; i < points.Count; i += channels) for (int j = i; j < channels; j++)
				Accum[j] = MinMax16Mono(points[i + j], Accum[i + j]);
			return Accum;
		}
		#endregion

		public List<FloatPoint> GetInt16(int width, int chanels, params byte[] buffer)
		{
			throw new NotImplementedException();
		}

		public AggrigationCalculator(WaveFormat info, long waveLength)
		{
			Load(info, waveLength);
		}
		static int GetFrameCount(int window, int hours, int minutes, int seconds, int channels = 2, int bytes = 2, int rate = 44100, float round = 0f)
		{
			long samples = channels * bytes * rate * ( Convert.ToInt64(hours)*60*60 + minutes*60 + seconds );
			return Convert.ToInt32(samples / window);
		}
		public AggrigationCalculator Load(WaveFormat info, long waveLength)
		{
			WaveInfo = info;
			WaveLength = waveLength;
			// note via bitspersample, 16bit is 2 bytes, hence the 2 in place of 16-bit.
			int bytesPerSample = WaveInfo.BitsPerSample >> 1;
			SampleSize = WaveInfo.Channels * (WaveInfo.BitsPerSample / 8) * WaveInfo.SampleRate;
			SampleCount = waveLength / SampleSize;
			MaximumSamples = SampleSize * 3600;
			// hr
			return this;
		}
	}
}


