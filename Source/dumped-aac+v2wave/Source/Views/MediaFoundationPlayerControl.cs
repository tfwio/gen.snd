/*
 * Created by SharpDevelop.
 * User: oio
 * Date: 9/13/2013
 * Time: 2:40 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using AvUtil.Core;
using NAudio.Wave;
namespace AvUtil.Views
{
	/// <summary>
	/// Description of UserControl2.
	/// </summary>
	public partial class MediaFoundationPlayerControl : UserControl
	{
		NAudioMediaFoundationPlayer player = new NAudioMediaFoundationPlayer();

		public MediaFoundationPlayerControl()
		{
			InitializeComponent();
			player.FormStoppedHandler = this.Event_OnPlaybackStopped;
			//            this.Disposed += OnDisposed;
			timer1.Interval = 250;
			btnPROFILES.DataSource = NAudioMediaFoundationPlayer.DEFAULTPROFILES;
			btnPROFILES.SelectedIndex = 0;
			btnPROFILES.DisplayMember = "Title";
			player.SetProfile(btnPROFILES.SelectedIndex);
			trackBar1.Value = 0;
			trackBar1.Maximum = NAudioMediaFoundationPlayer.TicksPerTrack;
			ConfigurePlayerEvents();
		}

		void Event_ProfileSelect(object sender, EventArgs args)
		{
			player.SetProfile(btnPROFILES.SelectedIndex);
		}

		void Event_LoadFromURL(object sender, EventArgs e)
		{
			//            if (wavePlayer != null)
			//            {
			//                wavePlayer.Dispose();
			//                wavePlayer = null;
			//            }
			//
			//            if (reader != null) reader.Dispose();
			//            reader = new MediaFoundationReader(textBox1.Text,true);
			//            IsUrlMedia = true;
			//            buttonPlay.PerformClick();
			//
		}

		void StatePlay()
		{
			btnPlay.Text = "Pause";
			btnStop.Enabled = true;
			btnLoadSoft.Enabled = true;
		}

		void StateStop()
		{
			btnPlay.Text = "Play";
			btnStop.Enabled = false;
			btnLoadSoft.Enabled = false;
		}

		void StatePause()
		{
			btnPlay.Text = "Play";
		}

		void StateX()
		{
		}

		void ConfigurePlayerEvents()
		{
			timer1.Tick += delegate {
				if (player.HasReader)
					ReportProgress();
			};
			// =============================================
			// LOAD (SOFT)
			btnLoadSoft.Click += delegate {
				StateStop();
				long pos = player.GetPosition();
				player.BrowseFile(true);
				player.SetPosition(pos);
				//                btnPlay.PerformClick();
			};
			// LOAD
			btnLoad.Click += delegate {
				player.BrowseFile();
				StateStop();
			};
			// PLAY
			btnPlay.Click += delegate(object o, EventArgs e) {
				if (player.HasPlayer && player.HasReader && player.PlaybackState == PlaybackState.Playing) {
					player.Pause();
					StatePause();
				}
				else {
					player.Play(/*radioButtonWasapi.Checked*/);
					StatePlay();
				}
				timer1.Enabled = true;
			};
			// STOP
			btnStop.Click += delegate(object o, EventArgs e) {
				player.Stop();
			};
			// PAUSE
			//            btnPause.Click += delegate(object o,EventArgs e){ player.Pause(); };
			// VOLUME
			volumeSlider1.VolumeChanged += delegate {
				player.SetVolume(volumeSlider1.Volume);
			};
			// TRACKBAR
			trackBar1.Scroll += delegate {
				player.SetPosition(trackBar1.Value);
			};
			// FORM
			this.Disposed += delegate {
				player.Dispose();
				//                Thread.Sleep(400);
				player = null;
				//                e.Cancel = false;
			};
		}

		void Event_OnPlaybackStopped(object sender, StoppedEventArgs stoppedEventArgs)
		{
			timer1.Enabled = false;
			ReportProgress();
			trackBar1.Value = 0;
			StateStop();
		}

		void ReportProgress()
		{
			trackBar1.Value = player.TimeStatusInt;
			labelPosition.Text = player.TimeStatusStringShort;
			//            label3.Text = player.TimeStatusStringLong;
		}
	}
}


