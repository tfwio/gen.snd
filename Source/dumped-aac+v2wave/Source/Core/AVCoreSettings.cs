using System;
using System.IO;
using System.Windows.Forms;

namespace AvUtil.Core
{

	public class AVCoreSettings : AVCoreResources
    {
        /// SPLITTER VIEW --- Main Input-File
        public TextBox tInput { get; set; }
        /// SPLITTER VIEW --- Main Output-File
        public TextBox tOutput { get; set; }

        // SPLITTER VIEW --- Input ExtendedFileInfo
        public ExtendedFileInfo FileInput
        {
            get {
                return fi;
            } set {
                fi = value;
                OnGotFileInput(EventArgs.Empty);
            }
        } ExtendedFileInfo fi;
        
        // SPLITTER VIEW --- RESPOND TO INPUT file changes
        public event EventHandler GotFileInput;
        
        // SPLITTER VIEW --- RESPOND TO INPUT file change event
        protected virtual void OnGotFileInput(EventArgs e) { if (GotFileInput != null) GotFileInput(this, e); }

        // SPLITTER VIEW --- RESPOND TO OUTPUT ExtendedFileInfo
        public ExtendedFileInfo FileOutput
        {
            get {
                return fo;
            } set {
                fo = value;
                OnGotFileInput(EventArgs.Empty);
            }
        } ExtendedFileInfo fo;

        // SPLITTER VIEW --- RESPOND TO OUTPUT Event Part
        public event EventHandler GotFileOutput;

        // RESPOND TO OUTPUT Event Method
        protected virtual void OnGotFileOutput(EventArgs e)
        {
            if (GotFileOutput != null) {
                GotFileOutput(this, e);
            }
        }

        internal bool HasInputFile { get { return fi != null && fi.Exists; } }
        
        /// <summary>Currently running application</summary>
        static public readonly ExtendedFileInfo apppath = new ExtendedFileInfo( Application.ExecutablePath );

        /// <summary>FFMpeg application</summary>
        static public readonly ExtendedFileInfo exepath = new ExtendedFileInfo( Path.Combine(apppath.Directory.FullName,Strings.ffmpeg_app) );

        /// <summary>
        /// Gets the FileInput or fake FileName if no input file was provided.
        /// </summary>
        public string TestInputFileString
        {
            get {
                return HasInputFile ? FileInput.info.FullName : "X:/sample-input-file.mp3";
            }
        }

        /// <summary>
        /// Returns a generated output file name or a fake output file name if no input was provided.
        /// </summary>
        /// <remarks>
        /// The current 'split' {time:location}-{time:length} is appended to the end of the file-name.
        /// </remarks>
        public string FullOutputFileName
        {
            get {
                string foo = HasInputFile ? FileInput.info.FullName : "X:/sample-output-file.mp3";
                string ext = HasInputFile ? FileInput.Extension : ".mp3";
                return foo.Replace(ext, string.Format(" {0}-{1}{2}", TimeBegin.TotalSeconds, TimeEnd.TotalSeconds, ext))
                       ;
            }
        }

        internal TimeEx TimeBegin { get; set; }
        internal TimeEx TimeEnd { get; set; }
        internal TimeEx TimeLength { get { return TimeEnd - TimeBegin; } }

        [System.ComponentModel.DefaultValue(true)]
        internal bool AutoCalculateLength { get; set; }
    }
}
