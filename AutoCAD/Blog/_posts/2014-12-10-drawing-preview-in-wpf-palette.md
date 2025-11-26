---
layout: "post"
title: "Drawing preview in WPF palette"
date: "2014-12-10 08:51:21"
author: "Balaji"
categories:
  - ".NET"
  - "2015"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "WPF"
original_url: "https://adndevblog.typepad.com/autocad/2014/12/drawing-preview-in-wpf-palette.html "
typepad_basename: "drawing-preview-in-wpf-palette"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>As you may already know, the BlockView .Net sample demonstrates the use of Graphics system to preview a drawing in a Windows form. The migrated sample that works with AutoCAD 2015 is available <a href="http://adndevblog.typepad.com/autocad/2014/04/graphic-changes-in-autocad-2015.html">here</a>.</p>
<p>To get the preview displayed inside a WPF user control hosted in an AutoCAD palette, poses a small problem.&nbsp;In that sample, as the "GraphicsManager.CreateAutoCADDevice" requires a Window handle to display the graphics, there is no direct way to get the display in a WPF user control. In a WPF window, the controls are managed by the top level window and there are no individual handles assigned separately for the controls.&nbsp;Because of this difference, the only way to get this working in WPF is to host the Windows user control that displays the preview in a&nbsp;WindowsFormsHost.</p>
<p>The attached sample is a modified version of the BlockView .Net sample that demonstrates this. 
<span class="asset  asset-generic at-xid-6a0167607c2431970b01bb07c4e172970d img-responsive"><a href="http://adndevblog.typepad.com/files/blockview.net-1.zip">Download Modified BlockView.NET</a></span>
</br>
To try it, netload the dll and run the "bviewpal" command. To preview a drawing, click on the button in the palette and browse to a drawing.</p></br>
<p>Here is a screenshot of the WPF palette previewing a drawing :</p>
<a class="asset-img-link"  style="float: left;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07c11388970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb07c11388970d img-responsive" alt="PreviewPalette" title="PreviewPalette" src="/assets/image_87404.jpg" style="margin: 0px 5px 5px 0px;" /></a>
