---
layout: "post"
title: "Unexpected behavior with .Net ResourceManager in Revit 2012"
date: "2012-05-08 16:15:13"
author: "Saikat Bhattacharya"
categories:
  - ".NET"
  - "Revit"
  - "Saikat Bhattacharya"
original_url: "https://adndevblog.typepad.com/aec/2012/05/unexpected-behavior-with-net-resourcemanager-in-revit-2012.html "
typepad_basename: "unexpected-behavior-with-net-resourcemanager-in-revit-2012"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/saikat-bhattacharya.html" target="_self">Saikat Bhattacharya</a></p>  <p>The existing Revit apps which uses the ResourceManager to pull out values from a resource file (resx) placed within the assembly worked well with Revit 2011 but stopped working when migrated to Revit 2012. During the migration of Revit 2011 apps to work with Revit 2012, after changing the references to API dlls and changing the target framework to .Net 4.0, the project compiles fine but while loading the app in Revit, it displays an dialog prompting the user to select an assembly that can not be resolved automatically (the dialog is the Assembly File Selector dialog). Some in-depth investigation revealed that this behavior was not related to Revit or its API but was due to the new globalization features in .NET Framework 4. For more details, please refer to:&#160; <a href="http://msdn.microsoft.com/en-us/netframework/dd890508.aspx">http://msdn.microsoft.com/en-us/netframework/dd890508.aspx</a></p>  <p>   <br />Adding the following entry to the AssemblyInfo.cs file helped resolve the problem: </p>  <p><font face="Courier New"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">[</font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt"><font color="#0000ff">assembly</font></span><span style="line-height: 11pt"><font color="#000000">: System.Resources.</font></span><span style="line-height: 11pt"><font color="#2b91af">NeutralResourcesLanguage</font></span><span style="line-height: 11pt"><font color="#000000">(</font></span><span style="line-height: 11pt"><font color="#a31515">&quot;en-US&quot;</font></span></font><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000">)]</font></span></font></p>
