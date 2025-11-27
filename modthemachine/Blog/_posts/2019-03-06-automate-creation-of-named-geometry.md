---
layout: "post"
title: "Automate creation of Named Geometry"
date: "2019-03-06 18:08:58"
author: "Adam Nagy"
categories:
  - "Adam"
  - "iLogic"
  - "Inventor"
  - "Visual Basic for Applications (VBA)"
original_url: "https://modthemachine.typepad.com/my_weblog/2019/03/automate-creation-of-named-geometry.html "
typepad_basename: "automate-creation-of-named-geometry"
typepad_status: "Publish"
---

<p><strong>Named Geometry</strong> is an <strong>iLogic</strong> feature that lets you tag geometry in the <strong>UI</strong>:<br />(See section &quot;Use Assign Name to Identify Geometry for Constraints&quot; <a href="https://help.autodesk.com/view/INVNTOR/2019/ENU/?guid=GUID-80AD0392-0B8C-4A27-A9B3-7466D53999BF">here</a>)</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a4426edd200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"> </a> <a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a49030d6200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="AssignName" class="asset  asset-image at-xid-6a00e553fcbfc688340240a49030d6200b img-responsive" src="/assets/image_802579.jpg" title="AssignName" /></a><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a4426edd200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><br /></a></p>
<p><strong>iLogic</strong> has an <a href="https://knowledge.autodesk.com/search-result/caas/CloudHelp/cloudhelp/2019/ENU/Inventor-iLogic/iLogic-API/html/f6ad8d4d-8d5a-8967-a35d-d77a28628d0b-htm.html">Automation</a> interface that lets you automate <strong>iLogic</strong> specific features from your <strong>iLogic Rule</strong> or <strong>add-in</strong>/<strong>app</strong>.&#0160;</p>
<p>Using that we can also manipulate <strong>Named Geometry </strong>entries - e.g. add a new one. <br />This is what it would look like in a <strong>Rule</strong>:</p>
<pre>&#39; Select Face before running this code
Dim iLogicAuto = iLogicVb.Automation

Dim namedEntities = iLogicAuto.GetNamedEntities(ThisDoc.Document)

Dim f = ThisDoc.Document.SelectSet(1)

namedEntities.SetName(f, &quot;MyNewFace&quot;)</pre>
<p>Result:</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a4903150200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Assignname2" class="asset  asset-image at-xid-6a00e553fcbfc688340240a4903150200b img-responsive" src="/assets/image_81958.jpg" title="Assignname2" /></a></p>
<p>We can also achieve the same from e.g. <strong>VBA</strong>:</p>
<pre>Sub AddNamedGeometry()
 &#39; Select Face before running this code
  Const iLogicAddinGuid As String = &quot;{3BDD8D79-2179-4B11-8A5A-257B1C0263AC}&quot;

  Dim doc As Document
  Set doc = ThisApplication.ActiveDocument
  
  Dim addin As ApplicationAddIn
  Set addin = ThisApplication.ApplicationAddIns.ItemById(iLogicAddinGuid)

  Dim iLogicAuto As Object
  Set iLogicAuto = addin.Automation

  Dim namedEntities As Object
  Set namedEntities = iLogicAuto.GetNamedEntities(doc)

  Dim f As Face
  Set f = doc.SelectSet(1)

  Call namedEntities.SetName(f, &quot;MyNewFace&quot;)
End Sub</pre>
<p>-Adam</p>
