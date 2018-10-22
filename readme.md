> ¡¡¡THIS SPACE IS VERY MUCH A OBSOLETE WASTE OF TIME!!! –let alone a dependency nightmare.
> 
> anything that was generally useful here has been migrated to  
> https://github.com/tfwio/modest-smf-vstnet (just a shoddy prototype / litmus test)
>
> https://github.com/tfwio/smfio  
> [the MIDI parser shown in here has been worked up to usable status, recently ;)]
> 
> there is plenty of good stuff out there...
>
> VlademarOrn's (csharp VA modeling *— highly recommended*)
> - http://valdemarorn.github.io/Projects.html
> - https://github.com/ValdemarOrn
> - → https://github.com/ValdemarOrn/SharpSoundPlugins
> - https://github.com/ValdemarOrn/CloudSeed
> 
> fluidsynth C# binding [SoundFont (fluidsynth.dll) player]
> - https://github.com/atsushieno/nfluidsynth
> - https://github.com/atsushieno/managed-midi
> - https://github.com/FluidSynth/fluidsynth [dependency for nfluidsynth, though you can just go find binaries]
>
> https://github.com/naudio/NAudio (you know)
> 
> https://github.com/nparkanyi/MiniMIDI and https://github.com/nparkanyi/libmidi  
> "Lightweight MIDI player and editor."  (C/CPP Cross-Platform FLTK + (libmidi) MIDI player/viewer demo in c/c++)
> 
> Laurent de Soras (Productions)  
> http://ldesoras.free.fr/prod.html
> 
> https://github.com/olilarkin/awesome-musicdsp
>
> ...



## general / common csharp sound library

just the meager general sound library – mostly serving as a self-education in general digital audio

a set of a couple parser modules for midi, soundfont, impulse-tracker and other stuff—including a vst-host prototype that can use some work.

### About

The idea of most any parser here is almost strictly for interpretation of a few media formats.  If anything, I was interested in writing a sample-import/export librarian tool, however many of the components were first authored in VB6, and as my understanding evolved, this project migrated first to C/CPP, then DOTNETv1.0—then finally, years later: DOTNETv4.x;
Hence the mess.  Now, it would be nice to write a vst-host in C/CPP when I have time and perhaps rewrite the genericwav thing to WPF.

### Dependencies

* using MSBUILD 5.0, DOTNET Framework v4.0–v4.5
* Targeting x86/Win32, while lead development has taken place on a WOW64 machine running WIN7.
* depends on `cor3.core` and `cor3.forms`, by way of files that are included into this common library.
* [naudio.codeplex.com](http://naudio.codeplex.com) — a custom version is used here, until I adequately resolve differences between the assembly I'm using and the original(s).  The primary reason I'm using a customized binary here is due to the lack of a signed assembly as distributed by naudio's default nuget package.
* See: [vstnet.codeplex.com](http://vstnet.codeplex.com/) for more info on CSharp/VST.
* Please note that any supplied binaries such as `naudio` and `jacobi.vstnet.interop` binaries were generated on a i686 processer, targeting i386 (win32) for cross-compatibility as this is the only machine used for testing thus far.

### History

This mess of code was at a time used a common-library for my little DOTNET VSTHOST (of which there is a gen.snd.vst-common library), which I intended on re-writing to resolve two issues I'm aware of: (1) MTC timing resolution needs either an overhaul (repair) or I just need to find and resolve the bug. (2) VST Effect and Instrument UIs have apparent threading issues that makes a VST Host in DOTNET not so useful as a all-purpose mastering suite solution.

Now I simply want to clean it up and see if perhaps I can use it for a 'sample archive tool / sample editor' or assist in some-such.

(A mental note)
The last thing I can remember when feeding the mindspace that generated the vst-host is that I ended up looking at and wanting to compile a JUCE framework apps, and I became quite sidetracked by researching my next step: writing a host in C.  Please note that I didn't expect to get the VSTHOST [`gen.snd.vstsmfui`](https://github.com/tfwio/gen.snd.vstsmfui) up here so quickly.  I'll be documenting it when I've got time...

----

More notes to come later...

### The MIT License (MIT)

Copyright (C) 2000-2014 github.com/tfwio

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

