---
layout: "post"
title: "How to set the view in a DWF file using Design Review API?"
date: "2013-07-09 22:37:00"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "ADR API"
  - "DWF"
  - "JavaScript"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/autocad/2013/07/how-to-set-the-view-in-a-dwf-file-using-design-review-api.html "
typepad_basename: "how-to-set-the-view-in-a-dwf-file-using-design-review-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>Setting the
View using Design Review API takes two different approach based on whether you
want to set View for a 2D DWF or 3D DWF.</p>
<p>Using
<strong>EPlotSection.SetView()</strong> function we can set a view for the current section
(page) of the DWF file based on the specified paper-based coordinates. Please
note, this API is available only for 2D sections i.e. <strong>2D DWF</strong> files.</p>
<p>Here is a relevant C# code snippet:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">CompositeViewer = (ECompositeViewer.</span><span style="color: #2b91af; line-height: 140%;">IAdECompositeViewer</span><span style="line-height: 140%;">) axCExpressViewerControl1.ECompositeViewer;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">SectionChk = (ECompositeViewer.</span><span style="color: #2b91af; line-height: 140%;">IAdSection</span><span style="line-height: 140%;">) CompositeViewer.Section;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">SectionTypeChk = (ECompositeViewer.</span><span style="color: #2b91af; line-height: 140%;">IAdSectionType</span><span style="line-height: 140%;">) SectionChk.SectionType;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (SectionTypeChk.Name == </span><span style="color: #a31515; line-height: 140%;">&quot;com.autodesk.dwf.ePlot&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; { </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; PlotSection = (EPlotViewer.</span><span style="color: #2b91af; line-height: 140%;">IAdEPlotSection</span><span style="line-height: 140%;">) CompositeViewer.Section;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; View = (AdCommon.</span><span style="color: #2b91af; line-height: 140%;">IAdPageView</span><span style="line-height: 140%;">)PlotSection.View;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">//Set View - interchange Left with Bottom and Top with Right </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; PlotSection.SetView (View.Bottom, View.Left, View.Top,&#0160; View.Right);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>&#0160;</p>
<p>In 3D, using
<strong>EModelSection.Camera</strong> property, we can access the camera object of a specific 3D
section in the DWF that is currently loaded in the canvas. Then it can be
modified using the <strong>EModelCamera</strong> object, to access various properties of the
camera.</p>
<p><strong>EModelCamera.Position</strong>
property -&gt; Returns the current position of the camera. We can use [set]
method of this property to set the position of the camera to a known set of
coordinates (represented by IAdPoint object).</p>
