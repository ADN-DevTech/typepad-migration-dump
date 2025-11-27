---
layout: "post"
title: "Get Named Face of iPartFactory"
date: "2020-09-15 14:30:05"
author: "Adam Nagy"
categories:
  - "Adam"
  - "iLogic"
  - "Inventor"
original_url: "https://modthemachine.typepad.com/my_weblog/2020/09/get-named-face-of-ipartfactory.html "
typepad_basename: "get-named-face-of-ipartfactory"
typepad_status: "Publish"
---

<p>One enhancement in <strong>Inventor 2019</strong> was the ability to <strong>Assign Name to objects</strong> - see <a href="https://knowledge.autodesk.com/support/inventor/learn-explore/caas/CloudHelp/cloudhelp/2019/ENU/Inventor-WhatsNew/files/GUID-80AD0392-0B8C-4A27-A9B3-7466D53999BF-htm.html">iLogic Enhancements</a></p>
<p>I wrote about how you could automate that in <a href="https://modthemachine.typepad.com/my_weblog/2019/03/automate-creation-of-named-geometry.html">Automate creation of Named Geometry</a></p>
<p>This feature is something that you could also use to mark objects in an <strong>iPart</strong> <strong>factory</strong> and use that later on to find them when its <strong>member</strong> is used in an <strong>assembly</strong>.</p>
<p>Here is an article on the difficulties of finding derived objects in <strong>iPart members</strong>: <a href="https://adndevblog.typepad.com/manufacturing/2014/11/get-edge-in-derived-part-that-drives-work-point.html">Get edge in derived part that drives work point</a>&#0160;</p>
<p>Let&#39;s say we have these files:</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc68834026be411ab03200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="NamedFaceAll" border="0" class="asset  asset-image at-xid-6a00e553fcbfc68834026be411ab03200d image-full img-responsive" src="/assets/image_297791.jpg" title="NamedFaceAll" /></a></p>
<p>The <strong>Named Face</strong> in the <strong>iPart Factory</strong> will not show up in the <strong>iPart Member</strong>, so we&#39;ll have to figure out the connection ourselves using the <strong>ReferencedEntity</strong> property of the <strong>Face</strong> in the member document.&#0160; &#0160;</p>
<p>Once we have that we need to also get the <strong>Face</strong> <strong>Proxy</strong> representing the specific <strong>Face</strong> of the <strong>iPart Member</strong> inside the <strong>Assembly</strong>, so that we can do things with it, like selecting it in the <strong>UI</strong>.</p>
<p>We can achieve all that using the following code - this is <strong>VBA</strong>&#0160;</p>
<pre>Sub TestSelectNamedFace()
  Dim selSet As SelectSet
  Set selSet = ThisApplication.ActiveDocument.SelectSet

  Dim occ As ComponentOccurrence
  Set occ = selSet(1)
  
  Call selSet.Clear
  
  Call selSet.Select(GetNamedFace(occ, &quot;MyFace&quot;))
End Sub

Function GetNamedFace(occ As ComponentOccurrence, name As String) As Face
  Dim memberDoc As PartDocument
  Set memberDoc = occ.Definition.Document
  
  If Not memberDoc.ComponentDefinition.IsiPartMember Then
    Call MsgBox(&quot;Not an iPart member based occurrence&quot;)
  End If
  
  Dim factoryDoc As PartDocument
  Set factoryDoc = memberDoc.ComponentDefinition. _
    iPartMember.ReferencedDocumentDescriptor.ReferencedDocument
    
  Dim addin As ApplicationAddIn
  Set addin = ThisApplication.ApplicationAddIns. _
    ItemById(&quot;{3BDD8D79-2179-4B11-8A5A-257B1C0263AC}&quot;)
  
  Dim iLogicAuto As Object
  Set iLogicAuto = addin.Automation
  
  Dim namedEntities As Object
  Set namedEntities = iLogicAuto.GetNamedEntities(factoryDoc)
  
  Dim f As Face
  Set f = namedEntities.FindEntity(name)
  
  If f Is Nothing Then
    Call MsgBox(&quot;Did not find named Face&quot;)
  End If
  
  Dim f2 As Face
  For Each f2 In memberDoc.ComponentDefinition.SurfaceBodies(1).Faces
    If f2.ReferencedEntity Is f Then
      &#39; Get the face into the context of the assembly
      Call occ.CreateGeometryProxy(f2, GetNamedFace)

      Exit Function
    End If
  Next
End Function</pre>
<p>First select the relevant occurrence in the assembly then run the code:</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340263e965940b200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="NamedFace5" class="asset  asset-image at-xid-6a00e553fcbfc688340263e965940b200b img-responsive" src="/assets/image_447558.jpg" title="NamedFace5" /></a></p>
<p>You can find info on <strong>NamedEntities</strong> object and its functions like <strong>FindEntity()</strong> <a href="https://help.autodesk.com/cloudhelp/2019/CSY/Inventor-iLogic/iLogic_API/html/6852069e-b8c4-e9d3-77e2-1e3d84cbbdde.htm">here</a>&#0160;</p>
<p>-Adam</p>
