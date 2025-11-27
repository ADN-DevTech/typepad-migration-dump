---
layout: "post"
title: "Change icon of native browser node"
date: "2012-11-26 06:22:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/11/change-icon-of-native-browser-node.html "
typepad_basename: "change-icon-of-native-browser-node"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Changing the icon of a native browser node is similar to <a href="http://adndevblog.typepad.com/manufacturing/2012/08/change-the-browser-node-icon.html">setting the icon of your own panel&#39;s client node</a>, but you work with NativeBrowserNodeDefinition and set the OverrideIcon property instead.</p>
<pre>Sub ChangeIcon()
    Dim bps As BrowserPanes
    Set bps = ThisApplication.ActiveDocument.BrowserPanes

    &#39; e.g. if you want to change the root node&#39;s icon
    &#39; in the assembly &quot;Model&quot; panel
    Dim bp As BrowserPane
    Set bp = bps(&quot;AmBrowserArrangement&quot;)
        
    Dim bmp As IPictureDisp
    Set bmp = LoadPicture(&quot;C:\Bitmap1.bmp&quot;)
        
    Dim cnr As ClientNodeResource
    &#39; Only Bitmaps are supported by the Add method<br />    &#39; and it should be 16x16
    Set cnr = bps.ClientNodeResources.Add( _
      &quot;MyResource&quot;, 2, bmp)
      
    Dim nbnd As NativeBrowserNodeDefinition
    Set nbnd = bp.TopNode.BrowserNodeDefinition

    nbnd.OverrideIcon = cnr
End Sub</pre>
