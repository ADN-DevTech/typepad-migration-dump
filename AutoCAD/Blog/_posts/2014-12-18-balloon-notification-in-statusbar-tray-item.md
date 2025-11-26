---
layout: "post"
title: "Balloon notification in statusbar tray item"
date: "2014-12-18 01:52:56"
author: "Balaji"
categories:
  - "2013"
  - "2014"
  - "2015"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2014/12/balloon-notification-in-statusbar-tray-item.html "
typepad_basename: "balloon-notification-in-statusbar-tray-item"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>ObjectARX 2004 SDK had this nice C++ sample on adding tray items to the status bar and displaying balloon notification in one of those tray items. I have migrated this sample project to work on AutoCAD 2015 and you can download it here :</p>
<span class="asset  asset-generic at-xid-6a0167607c2431970b01bb07c746b8970d img-responsive"><a href="http://adndevblog.typepad.com/files/statusbar.zip">StatusBar</a></span></p>
<p>
To build this sample, place it under \samples\editor folder in ObjectARX 2015 SDK path. Here is a sample code snippet to display balloon window from that sample :</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#008000">// Create the bubble notification message, and callbacks.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">int</span><span style="color:#000000">  result;</pre>
<pre style="margin:0em;"> AcApDocument *pDoc=acDocManager-&gt;curDocument();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> CString strMsg(_T(<span style="color:#a31515">&quot; Notification&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcTrayItemBubbleWindowControl bwControl(</pre>
<pre style="margin:0em;"> 	<span style="color:#a31515">&quot;Attention!&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;"> 	strMsg, </pre>
<pre style="margin:0em;"> 	<span style="color:#a31515">&quot;HyperText Here&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;"> 	<span style="color:#a31515">&quot;www.autodesk.com&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> bwControl.SetIconType(</pre>
<pre style="margin:0em;"> 	AcTrayItemBubbleWindowControl::</pre>
<pre style="margin:0em;"> 	BUBBLE_WINDOW_ICON_INFORMATION);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> bwControl.SetCallback(BubbleWindowCallback, pDoc);</pre>
<pre style="margin:0em;"> 	</pre>
<pre style="margin:0em;"> result=trayItems[0]-&gt;ShowBubbleWindow(&amp;bwControl);</pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
<p>To control the time the balloon will be displayed, please run the "TRAYSETTINGS" command and set the display time.</p>
