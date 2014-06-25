using System;
using System.IO;
using System.Runtime.InteropServices;

namespace itik
{
	public class ITI
	{
		#region Properties

		private string[] nn = new string[127];
		public string[] NN
		{
			get { return nn; }
			set { nn = value; }
		}
		
		private impi iti_instrument = new impi();
		public impi ITI_INST {
			get { return iti_instrument; }
			set { iti_instrument = value; }
		}
		
		private impx[] iti_sampleheaders;
		public impx[] ITI_SMPH {
			get { return iti_sampleheaders; }
			set { iti_sampleheaders = value; }
		}

		#endregion
		
		#region ITI Structures
		/// <summary>
		/// Envelope Point<br/>
		/// byte : epVal<br/>
		/// short : epPos
		///</summary>
		[ StructLayout( LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi )]
		public struct enveloPoint
		{
			public byte		epVal;
			public short		epPos;
		}
		/// <summary>
		/// keyMap structure to contain key->sample map
		///</summary>
		[ StructLayout( LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi )]
		public struct keyMap
		{
			public byte		epVal;
			public byte		epPos;
		}
		[ StructLayout( LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi )]
		/// <summary>
		/// Envelop Point Information
		/// </summary>
		public struct envL
		{
			public evl			envFlag;
			public byte		envNodeCount;
			public byte		envLoopStart;
			public byte		envLoopEnd;
			public byte		envSusLoopStart;
			public byte		envSusLoopEnd;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst=25)]
			public enveloPoint[] envPoints;
		}
		[ StructLayout( LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi )]
		public struct impi
		{
			[MarshalAs(UnmanagedType.ByValTStr,SizeConst=4)]
			public string						IMPI;
			[MarshalAs(UnmanagedType.ByValTStr,SizeConst=12)]
			public string						impDosFileName;
			public byte							impNull0;
			public NewNoteAction				impNewNoteAction;
			public DuplicateCheckType			impDuplicateCheck;
			public DuplicateCheckAction			impDuplicateCheckAct;
			public short						impFadeOut;
			public byte							impPitchPanSeperation;
			public byte							impPitchPanCenter;
			public byte							impGlobalVol;
			public byte							impDefaultPan;
			public byte							impRandomVolumeVar;
			public byte							impRandomPanVariation;
			public short						impTrackVers;
			public byte							impNumberOfSamples;
			public byte							impNull1;
			[MarshalAs(UnmanagedType.ByValTStr,SizeConst=26)]
			public string						impInstrumentName;
			public byte							impIFC;
			public byte							impIFR;
			public byte							impMCh;
			public byte							impMPr;
			public short						impMidiBank;
			[MarshalAs(UnmanagedType.ByValArray,SizeConst=120)]
			public keyMap[]	impNoteMap;
			public envL							impVolEnvelop;
			public envL							impPanEnvelop;
			public envL							impEFXEnvelop;
			[MarshalAs(UnmanagedType.ByValTStr,SizeConst=7)]
			public string						impPadding;
		}
		
		/// <summary>
		/// sammple
		/// </summary>
		[ StructLayout( LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi )]
		public struct impx
		{
			[MarshalAs(UnmanagedType.ByValTStr,SizeConst=4)]
			public string						IMPS;
			[MarshalAs(UnmanagedType.ByValTStr,SizeConst=12)]
			public string						impsDosFileName;
			public byte							impsNull0;
			public byte							impsGlobalVolume;
			public flg							impsFlag;
			public byte							impsVolume;
			[MarshalAs(UnmanagedType.ByValTStr,SizeConst=26)]
			public string						impsSampleName;
			public cvt							impsCvt;
			public byte							impsDfP;
			public int							impsLength;
			public int							impsLoopBegin;
			public int							impsLoopEnd;
			public int							impsC5Speed;
			public int							impsSusLoopBein;
			public int							impsSusLoopEnd;
			public int							impsSamplePointer;
			public byte							impsVibSpeed;
			public byte							impsVibDepth;
			public byte							impsVibRate;
			public byte							impsVibType;
		}
		
		[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi, Pack=1 )]
		public struct sampleData
		{
			public int		impsFileSize;
			public short	impsDate;
			public short	impsTime;
			public byte		impsFormat; // 89 in length
			public byte[]	impsByteSampleData;
			public short[]	impsShrtSampleData;
			// who fucking knows what kind of data is in here allready?
		}
		#endregion

		#region Enumerations
		[Flags] public enum flg : byte
		{
			IMPS = 1, //sample associated with header.
			PCM16 = 2,  //16 bit, Off = 8 bit.
			S = 4, //stereo, Off = mono. Stereo samples not supported yet
			C = 8, //compressed samples.
			L = 16, //Use loop
			SL = 32, //Use sustain loop
			p = 64, //Ping Pong loop, Off = Forwards loop
			Sp = 128 //Ping Pong Sustain loop, Off = Forwards Sustain loop
		}
		[Flags] public enum cvt : byte
		{
			u = 1,
			moterola_intel = 2,
			delta_pcm = 4,
			delta = 8,
			txWave12bit = 16,
			lrasP = 32,
			res0 = 64,
			res1 = 128
		}
		[Flags]	public enum evl : byte
		{
			nutn = 0,
			on = 1,
			loop = 2,
			sustain = 4
		}
		public enum ramp : byte
		{
			Sine = 0,
			Ramp = 1,
			Squa = 2,
			Rand = 3
		}
		public enum NewNoteAction : byte
		{
			Cut = 0,
			Continue = 1,
			Note_Off = 2,
			Note_Fade = 3
		}
		public enum DuplicateCheckType : byte
		{
			Off = 0,
			Note = 1,
			Sample = 2,
			Instrument = 3
		}
		public enum DuplicateCheckAction : byte
		{
			Cut = 0,
			NoteOff = 1,
			NoteFade = 2
		}
		#endregion
		
		#region itinst default method
		public ITI(string filename)
		{
			impi fil = new impi();
			this.nn = octMac();
			FileStream rop = new FileStream(filename, FileMode.Open, FileAccess.Read);
			BinaryReader bob = new BinaryReader(rop);
			byte[] mal = bob.ReadBytes(Marshal.SizeOf(fil));
			this.iti_instrument = (impi)mread(fil,mal);
			
			impx[] samp = new impx[]{};
			Array.Resize(ref samp,iti_instrument.impNumberOfSamples);
			for (int i = 0; i < iti_instrument.impNumberOfSamples;i++)
			{
				samp[i] = (impx)mread((impx)samp[i],bob.ReadBytes(Marshal.SizeOf(samp[i])));
			} this.ITI_SMPH = samp;
			bob.Close(); rop.Dispose();
			mal = null;
		}
		#endregion
		
		#region Marshal.PtrToStructure
		static private object mread(object reffer, byte[] data)
		{
			IntPtr hrez = Marshal.AllocHGlobal( Marshal.SizeOf(reffer) );
			Marshal.Copy( data, 0, hrez, Marshal.SizeOf(reffer) );
			reffer = Marshal.PtrToStructure( hrez, reffer.GetType());
			Marshal.FreeHGlobal( hrez );
			return reffer;
		}
		#endregion

		#region octave helper
		public string[] octMac()
		{
			string[] ax = new string [ 128 ]; int octave = 0; for (int i = 0; i<127;i+=12)  {
				wroct(ref ax, ref i, ref octave);
			}
			return ax;
		}
		public string[] octMac(int lo, int hi)
		{
			string[] ax = new string [ hi-lo ];
			int octave = 0;
			for (int i = octave; i < hi; i+=12)  {
				wroct(ref ax, ref i, ref octave);
			}
				return ax;
		}
		static public void wroct(ref string[] arx, ref int i, ref int octave)
		{
			if (i<120) {
				arx[i] = "C "+octave; arx[i+1] = "C#"+octave; arx[i+2] = "D "+octave;
				arx[i+3] = "D#"+octave; arx[i+4] = "E "+octave; arx[i+5] = "F "+octave;
				arx[i+6] = "F#"+octave; arx[i+7] = "G "+octave; arx[i+8] = "G#"+octave;
				arx[i+9] = "A "+octave; arx[i+10] = "A#"+octave; arx[i+11] = "B "+octave;
				} else {
				arx[i] = "C "+octave; arx[i+1] = "C#"+octave; arx[i+2] = "D "+octave;
				arx[i+3] = "D#"+octave; arx[i+4] = "E "+octave;arx[i+5] = "F "+octave;
				arx[i+6] = "F#"+octave; arx[i+7] = "G "+octave;
				} octave++;
		}
		#endregion
	}
}
