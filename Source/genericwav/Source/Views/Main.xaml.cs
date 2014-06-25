/* oio * 6/16/2014 * Time: 11:06 PM */
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using genericwav.Modules;
using genericwav.Source.Views.ImpulseTracker;

namespace genericwav.Views
{
	/// <summary>Interaction logic for Main.xaml</summary>
	public partial class Main : UserControl
	{
		static public DICT<GeneralModule,UserControl> Modules { get; internal set; }
		static public GeneralModule ApplyExtension(FileInfo file)
		{
			var ext = file.Extension;
			foreach (var m in Modules)
			{
				if (m.Key.Extensions.Contains(ext))
				    return m.Key;
			}
			return null;
		}
		static public GeneralModule ApplyExtension(string ext)
		{
			foreach (var m in Modules)
			{
				if (m.Key.Extensions.Contains(ext))
				    return m.Key;
			}
			return null;
		}
		
		static public UserControl GetViewForModule(GeneralModule module)
		{
			return Modules[module];
		}
		
		void FileNavigator_FileList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
//			MessageBox.Show(string.Format("",null));
			if (Navigator.Content!=null) Navigator.Content = null;
			FileNavigator.Model.FilePath = FileNavigator.FileList.SelectedValue as FileInfo;
			var m = ApplyExtension(FileNavigator.Model.FilePath);
			if (m==null) return;
			var v = Main.Modules[m];
			v.DataContext = null;
			Navigator.Content = v;
			m.ModulePath = FileNavigator.Model.FilePath;
			m.LoadModule();
			v.DataContext = m;
		}
		
		public Main()
		{
			InitializeComponent();
			//
			Modules = new DICT<GeneralModule,UserControl>();
			Modules.Add(new ImpulseTrackerInstrumentModel(), new ImpulseTrackerView());
			FileNavigator.FileList.MouseDoubleClick += new MouseButtonEventHandler(FileNavigator_FileList_MouseDoubleClick);
		}
	}
}