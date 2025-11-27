---
layout: "post"
title: "Republish Markup Sheets"
date: "2014-10-07 03:19:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/10/republish-markup-sheets.html "
typepad_basename: "republish-markup-sheets"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>The <strong>DWF</strong> markup related functionalities in the drawing are provided by the <strong>DWF Markup Manager</strong> tranlator addin, and it does not provide an <strong>API</strong>. However, you can still take advantage of its <strong>ControlDefinitions</strong> if needed:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c6ef84e3970b-pi" style="display: inline;"><img alt="Markupmgr" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c6ef84e3970b image-full img-responsive" src="/assets/image_114399.jpg" title="Markupmgr" /></a></p>
<p>If you use the&#0160;<strong>PrintCommandNames</strong>&#0160;function from <a href="http://modthemachine.typepad.com/my_weblog/2009/03/running-commands-using-the-api.html" target="_self">this blog post</a> then you&#39;ll find these registered by this addin:</p>
<pre>MarkupManager:Command: Activate Parent                &amp;Activate
MarkupManager:Command: Done                           Done
MarkupManager:Command: DWF File Close                 &amp;Close Markup DWF ...
MarkupManager:Command: DWF File Open                  Open DWF Markup set
MarkupManager:Command: DWF Republish All Sheets       Republish &amp;All Sheets
MarkupManager:Command: DWF Republish Markup Sheets    Republish &amp;Markup Sheets
MarkupManager:Command: Hide Non Markup Sheets         Show &amp;non-markup sheets
MarkupManager:Command: Markup Information             &amp;Properties...
MarkupManager:Command: Open DWFViewer                 &amp;Open in Design Review
MarkupManager:Command: Question                       Question
MarkupManager:Command: Resolve Link                   &amp;Resolve Link...
MarkupManager:Command: Review                         For Review
MarkupManager:Command: Save Markup History Changes    &amp;Save Markup History Changes
MarkupManager:Command: Sheet Information              &amp;Properties...
MarkupManager:Command: Switch Markup Visibility       Hide markups
MarkupManager:Command: Zoom To Markup Graphics        &amp;Zoom To Markup</pre>
<p>The <strong>Republish Markup Sheets&#0160;</strong>command requires a file name. The way to provide that is through the&#0160;<strong>PostPrivateEvent</strong> function. Here is a VBA sample:</p>
<pre>Sub RepublishMarkupSheets()
  Dim oCommandMgr As CommandManager
  Set oCommandMgr = ThisApplication.CommandManager
    
  &#39; Get the collection of control definitions
  Dim oControlDefs As ControlDefinitions
  Set oControlDefs = oCommandMgr.ControlDefinitions
    
  Call oCommandMgr.PostPrivateEvent( _
    kFileNameEvent, &quot;C:\temp\test.dwf&quot;)
  Call oControlDefs( _
    &quot;MarkupManager:Command: DWF Republish Markup Sheets&quot;).Execute
End Sub</pre>
<p><strong>iLogic</strong>&#0160;/<strong> .NET</strong> version:</p>
<pre>Dim oCommandMgr As CommandManager
oCommandMgr = ThisApplication.CommandManager

&#39; Get the collection of control definitions
Dim oControlDefs As ControlDefinitions
oControlDefs = oCommandMgr.ControlDefinitions

Call oCommandMgr.PostPrivateEvent(
  PrivateEventTypeEnum.kFileNameEvent, &quot;C:\temp\test.dwf&quot;)
Call oControlDefs(
  &quot;MarkupManager:Command: DWF Republish Markup Sheets&quot;).Execute</pre>
