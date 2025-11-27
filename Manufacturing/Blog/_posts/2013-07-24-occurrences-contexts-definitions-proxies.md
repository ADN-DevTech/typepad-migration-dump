---
layout: "post"
title: "Occurrences, Contexts, Definitions, Proxies"
date: "2013-07-24 15:49:58"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/07/occurrences-contexts-definitions-proxies.html "
typepad_basename: "occurrences-contexts-definitions-proxies"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>There is already a <a href="http://adndevblog.typepad.com/manufacturing/2012/12/componentoccurrence-contexts-and-reference-keys.html" target="_self">blog post on this topic</a>, but it might be worth looking at it from another angle as well. In this case I&#39;ll try to talk about it through geometric information of the model. </p>
<p>Let&#39;s say we have the following document structure: <strong>Assembly</strong> &gt;&gt; <strong>SubAssembly</strong> &gt;&gt; <strong>Part</strong></p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901e6d237e970b-pi" style="display: inline;"><img alt="Proxies2" class="asset  asset-image at-xid-6a0167607c2431970b01901e6d237e970b" src="/assets/image_6b3465.jpg" style="width: 200px;" title="Proxies2" /></a>&#0160;</p>
<p><strong>Part</strong> is placed inside <strong>SubAssembly</strong> at <strong>X=3, Y=4</strong> position, and <strong>SubAssembly</strong> is placed inside <strong>Assembly</strong> at <strong>X=1, Y=2</strong> position. Let&#39;s say that the highlighted vertex is the first in the vertex list of the model:&#0160;<strong>prtDef.SurfaceBodies(1).Vertices(1)</strong></p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0192ac2c774c970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Proxies" class="asset  asset-image at-xid-6a0167607c2431970b0192ac2c774c970d" src="/assets/image_0c6a41.jpg" title="Proxies" /></a>&#0160;</p>
<p>The <strong>Vertex</strong> is the native object residing inside the part document. A <strong>VertexProxy</strong> is the <strong>proxy</strong> object that represents the <strong>Vertex</strong> inside another document. A <strong>proxy</strong> represents a <strong>native</strong> object inside another document and provides all the same properties but with values in context of the document that is referencing the native object&#39;s owner document. The <strong>vertex</strong> of the part is at <strong>X=6, Y=5</strong> inside the part. If we get to the same <strong>vertex</strong> from <strong>SubAssembly</strong> through <strong>Occurrences</strong>/<strong>SubOccurrences</strong> then we&#39;ll see it as a <strong>VertexProxy</strong> and the information will be provided in context of <strong>SubAssembly</strong> so the <strong>Point</strong> information of the <strong>vertex</strong> will be <strong>X=9, Y=9. </strong>If we get to the same vertex from the <strong>Assembly</strong> then the <strong>VertexProxy&#39;s Point</strong> will provide values in context of <strong>Assembly</strong> so it will be <strong>X=10, Y=11</strong>.&#0160;&#0160;</p>
<pre>Sub GetVertex()
  Dim docs As Documents
  Set docs = ThisApplication.Documents
    
  &#39; Assembly
  Dim asm As AssemblyDocument
  Set asm = docs.Open(&quot;C:\Occurrences\Assembly.iam&quot;)
  
  Dim asmDef As AssemblyComponentDefinition
  Set asmDef = asm.ComponentDefinition
  
  &#39; SubAssembly
  Dim subAsm As AssemblyDocument
  Set subAsm = docs.Open(&quot;C:\Occurrences\SubAssembly.iam&quot;)
  
  Dim subAsmDef As AssemblyComponentDefinition
  Set subAsmDef = subAsm.ComponentDefinition
  
  &#39; Part
  Dim prt As PartDocument
  Set prt = docs.Open(&quot;C:\Occurrences\Part.ipt&quot;)
  
  Dim prtDef As PartComponentDefinition
  Set prtDef = prt.ComponentDefinition
  
  &#39; Let&#39;s get the vertex in context of the Part
  Dim vPart As Vertex
  Set vPart = prtDef.SurfaceBodies(1).Vertices(1)
  
  Debug.Print &quot;In context of Part: &quot; + _
    &quot;x = &quot; + str(vPart.Point.X) + &quot;; &quot; + _
    &quot;y = &quot; + str(vPart.Point.Y)
    
  &#39; Let&#39;s get it in context of SubAssembly
  &#39; In this case we&#39;ll get a VertexProxy
  &#39; that represents Vertex of the Part
  &#39; Note: we could still declare it as Vertex
  Dim vSubAssembly As VertexProxy
  Set vSubAssembly = _
    subAsmDef.Occurrences(1).SurfaceBodies(1).Vertices(1)
  
  Debug.Print &quot;In context of SubAssembly: &quot; + _
    &quot;x = &quot; + str(vSubAssembly.Point.X) + &quot;; &quot; + _
    &quot;y = &quot; + str(vSubAssembly.Point.Y)
    
  &#39; We could also break the chain to get to the occurrence&#39;s
  &#39; definition by using the Definition property
  &#39; Some objects can only be reached from the ComponentDefinition
  &#39; in which case we need to create a GeometryProxy for it ourselves
  &#39; When using the Occurrences/SubOccurrences properties
  &#39; then the proxies are available without us having to create them
  Debug.Print _
    &quot;Note: subAsmDef.Occurrences(1).Definition = prtDef is &quot; + _
    str(subAsmDef.Occurrences(1).Definition Is prtDef)
  
  &#39; Let&#39;s get it in context of Assembly
  &#39; In this case we&#39;ll get a VertexProxy
  &#39; that represents Vertex of the Part
  &#39; Note: we could still declare it as Vertex
  Dim vAssembly As VertexProxy
  Set vAssembly = asmDef.Occurrences(1).SubOccurrences(1). _
    SurfaceBodies(1).Vertices(1)
  
  Debug.Print &quot;In context of Assembly: &quot; + _
    &quot;x = &quot; + str(vAssembly.Point.X) + &quot;; &quot; + _
    &quot;y = &quot; + str(vAssembly.Point.Y)
    
  Debug.Print _
    &quot;Note: asmDef.Occurrences(1).Definition = subAsmDef is &quot; + _
    str(asmDef.Occurrences(1).Definition Is subAsmDef)
    
  Debug.Print _
    &quot;Note: asmDef.Occurrences(1).SubOccurrences(1).Definition&quot; + _
    &quot; = prtDef is &quot; + _
    str(asmDef.Occurrences(1).SubOccurrences(1).Definition Is prtDef)
    
  &#39; If you wanted to get to the vertex in context of the Assembly
  &#39; through one of the definitions then you just have to close the
  &#39; loop of Assembly &gt;&gt; SubAssembly &gt;&gt; Part &gt;&gt; Vertex
  
  &#39; If you got the vertex from Part then you can do it by creating the
  &#39; proxy on the Part&#39;s occurrence
  Call asmDef.Occurrences(1).SubOccurrences(1). _
    CreateGeometryProxy(vPart, vAssembly)
  
  Debug.Print &quot;In context of Assembly #1: &quot; + _
    &quot;x = &quot; + str(vAssembly.Point.X) + &quot;; &quot; + _
    &quot;y = &quot; + str(vAssembly.Point.Y)
    
  &#39; If you got the vertex (VertexProxy) from SubAssembly then you can
  &#39; do it by creating proxy on the SubAssembly&#39;s occurrence
  Call asmDef.Occurrences(1). _
    CreateGeometryProxy(vSubAssembly, vAssembly)
  
  Debug.Print &quot;In context of Assembly #2: &quot; + _
    &quot;x = &quot; + str(vAssembly.Point.X) + &quot;; &quot; + _
    &quot;y = &quot; + str(vAssembly.Point.Y)
End Sub</pre>
<p>The above VBA sample code will print this to the <strong>Immediate</strong> window of the <strong>VBA</strong> environment:</p>
<pre>In context of Part: x =  6; y =  5
In context of SubAssembly: x =  9; y =  9
Note: subAsmDef.Occurrences(1).Definition = prtDef is True
In context of Assembly: x =  10; y =  11
Note: asmDef.Occurrences(1).Definition = subAsmDef is True
Note: asmDef.Occurrences(1).SubOccurrences(1).Definition = prtDef is True
In context of Assembly #1: x =  10; y =  11
In context of Assembly #2: x =  10; y =  11</pre>
