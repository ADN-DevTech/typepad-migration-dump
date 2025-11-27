---
layout: "post"
title: "Map family's table column to Inventor property "
date: "2016-07-13 05:14:05"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/07/map-familys-table-column-to-inventor-property.html "
typepad_basename: "map-familys-table-column-to-inventor-property"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>When trying to figure out how to do something in the <strong>Inventor API</strong>, then almost always the best way to go is: do it in the <strong>UI</strong>, investigate in the <strong>API</strong>.</p>
<p>So I registered in the <strong>Content Center</strong> my own part, created a <strong>SIZE</strong>&#0160;property then mapped it to <strong>Project.Description (Design Tracking Properties:Description)</strong>:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d204b365970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="PropertyMapping" class="asset  asset-image at-xid-6a0167607c2431970b01b8d204b365970c img-responsive" src="/assets/image_58169e.jpg" title="PropertyMapping" /></a></p>
<p>Now using the <strong>API</strong> I can find out how the <strong>SIZE</strong> property got mapped to <strong>Project.Description</strong> property:</p>
<pre>Public Sub CCtest()
  Dim cc As ContentCenter
  Set cc = ThisApplication.ContentCenter
  
  Dim ctvn As ContentTreeViewNode
  Set ctvn = cc.TreeViewTopNode. _
    ChildNodes(&quot;Features&quot;). _
    ChildNodes(&quot;English&quot;). _
    ChildNodes(&quot;Block&quot;)
    
  Dim cf As ContentFamily
  Set cf = ctvn.Families(1)
  
  Dim ctc As ContentTableColumn
  Set ctc = cf.TableColumns(&quot;Size&quot;)
  
  If ctc.HasPropertyMap Then
    Dim psid As String
    Dim pid As String
    Call ctc.GetPropertyMap(psid, pid)
    Debug.Print &quot;PropertySetId = &quot; &amp; psid
    Debug.Print &quot;PropertyId = &quot; &amp; pid
  End If
End Sub</pre>
<p><strong>Result:</strong></p>
<pre>PropertySetId = {32853F0F-3444-11d1-9E93-0060B03C1CA6}
PropertyId = 29</pre>
<p>So basically you just have to look for the given property set in <strong>Document.PropertySets</strong> and use its <strong>InternalName</strong> property, then look for the property inside it and use the <strong>Property</strong>&#39;s <strong>PropId</strong>.</p>
