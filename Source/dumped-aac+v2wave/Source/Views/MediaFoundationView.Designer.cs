/*
 * Created by SharpDevelop.
 * User: oio
 * Date: 9/9/2013
 * Time: 5:51 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace AvUtil.Views
{
    partial class MediaFoundationView
    {        
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        	this.components = new System.ComponentModel.Container();
        	this.timer1 = new System.Windows.Forms.Timer(this.components);
        	this.userControl21 = new AvUtil.Views.MediaFoundationSamplerControl();
        	this.userControl22 = new AvUtil.Views.MediaFoundationPlayerControl();
        	this.userControl23 = new AvUtil.Views.MediaFoundationPlayerControl();
        	this.userControl24 = new AvUtil.Views.MediaFoundationPlayerControl();
        	this.panel1 = new System.Windows.Forms.Panel();
        	this.label1 = new System.Windows.Forms.Label();
        	this.panel1.SuspendLayout();
        	this.SuspendLayout();
        	// 
        	// userControl21
        	// 
        	this.userControl21.AutoSize = true;
        	this.userControl21.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        	this.userControl21.BackColor = System.Drawing.Color.Gray;
        	this.userControl21.Dock = System.Windows.Forms.DockStyle.Top;
        	this.userControl21.Font = new System.Drawing.Font("Segoe UI", 8.25F);
        	this.userControl21.Location = new System.Drawing.Point(8, 40);
        	this.userControl21.Name = "userControl21";
        	this.userControl21.Size = new System.Drawing.Size(670, 51);
        	this.userControl21.TabIndex = 0;
        	// 
        	// userControl22
        	// 
        	this.userControl22.AutoSize = true;
        	this.userControl22.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        	this.userControl22.BackColor = System.Drawing.Color.White;
        	this.userControl22.Dock = System.Windows.Forms.DockStyle.Top;
        	this.userControl22.Font = new System.Drawing.Font("Segoe UI", 8.25F);
        	this.userControl22.Location = new System.Drawing.Point(8, 91);
        	this.userControl22.Name = "userControl22";
        	this.userControl22.Size = new System.Drawing.Size(670, 51);
        	this.userControl22.TabIndex = 1;
        	// 
        	// userControl23
        	// 
        	this.userControl23.AutoSize = true;
        	this.userControl23.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        	this.userControl23.BackColor = System.Drawing.Color.White;
        	this.userControl23.Dock = System.Windows.Forms.DockStyle.Top;
        	this.userControl23.Font = new System.Drawing.Font("Segoe UI", 8.25F);
        	this.userControl23.Location = new System.Drawing.Point(8, 142);
        	this.userControl23.Name = "userControl23";
        	this.userControl23.Size = new System.Drawing.Size(670, 51);
        	this.userControl23.TabIndex = 2;
        	// 
        	// userControl24
        	// 
        	this.userControl24.AutoSize = true;
        	this.userControl24.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        	this.userControl24.BackColor = System.Drawing.Color.White;
        	this.userControl24.Dock = System.Windows.Forms.DockStyle.Top;
        	this.userControl24.Font = new System.Drawing.Font("Segoe UI", 8.25F);
        	this.userControl24.Location = new System.Drawing.Point(8, 193);
        	this.userControl24.Name = "userControl24";
        	this.userControl24.Size = new System.Drawing.Size(670, 51);
        	this.userControl24.TabIndex = 3;
        	// 
        	// panel1
        	// 
        	this.panel1.Controls.Add(this.label1);
        	this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
        	this.panel1.Location = new System.Drawing.Point(8, 8);
        	this.panel1.Name = "panel1";
        	this.panel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 4);
        	this.panel1.Size = new System.Drawing.Size(670, 32);
        	this.panel1.TabIndex = 4;
        	// 
        	// label1
        	// 
        	this.label1.BackColor = System.Drawing.SystemColors.Info;
        	this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.label1.ForeColor = System.Drawing.SystemColors.InfoText;
        	this.label1.Location = new System.Drawing.Point(0, 0);
        	this.label1.Name = "label1";
        	this.label1.Size = new System.Drawing.Size(670, 28);
        	this.label1.TabIndex = 6;
        	this.label1.Text = "This player is capable of playing files up to 24 hours long.";
        	this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        	// 
        	// MediaFoundationView
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.AutoScroll = true;
        	this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
        	this.Controls.Add(this.userControl24);
        	this.Controls.Add(this.userControl23);
        	this.Controls.Add(this.userControl22);
        	this.Controls.Add(this.userControl21);
        	this.Controls.Add(this.panel1);
        	this.Name = "MediaFoundationView";
        	this.Padding = new System.Windows.Forms.Padding(8);
        	this.Size = new System.Drawing.Size(686, 266);
        	this.panel1.ResumeLayout(false);
        	this.ResumeLayout(false);
        	this.PerformLayout();

        }
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private AvUtil.Views.MediaFoundationPlayerControl userControl24;
        private AvUtil.Views.MediaFoundationPlayerControl userControl23;
        private AvUtil.Views.MediaFoundationPlayerControl userControl22;
        private AvUtil.Views.MediaFoundationSamplerControl userControl21;

        #endregion

        private System.Windows.Forms.Timer timer1;
    }
}
