---
layout: "post"
title: "Room Boundary Location"
date: "2009-09-11 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Geometry"
  - "Settings"
  - "VB"
original_url: "https://thebuildingcoder.typepad.com/blog/2009/09/room-boundary-location.html "
typepad_basename: "room-boundary-location"
typepad_status: "Publish"
---

<p>We already had a look at

<a href="http://thebuildingcoder.typepad.com/blog/2009/06/volume-computation-enable.html">
enabling the room volume computation</a>

in C#.
Here is a closely related question that also prompted me to implement something similar in VB for a change.

<p><strong>Question:</strong>
Could you please provide some VB.NET code showing how to set the Autodesk.Revit.Rooms.BoundaryLocationType in the document, for instance to 'WallCenter'?

<p><strong>Answer:</strong>
The boundary location type is applied when calculating the room volume.
These settings are defined by the document settings volume calculation options.
The post mentioned above discusses how to access and modify these settings.

<p>I implemented a VB.NET sample application RoomBoundaryLocation for you to demonstrate this in VB as well.
Here is the mainline code of its Execute method:

<pre class="code">
<span class="blue">Public</span> <span class="blue">Function</span> Execute( _
&nbsp; <span class="blue">ByVal</span> commandData <span class="blue">As</span> ExternalCommandData, _
&nbsp; <span class="blue">ByRef</span> message <span class="blue">As</span> <span class="blue">String</span>, _
&nbsp; <span class="blue">ByVal</span> elements <span class="blue">As</span> ElementSet) _
&nbsp; <span class="blue">As</span> CmdResult _
&nbsp; <span class="blue">Implements</span> IExternalCommand.Execute

&nbsp; <span class="blue">Dim</span> app <span class="blue">As</span> Application = commandData.Application
&nbsp; <span class="blue">Dim</span> doc <span class="blue">As</span> Document = app.ActiveDocument

&nbsp; <span class="blue">Dim</span> opt <span class="blue">As</span> VolumeCalculationOptions _
&nbsp; &nbsp; = doc.Settings.VolumeCalculationSetting _
&nbsp; &nbsp; &nbsp; .VolumeCalculationOptions

&nbsp; opt.VolumeComputationEnable = <span class="blue">True</span>

&nbsp; opt.RoomAreaBoundaryLocation _
&nbsp; &nbsp; = Rooms.BoundaryLocationType.WallCenter

&nbsp; doc.Settings.VolumeCalculationSetting _
&nbsp; &nbsp; .VolumeCalculationOptions = opt

&nbsp; <span class="blue">Dim</span> volumes <span class="blue">As</span> List(<span class="blue">Of</span> <span class="blue">String</span>) = <span class="blue">New</span> List(<span class="blue">Of</span> <span class="blue">String</span>)
&nbsp; <span class="blue">Dim</span> els <span class="blue">As</span> ElementSet = doc.Selection.Elements

&nbsp; <span class="blue">Dim</span> e <span class="blue">As</span> Autodesk.Revit.Element
&nbsp; <span class="blue">For</span> <span class="blue">Each</span> e <span class="blue">In</span> els
&nbsp; &nbsp; <span class="blue">If</span> <span class="blue">TypeOf</span> e <span class="blue">Is</span> Room <span class="blue">Then</span>
&nbsp; &nbsp; &nbsp; <span class="blue">Dim</span> room <span class="blue">As</span> Room = <span class="blue">CType</span>(e, Room)
&nbsp; &nbsp; &nbsp; volumes.Add(room.Volume.ToString(<span class="maroon">&quot;0.##&quot;</span>))
&nbsp; &nbsp; &nbsp; <span class="green">'Else</span>
&nbsp; &nbsp; &nbsp; <span class="green">'&nbsp; volumes.Add(&quot;Not a room&quot;)</span>
&nbsp; &nbsp; <span class="blue">End</span> <span class="blue">If</span>
&nbsp; <span class="blue">Next</span>

&nbsp; <span class="blue">If</span> 0 = volumes.Count <span class="blue">Then</span>
&nbsp; &nbsp; message = <span class="maroon">&quot;Please select some rooms.&quot;</span>
&nbsp; <span class="blue">Else</span>
&nbsp; &nbsp; <span class="blue">Dim</span> s <span class="blue">As</span> <span class="blue">String</span> = _
&nbsp; &nbsp; &nbsp; +<span class="maroon">&quot;Selected room volumes in cubic feet: &quot;</span> _
&nbsp; &nbsp; &nbsp; + <span class="blue">String</span>.Join(<span class="maroon">&quot;, &quot;</span>, volumes.ToArray()) _
&nbsp; &nbsp; &nbsp; + <span class="maroon">&quot;.&quot;</span>
&nbsp; &nbsp; MsgBox(s)
&nbsp; <span class="blue">End</span> <span class="blue">If</span>

&nbsp; <span class="blue">Return</span> CmdResult.Failed
<span class="blue">End</span> <span class="blue">Function</span>
</pre>

<p>Here are some sample rooms and spaces selected in the Revit user interface:

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330120a5b2d1e0970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="at-xid-6a00e553e1689788330120a5b2d1e0970c" alt="Selected rooms and spaces" title="Selected rooms and spaces" src="/assets/image_e80208.jpg" border="0"  /></a>

</center>

<p>This is the resulting dialogue displayed by running the command:

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330120a5b2d10f970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="at-xid-6a00e553e1689788330120a5b2d10f970c" alt="Selected room volumes" title="Selected room volumes" src="/assets/image_47733d.jpg" border="0"  /></a>

</center>

<p>Here is the complete Visual Studio solution

<span class="at-xid-6a00e553e1689788330120a5b2cfca970c"><a href="http://thebuildingcoder.typepad.com/files/roomboundarylocation.zip">RoomBoundaryLocation</a></span>

implementing the new command.</p>
