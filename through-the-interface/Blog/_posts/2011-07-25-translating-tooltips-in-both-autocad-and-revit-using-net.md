---
layout: "post"
title: "Translating tooltips in both AutoCAD and Revit using .NET"
date: "2011-07-25 22:50:05"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Notification / Events"
  - "Revit"
  - "Translation"
  - "XML"
original_url: "https://www.keanw.com/2011/07/translating-tooltips-in-both-autocad-and-revit-using-net.html "
typepad_basename: "translating-tooltips-in-both-autocad-and-revit-using-net"
typepad_status: "Publish"
---

<p>A quick update, today. Last week Jeremy posted <a href="http://thebuildingcoder.typepad.com/blog/2011/07/translate-revit-tooltips.html" target="_blank">a migrated version</a> of the <a href="http://through-the-interface.typepad.com/through_the_interface/2011/07/caching-translations-of-autocad-tooltips-using-net.html" target="_blank">TransTips plug-in for AutoCAD</a>, this time working inside Revit. Thanks to the shared use of <em>AdWindows.dll</em> in both products, this was actually really easy.</p>
<p>After this initial version, it made sense to refactor the code to have a core, shared file (not necessarily a separate DLL component – sharing source can give many benefits for smaller projects, such as this) used to build plugin DLLs for both AutoCAD and Revit.</p>
<p>Here’s the result: <a href="http://through-the-interface.typepad.com/files/TransTips-AutoCAD_Revit.zip" target="_blank">a single solution which will build TransTips DLLs for AutoCAD and Revit</a> (including built versions of the DLLs). I won’t go into the specific details, here, although I did do as promised with regards to checking for network availability: if the app doesn’t manage to contact the Bing Translate service, it continues to work in offline mode, picking up translations from local XML files, where available. It’s also possible to specify this mode by default (with a very minor code change).</p>
<p>Otherwise the code itself should be reasonably straightforward. I used the “Add Existing Item –&gt; Add Link” capability in VS 2010 to maintain a common file across both projects in the solution, which was new to me, and gave me exactly the project structure I was after.</p>
<p>In the next post in this series: support for Inventor (that one’s for you, Alex ;-) and additional work to share a common, WPF-based language selection UI across all the projects. At some point, beyond, I’ll also look at an editing capability for the XML data (also shared), and perhaps even some true componentization. We’ll see about that, though – in many ways I quite like the current approach of building a single DLL per-product.</p>
