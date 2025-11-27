---
layout: "post"
title: "AutoCAD DGN Hotfix now available"
date: "2013-07-29 12:31:51"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Drawing structure"
original_url: "https://www.keanw.com/2013/07/autocad-dgn-hotfix-now-available.html "
typepad_basename: "autocad-dgn-hotfix-now-available"
typepad_status: "Publish"
---

<p>I’m just back from a relaxing week spent in <a href="http://en.wikipedia.org/wiki/Ried-Brig" target="_blank">Ried-Brig</a> and <a href="http://en.wikipedia.org/wiki/Valsesia" target="_blank">Valsesia</a>, two very interesting – and in many ways similar – <a href="http://en.wikipedia.org/wiki/Alps" target="_blank">Alpine</a> locations in Switzerland and Italy respectively. We were travelling with close friends of ours – she’s from Ried-Brig, he’s from Valsesia – which meant the week was packed with interesting (and usually “off the beaten track”) activities. For instance, here’s <a href="https://plus.google.com/111739174732269215736/posts/XSY8Xp83uLz" target="_blank">a panoramic photo</a> I shot from the top of the Sacro Monte in <a href="http://en.wikipedia.org/wiki/Varallo_Sesia" target="_blank">Varallo</a>.</p>  <p>Next week I’ll be taking some time off to spend with old friends who are visiting from the UK, but in the meantime I have some things to catch up on.</p>  <p>One of the “good news” emails I had waiting for me this morning was from Albert Rius in Product Support. He has informed me that the <a href="http://usa.autodesk.com/getdoc/id=DL22002791" target="_blank">AutoCAD DGN Hotfix</a> is now available for download.</p>  <p>The hotfix contains two components, this time around: firstly it contains a clean-up tool – a .NET DLL built with the code provided in <a href="http://through-the-interface.typepad.com/through_the_interface/2013/07/minor-update-to-the-dgn-purge-command.html" target="_blank">this post</a> – but it also contains a fix to stop unwanted linestyles from being propagated via copy &amp; paste or external referencing – a .DBX file to replace the one in the standard AutoCAD installation.</p>  <p>The clean-up tool – which defines the DGNPURGE command – should now deal properly with compound linestyles as well as clearing up some data that you’d otherwise have to use the standards PURGE command to remove (after having called DGNPURGE, of course).</p>  <p>According to <a href="http://images.autodesk.com/adsk/files/AutoCAD_2013-2014_DGN_Hotfix_Readme.pdf" target="_blank">the posted Readme</a>, the updated AcDgnLS.dbx file – for which both 32- and 64-bit versions are available – is for AutoCAD 2013 and its verticals. It seems a fixed version of this module is to be included in an upcoming Service Pack for AutoCAD 2014, which presumably explains the fact only 2013 versions have been provided in this hotfix.</p>
