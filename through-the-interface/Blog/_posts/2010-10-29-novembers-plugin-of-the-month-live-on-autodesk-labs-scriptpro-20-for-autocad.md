---
layout: "post"
title: "November&rsquo;s Plugin of the Month live on Autodesk Labs: ScriptPro 2.0 for AutoCAD"
date: "2010-10-29 18:36:38"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Batch processing"
  - "Plugin of the Month"
  - "User interface"
  - "WPF"
original_url: "https://www.keanw.com/2010/10/novembers-plugin-of-the-month-live-on-autodesk-labs-scriptpro-20-for-autocad.html "
typepad_basename: "novembers-plugin-of-the-month-live-on-autodesk-labs-scriptpro-20-for-autocad"
typepad_status: "Publish"
---

<p><a href="http://through-the-interface.typepad.com/through_the_interface/2010/10/sortonversion-scriptpro.html" target="_blank">As mentioned previously</a>, Viru Aithal, from DevTech India, has been working hard on a replacement for <a href="http://autodesk.blogs.com/between_the_lines/2009/04/autocad-scriptpro-updated-for-autocad-2010.html" target="_blank">the venerable ScriptPro tool</a>. I’m delighted to say that it’s now ready, and is <a href="http://labs.blogs.com/its_alive_in_the_lab/2010/10/adn-plugin-of-the-month-scriptpro-for-autocad-now-available.html" target="_blank">live on Autodesk Labs</a> as November’s <a href="http://labs.autodesk.com/utilities/ADN_plugins/" target="_blank">Plugin of the Month</a>.</p>  <p>Viru took the codebase he developed for the <a href="http://through-the-interface.typepad.com/through_the_interface/2010/03/marchs-plugin-of-the-month-live-on-autodesk-labs-batch-publish-for-autocad.html" target="_blank">DWF/PDF Batch Publish tool</a> and created ScriptPro 2.0, written from the ground up in C# and developed without any dependency on a specific AutoCAD version. Viru’s approach uses the equivalent of late binding to call into AutoCAD through COM and so doesn’t require a specific AutoCAD Type Library. Which means the tool should also work for future versions of AutoCAD: there’s no longer any need to wait for an update to the tool. And one advantage of being written in a .NET language is that the application works as well on 64-bit platforms as it does on 32-bit ones (a historical frustration of ScriptPro users).</p>  <p>ScriptPro 2.0 has a new project format – with the file extension .BPL (for Batch Process List). While it can load old ScriptPro projects (.SCP), it doesn’t yet have perfect feature parity – there are some ScriptPro-specific keywords that have not yet been implemented in the 2.0 release, but (providing we hear it’s important to do so) we’ll get there.</p>  <p>The tool has a ribbon UI – built using WPF – but the bulk of the functionality is packaged up in a WinForms User Control and hosted by the WPF app (me may yet move the whole thing to WPF, in time). Here’s the new UI:</p>  <p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20134888f61e6970c-pi"><img style="border-right-width: 0px; margin: 20px auto; display: block; float: none; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px" title="ScriptPro 2.0" border="0" alt="ScriptPro 2.0" src="/assets/image_910204.jpg" width="473" height="284" /></a></p>  <p>And the best thing is that – as a Plugin of the Month – it comes with the complete source code for you to dig into and extend. Be sure to take a look at this very exciting new tool and <a href="mailto:labs.plugins@autodesk.com" target="_blank">let us know what you think</a>.</p>
