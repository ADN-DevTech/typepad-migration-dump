---
layout: "post"
title: "Get outer profile loop in sketch"
date: "2015-01-30 08:33:17"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/01/get-outer-profile-loop-in-sketch.html "
typepad_basename: "get-outer-profile-loop-in-sketch"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If you want to find the outermost profile loop in a sketch then you can just create a temporary <strong>Profile</strong> from all the sketch entities and then check the first <strong>ProfilePath</strong> in it. That should be the outer loop. If there are multiple outer loops then you would need to check the other <strong>ProfilePath</strong>&#39;s too with <strong>AddsMaterial = True</strong>. Also if two paths are intersecting then you cannot be sure which one of those you&#39;ll get back - see below pics.</p>
<p>Here is a <strong>VBA</strong> code that shows how to do it:</p>
<pre>Sub HighlightOuterSketchLoop()
  &#39; You need to be inside the sketch
  Dim sk As PlanarSketch
  Set sk = ThisApplication.ActiveEditObject
  
  Dim doc As Document
  Set doc = ThisApplication.ActiveDocument
  
  Dim tm As TransactionManager
  Set tm = ThisApplication.TransactionManager
  
  &#39; Create a transaction that we&#39;ll abort
  Dim tr As Transaction
  Set tr = tm.StartTransaction(doc, &quot;Temp&quot;)
    
  &#39; You should not leave a transaction
  &#39; hanging in the air. It will make Inventor
  &#39; unstable. So in case of error we&#39;ll
  &#39; skip to aborting the whole thing
  On Error GoTo Oops
    
  Dim tro As TransientObjects
  Set tro = ThisApplication.TransientObjects
    
  Dim p As Profile
  Set p = sk.Profiles.AddForSolid()
  
  Dim coll As ObjectCollection
  Set coll = tro.CreateObjectCollection
  
  &#39; If there is a single outermost loop
  &#39; then that should be the first one
  Dim pp As ProfilePath
  Set pp = p(1)
    
  Dim pe As ProfileEntity
  For Each pe In pp
    Call coll.Add(pe.SketchEntity)
  Next
  
Oops:
  Call tr.Abort
  If Err &lt;&gt; 0 Then Exit Sub
  
  &#39; Let&#39;s select the lines in the UI
  Call doc.SelectSet.SelectMultiple(coll)
End Sub</pre>
<p>Here are pics showing the results:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0cbaedd970c-pi" style="display: inline;"><img alt="Sketchprofile" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0cbaedd970c image-full img-responsive" src="/assets/image_d53837.jpg" title="Sketchprofile" /></a></p>
<p>&#0160;</p>
