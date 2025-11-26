---
layout: "post"
title: "Revit add-in does not do anything after clicking the add-in command in the user interface"
date: "2012-05-21 17:37:00"
author: "Saikat Bhattacharya"
categories:
  - ".NET"
  - "Revit"
  - "Saikat Bhattacharya"
original_url: "https://adndevblog.typepad.com/aec/2012/05/revit-add-in-does-not-do-anything-after-clicking-the-add-in-command-in-the-user-interface.html "
typepad_basename: "revit-add-in-does-not-do-anything-after-clicking-the-add-in-command-in-the-user-interface"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/saikat-bhattacharya.html" target="_self">Saikat Bhattacharya</a></p>  <p>Quite often we get partners/developers saying that their when their customers click on the add-in command under <em>External Tools</em> in <em>Add-Ins</em> tab in Revit UI, nothing happens. The same plug-in works on their system (development machine). This short blog-post covers this commonly asked question. </p>  <p>With add-ins created using .NET framework 4.0, this framework implements a slightly more stringent security than the predecessor frameworks. So in many cases, if a plug-in DLL (including that of Revit) has been downloaded from the web or has been loaded from a network share, this has often resulted in reduced set of privileges on the local machine. A common result of this has been such reports from API users who mention that a specific plug-in used to work in the development machine but does not do anything in client's machine (even though the manifest file and its contents are correct), when they click on the plug-in command in the Revit UI under Add-Ins tab. </p>  <p>The solution to this problem is to “Unblock” the DLL by right-clicking on it in Windows Explorer and selecting “Properties” and “Unblock”. You can read up more on this topic from the following public blog-post :</p>  <p><a href="http://labs.blogs.com/its_alive_in_the_lab/2011/05/unblock-net.html">http://labs.blogs.com/its_alive_in_the_lab/2011/05/unblock-net.html</a></p>
