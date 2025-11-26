---
layout: "post"
title: "Revit 2014 SDK and Visual Debugging Tools"
date: "2013-05-02 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2014"
  - "Debugging"
  - "DWF"
  - "gbXML"
  - "Geometry"
  - "Utilities"
  - "View"
original_url: "https://thebuildingcoder.typepad.com/blog/2013/05/revit-2014-sdk-and-visual-debugging-tools.html "
typepad_basename: "revit-2014-sdk-and-visual-debugging-tools"
typepad_status: "Publish"
---

<p>Two hot topics for today, especially the first:</p>

<ul>
<li><a href="#2">Updated Revit 2014 SDK</a></li>
<li><a href="#3">Visual debugging tools</a></li>
</ul>

<a name="2"></a>

<h4>Updated Revit 2014 SDK</h4>

<p>An updated Revit 2014 SDK is live on the ADN Open

<a href="http://www.autodesk.com/developrevit">Revit developer page</a> now:</p>

<center>
<a href="http://www.autodesk.com/developrevit" style="text-align:center; font-size:large; font-weight:bold; color:orange; text-decoration:blink;">www.autodesk.com/developrevit</a>
</center>

<p>This version includes RevitLookup and the AddInManager!</p>

<a name="3"></a>

<h4>Visual Debugging Tools</h4>

<p>Rudolf Honke of

<a href="http://www.acadgraph.de">
Mensch und Maschine acadGraph GmbH</a> discovered

a glitch in the definition of the room solids in one of the standard sample RVT project files.</p>

<p><strong>Rudi says:</strong> When testing VRML exporter with Revit 2014, I noticed that the brand new rac_sample_project.rvt has some inconsistencies.

<p>I export the ClosedShells of the rooms into VRML format to display then in my browser.
On the left side you see overlapping room volumes from 'Hall' and 'Entry Hall':</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833019101b94949970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833019101b94949970c" alt="Overlapping room volumes" title="Overlapping room volumes" src="/assets/image_d23691.jpg" /></a><br />

</center>

<p>'Kitchen and dining' has an upper edge that needs to be cut off:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017eeac0df08970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017eeac0df08970d" alt="Upper edge sticks out" title="Upper edge sticks out" src="/assets/image_60eccb.jpg" /></a><br />

</center>

<p>'Hall' has two ears protruding into the sky:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301901bc3680f970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301901bc3680f970b" alt="Hall with ears" title="Hall with ears" src="/assets/image_056977.jpg" /></a><br />

</center>

<p>I noticed this when debugging the funny results my application was producing.</p>

<p>Since it is not the code, I concluded it must be the input that was corrupted…</p>

<p>Without a visual tool like this, it is quite hard to evaluate what's wrong.</p>

<p><strong>Jeremy adds:</strong> You can also observe this using the Export &gt; gbXML command:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833019101b94bed970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833019101b94bed970c image-full" alt="Overlapping room volumes" title="Overlapping room volumes" src="/assets/image_2e0e5f.jpg" border="0" /></a><br />

</center>

<p>'Kitchen and dining' has an upper edge that needs to be cut off:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017eeac0e14b970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017eeac0e14b970d image-full" alt="Upper edge sticks out" title="Upper edge sticks out" src="/assets/image_051b2d.jpg" border="0" /></a><br />

</center>

<p>'Hall' has two ears protruding into the sky:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833019101b94d60970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833019101b94d60970c image-full" alt="Hall with ears" title="Hall with ears" src="/assets/image_413dcb.jpg" border="0" /></a><br />

</center>

<p>Furthermore, the Revit 2014 program folder contains a utility named <b>gbXML2dwfx.exe</b>.

<p>If you drag and drop a gbXML file onto this it will create a DWFx file that can be viewed in

<a href="http://usa.autodesk.com/design-review">
Autodesk Design Review</a>,

the free 2D and 3D DWF viewer, which provides more functionality than many VRML viewers.</p>

<p><strong>Response:</strong> Interesting.
GbXML2Dwfx.exe appears to be new in Revit 2014.

<p>Regarding the geometry, it is important to note that you cannot rely on geometrical integrity of your Revit Elements.

<p>I faced this problem previously, not only with rooms but also with walls.

<p>Every cut-off and every Boolean operation makes the geometry more complex, and rounding errors may occur.

<p>This totally confirms the GiGo principle of

<a href="http://en.wikipedia.org/wiki/Garbage_in,_garbage_out">
Garbage in, garbage out</a>,

slightly less helpful than

<a href="http://en.wikipedia.org/wiki/FIFO">
FiFo</a>...
