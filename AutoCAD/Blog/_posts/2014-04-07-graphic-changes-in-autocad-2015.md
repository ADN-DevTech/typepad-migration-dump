---
layout: "post"
title: "Graphic changes in AutoCAD 2015:"
date: "2014-04-07 00:34:41"
author: "Virupaksha Aithal"
categories:
  - ".NET"
  - "2015"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2014/04/graphic-changes-in-autocad-2015.html "
typepad_basename: "graphic-changes-in-autocad-2015"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Virupaksha-Aithal.html" target="_self">Virupaksha Aithal</a></p>
<p>Below are some of the important Graphic changes with respect to AutoCAD 2015.</p>
<p><strong>To create “CreateAutoCADOffScreenDevice”</strong></p>
<p><strong>.NET</strong></p>
<p>KernelDescriptor descriptor&#0160; = new KernelDescriptor();</p>
<p>descriptor.addRequirement(Autodesk.AutoCAD.UniqueString.Intern(&quot;3D Drawing&quot;));</p>
<p>GraphicsKernel kernal =&#0160;Manager.AcquireGraphicsKernel(descriptor);</p>
<p>Device dev =&#0160;gsm.CreateAutoCADOffScreenDevice(kernal);</p>
<p><strong>ObjectARX:</strong></p>
<p>descriptor.addRequirement(AcGsKernelDescriptor::k3DDrawing);<br />AcGsGraphicsKernel *pGraphicsKernel = <br /> AcGsManager::acquireGraphicsKernel(descriptor);</p>
<p>AcGsDevice *offDevice = pGraphicsKernel-&gt;createOffScreenDevice();</p>
<p><strong>To create “CreateAutoCADModel”</strong></p>
<p><strong><strong>.NET</strong></strong></p>
<p>Model model = gsm.CreateAutoCADModel(kernal);</p>
<p><strong>ObjectARX:</strong></p>
<p>AcGsModel *pModel = gsManager-&gt;createAutoCADModel(*pGraphicsKernel);</p>
<p><strong>Regarding Autodesk.AutoCAD.GraphicsSystem.RenderMode:</strong></p>
<p>RenderMode is retried now. Use visual style in View.</p>
<p><strong>.NET</strong></p>
<p>view.VisualStyle =&#0160;new VisualStyle(VisualStyleType.Realistic);</p>
<p><strong>ObjectARX:</strong></p>
<p>pView-&gt;setVisualStyle(AcGiVisualStyle::kGouraud);</p>
<p>Download&#0160;<a href="http://adndevblog.typepad.com/autocad/Downloads/BlockView-2015.zip" target="_self" title="ObjectARX">ObjectARX</a>&#0160;and&#0160;<a href="http://adndevblog.typepad.com/autocad/Downloads/BlockView.NET.zip" target="_self">.NET</a>&#0160;&#0160;Block View sample for AutoCAD 2015.</p>
