using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

//425:12
namespace AvUtil.Core
{

    /// <summary>
    /// UI Bridge to calculator functions and other functions as well.
    /// <para>
    /// If more is exibited then a splitter or time calculator, then
    /// this class needs to generally be re-written.
    /// </para>
    /// </summary>
    public class AVCoreModel : AVCoreSettings //avmodel
    {
        #region User Controls
        
        readonly Timer tim = null;
        
        public TextBox tBegin, tEnd;
        
        public Label lBegin, lEnd;
        
        /// <summary>
        /// This should be for the splitterview.
        /// </summary>
        public RichTextBox richTextBox1;

        #endregion

        public string[] processoutput = null;

        public string TextOutput { get; set; }

        /// <summary>Updates and retrieves command text (minus exe-name).</summary>
        /// <remarks>This also is triggered via our timer;<br/>
        /// TimerUpdate();<br/>
        /// ExecuteSplitter();<br/>
        /// MainForm.Event_Preview(sender,event)</remarks>
        /// <returns>The TextOutput property value.</returns>
        public string UpdateText()
        {
            TimeBegin = tBegin.Text;
            TimeEnd = tEnd.Text; // length---not time-end
            
            return  TextOutput = string
            	.Format(
            		Strings.Command_FFMPEG_Command,
            		TestInputFileString/**/,
            		(AutoCalculateLength ? TimeLength.TotalSeconds:TimeEnd.TotalSeconds), //.TotalSeconds
            		TimeBegin.TotalSeconds/*TimeBegin*/, //.TotalSeconds
            		FullOutputFileName
            	);
        }

        public AVCoreModel()
        {
            tim = new Timer() {
                Interval = 1200
            };
            tim.Tick += Event_UpdateInterval;
        }

        // Events
        #region File In/Out Automation on Set, & Events

        public event EventHandler DirectoryLoaded;
        public virtual void OnDirectoryLoaded(EventArgs e)
        {
            if (DirectoryLoaded != null) {
                DirectoryLoaded(this, e);
            }
        }

        public event EventHandler GotProcessInfo;
        protected virtual void OnGotProcessInfo(EventArgs e)
        {
            if (GotProcessInfo != null) {
                GotProcessInfo(this, e);
            }
        }

        #endregion

        //EventHandler
        #region GetInput/Output File, SplitterCommand, TimerReset
        public void Event_GetInputFile(object sender, EventArgs e)
        {
            GetInputFile();
        }
        public void Event_GetOutputFile(object sender, EventArgs e)
        {
            GetOutputFile();
        }
        public void Event_ExecuteSplitter(object sender, EventArgs e)
        {
            ExecuteSplitter();
        }
        public void Event_TimerRest(object sender, KeyPressEventArgs e)
        {
            TimerReset();
        }
        #endregion

        // Timer Handler
        void Event_UpdateInterval(object sender, EventArgs e)
        {
            TimerUpdate();
            richTextBox1.Text = TextOutput;
        }

        // File Dialog Usage
        #region Get Input and Output Files
        public void GetInputFile()
        {
            if (ofd.ShowDialog() == DialogResult.OK) {
                FileInput = new ExtendedFileInfo(ofd.FileName);
            }
        }
        public void GetOutputFile()
        {
            if (sfd.ShowDialog() == DialogResult.OK) {
                FileOutput = new ExtendedFileInfo(sfd.FileName);
            }
        }
        #endregion


        #region Timer Related Functions
        public void TimerReset()
        {
            if (tim.Enabled) tim.Stop();
            tim.Start();
        }
        void TimerUpdate()
        {
            tim.Stop();
            TimeCapsule.FormatHMS(lBegin, tBegin);
            TimeCapsule.FormatHMS(lEnd, tEnd);
            UpdateText();
        }

        #endregion
        
        /// <summary>Executes the process (having been prepared first).</summary>
        public void ExecuteProcess()
        {
        	ProcessStartInfo psi = new ProcessStartInfo(exepath.FileName,TextOutput);
        	psi.RedirectStandardOutput = false; //                         same as default
        	psi.RedirectStandardInput = false; //                          same as default
        	psi.UseShellExecute = false; //                                not a default
            Process px = Process.Start(psi);
            px.OutputDataReceived += delegate(object sender, DataReceivedEventArgs e) {
            	richTextBox1.Text += string.Format(e.Data);
            };
//            while (px.StandardOutput.Read(stdBuffer,px.StandardOutput.BaseStream.Position,))
            	
        }

        /// <summary>Calls ExecuteProcess after (also) preparing.</summary>
        public void ExecuteSplitter()
        {
            //
            UpdateText();
            //
            richTextBox1.Text = TextOutput;
            //
            ExecuteProcess();
        }

        public void GetFormat(Label linput, TextBox tinput)
        {
            string outvalue = null;
            try {
                outvalue = GetTimeFormat(tinput.Text).ToString();
            } catch {  }

            linput.Text = string.IsNullOrEmpty(outvalue) ? "00:00:00" : outvalue;
            linput.ForeColor = string.IsNullOrEmpty(outvalue) ? System.Drawing.Color.Red : Label.DefaultForeColor;
            
            TimeStruct? ts = TimeEx.GetStruct(tinput.Text);
            tinput.Text = ts.HasValue ? ts.Value.GetHMS() : "00:00:00";
        }

        static public double GetTimeFormat(string input)
        {
            TimeEx smpte = input;
            return smpte.TotalSeconds;
        }

        class TimeSplitter
        {
            public TimeEx TimeBegin;
            public TimeEx TimeEnd;
            public TimeEx TimeLength;

            public void Reset(string timeBegin, string timeTwo, bool twoIsEnd=true)
            {
                TimeBegin = timeBegin;
                if (twoIsEnd) {
                    TimeEnd = timeTwo;
                    TimeLength = TimeEnd - TimeBegin;
                } else {
                    TimeLength = timeTwo;
                    TimeEnd = TimeBegin + TimeLength;
                }
            }
        }
        class TimeCapsule
        {
            readonly public string NullSeconds = "0";
            readonly public string NullValue = "00:00:00";

            /// <summary>
            /// don't write directly to this value.  Use HmsfString.
            /// </summary>
            public TimeEx HMSF;

            public string HmsfString
            {
                get {
                    return hmsfString;
                } set {
                    hmsfString = value;
                    HMSF = hmsfString;
                }
            } string hmsfString = null;

            public bool HasValue
            {
                get {
                    string temp = null;
                    bool result = true;
                    try {
                        temp = TotalSeconds.ToString();
                    } catch {
                        result = false;
                    }
                    temp = null;
                    return result;
                }
            }

            /// <summary>
            /// Could cause an unhandled exception.
            /// </summary>
            /// <param name="input"></param>
            /// <returns></returns>
            public double TotalSeconds
            {
                get {
                    return HMSF.TotalSeconds;
                }
            }

            public string TotalSecondsString
            {
                get {
                    return HasValue? TotalSeconds.ToString(): NullSeconds;
                }
            }

            public string HmsString
            {
                get {
                    string result = null;
                    TimeStruct? ts = TimeEx.GetStruct(hmsfString);
                    result = ts.HasValue ? ts.Value.GetHMS() : TimeStruct.NullHms;
                    ts = null;
                    return result;
                }
            }

            public TimeStruct TimeStruct
            {
                get {
                    return TimeEx.GetStruct(hmsfString).Value;
                }
            }

            static public void FormatHMS(Label linput, TextBox tinput)
            {
                TimeCapsule time = new TimeCapsule();
                time.HmsfString = tinput.Text;

                linput.Text = time.TotalSecondsString;
                linput.ForeColor = !time.HasValue ? System.Drawing.Color.Red : Label.DefaultForeColor;
                tinput.Text = time.HmsString;

                time = null;
            }

        }
    }
}