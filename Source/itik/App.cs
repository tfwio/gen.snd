using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

[assembly: AssemblyTitle("itik")]
//[assembly: AssemblyDescription("")]
//[assembly: AssemblyConfiguration("")]
//[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("itik")]
//[assembly: AssemblyCopyright("")]
//[assembly: AssemblyTrademark("")]
//[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: AssemblyVersion("1.0.0.0")]
namespace itik
{
	public class app
	{
		
		[STAThread]
		public static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new gen.snd.Forms.ImpulseTrackerInstrumentDemoForm());
//			foreach (string argx in args)
//			{
//				if(System.IO.File.Exists(argx)) goto handlefile;
//				if (argx=="/9x") goto isold; else goto notold;
//		handlefile:
//		//	MessageBox.Show("handling file");
//		notold:
//			Application.EnableVisualStyles();
//			Application.SetCompatibleTextRenderingDefault(true);
//			}
//		isold:
//			Application.Run(new MainForm());
		}
	}
}
