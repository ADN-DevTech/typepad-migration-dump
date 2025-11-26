---
layout: "post"
title: "Lunar Eclipse and Custom File Properties"
date: "2015-09-29 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Data Access"
  - "External"
  - "Photo"
  - "Python"
  - "Utilities"
  - "Win32"
original_url: "https://thebuildingcoder.typepad.com/blog/2015/09/lunar-eclipse-and-custom-file-properties.html "
typepad_basename: "lunar-eclipse-and-custom-file-properties"
typepad_status: "Publish"
---

<p>Today, let's look at a Windows API question that can be of interest handling Revit documents as well, and mention my night out to watch the lunar eclipse:</p>

<ul>
<li><a href="#2">Lunar Eclipse</a></li>
<li><a href="#3">Custom File Properties</a></li>
</ul>

<h4><a name="2"></a>Lunar Eclipse</h4>

<p>Did you notice that we had
a <a href="https://en.wikipedia.org/wiki/September_2015_lunar_eclipse">total lunar eclipse</a> early
Monday morning?</p>

<p>I spent Sunday night on a hill with some friends celebrating a full moon fire, then slept out beside the embers to catch it beginning around 4:50 in the morning:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb0879bb02970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb0879bb02970d img-responsive" style="width: 300px; " alt="Lunar eclipse beginning" title="Lunar eclipse beginning" src="/assets/image_3fdd41.jpg" /></a><br /></p>

<p></center></p>

<p>Here is a picture a little later, after the event, with the moon whole and intact again, setting over Basle:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d15f8961970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d15f8961970c image-full img-responsive" alt="Moonset over Basle" title="Moonset over Basle" src="/assets/image_cba0a9.jpg" border="0" /></a><br /></p>

<p></center></p>

<p>Unfortunately you cannot see the new Roche tower, recently completed, currently
the <a href="http://www.guiding-architects.net/highest-high-rise-switzerland">highest building in Switzerland</a>.
It is just off the picture, further left, i.e. south.</p>

<p>Nice experience, anyway, this 'red moon', well worth braving the cold.</p>

<h4><a name="3"></a>Custom File Properties</h4>

<p>Steve Goldsmith raised a question
on <a href="https://forums.autodesk.com/t5/revit-api/revit-custom-file-properties/td-p/5533067">Revit custom file properties</a> quite a while back.</p>

<p>It now came up again
on <a href="http://stackoverflow.com/questions/32735888/revit-custom-file-properties">Stack Overflow</a>,
this time with more luck, receiving answers
from <a href="http://stackoverflow.com/users/200443/maxence">Maxence</a>
and <a href="http://stackoverflow.com/users/5064688/axel-minet">Axel Minet</a>:</p>

<p><strong>Question:</strong>
Does anyone know how to access, read or write the Revit file custom properties using VB.NET and the Revit API?</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d15f8918970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d15f8918970c img-responsive" style="width: 390px; " alt="Custom file properties" title="Custom file properties" src="/assets/image_bc237f.jpg" /></a><br /></p>

<p></center></p>

<p>They can be attached to both RVT and RFA files.</p>

<p><strong>Answer:</strong>
These properties have nothing to do with Revit specifically.</p>

<p>They are standard Windows properties associated with OLE structured storage,
<a href="https://en.wikipedia.org/wiki/COM_Structured_Storage">COM Structured Storage</a> and
the <a href="https://en.wikipedia.org/wiki/Compound_File_Binary_Format">Compound File Binary Format</a>.</p>

<p>They are stored
in <a href="https://msdn.microsoft.com/en-us/library/dd942421.aspx">OLE property sets</a>.</p>

<p>You can read them using the Windows API and view them using tools like
the <a href="http://www.mitec.cz/ssv.html">Structured Storage Viewer</a>.</p>

<p>The Revit API does not support them in any way whatsoever.</p>

<p>The Building Coder showed how to access them using other means, e.g., Python and .NET, respectively, in the discussions
on <a href="http://thebuildingcoder.typepad.com/blog/2008/10/rvt-file-version.html">RVT File Version</a>
and <a href="http://thebuildingcoder.typepad.com/blog/2010/06/open-revit-ole-storage.html">Open Revit OLE Storage</a>.</p>

<p>Rod Howarth published a more extensive example
on <a href="http://blog.rodhowarth.com/2008/06/how-to-set-custom-attributes-file.html">how to set custom file properties and attributes in C# .NET</a>.</p>
