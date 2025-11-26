---
layout: "post"
title: "AIMS 2012 login failed"
date: "2012-05-22 01:41:00"
author: "Daniel Du"
categories:
  - "AIMS 2012"
  - "Daniel Du"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/05/aims-2012-login-failed.html "
typepad_basename: "aims-2012-login-failed"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/daniel-du.html">Daniel Du</a></p>  <p>I installed AIMS 2012 in windows with IIS + .net binding, but I cannot login in MapGuide Studio or MapAdmin.</p>  <p>Many reasons may cause the failure of login of MapGuide server. Here is a checklist for your reference: </p>  <ol>   <li>Are your system is qualified according to <a href="http://images.autodesk.com/adsk/files/Autodesk_Infrastructure_Map_Server_2012_System_requirements.pdf" target="_blank">system requirement of AIMS2012</a>?&#160;&#160; </li>    <li>Is the MapGuide server services started and running?      <br />Start --&gt; run --&gt; “services.msc” to open Services console, and make sure “Infrastructure Map Server 2012” is started. </li>    <li>Are you using the correct user name and password? Is there anyone(may be others) changed the default user name and password without informing your?&#160; The default user name and password is(case is important):      <br />username: Administrator       <br />password: admin </li>    <li>Have you ever installed IIS correctly before you install AIMS 2012?      <br />Start --&gt; Control Panel --&gt; Programs and Features --&gt; Turn Windows features on or off(from left), check *all* items under Internet Information Services\World Wide Web Services\Application Development Features </li>    <li>Do you configure php correctly?      <br />Start --&gt; Run --&gt; &quot;regedit&quot; , open registry to check whether following key exists or not, if not, create one and reboot:       <br />HKEY_LOCAL_MACHINE\SOFTWARE\PHP\5.3.3\IniFilePath&#160; to C:\Program Files\Autodesk\Autodesk Infrastructure Web Server Extension 2012\Php </li>    <li>Are you using trail version? Is the trail version expired? Have you ever run ServerActivator.exe to activate you license?      <br />Run ServerActivator.exe to activate your product. </li> </ol>
