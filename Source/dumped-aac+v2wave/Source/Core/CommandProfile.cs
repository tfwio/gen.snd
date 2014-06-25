/*
 * Created by SharpDevelop.
 * User: oio
 * Date: 2/5/2013
 * Time: 12:49 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

//
// THIS CLASS IS NOT USED
//
namespace AvUtil.Core
{
	/// <summary>
	/// This is a temporary class for eventually running FFMPEG.exe
	/// or any other executable via configuration using this as a model.
	/// </summary>
	public class SingleInputCommandProfile : CommandProfile
	{
		/// <summary>
		/// Expected templates are: $(InputFile), $(OutputFile)
		/// </summary>
		public string OutputFile { get;set; }
		public string InputFile { get;set; }
		public IDictionary<string, string> Parameters { get; set; }

		public ExtendedFileInfo InputInfo { get; set; }
		public ExtendedFileInfo OutputInfo { get; set; }
		
		override internal string Prepare()
		{
			string result = Command;
			foreach (KeyValuePair<string, string> kvp in Parameters) result = result.Replace(kvp.Key,kvp.Value);
			return result
				.Replace("$(InputFile)",InputFile)
				.Replace("$(OutputFile)",OutputFile);
		}
		public override void Run()
		{
			string command = Prepare();
			Process.Start(BinaryPath,command);
		}

	}
	abstract public class CommandProfile
	{
		/// <summary>
		/// This is a command template where our overriding class is going to
		/// provide it's variables.
		/// </summary>
		public string Command { get; set; }
		public string BinaryPath { get; set; }
		abstract public void Run();
		abstract internal string Prepare();
	}
}
