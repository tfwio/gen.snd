using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Microsoft.Win32;
using NAudio.Wave;
using FntSize = FirstFloor.ModernUI.Presentation.FontSize;
namespace genericwav
{
	public class NAudioMediaFoundationExporter : IDisposable
	{
		public PlaybackState PlaybackState {
			get {
				return (HasPlayer) ? wavePlayer.PlaybackState : PlaybackState.Stopped;
			}
		}

		#region Constants & ReadOnly Memory
		OpenFileDialog ofd = new OpenFileDialog() {
			Filter = "All Known Formats|*.mp3;*.m4a;*.mp4;*.m4b;*.aac;*.mpeg|mpeg/aac+|*.m4a|mpeg/mp3|*.mp3|All files|*",
			Title = "Load Audio File"
		};

		static readonly SampleSetting DefaultSampleSetting = new SampleSetting(0, "Auto-Detect", 0, 0, 0);

		/// <summary>
		/// 10,000,000
		/// </summary>
		public const int TicksPerTrack = 10000000;

		const string EmptyTime = "00:00:00.000";

		const string FormatTime = "{0:##,###,00}:{1:00}:{2:00}.{3:d3}";

		const string FormatProfile = "nch={0}, rate={1}, bps={2}\n{3}\n{4}";

		const string FormatProfileShort = "{0} CH {2} BPS, RATE: {1}";

		public enum PlaybackMethod
		{
			Winapi,
			Wasapi,
			DataProvider
		}

		#endregion
		#region Properties
		private IWavePlayer wavePlayer;

		private WaveStream reader;

		bool IsUrlMedia = false;

		/// <summary>
		/// We use this to destroy the player when we are exiting
		/// the application.
		/// </summary>
		public bool IsWaitingForExit {
			get {
				return isWaitingForExit;
			}
			set {
				isWaitingForExit = value;
			}
		}

		bool isWaitingForExit = false;

		#region Playlist coming soon...
		public string[] LoadedFile {
			get {
				return loadedFile;
			}
			set {
				loadedFile = value;
			}
		}

		string[] loadedFile = null;

		public int LoadedFileCount {
			get {
				return loadedFile.Length;
			}
		}

		#endregion
		#endregion
		#region Active File or Content Information
		bool UseOverrides {
			get {
				return SELECTEDPROFILEID != 0;
			}
		}

		public PlaybackMethod PlayerMethod {
			get {
				return playbackMethod;
			}
			set {
				playbackMethod = value;
				DestroyPlayer();
			}
		}

		PlaybackMethod playbackMethod = PlaybackMethod.DataProvider;

		#endregion
		#region Boolean Checks (HasReader, HasPlayer, HasFile)
		public bool HasReader {
			get {
				return reader != null;
			}
		}

		public bool HasPlayer {
			get {
				return wavePlayer != null;
			}
		}

		public bool HasFile {
			get {
				return ofd.FileNames.Length != 0 && File.Exists(ofd.FileNames[0]);
			}
		}

		#endregion
		public MinMaxValue TimeValue {
			get {
				return _TimeValue;
			}
			set {
				_TimeValue = value;
			}
		}

		MinMaxValue _TimeValue = new MinMaxValue(0, TicksPerTrack, 0);

		//	    public string CurrentTimePosition { get{ return string.Format(FormatTime,reader.CurrentTime.Hours,reader.CurrentTime.Minutes,reader.CurrentTime.Seconds,reader.CurrentTime.Milliseconds); } }
//		public string CurrentGuidProfile {
//			get {
//				return string.Format(
//					FormatProfile,
//					reader.WaveFormat.Channels,
//					reader.WaveFormat.SampleRate,
//					reader.WaveFormat.BitsPerSample,
//					(reader as MediaFoundationReader).TypeMain,
//					(reader as MediaFoundationReader).TypeSub);
//			}
//		}

		#region TimeStatus (TimeStatusInt, TimeStatusStringShort, TimeStatusStringLong, TimeStatus)
		/// <summary>
		/// Applied to a TrackBar (seek-bar).
		/// </summary>
		public int TimeStatusInt {
			get {
				return TimeValue.Value = Math.Min((int)((TimeValue.Maximum * reader.Position) / reader.Length), TimeValue.Maximum);
			}
		}

		/// <summary>
		/// gets H:M.S format (TimeFormatString.HMS)
		/// </summary>
		public string TimeStatusStringShort {
			get {
				if (reader == null)
					return "00:00.000/00:00.000";
				TimeFormat current, total;
				current = new TimeFormat(0, 0, 0, reader.CurrentTime.TotalSeconds, 0);
				total = new TimeFormat(0, 0, 0, reader.TotalTime.TotalSeconds, 0);
				if (!IsUrlMedia)
					return string.Format("{0}/{1}", current.ToString(TimeFormatString.HMS), total.ToString(TimeFormatString.HMS));
				// return string.Format("{0:n3}/{1:n3}", reader.CurrentTimeSeconds, reader.TotalTimeSeconds);
				return EmptyTime;
			}
		}

		public int TimeStatus {
			get {
				/*if (!IsUrlMedia) */TimeValue.Value = Math.Min((int)((TimeValue.Maximum * reader.Position) / reader.Length), TimeValue.Maximum);
				Debug.Print("TimeStatus: {0}, {1}, {2}\n", TimeValue.Minimum, TimeValue.Maximum, TimeValue.Value);
				return IsUrlMedia ? 0 : TimeValue.Value;
			}
		}

		#endregion
		#region Methods
		void DestroyPlayer()
		{
			if (HasPlayer) {
				wavePlayer.Dispose();
				wavePlayer = null;
			}
		}

		void DestroyReader()
		{
			if (HasReader) {
				reader.Dispose();
				reader = null;
			}
		}

		public void SetVolume(float value)
		{
			try {
				wavePlayer.Volume = value;
			}
			catch {
			}
		}

		public long GetPosition()
		{
			return reader.Position;
		}

		public long SetPosition(long value)
		{
			return reader.Seek(value, SeekOrigin.Begin);
		}

		/// <summary>
		/// This is generally a setter for <tt>TimeValue.Value</tt>.
		/// </summary>
		/// <param name="value"></param>
		public void SetPosition(int value)
		{
			// TimeValue.Minimum = value.Minimum;
			// TimeValue.Maximum = value.Maximum;
			// TimeValue.Value = ScrollHelper(reader.Length);
			// long rv = ;
			if (HasReader)
				reader.Position = (value * reader.Length) / TimeValue.Maximum;
			;
			// Debug.Print("local value: {0}, Tick: {1}, TimeValue: {2}, {3}:{4}:{5} (vminmax)",value, rv, TimeValue.Value, TimeValue.Minimum, TimeValue.Maximum);
		}

		/// <summary>
		/// This method is used to override the "automatically detected"
		/// channel, rate and bits-per-sample information in the case
		/// that no (or incorrect) sampling information is obtained.
		/// </summary>
		/// <param name = "index"></param>
		public void SetProfile(int index)
		{
			SELECTEDPROFILEID = index;
			//	        if (sender==null) return;
			//	        foreach (ToolStripMenuItem item in (sender.OwnerItem as ToolStripMenuItem).DropDownItems)
			//	            item.Checked = (((int)item.Tag) == SELECTEDPROFILEID);
			// set the text of the parent menu item to our item's title
			//	        sender.OwnerItem.Text = SELECTEDPROFILE.Title;
			Debug.Print("Set: CH: {0}, BITS: {1}, RATE: {2}\n", SELECTEDPROFILE.NumberOfChannels, SELECTEDPROFILE.BitDepth, SELECTEDPROFILE.SampleRate);
			Stop();
			Debug.Print("Stop\n", SELECTEDPROFILE.Title);
			Debug.Print("Reloading={0}\n", HasFile);
			if (HasFile)
				Load();
		}

		#endregion
		#region Default Sample Settings
		public static readonly SampleSetting[] DEFAULTPROFILES = new SampleSetting[8] {
			DefaultSampleSetting,
			new SampleSetting(1, "Shoutcast AAC+ 2Ch 44100", 44100, 2, 16),
			new SampleSetting(2, "Shoutcast MP3: 1Ch 22050", 22050, 1, 16),
			new SampleSetting(3, "Shoutcast MP3: 2Ch 22050", 22050, 2, 16),
			new SampleSetting(4, "Shoutcast MP3: 1Ch 44100", 44100, 1, 16),
			new SampleSetting(5, "Shoutcast MP3: 2Ch 44100", 44100, 2, 16),
			new SampleSetting(6, "Shoutcast MP3: 1Ch 48000", 48000, 1, 16),
			new SampleSetting(7, "Shoutcast MP3: 2Ch 48000", 48000, 2, 16),
		};

		public SampleSetting SELECTEDPROFILE {
			get {
				return DEFAULTPROFILES[SELECTEDPROFILEID];
			}
		}

		int SELECTEDPROFILEID = 0;

		#endregion
		public NAudioMediaFoundationExporter()
		{
		}

		~NAudioMediaFoundationExporter()
		{
			Dispose();
		}

		#region Actions
		long ScrollHelper(long length)
		{
			return ScrollHelper(TimeValue.Value, TimeValue.Maximum, length);
		}

		long ScrollHelper(int value, int maximum, long length)
		{
			return (value * length) / maximum;
		}

		#endregion
		#region Playback
		public void BrowseFile(bool soft = false)// and load
			
		{
			if (wavePlayer != null) {
				wavePlayer.Dispose();
				wavePlayer = null;
			}
			if (HasFile && soft) {
			}
			else {
				if (!ofd.ShowDialog().HasValue)
					return;
			}
			IsUrlMedia = false;
			LoadedFile = ofd.FileNames;
			Load();
			// waiting for play button to be depressed
		}

		void Load()
		{
			if (HasReader)
				reader.Dispose();
			if (UseOverrides)
				reader = new MediaFoundationReader(
					LoadedFile[0]//,
//					UseOverrides,
//					SELECTEDPROFILE.BitDepth,
//					SELECTEDPROFILE.NumberOfChannels,
//					SELECTEDPROFILE.SampleRate
				);
			else
				reader = new MediaFoundationReader(ofd.FileName);
			Debug.WriteLine("use overrides = {0}", UseOverrides);
		}

		public void Play()// bool wasapi=false
			
		{
			if (!HasReader) {
				/*if (!HasFile)) */BrowseFile();
				if (!HasReader)
					return;
			}
			if (!HasPlayer) {
				wavePlayer = (IWavePlayer)new NAudioAggrigationPlayer();
				//wasapi ? (IWavePlayer)new WasapiOutGuiThread() : (IWavePlayer)new WaveOut();
				if (FormStoppedHandler != null)
					wavePlayer.PlaybackStopped -= FormStoppedHandler;
				if (FormStoppedHandler != null)
					wavePlayer.PlaybackStopped += FormStoppedHandler;
				wavePlayer.PlaybackStopped -= Event_OnPlaybackStopped;
				wavePlayer.PlaybackStopped += Event_OnPlaybackStopped;
				wavePlayer.Init(reader);
			}
			wavePlayer.Play();
		}

		public void Stop(bool dispose = false)
		{
			if (dispose)
				IsWaitingForExit = true;
			if (HasPlayer)
				wavePlayer.Stop();
		}

		public void Pause()
		{
			if (HasPlayer)
				wavePlayer.Pause();
		}

		#endregion
		public EventHandler<StoppedEventArgs> FormStoppedHandler {
			get;
			set;
		}

		public void Event_OnPlaybackStopped(object sender, StoppedEventArgs stoppedEventArgs)
		{
			if (!IsUrlMedia)
				reader.Position = 0;
			if (stoppedEventArgs.Exception != null) {
				MessageBox.Show(stoppedEventArgs.Exception.Message);
			}
			if (IsWaitingForExit) {
				DestroyPlayer();
				DestroyReader();
			}
		}

		public void Dispose()
		{
			Debug.WriteLine("Player.Dispose");
			if (HasPlayer && HasReader && HasFile)
				Stop(true);
			else {
				DestroyPlayer();
				DestroyReader();
			}
		}
	}
}


