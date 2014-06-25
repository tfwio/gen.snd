/*
 * Created by SharpDevelop.
 * User: oio
 * Date: 1/25/2013
 * Time: 11:07 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AvUtil.Core
{
	/// <summary>
	/// Description of MP4BoxProcessDepreceated.
	/// </summary>
	partial class AvUtilFn
	{
		Stack<string> stack = new Stack<string>();
		public void TestCommand(object sender, EventArgs e) { TestCommand(); }
		public void TestCommand2(object sender, EventArgs e) { TestCommand2(); }
		
		public void TestCommand()
		{
			if (px1 != null)
			{
				px1.Close();
				px1.Dispose();
			}
			px1 = new Process();
			px1.StartInfo.FileName = Strings.Command_Resource_MP4BOX;
			px1.StartInfo.CreateNoWindow = true;
			px1.StartInfo.UseShellExecute = false;
			px1.StartInfo.Arguments = "-h";
			px1.StartInfo.RedirectStandardOutput = true;
			px1.StartInfo.RedirectStandardInput = true;
			stack.Clear();
			px1.EnableRaisingEvents = true;
			px1.Exited -= ProcessExitedEventHandler;
			px1.Exited += ProcessExitedEventHandler;
			px1.OutputDataReceived -= DataRecievedEventHandler;
			px1.OutputDataReceived += DataRecievedEventHandler;
			px1.Start();
			px1.BeginOutputReadLine();
			px1.WaitForExit();
			lock (this.processoutput = stack.ToArray())
			{
				OnGotProcessInfo(EventArgs.Empty);
			}
			Array.Clear(processoutput,0,processoutput.Length);
		}
		public void TestCommand2()
		{
			if (px1 != null)
			{
				px1.Close();
				px1.Dispose();
			}
			px1 = new Process();
			px1.StartInfo.FileName = "GenralOutputTest.exe";
			px1.StartInfo.CreateNoWindow = true;
			px1.StartInfo.UseShellExecute = false;
			px1.StartInfo.RedirectStandardOutput = true;
			px1.StartInfo.RedirectStandardInput = true;
			stack.Clear();
			px1.EnableRaisingEvents = true;
			px1.Exited -= ProcessExitedEventHandler;
			px1.Exited += ProcessExitedEventHandler;
			px1.OutputDataReceived -= DataRecievedEventHandler;
			px1.OutputDataReceived += DataRecievedEventHandler;
			px1.Start();
			px1.BeginOutputReadLine();
			px1.WaitForExit();
			px1.Close();
			lock (this.processoutput = stack.ToArray())
			{
				OnGotProcessInfo(EventArgs.Empty);
			}
			Array.Clear(processoutput,0,processoutput.Length);
		}
		
		public void ProcessExitedEventHandler(object sender, EventArgs e)
		{
			Console.Out.Write("The (hosting) process has exited.\n");
		}
		public void DataRecievedEventHandler(object sender, DataReceivedEventArgs e)
		{
			stack.Push(e.Data);
		}
		public string MakeCommand()
		{
			Time timeBegin = tBegin.Text;
			Time timeEnd = tEnd.Text;
			return string.Format(@"{0} -splitx {1}:{2} ""{3}""",Strings.Command_EXE_MP4BOX,timeBegin.TotalSeconds,timeEnd.TotalSeconds,fi.info.FullName);
		}
	}
}
