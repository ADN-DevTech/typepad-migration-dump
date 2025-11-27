---
layout: "post"
title: "Localize Inventor .NET AddIn"
date: "2013-05-12 19:01:26"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/05/localize-inventor-net-addin.html "
typepad_basename: "localize-inventor-net-addin"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>There are three things related to this:<br />1) Localizing the AddIn registration information that shows up in the <strong>Add-In Manager</strong> dialog<br />2) Localizing resources used by the Add-In assembly&#0160;<br />3) Localizing data for the installer of your <a href="http://apps.exchange.autodesk.com/en" target="_self">Autodesk Exchange Apps</a> store AddIn</p>
<p>In order to test the localization you would need a localized version of Inventor. You can download language packs from the Autodesk site for that:&#0160;<a href="http://usa.autodesk.com/adsk/servlet/item?siteID=123112&amp;id=21362325&amp;linkID=9242019" target="_self" title="Autodesk Inventor 2014 Language Packs">Autodesk Inventor 2014 Language Packs</a></p>
<p>Some information about this in the online help:&#0160;<a href="http://wikihelp.autodesk.com/enu?adskContextId=LANGUAGEPACKS&amp;product=Installation_Help&amp;release=2014&amp;language=enu" target="_self" title="Installing Language Packs">Installing Language Packs</a></p>
<p><strong>Localizing the AddIn registration information</strong></p>
<p>This basically means adding translated texts inside the <strong>*.addin</strong> file that provides information about the AddIn. You can simply add a <strong>Language</strong> attribute to the tags that you want to localize. You should list the language spacific resources first and then the default resource which does not contain the <strong>Language</strong> tag. Like this:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">&#0160; &lt;</span><span style="color: #a31515;">DisplayName</span><span style="color: blue;"> </span><span style="color: red;">Language</span><span style="color: blue;">=</span>&quot;<span style="color: blue;">1046</span>&quot;<span style="color: blue;">&gt;</span>Teste de localização<span style="color: blue;">&lt;/</span><span style="color: #a31515;">DisplayName</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">&#0160; &lt;</span><span style="color: #a31515;">DisplayName</span><span style="color: blue;">&gt;</span>Localization Test<span style="color: blue;">&lt;/</span><span style="color: #a31515;">DisplayName</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">&#0160; &lt;</span><span style="color: #a31515;">Description</span><span style="color: blue;"> </span><span style="color: red;">Language</span><span style="color: blue;">=</span>&quot;<span style="color: blue;">1046</span>&quot;<span style="color: blue;">&gt;</span>Un simples AddIn para testar a localização<span style="color: blue;">&lt;/</span><span style="color: #a31515;">Description</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">&#0160; &lt;</span><span style="color: #a31515;">Description</span><span style="color: blue;">&gt;</span>A simple AddIn to test localization<span style="color: blue;">&lt;/</span><span style="color: #a31515;">Description</span><span style="color: blue;">&gt;</span></p>
</div>
<p>In this case I added a Brazilian Portuguese version of the <strong>DisplayName</strong> and <strong>Description</strong> tags. One way to get the number that you need to use for the tag is by checking the <strong>LCID Dec</strong> column on this site: <a href="http://msdn.microsoft.com/en-gb/goglobal/bb964664.aspx" target="_self" title="Locale IDs Assigned by Microsoft">Locale IDs Assigned by Microsoft</a>&#0160;</p>
<p>Now when I check the AddIn information then depending on the language version of Inventor I started I get the appropriate text:</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017eeb189a1a970d-pi" style="display: inline;"><img alt="Addins" border="0" class="asset  asset-image at-xid-6a0167607c2431970b017eeb189a1a970d image-full" src="/assets/image_519fe3.jpg" title="Addins" /></a></p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901c1b34b9970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Local-enpt" class="asset  asset-image at-xid-6a0167607c2431970b01901c1b34b9970b" src="/assets/image_33c298.jpg" style="width: 450px;" title="Local-enpt" /></a></p>
<p><strong>Localizing resources used by the Add-In assembly</strong>&#0160;</p>
<p>You can take advantage of the .NET Framework localization to implement this. There are many articles that discuss this topic, but here are two of them:<br />- <a href="http://msdn.microsoft.com/en-GB/library/70s77c20(v=vs.71).aspx" target="_self" title="Retrieving Resources in Satellite Assemblies">Retrieving Resources in Satellite Assemblies</a><br />-&#0160;<a href="http://msdn.microsoft.com/en-us/library/21a15yht.aspx" target="_self" title="Creating Satellite Assemblies for Desktop Apps">Creating Satellite Assemblies for Desktop Apps</a></p>
<p>To test things I simply created a new C# AddIn using the <strong>Inventor AddIn Wizard</strong> that is part of the SDK that comes with the Inventor 2014 installation and then followed the steps that are also listed in this article:&#0160;<a href="http://through-the-interface.typepad.com/through_the_interface/2009/06/registering-autocad-commands-with-localized-names-using-net.html" target="_self" title="Registering AutoCAD commands with localized names using .NET">Registering AutoCAD commands with localized names using .NET</a>, and added the strings I wanted to use: <strong>TestString</strong> and <strong>TestCommand</strong>.</p>
<p>Then I created a button definition and a form, both of which rely on localized content. In case of the form I just modified its&#0160;<strong>InitializeComponent</strong> function so that the assigned strings are coming from the resource files and did the same when creating the <strong>ButtonDefinition</strong> object.&#0160;</p>
<p>Visual Studio automatically created a wrapper class called <strong>Resources</strong> for my resources which makes it very easy to access the strings I want to use - e.g. when creating the button definition:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; m_button = mgr.ControlDefinitions.AddButtonDefinition(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <strong><span style="color: #2b91af;">Resources</span>.TestCommand</strong>, <span style="color: #a31515;">&quot;Autodesk:Adam:TestCommand&quot;</span>, </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: #2b91af;">CommandTypesEnum</span>.kNonShapeEditCmdType);</p>
</div>
<p>Here is the result when running the AddIn in the English and the Brazilian version of Inventor:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901c1b51e3970b-pi" style="display: inline;"><img alt="Form-enpt" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01901c1b51e3970b image-full" src="/assets/image_9bb3e4.jpg" title="Form-enpt" /></a></p>
<p>You can download the test project from here: 
<span class="asset  asset-generic at-xid-6a0167607c2431970b019102117fa6970c"><a href="http://adndevblog.typepad.com/files/localizationtest.zip">Download LocalizationTest</a></span>&#0160;</p>
<p><strong>Localizing data for the installer of your Autodesk Exchange Apps store AddIn</strong></p>
<p>It&#39;s the same for all AddIn&#39;s on the Exhange Apps store: AutoCAD, Inventor, etc, so you can just follow the information provided in this blog post:&#0160;<a href="http://adndevblog.typepad.com/autocad/2013/05/about-exchange-app-installer-automatic-localization.html" target="_self" title="About Exchange App Installer automatic Localization">About Exchange App Installer automatic Localization</a></p>
