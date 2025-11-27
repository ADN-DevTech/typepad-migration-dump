---
layout: "post"
title: "Model geometry in different contexts"
date: "2015-08-17 05:21:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/08/model-geometry-in-different-contexts.html "
typepad_basename: "model-geometry-in-different-contexts"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>There is already a blog post on a similar topic <a href="http://adndevblog.typepad.com/manufacturing/2013/07/occurrences-contexts-definitions-proxies.html" target="_self">here</a>, and so this one is just to clarify that not only transformations, but model geometry is also affected by it. &#0160;</p>
<p>a) If you have a part and you modify its <strong>PartComponentDefinition</strong> then all occurrences of it in assemblies will be affected by it.<br />b) If you only modify the geometry of its <strong>ComponentOccurrence</strong> in the assembly then it will not affect its definition, and so will only be reflected in that given occurrence.</p>
<p>We have the following part and assembly, where the part&#39;s geometry is affected by features added at the assembly level - i.e. only the component occurrence will be affected:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08634f44970d-pi" style="display: inline;"><img alt="ProxyVsNative2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb08634f44970d image-full img-responsive" src="/assets/image_200fc1.jpg" title="ProxyVsNative2" /></a></p>
<p>This <strong>VBA</strong> code shows the difference between the bodies you get back in different ways: the <strong>original has 6 faces</strong> as expected, and the <strong>occurrence has 8 faces</strong> because of the extra <strong>2 faces created by the chamfer feature in the assembly</strong>:</p>
<pre>Sub FaceCountOfProxyVsNative()
  Dim occ As ComponentOccurrence
  Set occ = ThisApplication.ActiveDocument.SelectSet(1)
  MsgBox _
    &quot;Proxy Body Face Count = &quot; + _
      Str(occ.SurfaceBodies(1).Faces.Count) + vbCrLf + _
    &quot;Native Body Face Count = &quot; + _
      Str(occ.<strong>Definition</strong>.SurfaceBodies(1).Faces.Count)
End Sub</pre>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d148de69970c-pi" style="display: inline;"><img alt="ProxyVsNative3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d148de69970c image-full img-responsive" src="/assets/image_5ea19c.jpg" title="ProxyVsNative3" /></a></p>
<p>&#0160;</p>
