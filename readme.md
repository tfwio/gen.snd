## general / common sound library

just a meager, preemp general sound library – mostly serving as a self-education in general digital audio

a set of a couple parser modules for midi, soundfont, impulse-tracker and other stuff.

### About

The idea of most any parser here is almost strictly for interpretation of a few media formats.  If anything, I was interested in writing a sample-import/export librarian tool, however many of the components were first authored in VB6, and as my understanding evolved, this project migrated first to C/CPP, then DOTNETv1.0—then finally, years later: DOTNETv4.x;
Hence the mess.

### Dependencies

* depends on `cor3.core` and `cor3.forms`, by way of files that are included into this common library.
* A custom version of NAudio is used here, until I adequately resolve differences between the assembly I'm using and the original(s).  The primary reason I'm using a customized binary here is due to the lack of a signed assembly as distributed by naudio's default nuget package.

### History

This mess of code was at a time used a common-library for my little DOTNET VSTHOST (of which there is a gen.snd.vst-common library), which I intended on re-writing to resolve two issues I'm aware of: (1) MTC timing resolution needs either an overhaul (repair) or I just need to find and resolve the bug. (2) VST Effect and Instrument UIs have apparent threading issues that makes a VST Host in DOTNET not so useful as a all-purpose mastering suite solution.  Sony (Sonic-Foundary) ACID is a good example of a VST-HOST written predominantly in C and merged with DOTNET where most of the windowing (I can only assume) was done in COM/ATL.  In other words, this library generally became abandoned in CSharp when I realized that I wanted to generally write the application in C.

Now I simply want to clean it up and see if perhaps I can use it for a 'sample archive tool / sample editor' or assist in some-such.

See: [vstnet.codeplex.com](http://vstnet.codeplex.com/) for more info on CSharp/VST.

----

More notes to come later...
