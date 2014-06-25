/*
 * Created by SharpDevelop.
 * User: oio
 * Date: 9/13/2013
 * Time: 2:40 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using AvUtil.Core;
using NAudio.Wave;

namespace AvUtil.Views
{
	/// <summary>
	/// Description of UserControl2.
	/// </summary>
	public partial class MediaFoundationSamplerControl : UserControl
	{
		NAudioMediaFoundationExporter player = new NAudioMediaFoundationExporter();
		
		public MediaFoundationSamplerControl()
		{
			InitializeComponent();
			
			player.FormStoppedHandler = this.Event_OnPlaybackStopped;
			
			timer1.Interval = 250;
			btnPROFILES.DataSource = NAudioMediaFoundationExporter.DEFAULTPROFILES;
			btnPROFILES.SelectedIndex = 0;
			btnPROFILES.DisplayMember = "Title";
			player.SetProfile(btnPROFILES.SelectedIndex);
			
			trackBar1.Value = 0;
			trackBar1.Maximum = NAudioMediaFoundationExporter.TicksPerTrack;
			
			ConfigurePlayerEvents();
		}
		
		void Event_ProfileSelect(object sender, EventArgs args)
		{
			player.SetProfile(btnPROFILES.SelectedIndex);
		}
		
		#region UI Visual States
		void StatePlay()
		{
			btnPlay.Text = "Pause";
			btnStop.Enabled = true;
		}
		void StateStop()
		{
			btnPlay.Text = "Play";
			btnStop.Enabled = false;
		}
		void StatePause()
		{
			btnPlay.Text = "Play";
		}
		void StateX()
		{
			
		}
		#endregion
		
		void ConfigurePlayerEvents()
		{
			timer1.Tick += delegate { if (player.HasReader) ReportProgress(); };
			// =============================================
			// LOAD
			btnLoad.Click += delegate { player.BrowseFile(); StateStop(); };
			// PLAY
			btnPlay.Click += delegate(object o,EventArgs e){
				if (player.HasPlayer && player.HasReader && player.PlaybackState== PlaybackState.Playing)
				{
					player.Pause();
					StatePause();
				}
				else
				{
					player.Play(/*radioButtonWasapi.Checked*/);
					StatePlay();
				}
				timer1.Enabled = true;
			};
			// STOP
			btnStop.Click += delegate(object o,EventArgs e){ player.Stop(); };
			// VOLUME
			volumeSlider1.VolumeChanged += delegate { player.SetVolume(volumeSlider1.Volume); };
			// TRACKBAR
			trackBar1.Scroll += delegate { player.SetPosition(trackBar1.Value); };
			// FORM
			this.Disposed += delegate { player.Dispose(); player = null; };
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
		}
		
		#region Design
		
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the control.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.btnPROFILES = new System.Windows.Forms.ComboBox();
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			this.volumeSlider1 = new NAudio.Gui.VolumeSlider();
			this.btnStop = new System.Windows.Forms.Button();
			this.btnPlay = new System.Windows.Forms.Button();
			this.btnLoad = new System.Windows.Forms.Button();
			this.labelPosition = new System.Windows.Forms.Label();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
			this.SuspendLayout();
			// 
			// btnPROFILES
			// 
			this.btnPROFILES.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.btnPROFILES.Location = new System.Drawing.Point(390, 8);
			this.btnPROFILES.Name = "btnPROFILES";
			this.btnPROFILES.Size = new System.Drawing.Size(192, 21);
			this.btnPROFILES.TabIndex = 11;
			this.btnPROFILES.SelectedIndexChanged += new System.EventHandler(this.Event_ProfileSelect);
			// 
			// trackBar1
			// 
			this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.trackBar1.AutoSize = false;
			this.trackBar1.LargeChange = 1000;
			this.trackBar1.Location = new System.Drawing.Point(0, 32);
			this.trackBar1.Maximum = 1000000;
			this.trackBar1.Name = "trackBar1";
			this.trackBar1.Size = new System.Drawing.Size(672, 16);
			this.trackBar1.TabIndex = 3;
			this.trackBar1.TickFrequency = 100000;
			this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBar1.Value = 100000;
			// 
			// volumeSlider1
			// 
			this.volumeSlider1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.volumeSlider1.Font = new System.Drawing.Font("Segoe UI", 7.25F);
			this.volumeSlider1.Location = new System.Drawing.Point(588, 8);
			this.volumeSlider1.Name = "volumeSlider1";
			this.volumeSlider1.Size = new System.Drawing.Size(76, 21);
			this.volumeSlider1.TabIndex = 10;
			// 
			// btnStop
			// 
			this.btnStop.Enabled = false;
			this.btnStop.Location = new System.Drawing.Point(336, 8);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(48, 23);
			this.btnStop.TabIndex = 1;
			this.btnStop.Text = "Stop";
			this.btnStop.UseVisualStyleBackColor = true;
			// 
			// btnPlay
			// 
			this.btnPlay.Location = new System.Drawing.Point(288, 8);
			this.btnPlay.Name = "btnPlay";
			this.btnPlay.Size = new System.Drawing.Size(48, 23);
			this.btnPlay.TabIndex = 0;
			this.btnPlay.Text = "Play";
			this.btnPlay.UseVisualStyleBackColor = true;
			// 
			// btnLoad
			// 
			this.btnLoad.Location = new System.Drawing.Point(240, 8);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new System.Drawing.Size(48, 23);
			this.btnLoad.TabIndex = 0;
			this.btnLoad.Text = "Load";
			this.btnLoad.UseVisualStyleBackColor = true;
			// 
			// labelPosition
			// 
			this.labelPosition.Font = new System.Drawing.Font("Segoe Condensed", 18F, System.Drawing.FontStyle.Bold);
			this.labelPosition.Location = new System.Drawing.Point(8, 8);
			this.labelPosition.Name = "labelPosition";
			this.labelPosition.Size = new System.Drawing.Size(224, 24);
			this.labelPosition.TabIndex = 4;
			this.labelPosition.Text = "00:00:00.000";
			this.labelPosition.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// MediaFoundationSamplerControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.btnPROFILES);
			this.Controls.Add(this.trackBar1);
			this.Controls.Add(this.volumeSlider1);
			this.Controls.Add(this.labelPosition);
			this.Controls.Add(this.btnStop);
			this.Controls.Add(this.btnLoad);
			this.Controls.Add(this.btnPlay);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.Name = "MediaFoundationSamplerControl";
			this.Size = new System.Drawing.Size(675, 51);
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
			this.ResumeLayout(false);

		}
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Label labelPosition;
		private System.Windows.Forms.Button btnLoad;
		private System.Windows.Forms.Button btnPlay;
		private System.Windows.Forms.Button btnStop;
		private NAudio.Gui.VolumeSlider volumeSlider1;
		private System.Windows.Forms.TrackBar trackBar1;
		private System.Windows.Forms.ComboBox btnPROFILES;
		#endregion
		
	}
}
