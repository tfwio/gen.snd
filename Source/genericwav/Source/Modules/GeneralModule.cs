/* oio : 6/17/2014 8:06 PM */
using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.IO;
using gen.snd.Core;

namespace genericwav.Modules
{
	public abstract class GeneralModule : PropertyChange
	{
		abstract public List<string> Extensions { get; }
		
		public IDictionary<string,ModuleInfo> Meta {
			get { return meta; }
		} protected IDictionary<string,ModuleInfo> meta;
		
		public bool IsLoaded {
			get { return isLoaded; }
			protected set { isLoaded = value; OnPropertyChanged("IsLoaded"); }
		} bool isLoaded;
		
		public FileInfo ModulePath {
			get { return modulePath; }
			set { modulePath = value; OnPropertyChanged("ModulePath"); }
		} FileInfo modulePath;
		
//		abstract public void SetModulePath(string path);
		abstract public void LoadModule();
		abstract public void UnloadModule();
		
	}
	public abstract class GeneralModule<TFileModule>
		: GeneralModule
	{
		public TFileModule Module { get; protected set; }
	}
	
	
	/// <summary>
	/// <para>
	/// The idea of this ModuleInfo class is to contain information
	/// which can be used to provide data to something as complex
	/// as Windows.Forms.DataGridView or WPF's GridView.
	/// </para>
	/// <para>
	/// For each table or list provided to an app, we provide
	/// a ModuleInfo populated with data.
	/// </para>
	/// </summary>
	public class ModuleInfo
	{
		public void Add(string[] keys, params object[] obj)
		{
			var list = new List<object>();
//			Cols = null;
			System.Diagnostics.Debug.WriteLine("Subitem:");
			for (int i = 0; i < obj.Length; i++)
			{
				var subitem = string.Format(Cols[keys[i]], obj[i]).Trim();
				list.Add( subitem );
				System.Diagnostics.Debug.WriteLine(subitem);
			}
			data.Add(list.ToArray());
		}
		
		public IDictionary<string,string> Cols { get; set; }
		
		/// <summary>
		/// This is our set of data rows
		/// </summary>
		public IList<object[]> Data {
			get { return data; }
			set { data = value; }
		} IList<object[]> data = new List<object[]>();
	}
}
