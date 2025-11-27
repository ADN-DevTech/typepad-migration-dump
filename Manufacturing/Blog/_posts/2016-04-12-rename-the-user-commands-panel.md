---
layout: "post"
title: "Rename the \"User Commands\" panel"
date: "2016-04-12 04:23:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/04/rename-the-user-commands-panel.html "
typepad_basename: "rename-the-user-commands-panel"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>When adding <strong>VBA</strong> macro functions to the <strong>Ribbon</strong> through the &quot;<strong>Customize User Commands...</strong>&quot; button&#0160;then they will appear in the &quot;<strong>User Commands</strong>&quot; panel.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c832eb26970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="UserCommands" class="asset  asset-image at-xid-6a0167607c2431970b01b7c832eb26970b img-responsive" src="/assets/image_792323.jpg" title="UserCommands" /></a></p>
<p><strong>RibbonPanel</strong> names cannot be changed, but maybe the panels could be replaced by other ones.</p>
<p>Instead of doing that though, I think the easiest might be just to create your own panel and add the macros to that.&#0160;</p>
<p>In this case I only have these two macros in my &quot;<strong>Module1</strong>&quot; and when I run &quot;<strong>CreateMyPanel</strong>&quot; then it will create a new panel called &quot;<strong>MyPanel</strong>&quot; and add a reference in it to the &quot;<strong>MyMessage</strong>&quot; macro.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08d7790f970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Module1" class="asset  asset-image at-xid-6a0167607c2431970b01bb08d7790f970d img-responsive" src="/assets/image_a6b2ea.jpg" title="Module1" /></a></p>
<pre>Sub MyMessage()
  MsgBox &quot;My message&quot;
End Sub

Sub CreateMyPanel()
  Dim rs As Ribbons
  Set rs = ThisApplication.UserInterfaceManager.Ribbons
  
  Dim cds As ControlDefinitions
  Set cds = ThisApplication.CommandManager.ControlDefinitions
  
  Dim cd As MacroControlDefinition
  Set cd = cds.AddMacroControlDefinition(&quot;Module1.MyMessage&quot;)
  
  Dim r As Ribbon
  Set r = rs(&quot;Part&quot;)
  
  Dim t As RibbonTab
  Set t = r.RibbonTabs(&quot;id_TabTools&quot;)
  
  Dim p As RibbonPanel
  Set p = t.RibbonPanels.Add(&quot;MyPanel&quot;, &quot;MyName.MyPanel&quot;, &quot;MyName.MyPanel&quot;)
  Call p.CommandControls.AddMacro(cd)
End Sub</pre>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c832ebf3970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="MyPanel" class="asset  asset-image at-xid-6a0167607c2431970b01b7c832ebf3970b img-responsive" src="/assets/image_0f9ebe.jpg" title="MyPanel" /></a></p>
<p>One thing to bear in mind is that programmatically added <strong>UI</strong> elements are not persisted. So you would have to run your command that adds the extra ribbon panel each time <strong>Inventor</strong> starts. The best way&#0160;to do that would be to create an <strong>addin</strong> for that. &#0160;</p>
<p>&#0160;</p>
