/*
 * Created by SharpDevelop.
 * User: tfooo
 * Date: 12/26/2005
 * Time: 5:44 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace DspAudio.Forms
{

	public interface IUi
	{
		Rectangle ClientRectangle { get ; }
	}
	public interface IUiOffset
	{
		event EventHandler XOffsetChanged;
		event EventHandler YOffsetChanged;
	}
	internal interface IKnobSetting : IUi
	{
		int Maximum { get; set; }
		int Minimum { get; set; }
		int Length { get; set; }
		KnobType Mode { get; set; }
		int Offset { get; set; }
		int KnobFrameIndex { get; set; }
		Rectangle RowsCols { get; set; }
		Image KnobImage { get; set; }
	}
}
