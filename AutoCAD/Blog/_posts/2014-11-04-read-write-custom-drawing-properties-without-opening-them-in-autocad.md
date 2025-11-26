---
layout: "post"
title: "Read / Write custom drawing properties without opening drawing in AutoCAD"
date: "2014-11-04 21:41:34"
author: "Balaji"
categories:
  - ".NET"
  - "2013"
  - "2014"
  - "2015"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2014/11/read-write-custom-drawing-properties-without-opening-them-in-autocad.html "
typepad_basename: "read-write-custom-drawing-properties-without-opening-them-in-autocad"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>In some cases, it might be required to read / write custom properties for AutoCAD drawings without having to open the drawing in AutoCAD. If you already have AutoCAD installed in the system, using accoreconsole.exe provides an easy way to achieve this. Here are the steps :</p>
<p>1) Create a .Net plugin that only references acdbmgd.dll and accoremgd.dll and implement a custom command with this code snippet :</p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> Document activeDoc = </pre>
<pre style="margin:0em;">     Autodesk.AutoCAD.ApplicationServices.Core.</pre>
<pre style="margin:0em;">     Application.DocumentManager.MdiActiveDocument;</pre>
<pre style="margin:0em;"> Database db = activeDoc.Database;</pre>
<pre style="margin:0em;"> DatabaseSummaryInfoBuilder builder </pre>
<pre style="margin:0em;">                 = <span style="color:#0000ff">new</span><span style="color:#000000">  DatabaseSummaryInfoBuilder();</pre>
<pre style="margin:0em;"> builder.Author = <span style="color:#a31515">&quot;GoCAD&quot;</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> builder.CustomPropertyTable.Add</pre>
<pre style="margin:0em;">                         (<span style="color:#a31515">&quot;Machine Type&quot;</span><span style="color:#000000"> , <span style="color:#a31515">&quot;Milling&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> builder.CustomPropertyTable.Add(<span style="color:#a31515">&quot;Model&quot;</span><span style="color:#000000"> , <span style="color:#a31515">&quot;Vertical&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> builder.CustomPropertyTable.Add(<span style="color:#a31515">&quot;Year&quot;</span><span style="color:#000000"> , <span style="color:#a31515">&quot;2014&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> db.SummaryInfo = builder.ToDatabaseSummaryInfo();</pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
<p>2) Create a AutoCAD script file to load the plugin and invoke the custom command. Save the script as a scr file.</p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color: black; overflow: auto; width: 99.5%;">
<pre style="margin: 0em;"> <span style="color: #008000;">//Script starts here</span></pre>
<pre style="margin: 0em;"> ; Load the custom plugin</pre>
<pre style="margin: 0em;"> (command <span style="color: #a31515;">"_.Netload"</span><span style="color: #000000;">  <span style="color: #a31515;">"D:\\\\Temp\\\\Test.dll"</span><span style="color: #000000;"> )</span></span></pre>
<pre style="margin: 0em;"> ; Run the command</pre>
<pre style="margin: 0em;"> (command <span style="color: #a31515;">"WriteCustomProp"</span><span style="color: #000000;"> )</span></pre>
<pre style="margin: 0em;"> save</pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> <span style="color: #008000;">//Script ends here</span></pre>
<pre style="margin: 0em;">&nbsp;</pre>
</div>
<!-- End block -->
<p>3) Run AccoreConsole.exe found in the AutoCAD 2015 install folder and provide the drawing to which custom properties are to be added.</p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> accoreconsole.exe /i <span style="color:#a31515">&quot;D:\\Temp\\Test.dwg&quot;</span><span style="color:#000000">  </pre>
<pre style="margin:0em;">                   /s <span style="color:#a31515">&quot;D:\\Temp\\WriteProp.scr&quot;</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                   /l en-US</pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
<p>If you do not have AutoCAD installed in the system, it is possible to read the custom properties using the COM API provided by DwgPropX ActiveX control. But this activeX does not provide write access to the properties. You can read about this approach in this blog post : </p>
<a href="http://adndevblog.typepad.com/autocad/2013/01/access-drawing-properties-outside-autocad.html">Access drawing properties outside AutoCAD</p>
