---
layout: "post"
title: "Namespaces"
date: "2009-10-15 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Getting Started"
original_url: "https://thebuildingcoder.typepad.com/blog/2009/10/namespaces.html "
typepad_basename: "namespaces"
typepad_status: "Publish"
---

<p>Here is a 

<a href="http://thebuildingcoder.typepad.com/blog/2009/04/getting-started-with-the-revit-2009-api.html?cid=6a00e553e1689788330120a5e41815970b#comment-6a00e553e1689788330120a5e41815970b">
question</a> by 

<a href="mailto:freelancecadd@gmail.com">
Vince</a> that 

many beginners may encounter, on namespaces and references and setting up a project.
Although it may seem confusing the first time through, it is very simple to handle once you know how.</p>

<p><strong>Question:</strong> Can you point me to material, blog or forum that discusses how to insert Code Regions from the SDK material into existing code? 

<p>I'm learning to code so any basic level information that you can provide me would be a big help. 

<p>I figured out how to do the Hello World tutorial located at the beginning of the Revit 2010 API user Manual, but now I would like to expand on the information by utilizing the additional code regions supplied in the User Manual but I continue to get errors.

<p>I'm sure it's basic in nature but because of my limited experience difficulty is around every corner. 

<p>Again, if you could supply me with a simple tutorial showing how to insert a code region into existing code would be very helpful.

<p><strong>Answer:</strong> I cannot really say anything special about copying source code snippets from one project to another. I just use copy and paste in any old editor, actually. In .NET, you just need to ensure that all required references and using statements are in place. Maybe that is causing the errors you see.

<p>The 'using' statements at the head of each module specify namespaces that can be used without explicitly specifying the namespace each time, so that you can write 'Element' instead of the full class name 'Autodesk.Revit.Element':

<pre class="code">
<span class="blue">using</span> System;
<span class="blue">using</span> System.Collections.Generic;
<span class="blue">using</span> System.Diagnostics;
<span class="blue">using</span> Autodesk.Revit;
<span class="blue">using</span> Autodesk.Revit.Elements;
</pre>

<p>The references provide the definitions of the namespaces, and are actually .NET assemblies, i.e. DLL files that need to exist on you system and on the system executing your plug-in. They are loaded by the .NET framework when your plug-in is loaded:

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330120a5e83438970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330120a5e83438970b" alt="References" title="References" src="/assets/image_c40de6.jpg" border="0"  /></a> <br />

</center>

<p>Adding the required references to a project is demonstrated by the 

<a href="http://thebuildingcoder.typepad.com/blog/2009/04/getting-started-with-the-revit-2009-api.html">
Revit DevTV recording</a>.

<p>The entire Revit API resides in one single assembly, RevitAPI.dll, which includes all the Revit namespaces, so that is simple. Which class resides in which namespace is documented by the Revit API help file RevitAPI.chm, which is included with the Revit SDK:

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330120a63edfa6970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330120a63edfa6970c image-full" alt="Namespaces" title="Namespaces" src="/assets/image_2d0e6d.jpg" border="0"  /></a> <br />

</center>

<p>In general, all you have to do when you copy code from one project to another is ensure that the required references are loaded, and then add appropriate using statements, unless the code you copied uses the full name of every class it references.
