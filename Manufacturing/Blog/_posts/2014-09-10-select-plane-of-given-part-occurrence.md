---
layout: "post"
title: "Select plane of given part occurrence"
date: "2014-09-10 06:43:39"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/09/select-plane-of-given-part-occurrence.html "
typepad_basename: "select-plane-of-given-part-occurrence"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If you want to select one of the planes of a component occurrence inside an assembly, then you first have to get the component definition of that occurrence in order to get to its work planes. Then you need to get the work plane&#39;s representaion (proxy) inside the assembly through the component occurrence object:<br /><a href="http://adndevblog.typepad.com/manufacturing/2013/07/occurrences-contexts-definitions-proxies.html" target="_self" title="">http://adndevblog.typepad.com/manufacturing/2013/07/occurrences-contexts-definitions-proxies.html</a></p>
<p>In this case we select the XY plane of the currently selected part component occurrence:</p>
<pre>Sub SelectXyPlaneOfOccurrence()
    Dim doc As AssemblyDocument
    Set doc = ThisApplication.ActiveDocument
    
    &#39; Let&#39;s say the occurrence is already
    &#39; selected in the UI
    Dim occ As ComponentOccurrence
    Set occ = doc.SelectSet(1)
    
    &#39; Let&#39;s select the XY plane (3rd in the list)
    Dim pcd As PartComponentDefinition
    Set pcd = occ.Definition
    
    Dim wp As WorkPlane
    Set wp = pcd.WorkPlanes(3)
    
    &#39; Get its representation in the
    &#39; assembly
    Dim wpp As WorkPlaneProxy
    Call occ.CreateGeometryProxy(wp, wpp)
    
    &#39; Now select it
    Call doc.SelectSet.Select(wpp)
End Sub</pre>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73e133671970d-pi" style="display: inline;"><img alt="Planeselection" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a73e133671970d image-full img-responsive" src="/assets/image_fb4cf6.jpg" title="Planeselection" /></a></p>
<p>&#0160;</p>
