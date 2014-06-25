/*
 * Created by SharpDevelop.
 * User: oio
 * Date: 04/27/2011
 * Time: 07:36
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace AvUtil
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.btnExecuteMain = new System.Windows.Forms.Button();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.panel2 = new System.Windows.Forms.Panel();
			this.btnPreviewMain = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label8 = new System.Windows.Forms.Label();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnExecuteMain
			// 
			this.btnExecuteMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnExecuteMain.Location = new System.Drawing.Point(625, 8);
			this.btnExecuteMain.Name = "btnExecuteMain";
			this.btnExecuteMain.Size = new System.Drawing.Size(102, 34);
			this.btnExecuteMain.TabIndex = 6;
			this.btnExecuteMain.TabStop = false;
			this.btnExecuteMain.Text = "Execute";
			this.btnExecuteMain.UseVisualStyleBackColor = true;
			// 
			// tabControl1
			// 
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.ItemSize = new System.Drawing.Size(120, 24);
			this.tabControl1.Location = new System.Drawing.Point(0, 59);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(735, 338);
			this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.tabControl1.TabIndex = 7;
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(251)))));
			this.panel2.Controls.Add(this.btnPreviewMain);
			this.panel2.Controls.Add(this.btnExecuteMain);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 397);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(735, 52);
			this.panel2.TabIndex = 9;
			// 
			// btnPreviewMain
			// 
			this.btnPreviewMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPreviewMain.Location = new System.Drawing.Point(517, 8);
			this.btnPreviewMain.Name = "btnPreviewMain";
			this.btnPreviewMain.Size = new System.Drawing.Size(102, 34);
			this.btnPreviewMain.TabIndex = 6;
			this.btnPreviewMain.TabStop = false;
			this.btnPreviewMain.Text = "Preview";
			this.btnPreviewMain.UseVisualStyleBackColor = true;
			// 
			// panel1
			// 
			this.panel1.BackgroundImage = global::AvUtil.Strings.image_title_area;
			this.panel1.Controls.Add(this.label8);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(735, 59);
			this.panel1.TabIndex = 10;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.BackColor = System.Drawing.Color.Transparent;
			this.label8.Font = new System.Drawing.Font("Segoe Condensed", 14F);
			this.label8.ForeColor = System.Drawing.Color.White;
			this.label8.Location = new System.Drawing.Point(8, 7);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(366, 44);
			this.label8.TabIndex = 8;
			this.label8.Text = "SHOUTCAST AAC/MP3 DUMP SAMPLER PROTOTYPE\r\nusing NAudio v1.7.1.17";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(735, 449);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.panel2);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Aloha";
			this.panel2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TabControl tabControl1;
		internal System.Windows.Forms.Button btnExecuteMain;
		internal System.Windows.Forms.Button btnPreviewMain;
	}
}
