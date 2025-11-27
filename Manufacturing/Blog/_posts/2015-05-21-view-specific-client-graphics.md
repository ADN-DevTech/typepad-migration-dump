---
layout: "post"
title: "View specific Client Graphics"
date: "2015-05-21 08:01:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/05/view-specific-client-graphics.html "
typepad_basename: "view-specific-client-graphics"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>When you create client graphics, you can also specify in which view they will be visible via the&#0160;<strong>GraphicsNode.VisibleInViews</strong>&#0160;collection.</p>
<p>Here is a <strong>VBA</strong> sample that creates view specific graphics in each view window:</p>
<pre>Sub DisplayGraphicsInViewSample()
  ThisApplication.Documents.CloseAll
  
  Dim oDoc As PartDocument
  Set oDoc = ThisApplication.Documents.Add(kPartDocumentObject)
  oDoc.Views.Add
  
  Dim oRangeViews As ControlDefinition
  Set oRangeViews = ThisApplication.CommandManager.ControlDefinitions( _
    &quot;AppArrangeAllWindowsCmd&quot;)
  oRangeViews.Execute
  
  Dim oCG As ClientGraphics
  Set oCG = oDoc.ComponentDefinition.ClientGraphicsCollection.Add( _
    &quot;ClientGraphics&quot;)
  
  &#39; graphics in this node will be displayed in the first View
  Dim oNode1 As GraphicsNode
  Set oNode1 = oCG.AddNode(1)
  oNode1.VisibleInViews.Add oDoc.Views(1)
  
  Dim oText1 As TextGraphics
  Set oText1 = oNode1.AddTextGraphics
  oText1.Text = oDoc.Views(1).Caption
  oText1.PutTextColor 255, 0, 0
  
  &#39; graphics in this node will be displayed in the second View
  Dim oNode2 As GraphicsNode
  Set oNode2 = oCG.AddNode(2)
  oNode2.VisibleInViews.Add oDoc.Views(2)
  
  Dim oText2 As TextGraphics
  Set oText2 = oNode2.AddTextGraphics
  oText2.Text = oDoc.Views(2).Caption
  oText2.PutTextColor 0, 255, 0
  
  &#39; update views
  oDoc.Views(1).Update
  oDoc.Views(2).Update
End Sub</pre>
<p>The result:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1174eba970c-pi" style="display: inline;"><img alt="CustomGraphics" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1174eba970c image-full img-responsive" src="/assets/image_199c08.jpg" title="CustomGraphics" /></a></p>
<p>&#0160;</p>
