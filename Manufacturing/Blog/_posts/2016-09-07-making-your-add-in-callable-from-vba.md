---
layout: "post"
title: "Making your add-in callable from VBA"
date: "2016-09-07 05:13:19"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/09/making-your-add-in-callable-from-vba.html "
typepad_basename: "making-your-add-in-callable-from-vba"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If you want to create your <strong>add-in</strong> in a way that its functionality can also be accessed by other programs (e.g. from <strong>VBA</strong>) then there are two ways to do it:</p>
<p><strong>a)&#0160;Use ControlDefinition object</strong><br />Most <strong>add-ins</strong> implement <strong>ControlDefinition</strong> objects, which are hooked up to controls in the <strong>Inventor User Interface</strong>. When the user clicks that control (mainly buttons) then your code can run to do its thing. These <strong>ControlDefintion</strong> objects can also be accessed by other programs, just like the ones <strong>Inventor</strong> exposes.&#0160;Once you found the <strong>ControlDefinition</strong> object you need you can call its <strong>Execute</strong> or <strong>Execute2</strong> method in order to run it:<br /><a href="http://modthemachine.typepad.com/my_weblog/2009/03/running-commands-using-the-api.html">Running Commands Using the API<br /></a>&amp;<br /><a href="http://adndevblog.typepad.com/manufacturing/2015/02/execute-vs-execute2-of-controldefinition.html">Execute vs Execute2 of ControlDefinition</a></p>
<p>You can also send parameters to a command in the form of a string. Your <strong>add-in</strong> then could call&#0160;<strong>PeekPrivateEvent</strong> to access that data:&#0160;<br /><a href="http://adndevblog.typepad.com/manufacturing/2013/01/provide-command-parameters-using-postprivateevent.html">Provide command parameters using PostPrivateEvent</a></p>
<p>Just to illustrate the solution:</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a542ce200d-pi" style="display: inline;"><img alt="ExecuteInventorCommand" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a542ce200d image-full img-responsive" src="/assets/image_1629ef.jpg" title="ExecuteInventorCommand" /></a></p>
<p><strong>b)&#0160;Implement Automation property</strong><br />Your <strong>add-in</strong> could also implement the <strong>Automation</strong> property of the <strong>ApplicationAddInServer</strong> class as described in this blog post:<br /><a href="http://adndevblog.typepad.com/manufacturing/2012/07/connect-to-an-inventor-add-in-an-external-application.html">Connect to an Inventor add-in an external application</a></p>
<p>Through the <strong>Automation</strong> property you can expose an <strong>object</strong> that can have any <strong>functions</strong> and <strong>properties</strong>&#0160;that you want.</p>
<p>Note that both solutions work even if <strong>Inventor</strong> is invisible.</p>
