# Custom NAudio.dll (NAudio Hg.Mercurial revision:467)

We use a custom build of NAudio for the following reasons:

- [NAudio does not provide a signed version in its NuGet package][forum]. Included in this readme below is the note from [wolf5370] posted `Aug 21, 2012 at 7:29 AM`.
- Two source-archives are available [here][package_loc].
    - NAudio-r467-mod+MEF-9-mod-source.zip — all sources required for building gen.snd libs and apps.
    - NAudio-r467-changed-files.zip — just the files changed in the particular NAudio revision.

[package_loc]: https://drive.google.com/folderview?id=0B3zU6tYfj4ZHbUg0Y0ZOaV9xWm8&usp=sharing
[forum]: http://naudio.codeplex.com/discussions/287348
[wolf5370]: http://www.codeplex.com/site/users/view/wolf5370

## Note from wolf5370

The DLL can be manually signed without rebuilding (necessary with 1.5 as source is missing the Midi library so won't compile currently) - direct from DLL binary...

- Delete reference in project to NAudio.dll
- Start VS2010 Command Prompt
- CD to NAudio.DLL directory
- Create a random SNK keypair file using `SN.EXE -k NAudio.snk`
- Disasemble NAudio.dll to IL using `ildasm /all /out=NAudio.il NAudio.dll`
- Rebuild IL with strong key pair using ILASM `ilasm /dll /key=NAudio.snk NAudio.il`
- Re-add recference to NAudio.dll in my project
- F6 rebuild in VS2010 - all done

Note: You may want to back up NAudio.dll (as an un-signed version first).

## LICENSE: Microsoft Public License (Ms-PL)

Microsoft Public License (Ms-PL)

This license governs use of the accompanying software. If you use the software, you accept this license. If you do not accept the license, do not use the software.

### 1. Definitions

The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning here as under U.S. copyright law.

A "contribution" is the original software, or any additions or changes to the software.

A "contributor" is any person that distributes its contribution under this license.

"Licensed patents" are a contributor's patent claims that read directly on its contribution.

### 2. Grant of Rights

(A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.

(B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.

### 3. Conditions and Limitations

(A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.

(B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your patent license from such contributor to the software ends automatically.

(C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution notices that are present in the software.

(D) If you distribute any portion of the software in source code form, you may do so only under this license by including a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.

(E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.