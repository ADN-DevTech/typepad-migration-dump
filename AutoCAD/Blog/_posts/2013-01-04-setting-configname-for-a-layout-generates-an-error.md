---
layout: "post"
title: "Setting configname for a layout generates an error"
date: "2013-01-04 22:58:05"
author: "Daniel Du"
categories:
  - "2013"
  - "AutoCAD"
  - "Daniel Du"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/setting-configname-for-a-layout-generates-an-error.html "
typepad_basename: "setting-configname-for-a-layout-generates-an-error"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/daniel-du.html">Daniel Du</a></p>  <p><b>Issue</b></p>  <p>Why do I get an error when I try to set the ConfigName of a Layout?</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>The usual reason for this error is that you are not calling 'RefreshPlotDeviceInfo' before assigning a 'ConfigName'.</p>  <p>Here is an example of some VBA code that does work:</p>  <pre>Sub Test()<br />Dim Layouts As AcadLayouts<br />Dim Layout As AcadLayout<br /><br />' Get the files preferences object<br />Set Layouts = ThisDrawing.Layouts<br /><br />' Change plotter configuration file<br />Layouts(&quot;Model&quot;).RefreshPlotDeviceInfo<br />Layouts(&quot;Model&quot;).ConfigName = &quot;PrinterName&quot;<br /><br />ThisDrawing.Plot.PlotToDevice<br />End Sub</pre>

<p>If you comment out the line 'Layout(&quot;Model&quot;).RefreshPlotDeviceInfo', the routine will work sometimes, but not always.</p>

<p>Please note that you will still generate an error if you try to set a ConfigName that AutoCAD cannot find.</p>
