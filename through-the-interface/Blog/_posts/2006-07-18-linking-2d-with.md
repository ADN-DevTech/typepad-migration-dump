---
layout: "post"
title: "Linking 2D and 3D with the DWF Viewer 7 API"
date: "2006-07-18 14:29:23"
author: "Kean Walmsley"
categories:
  - "DWF"
original_url: "https://www.keanw.com/2006/07/linking_2d_with.html "
typepad_basename: "linking_2d_with"
typepad_status: "Publish"
---

<p>I haven't written about the API capabilities of the DWF Viewer, as yet, but this is a technology I really enjoy working with. The latest release (version 7), especially, has some really cool API capabilities: while you've been able to respond to selection events in 2D views for some time, it's now also possible to manipulate 3D views, selecting which objects should be displayed.</p>

<p>If you throw in the capabilities of AutoCAD's DWF Metadata API (which allows you to add in custom metadata into both 2D and 3D DWF sheets, as they're being published) you really get some interesting possibilities for applications working on &quot;downstream&quot; data.</p>

<p>A quick aside - if you're thinking of embedding the DWF Viewer in your application, be sure to check out the <a href="http://images.autodesk.com/adsk/files/Autodesk_Design_Review_2007_and_DWF_Viewer_7.0_API_Reference.zip">API documentation</a>.</p>

<p>So here's what I've been playing around with - primarily intended to be part of the Component Technologies webcast material I mentioned in a post a few weeks ago.</p>

<p>The idea is that during the publishing of your model from AutoCAD, you add some metadata to objects (in this case I'm using 3D solids) as they get published both into 2D and 3D sheets. The data being published in this sample is simple enough - the handle of the object and the material applied to it. Having this metadata in the DWF will allow us to hook into selection events in the 2D view and create a connection between what's selected in 2D and displayed in 3D.</p>

<p>Next we create a web app in HTML that embeds the viewer twice - once to display the 2D sheet, which is our &quot;main&quot; view, and a second time to show a small window in the corner, which will show a 3D view of the model (or a subset of the model).</p>

<p>And here's where it gets cool: you can define JavaScript functions that respond to events in the 2D view, and dynamically manipulate the 3D view as you hover around in the 2D view. Whichever object you're hovering over in the 2D view gets isolated and displayed in 3D. You can move across and orbit on the 3D view before moving back to hover around the model, looking at different objects.</p>

<p>Here's a quick screenshot of the app as it stands right now:</p>

<p><a onclick="window.open(this.href, '_blank', 'width=800,height=614,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/materials_dashboard.png"><img title="Materials_dashboard" height="76" alt="Materials_dashboard" src="/assets/materials_dashboard.png" width="100" border="0" style="FLOAT: left; MARGIN: 0px 5px 5px 0px" /></a> </p><br /><br /><br /><br /><p>The code is certainly still a &quot;work-in-progress&quot;, there are still some quirks I'm still trying to iron out (it even occasionally freezes my machine - although I haven't yet determined whether that is due to this app or something else). All this to say - please save anything you're working on before launching the app, if indeed you feel you must take a look... :-)</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/files/dwf_materials_demo.zip">Download dwf_materials_demo.zip</a> </p>
