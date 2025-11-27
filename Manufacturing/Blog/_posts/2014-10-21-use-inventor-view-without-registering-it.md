---
layout: "post"
title: "Use Inventor View without registering it"
date: "2014-10-21 07:43:25"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/10/use-inventor-view-without-registering-it.html "
typepad_basename: "use-inventor-view-without-registering-it"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>As pointed out in <a href="http://adndevblog.typepad.com/manufacturing/2012/05/inventor-view-control-is-not-registered-on-inventor-2012.html" target="_self">this blog post</a>, from <strong>Inventor 2012</strong> onwards <strong>Inventor</strong> does not register the <strong>View</strong> component.</p>
<p>If you do not want to or cannot register the component using the <a href="http://en.wikipedia.org/wiki/Regsvr32" target="_self">regsvr32</a> utility, then you can embed a <a href="http://msdn.microsoft.com/en-us/library/1w45z383(v=vs.110).aspx" target="_self">manifest</a> in your application instead. In this case however, you&#39;ll need to make sure that <strong>InventorViewCtrl.ocx</strong> is in the same folder as your application: you can either distribute this file with your application or copy the one installed with <strong>Inventor</strong> or <strong>Inventor View</strong>.</p>
<p>You can use the <a href="http://msdn.microsoft.com/en-us/library/aa375649(v=vs.85).aspx" target="_self">mt.exe</a> utility to get the manifest itself from <strong>VS Command Prompt</strong> run as <strong>Administrator</strong>:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d081877c970c-pi" style="display: inline;"><img alt="Mtexe" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d081877c970c image-full img-responsive" src="/assets/image_dc3de6.jpg" title="Mtexe" /></a></p>
<p>Then through the <strong>Project Settings</strong> you can get it embedded in your project:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c6f77b74970b-pi" style="display: inline;"><img alt="Projectsettings" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c6f77b74970b image-full img-responsive" src="/assets/image_f72594.jpg" title="Projectsettings" /></a></p>
<p>In case of <strong>Inventor 2015</strong> you still need the workaround even when using <strong>Registration Free</strong> method: <br /><a href="http://adndevblog.typepad.com/manufacturing/2014/10/using-inventor-view-from-64-and-32-bit-process.html" target="_self" title="">http://adndevblog.typepad.com/manufacturing/2014/10/using-inventor-view-from-64-and-32-bit-process.html</a>&#0160;&#0160;</p>
<p>Here is the sample code: <a href="https://github.com/adamenagy/InventorView-FileDisplay-.NET-4.0-RegFree" target="_self" title="">https://github.com/adamenagy/InventorView-FileDisplay-.NET-4.0-RegFree</a>&#0160;</p>
