---
layout: "post"
title: "Get path of file on clipboard "
date: "2013-03-06 09:10:56"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2013/03/get-path-of-file-on-clipboard-.html "
typepad_basename: "get-path-of-file-on-clipboard-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>As mentioned in <a href="http://through-the-interface.typepad.com/through_the_interface/2010/10/previewing-the-contents-of-the-clipboard-in-an-autocad-palette-using-net.html" target="_self">Kean&#39;s blog post</a>, when you select entities in a drawing inside AutoCAD and run _COPYCLIP (Ctrl+C) then AutoCAD will WBLOCK out the selected entities into a temp DWG file. The content that&#39;s placed on the clipboard will also contain the path to that temp file. In case you would like to use that file, here is one way to find out its path:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">Public</span> <span style="color: blue;">Function</span> GetClipboardFilePath() <span style="color: blue;">As</span> <span style="color: blue;">String</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">Dim</span> dataObject <span style="color: blue;">As</span> <span style="color: #2b91af;">IDataObject</span> = </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; System.Windows.<span style="color: #2b91af;">Clipboard</span>.GetDataObject()</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: green;">&#39; Get the list of available formats</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">Dim</span> formats() <span style="color: blue;">As</span> <span style="color: blue;">String</span> = dataObject.GetFormats()</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">For</span> <span style="color: blue;">Each</span> format <span style="color: blue;">As</span> <span style="color: blue;">String</span> <span style="color: blue;">In</span> formats</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: green;">&#39; In case of AutoCAD 2013 this would be &quot;AutoCAD.R19&quot;</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">If</span> format.StartsWith(<span style="color: #a31515;">&quot;AutoCAD.&quot;</span>) <span style="color: blue;">Then</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">Using</span> stream <span style="color: blue;">As</span> <span style="color: #2b91af;">MemoryStream</span> = dataObject.GetData(format)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: green;">&#39; Unicode encoding is 16 bit (2 bytes) just like ACHAR</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Using</span> reader <span style="color: blue;">As</span> <span style="color: #2b91af;">TextReader</span> = </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">New</span> <span style="color: #2b91af;">StreamReader</span>(stream, System.Text.<span style="color: #2b91af;">Encoding</span>.Unicode) </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">&#39; 0..259 = 260 </span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> text(259) <span style="color: blue;">As</span> <span style="color: blue;">Char</span> </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; reader.Read(text, 0, 260)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">&#39; There will be lots of 0 value characters in the array</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">&#39; that we need to clean up&#0160;&#0160; </span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Return</span> <span style="color: blue;">New</span> <span style="color: blue;">String</span>(text).TrimEnd(<span style="color: blue;">New</span> <span style="color: blue;">Char</span>(){Chr(0)})&#0160;&#0160; </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">End</span> <span style="color: blue;">Using</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">End</span> <span style="color: blue;">Using</span> </p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">Next</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">Return</span> <span style="color: blue;">Nothing</span></p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">End</span> <span style="color: blue;">Function</span></p>
</div>
