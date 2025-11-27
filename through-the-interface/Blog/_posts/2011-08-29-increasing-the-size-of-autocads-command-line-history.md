---
layout: "post"
title: "Increasing the size of AutoCAD&rsquo;s command line history"
date: "2011-08-29 13:41:26"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
original_url: "https://www.keanw.com/2011/08/increasing-the-size-of-autocads-command-line-history.html "
typepad_basename: "increasing-the-size-of-autocads-command-line-history"
typepad_status: "Publish"
---

<p>Ever been frustrated when scrolling up in the AutoCAD text window, only to find information from earlier in that session had disappeared? I find this when listing AutoCAD commands and system variables, in particular. The standard command-line history is 400 lines, as far as I can tell, but the good news is that there’s a Registry setting allowing you to increase this amount.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2015434eda4c0970c-pi" target="_blank"><img alt="AutoCAD&#39;s text window" border="0" height="319" src="/assets/image_114517.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="AutoCAD&#39;s text window" width="420" /></a></p>
<p>[Thanks to various people in AutoCAD Engineering for tracking down this information (including Asheem Mamoowala, Karen Mason and Randy Kintzley).]</p>
<p>In AutoCAD R14, we had a Registry setting in the current profile exposed via an option in the old Preferences dialog. While the ability to edit the setting inside the product went away in AutoCAD 2000, the underlying Registry entry is still respected.</p>
<p>It’s called CmdHistLines, and is a DWORD setting with a valid range of 25-2048. Here’s the .reg file I used to set this to its maximum value for AutoCAD 2012 on my system:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">Windows Registry Editor Version 5.00</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">[HKEY_CURRENT_USER\Software\Autodesk\AutoCAD\R18.2\ACAD-A001:409\Profiles\&lt;&lt;Unnamed Profile&gt;&gt;\General]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&quot;CmdHistLines&quot;=dword:00000800</span></p>
</div>
<p>The results are as you’d expect: using the SETVAR command to list the current system variables no longer fills the history buffer. You should be able to scroll back all the way to the beginning of the session.</p>
<p>[Finally, a complementary tip from Fenton Webb: if you set QAFLAGS to 2, this enables full scrolling, which means you don’t have to keep pressing the Return key to see the entire results of “ARX C *” or “SETVAR ? *”. Thanks, Fents! :-)]</p>
