---
layout: "post"
title: "Revit Add-in File Load Exception"
date: "2011-10-31 06:00:00"
author: "Jeremy Tammik"
categories:
  - "External"
  - "Installation"
  - "Plugin"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/10/revit-add-in-file-load-exception.html "
typepad_basename: "revit-add-in-file-load-exception"
typepad_status: "Publish"
---

<p>Back from my vacation in Andalusia, and now moving full speed ahead towards 

<a href="http://thebuildingcoder.typepad.com/blog/2011/09/revit-and-aec-api-classes-at-autodesk-university.html">
Autodesk University</a>,

my lecture and hands-on lab on the 

<a href="http://thebuildingcoder.typepad.com/blog/2011/09/revit-and-aec-api-classes-at-autodesk-university.html">
Revit extensible storage API</a> and all the other exciting goodies there.

<p>Meanwhile, here is one little issue that immediately arose with the 

<a href="http://thebuildingcoder.typepad.com/blog/2011/10/string-search-adn-plugin-of-the-month.html">
Revit String Search</a> utility

published last week:

<p>Probably the most common problem that people keep running into with add-ins on Windows Vista, both in Revit and other environments, is the need to <b>unblock the zip file</b>.

<p>If you are impatient, the 

<a href="http://labs.blogs.com/its_alive_in_the_lab/2011/05/unblock-net.html">
solution is easy and available</a> with no need to read any further.

<p>If you are interested in other aspects, here goes with another take on this:

<p>Zach Kron had an issue on some non-XP machines trying to run Daren Thomas' 

<a href="http://thebuildingcoder.typepad.com/blog/2011/07/python-shell-in-revit-and-vasari.html">
RevitPythonShell</a>.

<p>It was not installing properly and displaying the following message:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833015436873728970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833015436873728970c" alt="FileLoadException running RevitPythonTool" title="FileLoadException running RevitPythonTool" src="/assets/image_07385f.jpg" border="0" /></a><br />

</center>

<p>The error message says:

<p style="color:darkblue">Revit cannot run the external application "RevitPythonShell".
Contact the provider for assistance. 
Information they provided to Revit about their identity: asdf.

<p style="color:darkblue">System.IO.FileLoadException

<p style="color:darkblue">Could not load file or assembly 
'file:///C:\addins\Daren Thomas\RevitPythonShell\RevitPythonShell.dll'
or one of its dependencies. 
Operation is not supported (Exception from HRESULT: 0X80131515)

<p>Zach figured out the solution, which has already been documented by Gregory Mertens of

<a href="http://www.mertens3d.com">
mertens3d.com</a> in

the 

<a href="http://mertens3d.com/tools/revit/2012/hatch22-2012/hatch22-2012-download-install.php">
hatch22-2012 installation guide</a>,

who in turn picked it up from joseguia in this 

<a href="http://revitforum.org/showthread.php/1793-RFO-Ribbon-Add-In-Downloads">
revitforum.org thread</a>:

<p>The issue is due to the security option.
You can right click on RevitPythonShell.dll in Win 7 and select Properties &gt; General to obtain access to the Unblock button for setting it:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833015392b3d179970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833015392b3d179970b" alt="Win 7 security option" title="Win 7 security option" src="/assets/image_5afe99.jpg" border="0" /></a><br />

</center>

<p>Many thanks to Zach for pointing this out, and to Gregory for running into, solving, and providing the problem solution in the first place!
Hopefully this will be a helpful hint for anyone running into a similar issue installing some other Revit add-in.

<p>We later discovered that 

<a href="http://through-the-interface.typepad.com/through_the_interface">
Kean Walmsley</a> also 

provided a comprehensive explanation of this issue to 

<a href="http://labs.blogs.com/its_alive_in_the_lab/2011/05/unblock-net.html">
unblock ZIP files before installing Plugins of the Month</a> on 

Autodesk Labs.
