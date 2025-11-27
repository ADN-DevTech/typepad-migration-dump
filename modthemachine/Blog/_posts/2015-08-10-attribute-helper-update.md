---
layout: "post"
title: "Attribute Helper Update"
date: "2015-08-10 17:34:06"
author: "Adam Nagy"
categories:
  - "Attributes"
  - "Brian"
  - "Inventor"
  - "Utilities"
original_url: "https://modthemachine.typepad.com/my_weblog/2015/08/attribute-helper-update.html "
typepad_basename: "attribute-helper-update"
typepad_status: "Publish"
---

<p>Quite a few years ago I wrote a little utility that lets you create, view, and edit attributes within a document.&#0160; If you’re relatively new to the Inventor API and are wondering what attributes are, here’s a <a href="http://modthemachine.typepad.com/my_weblog/2009/07/introduction-to-attributes.html">post giving a quick introduction</a>.&#0160;</p>
<p>I’ve used the utility quite a bit over the years and haven’t had any problems but someone recently reported a problem they were having and I had a hard time believing that the problem could exist.&#0160; I was finally able to reproduce the problem and found out it could be a fairly common problem, it’s just that I happened to always use a certain workflow that avoided it.&#0160; That’s why it’s always good to get someone else to help test your programs. <img alt="Smile" class="wlEmoticon wlEmoticon-smile" src="/assets/image_991054.jpg" />&#0160;</p>
<p>I’ve fixed the problem and have a new version available.&#0160; If you already have a version of “Attribute Helper” installed, you should first uninstall the version you have, which you can do using the standard Windows uninstall utility where you should see “Attribute Helper” listed as one of the installed programs, as shown below.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b8d146b5ef970c-pi"><img alt="UninstallAttributeHelper" border="0" height="349" src="/assets/image_399286.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin-left: auto; display: block; padding-right: 0px; margin-right: auto; border: 0px;" title="UninstallAttributeHelper" width="450" /></a></p>
<p><br />Once it’s uninstalled, you can install the <a href="http://modthemachine.typepad.com/AttributeHelperSetup.exe">new version of Attribute Helper</a>.&#0160; There is some help documentation delivered with the utility with instructions on how to use it and you can quickly determine if it was installed correctly by checking to see if the Attribute Helper command is now available in the Tools panel when you have a document open.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b8d146b5f3970c-pi"><img alt="AttributeHelperCommand" border="0" height="70" src="/assets/image_464362.jpg" style="background-image: none; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border: 0px;" title="AttributeHelperCommand" width="450" /></a></p>
<p><br />And when you run the Attribute Helper command, you should see the dialog below with the version displayed being “2.4”, as shown below.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b8d146b5f9970c-pi"><img alt="AttributeHelper2_4" border="0" height="500" src="/assets/image_650853.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin-left: auto; display: block; padding-right: 0px; margin-right: auto; border: 0px;" title="AttributeHelper2_4" width="350" /></a></p>
<p>&#0160;</p>
<p>I’ve also made the <a href="https://github.com/brianekins/AttributeHelper">source code</a> available on <a href="https://github.com/">GitHub</a></p>
<p>As always, please let me know if you find any problems or have suggestions for improvement.</p>
<p>-Brian</p>
