---
layout: "post"
title: "Get Attributes of source iFeature"
date: "2015-03-03 05:40:55"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/03/get-attributes-of-source-ifeature.html "
typepad_basename: "get-attributes-of-source-ifeature"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>If you add an <strong>AttributeSet</strong> to the <strong>iFeature</strong> in the definition file (*.<strong>ide</strong>), that will not be transferred to the <strong>iFeature</strong> instance in the target part document. If the source file still exists then you can get the attributes from there like this:</p>
<pre>Sub LookForAttributeSet()
  Dim pd As PartDocument
  Set pd = ThisApplication.ActiveDocument
  
  &#39; Select the iFeature instance in the Part document
  Dim oif As iFeature
  Set oif = pd.SelectSet(1)
  
  Dim pd2 As PartDocument
  Set pd2 = ThisApplication.Documents.Open( _
    oif.iFeatureTemplateDescriptor.LastKnownSourceFileName, _
    False)
    
  Dim oif2 As iFeature
  For Each oif2 In pd2.ComponentDefinition.Features.iFeatures
    If oif2.iFeatureTemplateDescriptor.InternalName = _<br />    oif.iFeatureTemplateDescriptor.InternalName Then
      MsgBox &quot;First AttributeSet = &quot; + oif2.AttributeSets(1).name
    End If
  Next
    
  pd2.Close
End Sub</pre>
<p>I assume the best way though to pass parameters from the <strong>iFeature</strong> definition to the <strong>iFeature</strong> instance might be to add parameters to the <strong>iFeature</strong> table in the definition file:</p>
<p><a class="asset-img-link" href="http://a7.typepad.com/6a0112791b8fe628a401b7c75819af970b-pi" style="display: inline;"><img alt="IFeature1" class="asset  asset-image at-xid-6a0112791b8fe628a401b7c75819af970b img-responsive" src="/assets/image_bd4e41.jpg" title="IFeature1" /></a></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07fbe278970d-pi" style="display: inline;"><img alt="IFeature2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb07fbe278970d image-full img-responsive" src="/assets/image_394d70.jpg" title="IFeature2" /></a><br /><br />Then with this code you can get your parameter:</p>
<pre>Sub GetIfeatureParams()
  Dim pd As PartDocument
  Set pd = ThisApplication.ActiveDocument
  
  &#39; Select the iFeature instance in the Part document
  Dim oif As iFeature
  Set oif = pd.SelectSet(1)
  
  Dim c As iFeatureTableCell
  For Each c In oif.iFeatureDefinition.ActiveTableRow
    If c.Column.Heading = &quot;MyParam&quot; Then
      Call MsgBox(c.Value)
      Exit For
    End If
  Next
End Sub</pre>
<p>&#0160;</p>
