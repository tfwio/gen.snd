Dumped aac+v2wave-NAudio-1.7.1.17
=======================================

This is a old app framework that was stripped to prototype something that I could use to
collect sample-data from seemingly arbitrary input using NAudio's MediaFoundation implementation.

- You can download this from google-drive
    - [here](https://drive.google.com/file/d/0B3zU6tYfj4ZHYWZmUUpiVC1jdms/view?usp=sharing) (newer source-distro, NAudio sources are included)
    - [here](https://drive.google.com/file/d/0B3zU6tYfj4ZHeTNvNEROS0VReGM/edit?usp=sharing) (without NAudio dependencies)

Demonstrated
---------------------------------------

- Demonstrates sample aggrigation---alluding to PCM data-dumping --via `source/core/NAudioAggrigationPlayer.cs` being put to use in the little player control.
- Play shoutcast AAC+ audio (dumps: see bullet below)
- Using the semantics, `LoadURL` (or something like that) to play-back
  live shoutcast mp3 and aac streams provided you select the appropriate drop-down
  sample-resolution setting.
    - If you intend to use IMediaFoundation or an alternative method for obtaining Shoutcast streams, you'll need to check [this](https://gist.github.com/tfwio/7175420) out---which enables security settings allowing 'unsafe-urls' into your app-domain.

BUILDING
---------------------------------------

* Download NAudio source-code for version 1.7.1.17.
  As of (re-) writing this, 1.7.1.17 is the most recent version of NAudio.
* Open the CSharp-Project file [dumped-aac+v2wave-NAudio-1.7.1.17.csproj](https://github.com/tfwio/gen.snd/blob/master/Source/dumped-aac%2Bv2wave/dumped-aac%2Bv2wave-NAudio-1.7.1.17.csproj#L24) and change the Property->NAudioPath to point to NAudio's top-level directory.
  This directory contains the NAudio.dll project `.\naudio\naudio*.csproj`.
  if the files don't become available/working in your IDE, apparently you did something wrong.
* build via the IDE
