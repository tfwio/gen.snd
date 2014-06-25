/*
 * Created by SharpDevelop.
 * User: oio
 * Date: 5/24/2013
 * Time: 8:12 AM
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Internals;

using AvUtil.Core;

namespace AvUtil.Views
{
	public class AVBaseView : UserView<MainForm>
	{
	    // we can customize each userview to contain a type specific parent element
	    //public MainForm ParentControl
	    //{
	    //	get { return this.ViewForm; }
	    //}
	    public AVCoreModel Wrapper
	    {
	        get {
	            return ViewForm.Wrapper;
	        } set {
	            ViewForm.Wrapper = value;
	        }
	    }
	}
}
