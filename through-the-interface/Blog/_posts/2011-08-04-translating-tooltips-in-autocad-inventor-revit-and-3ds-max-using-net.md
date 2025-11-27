---
layout: "post"
title: "Translating tooltips in AutoCAD, Inventor, Revit and 3ds Max using .NET"
date: "2011-08-04 12:42:40"
author: "Kean Walmsley"
categories:
  - "3ds Max"
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Inventor"
  - "Revit"
  - "User interface"
original_url: "https://www.keanw.com/2011/08/translating-tooltips-in-autocad-inventor-revit-and-3ds-max-using-net.html "
typepad_basename: "translating-tooltips-in-autocad-inventor-revit-and-3ds-max-using-net"
typepad_status: "Publish"
---

<p>After adding <a href="http://through-the-interface.typepad.com/through_the_interface/2011/07/translating-tooltips-in-both-autocad-and-revit-using-net.html" target="_blank">Revit</a> and then <a href="http://through-the-interface.typepad.com/through_the_interface/2011/07/translating-tooltips-inside-autocad-inventor-and-revit-using-net.html" target="_blank">Inventor</a> support to <a href="http://through-the-interface.typepad.com/through_the_interface/2011/07/automatic-translation-of-autocad-tooltips-using-net.html" target="_blank">the original</a> <a href="http://through-the-interface.typepad.com/through_the_interface/2011/07/caching-translations-of-autocad-tooltips-using-net.html" target="_blank">AutoCAD application</a>, it made sense to go ahead and include support for 3ds Max. <a href="http://through-the-interface.typepad.com/files/TransTips-AutoCAD_Inventor_Revit_3dsMax.zip" target="_blank">Here’s a solution supporting these four products (and their verticals)</a>. And I can now confirm that a version of this application will be September’s <a href="http://labs.autodesk.com/utilities/ADN_plugins/" target="_blank">Plugin of the Month</a> on <a href="http://labs.autodesk.com/" target="_blank">Autodesk Labs</a>.</p>
<p>You may have noticed a lot of UI consistency introduced across these Autodesk products, in recent years, mainly due to a coordinated push from our product teams for a more consistent user experience. An internal acronym was used for the project driving consistency across these four products, which happens to be the name of a famous Nike running shoe (any guesses? ;-). We tend not to use the name externally – as it’s an existing product name – but the project has done a fantastic job of driving consistency and technology sharing across these products, ultimately paving the way for the introduction of <a href="http://autodesk.com/suites" target="_blank">suites</a>.</p>
<p>It’s this sharing of component technology across our products – in particular of the <em>AdWindows.dll</em> module – that has enabled this application to work across all these products with a relatively small amount of product-specific code.</p>
<p>Here’s a quick screenshot of TransTips working inside 3ds Max. With 2012 there’s a fairly lightweight ribbon implementation, which means this tool doesn’t really help translate very much of 3ds Max’s functionality, but it’s something. The below image shows a tooltip being translated into Latvian:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20154343f70ea970c-pi" target="_blank"><img alt="TransTips working in 3ds Max" border="0" height="319" src="/assets/image_484402.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border: 0px;" title="TransTips working in 3ds Max" width="450" /></a></p>
<p>For now I’ve held off on the crowdsourcing-related enhancements (including the hooks and UI for direct XML editing). It’s likely I’ll come back to this, at some point, but for now I’m going to wait and see how the plugin is received (and used).</p>
