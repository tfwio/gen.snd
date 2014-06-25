using System;
using System.IO;
using System.Windows.Forms;

namespace AvUtil.Core
{
	public class AVCoreResources
	{
	    	#region File Formats
	        public const string Av_Audio_MPEG4      = "MPEG Layer 4 Audio|*.m4a;";
	        public const string Av_Audio_AAC        = "Advanced Audio Coding|*.aac;";
	        public const string Av_Audio_MP3        = "MP3 Audio|*.mp3;";
	        public const string Av_Audio_OGG        = "OGG Vorbis Audio|*.ogg;";
	        public const string Av_Audio_FLAC       = "FLAC Audio|*.flac;";
	        public const string Av_Audio_Quicktime  = "Quicktime Movie|*.mov;";
	        public const string Av_Audio_AVI        = "AVI Movie|*.avi;";
	        public const string Av_Audio_MS_WAVE    = "Microsoft RIFF/WAVE|*.wav;";
	        // public const string Av_Audio_ = ";";
	        public static readonly string[] AvFormatArray = {
	            "All Known Formats",
	            "*.m4a;*.aac;*.mp3;*.ogg;*.flac;*.mpv;*.avi;*.wav;",
	            Av_Audio_MPEG4,
	            Av_Audio_AAC,
	            Av_Audio_MP3,
	            Av_Audio_OGG,
	            Av_Audio_FLAC,
	            Av_Audio_Quicktime,
	            Av_Audio_AVI,
	            Av_Audio_MS_WAVE,
	        };
	        public static readonly string AudioFormats = string.Join("|",AvFormatArray);
	
	    	#endregion
	
	        internal OpenFileDialog ofd = new OpenFileDialog() { Filter=AudioFormats };
	        internal SaveFileDialog sfd = new SaveFileDialog() { Filter=AudioFormats };
	}
}
