using System.IO;
using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace genericwav.Models
{
//	public class FileSelectedEventHandler : RoutedEventArgs
//	{
//		public FileInfo FileInfo { get; set; }
//
//		public FileSelectedEventHandler(FileInfo file)
//		{
//			this.FileInfo = file;
//		}
//	}
	public class FileNavigatorModel : NotifyPropertyChanged
	{
//    	public event EventHandler<FileSelectedEventHandler> FileSelected;
//
//		protected virtual void OnFileSelected(FileInfo file)
//		{
//			if (FileSelected != null) {
//				FileSelected(this, e);
//			}
//		}

		public bool HasDirectory
		{
			get { return DirectoryPath != null; }
		}
		public bool HasChildrenDirectories
		{
			get { return lastPath != null && lastPath.GetDirectories().Count() > 0; }
		}
		public bool HasChildrenFiles
		{
			get { return lastPath != null && lastPath.GetFiles().Count() > 0; }
		}
		
		public DirectoryInfo DirectoryPath {
			get { return _DirectoryPath; }
			set { _DirectoryPath = value; LocalMuiSettings.SelectedPath = value==null ? "" : value.FullName; this.OnPropertyChanged("DirectoryPath"); }
		} DirectoryInfo _DirectoryPath = null;
		DirectoryInfo lastPath = null;
		
		public bool HasFile
		{
			get { return FilePath != null; }
		}
		public FileInfo FilePath
		{
			get { return _FilePath; }
			set { _FilePath = value; this.OnPropertyChanged("FilePath"); }
		} FileInfo _FilePath = null;
		
		public IList<DirectoryInfo> DirectoryListing
		{
			get { return directoryListing; }
			set { directoryListing = value; OnPropertyChanged("DirectoryListing"); }
		} IList<DirectoryInfo> directoryListing = null;
		
		public IList<FileInfo> FileListing
		{
			get { return fileListing; }
			set { fileListing = value; OnPropertyChanged("FileListing"); }
		} IList<FileInfo> fileListing = null;
		
		void ListDrives()
		{
			var list = new List<DirectoryInfo>();
			foreach (var drive in DriveInfo.GetDrives())
			{
				if (drive.IsReady) list.Add(new DirectoryInfo(drive.RootDirectory.FullName));
			}
			DirectoryListing = list;
		}
		
		int Compare(DirectoryInfo a, DirectoryInfo b)
		{
			if (a == null && b == null) return 0;
			else if (a == null) return -1;
			else if (b == null) return 1;
			return string.Compare(a.FullName, b.FullName);
		}
		public void LoadDirectory()
		{
			lastPath = DirectoryPath;
			if (DirectoryListing == null && lastPath == null) { ListDrives(); return; }
			else if (lastPath == null) { ListDrives(); return; }

			var dirs = new List<DirectoryInfo>();
			
			dirs.Add(lastPath.Parent);
			foreach (var item in lastPath.GetDirectories()) dirs.Add(item);
			
			dirs.Sort(Compare);
			DirectoryListing = null;
			DirectoryListing = new List<DirectoryInfo>(dirs);
			
			FileListing = null;
			if (!HasChildrenFiles) { return; }
			FileListing = new List<FileInfo>(lastPath.GetFiles());
		}
		
		public FileNavigatorModel()
		{
			string selected = LocalMuiSettings.SelectedPath;
			if (!string.IsNullOrEmpty(selected))
			{
				DirectoryPath = new DirectoryInfo(LocalMuiSettings.SelectedPath);
			}
			LoadDirectory();
		}

	}
}
