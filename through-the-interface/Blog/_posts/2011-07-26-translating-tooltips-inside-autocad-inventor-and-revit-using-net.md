---
layout: "post"
title: "Translating tooltips in AutoCAD, Inventor and Revit using .NET"
date: "2011-07-26 15:29:28"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Inventor"
  - "Revit"
  - "Translation"
  - "WPF"
original_url: "https://www.keanw.com/2011/07/translating-tooltips-inside-autocad-inventor-and-revit-using-net.html "
typepad_basename: "translating-tooltips-inside-autocad-inventor-and-revit-using-net"
typepad_status: "Publish"
---

<p>Once again, it turned out to be pretty straightforward to add Inventor support to our TransTips application. We now have <a href="http://through-the-interface.typepad.com/files/TransTips-AutoCAD_Inventor_Revit.zip" target="_blank">a single solution which builds plugins for AutoCAD, Inventor and Revit</a>. The plugins share a common translation and caching engine as well as a WPF graphical user interface for selecting languages.</p>  <p>Here’s a demonstration of the various plugins in action:</p> <iframe allowfullscreen="allowfullscreen" frameborder="0" height="296" src="http://www.youtube.com/embed/7YeM1a8rJQo" width="475"></iframe>  <p>I’ve fixed the right-to-left issue highlighted in the video, incidentally.</p>  <p>The next steps are to implement an in-product editing capability and then perhaps to support 3ds Max. One great aspect of this project is that I’m getting to learn a little more about developing for products other than AutoCAD, which is fun. :-)</p>
