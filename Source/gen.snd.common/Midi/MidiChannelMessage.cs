/*
 * Date: 11/12/2005 * Time: 4:19 PM
 */
using System;
using System.Collections.Generic;

namespace DspAudio.Midi
{
	public class MidiChannelMessage : MidiMessage
	{
		public MidiChannelMessage(ulong delta, int message, params byte[] data) : base(MidiMsgType.Channel,delta,message,data)
		{
		}
	}
}
