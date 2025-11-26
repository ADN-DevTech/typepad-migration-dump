---
layout: "post"
title: "Enumerate plotters in ActiveX"
date: "2012-09-30 03:20:00"
author: "Xiaodong Liang"
categories:
  - "ActiveX"
  - "AutoCAD"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/autocad/2012/09/enumerate-plotters-in-activex.html "
typepad_basename: "enumerate-plotters-in-activex"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>The following VBA function enumerates all plotters that have corresponding .pc3 files, you can use this in your VBA applications:</p>
<pre>Public Function GetPlotters() As Collection<br />Set GetPlotters = New Collection<br />Dim strPlotter As String<br />strPlotter = Dir(Application.Preferences.Files.PrinterConfigPath + &quot;\*.pc3&quot;)<br />While Not strPlotter = &quot;&quot;<br />&#0160;&#0160; GetPlotters.Add strPlotter<br />&#0160;&#0160; strPlotter = Dir<br />&#0160;&#0160; MsgBox strPlotter<br />Wend<br />End Function</pre>
<p>A method to enumerate system printers (these are also available in the plot dialog of AutoCAD) is available on the following Microsoft knowledge base article:</p>
<p><a href="http://support.microsoft.com/kb/166008">http://support.microsoft.com/kb/166008</a></p>
