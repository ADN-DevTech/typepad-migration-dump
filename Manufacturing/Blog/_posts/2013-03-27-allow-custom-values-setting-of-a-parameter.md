---
layout: "post"
title: "Allow custom values setting of a parameter"
date: "2013-03-27 03:45:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/03/allow-custom-values-setting-of-a-parameter.html "
typepad_basename: "allow-custom-values-setting-of-a-parameter"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>In the user interface this is where you can set the <strong>Allow custom values</strong> option for a parameter:</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c3820db37970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="AllowCustomValuesSetting" class="asset  asset-image at-xid-6a0167607c2431970b017c3820db37970b" src="/assets/image_767307.jpg" style="width: 450px;" title="AllowCustomValuesSetting" /></a></p>
<p>In case of the API, you&#39;ll find it in a custom Attribute:</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d4250018e970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="AllowCustomValue" class="asset  asset-image at-xid-6a0167607c2431970b017d4250018e970c" src="/assets/image_0f8c6e.jpg" style="width: 450px;" title="AllowCustomValue" /></a></p>
<p>Here is a VBA sample that toggles this value for the first user parameter if possible:</p>
<pre>Sub Toggle_AllowCustomValue()
    Dim doc As PartDocument
    Set doc = ThisApplication.ActiveDocument
    
    Dim pcd As PartComponentDefinition
    Set pcd = doc.ComponentDefinition
    
    Dim p As UserParameter
    Set p = pcd.Parameters.UserParameters(1)
    
    On Error Resume Next
    
    Dim att As Inventor.Attribute
    Set att = p.AttributeSets _
        (&quot;com.autodesk.inventor.ParameterAttributes&quot;) _
        (&quot;AllowCustomValue&quot;)
    
    If Not att Is Nothing Then
        att.Value = Not att.Value
    End If
    
    On Error GoTo 0
End Sub</pre>
