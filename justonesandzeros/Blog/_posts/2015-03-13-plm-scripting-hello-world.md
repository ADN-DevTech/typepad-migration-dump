---
layout: "post"
title: "PLM Scripting Hello World"
date: "2015-03-13 11:45:04"
author: "Michal Liu"
categories:
  - "PLM 360"
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2015/03/plm-scripting-hello-world.html "
typepad_basename: "plm-scripting-hello-world"
typepad_status: "Publish"
---

<p>Hi, I&#39;m Michal Liu, a developer on PLM 360. I will be showing you how to get the most out of PLM scripting.</p>
<p>PLM 360 scripting is a JavaScript-influenced language, which is customized to work with PLM. It gives users ability to extend the core functionalities or customize them to achieve various business goals. The language is designed for every user, not just for veteran programmers. If you do have some programming background, it will help you to grasp JavaScript quickly.&#0160; However, if your programming background is zero, you will still be able to write some simple but pragmatic code by reading the &quot;Help Guide&quot;. Other than the guide, you also can get the help from <a href="http://forums.autodesk.com/t5/plm-360-general-discussion/bd-p/705" target="_blank">PLM 360 General Discussion</a> forum and this blog.</p>
<p>As this is my first post, I am going to walk through the process to create and run a simple script code. In future posts, I will dive into some topics like script types, triggers, script chain, programming tabs and so on. For now, let&#39;s just build a &quot;hello world&quot; code.</p>
<p><strong>Prerequisite:</strong></p>
<ul>
<li>You need to be logged in as an administrator, otherwise you won&#39;t be able to create scripts.</li>
<li>Make sure you have a workspace <strong>W</strong> having a &quot;Single Line Text&quot; field named &quot;Title&quot;.</li>
<li>You also need &quot;Edit Items&quot; permission for workspace <strong>W</strong>.</li>
</ul>
<p><strong>Step 1: Write some code</strong></p>
<p>To create a script code, you need go to Administration -&gt; System Configuration -&gt; Scripting -&gt; click &quot;New Script&quot; button on the top left of the page.</p>
<ul>
<li>Leave &quot;Script Type&quot; as default type &quot;Action&quot;.</li>
<li>Give it a &quot;Unique Name&quot;. e.g. <strong>myActionScript</strong>. (The unique name must be between 3 and 50 characters.)</li>
<li>You can leave the &quot;Description&quot;, &quot;Imports&quot; and &quot;Enable Code Complete&quot; as they are for now.</li>
<li>Put the below text into &quot;Code&quot; and click Save button.</li>
</ul>
<p style="text-align: left; padding-left: 30px;">&#0160;&#0160;&#0160;&#0160; <em>&#0160;&#0160; item.TITLE = &quot;Hello World&quot;;</em></p>
<p style="text-align: left;"><img alt="PLMScriptHelloWorld_scriptCode" border="0" src="/assets/ScriptEditor.png" /></p>
<p style="text-align: left;"><strong>Step 2: Configure the script into workspace W</strong></p>
<p>Go to Administration -&gt; Workspace Manager -&gt;<strong> W</strong> -&gt; Behaviors. Select &quot;myActionScript&quot; for &quot;Script to run on demand&quot;. After the selection, another row of &quot;Script to run on demand&quot; pops up. It&#39;s okay. Click Save.</p>
<p><strong>Step 3: Run the script</strong></p>
<p>Go to an item&#39;s page from workspace <strong>W</strong>. Click the script icon; click Confirm in the popped up dialog.</p>
<p><br /><a class="asset-img-link" href="http://justonesandzeros.typepad.com/.a/6a0120a5728249970b01b7c760b96f970b-pi" style="display: inline;"><img alt="PLMScriptHelloWorld_workspaceBar" border="0" class="asset  asset-image at-xid-6a0120a5728249970b01b7c760b96f970b img-responsive" src="/assets/image_949c02.jpg" style="margin-left: auto; display: block; margin-right: auto;" title="PLMScriptHelloWorld_workspaceBar" /></a>If everything are correct so far. You will see a green bar saying &quot;Operation completed successfully&quot;, and now the Title field will be changed to &quot;Hello World&quot;.</p>
<p><img alt="PLM - Selenium - Parts Hello World" border="0" src="/assets/Completed.png" title="PLM - Selenium - Parts Hello World" /></p>
