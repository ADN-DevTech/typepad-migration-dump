---
layout: "post"
title: "List Custom iProperties of selected occurrence"
date: "2014-09-29 11:56:20"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "iLogic"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/09/list-custom-iproperties-of-selected-occurrence.html "
typepad_basename: "list-custom-iproperties-of-selected-occurrence"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>As you can see in this blog post you can prompt the user to select an object using <strong>iLogic</strong>:<br /><a href="http://adndevblog.typepad.com/manufacturing/2013/11/do-selection-from-ilogic.html" target="_self" title="">http://adndevblog.typepad.com/manufacturing/2013/11/do-selection-from-ilogic.html</a></p>
<p>Once you have the selected component then using the <strong>Inventor API</strong> you can also get all the <strong>iProperties</strong> from the document that is represented by that occurrence:<br /><a href="http://modthemachine.typepad.com/my_weblog/2010/02/accessing-iproperties.html" target="_self" title="">http://modthemachine.typepad.com/my_weblog/2010/02/accessing-iproperties.html</a></p>
<p>Here is an <strong>iLogic Rule</strong> that asks the user to select a component then lists its <strong>Custom iProperties:&#0160;</strong></p>
<pre>Dim entity = ThisApplication.CommandManager.Pick(
  SelectionFilterEnum.kAssemblyOccurrenceFilter, 
  &quot;Select Component:&quot;)

If (Not entity Is Nothing) And _
(TypeOf entity Is ComponentOccurrence) Then
  Dim doc = entity.Definition.Document
  Dim propSet = doc.propertySets(
    &quot;Inventor User Defined Properties&quot;)
  Dim msg As String
  For Each prop In propSet
    msg = msg + prop.Name + &quot; = &quot; + _
      prop.Value.ToString() + vbCrLf
  Next
  MessageBox.Show(msg, &quot;iProperties&quot;)
End If</pre>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d073be9c970c-pi" style="display: inline;"><img alt="IProperties" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d073be9c970c image-full img-responsive" src="/assets/image_bb318e.jpg" title="IProperties" /></a></p>
<p>&#0160;If you want to do things wihtout the user having to select a component then you could do it like this:</p>
<pre>Dim msg As String

&#39; If you know the name of both the component 
&#39; and the iProperties then you can do this
msg = &quot;MyText = &quot; + 
  iProperties.Value(&quot;TheBox:1&quot;, &quot;Custom&quot;, &quot;MyText&quot;)
MessageBox.Show(msg, &quot;iProperties&quot;)

&#39; If you only know the component name then
&#39; first get the component
comp = Component.InventorComponent(&quot;TheBox:1&quot;)

&#39; Then iterate through the Custom iProperties
Dim doc = comp.Definition.Document
Dim propSet = doc.propertySets(
  &quot;Inventor User Defined Properties&quot;)

msg = &quot;&quot;
For Each prop In propSet
  msg = msg + prop.Name + &quot; = &quot; + _
    prop.Value.ToString() + vbCrLf
Next
MessageBox.Show(msg, &quot;iProperties&quot;)</pre>
