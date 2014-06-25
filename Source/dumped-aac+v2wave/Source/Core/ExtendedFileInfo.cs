/*
 * User: oio
 * Date: 04/26/2011
 * Time: 14:19
 */
using System;
using System.IO;

namespace AvUtil.Core
{
	/// <summary>
	/// with no extensions!
	/// I don't know why this class exists.  It just wrps a fileinfo struct
	/// and does not really even bother to add anything to the available scope.
	/// The idea here was to use this as a base class for others.
	/// I believe the idea came from taglib-sharp.
	/// </summary>
	public class ExtendedFileInfo
	{
		public DirectoryInfo Directory { get { return info.Directory; } }
		public string DirectoryName { get { return info.DirectoryName; } }
		public string FileName { get { return info.FullName; } }
		public string Name { get { return info.Name; } }
		public string Extension { get { return info.Extension; } }
		public bool Exists { get { return info.Exists; } }
		public bool IsReadOnly { get { return info.IsReadOnly; } }
		
		public DateTime CreationTime { get { return info.CreationTime; } }
		public DateTime LastAccessTime { get { return info.LastAccessTime; } }
		public DateTime LastWriteTime { get { return info.LastWriteTime; } }
		
		public long Length { get { return info.Length; } }
		
		public FileInfo info { get;set; }
		public ExtendedFileInfo(string path)
		{
			info = new FileInfo(path);
		}
		
		static public implicit operator ExtendedFileInfo(string fileName)
		{
			return new ExtendedFileInfo(fileName);
		}
	}
}
