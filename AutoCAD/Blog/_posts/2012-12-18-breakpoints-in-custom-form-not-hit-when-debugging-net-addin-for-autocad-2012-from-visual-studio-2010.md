---
layout: "post"
title: "Breakpoints in custom form not hit when debugging .NET AddIn for AutoCAD 2012 from Visual Studio 2010"
date: "2012-12-18 19:12:04"
author: "Daniel Du"
categories:
  - ".NET"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Daniel Du"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/breakpoints-in-custom-form-not-hit-when-debugging-net-addin-for-autocad-2012-from-visual-studio-2010.html "
typepad_basename: "breakpoints-in-custom-form-not-hit-when-debugging-net-addin-for-autocad-2012-from-visual-studio-2010"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/daniel-du.html">Daniel Du</a></p>  <p><b>Issue</b></p>  <p>I am trying to create .net add-in based on AutoCAD 2012 with Visual Studio 2010. The breakpoints in common classed can be hit, but those in custom forms cannot be hit. I looked into this post <a href="http://through-the-interface.typepad.com/through_the_interface/2010/04/hitting-breakpoints-in-net-class-libraries-while-debugging-with-visual-studio-2010.html">http://through-the-interface.typepad.com/through_the_interface/2010/04/hitting-breakpoints-in-net-class-libraries-while-debugging-with-visual-studio-2010.html</a>.But it does not help. I tried “Autodesk.Autodesk.ApplicationServices.Application.ShowModalDialog(oForm)” and “oForm.ShowDialog()”, same result. Is there anything else I can do?</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>VS2010 debugger does not work well with fiber; the solution is to turn off fiber in AutoCAD .</p>  <p>If you do not know yet, fibers are being deprecated, as Microsoft is dropping Windows support for fibers. For more information of fibers, please refer to <a href="http://msdn.microsoft.com/en-us/library/ms682661(v=vs.85).aspx">http://msdn.microsoft.com/en-us/library/ms682661(v=vs.85).aspx</a></p>  <p>To turn it off, you can set NEXTFIBERWORLD to 0, close all documents, and AutoCAD will run fiberless in subsequent documents.&#160; The system environment variable FIBERWORLD can show the current status of fiber.</p>  <p>Another thing should mention is: using NEXTFIBERWORLD = 0 the Ribbon might stop working – when you click on a button on ribbon, nothing happens. You can use command line as a workaround.</p>  <p>It applies to other AutoCAD vertical products like Civil 3D, ACA, AME, etc. as well.</p>
