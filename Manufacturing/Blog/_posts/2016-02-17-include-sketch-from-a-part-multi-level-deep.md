---
layout: "post"
title: "Include Sketch from a Part multi-level deep"
date: "2016-02-17 02:01:46"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/02/include-sketch-from-a-part-multi-level-deep.html "
typepad_basename: "include-sketch-from-a-part-multi-level-deep"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>We already have a blog post on <a href="http://adndevblog.typepad.com/manufacturing/2012/06/include-sketches-from-sub-assemblies-in-a-drawingview-using-a-sketch-proxy.html">Include sketches from sub-assemblies in a DrawingView using a sketch proxy</a><span class="s1">, but it does not cover the scenario where the <strong>Document</strong>&#0160;the <strong>Sketch</strong>&#0160;resides in is <strong>multi-level deep</strong> in the assembly hierarchy - i.e. it&#39;s a <strong>SubOccurrence</strong> of an <strong>Occurrence</strong>.</span></p>
<p><span class="s1">As I mentioned in <a href="http://adndevblog.typepad.com/manufacturing/2013/07/occurrences-contexts-definitions-proxies.html">this blog post</a> you have to make sure that you are getting objects in the right context. You can either do that by reaching objects only through <strong>Occurrences</strong> and <strong>SubOccurrences</strong> collections (never using <strong>Definition</strong>) or if you have to use <strong>Definition</strong> then get the object back into the right context using <strong>CreateGeometryProxy</strong>().<br />If you do not follow that correctly, like the below code, then your program won&#39;t work:</span></p>
<p><span class="s1"> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c816ab2a970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="IncludeSketchFromPart" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c816ab2a970b image-full img-responsive" src="/assets/image_414fea.jpg" title="IncludeSketchFromPart" /></a><br /></span></p>
<p>Whenever you use the property &quot;<strong>Definition</strong>&quot; you leave the <strong>current context</strong> and move into a lower one. As you can see the code only brings the&#0160;<strong>oPrtSketch</strong> entity into the <strong>context</strong> of the <strong>MidAssembly.iam</strong> document. However, the <strong>DrawingView</strong> is referencing <strong>TopAssembly.iam</strong>, so that is the <strong>context</strong> the objects you pass to it should be in. &#0160;</p>
<p>The quickest fix to the above code is taking note of the occurrence of <strong>MidAssembly.iam</strong> inside <strong>TopAssembly.iam</strong>&#0160;(<strong>oMidAsmOcc</strong>) and doing an extra call to&#0160;<strong>CreateGeometryProxy</strong> to bring the object into the context of <strong>TopAssembly.iam</strong>. New code highlighted in red:<strong>&#0160;</strong></p>
<pre>Sub IncludeSketchFromPart()
Dim oDwg As DrawingDocument
Set oDwg = ThisApplication.ActiveDocument

Dim oView As DrawingView
Set oView = oDwg.ActiveSheet.DrawingViews(1)

Dim oTopAsm As AssemblyDocument
Set oTopAsm = oView.ReferencedDocumentDescriptor.ReferencedDocument

&#39;Dim oMidAsmDef As AssemblyComponentDefinition
&#39;Set oMidAsmDef = oAssy.ComponentDefinition.Occurrences.ItemByName(&quot;MidAssembly:1&quot;).Definition

<span style="color: #ff0000;">Dim oMidAsmOcc As ComponentOccurrence
Set oMidAsmOcc = oTopAsm.ComponentDefinition.Occurrences.ItemByName(&quot;MidAssembly:1&quot;)</span>

Dim oMidAsmDef As AssemblyComponentDefinition
Set oMidAsmDef = oMidAsmOcc.Definition

Dim oPrtOcc As ComponentOccurrence
Set oPrtOcc = oMidAsmDef.Occurrences.ItemByName(&quot;PartWithSketch:1&quot;)

Dim oSketch As PlanarSketch
Set oSketch = oPrtOcc.Definition.Sketches(&quot;PartSketch&quot;)

Dim oSketchProxy As PlanarSketchProxy
&#39; This will get it into context of &quot;MidAssembly.iam&quot;
Call oPrtOcc.CreateGeometryProxy(oSketch, oSketchProxy)

<span style="color: #ff0000;">&#39; This will get it into context of &quot;TopAssembly.iam&quot;
Call oMidAsmOcc.CreateGeometryProxy(oSketchProxy, oSketchProxy)</span>

Call oView.SetVisibility(oSketchProxy, True)
End Sub</pre>
