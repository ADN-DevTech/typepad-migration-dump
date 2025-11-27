---
layout: "post"
title: "October&rsquo;s Plugin of the Month: BrowsePhotosynth for AutoCAD"
date: "2010-10-01 22:37:04"
author: "Kean Walmsley"
categories:
  - "AU"
  - "AutoCAD"
  - "AutoCAD .NET"
  - "F#"
  - "Plugin of the Month"
  - "Point clouds"
  - "Reality capture"
original_url: "https://www.keanw.com/2010/10/octobers-plugin-of-the-month-browsephotosynth-for-autocad.html "
typepad_basename: "octobers-plugin-of-the-month-browsephotosynth-for-autocad"
typepad_status: "Publish"
---

<p>Many of you will have seen <a href="http://through-the-interface.typepad.com/through_the_interface/2010/04/importing-photosynth-point-clouds-into-autocad-2011-part-1.html" target="_blank">previous</a> <a href="http://through-the-interface.typepad.com/through_the_interface/2010/04/importing-photosynth-point-clouds-into-autocad-2011---part-2.html" target="_blank">incarnations</a> <a href="http://through-the-interface.typepad.com/through_the_interface/2010/04/importing-photosynth-point-clouds-into-autocad-2011---part-3.html" target="_blank">of</a> <a href="http://through-the-interface.typepad.com/through_the_interface/2010/04/importing-photosynth-point-clouds-into-autocad-2011-part-4.html" target="_blank">this</a> <a href="http://through-the-interface.typepad.com/through_the_interface/2010/04/adding-to-autocads-application-menu-and-quick-access-toolbar-using-net.html" target="_blank">tool</a>, <a href="http://through-the-interface.typepad.com/through_the_interface/2010/09/building-an-installer-part-1.html" target="_blank">during</a> <a href="http://through-the-interface.typepad.com/through_the_interface/2010/09/building-an-installer-part-2.html" target="_blank">its</a> <a href="http://through-the-interface.typepad.com/through_the_interface/2010/09/building-an-installer-part-3.html" target="_blank">development</a>. It’s a little more complex than most of our other monthly plugins – mostly as it depends on a couple of external components – but the functionality should hopefully be simple enough to understand and use.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2013487e75b39970c-pi"><img align="right" alt="BrowsePhotosynth for AutoCAD" border="0" height="142" src="/assets/image_122402.jpg" style="margin: 0px 10px 10px; display: inline; border-width: 0px;" title="BrowsePhotosynth for AutoCAD" width="210" /></a>I won’t go into great details here, but if you’re using AutoCAD 2011, give it a try by downloading the ZIP from <a href="http://labs.autodesk.com/utilities/ADN_plugins" target="_blank">the Autodesk Labs Plugin of the Month site</a> and executing the contained installer package. From there you should be able to run the BROWSEPS command inside AutoCAD to load the browser dialog, at which point you can simply browse the Photosynth website: as you encounter point clouds in the browsed content, they will appear in the list on the right-hand side. Clicking on an item in the list will cause it to be downloaded and inserted into your current AutoCAD session as a point cloud object.</p>
<p>For those of you interested in gory details, the code – which is installed with the application – is primarily in C# but also includes an F# module for downloading the various point cloud files asynchronously. This implementation is the subject of my upcoming AU class, <a href="http://au.autodesk.com/?nd=class&amp;session_id=6812" target="_blank">Integrate F# into Your C# or VB.NET Application for an 8x Performance Boost (CP322-2)</a>.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20133f4c76ff2970b-pi"><img alt="BrowsePhotosynth dialog" border="0" height="314" src="/assets/image_707620.jpg" style="margin: 20px auto; display: block; float: none; border-width: 0px;" title="BrowsePhotosynth dialog" width="479" /></a></p>
<p>I’ve been working on – and demoing – this little application for the best part of a year, so am probably too close to it to be appropriately aware of its shortcomings: I’d really appreciate you trying it out and letting me know of any issues, which I’ll do my best to fix in an interim update.</p>
