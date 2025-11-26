---
layout: "post"
title: "Civil 3D Project Template for Visual Studio"
date: "2014-09-24 14:52:55"
author: "Augusto Goncalves"
categories:
  - ".NET"
  - "Augusto Goncalves"
  - "AutoCAD Civil 3D 2015"
original_url: "https://adndevblog.typepad.com/infrastructure/2014/09/civil-3d-project-template-for-visual-studio.html "
typepad_basename: "civil-3d-project-template-for-visual-studio"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/augusto-goncalves.html">Augusto Goncalves</a></p>
<p>This is a long time request: an easy way to create a Visual Studio project for Civil 3D using VB.NET or C#.</p>
<p>If you visit the <a href="http://www.autodesk.com/developautocad">AutoCAD Developer Center</a> you’ll see some AutoCAD .NET Wizards for both VB.NET and C#, which is very powerful and can be customized. Some time ago I started by doing some templates for <a href="http://thebuildingcoder.typepad.com/blog/2010/07/devtv-addin-templates.html">Revit</a>, which still in use, so now I decided to try one for Civil 3D, and here it is.</p>
<p>Basic benefits:</p>
<ul>
<li>Create a simple project, nothing else;</li>
<li>Configure all required references (AcMgd, AcCoreMgd, AcDbMgd, AecBaseMgd and AeccDbMgd) with respective reference paths;</li>
<li>Debug settings ready;</li>
</ul>
<p>Extra benefits:</p>
<ul>
<li>Create a basic PackageContents.xml with the basic required tags and attributes for Civil 3D;</li>
<li>On build, copy all the files (DLL and XML) to a newly created folder under Application Plugins folder;</li>
<li>On clean, remove the plugin folder from Application Plugins</li>
</ul>
<p>Cons:</p>
<ul>
<li>The path are hardcoded to 2015 release. If you are using a different version, you’ll need to open and change the paths and reference versions.</li>
</ul>
<p>To use it, simple download the packages below and place under [<span style="text-decoration: underline;">My Documents]\Visual Studio 2012\Templates\ProjectTemplates\Visual Basic\</span> folder for VB.NET or [<span style="text-decoration: underline;">My Documents]\Visual Studio 2012\Templates\ProjectTemplates\Visual C#\</span> folder for C#. <strong>Do not unzip the files</strong>.</p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b01b7c6e6f784970b img-responsive"><a href="http://adndevblog.typepad.com/files/civil2015template_vb.zip">VB.NET Template</a></span></p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b01b7c6e6f79b970b img-responsive"><a href="http://adndevblog.typepad.com/files/civil2015template_cs.zip">C# Template</a></span></p>
<p>With the template .ZIP file in place, you should see the following:</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb078c0c0c970d-pi"><img alt="new_project" border="0" height="135" src="/assets/image_a692b2.jpg" style="display: inline; border-width: 0px;" title="new_project" width="473" /></a></p>
<p><strong>Tip</strong>: you can organize it better by creating a subfolder “Autodesk” and placing the ZIP at this location.</p>
<p>Finally, I will be using these template at the AU Russia next week, if you are attending, come and say ‘hi’.</p>
<p><a href="http://www.autodeskuniversity.ru/"><img alt="au_russia" border="0" height="213" src="/assets/image_c0769a.jpg" style="display: inline; border-width: 0px;" title="au_russia" width="385" /></a></p>
