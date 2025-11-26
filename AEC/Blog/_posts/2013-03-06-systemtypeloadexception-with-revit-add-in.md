---
layout: "post"
title: "System.TypeLoadException with Revit Add-in"
date: "2013-03-06 14:56:29"
author: "Saikat Bhattacharya"
categories:
  - ".NET"
  - "Revit"
  - "Saikat Bhattacharya"
original_url: "https://adndevblog.typepad.com/aec/2013/03/systemtypeloadexception-with-revit-add-in.html "
typepad_basename: "systemtypeloadexception-with-revit-add-in"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/saikat-bhattacharya.html">Saikat Bhattacharya</a></p>  <p>I have had couple of emails in recent past with different third party developers providing me with their Revit add-in samples, where they are stuck because of the following System.TypeLoadException. </p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c375dc05c970b-pi"><img style="background-image: none; border-bottom: 0px; border-left: 0px; padding-left: 0px; padding-right: 0px; display: block; float: none; margin-left: auto; border-top: 0px; margin-right: auto; border-right: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_303652.jpg" width="296" height="235" /></a></p>  <p>As mentioned <a href="http://msdn.microsoft.com/en-us/library/system.typeloadexception.aspx" target="_blank">here</a>, the reason for this exception typically is that CLR did not find the assembly, or the type within the assembly or could not load the type. This in most cases that I have seen with Revit add-ins seems to commonly due to a mismatch between the .NET framework and the Revit version being used to develop the add-in. There are also couple of discussions on this topic in the <a href="http://forums.autodesk.com/t5/Autodesk-Revit-API/bd-p/160" target="_blank">Revit API discussion forum</a>.&#160;&#160; </p>  <p>In the recent case, I had spent quite sometime confirming that the correct for the .NET framework was being targeted for in the VS project, the version of the Revit API references, and other settings and even double checked that entries in the .addin manifest file – all of which seemed perfect. Next, I started reading through the VB.NET code and immediately saw Namespace was provided in the code as well as in the <em>Root Namespace</em> field in the VB.NET project settings. Once I removed the Namespace entry from the code, the Add-in loaded up just fine in Revit. This would be a typical case whenever we end up converting C# code to VB.NET. Something to watch out for!</p>
