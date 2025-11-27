---
layout: "post"
title: "Get to iPart factory from a member"
date: "2015-09-09 03:25:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/09/get-to-ipart-factory-from-a-member.html "
typepad_basename: "get-to-ipart-factory-from-a-member"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If there are some features that you would like to get from an <strong>iPart instance</strong>, then you&#39;ll have to get inside the factory, because the instance is just like a derived part referencing the solid from another part document:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d155bb92970c-pi" style="display: inline;"><img alt="IPart_member" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d155bb92970c img-responsive" src="/assets/image_4f9ad9.jpg" title="IPart_member" /></a></p>
<p>If you&#39;re using an <strong>iPart member</strong> inside an assembly then the <strong>Open</strong> command will take you to the <strong>factory</strong>, since it&#39;s more useful, I guess:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d155bbba970c-pi" style="display: inline;"><img alt="IPart_assembly" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d155bbba970c image-full img-responsive" src="/assets/image_bc96d3.jpg" title="IPart_assembly" /></a><br />However, when using the&#0160;<strong>API</strong>, you&#39;ll have to get to the <strong>factory</strong> from the <strong>member</strong>.&#0160;</p>
<p>If we have the above assembly open and run the following <strong>VBA</strong> code then it will list the parameters that the <strong>iFeature</strong> has been placed with inside the <strong>iPart factory</strong> document:</p>
<pre>Sub iFeatureFromFactory()
  Dim asm As AssemblyDocument
  Set asm = ThisApplication.ActiveDocument
  
  Dim coMember As ComponentOccurrence
  Set coMember = asm.ComponentDefinition.Occurrences(1)
  
  &#39; This will give you back the definition of the
  &#39; iPart member, not the factory &quot;iPart-01.ipt&quot;
  Dim pcdMember As PartComponentDefinition
  Set pcdMember = coMember.Definition
  
  &#39; Now this is the actual factory document &quot;iPart.ipt&quot;
  Dim factoryDoc As PartDocument
  Set factoryDoc = pcdMember.iPartMember. _
    ReferencedDocumentDescriptor.ReferencedDocument
  
  Dim pcdFactory As PartComponentDefinition
  Set pcdFactory = factoryDoc.ComponentDefinition
  
  Dim ifFactory As iFeature
  Set ifFactory = pcdFactory.Features.iFeatures(1)
  
  Dim text As String
  text = ifFactory.Name
  
  &#39; Values are in internal units, e.g. length is cm 
  Dim param As Parameter
  For Each param In ifFactory.Parameters
    text = text + vbCrLf + _
      param.Name + &quot; = &quot; + _
      Str(param.Value)
  Next
  
  MsgBox (text)
End Sub</pre>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d15600e6970c-pi" style="display: inline;"><img alt="IPart_feature" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d15600e6970c img-responsive" src="/assets/image_d4cb31.jpg" title="IPart_feature" /></a></p>
<p>&#0160;</p>
