---
layout: "post"
title: "Set the offset of components to zero and ground them"
date: "2014-10-21 14:49:54"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "iLogic"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/10/set-the-offset-of-components-to-zero-and-ground-them.html "
typepad_basename: "set-the-offset-of-components-to-zero-and-ground-them"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>When you are inside an assembly then you can right-click on any of the components in the model tree and inside the <strong>iProperties</strong> dialog you can set their offset:</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c6f7c9c6970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c6f7c9c6970b image-full img-responsive" title="OccurrenceProperties" src="/assets/image_40d972.jpg" alt="OccurrenceProperties" border="0" /></a><br />In the <strong>API</strong> you get/set that information through the <strong>Transformation</strong>&nbsp;matrix of the <strong>Occurrence</strong>. That has the component's position and alignment.</p>
<p>If you simply want to set all the offsets to zero and orient them the same way as the assembly then you just have to use an <a href="http://en.wikipedia.org/wiki/Identity_matrix" target="_self">identity matrix</a> (default of <strong>CreateMatrix()</strong>) as their <strong>Transformation</strong>.&nbsp;</p>
<p>This is what the code could look like in <strong>iLogic</strong>:</p>
<pre>Dim doc As AssemblyDocument
doc = ThisApplication.ActiveDocument

Dim tr As TransientGeometry
tr = ThisApplication.TransientGeometry

Dim occ As ComponentOccurrence
For Each occ In doc.ComponentDefinition.Occurrences
  ' If it's suppressed we cannot do
  ' anything with it
  If Not occ.Suppressed Then
    Call occ.SetTransformWithoutConstraints( _
      tr.CreateMatrix())
    occ.Grounded = True
  End If
Next</pre>
