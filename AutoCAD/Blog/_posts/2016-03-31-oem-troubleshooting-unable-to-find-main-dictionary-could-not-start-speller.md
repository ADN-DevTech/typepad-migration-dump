---
layout: "post"
title: "OEM Troubleshooting: Unable to find main dictionary, Could Not Start Speller"
date: "2016-03-31 17:31:00"
author: "Madhukar Moogala"
categories:
  - "AutoCAD OEM"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2016/03/oem-troubleshooting-unable-to-find-main-dictionary-could-not-start-speller.html "
typepad_basename: "oem-troubleshooting-unable-to-find-main-dictionary-could-not-start-speller"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p> <p>&nbsp;</p> <p><strong>Q. </strong>When I run my app and hit any key at AutoCAD command line, I get a AutoCAD dialog saying:<br>"<em>Unable to find main dictionary. Could not start speller"<br></em>AutoCAD command line then reports: "Can't find speller module: AcSpellEng.dll"<br>What am I supposed to do in the OEM wizard to get this going?</p> <p><strong>A</strong>. </p> <p>It seems that Copying and Patching Spell files has not taken place while building your OEM application, this is because "_Spell" command has not enabled in your commands page.  <p>OEM Wizard while creating stamp.bat file&nbsp; wizard initiates stamping for each enabled command such that all dependencies are copied to your project folder [.\AutoCAD OEM XXXX- English\Projects\&lt;OEMAPP&gt;\Toolkit]  <p>XXXX stands for version for e.g. 2016.  <p>So enable full support for "'_Spell'", after building application, open &lt;oemapp&gt;stamp.bat and find for :SPELLDONE, you 'll find stamping of AcSpell.dll takes place.
