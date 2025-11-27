---
layout: "post"
title: "Execute vs Execute2 of ControlDefinition"
date: "2015-02-03 15:50:30"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/02/execute-vs-execute2-of-controldefinition.html "
typepad_basename: "execute-vs-execute2-of-controldefinition"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>This <a href="http://modthemachine.typepad.com/my_weblog/2009/03/running-commands-using-the-api.html" target="_self">blog post</a>&#39;s <strong>Command Execution Behavior</strong> section already talks about this but might be worth mentioning it again and providing some more details.</p>
<p>There are three ways to run an existing command: <strong>Execute</strong>, <strong>Execute2(<strong>Synchronous=</strong>False)</strong> and <strong>Execute2(<strong>Synchronous=</strong>True)</strong>.</p>
<p>If we ran the same command that was also used in the other article in three different ways then we would observe the following behaviour:</p>
<pre>Sub TestControlDefinitionExecute()
  Dim cm As CommandManager
  Set cm = ThisApplication.CommandManager
  
  Dim cds As ControlDefinitions
  Set cds = cm.ControlDefinitions
  
  Dim cd As ControlDefinition
  Set cd = cds(&quot;AssemblyPlaceComponentCmd&quot;)
  
  Debug.Print &quot;Before Execute&quot;
  
  Call cd.Execute
  &#39;Call cd.Execute2(False)
  &#39;Call cd.Execute2(True)
  
  Debug.Print &quot;After Execute&quot;
End Sub</pre>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0ce5c34970c-pi" style="display: inline;"><img alt="Execute" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0ce5c34970c image-full img-responsive" src="/assets/image_5e6dc5.jpg" title="Execute" /></a><br />So <strong>Execute2(Synchronous=False)</strong> is fully <strong>asynchronous</strong>, it does not wait for dialogs either, whereas <strong>Execute()</strong> does, and only returns once the dialog got dismissed. <strong>Execute2(Synchronous=True)</strong> is fully <strong>synchronous</strong> as it waits for the command to finish completely. <br />In case of commands (e.g.&#0160;AppFileOpenCmd)&#0160;which show a dialog but have no interactive bits like moving a component around in the document view, the&#0160;<strong>Execute()</strong> and <strong>Execute2(True)</strong>&#0160;functions return at the same time, right after the dialog got dismissed.&#0160;</p>
