---
layout: "post"
title: "Vector of minimum distance"
date: "2014-01-30 03:22:02"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/01/vector-of-minimum-distance.html "
typepad_basename: "vector-of-minimum-distance"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>There is a utility object in the API called <strong>MeasureTools</strong> that enables you to do the same measuring that is available in the UI: e.g. to get the minimum distance between components of an assembly. In the UI you can also see a line showing the distance between the components:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a5115ef6a8970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Mindist_UI" class="asset  asset-image at-xid-6a0167607c2431970b01a5115ef6a8970c img-responsive" src="/assets/image_38de7b.jpg" style="width: 450px;" title="Mindist_UI" /></a>&#0160;</p>
<p>You can get back the points of the distance line and the objects it was measured between using the API as well. Here is the description in the API Help file about the last parameter of&#0160;<strong>GetMinimumDistance</strong>:</p>
<p><span style="font-family: &#39;courier new&#39;, courier;"><strong>Context</strong> - Optional output <strong>NameValueMap</strong> object that returns additional information regarding the measurement. Following are the possible return values (descriptions are below the table): </span></p>
<p><span style="font-family: &#39;courier new&#39;, courier;"><strong>ClosestPointOne</strong> - <strong>Point</strong> object that returns a transient point closest to entity one in the minimum distance measure. This point may or may not lie on entity one (but will lie on the plane or axis of entity one). If entity one is a position input (Point, Vertex, WorkPoint, etc.), the position of the input entity is returned </span></p>
<p><span style="font-family: &#39;courier new&#39;, courier;"><strong>ClosestPointTwo</strong> - <strong>Point</strong> object that returns a transient point closest to entity two in the minimum distance measure. This point may or may not lie on entity two (but will lie on the plane or axis of entity two). If entity two is a position input (Point, Vertex, WorkPoint, etc.), the position of the input entity is returned </span></p>
<p><span style="font-family: &#39;courier new&#39;, courier;"><strong>ClosestEntityOne</strong> - <strong>Object</strong> type that returns the entity on which the returned ClosestPointOne lies. This is applicable only in the case where the input EntityOne is a ComponentOccurrence (or its proxy) </span></p>
<p><span style="font-family: &#39;courier new&#39;, courier;"><strong>ClosestEntityTwo</strong> - <strong>Object</strong> type that returns the entity on which the returned ClosestPointTwo lies. This is applicable only in the case where the input EntityTwo is a ComponentOccurrence (or its proxy) </span></p>
<p><span style="font-family: &#39;courier new&#39;, courier;"><strong>IntersectionFound</strong> - <strong>Boolean</strong> that indicates whether an intersection was found between entities one and two. If True, the method returns a value of 0</span></p>
<p>The below VBA code will create two work points to represent the points we get from the <strong>GetMinimumDistance</strong> function:</p>
<pre>Sub MinDistance()
  Dim doc As AssemblyDocument
  Set doc = ThisApplication.ActiveDocument
  
  Dim occs As ComponentOccurrences
  Set occs = doc.ComponentDefinition.Occurrences
  
  Dim context As NameValueMap
  Dim dist As Double
  dist = ThisApplication.MeasureTools.GetMinimumDistance( _
    occs(1), occs(2), , , context)
  
  &#39; Let&#39;s add the points as work points
  Dim wps As WorkPoints
  Set wps = doc.ComponentDefinition.WorkPoints
  
  Call wps.AddFixed(context.Item(&quot;ClosestPointOne&quot;))
  Call wps.AddFixed(context.Item(&quot;ClosestPointTwo&quot;))
End Sub</pre>
<p>And here is the result of the above code:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73d6a80c6970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Mindist_API" class="asset  asset-image at-xid-6a0167607c2431970b01a73d6a80c6970d img-responsive" src="/assets/image_53a87b.jpg" style="width: 450px;" title="Mindist_API" /></a></p>
<p>&#0160;</p>
