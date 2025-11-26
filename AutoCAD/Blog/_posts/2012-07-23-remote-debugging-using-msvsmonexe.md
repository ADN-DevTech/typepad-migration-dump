---
layout: "post"
title: "Remote debugging using msvsmon.exe"
date: "2012-07-23 02:38:00"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/remote-debugging-using-msvsmonexe.html "
typepad_basename: "remote-debugging-using-msvsmonexe"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I would like to debug my AddIn running on a virtual machine from my local machine. Could you list the steps I need to follow?</p>
<p><strong>Solution</strong></p>
<p>I think it does not really matter if you are trying to debug remotely between virtual machines or real ones, the procedure should be the same. I tested the below steps using two virtual machines: adam3596 is the one that I'm debugging from (local computer), and adam3597 is the one where the application that loads my addin is running (remote computer). Also, I'm testing with a .NET AddIn that is loaded into AutoCAD MEP 2009.</p>
<ol>
<li>We'll be usig Windows Authentication to access one pc from the other, and for this we need to make sure that both the local and remote pc has a local account with the same name and password <br />&nbsp;</li>
<li>As msvsmon.exe would warn us about it as well, we need to set the local policy on both computers as follows: <br /><strong>Local Security Settings &gt; Security Settings &gt; Local Policies &gt; Security Options &gt; Network access: Sharing and security model for local accounts = Classic - local users authenticate as themselves</strong><br /> &nbsp;<br /><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01774370ee20970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01774370ee20970d image-full" title="Remote_policy" src="/assets/image_910385.jpg" border="0" alt="Remote_policy" /></a><br /> &nbsp;</li>
<li>We need to copy msvsmon.exe and its related files over to the remote pc or virtual machine. Just copy the whole Remote Debugger folder to the remote machine. In case of Visual Studio 2008 you'll find the necessary folder under <br /><strong>C:\Program Files\Microsoft Visual Studio 9.0\Common7\IDE\Remote Debugger<br />&nbsp;</strong></li>
<li>Set up a folder that is visible from both computers and use that as your project's build folder<br />&nbsp;<br /> <a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01774370efbe970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01774370efbe970d image-full" title="Remote_settings-build" src="/assets/image_889061.jpg" border="0" alt="Remote_settings-build" /></a><br /></li>
<li>In some cases I had issues with attaching to the remote process - e.g. AutoCAD simply froze - but if I started the exe from Visual Studio then all worked fine. In the project's debug settings use the remote pc and set the path of the executable you want to start<br /> &nbsp; <br /><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01676895f8f3970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01676895f8f3970b image-full" title="Remote_settings-debug" src="/assets/image_228573.jpg" border="0" alt="Remote_settings-debug" /><br /></a></li>
<li>If you also want to set the working folder then it's better to avoid using network names that are not mapped. If you want to use a network folder just right-click on the network drive in Explorer and choose <strong>Map Network Drive...</strong> and then use the mapped name, e.g. <strong>Z:\VMwareShare</strong> instead of <strong>\\.host\Shared Folders\VMwareShare<br />&nbsp;</strong></li>
<li>Start <strong>msvsmon.exe</strong> on the remote pc<br />&nbsp;<br /> <a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01774370f1b7970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01774370f1b7970d image-full" title="Remote_debugging-remote" src="/assets/image_344412.jpg" border="0" alt="Remote_debugging-remote" /></a><br /></li>
<li>Start debugging your project on the local pc and NETLOAD your addin in AutoCAD on the remote pc<br />&nbsp;<br /><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01676895fb4a970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01676895fb4a970b image-full" title="Remote_debugging-local" src="/assets/image_403195.jpg" border="0" alt="Remote_debugging-local" /></a></li>
</ol>
<p>You'll find several articles on the net about remote debugging if you simply search for msvsmon.exe.</p>
