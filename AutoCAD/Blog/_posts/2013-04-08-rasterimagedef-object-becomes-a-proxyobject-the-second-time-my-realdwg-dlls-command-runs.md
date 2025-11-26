---
layout: "post"
title: "RasterImageDef object becomes a ProxyObject the second time my RealDWG dll's command runs"
date: "2013-04-08 07:55:00"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "RealDWG"
original_url: "https://adndevblog.typepad.com/autocad/2013/04/rasterimagedef-object-becomes-a-proxyobject-the-second-time-my-realdwg-dlls-command-runs.html "
typepad_basename: "rasterimagedef-object-becomes-a-proxyobject-the-second-time-my-realdwg-dlls-command-runs"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p><strong>Issue</strong></p>
<p>I have a RealDWG exe application and its installed version runs fine.</p>
<p>However, when I place the same functionality/code in a RealDWG dll and call that from an exe, then in case of its&#0160;installed version,&#0160;the second time I call my function from the dll the RasterImageDef objects are only available as ProxyObjects.</p>
<p><strong>Solution</strong></p>
<p>The <strong>RuntimeSystem.Initialize()</strong> and <strong>Terminate()</strong> functions are only supposed to be called once within an application session.</p>
<p>The easiest solution is&#0160;to have a single static/Shared instance of your <strong>DwgHost</strong> class in your RealDWG dll and its constructor and destructor is implemented the way shown in the below code.</p>
<p>This way even if you provide static/Shared public functions in your RealDWG dll that can be called&#0160;from your other applications, the RealDWG system&#0160;is already initialized and its <strong>Initialize</strong> and <strong>Terminate</strong> functions&#0160;are&#0160;only called once:</p>
<pre>Public Class RealDwgDLL
  Public Class DwgHost
    Inherits Autodesk.AutoCAD.DatabaseServices.HostApplicationServices

    Public Sub New()
      Autodesk.AutoCAD.Runtime.RuntimeSystem.Initialize(Me, 1033)
    End Sub

    Protected Overrides Sub Finalize()
      Autodesk.AutoCAD.Runtime.RuntimeSystem.Terminate()
      MyBase.Finalize()
    End Sub

    &#39; ...

  End Class

  Public Shared mInstance As DwgHost = New DwgHost

  &#39; ...</pre>
