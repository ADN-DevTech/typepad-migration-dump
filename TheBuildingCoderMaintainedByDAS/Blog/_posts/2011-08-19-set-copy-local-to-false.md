---
layout: "post"
title: "Set Copy Local to False"
date: "2011-08-19 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Debugging"
  - "Getting Started"
  - "Migration"
  - "Settings"
  - "VB"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/08/set-copy-local-to-false.html "
typepad_basename: "set-copy-local-to-false"
typepad_status: "Publish"
---

<p>I cannot count the number of times I have pointed out that the Copy Local flag should be set to false on the Revit API assembly references.

Here are some of the numerous previous examples and explanations of this:

<!--

006_sdk_samples_solution.htm:'Copy Local' flag maintained by
006_sdk_samples_solution.htm:and at the same time the existi
008_debugging.htm:<li>Do not forget to set the 'Copy Local'
008_debugging.htm:<li>Do not forget to set the 'Copy Local'
024_application_events_vb.htm:<li>Set its 'Copy Local' flag
159_export_instance_to_gbxml.htm:<li><a href="#2">Set Copy L
159_export_instance_to_gbxml.htm:<h4>Set Copy Local to False
159_export_instance_to_gbxml.htm:<p>Always ensure that the C
189_porting_to_vb.htm:<li>Click the "Show All Files" button
268_custom_ribbon_tab.htm:As always, we need to remember to
310_reload_addin.htm:<p>By the way, in this sample, the Revi
360_p2c.htm:<p>Almost all Revit add-ins will need to referen
415_devtv_addin_templates.htm:<li>References to Revit assemb
443_idling_selection_watcher.htm:<li>You need to set the 'Co
459_cpp_addin.htm:<p>Don't forget to set the Copy Local flag

-->

<ul>
<li>A <a href="http://thebuildingcoder.typepad.com/blog/2010/10/c-revit-add-in.html">C++ Revit add-in</a>
<li><a href="http://thebuildingcoder.typepad.com/blog/2010/09/selection-watcher-using-idling-event.html">Selection watcher using Idling event</a>
<li>The <a href="http://thebuildingcoder.typepad.com/blog/2010/07/devtv-addin-templates.html">DevTV add-in templates</a>
<li><a href="http://thebuildingcoder.typepad.com/blog/2010/05/pipe-to-conduit-converter.html#1">Pipe to conduit converter</a>
<li><a href="http://thebuildingcoder.typepad.com/blog/2009/12/custom-ribbon-tab.html">Custom ribbon tab</a>
<li><a href="http://thebuildingcoder.typepad.com/blog/2009/07/porting-from-c-to-vbnet.html">Porting from C# to VB.NET</a>
<li><a href="http://thebuildingcoder.typepad.com/blog/2009/06/export-family-instance-to-gbxml.html#2">Exporting a family instance to gbXML</a>
<li><a href="http://thebuildingcoder.typepad.com/blog/2008/10/application-events-in-vb.html">Application events in VB</a>
<li><a href="http://thebuildingcoder.typepad.com/blog/2008/09/debugging-a-rev.html">Debugging a Revit add-in</a>
<li>The <a href="http://thebuildingcoder.typepad.com/blog/2008/09/the-sdk-samples.html">SDK samples solution</a>
</ul>

<p>This flag appears in the properties of the Revit API references.
It is also displayed in the list of the references themselves in a VB project.
Here they are set to True, which is what we want to <b>avoid</b>:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330154349f7a7c970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330154349f7a7c970c image-full" alt="Copy Local flag in a VB project" title="Copy Local flag in a VB project" src="/assets/image_64262a.jpg" border="0" /></a> <br />

</center>

<p>In a C# project, you can right click on the Revit API references in the Visual Studio solution explorer and selecting its properties in the context menu to see and modify its current setting:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833014e8abf5fb3970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833014e8abf5fb3970d" alt="Copy Local flag in a C# project" title="Copy Local flag in a C# project" src="/assets/image_e4c7b8.jpg" border="0" /></a> <br />

</center>

<p>You can toggle this property with a double click.

<p>If this flag is set to True, Visual Studio will create local copies of RevitAPI.dll and RevitAPIUI.dll when compiling the plug-in and use these copies when loading them. 
This confuses the debugger and Revit when running the add-in, as well as unnecessarily polluting your hard disk with copies of this multi-MB file.

<p>To avoid having to reset this property when modifying an existing reference, for instance when migrating to a new version of the Revit API, do not delete the existing reference. 
Instead, simply add the new reference to the current assembly to overwrite the old one.
The old, existing data will be updated, the new path will be stored, and the existing 'Copy Local' setting will be preserved.

<p>By the way, the same applies to the 

<a href="http://through-the-interface.typepad.com/through_the_interface/2006/09/initialization_.html">
AutoCAD.NET assemblies</a>.

<p>At least this post gives me a completely comprehensive description to refer to next time I have to point this out...
