/*
 * Created by SharpDevelop.
 * User: oio
 * Date: 9/9/2013
 * Time: 5:51 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

using AvUtil.Core;
using NAudio.Wave;

namespace AvUtil.Views
{
    /// <summary>
    /// Description of UserControl2.
    /// </summary>
    public partial class MediaFoundationView : AVBaseView
    {
        public MediaFoundationView()
        {
            InitializeComponent();
        }
    }
    public class MediaFoundationrViewProvider : AVViewPoint
    {
        public override string Title {
            get { return "Player"; }
        }
        public override Nullable<int> InsertionIndex {
            get { return 0; }
        }
        public MediaFoundationrViewProvider()
        {
            this.View = new MediaFoundationView();
        }
    }
}
