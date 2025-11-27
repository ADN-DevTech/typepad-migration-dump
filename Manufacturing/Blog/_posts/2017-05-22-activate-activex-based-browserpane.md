---
layout: "post"
title: "Activate ActiveX based BrowserPane"
date: "2017-05-22 06:01:46"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2017/05/activate-activex-based-browserpane.html "
typepad_basename: "activate-activex-based-browserpane"
typepad_status: "Publish"
---

<p>In <strong>Inventor 2018</strong> custom browser panes got their own dockable window so that they can be moved around by the user.</p>
<p>As a result your old code that was creating your new <strong>ActiveX</strong> based <strong>BrowserPane</strong> and was trying to <strong>activate</strong> it does not work anymore.<br />You need to find the <strong>DockableWindow</strong> that was created for your <strong>BrowserPane</strong> and make that <strong>visible</strong> - this action will also make it <strong>active</strong>:</p>
<pre>Public Sub BrowserTest()
  Dim partDoc As PartDocument
  Set partDoc = ThisApplication.ActiveDocument
       
  Dim myBrowserPane As BrowserPane
  Set myBrowserPane = partDoc.BrowserPanes.Add(&quot;Test&quot;, &quot;WMPlayer.OCX&quot;)
   
  &#39; First you have to make its container visible
  Dim uiMgr As UserInterfaceManager
  Set uiMgr = ThisApplication.UserInterfaceManager
    
  Dim dockWindow As DockableWindow
  Set dockWindow = uiMgr.DockableWindows(myBrowserPane.Name)
    
  &#39; This will also activate it
  dockWindow.Visible = True
End Sub</pre>
<p>This will achieve the same as clicking the <strong>+</strong> button in the <strong>UI</strong> and selecting your browser pane that way:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb099de4c4970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Browserpane" class="asset  asset-image at-xid-6a0167607c2431970b01bb099de4c4970d img-responsive" src="/assets/image_82065a.jpg" title="Browserpane" /></a></p>
<p>&#0160;</p>
