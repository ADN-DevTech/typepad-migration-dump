---
layout: "post"
title: "COM References on Civil 3D 2013"
date: "2012-04-22 15:10:09"
author: "Augusto Goncalves"
categories:
  - "Augusto Goncalves"
  - "AutoCAD Civil 3D 2013"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/04/com-references-on-civil-3d-2013.html "
typepad_basename: "com-references-on-civil-3d-2013"
typepad_status: "Publish"
---

<p>By Augusto Goncalves</p>
<p>Now with this new release, the COM references are not registered on the system, therefore we cannot add the from the traditional ‘COM’ tab we adding references, but use the ‘Browse’ tab to search them at the Civil 3D install folder. To seem the references, go to the folder and search for *interop*.dll, like shown below.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0163049aa809970d-pi"><img alt="c3d_2013_refs" border="0" height="213" src="/assets/image_241965.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: block; float: none; margin-left: auto; margin-right: auto; padding-top: 0px; border: 0px;" title="c3d_2013_refs" width="244" /></a></p>
<p>To use if on your .NET project, go to ‘Add reference’, then select ‘Browse’ tab, then locate the required assemblies at the Civil 3D folder. By default, Visual Studio 2010 (all versions) mark these references as ‘Embed Interop Types’ by leaving it as True. This is required, as the references as not registered, the plug-in DLL must include the type definitions.</p>
<p><strong>Update</strong></p>
<p>There is a change on Civil 3D 2013: some of the COM types now have GUIDs, therefore you need to compile using 64 bit references for 64 bit OS, and replace the references and compile again for 32 bit. A common symptom of these wrong references is the runtime error showing &#39;Unable to cast COM object&#39;.</p>
<p>This blog post was changed to reflect this updated information.</p>
