---
layout: "post"
title: "Modify Ribbon and Menu items"
date: "2014-01-15 05:22:51"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/01/modify-ribbon-and-menu-items.html "
typepad_basename: "modify-ribbon-and-menu-items"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Just like in other <strong>Autodesk</strong> products, in <strong>Inventor</strong> as well the same library is used to present a <strong>Ribbon</strong> interface: <strong>AdWindows.dll</strong><br />This provides a public API under the namespace&#0160;<strong>Autodesk.Windows</strong></p>
<p>Here is an example of using it inside <strong>Revit</strong>: <a href="http://thebuildingcoder.typepad.com/blog/2011/02/pimp-my-autocad-or-revit-ribbon.html" target="_self" title="">http://thebuildingcoder.typepad.com/blog/2011/02/pimp-my-autocad-or-revit-ribbon.html</a></p>
<p>It&#39;s not a fully supported approach though, since some functions like adding a command button to the <strong>Ribbon</strong> are not supported - it would be difficut to properly hook up a control created this way to an Inventor command. For that you would need to use the <strong>Inventor API</strong>.<br />So when using it, take care and make sure that everything is working as expected.</p>
<p>E.g. the following <strong>VB.NET</strong> code could be used to hide the <strong>Options</strong> button in the <strong>Application Menu</strong>:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fc3f27ce970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="AdWindows" class="asset  asset-image at-xid-6a0167607c2431970b01a3fc3f27ce970b" src="/assets/image_90a98b.jpg" style="width: 450px;" title="AdWindows" /></a></p>
<pre>&#39; 1) Need to reference AdWindows.dll from
&#39; &lt;Inventor Install Folder&gt;\bin
&#39; 2) Set it to &quot;Copy Local&quot; = &quot;False&quot;
Dim menu As Autodesk.Windows.ApplicationMenu = 
  Autodesk.Windows.ComponentManager.ApplicationMenu
menu.OptionsButton.IsVisible = False </pre>
<p>Note: when referencing <strong>AdWindows.dll</strong> the project will require other dependencies as well that the compiler will warn you about. You&#39;ll simply have to add a reference to those <strong>Windows .NET assemblies</strong> as well: <strong>PresentationCore</strong>, <strong>PresentationFramework</strong>, <strong>WindowsBase</strong> and <strong>System.Xaml</strong></p>
