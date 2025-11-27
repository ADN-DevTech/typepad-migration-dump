---
layout: "post"
title: "Pass parameters to a VBA macro from an iLogic Rule"
date: "2015-11-16 05:37:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/11/pass-parameters-to-a-vba-macro-from-an-ilogic-rule.html "
typepad_basename: "pass-parameters-to-a-vba-macro-from-an-ilogic-rule"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>Though <strong>RunMacro</strong> functionality is only listed in the <strong>iLogic</strong> function browser tree without the argument parameters: <strong>RunMacro(projectName, moduleName, macroName)</strong>&#0160;</p>
<p><a class="asset-img-link" href="http://a6.typepad.com/6a0112791b8fe628a401b7c7ede25e970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="RunMacroWithArgs" class="asset  asset-image at-xid-6a0112791b8fe628a401b7c7ede25e970b img-responsive" src="/assets/image_9efa8c.jpg" style="width: 400px;" title="RunMacroWithArgs" /></a></p>
<p>... it seems it can be used this way too:&#0160;<strong>RunMacro(projectName, moduleName, macroName, argument1, argument2, etc)&#0160;</strong>- in which case&#0160;<strong>argument1</strong>,&#0160;<strong>argument2</strong>, etc. will passed as the <strong>VBA</strong> <strong>macro</strong>&#39;s input parameters in the same order.</p>
<p>It was discussed in this forum thread:&#0160;<br /><a href="https://forums.autodesk.com/t5/inventor-general-discussion/ilogic/td-p/2611136" target="_self" title="">https://forums.autodesk.com/t5/inventor-general-discussion/ilogic/td-p/2611136</a></p>
<p>So you could have an<strong> iLogic Rule</strong>&#0160;calling a <strong>VBA macro</strong> with parameters like this:&#0160;</p>
<pre>InventorVb.RunMacro(
  &quot;ApplicationProject&quot;, &#39; projectName
  &quot;Module1&quot;,               &#39; moduleName 
  &quot;MyMacro&quot;,              &#39; macroName
  &quot;Hello again!&quot;)         &#39; the first argument we pass to the VBA macro</pre>
<p>The above would call a&#0160;<strong>VBA</strong> macro from e.g. <strong>ApplicationProject</strong> &gt;&gt; <strong>Module1</strong>&#0160;</p>
<pre>Sub MyMacro(argument1 As String)
  Call MsgBox(argument1) &#39; &gt;&gt; would pop up with &quot;Hello again!&quot; 
End Sub</pre>
