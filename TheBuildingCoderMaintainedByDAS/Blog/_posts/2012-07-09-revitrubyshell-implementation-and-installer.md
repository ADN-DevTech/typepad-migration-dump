---
layout: "post"
title: "RevitRubyShell Implementation and Installer"
date: "2012-07-09 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Climbing"
  - "Data Access"
  - "External"
  - "User Interface"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2012/07/revitrubyshell-implementation-and-installer.html "
typepad_basename: "revitrubyshell-implementation-and-installer"
typepad_status: "Publish"
---

<p>I had a very impressive train ride on Friday evening on my way up to meet my friends to climb the 

<a href="http://en.wikipedia.org/wiki/Spitzhorn">Spitzhorn</a> mountain, 

happening to take the 

<a href="http://www.myswitzerland.com/en/montreux-chateau-d-oex-gstaad-panoramic-express.html">
panoramic express</a> up 

from Montreux to Gstaad, completely by chance. 
I was just expecting a normal train ride, but it turned out to be extremely beautiful, the most comfortable way I have yet experienced to get an almost overwhelming impression of Swiss mountain beauty without having to do any hiking, just looking out of the train window.
We made it to the summit on Saturday

(<a href="http://www.facebook.com/media/set/?set=a.4253220338006.2173087.1510729143&type=1">photos</a>):</p>


<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301761644f523970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301761644f523970c image-full" alt="Jeremy on Spitzhorn peak cross" title="Jeremy on Spitzhorn peak cross" src="/assets/image_086b67.jpg" border="0" /></a><br />

</center>

<p>Last week, I provided a

<a href="http://thebuildingcoder.typepad.com/blog/2012/07/meetings-football-and-revitrubyshell.html#2">
short description</a> of

the interactive real-time Revit programming environment 

<a href="https://github.com/hakonhc/RevitRubyShell">
RevitRubyShell</a> provided 

by 

<a href="http://www.hclausen.net">H&aring;kon Clausen</a> and 

mentioned how impressed I was by its minimalistic single-click

<a href="http://www.hclausen.net/RevitRubyShell/setup.exe">
installer</a>.

<p>In the train on the way up to Gstaad, I took a closer look at it.
H&aring;kon points out that RevitRubyShell is heavily inspired by 

<a href="http://code.google.com/p/revitpythonshell">
RevitPythonShell</a> and 

Jimmy Schementi's article about 

<a href="http://blog.jimmy.schementi.com/2009/12/ironruby-rubyconf-2009-part-35.html">
embedding IronRuby</a>.

<p>The entire source code is provided on the 

<a href="http://en.wikipedia.org/wiki/GitHub">
GitHub social coding platform</a>.

<a href="https://github.com">
GitHub</a> is a web-based hosting service for software development projects using the 

<a href="http://git-scm.com">
Git revision control system</a>.

<p>I found the RevitRubyShell source impressive and edifying, sporting a number of aspects well worth looking at in depth for most Revit API programmers, and for that matter most .NET programmers in general as well.
I probably missed lots of other interesting features, but here are some that sprang to eye:

<ul>
<li>Optimal use of numerous bits of .NET functionality to achieve a lot with a minimum of code.
<li>Use of XAML to implement its shell window form, including use of tabs and command buttons.
<li>A couple of useful extension methods for the .NET KeyEventArgs and RichTextBox classes.
<li>One-click installer making use of RevitAddInUtility and its RevitProductUtility and AddInManifestUtility classes.
</ul>

<p>I sat down with the RevitRubyShell console and tried to figure out how to count the number of doors in the Revit basic sample model.
<p>It took me a bit of fiddling and googling, but this seems to do the trick:

<pre>
bic = BuiltInCategory.OST_Doors

doors = FilteredElementCollector.new(doc)
  .OfCategory( bic ).ToElements();

count = doors.select {|d| d.is_a?( FamilyInstance )}.size

=> 29
</pre>

<p>Only three lines of pretty obvious code, even if you know just a little bit of Ruby and the Revit API:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330177432b12de970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330177432b12de970d" alt="Door count in Ruby" title="Door count in Ruby" src="/assets/image_e021b0.jpg" border="0" /></a><br />

</center>

<p>I find this pretty impressive, once again.
The handling is totally intuitive, and everything simply works.
Brilliant.

<p>Many thanks again to H&aring;kon for developing and sharing this!
