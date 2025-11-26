---
layout: "post"
title: "Common exceptions while running RealDWG application"
date: "2015-04-20 22:32:49"
author: "Balaji"
categories:
  - ".NET"
  - "2013"
  - "2014"
  - "2015"
  - "2016"
  - "Balaji Ramamoorthy"
  - "RealDWG"
original_url: "https://adndevblog.typepad.com/autocad/2015/04/common-exceptions-while-running-realdwg-application.html "
typepad_basename: "common-exceptions-while-running-realdwg-application"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>In this blog post, we will look at a few common setup related issues that can cause problems in running your RealDWG application.</p>
<p><strong>System.IO.FileNotFoundException</strong> : This exception get thrown usually because your RealDwg Application does not find the dependent dlls.&nbsp;To ensure&nbsp;your RealDwg application accesses the right DLLs is to create an installer for your application with all the dependencies and then installing your application in a clean test machine&nbsp;and test it. But, for testing purposes during development, you can place your RealDWG application in the RealDWG root folder and test it. It is easier to simply set the build output path in the Visual Studio solution to place the output in the RealDWG root folder.</p>
<p>Also, ensure that the [RealDWG root path], [RealDWG root path]\Fonts and [RealDWG root path]\Support folder paths are added to the system's PATH environment variable. You may need to restart Visual Studio if you made the environment variable change when Visual Studio was open.</p>
<p><strong>System.InvalidProgramException</strong> : RealDWG SDK has two versions of acdbmgd.dll just as the ObjectARX SDK does. The dll in RealDWG 2016\Inc folder has its executable code removed and the one in the RealDWG root folder is the unmodified dll. When referencing the "acdbmgd.dll" in your VisualStudio project from the Inc folder, remember to set "Copy Local" property to false for the reference. This is to prevent Visual Studio from replacing the "acdbmgd.dll" in the RealDWG root folder with the one found in "Inc" folder. If this happens, your application can throw a System.InvalidProgramException and the only way to fix it would be to find the "acdbmgd.dll" from the RealDWG install CD and copy it to the RealDWG root folder.</p>
<p></p>
