---
layout: "post"
title: "Automatic loading of .NET modules"
date: "2006-09-11 23:27:43"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Runtime"
original_url: "https://www.keanw.com/2006/09/automatic_loadi.html "
typepad_basename: "automatic_loadi"
typepad_status: "Publish"
---

<p>Clearly it’s far from ideal to ask your users to load your application modules manually into AutoCAD whenever they need them, so over the years a variety of mechanisms have been put in place to enable automatic loading of applications – acad.lsp, acad.ads, acad.rx, the Startup Suite, to name but a few.</p>
<p>The most elegant way to auto-load both ObjectARX and .NET applications is the demand-loading mechanism. This mechanism is based on information stored in the Registry describing the conditions under which modules should be loaded and how to load them.</p>
<p>Demand loading is fairly straightforward and well documented in the ObjectARX Developer’s Guide (look under “demand loading applications”).</p>
<p>Essentially the information can be stored in one of two places: under HKEY_LOCAL_MACHINE or under HKEY_CURRENT_USER. The decision on where to place the information will depend on a few things – mainly whether the application is to be shared across all users but also whether your application installer has the privileges to write to HKEY_LOCAL_MACHINE or not.</p>
<p>It’s not really the place to talk about the pros and cons of these two locations – for the sake of simplicity the following examples show writing the information to HKEY_CURRENT_USER. Let’s start by looking at the root location for the demand-loading information:</p>
<p><em>HKEY_CURRENT_USER\Software\Autodesk\AutoCAD\R17.0\ACAD-5001:409\Applications</em></p>
<p>Most of this location is logical enough (to humans), although the “ACAD-5001:409” piece needs a bit of explanation. This number has evolved over the releases, but right now 5001 means AutoCAD 2007 (it was 4001 for AutoCAD 2006, 301 for AutoCAD 2005 and 201 for AutoCAD 2004), and 409 is the “locale” corresponding to English.</p>
<p>A more complete description of the meaning of this key is available to ADN members at:</p>
<p><a href="http://adn.autodesk.com/adn/servlet/devnote?siteID=4814862&amp;id=5413196&amp;linkID=4900509">Registry values for ProductID and LocaleID for AutoCAD and the vertical products</a></p>
<p>There are two common times to load a .NET application: on AutoCAD startup and on command invocation. ObjectARX applications might also be loaded on object detection, but as described in <a href="http://through-the-interface.typepad.com/through_the_interface/2006/09/custom_objects_.html">a previous post</a> this is not something that is currently available to .NET applications.</p>
<p>Let’s take the two common scenarios and show typical settings for the test application shown in <a href="http://through-the-interface.typepad.com/through_the_interface/2006/09/optimizing_the_.html">this previous post</a>.</p>
<p><strong>Loading modules on AutoCAD startup</strong></p>
<p>Under the root key (<em>HKEY_CURRENT_USER\Software\Autodesk\AutoCAD\R17.0\ACAD-5001:409\Applications</em>) there is some basic information needed for our application. Firstly, you need to create a key just for our application: in this case I’ve used “MyTestApp” (as a rule it is recommended that professional software vendors prefix this string with their Registered Developer Symbol (RDS), which can be logged <a href="http://usa.autodesk.com/adsk/servlet/index?siteID=123112&amp;id=1075006">here</a>, but for in-house development this is not necessary – just avoid beginning the key with “Acad” :-).</p>
<p>Under our application key (<em>HKEY_CURRENT_USER\Software\Autodesk\AutoCAD\R17.0\ACAD-5001:409\Applications\MyTestApp</em>), we then create a number of values:</p>
<ul>
<li>DESCRIPTION    A string value describing the purpose of the module</li>
<li>LOADCTRLS      A DWORD (numeric) value describing the reasons for loading the app</li>
<li>MANAGED         Another DWORD that should be set to &quot;1&quot; for .NET modules</li>
<li>LOADER            A string value containing the path to the module</li>
</ul>
<p>The interesting piece is the LOADCTRLS value – the way to encode this is described in detail in the ObjectARX Developer’s Guide, but to save time I’ll cut to the chase: this needs to have a value of &quot;2&quot; for AutoCAD to load the module on startup.</p>
<p>Here&#39;s a sample .reg file:</p>
<div style="font-size: 8pt; background: white; color: black; font-family: Courier New;">
<p style="font-size: 8pt; margin: 0px;">Windows Registry Editor Version 5.00</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160;</p>
<p style="font-size: 8pt; margin: 0px;">[HKEY_CURRENT_USER\Software\Autodesk\AutoCAD\R17.0\ACAD-5001:409\Applications\MyTestApplication]</p>
<p style="font-size: 8pt; margin: 0px;">&quot;DESCRIPTION&quot;=&quot;Kean&#39;s test application&quot;</p>
<p style="font-size: 8pt; margin: 0px;">&quot;LOADCTRLS&quot;=dword:00000002</p>
<p style="font-size: 8pt; margin: 0px;">&quot;MANAGED&quot;=dword:00000001</p>
<p style="font-size: 8pt; margin: 0px;">&quot;LOADER&quot;=&quot;C:\\My Path\\MyTestApp.dll&quot;</p>
<p style="font-size: 8pt; margin: 0px;">&#0160;</p>
</div>
<p>After merging it into the Registry, here&#39;s what happens when you launch AutoCAD:</p>
<div style="font-size: 8pt; background: white; color: black; font-family: Courier New;">
<p style="font-size: 8pt; margin: 0px;">Regenerating model.</p>
<p style="font-size: 8pt; margin: 0px;">Initializing - do something useful.</p>
<p style="font-size: 8pt; margin: 0px;">AutoCAD menu utilities loaded.</p>
<p style="font-size: 8pt; margin: 0px;">Command: <span style="color: red;">tst</span></p>
<p style="font-size: 8pt; margin: 0px;">This is the TST command.</p>
<p style="font-size: 8pt; margin: 0px;">Command:</p>
</div>
<p><strong>Loading modules on command invocation</strong></p>
<p>To do this we need to add a little more information into the mix.</p>
<p>Firstly we need to change the value of LOADCTRLS to &quot;12&quot; (or &quot;c&quot; in hexadecimal), which is actually a combination of 4 (which means &quot;on command invocation&quot;) and 8 (which means &quot;on load request&quot;). For people that want to know the other flags that can be used, check out rxdlinkr.h in the inc folder of the ObjectARX SDK.</p>
<p>Secondly we need to add a couple more keys, to contain information about our commands and command-groups.</p>
<p>Beneath a &quot;Commands&quot; key (<em>HKEY_CURRENT_USER\Software\Autodesk\AutoCAD\R17.0\ACAD-5001:409\Applications\MyTestApplication\Commands</em>), we&#39;ll create as many string values as we have commands, each with the name of the &quot;global&quot; command name, and the value of the &quot;local&quot; command name. As well as the &quot;TST&quot; command, I&#39;ve added one more called &quot;ANOTHER&quot;.</p>
<p>Beneath a &quot;Groups&quot; key (<em>HKEY_CURRENT_USER\Software\Autodesk\AutoCAD\R17.0\ACAD-5001:409\Applications\MyTestApplication\Groups</em>), we&#39;ll do the same for the command-groups we&#39;ve registered our commands under (I used the default CommandMethod attribute that doesn&#39;t mention a group name, so this is somewhat irrelevant for our needs - I&#39;ll use &quot;ASDK_CMDS&quot; as an example, though).</p>
<p>Here&#39;s the the updated .reg file:</p>
<div style="font-size: 8pt; background: white; color: black; font-family: Courier New;">
<p style="font-size: 8pt; margin: 0px;">Windows Registry Editor Version 5.00</p>
<p style="font-size: 8pt; margin: 0px;">&#0160;</p>
<p style="font-size: 8pt; margin: 0px;">[HKEY_CURRENT_USER\Software\Autodesk\AutoCAD\R17.0\ACAD-5001:409\Applications\MyTestApplication]</p>
<p style="font-size: 8pt; margin: 0px;">&quot;DESCRIPTION&quot;=&quot;Kean&#39;s test application&quot;</p>
<p style="font-size: 8pt; margin: 0px;">&quot;LOADCTRLS&quot;=dword:0000000c</p>
<p style="font-size: 8pt; margin: 0px;">&quot;MANAGED&quot;=dword:00000001</p>
<p style="font-size: 8pt; margin: 0px;">&quot;LOADER&quot;=&quot;C:\\My Path\\MyTestApp.dll&quot;</p>
<p style="font-size: 8pt; margin: 0px;">&#0160;</p>
<p style="font-size: 8pt; margin: 0px;">[HKEY_CURRENT_USER\Software\Autodesk\AutoCAD\R17.0\ACAD-5001:409\Applications\MyTestApplication\Commands]</p>
<p style="font-size: 8pt; margin: 0px;">&quot;TST&quot;=&quot;TST&quot;</p>
<p style="font-size: 8pt; margin: 0px;">&quot;ANOTHER&quot;=&quot;ANOTHER&quot;</p>
<p style="font-size: 8pt; margin: 0px;">[HKEY_CURRENT_USER\Software\Autodesk\AutoCAD\R17.0\ACAD-5001:409\Applications\MyTestApplication\Groups]</p>
<p style="font-size: 8pt; margin: 0px;">&quot;ASDK_CMDS&quot;=&quot;ASDK_CMDS&quot;</p>
</div>
<p>And here&#39;s what happens when we launch AutoCAD this time, and run the tst command. You see the module is only loaded once the command is invoked:</p>
<div style="font-size: 8pt; background: white; color: black; font-family: Courier New;">
<p style="font-size: 8pt; margin: 0px;">Regenerating model.</p>
<p style="font-size: 8pt; margin: 0px;">AutoCAD menu utilities loaded.</p>
<p style="font-size: 8pt; margin: 0px;">Command: <span style="color: red;">tst</span></p>
<p style="font-size: 8pt; margin: 0px;">Initializing - do something useful.This is the TST command.</p>
<p style="font-size: 8pt; margin: 0px;">Command:</p>
</div>
