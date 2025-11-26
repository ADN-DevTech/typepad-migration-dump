---
layout: "post"
title: "RVT File Version"
date: "2008-10-13 17:00:00"
author: "Jeremy Tammik"
categories:
  - "External"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2008/10/rvt-file-version.html "
typepad_basename: "rvt-file-version"
typepad_status: "Publish"
---

<P>And now for something completely different.</P>
<P>The Revit API does not provide any method for determining the version of an RVT file on disk. And even if it did, it would often be useful to be able to determine the file version from outside of Revit, i.e. without having access to the Revit API. Happily, this is easily possible and also gives us a chance to look at a completely different programming environment, for a welcome change.</P>
<P>To see how easy it is to discover this information in the binary RVT file, you can simply try opening an RVT file in the Visual Studio editor. It will open it in binary mode, so it will be displayed in hexadecimal. Scrolling through the gibberish, you will occasionally see some intelligible strings. Since they are in Unicode, there will be intervening null bytes between the individual ASCII characters. Sooner or later, one of these will say 'Build:', followed by the build number.</P>
<P>Actually, the RVT file is an OLE document. It contains various separate sections, and these can be viewed using the Windows NT Structured Storage Docfile Viewer dfview.exe. It is an executable from Windows 3.50.1, and was also shipped with Visual Studio 6.0 (<A href="http://support.microsoft.com/kb/273992">^</A>). By the way, looking at a RVT file in dfview.exe also shows that the bitmap is buried somewhere in there, in a branch named RevitPreview4.0 ... but that is a different topic. Here is an example of dfview displaying a Revit document named wall.rvt:</P><A style="DISPLAY: inline" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330105356d1397970c-pi"><img  class="at-xid-6a00e553e1689788330105356d1397970c " title="Dfview displaying a Revit Structured Storage Document wall.rvt" alt="Dfview displaying a Revit Structured Storage Document wall.rvt" src="/assets/image_c8d41f.jpg" border=0></A> 
<P>Enough said. Almost. Another note I would like to make is that you as a professional programmer certainly must be aware of the fact that for little free-standing programming tasks like extracting this Unicode build string from a binary file, and actually for large programming tasks as well, scripting languages are vastly more efficient than compiled ones. For a discussion of this, you can refer to this <A href="http://home.pacbell.net/ouster/scripting.html">paper</A> by John Ousterhout, the father of the Tcl/Tk scripting and GUI language, which discusses the impressive effort ratio ranging between 3 and 60 comparing both small and large programming tasks implemented in a compiled language such as C, C++ or Java versus scripted versions in Tcl and Perl. Regardless of whether the compiled or scripted version was written first, the scripted version comes out on top. On the other hand, with very strong support for Intellisense and on-the-fly recompilation during the debugging session for both C++ and .NET, the Visual Studio development environment is improving as well.</P>
<P>A handy scripting language that I use a lot nowadays is <A href="http://www.python.org/">Python</A>. I did use <A href="http://www.perl.org/">Perl</A> for a year or two, but since that does has tendency towards being a <A href="http://en.wikipedia.org/wiki/Write-only_language">write-only language</A>, I have gradually tended towards Python lately. Here is the source code for a Python script that reads the entire RVT file into memory, searches for the Unicode build string, and prints the result:</P><PRE>
"""
rvtver.py - read Revit version from *.rvt file

Copyright (C) 2008 by Jeremy Tammik, Autodesk Inc.

2008-09-02 initial version
2008-10-01 unicode handling added by ralf huvendiek
"""

import os, sys

if 1 == len(sys.argv):
 print 'usage: a.py rvtfile'
 sys.exit()

filename = sys.argv[1]

if not os.path.exists( filename ):
 print "File '%s' does not exist." % filename
 sys.exit()

f = open( filename, 'rb' )
data = f.read()
f.close()

s = 'Build'.encode( 'UTF-16-LE' )
i = data.find( s )

if i &gt; 0:
 build_string = data[ i: i+40 ]
 build_string = build_string.decode( 'UTF-16-LE' )
 print "Build string found in '%s' at offset %s: %s" % (filename, i, build_string)
else:
 print 'Build string not found.'
</PRE>
<P>Many thanks to Ralf Huvendiek for checking the code and adding the Unicode conversion calls. As Ralf pointed out and many of you are certainly aware, this little script can quickly become a pretty monstrous memory hog, since f.read() will attempt to read in the entire RVT file into memory in one go. This just goes to underline the fact that this is not production code, just a proof of concept. Production code or even a useful generic little utility would read the file one chunk at a time and terminate once the search string is found. In my tests so far, the string occurred pretty early on, so it should terminate pretty quickly.</P>
