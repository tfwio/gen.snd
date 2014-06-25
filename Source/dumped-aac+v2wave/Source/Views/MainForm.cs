/*
 * User: oio
 * Date: 04/27/2011
 * Time: 07:36
 */
using System;
using System.Collections.Generic;
using System.Internals;
using System.Reflection;
using System.Windows.Forms;

using AvUtil.Core;
using AvUtil.Views;

namespace AvUtil
{
	// 
	// http://www.java2s.com/Tutorial/CSharp/0460__GUI-Windows-Forms/FileDropTarget.htm
	// 
	public partial class MainForm : Form
	{
		#region APP
		[STAThread]
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
		#endregion
		
		internal AVCoreModel Wrapper;
		
		internal TimeCalc tcalc = new TimeCalc(0,0);
		
		#region View Management
		
		static int ViewPointCompare(AVViewPoint A, AVViewPoint B)
		{
			// these next two lines provide a value for
			// views which provide no index.
			int a = A.InsertionIndex.HasValue ? A.InsertionIndex.Value: 0;
			int b = B.InsertionIndex.HasValue ? B.InsertionIndex.Value: 0;
			
			// Use string comparison for values with equal indexes.
			if ( a == b ) return A.Title.CompareTo(B.Title);
			
			// Standard comparison.
			return a - b;
		}
		static int ViewPointCompare_Reverse(AVViewPoint A, AVViewPoint B)
		{
			return ViewPointCompare(A,B) * -1;
		}
		
		List<Assembly> AssemblyReferences { get;set; }
		
		List<AVViewPoint> ViewList { get; set; }
		
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			
			List<string> ViewNames = new List<string>();
			SuspendLayout();
			foreach (AVViewPoint point in ViewList)
			{
				point.View.ViewForm = this;
				ViewNames.Add(point.Title);
				TabPage pageX = new TabPage(point.Title);
				pageX.Controls.Add(point.View);
				point.View.Dock = DockStyle.Fill;
				this.tabControl1.TabPages.Add(pageX);
			}
			ResumeLayout();
		}
		#endregion
		
		public MainForm(params Assembly[] viewSpace)
		{
			// 'this'
			AssemblyReferences = new List<Assembly>(){ GetType().Assembly };
			
			foreach (var asm in viewSpace) AssemblyReferences.Add(asm);
			ViewList = ViewPointBase.EnumerateViewTypes<AVViewPoint>(AssemblyReferences.ToArray());
			ViewList.Sort(ViewPointCompare);
			
			InitializeComponent();
			
		}
	}
}
