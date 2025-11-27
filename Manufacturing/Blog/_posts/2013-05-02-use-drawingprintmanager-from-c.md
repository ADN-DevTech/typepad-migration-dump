---
layout: "post"
title: "Use DrawingPrintManager from C++"
date: "2013-05-02 05:16:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/05/use-drawingprintmanager-from-c.html "
typepad_basename: "use-drawingprintmanager-from-c"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Here is a sample code that is using the DrawingPrintManager object to print the active drawing document:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px; line-height: 120%;"><span style="color: green;">// Make sure a Drawing Document is active</span></p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">static</span> <span style="color: blue;">void</span> PrintDrawing(CComPtr&lt;Application&gt; app)</p>
<p style="margin: 0px; line-height: 120%;">{</p>
<p style="margin: 0px; line-height: 120%;">&#0160; CComQIPtr&lt;DrawingPrintManager&gt; mgr =&#0160; </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; app-&gt;ActiveDocument-&gt;GetPrintManager();</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; MessageBox(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; NULL, mgr-&gt;Printer, _T(<span style="color: #a31515;">&quot;Currently selected printer&quot;</span>), MB_OK);</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; mgr-&gt;PutPrinter(_T(<span style="color: #a31515;">&quot;\\\\adsbeidc1\\HP LaserJet 5000 Series PCL&quot;</span>));</p>
<p style="margin: 0px; line-height: 120%;">&#0160; mgr-&gt;ColorMode = kPrintColorPalette;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; mgr-&gt;NumberOfCopies = 1;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; mgr-&gt;Orientation = kLandscapeOrientation;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; mgr-&gt;ScaleMode = kPrintBestFitScale;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; mgr-&gt;SetSheetRange(1, 1);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; mgr-&gt;PaperSize = kPaperSizeA4;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; MessageBox(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; NULL, mgr-&gt;Printer, _T(<span style="color: #a31515;">&quot;Newly selected printer&quot;</span>), MB_OK);</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">if</span> (FAILED(mgr-&gt;SubmitPrint()))</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; MessageBox(</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; NULL, _T(<span style="color: #a31515;">&quot;Error with SubmitPrint&quot;</span>), _T(<span style="color: #a31515;">&quot;Error&quot;</span>), MB_OK);</p>
<p style="margin: 0px; line-height: 120%;">}</p>
</div>
