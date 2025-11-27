---
layout: "post"
title: "Attributes of ComponentOccurrence vs ComponentOccurrenceProxy"
date: "2015-07-15 18:06:41"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/07/attributes-of-componentoccurrence-vs-componentoccurrenceproxy.html "
typepad_basename: "attributes-of-componentoccurrence-vs-componentoccurrenceproxy"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Attributes are context sensitive because they are saved in the document which is hosting the object that has the attribute.</p>
<p>Let&#39;s say we have the following assembly structure:</p>
<p><strong>Asm1</strong><br />&#0160; + <strong>Asm2</strong>:1<br />&#0160; &#0160; + <strong>Part1</strong>:1<br />&#0160; &#0160; + <strong>Part2</strong>:1</p>
<p>If you open up <strong>Asm2.iam</strong> and add an attribute to <strong>Part1:1</strong> then it will be stored in <strong>Asm2.iam</strong>, so if now you open up <strong>Asm1.iam</strong> and check the attributes on <strong>Asm2:1</strong> &gt;&gt; <strong>Part1:1</strong>, the attribute will not be there. However, you can easily move to the corresponding object in <strong>Asm2.iam</strong> using the <strong>NativeObject</strong> property of the <strong>ComponentOccurrenceProxy</strong> object.&#0160;</p>
<p>Some info on Proxies and Contexts:&#0160;<a href="http://adndevblog.typepad.com/manufacturing/2013/07/occurrences-contexts-definitions-proxies.html" target="_self" title="">http://adndevblog.typepad.com/manufacturing/2013/07/occurrences-contexts-definitions-proxies.html</a></p>
<p>You can play with attributes using the following <strong>VBA</strong> code:<br /><strong>- SetAttribute</strong> adds an attribute to the selected occurrence with a value that also contains the display name of the assembly document that hosts the occurrence&#0160;<br /><strong>- GetAttribute</strong> gets back the attribute of the selected occurrence - if <strong>useNativeObject = True</strong> then it will look for the attribute inside the document that hosts the occurrence:</p>
<pre>Sub SetAttribute()
  Dim doc As Document
  Set doc = ThisApplication.ActiveDocument

  Dim ent As Object
  Set ent = doc.SelectSet(1)

  Dim attSets As AttributeSets
  Set attSets = ent.AttributeSets
  
  Dim attSet As AttributeSet

  If attSets.NameIsUsed(&quot;MyAttSet&quot;) Then
    Set attSet = attSets(&quot;MyAttSet&quot;)
  Else
    Set attSet = attSets.Add(&quot;MyAttSet&quot;)
  End If
  
  Dim att As Attribute
  
  If attSet.NameIsUsed(&quot;MyAtt&quot;) Then
    Set att = attSet(&quot;MyAtt&quot;)
  Else
    Set att = attSet.Add(&quot;MyAtt&quot;, kStringType, _
      &quot;MyAttValue in &quot; + doc.DisplayName)
  End If
End Sub

Sub GetAttribute()
  Dim doc As Document
  Set doc = ThisApplication.ActiveDocument

  Dim ent As Object
  Set ent = doc.SelectSet(1)
  
  &#39; You can set this
  Dim useNativeObject As Boolean
  useNativeObject = False
  
  If TypeOf ent Is ComponentOccurrenceProxy _
  And useNativeObject Then
    Set ent = ent.NativeObject
  End If

  Dim attSets As AttributeSets
  Set attSets = ent.AttributeSets
  
  Dim attSet As AttributeSet

  If attSets.NameIsUsed(&quot;MyAttSet&quot;) Then
    Set attSet = attSets(&quot;MyAttSet&quot;)

    Dim att As Attribute
  
    If attSet.NameIsUsed(&quot;MyAtt&quot;) Then
      Set att = attSet(&quot;MyAtt&quot;)
    End If
    
    Call MsgBox(att.Value)
  End If
End Sub</pre>
<p>Here is a pic showing what you get back using <strong>GetAttribute</strong> if both in <strong>Asm1</strong> and <strong>Asm2</strong> you added attributes to&#0160;<strong>Part2:1</strong>&#0160;by selecting it and calling&#0160;<strong>SetAttribute:</strong></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d138aa2c970c-pi" style="display: inline;"><img alt="Proxyattributes" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d138aa2c970c image-full img-responsive" src="/assets/image_b2e547.jpg" title="Proxyattributes" /></a></p>
<p>&#0160;</p>
