---
layout: "post"
title: "Object enabler's interop assembly references the latest AutoCAD interop dll instead of the version the COM wrapper was built with"
date: "2012-05-28 08:14:33"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/object-enablers-interop-assembly-references-the-latest-autocad-interop-dll-instead-of-the-version-the-com-wrapper-was-built.html "
typepad_basename: "object-enablers-interop-assembly-references-the-latest-autocad-interop-dll-instead-of-the-version-the-com-wrapper-was-built"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I created a custom entity and made sure that its COM wrapper is using the AutoCAD 2010 tlb's, but when I'm trying to use it from a .NET AddIn I run into problems for the interop dll created for it automatically by Visual Studio is referencing the AutoCAD 2011 interop dll's and so I get an error when compiling my project: <br />"error BC32206: The project currently contains references to more than one version of Autodesk.AutoCAD.Interop.Common, a direct reference to version 18.0.0.0 and an indirect reference (through 'AEN1MyLineOE3Lib.MyLineCom') to version 18.1.0.0. Change the direct reference to use version 18.1.0.0 (or higher) of Autodesk.AutoCAD.Interop.Common."</p>
<p>If I uninstall AutoCAD 2011, then of course everything is fine, but I'd prefer a different solution.</p>
<p><a name="section2"></a></p>
<p><strong>Solution</strong></p>
<p>When you reference a tlb from Visual Studio then it's using tlbimp in the background to create an interop dll for it. If we do it ourselves then we can check what's going on during the interop making process using the /verbose option of tlbimp.exe.</p>

<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168ebe0b404970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0168ebe0b404970c image-full" alt="_tlbimp-default" title="_tlbimp-default" src="/assets/image_103146.jpg" border="0" /></a><br />
<p>As you can see the primary interop assembly is being used - which is from AutoCAD 2011.</p>
<p>However with the /reference option you can set which interop assembly to use:</p>

<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016305eb69e1970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b016305eb69e1970d image-full" alt="_tlbimp" title="_tlbimp" src="/assets/image_547434.jpg" border="0" /></a><br />
<p>Now that the interop assembly used the AutoCAD 2010 interop assembly, everything was fine when I referenced my interop assembly in the .NET AddIn.</p>
