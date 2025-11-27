---
layout: "post"
title: "OffsetInXref: September&rsquo;s ADN Plugin of the Month, now live on Autodesk Labs"
date: "2009-09-01 13:23:12"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Plugin of the Month"
original_url: "https://www.keanw.com/2009/09/offsetinxref-septembers-adn-plugin-of-the-month-now-live-on-autodesk-labs.html "
typepad_basename: "offsetinxref-septembers-adn-plugin-of-the-month-now-live-on-autodesk-labs"
typepad_status: "Publish"
---

<p>As announced on Scott Sheppard’s blog, <a href="http://labs.blogs.com/its_alive_in_the_lab/2009/09/our-first-plugin-of-the-month.html">It’s Alive in the Lab</a>, you can now find the inaugural ADN Plugin of the Month available for download on <a href="http://labs.autodesk.com/utilities/ADN_plugins">Autodesk Labs</a>. I introduced the concept <a href="http://through-the-interface.typepad.com/through_the_interface/2009/07/coming-soon-to-autodesk-labs-plugin-of-the-month.html">a little over a month ago</a> – thanks to all of you who responded! – and we’ve decided to get things started with OffsetInXref, an AutoCAD plugin allowing you to offset geometry contained in external references without first having to explode them. The code for this plugin can be found in these <a href="http://through-the-interface.typepad.com/through_the_interface/2009/05/enabling-autocads-offset-to-work-on-the-contents-of-xrefs-using-net.html">previous</a> <a href="http://through-the-interface.typepad.com/through_the_interface/2009/05/importing-autocad-layers-from-xrefs-using-net.html">posts</a> on this blog, as well as with the plugin download itself. The inspiration for the plugin came from Mark Doel, from RMJM Hong Kong Ltd., who also helped test the application and has now deployed it successfully to his various users.</p>
<p>I’ve already received a question about the contents of the package, which I’ll reproduce here, in case it helps anyone:</p>
<blockquote>
<p><em>Thanks for the &quot;Offset in Xref&quot; plugin. Simple question for a start: why are there two identical readme.txt files in the ZIP and apparently two identical .dll files? Thanks.</em></p></blockquote>
<p>Here was my response…</p>
<blockquote>
<p>The ReadMe is duplicated because I wanted it to be outside the source folder, but also in the project (so it appears in the solution explorer window inside Visual Studio). When you add a file to a project that&#39;s outside the project folder, Visual Studio copies it into the project folder. So it was a choice: only have it in the project folder (bad), don&#39;t have it in the project (a little less bad), or have the file duplicated (the choice I went for).</p>
<p>You&#39;ll hopefully find the DLLs only to be &quot;apparently&quot; identical: one is for use in 32-bit and the other is for use in 64-bit AutoCAD versions. The source used to build the DLLs is indeed identical, but you need to include different reference assemblies (i.e. different versions of both AcMgd.dll &amp; AcDbMgd.dll) depending on whether you build for a 32- or 64-bit platform. So one (the one found in the win32 folder) should only work on 32-bit AutoCAD, and the other (the one in the x64 folder) should only work on 64-bit AutoCAD.</p></blockquote>
<p>One additional, related point to mention: the 32-bit version will load in 64-bit AutoCAD – but cause it to crash once its code is executed – just as the 64-bit version will do in 32-bit AutoCAD. I’m not sure why this happens, but care should be taken to pick up the correct DLL for the target system.</p>
<p>We’ve had some really interesting “plugin of the month” submissions come in from this blog’s readership – October’s plugin, a very cool Clipboard Manager for AutoCAD, is a great example – and I’m really excited about how these plugins will (hopefully) show people that customizing software – tailoring it to their specific needs – can pay real dividends in terms of productivity. As we also make progress defining a <a href="http://through-the-interface.typepad.com/through_the_interface/2009/06/a-learning-path-for-newbie-programmers.html">learning path for novice programmers</a> (something else that’s on my plate, at the moment), I’m optimistic that more people will feel motivated to take the plunge. :-)</p>
