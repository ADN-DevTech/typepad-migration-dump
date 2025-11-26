---
layout: "post"
title: "AutoCAD .net Wizard supports Map 3D, Civil 3D, AutoCAD Architecture and AutoCAD MEP now"
date: "2012-05-02 03:31:27"
author: "Daniel Du"
categories:
  - "AutoCAD Civil 3D 2013"
  - "AutoCAD Map 3D 2013"
  - "Daniel Du"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/05/autocad-net-wizard-support-map-3d-civil-3d-autocad-architecture-and-autocad-mep-now.html "
typepad_basename: "autocad-net-wizard-support-map-3d-civil-3d-autocad-architecture-and-autocad-mep-now"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/daniel-du.html">Daniel Du</a></p>
<p>You must be familiar with the <a href="http://images.autodesk.com/adsk/files/autocad_2013_dotnet_wizards.zip" target="_blank">AutoCAD .NET Wizard</a>, which helps you to create a AutoCAD .net Plug-in easily, and also enables debugging from Visual Studio Express.</p>
<p>AutoCAD.Net Wizard now has been upgraded to support AutoCAD 2013 and Visual Studio 2010, it is also extended to other AutoCAD vertical products, including Map 3D 2013, Civil 3D 2013, AutoCAD Architecture 2013 and AutoCAD MEP 2013.</p>
<p>You can find the “AutoCAD 2013 CSharp plug-in” wizard when creating new project</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016765ff7bfe970b-pi"><img alt="image" border="0" height="298" src="/assets/image_baaffd.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="457" /></a></p>
<p>The VB.net wizard also is available.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0163050c239f970d-pi"><img alt="image" border="0" height="319" src="/assets/image_b78682.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="468" /></a></p>
<p>The AutoCAD tab is always available. For Map 3D 2013,&#0160; “Map 3D” tab is available, you can select the Map 3D related references.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168eb01e26b970c-pi"><img alt="image" border="0" height="409" src="/assets/image_6ee721.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="379" /></a></p>
<p>There is a trick here.This wizard is supposed to use for Map 3D 2013, but it works for Map 3D 2012 as well.&#0160; if you are using Map 3D 2012, please check “Autodesk.Platform.Core(for 2012)”, and delete the reference of acCoreMgd from references of Visual Studio.</p>
<p>For Civil 3D, Civil 3D related references can be checked from “Civil 3D” tab.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0163050c23e0970d-pi"><img alt="image" border="0" height="415" src="/assets/image_538ad0.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="384" /></a></p>
<p>&#0160;</p>
<p>It also works for Architecture 2013 and AutoCAD MEP 2013, please add references from “ACA” and “AME” tab, so download a copy and give it a try.</p>
<p>It can be downloaded from AutoCAD Developer Center:</p>
<p><a href="http://images.autodesk.com/adsk/files/autocad_2013_dotnet_wizards.zip" title="http://images.autodesk.com/adsk/files/autocad_2013_dotnet_wizards.zip">http://images.autodesk.com/adsk/files/autocad_2013_dotnet_wizards.zip</a></p>
